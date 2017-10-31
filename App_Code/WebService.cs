using System;
using System.IO;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Text;
using System.Drawing;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Web.Script.Services;
using System.Web.Script.Serialization;

/// <summary>
/// Summary description for WebService
/// Just made some changes to this
/// </summary>
[WebService(Namespace = "http:/myaddb.azurewebsites.net/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod(EnableSession = true)]
    public string Register(string Code, string FirstName, string LastName, string Email, string Username, string Password) 
    {
        string status = "Not Register";
        string UserRefID = "";
        string SessionID = Session["CurrentSession"].ToString();

        Register newRegister = new Register(FirstName, LastName, Email, Username, Password, Code);
        bool IsExist = newRegister.CheckUserExist();
        if (IsExist) {
            status = "Exist";
            UserRefID = newRegister.GetUserRefID();
        }
        else
        {
            status = newRegister.AddNewUser();
            if (status == "Success")
            {
                status = "Registered";
                UserRefID = newRegister.GetUserRefID();
            }
        }

        User currentuser = new User(UserRefID, SessionID, status);

        JavaScriptSerializer js = new JavaScriptSerializer();

        return js.Serialize(currentuser);       
    }

    [WebMethod]
    public User LogIn(string Username, string Password, string SessionID)
    {
        string status = "Not LoggedIn";
        string UserRefID = "";  
       
        LogIn userlogin = new LogIn(Username, Password);
        bool IsExist = userlogin.CheckUserExist();
        if (IsExist)
        {
            bool IsMatching = userlogin.CheckPassword();
            if (IsMatching)
            {
                UserRefID = userlogin.GetUserRefID();
                status = "LoggedIn";                
            }
            else
            {
                status = "Password Incorrect";
            }
        }
        else
        {
            status = "Not Exist";
        }

        User currentuser = new User(UserRefID, SessionID, status);

        return currentuser;
    }

    /*
    [WebMethod]
    public void MobileLogIn(string Username, string Password, string SessionID)
    {
        string status = "Not LoggedIn";
        string UserRefID = "";
        http://myaddb.azurewebsites.net/
        LogIn userlogin = new LogIn(Username, Password);
        bool IsExist = userlogin.CheckUserExist();
        if (IsExist)
        {
            bool IsMatching = userlogin.CheckPassword();
            if (IsMatching)
            {
                UserRefID = userlogin.GetUserRefID();
                status = "LoggedIn";
            }
            else
            {
                status = "Password Incorrect";
            }
        }
        else
        {
            status = "Not Exist";
        }

        User currentuser = new User(UserRefID, SessionID, status);

        JavaScriptSerializer js = new JavaScriptSerializer();

        Context.Response.Write(js.Serialize(currentuser));
    }
    */

    [WebMethod(EnableSession = true)]
    public string MobileLogIn(string Username, string Password, string SessionID)
    {
        string status = "Not LoggedIn";
        string UserRefID = "";
        SessionID = Session["CurrentSession"].ToString() + "-Mobile";
        string Validate = "No";

        LogIn userlogin = new LogIn(Username, Password);
        bool IsExist = userlogin.CheckUserExist();
        if (IsExist)
        {
            bool IsMatching = userlogin.CheckPassword();
            if (IsMatching)
            {
                UserRefID = userlogin.GetUserRefID();
                status = "LoggedIn";
                Validate = "Yes";
            }
            else
            {
                status = "Password Incorrect";
            }
        }
        else
        {
            status = "Not Exist";
        }

        AppData.SaveLoginSession(SessionID, Username, status, Validate);

        User currentuser = new User(UserRefID, SessionID, status);

        JavaScriptSerializer js = new JavaScriptSerializer();

        return js.Serialize(currentuser);
    }

    [WebMethod]
    public string WebLogIn(string Username, string Password, string SessionID)
    {
        string status = "Not LoggedIn";
        string UserRefID = "";
        string Validate = "No";

        LogIn userlogin = new LogIn(Username, Password);
        bool IsExist = userlogin.CheckUserExist();
        if (IsExist)
        {
            bool IsMatching = userlogin.CheckPassword();
            if (IsMatching)
            {
                UserRefID = userlogin.GetUserRefID();
                status = "LoggedIn";
                Validate = "Yes";
            }
            else
            {
                status = "Password Incorrect";
            }
        }
        else
        {
            status = "Not Exist";
        }

        AppData.SaveLoginSession(SessionID, Username, status, Validate);

        User currentuser = new User(UserRefID, SessionID, status);

        JavaScriptSerializer js = new JavaScriptSerializer();

        return js.Serialize(currentuser);
    }

    [WebMethod]
    public void GetUserInfo(int UserID)
    {

        User userinfo = new User(UserID);        

        JavaScriptSerializer js = new JavaScriptSerializer();

        Context.Response.Write(js.Serialize(userinfo));
    }

    [WebMethod(EnableSession = true)]
    public string GetUsers()
    {       
        string SessionID = Session["CurrentSession"].ToString() + "-Mobile";

        DataTable UsersTB = AppData.GetUsers();
        List<User> usersList = new List<User>();


        if (UsersTB.Rows.Count > 0)
        {
            foreach (DataRow row in UsersTB.Rows)
            {

                int userID = 0;
                try
                {
                    userID = Convert.ToInt32(row["UserID"]);
                }
                catch
                {
                    userID = 0;
                }

                User newUser = new User(userID);
                usersList.Add(newUser);
            }
        }

       
        JavaScriptSerializer js = new JavaScriptSerializer();

        string result = "{\"Users\":" + js.Serialize(usersList) + "}";
        return result;
    }

    [WebMethod(EnableSession = true)]
    public string AddCourse(string courseName, string courseSession, int userID)
    {
        string result = "";

        DataTable CheckCourseExist = AppData.CheckCourseExist(courseName, courseSession, userID);
        if(CheckCourseExist.Rows.Count > 0)
        {
            result = "Exist";
        }
        else
        {
            result = AppData.AddCourse(courseName, courseSession, userID);
        }

        return result;
    }

    [WebMethod(EnableSession = true)]
    public string GetCourses(int userID)
    {
        DataTable CoursesTB = AppData.GetCourses(userID);
        List<Course> courseList = new List<Course>();

        if (CoursesTB.Rows.Count > 0)
        {
            foreach (DataRow row in CoursesTB.Rows)
            {

                int courseID = 0;
                try
                {
                    courseID = Convert.ToInt32(row["CourseID"]);
                }
                catch
                {
                    courseID = 0;
                }

                Course newCourse = new Course(courseID);
                courseList.Add(newCourse);
            }
        }

        JavaScriptSerializer js = new JavaScriptSerializer();

        string result = "{\"Courses\":" + js.Serialize(courseList) + "}";
        return result;
    }

    [WebMethod(EnableSession = true)]
    public string AddUnit(string unitName, int courseID, int userID)
    {
        string result = "";

        DataTable CheckUnitExist = AppData.CheckUnitExist(unitName, courseID, userID);
        if (CheckUnitExist.Rows.Count > 0)
        {
            result = "Exist";
        }
        else
        {
            result = AppData.AddUnit(unitName, courseID, userID);
        }

        return result;
    }

    [WebMethod(EnableSession = true)]
    public string GetUnits(int courseID)
    {
        DataTable UnitsTB = AppData.GetUnits(courseID);
        List<Unit> unitList = new List<Unit>();

        if (UnitsTB.Rows.Count > 0)
        {
            foreach (DataRow row in UnitsTB.Rows)
            {

                int unitID = 0;
                try
                {
                    unitID = Convert.ToInt32(row["UnitID"]);
                }
                catch
                {
                    unitID = 0;
                }

                Unit newUnit = new Unit(unitID);
                unitList.Add(newUnit);
            }
        }

        JavaScriptSerializer js = new JavaScriptSerializer();

        string result = "{\"Units\":" + js.Serialize(unitList) + "}";
        return result;
    }

    [WebMethod(EnableSession = true)]
    public string AddChapter(string chapterName, string chapterDescription, int courseID, int userID)
    {
        string result = "";

        DataTable CheckChapterExist = AppData.ChecChapterExist(chapterName, courseID);
        if (CheckChapterExist.Rows.Count > 0)
        {
            result = "Exist";
        }
        else
        {
            result = AppData.AddChapter(chapterName, chapterDescription, courseID, userID);
        }

        return result;
    }

    [WebMethod(EnableSession = true)]
    public string GetChapters(int courseID)
    {
        DataTable ChaptersTB = AppData.GetChapters(courseID);
        List<Chapter> chapterList = new List<Chapter>();

        if (ChaptersTB.Rows.Count > 0)
        {
            foreach (DataRow row in ChaptersTB.Rows)
            {

                int chapterID = 0;
                try
                {
                    chapterID = Convert.ToInt32(row["ChapterID"]);
                }
                catch
                {
                    chapterID = 0;
                }

                Chapter newChapter = new Chapter(chapterID);
                chapterList.Add(newChapter);
            }
        }

        JavaScriptSerializer js = new JavaScriptSerializer();

        string result = "{\"Chapters\":" + js.Serialize(chapterList) + "}";
        return result;
    }

    [WebMethod(EnableSession = true)]
    public string AddAssignment(string aName, string aDescription, int chapterID, int userID)
    {
        string result = "";

        DataTable CheckAssignmentExist = AppData.CheckAssignmentExist(aName, chapterID);
        if (CheckAssignmentExist.Rows.Count > 0)
        {
            result = "Exist";
        }
        else
        {
            result = AppData.AddAssignment(aName, aDescription, chapterID, userID);
        }

        return result;
    }

    [WebMethod(EnableSession = true)]
    public string GetAssignments(int chapterID)
    {
        DataTable AssignmentsTB = AppData.GetAssignments(chapterID);
        List<Assignment> assignmentList = new List<Assignment>();

        if (AssignmentsTB.Rows.Count > 0)
        {
            foreach (DataRow row in AssignmentsTB.Rows)
            {

                int aID = 0;
                try
                {
                    aID = Convert.ToInt32(row["aID"]);
                }
                catch
                {
                    aID = 0;
                }

                Assignment newAssignment = new Assignment(aID);
                assignmentList.Add(newAssignment);
            }
        }

        JavaScriptSerializer js = new JavaScriptSerializer();

        string result = "{\"Assignments\":" + js.Serialize(assignmentList) + "}";
        return result;
    }

    [WebMethod(EnableSession = true)]
    public string GetAssignmentDetail(int aID)
    {
        Assignment newAssignment = new Assignment(aID);

        JavaScriptSerializer js = new JavaScriptSerializer();

        string result = js.Serialize(newAssignment);
        return result;
    }


    [WebMethod(EnableSession = true)]
    public string UpdateAssignment(string aName, string aDescription, int userID, int aID)
    {
        string result = "";

        result = AppData.UpdateAssignment(aName, aDescription, userID, aID);
       
        return result;
    }

    [WebMethod(EnableSession = true)]
    public string GetCourseDetail(int courseID)
    {
        Course detail = new Course(courseID);

        JavaScriptSerializer js = new JavaScriptSerializer();

        string result = js.Serialize(detail);
        return result;
    }

    [WebMethod(EnableSession = true)]
    public string UpdateCourse(string courseName, string courseSession, int courseID, int userID)
    {
        string result = "";

        result = AppData.UpdateCourse(courseName, courseSession, courseID, userID);

        return result;
    }

    [WebMethod(EnableSession = true)]
    public string DeleteCourse(int courseID, int userID)
    {
        string result = "";

        result = AppData.DeleteCouse(courseID, userID);

        return result;
    }

    [WebMethod(EnableSession = true)]
    public string GetChapterDetail(int chapterID)
    {
        Chapter detail = new Chapter(chapterID);

        JavaScriptSerializer js = new JavaScriptSerializer();

        string result = js.Serialize(detail);
        return result;
    }

    [WebMethod(EnableSession = true)]
    public string UpdateChapter(string chapterName, string chapterDescription, int chapterID, int userID)
    {
        string result = "";

        result = AppData.UpdateChapter(chapterName, chapterDescription, chapterID, userID);

        return result;
    }

    [WebMethod(EnableSession = true)]
    public string DeleteChapter(int chapterID, int userID)
    {
        string result = "";

        result = AppData.DeleteChapter(chapterID, userID);

        return result;
    }

    [WebMethod(EnableSession = true)]
    public string GetAssignedCourses(int userID)
    {
        DataTable table = AppData.GetAssignedCourses(userID);
        List<Course> list = new List<Course>();

        if (table.Rows.Count > 0)
        {
            foreach (DataRow row in table.Rows)
            {
                int courseID = 0;
                try
                {
                    courseID = Convert.ToInt32(row["CourseID"]);
                }
                catch
                {
                    courseID = 0;
                }

                Course newCourse = new Course(courseID, userID);
                list.Add(newCourse);
            }
        }

        JavaScriptSerializer js = new JavaScriptSerializer();

        string result = "{\"Courses\":" + js.Serialize(list) + "}";
        return result;
    }

    [WebMethod(EnableSession = true)]
    public string GetAssignedAssignment(int userID, int courseID)
    {
        DataTable table = AppData.GetAssignedAssignments(userID, courseID);
        List<Chapter> list = new List<Chapter>();

        if (table.Rows.Count > 0)
        {
            foreach (DataRow row in table.Rows)
            {
                int chapterID = 0;
                try
                {
                    chapterID = Convert.ToInt32(row["ChapterID"]);
                }
                catch
                {
                    chapterID = 0;
                }

                int aID = 0;
                try
                {
                    aID = Convert.ToInt32(row["aID"]);
                }
                catch
                {
                    aID = 0;
                }

                Chapter newChapter = new Chapter(chapterID, aID);
                list.Add(newChapter);
            }
        }

        JavaScriptSerializer js = new JavaScriptSerializer();

        string result = "{\"Assignments\":" + js.Serialize(list) + "}";
        return result;
    }

}
