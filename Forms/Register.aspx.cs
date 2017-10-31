using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Forms_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string WhichUser = Request.Form["WhichUser"].ToString();
            if (WhichUser == "Student")
            {
                CodeTxt.Value = "STUDENT";
            }

            //Session.Clear();
            //Session.Abandon();

            string SessionID = this.Session.SessionID;            
        
            RegisterBtn.Attributes.Add("onclick", "ValidateRegisterForm('"+ SessionID + "');");
        }
        
    }
}