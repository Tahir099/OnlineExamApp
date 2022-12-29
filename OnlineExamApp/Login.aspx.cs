using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace OnlineExamApp
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con = null;
        protected void Page_Load(object sender, EventArgs e)
        {
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("check_user", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username" , TextBox1.Text);
            cmd.Parameters.AddWithValue("@password", TextBox2.Text);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Session["username"] = TextBox1.Text;
                Response.Redirect("GuestionsPage.aspx");
            }
            con.Close();
        }
    }
}