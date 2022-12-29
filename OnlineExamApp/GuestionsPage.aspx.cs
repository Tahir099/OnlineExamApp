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
    public partial class GuestionsPage : System.Web.UI.Page
    {
        SqlConnection con = null;
        int a;
        protected void Page_Load(object sender, EventArgs e)
        {
            LabelHeader.Text = Convert.ToString(Session["username"]);
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("get_guestion", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@username", Convert.ToString(Session["username"]));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            a = dt.Rows.Count;

            for (int i = 0; i < a; i++)
            {
                Label myLabel = new Label();
                TextBox textBox = new TextBox();

                myLabel.Text = Convert.ToString(dt.Rows[i]["guestion"]);
                myLabel.ID = "Label" + i.ToString();
                myLabel.Font.Size = 15;
                Panel1.Controls.Add(myLabel);
                Panel1.Controls.Add(new LiteralControl("<br />"));
                textBox.ID = "tb" + i.ToString();
                Panel1.Controls.Add(textBox);
                Panel1.Controls.Add(new LiteralControl("<br />"));
                Panel1.Controls.Add(new LiteralControl("<br />"));

            }

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString);


            con.Open();
            for (int i = 0;i<a;i++)
            {

                SqlCommand cmd = new SqlCommand("insert_answers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user_", Convert.ToString(LabelHeader.Text));
                Label lbl = (Label)Panel1.FindControl("Label" + i.ToString());
                cmd.Parameters.AddWithValue("@guestion", Convert.ToString(lbl.Text));
                TextBox tb = (TextBox)Panel1.FindControl("tb" + i.ToString());
                cmd.Parameters.AddWithValue("@answer", Convert.ToString(tb.Text));
                tb.Text = "";
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

            }
            con.Close();
        }
    }
}