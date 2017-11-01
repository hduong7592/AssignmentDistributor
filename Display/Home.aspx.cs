using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Display_Home : System.Web.UI.Page
{
    public string SessionID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["CurrentSession"].ToString().Trim();
            SessionID = Session["CurrentSession"].ToString().Trim();
            if (SessionID != "")
            {
                GetSessionData(SessionID);
            }
        }
    }

    private void GetSessionData(string sessionID)
    {
        SessionData session = new SessionData(sessionID);
        int userID = session.GetUserID();

        User user = new User(userID);
        UserLB.Text = user.GetUserName();
        LogoutLB.Attributes.Add("onclick", "Logout('"+SessionID+"');");
        LogoutLB.Text = "(Logout)";

        DataTable Table = new DataTable();

        int userRole = user.GetUserRole();
        switch (userRole)
        {
            case 1:
                Table = AppData.GetAdminnData();
                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;

            default:

                break;
        }

        GridView1.DataSource = Table;
        GridView1.DataBind();
    }
}