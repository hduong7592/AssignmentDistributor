using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Assignment
/// </summary>
public class Assignment
{
    public int aID;
    public string aName;
    public string aDescription;
    public string assignedTo;

    public Assignment(int aID)
    {
        this.aID = aID;
        DataTable aTB = AppData.GetAssignmentDetail(aID);
        if (aTB.Rows.Count > 0)
        {
            this.aID = aID;
            this.aName = aTB.Rows[0]["aName"].ToString();
            this.aDescription = aTB.Rows[0]["aDescription"].ToString();
            this.assignedTo = "NA";
        }
    }

    public string GetAssignmentName()
    {
        return aName;
    }

    public static int GetAssignmenCount(int chapterID)
    {
        int assignmentCount = 0;
        DataTable getAsssignmentTB = AppData.GetAssignments(chapterID);
        assignmentCount = getAsssignmentTB.Rows.Count;
        return assignmentCount;
    }

    public static int GetAssignedAssignmentsCount(int userID, int courseID)
    {
        int assignmentCount = 0;
        DataTable getAsssignmentTB = AppData.GetAssignedAssignmentsCount(userID, courseID);
        assignmentCount = getAsssignmentTB.Rows.Count;
        return assignmentCount;
    }
}