using System;
using System.Reflection;

namespace Framework.Validate
{
    public static class DateValidateExtend
    {
        public static bool NoNullValidate<T>(this T t)
        {
            Type type = typeof(T);
            foreach(var prop in type.GetProperties())
            {
                // 使用父类
                if (prop.IsDefined(typeof(BaseValidateAttribute),true))
                {
                    object oValue = prop.GetValue(t);
                    var attributeList = prop.GetCustomAttributes<BaseValidateAttribute>();

                    foreach(var attribute in attributeList)
                    {
                        if (!attribute.Validate(oValue))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}

