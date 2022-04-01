using System;
using System.Reflection;
using System.Text;
using Framework.Mapping;

namespace Framework.DBFilter
{
    public static class DBFilterExtend
    {
        public static IEnumerable<PropertyInfo> GetPropertiesWithoutKey(this Type type)
        {
            return type.GetProperties().Where(x => !x.IsDefined(typeof(KeyAttribute), true));
        }
    }
}

