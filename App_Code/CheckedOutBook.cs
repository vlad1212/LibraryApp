using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CheckedOutBook
/// </summary>
/// 
namespace Library
{
    public class CheckedOutBook : Book
    {
        protected int m_nLinkId;
        protected DateTime m_dtCheckOutDate;

        #region Constructs
        public CheckedOutBook() : base()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public CheckedOutBook(DateTime checkoutDate, string title, string author, string isbn, int pageCount, DateTime pubDate, bool isAvailable) : base(title, author, isbn, pageCount, pubDate, isAvailable)
        {
            this.m_dtCheckOutDate = checkoutDate;
            this.m_strTitle = title;
            this.m_strAuthor = author;
            this.m_strISBN = isbn;
            this.m_nPageCount = pageCount;
            this.m_dtPublishDate = pubDate;
            this.m_bIsAvailable = isAvailable;
        }
        #endregion

        #region Properties
        public DateTime CheckOutDate
        {
            get { return this.m_dtPublishDate; }
            set { this.m_dtPublishDate = value; }
        }
        public int LinkId
        {
            get { return this.m_nLinkId; }
            set { this.m_nLinkId = value; }
        }
        #endregion
    }
}