using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetData();
        }
    }

    private void GetData()
    {
        GridView1.DataBind();

        DataTable Data = AppData.GetData();
        if (Data.Rows.Count > 0)
        {
            GridView1.DataSource = Data;
            GridView1.DataBind();
        }
    }

    protected void SubmitBtn_Click(object sender, EventArgs e)
    {
        string Username = UserTxt.Text;
        string Password = PassTxt.Text;
        //string result = AppData.AddNewUser(Username, Password);
        //Label1.Text = result;
        //GetData();
    }

    protected void SendmailBtn_Click(object sender, EventArgs e)
    {
        string status = SendMail.Sendmail();
        Label2.Text = DateTime.Now + ": " + status;
    }
}