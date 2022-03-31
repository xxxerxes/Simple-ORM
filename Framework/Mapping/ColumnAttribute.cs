using System;
namespace Framework.Mapping
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : BaseMappingAttribute
    {
        //private string columnName = null;

        public ColumnAttribute(string columnName) : base(columnName)
        {
            //this.columnName = columnName;
        }

        //public string GetMappingName()
        //{
        //    return this.columnName;
        //}
    }
}

