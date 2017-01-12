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

        getData();
    }

    private void getData()
    {
        string query = "SELECT * from sections";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);

            SqlDataReader reader = com.ExecuteReader();
            string list = "<ul class='list-group'>"; 
            while (reader.Read())
            {
                list += "<a href='currentSection.aspx?id=" + reader["Id"] + "'><li class='list-group-item'><div class='section'><h3 class='text-center'>"; 
                list += reader["nume"].ToString()+"</h3>";
                list += reader["descriere"].ToString();
                list += "<div class='pull-right'>Created at: " + reader["created_at"].ToString() + "</div>"; 
                list += "</div></li></a>"; 
            }
            list += "</ul>";

            sectionsList.Text = list;



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
