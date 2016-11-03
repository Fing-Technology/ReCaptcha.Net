using System;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblResult.Text = "You Got It!";
            lblResult.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lblResult.Text = "Incorrect";
            lblResult.ForeColor = System.Drawing.Color.Red;
        }
    }
}