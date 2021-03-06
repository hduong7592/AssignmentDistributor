﻿using System;
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

        RoleLB.Text = user.GetRoleDescription();

        DataTable Table = new DataTable();

        int userRole = user.GetUserRole();
        try
        {
            switch (userRole)
            {
                case 1:
                    Table = AppData.GetAdminData();
                    break;
                case 2:
                    Table = AppData.GetAdminData();
                    break;
                case 3:
                    Table = AppData.GetTeacherData(userID);
                    break;
                case 4:
                    Table = AppData.GetStudentData(userID);
                    break;
                default:
                    Table = new DataTable();
                    break;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        GridView1.DataSource = Table;
        GridView1.DataBind();
    }
}