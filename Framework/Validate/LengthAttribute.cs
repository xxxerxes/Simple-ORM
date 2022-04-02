namespace Framework.Validate
{
    /// <summary>
    /// 长度限制
    /// </summary>
    public class LengthAttribute : BaseValidateAttribute
    {
        private int min = 0, max = 0;

        /// <summary>
        /// 左闭右开区间
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public LengthAttribute(int min,int max)
        {
            this.max = max;
            this.min = min;
        }

        public override bool Validate(object oValue)
        {
            int length = oValue.ToString().Length;
            return oValue != null
                && !string.IsNullOrWhiteSpace(oValue.ToString())
                && length >= min
                && length < max;
        }
    }
}

