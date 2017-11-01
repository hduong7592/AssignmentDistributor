using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// Summary description for AppData
/// </summary>
public class AppData
{
    public AppData()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable GetData()
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT users.* 
                                                FROM [Users] users
                            ";

                sda.SelectCommand = command;

                //command.Parameters.AddWithValue("@TotalLotsLastSevenDays", TotalLotsLastSevenDays);

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static DataTable GetUserData(int UserID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT users.* 
                                                FROM [Users] users
                                                Where users.UserID = @UserID
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@UserID", UserID);

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static DataTable GetUserData(string UserRefID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT users.* 
                                                FROM [Users] users
                                                Where users.UserRefID = @UserRefID
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@UserRefID", UserRefID);

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static DataTable GetUsers()
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT users.* 
                                                FROM [Users] users                                              
                            ";

                sda.SelectCommand = command;

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static DataTable CheckUserExist(string Username)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT users.* 
                                                FROM [Users] users
                                                Where users.Username = @Username
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@Username", Username);

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static DataTable CheckUserPassword(string Username, string Password)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT users.* 
                                                FROM [Users] users
                                                Where users.Username = @Username
                                                And   users.Password = @Password
                                                And   users.Active = 'Yes'
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", Password);

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static string AddNewUser(string RefID, string Username, string Password, int Role, string FirstName, string LastName, string Email)
    {

        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        string insertresult;

        using (SqlConnection conn = new SqlConnection(NewDsnConnection))
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandType = CommandType.Text;

                command.CommandText = @"INSERT INTO [Users]
                                        (
                                               [UserRefID]
                                              ,[Username]
                                              ,[Password]
                                              ,[UserRole]
                                              ,[Active]
                                              ,[FirstName]
                                              ,[LastName]
                                              ,[Email]
                                              ,[Added_datetime]                                                                                                                          
                                        )
                            VALUES      (
                                            @RefID
                                            ,@Username
                                            ,@Password
                                            ,@Role
                                            ,@Active
                                            ,@FirstName
                                            ,@LastName
                                            ,@Email
                                            ,@datetime
                                        )";

                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", Password);
                command.Parameters.AddWithValue("@RefID", RefID);
                command.Parameters.AddWithValue("@Role", Role);                
                command.Parameters.AddWithValue("@FirstName", FirstName);
                command.Parameters.AddWithValue("@LastName", LastName);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Active", "Yes");
                command.Parameters.AddWithValue("@datetime", DateTime.Now);

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    insertresult = "Success";
                }

                catch (Exception e)
                {
                    insertresult = "Error";
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        return insertresult;
    }

    public static string SaveLoginSession(string SessionID, string Username, string Status, string Validate, string logfrom)
    {

        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        string insertresult;

        using (SqlConnection conn = new SqlConnection(NewDsnConnection))
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandType = CommandType.Text;

                command.CommandText = @"INSERT INTO [LogedIn_Logs]
                                        (
                                               [SessionID]
                                              ,[Username]
                                              ,[Status]
                                              ,[Validate]
                                              ,[Added_datetime]  
                                              ,[Logfrom]
                                        )
                            VALUES      (
                                            @SessionID
                                            ,@Username
                                            ,@Status
                                            ,@Validate
                                            ,@datetime
                                            ,@logfrom
                                        )";

                command.Parameters.AddWithValue("@SessionID", SessionID);
                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Status", Status);
                command.Parameters.AddWithValue("@Validate", Validate);
                command.Parameters.AddWithValue("@logfrom", logfrom);
                command.Parameters.AddWithValue("@datetime", DateTime.Now);

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    insertresult = "Success";
                }

                catch (Exception e)
                {
                    insertresult = "Error";
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        return insertresult;
    }

    public static string AddCourse(string courseName, string courseSession, int userID)
    {

        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        string insertresult;

        using (SqlConnection conn = new SqlConnection(NewDsnConnection))
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandType = CommandType.Text;

                command.CommandText = @"INSERT INTO [Courses]
                                        (
                                               [CourseName]
                                              ,[CourseSession]
                                              ,[Active]
                                              ,[Added_by]
                                              ,[Added_datetime]                                                                                                                       
                                        )
                            VALUES      (
                                            @courseName
                                            ,@courseSession
                                            ,@Active
                                            ,@userID                                         
                                            ,@datetime
                                        )";

                command.Parameters.AddWithValue("@courseName", courseName);
                command.Parameters.AddWithValue("@courseSession", courseSession);
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@Active", "Yes");
                command.Parameters.AddWithValue("@datetime", DateTime.Now);

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    insertresult = "Success";
                }

                catch (Exception e)
                {
                    insertresult = "Error";
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        return insertresult;
    }

    public static DataTable GetCourses(int userID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT courses.* 
                                                FROM [Courses] courses
                                                Where courses.Added_by = @userID
                                              
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@userID", userID);
               
                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static DataTable GetAllCourses(int userID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT courses.* 
                                                FROM [Courses] courses
                                                Where courses.Active = 'Yes'
                                                And courses.CourseID NOT IN 
                                                    (Select assigned.CourseID
												    From [Courses_Assigned] assigned
												    Where assigned.UserID = @userID)
                                              
                            ";
                
                sda.SelectCommand = command;
                command.Parameters.AddWithValue("@userID", userID);
                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static DataTable GetCourseDetail(int courseID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT courses.* 
                                                        ,(users.FirstName + ' ' + users.LastName) as courseOwner
                                                FROM [Courses] courses
                                                     ,[Users] users
                                                Where courses.CourseID = @courseID
                                                And     courses.Added_by = users.UserID
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@courseID", courseID);

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static DataTable CheckCourseExist(string courseName, string courseSession, int userID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT courses.* 
                                                FROM [Courses] courses
                                                Where courses.CourseName = @courseName
                                                And   courses.CourseSession = @courseSession
                                                And   courses.Added_by = @userID
                                              
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@courseName", courseName);
                command.Parameters.AddWithValue("@courseSession", courseSession);
                command.Parameters.AddWithValue("@userID", userID);

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static string AddUnit(string unitName, int courseID, int userID)
    {

        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        string insertresult;

        using (SqlConnection conn = new SqlConnection(NewDsnConnection))
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandType = CommandType.Text;

                command.CommandText = @"INSERT INTO [Units]
                                        (
                                               [UnitName]
                                              ,[CourseID]
                                              ,[Active]
                                              ,[Added_by]
                                              ,[Added_datetime]                                                                                                                   
                                        )
                            VALUES      (
                                            @unitName
                                            ,@courseID
                                            ,@Active
                                            ,@userID                                         
                                            ,@datetime
                                        )";

                command.Parameters.AddWithValue("@unitName", unitName);
                command.Parameters.AddWithValue("@courseID", courseID);
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@Active", "Yes");
                command.Parameters.AddWithValue("@datetime", DateTime.Now);

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    insertresult = "Success";
                }

                catch (Exception e)
                {
                    insertresult = "Error";
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        return insertresult;
    }

    public static DataTable CheckUnitExist(string unitName, int courseID, int userID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT units.* 
                                                FROM [Units] units
                                                Where units.UnitName = @unitName
                                                And   units.CourseID = @courseID
                                                And   units.Added_by = @userID
                                              
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@unitName", unitName);
                command.Parameters.AddWithValue("@courseID", courseID);
                command.Parameters.AddWithValue("@userID", userID);

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static DataTable GetUnits(int courseID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT units.* 
                                                FROM [Units] units
                                                Where units.CourseID = @courseID
                                              
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@courseID", courseID);

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static DataTable GetUnitDetail(int unitID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT units.* 
                                                FROM [Units] units
                                                Where units.UnitID = @unitID
                                              
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@unitID", unitID);

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static string AddChapter(string chapterName, string chapterDescription, int courseID, int userID)
    {

        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        string insertresult;

        using (SqlConnection conn = new SqlConnection(NewDsnConnection))
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandType = CommandType.Text;

                command.CommandText = @"INSERT INTO [Chapters]
                                        (
                                               [ChapterName]
                                              ,[ChapterDescription]
                                              ,[Active]
                                              ,[CourseID]
                                              ,[Added_by]
                                              ,[Added_datetime]                                                                                                                  
                                        )
                            VALUES      (
                                           @chapterName
                                            ,@chapterDescription
                                            ,@Active
                                            ,@courseID
                                            ,@userID                                         
                                            ,@datetime
                                        )";

                command.Parameters.AddWithValue("@chapterName", chapterName);
                command.Parameters.AddWithValue("@chapterDescription", chapterDescription);
                command.Parameters.AddWithValue("@courseID", courseID);
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@Active", "Yes");
                command.Parameters.AddWithValue("@datetime", DateTime.Now);

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    insertresult = "Success";
                }

                catch (Exception e)
                {
                    insertresult = "Error";
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        return insertresult;
    }

    public static DataTable ChecChapterExist(string chapterName, int courseID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT chapters.* 
                                                FROM [Chapters] chapters
                                                Where chapters.ChapterName = @chapterName
                                                And   chapters.CourseID = @courseID                                               
                                              
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@chapterName", chapterName);
                command.Parameters.AddWithValue("@courseID", courseID);
       
                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static DataTable GetChapters(int courseID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT chapters.* 
                                                FROM [Chapters] chapters
                                                Where chapters.CourseID = @courseID
                                              
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@courseID", courseID);

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static DataTable GetChapterDetail(int chapterID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT chapter.* 
                                                FROM [Chapters] chapter
                                                Where chapter.ChapterID = @chapterID
                                              
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@chapterID", chapterID);

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static string AddAssignment(string aName, string aDescription, int chapterID, int userID)
    {

        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        string insertresult;

        using (SqlConnection conn = new SqlConnection(NewDsnConnection))
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandType = CommandType.Text;

                command.CommandText = @"INSERT INTO [Assignments]
                                        (
                                               [aName]
                                              ,[aDescription]
                                              ,[ChapterID]
                                              ,[Active]
                                              ,[Added_by]  
                                              ,[Added_datetime]
                                        )
                            VALUES      (
                                            @aName
                                            ,@aDescription
                                            ,@chapterID
                                            ,@Active
                                            ,@userID                                         
                                            ,@datetime
                                        )";

                command.Parameters.AddWithValue("@aName", aName);
                command.Parameters.AddWithValue("@aDescription", aDescription);
                command.Parameters.AddWithValue("@chapterID", chapterID);
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@Active", "Yes");
                command.Parameters.AddWithValue("@datetime", DateTime.Now);

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    insertresult = "Success";
                }

                catch (Exception e)
                {
                    insertresult = "Error";
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        return insertresult;
    }

    public static DataTable CheckAssignmentExist(string aName, int chapterID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT assigns.* 
                                                FROM [Assignments] assigns
                                                Where assigns.aName = @aName
                                                And   assigns.ChapterID = @chapterID                                           
                                              
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@aName", aName);
                command.Parameters.AddWithValue("@chapterID", chapterID);

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static DataTable GetAssignments(int chapterID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT assigns.* 
                                                FROM [Assignments] assigns
                                                Where assigns.ChapterID = @chapterID
                                              
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@chapterID", chapterID);

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }    

    public static DataTable GetAssignmentDetail(int aID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT assigns.* 
                                                FROM [Assignments] assigns
                                                Where assigns.aID = @aID
                                              
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@aID", aID);

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static string UpdateAssignment(string aName, string aDescription, int userID, int aID)
    {

        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        string insertresult;

        using (SqlConnection conn = new SqlConnection(NewDsnConnection))
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandType = CommandType.Text;

                command.CommandText = @"UPDATE [Assignments]
                                        SET [aName] = @aName
                                            ,[aDescription] = @aDescription
                                            ,[Edited_by] = @userID
                                            ,[Edited_datetime] = @datetime

                                        WHERE [aID] = @aID
                                        ";

                command.Parameters.AddWithValue("@aName", aName);
                command.Parameters.AddWithValue("@aDescription", aDescription);
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@aID", aID);
                command.Parameters.AddWithValue("@datetime", DateTime.Now);

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    insertresult = "Success";
                }

                catch (Exception ex)
                {
                    insertresult = "Error";
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        return insertresult;
    }

    public static string UpdateCourse(string courseName, string courseSession, int courseID, int userID)
    {

        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        string insertresult;

        using (SqlConnection conn = new SqlConnection(NewDsnConnection))
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandType = CommandType.Text;

                command.CommandText = @"UPDATE [Courses]
                                        SET [CourseName] = @courseName
                                            ,[CourseSession] = @courseSession
                                            ,[Edited_by] = @userID
                                            ,[Edited_datetime] = @datetime

                                        WHERE [CourseID] = @courseID
                                        ";

                command.Parameters.AddWithValue("@courseName", courseName);
                command.Parameters.AddWithValue("@courseSession", courseSession);
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@courseID", courseID);
                command.Parameters.AddWithValue("@datetime", DateTime.Now);

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    insertresult = "Success";
                }

                catch (Exception ex)
                {
                    insertresult = "Error";
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        return insertresult;
    }

    public static string DeleteCouse(int courseID, int userID)
    {

        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        string insertresult;

        using (SqlConnection conn = new SqlConnection(NewDsnConnection))
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandType = CommandType.Text;

                command.CommandText = @"DELETE FROM [Courses]                                       
                                        WHERE [CourseID] = @courseID
                                        ";

                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@courseID", courseID);
                command.Parameters.AddWithValue("@datetime", DateTime.Now);

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    insertresult = "Success";
                }

                catch (Exception ex)
                {
                    insertresult = "Error";
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        return insertresult;
    }

    public static string UpdateChapter(string chapterName, string chapterDescription, int chapterID, int userID)
    {

        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        string insertresult;

        using (SqlConnection conn = new SqlConnection(NewDsnConnection))
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandType = CommandType.Text;

                command.CommandText = @"UPDATE [Chapters]
                                        SET    [ChapterName] = @chapterName
                                              ,[ChapterDescription] = @chapterDescription
                                              ,[Edited_by] = @userID
                                              ,[Edited_datetime] = @datetime

                                        WHERE [ChapterID] = @chapterID
                                        ";

                command.Parameters.AddWithValue("@chapterName", chapterName);
                command.Parameters.AddWithValue("@chapterDescription", chapterDescription);
                command.Parameters.AddWithValue("@chapterID", chapterID);
                command.Parameters.AddWithValue("@userID", userID);         
                command.Parameters.AddWithValue("@datetime", DateTime.Now);

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    insertresult = "Success";
                }

                catch (Exception ex)
                {
                    insertresult = "Error";
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        return insertresult;
    }

    public static string DeleteChapter(int chapterID, int userID)
    {

        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        string insertresult;

        using (SqlConnection conn = new SqlConnection(NewDsnConnection))
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandType = CommandType.Text;

                command.CommandText = @"DELETE FROM [Chapters]                                       
                                        WHERE [ChapterID] = @chapterID
                                        ";

                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@chapterID", chapterID);
                command.Parameters.AddWithValue("@datetime", DateTime.Now);

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    insertresult = "Success";
                }

                catch (Exception ex)
                {
                    insertresult = "Error";
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        return insertresult;
    }

    public static DataTable GetAssignedCourses(int userID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT assigns.* 
                                                FROM [Courses_Assigned] assigns
                                                    ,[Courses] courses
                                                Where assigns.UserID = @userID
                                                And   assigns.CourseID = courses.CourseID
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@userID", userID);

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static DataTable GetAssignedCourses(int userID, int courseID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT assigns.* 
                                                FROM [Courses_Assigned] assigns
                                                Where assigns.UserID = @userID
                                                And   assigns.CourseID = @courseID
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@courseID", courseID);

                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static DataTable GetAssignedAssignmentsCount(int userID, int courseID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        SELECT assigns.* 
                                                FROM [Assignments_Assigned] assigns
                                                Where assigns.UserID = @userID
                                                And   assigns.CourseID = @courseID
                                              
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@courseID", courseID);
                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static DataTable GetAssignedAssignments(int userID, int courseID)
    {
        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(NewDsnConnection))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;

                command.CommandType = CommandType.Text;
                command.CommandText = @"        Select assigns.ID
		                                                ,assigns.UserID
		                                                ,assigns.aID
		                                                ,assigns.ChapterID
		                                                ,assigns.CourseID
		                                                ,assignment.aName
		                                                ,assignment.aDescription
		                                                ,chapter.ChapterName
		                                                ,chapter.ChapterDescription

                                                  FROM [Assignments_Assigned] assigns
		                                                ,[Chapters] chapter
		                                                ,[Assignments] assignment
                                                  WHERE	
		                                                assigns.ChapterID = chapter.ChapterID
                                                  AND	assigns.aID = assignment.aID
                                                  AND	assigns.CourseID = @courseID
                                                  AND	assigns.UserID = @userID
                                              
                            ";

                sda.SelectCommand = command;

                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@courseID", courseID);
                try
                {
                    con.Open();
                    sda.Fill(dt);
                }

                catch (Exception e)
                {
                    string msg_subject = "Error";
                    string msg_content = "Error: " + e.ToString();

                    //SendMail.SendErrorMessage(msg_subject, msg_content);
                }
                finally
                {
                    con.Close();
                }

            }
        }
        return dt;

    }

    public static string AssignCourse(int courseID, int userID)
    {

        string NewDsnConnection = ConfigurationManager.ConnectionStrings["dsn"].ToString();

        string insertresult;

        using (SqlConnection conn = new SqlConnection(NewDsnConnection))
        {
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandType = CommandType.Text;

                command.CommandText = @"INSERT INTO [Courses_Assigned]
                                        (
                                               [UserID]
                                              ,[CourseID]
                                              ,[Added_by]
                                              ,[Added_datetime]                                                                                                                 
                                        )
                            VALUES      (
                                            @userID 
                                            ,@courseID                                            
                                            ,@userID                                         
                                            ,@datetime
                                        )";
                
                command.Parameters.AddWithValue("@courseID", courseID);
                command.Parameters.AddWithValue("@userID", userID);
                command.Parameters.AddWithValue("@Active", "Yes");
                command.Parameters.AddWithValue("@datetime", DateTime.Now);

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    insertresult = "Success";
                }

                catch (Exception e)
                {
                    insertresult = "Error";
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        return insertresult;
    }
}