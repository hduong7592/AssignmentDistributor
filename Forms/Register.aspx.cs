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
        string WhichUser = Request.Form["WhichUser"].ToString();
        if(WhichUser == "Student")
        {
            CodeTxt.Value = "STUDENT";
        }
    }
}