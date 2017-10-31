using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for User
/// </summary>
public class LogIn
{
    private int UserID;
    private string FirstName;
    private string LastName;
    private string Email;
    private string Username;
    private string Password;
    private string RefID; 

    public LogIn()
    {

    }

    public LogIn(string User, string Pass)
    {      
        this.Username = User;
        this.Password = Security.EncodePassword(Pass);
    }

    public bool CheckUserExist()
    {
        bool IsExist = false;

        DataTable CheckUserExistTB = AppData.CheckUserExist(Username);
        if (CheckUserExistTB.Rows.Count > 0)
        {
            IsExist = true;
        }

        return IsExist;
    }

    public bool CheckPassword()
    {
        bool IsMatching = false;

        DataTable CheckUserPassword = AppData.CheckUserPassword(Username, Password);
        if(CheckUserPassword.Rows.Count > 0)
        {
            UserID = Convert.ToInt32(CheckUserPassword.Rows[0]["UserID"]);
            FirstName = CheckUserPassword.Rows[0]["FirstName"].ToString();
            LastName = CheckUserPassword.Rows[0]["LastName"].ToString();
            Email = CheckUserPassword.Rows[0]["Email"].ToString();
            RefID = CheckUserPassword.Rows[0]["UserRefID"].ToString();
            IsMatching = true;
        }

        return IsMatching;
    }

    public int GetUserID()
    {
        return UserID;
    }

    public string GetUserFirstName()
    {
        return FirstName;
    }

    public string GetUserLastName()
    {
        return LastName;
    }

    public string GetUserFullName()
    {
        return FirstName + " " + LastName;
    }

    public string GetUserRefID()
    {
        return RefID;
    }
}