using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BookDAL
/// </summary>
///
namespace Library.DAL
{
    public class BookDAL : BaseDAL
    {
        protected Book m_Book;
        protected string m_strFindByTitle;
        protected string m_strFindByAuthor;
        protected string m_strFindByISBN;
        protected string m_strBooksListByUser;
        protected string m_strCheckOutBook;
        protected string m_strCheckInBook;

        #region Constructs
        public BookDAL() : base()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public BookDAL(string constr) : base(constr)
        {
            this.m_Book = new Book();
            this.m_strFindByTitle = "spBooksFindByTitle";
            this.m_strFindByAuthor = "spBooksFindByAuthor";
            this.m_strFindByISBN = "spBooksFindByISBN";
            this.m_strBooksListByUser = "spBooksListByUser";
            this.m_strCheckOutBook = "spBookCheckOut";
            this.m_strCheckInBook = "spBookCheckIn";
        }
        ~BookDAL()
        {
            this.Dispose(true);
        }
        #endregion

        #region Properties
        public Book BookInfo
        {
            get { return this.m_Book; }
            set
            {
                if (this.m_Book != null)
                {
                    this.m_Book = null;
                }
                this.m_Book = value;
            }
        }
        #endregion

        #region Public Functions
        public virtual DataSet FindByAuthor(string name)
        {
            DataSet ds = new DataSet();
            try
            {
                this.OpenConn();
                using (SqlCommand cmd = new SqlCommand(this.m_strFindByAuthor, this.m_Connection))
                {
                    cmd.CommandType = this.SetCommadType();
                    cmd.Parameters.Add(new SqlParameter("@Author", SqlDbType.VarChar, 50));
                    cmd.Parameters["@Author"].Value = name;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            catch (Exception er)
            {
                throw er;
            }
            return ds;
        }
        public virtual DataSet FindByTitle(string title)
        {
            DataSet ds = new DataSet();
            try
            {
                this.OpenConn();
                using (SqlCommand cmd = new SqlCommand(this.m_strFindByTitle, this.m_Connection))
                {
                    cmd.CommandType = this.SetCommadType();
                    cmd.Parameters.Add(new SqlParameter("@Title", SqlDbType.VarChar, 100));
                    cmd.Parameters["@Title"].Value = title;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            catch (Exception er)
            {
                throw er;
            }
            return ds;
        }
        public virtual DataSet FindByISBN(string isbn)
        {
            DataSet ds = new DataSet();
            try
            {
                this.OpenConn();
                using (SqlCommand cmd = new SqlCommand(this.m_strFindByISBN, this.m_Connection))
                {
                    cmd.CommandType = this.SetCommadType();
                    cmd.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.VarChar, 100));
                    cmd.Parameters["@ISBN"].Value = isbn;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            catch (Exception er)
            {
                throw er;
            }
            return ds;
        }
        public virtual void CheckOutBook(int userId, int bookId)
        {
            SqlTransaction tx = null;
            try
            {
                this.OpenConn();
                tx = this.m_Connection.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand(this.m_strCheckOutBook, this.m_Connection))
                {
                    cmd.Transaction = tx;
                    cmd.CommandType = this.SetCommadType();
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int, 4));
                    cmd.Parameters.Add(new SqlParameter("@BookId", SqlDbType.Int, 4));
                    cmd.Parameters["@UserId"].Value = userId;
                    cmd.Parameters["@BookId"].Value = bookId;
                    cmd.ExecuteNonQuery();
                    tx.Commit();
                    cmd.Dispose();
                }
            }
            catch (SqlException sqler)
            {
                tx.Rollback();
                throw sqler;
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                if (tx != null)
                {
                    tx.Dispose();
                }
            }
        }
        #endregion

        #region Private Functions
        private void SetProperties(SqlCommand c)
        {
            c.Parameters.Add(new SqlParameter("@Title", SqlDbType.VarChar, 100));
            c.Parameters.Add(new SqlParameter("@Author", SqlDbType.VarChar, 50));
            c.Parameters.Add(new SqlParameter("@ISBN", SqlDbType.VarChar, 50));
            c.Parameters.Add(new SqlParameter("@PageCount", SqlDbType.Int, 4));
            c.Parameters.Add(new SqlParameter("@PublishDate", SqlDbType.SmallDateTime, 4));
            c.Parameters.Add(new SqlParameter("@IsAvailable", SqlDbType.Bit, 1));

            c.Parameters["@Title"].Value = this.m_Book.Title;
            c.Parameters["@Author"].Value = this.m_Book.Author;
            c.Parameters["@ISBN"].Value = this.m_Book.ISBN;
            c.Parameters["@PageCount"].Value = this.m_Book.PageCount;
            c.Parameters["@PublishDate"].Value = this.m_Book.PublishDate;
            c.Parameters["@IsAvailable"].Value = this.m_Book.IsAvailable;
        }
        private void GetProperties(SqlDataReader data)
        {
            this.m_Book.Id = Convert.ToInt32(data["Id"]);
            this.m_Book.Title = data["Title"].ToString();
            this.m_Book.Author = data["Author"].ToString();
            this.m_Book.ISBN = data["ISBN"].ToString();
            this.m_Book.PageCount = Convert.ToInt32(data["PageCount"]);
            this.m_Book.PublishDate = Convert.ToDateTime(data["PublishDate"]);
            this.m_Book.IsAvailable = Convert.ToBoolean(data["IsAvailable"]);
        }
        private void GetProperties(SqlDataReader data, Book item)
        {
            item.Id = Convert.ToInt32(data["Id"]);
            item.Title = data["Title"].ToString();
            item.Author = data["Author"].ToString();
            item.ISBN = data["ISBN"].ToString();
            item.PageCount = Convert.ToInt32(data["PageCount"]);
            item.PublishDate = Convert.ToDateTime(data["PublishDate"]);
            item.IsAvailable = Convert.ToBoolean(data["IsAvailable"]);
        }
        #endregion
    }
}
