using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageID.Text = "";
        TitleID.InnerText = getCurrentPostTitle();

        currentPost.Text = loadCurrentPost();
        //loadAnswers();
    }

    private string getCurrentPostTitle()
    {
        int postId = int.Parse(Request.QueryString["id"]);
        string query = "SELECT nume from posts WHERE id = @ID_POST";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);

            com.Parameters.AddWithValue("ID_POST", postId);

            SqlDataReader reader = com.ExecuteReader();
            if (reader.Read())
            {
                return reader["nume"].ToString();
            }
        }
        catch (Exception ex)
        {
            MessageID.Text = "Database select error : " + ex.Message;
        }
        finally
        {
            con.Close();
        }
        return "";
    }
    private string loadCurrentPost()
    {
        int postId = int.Parse(Request.QueryString["id"]);
        string query = "SELECT continut from posts WHERE id = @ID_POST";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);

            com.Parameters.AddWithValue("ID_POST", postId);

            SqlDataReader reader = com.ExecuteReader();
            if (reader.Read())
            {
                return reader["continut"].ToString();
            }
        }
        catch (Exception ex)
        {
            MessageID.Text = "Database select error : " + ex.Message;
        }
        finally
        {
            con.Close();
        }
        return "";
    }
}
