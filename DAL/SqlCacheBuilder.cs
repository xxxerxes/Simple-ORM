using System;
using Framework.DBFilter;
using Framework.Mapping;
using Model;

namespace DAL
{
    /// <summary>
    /// 生成Sql时缓存以备复用
    /// </summary>
    public class SqlCacheBuilder<T> where T : BaseModel
    {

        private static string searchSql = null;
        private static string insertSql = null;
        private static string UpdateSql = null;
        private static string DeleteSql = null;


        static SqlCacheBuilder()
        {
            // Search
            {
                Type type = typeof(T);

                // 通过拓展方法获取特性里的名字
                string colunmStrings = string.Join(",", type.GetProperties().Select(x => $"`{x.GetMappingName()}`"));

                // searchSql = $@"SELECT {colunmStrings} From {type.GetMappingName()} WHERE Id = @Id";
                searchSql = $@"SELECT {colunmStrings} From {type.GetMappingName()} ";
            }

            // Insert
            {
                Type type = typeof(T);
                string columnStrings = string.Join(",", type.GetPropertiesWithoutKey().Select(x => $"{x.GetMappingName()}"));
                // sql参数化，防止sql注入
                string valueStrings = string.Join(",", type.GetPropertiesWithoutKey().Select(x => $"@{x.GetMappingName()}"));

                insertSql = $"Insert into `{type.GetMappingName()}` ({columnStrings}) Values ({valueStrings})";
            }

            // Update
            {
                Type type = typeof(T);
                string columnAndValueStrings = string.Join(",", type.GetPropertiesWithoutKey()
                                                .Select(x => $"{x.GetMappingName()}=@{x.GetMappingName()}"));
                UpdateSql = $"Update {type.GetMappingName()} Set {columnAndValueStrings} Where Id = @Id;";
            }

            // Delete
            {
                Type type = typeof(T);
                DeleteSql = $"Delete From {type.GetMappingName()} where Id = @Id";
            }
        }

        internal static string GetSql(SqlCacheBuilderEnum type)
        {
            switch (type)
            {
                case (SqlCacheBuilderEnum.Search):
                    return searchSql;
                case (SqlCacheBuilderEnum.Insert):
                    return insertSql;
                case (SqlCacheBuilderEnum.Update):
                    return UpdateSql;
                case (SqlCacheBuilderEnum.Delete):
                    return DeleteSql;
                default:
                    throw new Exception("Unkown SqlCacheType");
            }
            return insertSql;
        }
    }

    internal enum SqlCacheBuilderEnum
    {
        Search,
        Insert,
        Update,
        Delete
    }
}

