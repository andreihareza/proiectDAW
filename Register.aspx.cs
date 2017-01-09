using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private bool alreadyExist(string userName)
    {
        string query = "SELECT * from users WHERE name LIKE @NAME";
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("NAME", userName);

            SqlDataReader reader = com.ExecuteReader();

            if (reader.HasRows)
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            MessageID.Text = "Database select error : " + ex.Message;
            return true;
        }
        finally
        {
            con.Close();
        }

        return false;
    }

    private bool insertUser(string user, string password, string email)
    {
        string query = "INSERT INTO users (name, parola, email, date_register, activated)"
            + " VALUES (@USER, @PASSWORD, @EMAIL, @DATE, @ACTIVATED)";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("USER", user);
            com.Parameters.AddWithValue("PASSWORD", password);
            com.Parameters.AddWithValue("EMAIL", email);
            com.Parameters.AddWithValue("DATE", DateTime.Now);
            com.Parameters.AddWithValue("ACTIVATED", false);

            com.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageID.Text = "Database user insert error : " + ex.Message;
            return false;
        }
        finally
        {
            con.Close();
        }

        return true;
    }

    private int getInsertedUserId(string name)
    {
        string query = "SELECT id from users WHERE name LIKE @NAME";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("NAME", name);

            SqlDataReader reader = com.ExecuteReader();

            if (reader.Read())
            {
                return Int32.Parse(reader["id"].ToString());
            }
            return -1;
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

    private bool insertRole(int userId, int roleId)
    {
        string query = "INSERT INTO user_roles (user_id, role_id)"
            + " VALUES (@USERID, @ROLEID)";

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("USERID", userId);
            com.Parameters.AddWithValue("ROLEID", roleId);

            com.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageID.Text = "Database role insert error : " + ex.Message;
            return false;
        }
        finally
        {
            con.Close();
        }

        return true;
    }

    private int getRoleIdByName(string roleName)
    {
        string query = "SELECT id from roles WHERE role LIKE @ROLENAME";
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\proiectDAW\App_Data\proiectDAW.mdf;Integrated Security=True");

        con.Open();

        try
        {
            SqlCommand com = new SqlCommand(query, con);
            com.Parameters.AddWithValue("ROLENAME", roleName);

            SqlDataReader reader = com.ExecuteReader();

            while(reader.Read())
            {
                return Int32.Parse(reader["id"].ToString());
            }
        }
        catch (Exception ex)
        {
            MessageID.Text = "Database roleid select error : " + ex.Message;
            return -1;
        }
        finally
        {
            con.Close();
        }

        return -1;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string user = UserID.Text;
            string password = PasswordID.Text;
            string confirm = ConfirmID.Text;
            string email = EmailID.Text;

            MessageID.Text = "";
            PasswordConfirmFailedID.Text = "";

            if (password == confirm)
            {
                try
                {
                    if (alreadyExist(user) == false)
                    {
                        bool result = insertUser(user, password, email);
                        if (result == true)
                        {
                            int userid = getInsertedUserId(user);
                            if (userid != -1)
                            {
                                int regularUserId = getRoleIdByName("regular");
                                if (regularUserId != -1)
                                {
                                    result = insertRole(userid, regularUserId);
                                }
                                else
                                {
                                    result = false;
                                }
                            }
                            else
                            {
                                result = false;
                            }
                        }

                        if (result == true)
                        {
                            MessageID.Text = "Success";
                        }
                    }
                }
                catch (SqlException se)
                {
                    MessageID.Text = "Database connection error : " + se.Message;
                }
                catch (Exception ex)
                {
                    MessageID.Text = "Data conversion error : " + ex.Message;
                }
            }
            else
            {
                PasswordConfirmFailedID.Text = "Passwords does not match";
            }
        }
    }
}