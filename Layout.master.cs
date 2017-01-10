using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session != null && Session["loggedIn"] != null && Session["loggedIn"].ToString() != "")
        {
            loginButton.Visible = false;
            registerButton.Visible = false;
            logoutButton.Visible = true;
        }
        else
        {
            loginButton.Visible = true;
            registerButton.Visible = true;
            logoutButton.Visible = false;
        }

    }

    protected void clickLogout(object sender, EventArgs e)
    {
        Session["loggedIn"] = "";

        Response.Redirect(Request.RawUrl);
    }
}
