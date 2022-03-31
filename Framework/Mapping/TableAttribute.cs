using System;
namespace Framework.Mapping
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : BaseMappingAttribute
    {
        //private string tableName = null;

        public TableAttribute(string tableName) : base(tableName)
        {
            //this.tableName = tableName;
        }

        //public string GetMappingName()
        //{
        //    return this.tableName;
        //}
    }
}

