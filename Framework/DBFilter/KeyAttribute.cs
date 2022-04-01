using System;
namespace Framework.Mapping
{
    [AttributeUsage(AttributeTargets.Property)]
    public class KeyAttribute : Attribute
    {
        public KeyAttribute()
        {
        }
    }
}

