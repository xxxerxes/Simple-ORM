using System;
namespace Framework.Validate
{
    /// <summary>
    /// 要求不为空
    /// </summary>
    public class NoNullAttribute : BaseValidateAttribute
    {
        public override bool Validate(object oValue)
        {
            return oValue != null && !string.IsNullOrWhiteSpace(oValue.ToString());
        }
    }
}

