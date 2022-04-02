using System;
using Model;
using Framework;
using MySql.Data.MySqlClient;
using Framework.Mapping;
using Framework.DBFilter;
using Framework.Validate;

namespace DAL
{
    public class SqlHelper
    {

        private static string ConnectionStringCustomers = ConfigurationManager.SqlConnectionStringCustom;

        #region 查询
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
            //string colunmStrings = string.Join(",", type.GetProperties().Select(x => $"{x.GetMappingName()}"));
            //string sql = $@"SELECT {colunmStrings} From {type.GetMappingName()} WHERE Id = {id}";

            // 从sql缓存中获取
            string sql = SqlCacheBuilder<T>.GetSql(SqlCacheBuilderEnum.Search);

            // sql参数化
            MySqlParameter[] parameters = new MySqlParameter[] { new MySqlParameter("@Id", id) };

            using (MySqlConnection conn = new MySqlConnection(ConnectionStringCustomers))
            {
                MySqlCommand command = new MySqlCommand(sql, conn);
                conn.Open();
                command.Parameters.AddRange(parameters);
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

        #endregion

        #region 插入
        public bool Insert<T>(T t) where T : BaseModel
        {
            Type type = typeof(T);

            //string columnStrings = string.Join(",", type.GetPropertiesWithoutKey().Select(x => $"{x.GetMappingName()}"));
            //string valueStrings = string.Join(",", type.GetPropertiesWithoutKey().Select(x => $"@{x.GetMappingName()}"));
            //string sql = $"Insert into `{type.GetMappingName()}` ({columnStrings}) Values ({valueStrings})";

            string sql = SqlCacheBuilder<T>.GetSql(SqlCacheBuilderEnum.Insert);
            // sql参数化，防止sql注入
            var parameters = type.GetProperties().Select(x => new MySqlParameter($"@{x.GetMappingName()}", x.GetValue(t) ?? DBNull.Value)).ToArray();

            using (MySqlConnection conn = new MySqlConnection(ConnectionStringCustomers))
            {
                MySqlCommand command = new MySqlCommand(sql, conn);
                command.Parameters.AddRange(parameters);
                conn.Open();
                int result = command.ExecuteNonQuery();
                return result == 1;
            }
        }

        #endregion

        #region 修改
        public bool Update<T>(T t) where T : BaseModel
        {
            if (!t.NoNullValidate())
            {
                throw new Exception("数据校验失败");
            }

            Type type = typeof(T);

            //string columnAndValueStrings = string.Join(",", type.GetPropertiesWithoutKey()
            //    .Select(x => $"{x.GetMappingName()}=@{x.GetMappingName()}"));
            //string sql = $"Update {type.GetMappingName()} Set {columnAndValueStrings} Where Id = @Id;";

            string sql = SqlCacheBuilder<T>.GetSql(SqlCacheBuilderEnum.Update);

            MySqlParameter[] parameter = type.GetPropertiesWithoutKey()
                .Select(x => new MySqlParameter($"@{x.GetMappingName()}", x.GetValue(t) ?? DBNull.Value))
                .Append(new MySqlParameter("@Id", t.Id)).ToArray();

            using (MySqlConnection conn = new MySqlConnection(ConnectionStringCustomers))
            {
                MySqlCommand command = new MySqlCommand(sql, conn);
                conn.Open();
                command.Parameters.AddRange(parameter);
                return 1 == command.ExecuteNonQuery();
            }
        }
        #endregion
    }
}
