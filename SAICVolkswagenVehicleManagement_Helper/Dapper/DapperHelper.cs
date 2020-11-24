using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Helper.Dapper
{
    public class DapperHelper
    {
        private const string connectionString = "Data Source=192.168.1.6;Initial Catalog=CSVMVehicle;User ID=sa;PassWord=123456";
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetToList<T>() where T : class,new()
        {
            using (IDbConnection conn = new SqlConnection() { ConnectionString = connectionString })
            {
                Type t = typeof(T);
                string sql = "";
                sql = $"select * from {t.Name}";
                return conn.Query<T>(sql).ToList();
            }
        }
        /// <summary>
        /// 获取单条信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public T GetModel<T>(string sql) where T : class,new()
        {
            T model = new T();
            using (IDbConnection conn = new SqlConnection() { ConnectionString = connectionString })
            {
                model = conn.QueryFirstOrDefault<T>(sql);
            }
            return model;
        }
        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {
            int res = 0;
            using (IDbConnection conn = new SqlConnection() { ConnectionString = connectionString })
            {
                res = conn.Execute(sql);
            }
            return res;
        }

        /// <summary>
        /// 单表存储过程查询
        /// </summary>
        /// <typeparam name="T">接收数据的类</typeparam>
        /// <param name="TableName">要查询的表名</param>
        /// <param name="ReFieldsStr">显示的字段</param>
        /// <param name="OrderString">排序的字段</param>
        /// <param name="WhereString">查询的条件</param>
        /// <param name="PageSize">页数</param>
        /// <param name="PageIndex">页码</param>
        /// <returns></returns>
        public ProcDataAndTotal<T> GetProcData<T>(string TableName,string ReFieldsStr,string OrderString,string WhereString,int PageSize,int PageIndex) where T : class, new()
        {
            using (IDbConnection conn = new SqlConnection() { ConnectionString = connectionString })
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@TableName", TableName);
                param.Add("@ReFieldsStr", ReFieldsStr);
                param.Add("@OrderString", OrderString);
                param.Add("@WhereString", WhereString);
                param.Add("@PageSize", PageSize);
                param.Add("@PageIndex", PageIndex);
                param.Add("@TotalRecord", 0, DbType.Int32, ParameterDirection.Output);
                //返回的类
                ProcDataAndTotal<T> dataAndTotal = new ProcDataAndTotal<T>()
                {
                    ProcData = conn.Query<T>("Proc_SingleTablePager", param, commandType: CommandType.StoredProcedure).ToList(),
                    Total = param.Get<int>("@TotalRecord"),
                };
                return dataAndTotal;
            }
        }

        /// <summary>
        /// 多表存储过程查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tblName"></param>
        /// <param name="strGetFields"></param>
        /// <param name="OrderfldName"></param>
        /// <param name="strWhere"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="OrderType"></param>
        /// <returns></returns>
        public ProcDataAndTotal<T> GetProcData<T>(string tblName,string strGetFields,string OrderfldName,string strWhere,int PageSize,int PageIndex,bool OrderType) where T : class, new()
        {
            using (IDbConnection conn = new SqlConnection() { ConnectionString = connectionString })
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@tblName", tblName);
                param.Add("@strGetFields", strGetFields);
                param.Add("@OrderfldName", OrderfldName);
                param.Add("@strWhere", strWhere);
                param.Add("@PageSize", PageSize);
                param.Add("@PageIndex", PageIndex);
                param.Add("@OrderType", OrderType);
                param.Add("@doCount", 0, DbType.Int32, ParameterDirection.Output);
                //返回的类
                ProcDataAndTotal<T> dataAndTotal = new ProcDataAndTotal<T>()
                {
                    ProcData = conn.Query<T>("Proc_MultipleTablesPager",param,commandType:CommandType.StoredProcedure).ToList(),
                     Total = param.Get<int>("@doCount"),
                };
                return dataAndTotal;
            }
        }

        /// <summary>
        /// 反射删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public int DeleteData<T>(T model) where T : class, new()
        {
            using (IDbConnection conn = new SqlConnection() { ConnectionString = connectionString })
            {
                Type t = model.GetType();
                PropertyInfo[] properties = t.GetProperties();
                string sql = $"delete from {t.Name}";
                foreach (PropertyInfo item in properties)
                {
                    if(!string.IsNullOrEmpty(item.GetValue(model).ToString()))
                    {
                        sql += $" where {item.Name} = '{item.GetValue(model)}'";
                    }
                }
                return conn.Execute(sql);
            }
        }
    }
}