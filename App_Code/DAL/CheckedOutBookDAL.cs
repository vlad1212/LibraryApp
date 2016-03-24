using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CheckedOutBookDAL
/// </summary>
/// 
namespace Library.DAL
{
    public class CheckedOutBookDAL : BookDAL
    {
        protected CheckedOutBook m_CheckedOutBook;

        #region Constructs
        public CheckedOutBookDAL() : base()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public CheckedOutBookDAL(string constr) : base(constr)
        {
            this.m_CheckedOutBook = new CheckedOutBook();
            this.m_strBooksListByUser = "spBooksListByUser";
            this.m_strCheckInBook = "spBookCheckIn";
        }
        ~CheckedOutBookDAL()
        {
            this.Dispose(true);
        }
        #endregion

        #region Properties
        public CheckedOutBook CheckedOutBookInfo
        {
            get { return this.m_CheckedOutBook; }
            set
            {
                if (this.m_CheckedOutBook != null)
                {
                    this.m_CheckedOutBook = null;
                }
                this.m_CheckedOutBook = value;
            }
        }
        #endregion

        #region Public Methods
        public virtual List<CheckedOutBook> ListUsersBooks(int uId)
        {
            List<CheckedOutBook> bookList = new List<CheckedOutBook>();
            try
            {
                this.OpenConn();
                using (SqlCommand cmd = new SqlCommand(this.m_strBooksListByUser, this.m_Connection))
                {
                    cmd.CommandType = this.SetCommadType();
                    cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int, 4));
                    cmd.Parameters["@UserId"].Value = uId;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            CheckedOutBook b = new CheckedOutBook();
                            this.GetProperties(dr, b);
                            bookList.Add(b);
                            while (dr.Read())
                            {
                                b = new CheckedOutBook();
                                this.GetProperties(dr, b);
                                bookList.Add(b);
                            }
                        }
                    }
                }
            }
            catch (Exception er)
            {
                throw er;
            }
            return bookList;
        }
        public virtual void CheckInBook(int id)
        {
            SqlTransaction tx = null;
            try
            {
                this.OpenConn();
                tx = this.m_Connection.BeginTransaction();
                using (SqlCommand cmd = new SqlCommand(this.m_strCheckInBook, this.m_Connection))
                {
                    cmd.Transaction = tx;
                    cmd.CommandType = this.SetCommadType();
                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int, 4));
                    cmd.Parameters["@Id"].Value = id;
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
        private void GetProperties(SqlDataReader data, CheckedOutBook item)
        {
            item.Id = Convert.ToInt32(data["Id"]);
            item.Title = data["Title"].ToString();
            item.Author = data["Author"].ToString();
            item.ISBN = data["ISBN"].ToString();
            item.PageCount = Convert.ToInt32(data["PageCount"]);
            item.PublishDate = Convert.ToDateTime(data["PublishDate"]);
            item.IsAvailable = Convert.ToBoolean(data["IsAvailable"]);
            item.CheckOutDate = Convert.ToDateTime(data["CheckOutDate"]);
            item.LinkId = Convert.ToInt32(data["LinkId"]);
        }
        #endregion
    }
}