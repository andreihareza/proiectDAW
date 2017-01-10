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
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string query = "SELECT id, name, parola from users WHERE name LIKE @NAME";

        string userName = UserID.Text;
        string password = PasswordID.Text;

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("NAME", userName);

            SqlDataReader reader = com.ExecuteReader();

            if (reader.Read())
            {
                if (reader["parola"].ToString() == password)
                {
                    Session["loggedIn"] = reader["id"].ToString();
                }
                else
                {
                    MessageID.Text = "Incorrect password";
                }
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


    }
}