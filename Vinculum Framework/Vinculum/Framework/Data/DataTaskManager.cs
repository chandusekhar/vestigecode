namespace Vinculum.Framework.Data
{
    using Microsoft.Practices.EnterpriseLibrary.Data;
    using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Data.MySql;
    using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
    using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.Common;
    using System.Data.OracleClient;
    using Vinculum.Framework.DataTypes;
    using System.Xml.Serialization;
    using Vinculum.Framework.Cryptography;

    public class DataTaskManager : IDisposable
    {
        private DbConnection m_databaseConnection;
        private Database m_databaseObject;
        private Stack<DbTransaction> m_databaseTransaction;
        private string dicryptedConnectionString;
        private string m_dbProvider;
        private bool ConnectionEncrypted;

        public DataTaskManager()
        {
            ConnectionEncrypted = Convert.ToBoolean(ConfigurationManager.AppSettings["ConnectionEncrypted"]);
            this.m_databaseObject = this.Create((ConfigurationManager.GetSection("dataConfiguration") as DatabaseSettings).DefaultDatabase);

            this.m_databaseTransaction = new Stack<DbTransaction>();
        }

        public DataTaskManager(string databaseInstance)
        {
            ConnectionEncrypted = Convert.ToBoolean(ConfigurationManager.AppSettings["ConnectionEncrypted"]);
            this.m_databaseObject = this.Create(databaseInstance);
            this.m_databaseTransaction = new Stack<DbTransaction>();
        }

        protected void AddParameter(DbCommand command, DBParameterList dbParam)
        {
            for (int i = 0; i < dbParam.Count; i++)
            {
                DBParameter var0007;
                DBParameter var0015;
                string var0000 = this.m_dbProvider;
                if (var0000 != null)
                {
                    if (!(var0000 == "System.Data.OracleClient"))
                    {
                        if (var0000 == "System.Data.SqlClient")
                        {
                            goto Label_00D0;
                        }
                        if (var0000 == "MySql.Data.MySqlClient")
                        {
                            goto Label_0192;
                        }
                    }
                    else
                    {
                        DBParameter var0001 = dbParam[i];
                        DBParameter var0002 = dbParam[i];
                        DBParameter var0003 = dbParam[i];
                        DBParameter var0004 = dbParam[i];
                        DBParameter var0005 = dbParam[i];
                        DBParameter var0006 = dbParam[i];
                        (this.m_databaseObject as OracleDatabase).AddParameter(command as OracleCommand, var0001.Name, (OracleType)var0002.Type, var0003.Size, var0004.Direction, var0005.IsNullable, 0, 0, string.Empty, DataRowVersion.Default, var0006.Value);
                    }
                }
                continue;
            Label_00D0:
                var0007 = dbParam[i];
                switch (var0007.Direction)
                {
                    case ParameterDirection.Input:
                        {
                            DBParameter var0009 = dbParam[i];
                            DBParameter var0010 = dbParam[i];
                            DBParameter var0011 = dbParam[i];
                            (this.m_databaseObject as SqlDatabase).AddInParameter(command, var0009.Name, (DbType)var0010.Type, var0011.Value);
                            continue;
                        }
                    case ParameterDirection.Output:
                    case ParameterDirection.InputOutput:
                        {
                            DBParameter var0012 = dbParam[i];
                            DBParameter var0013 = dbParam[i];
                            DBParameter var0014 = dbParam[i];
                            (this.m_databaseObject as SqlDatabase).AddOutParameter(command, var0012.Name, (DbType)var0013.Type, var0014.Size);
                            continue;
                        }
                    default:
                        {
                            continue;
                        }
                }
            Label_0192:
                var0015 = dbParam[i];
                switch (var0015.Direction)
                {
                    case ParameterDirection.Input:
                        {
                            DBParameter var0017 = dbParam[i];
                            DBParameter var0018 = dbParam[i];
                            DBParameter var0019 = dbParam[i];
                            this.m_databaseObject.AddInParameter(command, var0017.Name, (DbType)var0018.Type, var0019.Value);
                            break;
                        }
                    case ParameterDirection.Output:
                    case ParameterDirection.InputOutput:
                        {
                            DBParameter var0020 = dbParam[i];
                            DBParameter var0021 = dbParam[i];
                            DBParameter var0022 = dbParam[i];
                            this.m_databaseObject.AddOutParameter(command, var0020.Name, (DbType)var0021.Type, var0022.Size);
                            break;
                        }
                }
            }
        }

        public void BeginTransaction()
        {
            try
            {
                if (this.m_databaseConnection == null)
                {
                    this.m_databaseConnection = this.m_databaseObject.CreateConnection();
                }
                this.m_databaseConnection.Open();
                this.m_databaseTransaction.Push(this.m_databaseConnection.BeginTransaction());
            }
            catch
            {
                throw;
            }
        }

        public void CommitTransaction()
        {
            DbTransaction currentTransaction = null;
            try
            {
                if (this.m_databaseTransaction.Count == 0)
                {
                    throw new Exception("No transaction exists");
                }
                currentTransaction = this.m_databaseTransaction.Pop();
                currentTransaction.Commit();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                }
                if ((this.m_databaseTransaction.Count == 0) && (this.m_databaseConnection != null))
                {
                    this.m_databaseConnection.Close();
                }
            }
        }

        public void CommitTransactionAll()
        {
            DbTransaction currentTransaction = null;
            try
            {
                if (this.m_databaseTransaction.Count != 0)
                {
                    goto Label_002C;
                }
                throw new Exception("No transaction exists");
            Label_001A:
                currentTransaction = this.m_databaseTransaction.Pop();
                currentTransaction.Commit();
            Label_002C:
                if (this.m_databaseTransaction.Count > 0)
                {
                    goto Label_001A;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                }
                if ((this.m_databaseTransaction.Count == 0) && (this.m_databaseConnection != null))
                {
                    this.m_databaseConnection.Close();
                }
            }
        }

        protected Database Create(string databaseInstance)
        {
            this.m_dbProvider = ConfigurationManager.ConnectionStrings[databaseInstance].ProviderName;
            string encryptedConnectionString = ConfigurationManager.ConnectionStrings[databaseInstance].ConnectionString;
            dicryptedConnectionString = ConfigurationManager.ConnectionStrings[databaseInstance].ConnectionString;
            if (ConnectionEncrypted)
            {
                string Epass = encryptedConnectionString.Substring(encryptedConnectionString.IndexOf("Password=") + 9);
                string Dpass = new CryptographyManager(CryptographyProviderType.SymmetricCryptoProvider).Decrypt(Epass);
                dicryptedConnectionString = encryptedConnectionString.Replace(Epass, Dpass);
            }
            switch (this.m_dbProvider)
            {
                case "System.Data.OracleClient":
                    return new OracleDatabase(dicryptedConnectionString);

                case "System.Data.SqlClient":
                    return new SqlDatabase(dicryptedConnectionString);

                case "MySql.Data.MySqlClient":
                    return new MySqlDatabase(dicryptedConnectionString);
            }
            return DatabaseFactory.CreateDatabase(databaseInstance);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(true);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if ((this.m_databaseTransaction != null) && (this.m_databaseTransaction.Count > 0))
                {
                    this.RollbackTransactionAll();
                }
                if (this.m_databaseConnection != null)
                {
                    this.m_databaseConnection.Dispose();
                }
                if (this.m_databaseObject != null)
                {
                    this.m_databaseObject = null;
                }
                if (this.m_dbProvider != null)
                {
                    this.m_dbProvider = null;
                }
            }
        }

        public IDataReader ExecuteDataReader(string spName, List<DBParameter> dbParam)
        {
            DBParameterList dbParamList = null;
            IDataReader var0000;
            try
            {
                dbParamList = new DBParameterList(dbParam);
                var0000 = this.ExecuteDataReader(spName, dbParamList);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                for (int i = 0; i < dbParam.Count; i++)
                {
                    DBParameter var0001 = dbParam[i];
                    if (var0001.Direction != ParameterDirection.Output)
                    {
                        DBParameter var0002 = dbParam[i];
                        if (var0002.Direction != ParameterDirection.InputOutput)
                        {
                            continue;
                        }
                    }
                    DBParameter var0003 = dbParam[i];
                    DBParameter var0004 = dbParamList[i];
                    DBParameter var0005 = dbParam[i];
                    DBParameter var0006 = dbParam[i];
                    DBParameter var0007 = dbParam[i];
                    DBParameter var0008 = dbParam[i];
                    dbParam[i] = new DBParameter(var0003.Name, var0004.Value, var0005.Type, var0006.Direction, var0007.Size, var0008.IsNullable);
                }
                if (dbParamList != null)
                {
                    dbParamList = null;
                }
            }
            return var0000;
        }

        public IDataReader ExecuteDataReader(string spName, params DBParameter[] dbParam)
        {
            DBParameterList dbParamList = null;
            IDataReader avar0000;
            try
            {
                dbParamList = new DBParameterList(dbParam);
                avar0000 = this.ExecuteDataReader(spName, dbParamList);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (dbParamList != null)
                {
                    dbParamList = null;
                }
            }
            return avar0000;
        }

        public IDataReader ExecuteDataReader(string spName, DBParameterList dbParam)
        {
            IDataReader reader = null;
            try
            {
                using (DbCommand command = this.m_databaseObject.GetStoredProcCommand(spName))
                {
                    this.AddParameter(command, dbParam);
                    if (this.m_databaseTransaction.Count > 0)
                    {
                        reader = this.m_databaseObject.ExecuteReader(command, this.m_databaseTransaction.Peek());
                    }
                    else
                    {
                        reader = this.m_databaseObject.ExecuteReader(command);
                    }
                    for (int i = 0; i < dbParam.Count; i++)
                    {
                        DBParameter var0000 = dbParam[i];
                        if (var0000.Direction != ParameterDirection.Output)
                        {
                            DBParameter var0001 = dbParam[i];
                            if (var0001.Direction != ParameterDirection.InputOutput)
                            {
                                continue;
                            }
                        }
                        DBParameter var0002 = dbParam[i];
                        DBParameter var0003 = dbParam[i];
                        DBParameter var0004 = dbParam[i];
                        DBParameter var0005 = dbParam[i];
                        DBParameter var0006 = dbParam[i];
                        dbParam[i] = new DBParameter(var0002.Name, command.Parameters[i].Value, var0003.Type, var0004.Direction, var0005.Size, var0006.IsNullable);
                    }
                }
            }
            catch
            {
                throw;
            }
            return reader;
        }

        public DataSet ExecuteDataSet(string spName, List<DBParameter> dbParam)
        {
            DBParameterList dbParamList = null;
            DataSet avar0000;
            try
            {
                dbParamList = new DBParameterList(dbParam);
                avar0000 = this.ExecuteDataSet(spName, dbParamList);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                for (int i = 0; i < dbParam.Count; i++)
                {
                    DBParameter var0001 = dbParam[i];
                    if (var0001.Direction != ParameterDirection.Output)
                    {
                        DBParameter var0002 = dbParam[i];
                        if (var0002.Direction != ParameterDirection.InputOutput)
                        {
                            continue;
                        }
                    }
                    DBParameter var0003 = dbParam[i];
                    DBParameter var0004 = dbParamList[i];
                    DBParameter var0005 = dbParam[i];
                    DBParameter var0006 = dbParam[i];
                    DBParameter var0007 = dbParam[i];
                    DBParameter var0008 = dbParam[i];
                    dbParam[i] = new DBParameter(var0003.Name, var0004.Value, var0005.Type, var0006.Direction, var0007.Size, var0008.IsNullable);
                }
                dbParamList = null;
            }
            return avar0000;
        }

        public DataSet ExecuteDataSet(string spName, params DBParameter[] dbParam)
        {
            DBParameterList dbParamList = null;
            DataSet avar0000;
            try
            {
                dbParamList = new DBParameterList(dbParam);
                avar0000 = this.ExecuteDataSet(spName, dbParamList);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbParamList = null;
            }
            return avar0000;
        }

        public DataSet ExecuteDataSet(string spName, DBParameterList dbParam)
        {
            DataSet dataSet = null;
            try
            {
                using (DbCommand command = this.m_databaseObject.GetStoredProcCommand(spName))
                {
                    command.CommandTimeout = 0;
                    this.AddParameter(command, dbParam);
                    if (this.m_databaseTransaction.Count > 0)
                    {
                        dataSet = this.m_databaseObject.ExecuteDataSet(command, this.m_databaseTransaction.Peek());
                    }
                    else
                    {
                        dataSet = this.m_databaseObject.ExecuteDataSet(command);
                    }
                    for (int i = 0; i < dbParam.Count; i++)
                    {
                        DBParameter var0000 = dbParam[i];
                        if (var0000.Direction != ParameterDirection.Output)
                        {
                            DBParameter var0001 = dbParam[i];
                            if (var0001.Direction != ParameterDirection.InputOutput)
                            {
                                continue;
                            }
                        }
                        DBParameter var0002 = dbParam[i];
                        DBParameter var0003 = dbParam[i];
                        DBParameter var0004 = dbParam[i];
                        DBParameter var0005 = dbParam[i];
                        DBParameter var0006 = dbParam[i];
                        dbParam[i] = new DBParameter(var0002.Name, command.Parameters[i].Value, var0003.Type, var0004.Direction, var0005.Size, var0006.IsNullable);
                    }
                }
            }
            catch
            {
                throw;
            }
            return dataSet;
        }

        public DataTable ExecuteDataTable(string spName, List<DBParameter> dbParam)
        {
            DBParameterList dbParamList = null;
            DataTable avar0000;
            try
            {
                dbParamList = new DBParameterList(dbParam);
                avar0000 = this.ExecuteDataTable(spName, dbParamList);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                for (int i = 0; i < dbParam.Count; i++)
                {
                    DBParameter var0001 = dbParam[i];
                    if (var0001.Direction != ParameterDirection.Output)
                    {
                        DBParameter var0002 = dbParam[i];
                        if (var0002.Direction != ParameterDirection.InputOutput)
                        {
                            continue;
                        }
                    }
                    DBParameter var0003 = dbParam[i];
                    DBParameter var0004 = dbParamList[i];
                    DBParameter var0005 = dbParam[i];
                    DBParameter var0006 = dbParam[i];
                    DBParameter var0007 = dbParam[i];
                    DBParameter var0008 = dbParam[i];
                    dbParam[i] = new DBParameter(var0003.Name, var0004.Value, var0005.Type, var0006.Direction, var0007.Size, var0008.IsNullable);
                }
                dbParamList = null;
            }
            return avar0000;
        }

        public DataTable ExecuteDataTable(string spName, DBParameterList dbParam)
        {
            DataSet dsResult = null;
            DataTable dtReturn = null;
            try
            {
                dsResult = this.ExecuteDataSet(spName, dbParam);
                if ((dsResult != null) && (dsResult.Tables.Count > 0))
                {
                    dtReturn = dsResult.Tables[0];
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (dsResult != null)
                {
                    dsResult.Dispose();
                }
            }
            return dtReturn;
        }

        public DataTable ExecuteDataTable(string spName, params DBParameter[] dbParam)
        {
            DBParameterList dbParamList = null;
            DataTable avar0000;
            try
            {
                dbParamList = new DBParameterList(dbParam);
                avar0000 = this.ExecuteDataTable(spName, dbParamList);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbParamList = null;
            }
            return avar0000;
        }

        public int ExecuteNonQuery(string spName, List<DBParameter> dbParam)
        {
            DBParameterList dbParamList = null;
            int avar0000;
            try
            {
                dbParamList = new DBParameterList(dbParam);
                avar0000 = this.ExecuteNonQuery(spName, dbParamList);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                for (int i = 0; i < dbParam.Count; i++)
                {
                    DBParameter var0001 = dbParam[i];
                    if (var0001.Direction != ParameterDirection.Output)
                    {
                        DBParameter var0002 = dbParam[i];
                        if (var0002.Direction != ParameterDirection.InputOutput)
                        {
                            continue;
                        }
                    }
                    DBParameter var0003 = dbParam[i];
                    DBParameter var0004 = dbParamList[i];
                    DBParameter var0005 = dbParam[i];
                    DBParameter var0006 = dbParam[i];
                    DBParameter var0007 = dbParam[i];
                    DBParameter var0008 = dbParam[i];
                    dbParam[i] = new DBParameter(var0003.Name, var0004.Value, var0005.Type, var0006.Direction, var0007.Size, var0008.IsNullable);
                }
                if (dbParamList != null)
                {
                    dbParamList = null;
                }
            }
            return avar0000;
        }

        public int ExecuteNonQuery(string spName, DBParameterList dbParam)
        {
            int iReturn;
            try
            {
                using (DbCommand command = this.m_databaseObject.GetStoredProcCommand(spName))
                {
                    this.AddParameter(command, dbParam);
                    if (this.m_databaseTransaction.Count > 0)
                    {
                        iReturn = this.m_databaseObject.ExecuteNonQuery(command, this.m_databaseTransaction.Peek());
                    }
                    else
                    {
                        iReturn = this.m_databaseObject.ExecuteNonQuery(command);
                    }
                    for (int i = 0; i < dbParam.Count; i++)
                    {
                        DBParameter var0000 = dbParam[i];
                        if (var0000.Direction != ParameterDirection.InputOutput)
                        {
                            DBParameter var0001 = dbParam[i];
                            if (var0001.Direction != ParameterDirection.Output)
                            {
                                continue;
                            }
                        }
                        DBParameter var0002 = dbParam[i];
                        DBParameter var0003 = dbParam[i];
                        DBParameter var0004 = dbParam[i];
                        DBParameter var0005 = dbParam[i];
                        DBParameter var0006 = dbParam[i];
                        dbParam[i] = new DBParameter(var0002.Name, command.Parameters[i].Value, var0003.Type, var0004.Direction, var0005.Size, var0006.IsNullable);
                    }
                }
            }
            catch
            {
                throw;
            }
            return iReturn;
        }

        public int ExecuteNonQuery(string spName, params DBParameter[] dbParam)
        {
            DBParameterList dbParamList = null;
            int avar0000;
            try
            {
                dbParamList = new DBParameterList(dbParam);
                avar0000 = this.ExecuteNonQuery(spName, dbParamList);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (dbParamList != null)
                {
                    dbParamList = null;
                }
            }
            return avar0000;
        }

        public object ExecuteScalar(string spName, List<DBParameter> dbParam)
        {
            DBParameterList dbParamList = null;
            object avar0000;
            try
            {
                dbParamList = new DBParameterList(dbParam);
                avar0000 = this.ExecuteScalar(spName, dbParamList);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                for (int i = 0; i < dbParam.Count; i++)
                {
                    DBParameter var0001 = dbParam[i];
                    if (var0001.Direction != ParameterDirection.Output)
                    {
                        DBParameter var0002 = dbParam[i];
                        if (var0002.Direction != ParameterDirection.InputOutput)
                        {
                            continue;
                        }
                    }
                    DBParameter var0003 = dbParam[i];
                    DBParameter var0004 = dbParamList[i];
                    DBParameter var0005 = dbParam[i];
                    DBParameter var0006 = dbParam[i];
                    DBParameter var0007 = dbParam[i];
                    DBParameter var0008 = dbParam[i];
                    dbParam[i] = new DBParameter(var0003.Name, var0004.Value, var0005.Type, var0006.Direction, var0007.Size, var0008.IsNullable);
                }
                if (dbParamList != null)
                {
                    dbParamList = null;
                }
            }
            return avar0000;
        }

        public object ExecuteScalar(string spName, params DBParameter[] dbParam)
        {
            DBParameterList dbParamList = null;
            object avar0000;
            try
            {
                dbParamList = new DBParameterList(dbParam);
                avar0000 = this.ExecuteScalar(spName, dbParamList);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (dbParamList != null)
                {
                    dbParamList = null;
                }
            }
            return avar0000;
        }

        public object ExecuteScalar(string spName, DBParameterList dbParam)
        {
            object oReturn = null;
            try
            {
                using (DbCommand command = this.m_databaseObject.GetStoredProcCommand(spName))
                {
                    this.AddParameter(command, dbParam);
                    if (this.m_databaseTransaction.Count > 0)
                    {
                        oReturn = this.m_databaseObject.ExecuteScalar(command, this.m_databaseTransaction.Peek());
                    }
                    else
                    {
                        oReturn = this.m_databaseObject.ExecuteScalar(command);
                    }
                    for (int i = 0; i < dbParam.Count; i++)
                    {
                        DBParameter var0000 = dbParam[i];
                        if (var0000.Direction != ParameterDirection.Output)
                        {
                            DBParameter var0001 = dbParam[i];
                            if (var0001.Direction != ParameterDirection.InputOutput)
                            {
                                continue;
                            }
                        }
                        DBParameter var0002 = dbParam[i];
                        DBParameter var0003 = dbParam[i];
                        DBParameter var0004 = dbParam[i];
                        DBParameter var0005 = dbParam[i];
                        DBParameter var0006 = dbParam[i];
                        dbParam[i] = new DBParameter(var0002.Name, command.Parameters[i].Value, var0003.Type, var0004.Direction, var0005.Size, var0006.IsNullable);
                    }
                }
            }
            catch
            {
                throw;
            }
            return oReturn;
        }

        public int ExecuteSqlQuery(string sqlQuery)
        {
            int iReturn;
            try
            {
                using (DbCommand command = this.m_databaseObject.GetSqlStringCommand(sqlQuery))
                {
                    if (this.m_databaseTransaction.Count > 0)
                    {
                        return this.m_databaseObject.ExecuteNonQuery(command, this.m_databaseTransaction.Peek());
                    }
                    return this.m_databaseObject.ExecuteNonQuery(command);
                }
            }
            catch
            {
                throw;
            }
            return iReturn;
        }

        public string ExecuteXmlDataSet(string spName, List<DBParameter> dbParam)
        {
            DBParameterList dbParamList = null;
            string avar0000;
            try
            {
                dbParamList = new DBParameterList(dbParam);
                avar0000 = this.ExecuteXmlDataSet(spName, dbParamList);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                for (int i = 0; i < dbParam.Count; i++)
                {
                    DBParameter var0001 = dbParam[i];
                    if (var0001.Direction != ParameterDirection.Output)
                    {
                        DBParameter var0002 = dbParam[i];
                        if (var0002.Direction != ParameterDirection.InputOutput)
                        {
                            continue;
                        }
                    }
                    DBParameter var0003 = dbParam[i];
                    DBParameter var0004 = dbParamList[i];
                    DBParameter var0005 = dbParam[i];
                    DBParameter var0006 = dbParam[i];
                    DBParameter var0007 = dbParam[i];
                    DBParameter var0008 = dbParam[i];
                    dbParam[i] = new DBParameter(var0003.Name, var0004.Value, var0005.Type, var0006.Direction, var0007.Size, var0008.IsNullable);
                }
                dbParamList = null;
            }
            return avar0000;
        }

        public string ExecuteXmlDataSet(string spName, DBParameterList dbParam)
        {
            DataSet dsResult = null;
            string strReturn = string.Empty;
            try
            {
                dsResult = this.ExecuteDataSet(spName, dbParam);
                if (dsResult != null)
                {
                    strReturn = dsResult.GetXml();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (dsResult != null)
                {
                    dsResult.Dispose();
                }
            }
            return strReturn;
        }

        public string ExecuteXmlDataSet(string spName, params DBParameter[] dbParam)
        {
            DBParameterList dbParamList = null;
            string avar0000;
            try
            {
                dbParamList = new DBParameterList(dbParam);
                avar0000 = this.ExecuteXmlDataSet(spName, dbParamList);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbParamList = null;
            }
            return avar0000;
        }

        public void RollbackTransaction()
        {
            DbTransaction currentTransaction = null;
            try
            {
                if (this.m_databaseTransaction.Count == 0)
                {
                    throw new Exception("No transaction exists");
                }
                currentTransaction = this.m_databaseTransaction.Pop();
                currentTransaction.Rollback();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                }
                if ((this.m_databaseTransaction.Count == 0) && (this.m_databaseConnection != null))
                {
                    this.m_databaseConnection.Close();
                }
            }
        }

        public void RollbackTransactionAll()
        {
            DbTransaction currentTransaction = null;
            try
            {
                if (this.m_databaseTransaction.Count != 0)
                {
                    goto Label_002C;
                }
                throw new Exception("No transaction exists");
            Label_001A:
                currentTransaction = this.m_databaseTransaction.Pop();
                currentTransaction.Rollback();
            Label_002C:
                if (this.m_databaseTransaction.Count > 0)
                {
                    goto Label_001A;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                }
                if ((this.m_databaseTransaction.Count == 0) && (this.m_databaseConnection != null))
                {
                    this.m_databaseConnection.Close();
                }
            }
        }
    }
}


