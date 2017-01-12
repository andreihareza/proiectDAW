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
            MessageID.Text = "Database select error getcurrentposttitle : " + ex.Message;
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
                list += "<li class='list-group-item'><div class='section'>";
                list += "<div class='row'><div class='col-xs-11 col-sm-11 col-md11 col-lg-11'><h5>"; 
                list += reader["continut"].ToString()+"</h5></div>";
                if (Session != null && Session["loggedIn"] != null &&
                    (reader["user_id"].ToString() == Session["loggedIn"].ToString() ||
                    isModerator(int.Parse(Session["loggedIn"].ToString()))))
                {
                    list += "<div class='col-xs-1 col-sm-1 col-md1 col-lg-1'>"
                        + "<a href='answerEdit.aspx?id=" + int.Parse(reader["id"].ToString())
                        + "'<span class='glyphicon glyphicon-edit'></span></a></div>";
                }
                list += "</div>";
                list += "Answered by: " + getUserNameById(Int32.Parse(reader["user_id"].ToString())); 
                list += "<div class='pull-right'>Created at: " + reader["updated_at"].ToString() + "</div>"; 
                list += "</div></li>"; 
            }
            list += "</ul>";

            return list;
        }
        catch (Exception ex)
        {
            MessageID.Text = "Database select error loadanswers : " + ex.Message;
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
                return "<div class='row'><div class='col-xs-11 col-sm-11 col-md-11 col-lg-11'><h4>" + reader["continut"].ToString() + "</h4></div>"
                    + ((Session != null && Session["loggedIn"] != null &&
                    (reader["user_id"].ToString() == Session["loggedIn"].ToString()
                    || isModerator(int.Parse(Session["loggedIn"].ToString()))))?
                    "<div class='col-xs-1 col-sm-1 col-md-1 col-lg-1'>"
                    + "<a href='postEdit.aspx?id=" + postId + "'"
                    + "<span class='glyphicon glyphicon-edit'></span></a></div>" : "")
                    + "</div>"
                    + "Created by: " + getUserNameById(Int32.Parse(reader["user_id"].ToString()))
                    + "<div class='pull-right'>" 
                    + "Created at: " + reader["created_at"].ToString() + "</div>";

            }
        }
        catch (Exception ex)
        {
            MessageID.Text = "Database select error loadcurrentpost : " + ex.Message;
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
            MessageID.Text = "Database select error getusernamebyid : " + ex.Message;
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

    bool isModerator(int userId)
    {
        string query = "SELECT r.role AS r_role FROM user_roles AS ur JOIN roles AS r " + 
            "ON ur.role_id = r.Id WHERE ur.user_id = @USER_ID";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);

            com.Parameters.AddWithValue("USER_ID", userId);

            SqlDataReader reader = com.ExecuteReader();
            if (reader.Read())
            {
                string role = reader["r_role"].ToString();
                return role == "moderator" || role == "admin";
            }
        }
        catch (Exception ex)
        {
            MessageID.Text = "Database select error ismoderator: " + ex.Message;
        }
        finally
        {
            con.Close();
        }
        return false; 
    }

}
