using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Book
/// </summary>
/// 
namespace Library
{
    public class Book
    {
        protected int m_nId;
        protected string m_strTitle;
        protected string m_strAuthor;
        protected string m_strISBN;
        protected int m_nPageCount;
        protected DateTime m_dtPublishDate;
        protected bool m_bIsAvailable;

        #region Constructs
        public Book()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public Book(string title, string author, string isbn, int pageCount, DateTime pubDate, bool isAvailable)
        {
            this.m_strTitle = title;
            this.m_strAuthor = author;
            this.m_strISBN = isbn;
            this.m_nPageCount = pageCount;
            this.m_dtPublishDate = pubDate;
            this.m_bIsAvailable = isAvailable;
        }
        #endregion

        #region Properties
        public int Id
        {
            get { return this.m_nId; }
            set { this.m_nId = value; }
        }
        public string Title
        {
            get { return this.m_strTitle; }
            set { this.m_strTitle = value; }
        }
        public string Author
        {
            get { return this.m_strAuthor; }
            set { this.m_strAuthor = value; }
        }
        public string ISBN
        {
            get { return this.m_strISBN; }
            set { this.m_strISBN = value; }
        }
        public int PageCount
        {
            get { return this.m_nPageCount; }
            set { this.m_nPageCount = value; }
        }
        public DateTime PublishDate
        {
            get { return this.m_dtPublishDate; }
            set { this.m_dtPublishDate = value; }
        }
        public bool IsAvailable
        {
            get { return this.m_bIsAvailable; }
            set { this.m_bIsAvailable = value; }
        }
        #endregion
    }
}