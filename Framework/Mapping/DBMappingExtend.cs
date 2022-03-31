using System;
using System.Reflection;

namespace Framework.Mapping
{
    public static class DBMappingExtend
    {
        #region 拓展方法，代码冗余
        //public static string GetMappingTableName(this Type type)
        //{
        //    if (type.IsDefined(typeof(TableAttribute), true))
        //    {
        //        var attribute = type.GetCustomAttribute<TableAttribute>();
        //        return attribute.GetMappingName();
        //    }
        //    else
        //    {
        //        return type.Name;
        //    }
        //}

        //public static string GetMappingPropertyName(this PropertyInfo prop)
        //{
        //    if (prop.IsDefined(typeof(BaseMappingAttribute), true))
        //    {
        //        var attribute = prop.GetCustomAttribute<BaseMappingAttribute>();
        //        return attribute.GetMappingName();
        //    }
        //    else
        //    {
        //        return prop.Name;
        //    }
        //}
        #endregion

        #region 代码抽象，消除冗余
        public static string GetMappingName<T>(this T t) where T : MemberInfo
        {
            if (t.IsDefined(typeof(TableAttribute), true))
            {
                var attribute = t.GetCustomAttribute<TableAttribute>();
                return attribute.GetMappingName();
            }
            else
            {
                return t.Name;
            }
        }
        #endregion
    }
}

