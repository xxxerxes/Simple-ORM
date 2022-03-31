using System;
using Model;
using Framework;
using MySql.Data.MySqlClient;
using Framework.Mapping;

namespace DAL
{
    public class SqlHelper
    {

        private static string ConnectionStringCustomers = ConfigurationManager.SqlConnectionStringCustom;

        #region 原始版本 每个表写一个查询方法
        //public Company FindCompany(int id)
        //{
        //    string sql = $@"SELECT Id,Name,CreateTime,LastModifierId,LastModifyTime From Company
        //                WHERE Id = {id}";

        //    using (MySqlConnection conn = new MySqlConnection(ConnectionStringCustomers))
        //    {
        //        MySqlCommand command = new MySqlCommand(sql, conn);
        //        conn.Open();

        //        var reader = command.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            Company company = new Company()
        //            {
        //                Id = (int)reader["Id"],
        //                Name = reader["Name"].ToString()
        //            };

        //            return company;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}
        #endregion

        #region 添加泛型 完成类型通用
        public T Find<T>(int id) where T : BaseModel //泛型约束保证类型正确
        {
            Type type = typeof(T);

            // 通过拓展方法获取特性里的名字
            string colunmStrings = string.Join(",", type.GetProperties().Select(x => $"{x.GetMappingName()}"));

            string sql = $@"SELECT {colunmStrings} From {type.GetMappingName()} WHERE Id = {id}";

            using (MySqlConnection conn = new MySqlConnection(ConnectionStringCustomers))
            {
                MySqlCommand command = new MySqlCommand(sql, conn);
                conn.Open();

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    // 创建实体
                    T t = Activator.CreateInstance<T>();

                    foreach (var prop in type.GetProperties())
                    {
                        string propName = prop.GetMappingName();

                        // 空类型转换

                        //if (reader[prop.Name] == DBNull.Value)
                        //{
                        //    prop.SetValue(t, null);
                        //}
                        //else
                        //{
                        //    prop.SetValue(t, reader[prop.Name]);
                        //}
                        prop.SetValue(t, reader[propName] is DBNull ? null : reader[propName]);
                    }

                    return t;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion
    }
}
