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
        FormPost.Text = "";
        postTemplate.Visible = false;
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
        string query = "SELECT * from posts WHERE sectiune_id = @ID_SECTIUNE " +
            "ORDER BY update_at DESC";

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
    protected void Button1_Click(object sender, EventArgs e)
    {
        postTemplate.Visible = true;
    }
    protected void AddPost(object sender, EventArgs e)
    {
        int sectionId = int.Parse(Request.QueryString["id"]);

        string query = "INSERT INTO posts (nume, continut, created_at, update_at, user_id, sectiune_id)"
            + " VALUES (@NAME, @CONTENT, @CREATED_AT, @UPDATED_AT, @USER_ID, @SECTION_ID)";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            DateTime currentTime = DateTime.Now;

            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("name", titlu.Value);
            com.Parameters.AddWithValue("CONTENT", continut.Value);
            com.Parameters.AddWithValue("CREATED_AT", currentTime);
            com.Parameters.AddWithValue("UPDATED_AT", currentTime);
            com.Parameters.AddWithValue("USER_ID", Session["loggedIn"]);
            com.Parameters.AddWithValue("SECTION_ID", sectionId);

            com.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageID.Text = "Database user insert error : " + ex.Message;
        }
        finally
        {
            con.Close();
        }
        Response.Redirect(Request.RawUrl);
    }

}