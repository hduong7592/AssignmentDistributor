using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int userID = 3;
        
        ArrayList courseList = new ArrayList();       
       

        DataTable coursesTB = AppData.GetCourses(userID);
        if(coursesTB.Rows.Count > 0)
        {           

            foreach(DataRow row in coursesTB.Rows)
            {
                int courseID = Convert.ToInt16(row["courseID"]);
                courseList.Add(courseID);

               
                //Response.Write("CourseID :" + courseID + "<br>");
                DataTable studentsTB = AppData.GetStudentsInCourse(courseID);
                

                ArrayList chapterList = new ArrayList();
               
                DataTable chaptersTB = AppData.GetChapters(courseID);
                if(chaptersTB.Rows.Count > 0)
                {
                    foreach(DataRow chapter in chaptersTB.Rows)
                    {
                        ArrayList studentList = new ArrayList();
                        ArrayList assignmentList = new ArrayList();

                        int chapterID = Convert.ToInt32(chapter["ChapterID"]);
                        chapterList.Add(chapterID);
                        Response.Write("Chapter ID: " + chapterID + "<br>");

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
                                    Response.Write("CourseID: " + courseID + ", StudentID " + studentID + "<br>");
                                }


                            }
                        }

                        DataTable assignmentsTB = AppData.GetAssignments(chapterID);
                        if(assignmentsTB.Rows.Count > 0)
                        {
                            int i = 0;
                            foreach(DataRow a in assignmentsTB.Rows)
                            {
                                int aID = Convert.ToInt32(a["aID"]);
                                assignmentList.Add(aID);
                                Response.Write("AssignmentID: " + aID + "<br>");

                               
                            }
                        }
                                               
                      
                        //assignmentList = RandomOrder(assignmentList);
                        studentList = RandomOrder(studentList);

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
                                    string result = AppData.AssignAssignment(studentID, aID, chapterID, courseID, userID);
                                    Response.Write("Add result: " + studentID + ", " + aID + "," + chapterID + ", " + courseID + ", " + userID + ":" + result + "<br>");
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
                                assignmentList = RandomOrder(assignmentList);                                
                                int aID = Convert.ToInt32(assignmentList[0]);
                                string result = AppData.AssignAssignment(studentID, aID, chapterID, courseID, userID);
                                Response.Write("Add result: " + studentID + ", " + aID + "," + chapterID + ", " + courseID + ", " + userID + ":" + result + "<br>");
                                
                            }

                        }                          
                       
                    }
                }  
                
            }
        }       
    }

    public ArrayList RandomOrder(ArrayList arrList)
    {
        Random r = new Random();
        for (int cnt = 0; cnt < arrList.Count; cnt++)
        {
            object tmp = arrList[cnt];
            int idx = r.Next(arrList.Count - cnt) + cnt;
            arrList[cnt] = arrList[idx];
            arrList[idx] = tmp;
        }

        return arrList;

    }
}