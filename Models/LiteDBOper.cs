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

            if (coll.Exists(x => x.Name.Equals("admin")))
            {
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

    public static bool AddAdmin()
    {
        var ldb = OpenLDB();

        if (ldb == null) return false;

        

        ldb.Dispose();
        return false;
    }
}