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
    /*
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
    public string LogIn(string Username, string Password, string logfrom)
    {
        string status = "Not LoggedIn";
        string UserRefID = "";
        string SessionID = Session["CurrentSession"].ToString().Trim();
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

        DataTable CheckSession = AppData.CheckSession(SessionID, Username);
        if(CheckSession.Rows.Count > 0)
        {

        }
        else
        {
            AppData.SaveLoginSession(SessionID, Username, status, Validate, logfrom);
        }       

        User currentuser = new User(UserRefID, SessionID, status);

        JavaScriptSerializer js = new JavaScriptSerializer();

        return js.Serialize(currentuser);
    }
    /*
    [WebMethod]
    public string WebLogIn(string Username, string Password, string SessionID, string logfrom)
    {
        string status = "Not LoggedIn";
        string UserRefID = "";
        string Validate = "No";
        string logfrom = "Web";
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

        AppData.SaveLoginSession(SessionID, Username, status, Validate, logfrom);

        User currentuser = new User(UserRefID, SessionID, status);

        JavaScriptSerializer js = new JavaScriptSerializer();

        return js.Serialize(currentuser);
    }*/

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
    public string GetAllCourses(int userID)
    {
        DataTable CoursesTB = AppData.GetAllCourses(userID);
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
    public string DeleteAssignment(int aID, int userID)
    {
        string result = "";

        result = AppData.DeleteAssignment(userID, aID);

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

    [WebMethod(EnableSession = true)]
    public string AssignCourse(int courseID, int userID)
    {
        string result = "";

        DataTable CheckCourseExist = AppData.GetAssignedCourses(userID, courseID);
        if (CheckCourseExist.Rows.Count > 0)
        {
            result = "Exist";
        }
        else
        {
            result = AppData.AssignCourse(courseID, userID);
        }

        return result;
    }

    [WebMethod(EnableSession = true)]
    public string Logout(string SessionID)
    {     
        string result = "";

        result = AppData.UpdateSession(SessionID);
       
        return result;
    }

    [WebMethod(EnableSession = true)]
    public string DeleteAll(int userID)
    {
        string result = "";

        result = AppData.DeleteAllCouse(userID);
        result = AppData.DeleteAllChapters(userID);
        result = AppData.DeleteAllAssignments(userID);
        result = AppData.DeleteAllAssignedCourses(userID);
        result = AppData.DeleteAllAssignedAssignments(userID);

        return result;
    }

    [WebMethod(EnableSession = true)]
    public string AssignAssignments( int userID)
    {
        string result = "";

        ArrayList courseList = new ArrayList();


        DataTable coursesTB = AppData.GetCourses(userID);
        if (coursesTB.Rows.Count > 0)
        {
            foreach (DataRow row in coursesTB.Rows)
            {
                int courseID = Convert.ToInt16(row["courseID"]);
                courseList.Add(courseID);

                DataTable studentsTB = AppData.GetStudentsInCourse(courseID);
                ArrayList chapterList = new ArrayList();

                DataTable chaptersTB = AppData.GetChapters(courseID);
                if (chaptersTB.Rows.Count > 0)
                {
                    foreach (DataRow chapter in chaptersTB.Rows)
                    {
                        ArrayList studentList = new ArrayList();
                        ArrayList assignmentList = new ArrayList();

                        int chapterID = Convert.ToInt32(chapter["ChapterID"]);
                        chapterList.Add(chapterID);

                        if (studentsTB.Rows.Count > 0)
                        {
                            foreach (DataRow student in studentsTB.Rows)
                            {
                                int studentID = Convert.ToInt32(student["UserID"]);

                                DataTable checkAssignmentTB = AppData.CheckAssignedAssignments(studentID, chapterID);
                                if (checkAssignmentTB.Rows.Count > 0)
                                {
                                    //Do nothing
                                }
                                else
                                {
                                    studentList.Add(studentID);
                                }


                            }
                        }

                        DataTable assignmentsTB = AppData.GetAssignments(chapterID);
                        if (assignmentsTB.Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow a in assignmentsTB.Rows)
                            {
                                int aID = Convert.ToInt32(a["aID"]);
                                assignmentList.Add(aID);
                            }
                        }


                        //assignmentList = RandomOrder(assignmentList);
                        studentList = AppData.RandomOrder(studentList);

                        //If number of assignment larger or equal number of students in class
                        //loop through the assignment list with the student list randomized
                        //assign each assignment to the randomize student list.
                        if (assignmentList.Count >= studentList.Count)
                        {
                            for (int i = 0; i < assignmentList.Count; i++)
                            {
                                int aID = Convert.ToInt32(assignmentList[i]);
                                int studentID = 0;
                                try
                                {
                                    studentID = Convert.ToInt32(studentList[i]);
                                }
                                catch
                                {
                                    studentID = 0;
                                }

                                if (studentID > 0)
                                {
                                    result = AppData.AssignAssignment(studentID, aID, chapterID, courseID, userID);
                                }
                            }
                        }
                        else
                        {
                            //If number of assignments less than number of students
                            //then generate a random index number
                            //for each student, assign the assignment with that random index number
                            //Or just randomize the arraylist and get the first index
                            //Or the combination of both
                            foreach (int studentID in studentList)
                            {
                                assignmentList = AppData.RandomOrder(assignmentList);
                                int aID = Convert.ToInt32(assignmentList[0]);
                                result = AppData.AssignAssignment(studentID, aID, chapterID, courseID, userID);

                            }

                        }

                    }
                }

            }
        }  
        return result;
    }
}
