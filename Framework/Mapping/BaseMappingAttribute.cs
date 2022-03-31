using System;
namespace Framework.Mapping
{
    public class BaseMappingAttribute : Attribute
    {
        private string mappingName = null;

        public BaseMappingAttribute(string mappingName)
        {
            this.mappingName = mappingName;
        }

        public string GetMappingName()
        {
            return this.mappingName;
        }
    }
}

