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

    public static string AddNewUser(string Username, string Password)
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
                                            [Username]
                                            ,[Password]
                                                                                                                                                                              
                                        )
                            VALUES      (
                                            @Username
                                            ,@Password
                             
                                        )";

                command.Parameters.AddWithValue("@Username", Username);
                command.Parameters.AddWithValue("@Password", Password);

                try
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    insertresult = "Success";
                }

                catch (Exception e)
                {
                    insertresult = "Error: " + e.ToString();
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