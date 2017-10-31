using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Course
/// </summary>
public class Course
{
    public int courseID;
    public string courseName;
    public string courseSession;
    public string courseOwner;
    public string available;
    public string chapterCount;
    public string assignmentCount;

    public Course(int courseID)
    {
        this.courseID = courseID;
        DataTable courseTB = AppData.GetCourseDetail(courseID);
        if(courseTB.Rows.Count > 0)
        {
            this.courseID = courseID;
            this.courseName = courseTB.Rows[0]["CourseName"].ToString();
            this.courseSession = courseTB.Rows[0]["CourseSession"].ToString();
            this.courseOwner = courseTB.Rows[0]["courseOwner"].ToString();
            this.available = courseTB.Rows[0]["Active"].ToString();
            this.chapterCount = Chapter.ChapterCount(courseID).ToString();
        }
    }

    public Course(int courseID, int userID)
    {
        this.courseID = courseID;
        DataTable courseTB = AppData.GetCourseDetail(courseID);
        if (courseTB.Rows.Count > 0)
        {
            this.courseID = courseID;
            this.courseName = courseTB.Rows[0]["CourseName"].ToString();
            this.courseSession = courseTB.Rows[0]["CourseSession"].ToString();
            this.courseOwner = courseTB.Rows[0]["courseOwner"].ToString();
            this.available = courseTB.Rows[0]["Active"].ToString();
            this.chapterCount = Chapter.ChapterCount(courseID).ToString();
            this.assignmentCount = Assignment.GetAssignedAssignmentsCount(userID, courseID).ToString();
        }
    }
}