using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User
/// </summary>
/// 
namespace Library
{
    public class User
    {
        protected int m_nId;
        protected List<Book> m_UsersBooks;
        protected string m_strFirstName;
        protected string m_strLastName;
        protected string m_strCardNumber;

        #region Constructs
        public User()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public User(string fName, string lName, string cardNum)
        {
            this.m_strFirstName = fName;
            this.m_strLastName = lName;
            this.m_strCardNumber = cardNum;
        }
        #endregion

        #region Properties
        public int Id
        {
            get { return this.m_nId; }
            set { this.m_nId = value; }
        }
        public string FirstName
        {
            get { return this.m_strFirstName; }
            set { this.m_strFirstName = value; }
        }
        public string LastName
        {
            get { return this.m_strLastName; }
            set { this.m_strLastName = value; }
        }
        public string CardNumber
        {
            get { return this.m_strCardNumber; }
            set { this.m_strCardNumber = value; }
        }
        public List<Book> UsersBooks
        {
            get { return this.m_UsersBooks; }
            set { this.m_UsersBooks = value; }
        }
        #endregion
    }
}