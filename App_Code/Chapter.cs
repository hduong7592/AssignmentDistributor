using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Chapter
/// </summary>
public class Chapter
{
    public int chapterID;
    public string chapterName;
    public string chapterDescription;
    public string aCount;
    public string aName;
    public int aID;

    public Chapter(int chapterID)
    {
        this.chapterID = chapterID;
        DataTable ChapterTB = AppData.GetChapterDetail(chapterID);
        if(ChapterTB.Rows.Count > 0)
        {
            this.chapterID = chapterID;
            this.chapterName = ChapterTB.Rows[0]["ChapterName"].ToString();
            this.chapterDescription = ChapterTB.Rows[0]["ChapterDescription"].ToString();
            this.aCount = Assignment.GetAssignmenCount(chapterID).ToString();
        }
    }

    public Chapter(int chapterID, int aID)
    {
        this.chapterID = chapterID;
        DataTable ChapterTB = AppData.GetChapterDetail(chapterID);
        if (ChapterTB.Rows.Count > 0)
        {
            this.chapterID = chapterID;
            this.chapterName = ChapterTB.Rows[0]["ChapterName"].ToString();
            this.chapterDescription = ChapterTB.Rows[0]["ChapterDescription"].ToString();
            this.aCount = Assignment.GetAssignmenCount(chapterID).ToString();
            Assignment assignment = new Assignment(aID);
            this.aName = assignment.GetAssignmentName();
            this.aID = aID;
        }
    }

    public static int ChapterCount(int courseID)
    {
        int chapterCount = 0;
        DataTable ChapterTB = AppData.GetChapters(courseID);
        chapterCount = ChapterTB.Rows.Count;
        return chapterCount;
    }
}