using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for BaseDAL
/// </summary>
/// 
namespace Library.DAL
{
    public class BaseDAL : IDisposable
    {
        //protected int m_nId;
        protected bool m_bDisposed;
        protected bool m_bUseStoredProc;
        protected string m_strConnectionString;
        protected string m_strListSql;
        protected SqlConnection m_Connection;

        #region Constructs
        public BaseDAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public BaseDAL(string constr)
        {
            this.m_bDisposed = false;
            this.m_bUseStoredProc = true;
            this.m_strConnectionString = constr;
            this.m_strListSql = null;
        }
        ~BaseDAL()
        {
            this.Dispose(true);
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!this.m_bDisposed)
                {
                    if (this.IsConnected())
                    {
                        this.m_Connection.Close();
                    }
                    this.m_bDisposed = true;
                }
            }
        }
        #endregion

        #region Properties
        public string ConnectionString
        {
            get { return this.m_strConnectionString; }
            set { this.m_strConnectionString = value; }
        }
        #endregion

        #region Public Methods

        public virtual DataSet List()
        {
            DataSet ds = new DataSet();
            try
            {
                this.OpenConn();
                using (SqlCommand cmd = new SqlCommand(this.m_strListSql, this.m_Connection))
                {
                    cmd.CommandType = this.SetCommadType();
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

        public bool IsConnected()
        {
            bool open = false;
            if (this.m_Connection != null)
            {
                open = (this.m_Connection.State == ConnectionState.Open ? true : false);
            }
            return open;
        }
        #endregion

        #region Protected Methods
        protected virtual void OpenConn()
        {
            if (this.m_Connection == null)
            {
                this.m_Connection = new SqlConnection(this.m_strConnectionString);
            }
            if (this.m_Connection.State == ConnectionState.Closed)
            {
                this.m_Connection.Open();
                this.m_bDisposed = false;
            }
        }
        protected virtual CommandType SetCommadType()
        {
            return (this.m_bUseStoredProc ? CommandType.StoredProcedure : CommandType.Text);
        }
        #endregion
    }
}