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
        answersList.Text = loadAnswers();
        if (Session != null && Session["loggedIn"] != null && Session["loggedIn"].ToString() != "")
        {
            answerVisibility.Visible = true;

        }
        else
        {
            answerVisibility.Visible = false; 

        }
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
    private string loadAnswers()
    {
        int postId = int.Parse(Request.QueryString["id"]);
        string query = "SELECT * from answers WHERE post_id = @ID_POST "
            + "ORDER BY updated_at DESC";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);

            com.Parameters.AddWithValue("ID_POST", postId);

            SqlDataReader reader = com.ExecuteReader();

            string list = "<ul class='list-group'>"; 
            while (reader.Read())
            {
                list += "<li class='list-group-item'><div class='section'><h5 class=''>"; 
                list += reader["continut"].ToString()+"</h5>";
                list += "Answered by: " + getUserNameById(Int32.Parse(reader["user_id"].ToString())); 
                list += "<div class='pull-right'>Created at: " + reader["updated_at"].ToString() + "</div>"; 
                list += "</div></li>"; 
            }
            list += "</ul>";

            return list;
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
        string query = "SELECT * from posts WHERE id = @ID_POST";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);

            com.Parameters.AddWithValue("ID_POST", postId);

            SqlDataReader reader = com.ExecuteReader();
            if (reader.Read())
            {
                return "<h4>" + reader["continut"].ToString() + "</h4>"
                    + "Created by: " + getUserNameById(Int32.Parse(reader["user_id"].ToString()))
                    + "<div class='pull-right'>" 
                    + "Created at: " + reader["created_at"].ToString() + "</div>";

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
    
    protected void AddAnswer(object Sender , EventArgs e)
    {
        int postId = int.Parse(Request.QueryString["id"]);

        string query = "INSERT INTO answers (continut, user_id, post_id, created_at, updated_at)"
            + " VALUES (@CONTENT, @USER_ID, @POST_ID, @CREATED_AT, @UPDATED_AT)";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            DateTime currentTime = DateTime.Now;

            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("CONTENT", textAnswer.Value);
            com.Parameters.AddWithValue("USER_ID", Session["loggedIn"]);
            com.Parameters.AddWithValue("POST_ID", postId);
            com.Parameters.AddWithValue("CREATED_AT", currentTime);
            com.Parameters.AddWithValue("UPDATED_AT", currentTime);

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
