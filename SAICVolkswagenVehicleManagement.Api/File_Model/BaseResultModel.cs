namespace ResultTest
{
    /// <summary>
    /// 返回
    /// </summary>
    public class BaseResultModel
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="result"></param>
        /// <param name="retureStates"></param>
        public BaseResultModel(int? code = null, string message = null,
            object result = null, RetureStates retureStates = RetureStates.Success)
        {
            this.Code = code;
            this.Result = result;
            this.Message = message;
            this.RetureStates = retureStates;
        }
        /// <summary>
        /// 返回状态码
        /// </summary>
        public int? Code { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回结果
        /// </summary>
        public object Result { get; set; }
        /// <summary>
        /// 结果状态
        /// </summary>
        public RetureStates RetureStates { get; set; }
    }
    /// <summary>
    /// 返回的状态
    /// </summary>
    public enum RetureStates
    {
        Success = 1,
        Fail = 0,
        ConfirmIsContinue = 2,
        Error = 3
    }
}
