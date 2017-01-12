using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class postEdit : System.Web.UI.Page
{
    private string getInitialTitle(int postId)
    {
        string query = "SELECT * from posts WHERE id = @POST_ID";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);

            com.Parameters.AddWithValue("POST_ID", postId);

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
    private string getInitialContent(int postId)
    {
        string query = "SELECT * from posts WHERE id = @POST_ID";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);

            com.Parameters.AddWithValue("POST_ID", postId);

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
    protected void Page_Load(object sender, EventArgs e)
    {
        MessageID.Text = "";
        int postId = int.Parse(Request.QueryString["id"]);
        string initialTitle = getInitialTitle(postId);
        string initialContent = getInitialContent(postId);

        if (IsPostBack == false)
        {
            textTitle.Value = initialTitle;
            textContent.Value = initialContent;
        }
    }

    protected void EditPost(object sender, EventArgs e)
    {
        int postId = int.Parse(Request.QueryString["id"]);
        int parentId = getParentId(postId);

        string title = textTitle.Value;
        string content = textContent.Value;
        if (justSpaces(title) == false && justSpaces(content) == false)
        {
            string query = "UPDATE posts SET continut = @CONTINUT, nume = @NUME "
                + "WHERE id = @POST_ID";

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

            con.Open();

            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("NUME", title);
                com.Parameters.AddWithValue("CONTINUT", content);
                com.Parameters.AddWithValue("POST_ID", postId);

                com.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageID.Text = "Database update error : " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
        else
        {
            MessageID.Text = "Text is empty!";
        }

        if (parentId != -1)
        {
            Response.Redirect("currentSection.aspx?id=" + parentId);
        }
        else
        {
            Response.Redirect("Home.aspx");
        }
    }
    protected void DeletePost(object sender, EventArgs e)
    {
        int postId = int.Parse(Request.QueryString["id"]);
        int parentId = getParentId(postId);

        string queryAnswers = "DELETE FROM answers "
            + "WHERE post_id = @POST_ID";
        string queryPosts = "DELETE FROM posts "
            + "WHERE id = @POST_ID";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand comAnswers = new SqlCommand(queryAnswers, con);
            SqlCommand comPosts = new SqlCommand(queryPosts, con);

            comAnswers.Parameters.AddWithValue("POST_ID", postId);
            comPosts.Parameters.AddWithValue("POST_ID", postId);

            comAnswers.ExecuteNonQuery();
            comPosts.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageID.Text = "Database delete error : " + ex.Message;
        }
        finally
        {
            con.Close();
        }

        if (parentId != -1)
        {
            Response.Redirect("currentSection.aspx?id=" + parentId);
        }
        else
        {
            Response.Redirect("Home.aspx");
        }
    }
    private bool justSpaces(string text)
    {
        int n = text.Length;
        if (n == 0)
        {
            return true;
        }

        for(int i = 0; i < n; i++ )
        {
            if (text[i] != ' ' && text[i] != '\n')
                return false;
        }
        return true; 
    }
    private int getParentId(int answerId)
    {
        string query = "SELECT * from posts WHERE id = @POST_ID";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);

            com.Parameters.AddWithValue("POST_ID", answerId);

            SqlDataReader reader = com.ExecuteReader();

            if (reader.Read())
            {
                return int.Parse(reader["sectiune_id"].ToString());
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
        return -1;
    }
}