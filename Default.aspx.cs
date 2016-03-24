using Library.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!this.IsPostBack)
        {
            using(UserDAL u = new UserDAL(Application["LibraryDB"].ToString()))
            {
                gvUsers.DataSource = u.List();
                gvUsers.DataBind();
                u.Dispose();
            }
        }
    }

    protected void gvUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        this.BindCheckedOutGrid();
    }

    protected void btnFindBooks_Click(object sender, EventArgs e)
    {
        this.BindBooksGrid();
    }

    protected void gvBooks_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(gvUsers.SelectedIndex == -1)
        {
            lblMsg.Text = "Please select a user to check out a book.";
        }
        else if (!string.IsNullOrEmpty(gvUsers.DataKeys[gvUsers.SelectedRow.RowIndex]["CardNumber"].ToString())) { 
            if (Convert.ToBoolean(gvBooks.DataKeys[gvBooks.SelectedRow.RowIndex]["IsAvailable"].ToString()))
            {
                using (BookDAL b = new BookDAL(Application["LibraryDB"].ToString()))
                {
                    b.CheckOutBook(Convert.ToInt32(gvUsers.DataKeys[gvUsers.SelectedRow.RowIndex]["Id"].ToString()), Convert.ToInt32(gvBooks.DataKeys[gvBooks.SelectedRow.RowIndex]["Id"].ToString()));
                    b.Dispose();
                }
                this.BindBooksGrid();
                this.BindCheckedOutGrid();
            }
            else
            {
                lblMsg.Text = "This book is already checked out.";
            }
        }
        else
        {
            lblMsg.Text = "Selected user does not have a library card.";
        }
    }

    protected void gvCheckedOut_SelectedIndexChanged(object sender, EventArgs e)
    {
        using (CheckedOutBookDAL b = new CheckedOutBookDAL(Application["LibraryDB"].ToString()))
        {
            b.CheckInBook(Convert.ToInt32(gvCheckedOut.DataKeys[gvCheckedOut.SelectedRow.RowIndex]["LinkId"].ToString()));
            b.Dispose();
        }
        this.BindBooksGrid();
        this.BindCheckedOutGrid();
    }
    private void BindCheckedOutGrid()
    {
        using (CheckedOutBookDAL b = new CheckedOutBookDAL(Application["LibraryDB"].ToString()))
        {
            gvCheckedOut.DataSource = b.ListUsersBooks(Convert.ToInt32(gvUsers.DataKeys[gvUsers.SelectedRow.RowIndex]["Id"].ToString()));
            gvCheckedOut.DataBind();
            b.Dispose();
        }
    }
    private void BindBooksGrid()
    {
        lblMsg.Text = "";
        if (!string.IsNullOrEmpty(tbSearch.Text))
        {
            using (BookDAL b = new BookDAL(Application["LibraryDB"].ToString()))
            {
                switch (ddlSearchtype.SelectedValue)
                {
                    case "0":
                        gvBooks.DataSource = b.FindByISBN(tbSearch.Text);
                        break;
                    case "1":
                        gvBooks.DataSource = b.FindByTitle(tbSearch.Text);
                        break;
                    case "2":
                        gvBooks.DataSource = b.FindByAuthor(tbSearch.Text);
                        break;
                }
                gvBooks.DataBind();
                b.Dispose();
            }
        }
        else
        {
            lblMsg.Text = "Please enter a value in the search field.";
        }
    }
}