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

    public static int CheckAdmin()
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
                    return 0;
                }

                ldb.Dispose();
                return 1;
            }
            else
            {
                ldb.Dispose();
                return 0;
            }
        }

        return -1;
    }

    public static ResultMsg CheckAdminsAuthString(string authString)
    {
        var ldb = OpenLDB();

        if (ldb != null)
        {

        }

        return new ResultMsg(false, 0, "error connecting database", ResultMsg.ResultType.info);
    }

    public static ResultMsg AddAdmin()
    {
        var ldb = OpenLDB();

        if (ldb == null) return new ResultMsg(false, 0, "database error", ResultMsg.ResultType.danger);

        var randomString = Path.GetRandomFileName().Replace(".", "");
        randomString += Path.GetRandomFileName().Replace(".", "");

        var cols = ldb.GetCollection<User>(collNames.users);
        cols.Insert(new User() {Name = "admin", AuthString=randomString});

        ldb.Dispose();
        return new ResultMsg(true, 0, randomString, ResultMsg.ResultType.success);
    }
}