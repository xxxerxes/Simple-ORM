using System;
namespace Framework.Validate
{
	public class IntValueAttribute : BaseValidateAttribute
	{
		private int[] values = null;

		public IntValueAttribute(params int[] values)
		{
			this.values = values;
		}

        public override bool Validate(object oValue)
        {
			string value = oValue.ToString();
			return oValue != null
				&& !string.IsNullOrWhiteSpace(value)
				&& int.TryParse(value, out int iValue)
				&& this.values != null
				&& this.values.Contains(iValue);
        }
    }
}

