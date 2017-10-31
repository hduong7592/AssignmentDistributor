using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
/// <summary>
/// Summary description for Register
/// </summary>
public class Register
{
    private int UserID;
    private string FirstName;
    private string LastName;
    private string Email;
    private string Username;
    private string Password;
    private int Role;
    private string RefID;

    public Register(string First, string Last, string Email, string User, string Pass, string UserRole)
    {
        this.FirstName = First;
        this.LastName = Last;
        this.Email = Email;
        this.Username = User;
       
        if(UserRole == "STUDENT")
        {
            this.Role = 4;
        }
        else
        {
            this.Role = 3;
        }

        Guid UniqID = Guid.NewGuid();
        this.RefID = "User-" + UniqID.ToString();

        this.Password = Security.EncodePassword(Pass);
    }

    public bool CheckUserExist()
    {
        bool IsExist = false;

        DataTable CheckUserExistTB = AppData.CheckUserExist(Username);
        if(CheckUserExistTB.Rows.Count > 0)
        {
            IsExist = true;           
            this.RefID = CheckUserExistTB.Rows[0]["UserRefID"].ToString();         
        }

        return IsExist;
    }

    public string GetUserRefID()
    {
        return this.RefID;
    }

    public string AddNewUser()
    {
        string status = "";

        status = AppData.AddNewUser(RefID, Username, Password, Role, FirstName, LastName, Email);

        return status;
    }    
}