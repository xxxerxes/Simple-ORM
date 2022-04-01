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


        static SqlCacheBuilder()
        {
            {
                Type type = typeof(T);

                // 通过拓展方法获取特性里的名字
                string colunmStrings = string.Join(",", type.GetProperties().Select(x => $"{x.GetMappingName()}"));

                searchSql = $@"SELECT {colunmStrings} From {type.GetMappingName()} WHERE Id = @Id";
            }

            {
                #region Insert
                Type type = typeof(T);
                string columnStrings = string.Join(",", type.GetPropertiesWithoutKey().Select(x => $"{x.GetMappingName()}"));
                // sql参数化，防止sql注入
                string valueStrings = string.Join(",", type.GetPropertiesWithoutKey().Select(x => $"@{x.GetMappingName()}"));

                insertSql = $"Insert into `{type.GetMappingName()}` ({columnStrings}) Values ({valueStrings})";
                #endregion
            }
        }

        public static string GetSql(SqlCacheBuilderEnum type)
        {
            switch (type)
            {
                case (SqlCacheBuilderEnum.Search):
                    return searchSql;
                case (SqlCacheBuilderEnum.Insert):
                    return insertSql;
                default:
                    throw new Exception("Unkown SqlCacheType");
            }
            return insertSql;
        }
    }

    public enum SqlCacheBuilderEnum
    {
        Search,
        Insert
    }
}

