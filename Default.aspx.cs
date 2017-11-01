using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public static string currentDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        currentDate = DateTime.Now.Millisecond.ToString();
        if (!IsPostBack)
        {
            LoginBtn.Attributes.Add("onclick", "Login();");
            RegisterBtn.Attributes.Add("onclick", "Register('Student');");
        }
    }
}