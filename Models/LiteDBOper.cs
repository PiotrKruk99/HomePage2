using LiteDB;

namespace homePage2.Models;

public static class LiteDBOper
{
    private const string liteDBPath = @"AppData/appData.ldb";
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

    public static ResultMsg CheckAdminExist()
    /*returns 1 on admin exists, 0 on admin not exists 
    and -1 on error on communication with database*/
    {
        var ldb = OpenLDB();

        if (ldb != null)
        {
            var cols = ldb.GetCollection<User>(collNames.users);
            var col = cols.FindOne(x => x.Name.Equals("admin"));

            if (col != null)
            {
                if ((col.Password ?? "").Equals(string.Empty))
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
                    return new ResultMsg(true, "password exists", ResultMsg.ResultType.info, 2);
                }
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
    /*checks if authorization string is correct or not*/
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
    /*adds new admin's account with random authorization string*/
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

    public static ResultMsg SetAdminPassword(string pass)
    /*set admins password*/
    {
        var ldb = OpenLDB();
        if (ldb == null) return new ResultMsg(false, "database error", ResultMsg.ResultType.danger);

        var cols = ldb.GetCollection<User>(collNames.users);
        var col = cols.FindOne(x => x.Name.Equals("admin"));

        if (col != null)
        {
            col.Password = pass;
            cols.Update(col);
            ldb.Dispose();
            return new ResultMsg(true);
        }

        ldb.Dispose();
        return new ResultMsg(false, "error connecting to database", ResultMsg.ResultType.warning);
    }

    public static ResultMsg CheckUsersAuthentication(string login, string password)
    /*chcking user login and password*/
    {
        var ldb = OpenLDB();
        if (ldb == null) return new ResultMsg(false, "database error", ResultMsg.ResultType.danger);

        var cols = ldb.GetCollection<User>(collNames.users);
        var col = cols.FindOne(x => x.Name.Equals(login));

        if (col != null)
        {
            if (col.Password.Equals(password))
            {
                ldb.Dispose();
                return new ResultMsg(true, "correct user name and password", ResultMsg.ResultType.success);
            }
        }

        ldb.Dispose();
        return new ResultMsg(false, "wrong user name or password", ResultMsg.ResultType.warning);
    }
}