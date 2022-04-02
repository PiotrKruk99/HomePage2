using LiteDB;

namespace homePage2.Models;

public static class LiteDBOper
{
    private const string liteDBPath = @"AppData/appData.ldb";
    // private static Dictionary<string, string> collNames =
    //     new Dictionary<string, string>() { { "users", "users" } };
    private static (string users, string) collNames = (users: "users", "");

    private static LiteDatabase? OpenLDB()
    {
        if (!Directory.Exists(Path.GetDirectoryName(liteDBPath)))
            return null;

        ConnectionString connStr = new ConnectionString
        {
            Filename = liteDBPath,
            Connection = ConnectionType.Shared
        };

        LiteDatabase ldb;
        try
        {
            ldb = new LiteDatabase(connStr);
        }
        catch (LiteException)
        {
            return null;
        }

        return ldb;
    }

    public static ResultMsg CheckAdmin()
    /*returns 1 on admin exists, 0 on admin not exists 
    and -1 on error on communication with database*/
    {
        var ldb = OpenLDB();

        if (ldb != null)
        {
            var cols = ldb.GetCollection<User>(collNames.users);//"users"]);
            var col = cols.FindOne(x => x.Name.Equals("admin"));

            if (col != null)
            {
                if (col.ExpireDate < DateTime.Now)
                {
                    cols.Delete(col.Id);
                    ldb.Dispose();
                    return new ResultMsg(false, "admin not exists", ResultMsg.ResultType.warning, 0);
                }

                ldb.Dispose();
                return new ResultMsg(true, "admin exists", ResultMsg.ResultType.info, 1);
            }
            else
            {
                ldb.Dispose();
                return new ResultMsg(false, "admin not exists", ResultMsg.ResultType.warning, 0);
            }
        }

        return new ResultMsg(false, "error connecting to database", ResultMsg.ResultType.danger, -1);
    }

    public static ResultMsg CheckAdminsAuthString(string authString)
    {
        var ldb = OpenLDB();

        if (ldb != null)
        {
            var col = ldb.GetCollection<User>(collNames.users).FindOne(x => x.Name.Equals("admin"));

            if (col == null)
            {
                return new ResultMsg(false, "no admin entry", ResultMsg.ResultType.warning);
            }

            if (col.AuthString.Equals(authString))
            {
                ldb.Dispose();
                return new ResultMsg(true, "authorization string correct", ResultMsg.ResultType.success);
            }
            else
            {
                ldb.Dispose();
                return new ResultMsg(false, "authorization string incorrect", ResultMsg.ResultType.warning);
            }
        }
        else
        {
            return new ResultMsg(false, "error connecting database", ResultMsg.ResultType.danger);
        }
    }

    public static ResultMsg AddAdmin()
    {
        var ldb = OpenLDB();

        if (ldb == null) return new ResultMsg(false, "database error", ResultMsg.ResultType.danger);

        var randomString = Path.GetRandomFileName().Replace(".", "");
        randomString += Path.GetRandomFileName().Replace(".", "");

        var cols = ldb.GetCollection<User>(collNames.users);
        cols.Insert(new User() { Name = "admin", AuthString = randomString });

        ldb.Dispose();
        return new ResultMsg(true, randomString, ResultMsg.ResultType.success);
    }
}