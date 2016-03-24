using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserDAL
/// </summary>
/// 
namespace Library.DAL
{
    public class UserDAL : BaseDAL
    {
        private User m_User;

        #region Constructs
        public UserDAL() : base()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public UserDAL(string constr) : base(constr)
        {
            this.m_User = new User();
            this.m_strListSql = "prUsersList";
        }
        ~UserDAL()
        {
            this.Dispose(true);
        }
        #endregion
        public User UserInfo
        {
            get { return this.m_User; }
            set
            {
                if (this.m_User != null)
                {
                    this.m_User = null;
                }
                this.m_User = value;
            }
        }
        #region Properties

        #endregion

        #region Public Functions
        //public virtual User FindById(int id)
        //{
        //    if(this.m_User == null)
        //    {
        //        this.m_User = new User();
        //    }
        //    this.m_User.Id = id;
        //}
        //public virtual User FindByCardNumber(string card)
        //{
        //    if (this.m_User == null)
        //    {
        //        this.m_User = new User();
        //    }
        //    this.m_User.CardNumber = card;
        //}
        //public virtual User FindByLastName(string name)
        //{
        //    if (this.m_User == null)
        //    {
        //        this.m_User = new User();
        //    }
        //    this.m_User.LastName = name;
        //}
        #endregion

        #region Private Functions
        private void SetProperties(SqlCommand c)
        {
            c.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.VarChar, 50));
            c.Parameters.Add(new SqlParameter("@LastName", SqlDbType.VarChar, 50));
            c.Parameters.Add(new SqlParameter("@CardNumber", SqlDbType.VarChar, 50));

            c.Parameters["@FirstName"].Value = this.m_User.FirstName;
            c.Parameters["@LastName"].Value = this.m_User.LastName;
            c.Parameters["@Cardnumber"].Value = this.m_User.CardNumber;
        }
        private void GetProperties(SqlDataReader data)
        {
            this.m_User.Id = Convert.ToInt32(data["Id"]);
            this.m_User.FirstName = data["FirstName"].ToString();
            this.m_User.LastName = data["LastName"].ToString();
            this.m_User.CardNumber = data["CardNumber"].ToString();
        }
        #endregion
    }
}