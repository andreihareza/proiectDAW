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
        TitleID.InnerText = getCurrentSectionTitle();

        loadPosts();
    }

    private string getCurrentSectionTitle()
    {
        int sectionId = int.Parse(Request.QueryString["id"]);
        string query = "SELECT nume from sections WHERE id = @ID_SECTIUNE";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);

            com.Parameters.AddWithValue("ID_SECTIUNE", sectionId);

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

    private string getUserNameById(int id)
    {
        string query = "SELECT name from users WHERE id = @ID_USER";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);

            com.Parameters.AddWithValue("ID_USER", id);

            SqlDataReader reader = com.ExecuteReader();
            if (reader.Read())
            {
                return reader["name"].ToString();
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

    private void loadPosts()
    {
        int sectionId = int.Parse(Request.QueryString["id"]);
        string query = "SELECT * from posts WHERE sectiune_id = @ID_SECTIUNE";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);

            com.Parameters.AddWithValue("ID_SECTIUNE", sectionId);

            SqlDataReader reader = com.ExecuteReader();
            string list = "<ul class='list-group'>";
            while (reader.Read())
            {
                int userId = int.Parse(reader["user_id"].ToString());
                list += "<a href ='currentPost.aspx?id=" + reader["Id"] + "'><li class='list-group-item'><div class='section'><h4 class='text-center'>";
                list += reader["nume"].ToString() + "</h4>";
                list += "Created by: " + getUserNameById(userId) + "<div class='pull-right'>Created at: " + reader["created_at"].ToString() + "</div>";
                list += "</div></li></a>";
            }
            list += "</ul>";

            postsList.Text = list;



        }
        catch (Exception ex)
        {
            MessageID.Text = "Database select error : " + ex.Message;
        }
        finally
        {
            con.Close();
        }
    }
}