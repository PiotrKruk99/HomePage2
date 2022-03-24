using LiteDB;

namespace homePage2.Models;

public static class LiteDBOper
{
    private const string liteDBPath = @"AppData/appData.ldb";
    private static Dictionary<string, string> collNames =
        new Dictionary<string, string>() { { "users", "users" } };

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
            var coll = ldb.GetCollection<User>(collNames["users"]);
            var elem = coll.FindOne(x => x.Name.Equals("admin"));

            if (elem != null)
            {
                if (elem.ExpireDate < DateTime.Now)
                {
                    coll.Delete(elem.Id);
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

    public static ResultMsg AddAdmin()
    {
        var ldb = OpenLDB();

        if (ldb == null) return new ResultMsg(false, "database error", ResultMsg.ResultType.danger);

        var randomString = Path.GetRandomFileName().Replace(".", "");
        randomString += Path.GetRandomFileName().Replace(".", "");

        var coll = ldb.GetCollection<User>(collNames["users"]);
        coll.Insert(new User() {Name = "admin", AuthString=randomString});

        ldb.Dispose();
        return new ResultMsg(true, randomString, ResultMsg.ResultType.success);
    }
}