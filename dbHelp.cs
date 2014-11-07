using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Threading;
using System.Data.SqlClient;
/// <summary>
    /// 公共类提供访问数据的公共函数
    /// </summary>
    /// 
namespace WcfSSO
{
    public static class dbHelp
    {

        public static string strConnSSO = ConfigurationManager.ConnectionStrings["StrConnSSO"].ToString();
        //-----------执行Sql公共函数

        #region 执行增加、删除、修改的函数(不带参数)
        /// <summary>
        /// 执行增加、删除、修改的函数
        /// </summary>
        /// <param name="SQLString">Sql语句</param>
        /// <param name="connectionString">链接数据库的字符串</param>
        /// <returns>受影响的行数</returns>
        public static int GetExecuteSql(string SQLString)
        {
            int rows = 0;
            SqlConnection connection = null;
            SqlCommand cmd = null;
            try
            {
                connection = new SqlConnection(strConnSSO);
                cmd = new SqlCommand(SQLString, connection);
                connection.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ex.Source += "ExecuteSql.不带参数";
                rows = 0;
            }
            finally
            {
                cmd.Dispose();
                connection.Dispose();
                connection.Close();
            }
            return rows;
        }
        #endregion

        #region 执行增加、删除、修改的函数（带参数）
        /// <summary>
        /// 执行增加、删除、修改的函数（带参数）
        /// </summary>
        /// <param name="SQLString">Sql语句</param>
        /// <param name="connectionString">链接数据库的字符串</param>
        /// <param name="cmdParms">参数</param>
        /// <returns>返回受影响的行数</returns>
        public static int GetExecuteSql(string SQLString, params SqlParameter[] cmdParms)
        {
            int rows = 0;
            SqlConnection connection = null;
            SqlCommand cmd = null;
            try
            {
                connection = new SqlConnection(strConnSSO);
                cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                rows = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                ex.Source += "ExecuteSql.带参数";
            }
            finally
            {
                cmd.Dispose();
                connection.Dispose();
                connection.Close();
            }
            return rows;
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = cmdText;
                if (trans != null)
                    cmd.Transaction = trans;
                cmd.CommandType = CommandType.Text;//cmdType; 
                if (cmdParms != null)
                {
                    //foreach (OracleParameter parm in cmdParms)
                    //    if (parm != null)
                    //        cmd.Parameters.Add(parm);
                    //    else
                    //        cmd.Parameters.Add(DBNull.Value);
                    cmd.Parameters.AddRange(cmdParms);
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region 判断数据库是否存在该记录
        /// <summary>
        /// 判断数据库是否存在该记录
        /// </summary>
        /// <param name="SQLString">执行操作的Sql语句</param>
        /// <param name="connectionString">链接数据库的字符串</param>

        public static int GetSqlScalar(string SQLString)
        {
            SqlConnection connection = null;
            SqlCommand cmd = null;
            int rows = 0;
            try
            {
                connection = new SqlConnection(strConnSSO);
                cmd = new SqlCommand(SQLString, connection);
                connection.Open();
                rows = int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                ex.Source += "GetScalar判断记录是否存在";
                rows = 0;
            }
            finally
            {
                cmd.Dispose();
                connection.Dispose();
                connection.Close();
            }
            return rows;
        }
        #endregion

        #region 返回数据集
        /// <summary>
        /// 返回数据集公共函数
        /// </summary>
        /// <param name="SQLString">执行的Sql语句</param>
        /// <param name="connectionString">链接数据库的字符串</param>
        /// <returns></returns>
        public static DataSet GetQuery(string SQLString)
        {
            SqlConnection connection = null;
            DataSet ds = null;
            try
            {
                connection = new SqlConnection(strConnSSO);
                connection.Open();
                SqlDataAdapter command = new SqlDataAdapter(SQLString, connection);
                ds = new DataSet();
                command.Fill(ds, "ds");
            }
            catch (Exception ex)
            {
                ex.Source += "Query.";
                ds = null;
            }
            finally
            {
                connection.Dispose();
                connection.Close();
            }
            return ds;

        }
        #endregion

        #region 返回SqlDataReader
        /// <summary>
        /// 返回SqlDataReader
        /// </summary>
        /// <param name="SQLString">执行操作的Sql语句</param>
        /// <param name="connectionString">链接数据库的字符串</param>
        public static SqlDataReader GetReturnValue(string SQLString)
        {
            string StrValue = string.Empty;
            SqlConnection connection = null;
            SqlCommand cmd = null;
            SqlDataReader SqlRead = null;
            try
            {
                connection = new SqlConnection(strConnSSO);
                cmd = new SqlCommand(SQLString, connection);
                connection.Open();
                SqlRead = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                ex.Source += "返回OracleDataReader.";
            }
            finally
            {
                // connection.Dispose();
                //connection.Close();
            }
            return SqlRead;
        }
        #endregion


        /// <summary> 
        /// 执行指定数据库连接字符串的命令,指定参数,返回结果集中的第一行第一列. 
        /// </summary> 
        /// <remarks> 
        /// 示例:  
        ///  int orderCount = (int)ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24)); 
        /// </remarks> 
        /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param> 
        /// <param name="commandText">存储过程名称或T-SQL语句</param> 
        /// <param name="commandParameters">分配给命令的SqlParamter参数数组</param> 
        /// <returns>返回结果集中的第一行第一列</returns> 
        public static object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            string connectionString = strConnSSO;
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");
            // 创建并打开数据库连接对象,操作完成释放对象. 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // 调用指定数据库连接字符串重载方法. 
                return ExecuteScalar(connection, commandType, commandText, commandParameters);
            }
        }

        /// <summary> 
        /// 执行指定数据库连接对象的命令,指定参数,返回结果集中的第一行第一列. 
        /// </summary> 
        /// <remarks> 
        /// 示例:  
        ///  int orderCount = (int)ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24)); 
        /// </remarks> 
        /// <param name="connection">一个有效的数据库连接对象</param> 
        /// <param name="commandType">命令类型 (存储过程,命令文本或其它)</param> 
        /// <param name="commandText">存储过程名称或T-SQL语句</param> 
        /// <param name="commandParameters">分配给命令的SqlParamter参数数组</param> 
        /// <returns>返回结果集中的第一行第一列</returns> 
        public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // 创建SqlCommand命令,并进行预处理 
            SqlCommand cmd = new SqlCommand();

            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            // 执行SqlCommand命令,并返回结果. 
            object retval = cmd.ExecuteScalar();

            // 清除参数,以便再次使用. 
            cmd.Parameters.Clear();

            if (mustCloseConnection)
                connection.Close();

            return retval;
        }

        /// <summary> 
        /// 预处理用户提供的命令,数据库连接/事务/命令类型/参数 
        /// </summary> 
        /// <param name="command">要处理的SqlCommand</param> 
        /// <param name="connection">数据库连接</param> 
        /// <param name="transaction">一个有效的事务或者是null值</param> 
        /// <param name="commandType">命令类型 (存储过程,命令文本, 其它.)</param> 
        /// <param name="commandText">存储过程名或都T-SQL命令文本</param> 
        /// <param name="commandParameters">和命令相关联的SqlParameter参数数组,如果没有参数为'null'</param> 
        /// <param name="mustCloseConnection"><c>true</c> 如果连接是打开的,则为true,其它情况下为false.</param> 
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it 
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // 给命令分配一个数据库连接. 
            command.Connection = connection;

            // 设置命令文本(存储过程名或SQL语句) 
            command.CommandText = commandText;

            // 分配事务 
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            // 设置命令类型. 
            command.CommandType = commandType;

            // 分配命令参数 
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }

        /// <summary> 
        /// 将SqlParameter参数数组(参数值)分配给SqlCommand命令. 
        /// 这个方法将给任何一个参数分配DBNull.Value; 
        /// 该操作将阻止默认值的使用. 
        /// </summary> 
        /// <param name="command">命令名</param> 
        /// <param name="commandParameters">SqlParameters数组</param> 
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // 检查未分配值的输出参数,将其分配以DBNull.Value. 
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }

        public static void ExecuteSql(string StoreName, params SqlParameter[] CmdParms)
        {
            SqlConnection Conn = new SqlConnection(dbHelp.strConnSSO);
            SqlCommand Command = new SqlCommand();
            if (Conn.State == ConnectionState.Closed)
                Conn.Open();
            else
                if (Conn.State == ConnectionState.Broken)
                {
                    Conn.Close();
                    Conn.Open();
                }
            Command.CommandText = StoreName;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Connection = Conn;
            Command.Parameters.AddRange(CmdParms);
            Command.ExecuteNonQuery();
        }

        public static int ExecuteProceSql(string StoreName, params SqlParameter[] CmdParms)
        {
            SqlConnection Conn = new SqlConnection(dbHelp.strConnSSO);
            SqlCommand Command = new SqlCommand();
            if (Conn.State == ConnectionState.Closed)
                Conn.Open();
            else
                if (Conn.State == ConnectionState.Broken)
                {
                    Conn.Close();
                    Conn.Open();
                }
            Command.CommandText = StoreName;
            Command.CommandType = CommandType.StoredProcedure;
            Command.Connection = Conn;
            Command.Parameters.AddRange(CmdParms);
            Command.ExecuteNonQuery();
            return int.Parse(CmdParms[0].Value.ToString());
        }
    }
}
