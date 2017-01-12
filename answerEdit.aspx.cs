using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    private string getInitialAnswer(int answerId)
    {
        string query = "SELECT * from answers WHERE id = @ANSWER_ID";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);

            com.Parameters.AddWithValue("ANSWER_ID", answerId);

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
        int answerId = int.Parse(Request.QueryString["id"]);
        string initialAnswer = getInitialAnswer(answerId);

        if (IsPostBack == false)
        {
            textAnswer.Value = initialAnswer;
        }
    }
    protected void EditAnswer(Object sender , EventArgs e)
    {
        int answerId = int.Parse(Request.QueryString["id"]);
        int parentId = getParentId(answerId);

        string answer = textAnswer.Value;
        if (justSpaces(answer) == false)
        {
            string query = "UPDATE answers SET continut = @CONTINUT "
                + "WHERE id = @ANSWER_ID";

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

            con.Open();

            try
            {
                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("CONTINUT", answer);
                com.Parameters.AddWithValue("ANSWER_ID", answerId);

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
            Response.Redirect("currentPost.aspx?id=" + parentId);
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
        string query = "SELECT * from answers WHERE id = @ANSWER_ID";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);

            com.Parameters.AddWithValue("ANSWER_ID", answerId);

            SqlDataReader reader = com.ExecuteReader();

            if (reader.Read())
            {
                return int.Parse(reader["post_id"].ToString());
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
    protected void DeleteAnswer(Object sender , EventArgs e)
    {
        int answerId = int.Parse(Request.QueryString["id"]);
        int parentId = getParentId(answerId);

        string query = "DELETE FROM answers "
            + "WHERE id = @ANSWER_ID";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("ANSWER_ID", answerId);


            com.ExecuteNonQuery();
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
            Response.Redirect("currentPost.aspx?id=" + parentId);
        }
        else
        {
            Response.Redirect("Home.aspx");
        }
    }
}