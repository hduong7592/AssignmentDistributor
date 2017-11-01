using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for SessionData
/// </summary>
public class SessionData
{
    private string username;
    private int userID;
    private string userRefID;
    public string sessionID;

    public SessionData(string sessionID)
    {
        this.sessionID = sessionID;
        DataTable SessionData = AppData.GetSessionData(sessionID);
        if(SessionData.Rows.Count > 0)
        {
            this.username = SessionData.Rows[0]["Username"].ToString();
            this.userRefID = SessionData.Rows[0]["UserRefID"].ToString();
            this.userID = Convert.ToInt32(SessionData.Rows[0]["UserID"]);
        }
    }

    public int GetUserID()
    {
        return userID;
    }
}