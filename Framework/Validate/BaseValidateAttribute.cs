using System;
namespace Framework.Validate
{
    /// <summary>
    /// 校验基类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public abstract class BaseValidateAttribute : Attribute
    {
        public abstract bool Validate(object oValue);
    }
}

