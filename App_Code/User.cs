using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for User
/// </summary>
public class User
{
    public int UserID;
    public string FirstName;
    public string LastName;
    public string Email;
    public string Username;
    private string Password;
    public string RefID;
    public int UserRole;
    public string SessionID;
    public string Status;

    public User()
    {

    }
    public User(int UserID)
    {
        DataTable UserInfo = AppData.GetUserData(UserID);
        if(UserInfo.Rows.Count > 0)
        {
            this.UserID = UserID;
            this.FirstName = UserInfo.Rows[0]["FirstName"].ToString();
            this.LastName = UserInfo.Rows[0]["LastName"].ToString();
            this.Email = UserInfo.Rows[0]["Email"].ToString();
            this.Username = UserInfo.Rows[0]["Username"].ToString();
            this.RefID = UserInfo.Rows[0]["UserRefID"].ToString();
            this.UserRole = Convert.ToInt32(UserInfo.Rows[0]["UserRole"]);
            this.Status = "NA";
        }
        else
        {
            this.UserID = 0;
        }
    }

    public User(string UserRefID)
    {
        DataTable UserInfo = AppData.GetUserData(UserRefID);
        if (UserInfo.Rows.Count > 0)
        {
            this.UserID = Convert.ToInt32(UserInfo.Rows[0]["UserID"]);
            this.FirstName = UserInfo.Rows[0]["FirstName"].ToString();
            this.LastName = UserInfo.Rows[0]["LastName"].ToString();
            this.Email = UserInfo.Rows[0]["Email"].ToString();
            this.Username = UserInfo.Rows[0]["Username"].ToString();
            this.RefID = UserInfo.Rows[0]["UserRefID"].ToString();
            this.UserRole = Convert.ToInt32(UserInfo.Rows[0]["UserRole"]);
        }
        else
        {
            this.UserID = 0;
        }
    }

    public User(string UserRefID, string SessionID, string Status)
    {
        this.SessionID = SessionID;
        this.Status = Status;

        DataTable UserInfo = AppData.GetUserData(UserRefID);
        if (UserInfo.Rows.Count > 0)
        {
            this.UserID = Convert.ToInt32(UserInfo.Rows[0]["UserID"]);
            this.FirstName = UserInfo.Rows[0]["FirstName"].ToString();
            this.LastName = UserInfo.Rows[0]["LastName"].ToString();
            this.Email = UserInfo.Rows[0]["Email"].ToString();
            this.Username = UserInfo.Rows[0]["Username"].ToString();
            this.RefID = UserInfo.Rows[0]["UserRefID"].ToString();
            this.UserRole = Convert.ToInt32(UserInfo.Rows[0]["UserRole"]);
        }
        else
        {
            this.UserID = 0;
        }
    }
}