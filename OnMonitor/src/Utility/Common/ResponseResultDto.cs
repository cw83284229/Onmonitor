using System;
using Utility.Common;

namespace OnMonitor
{
    public partial class ResponseResultDto
    {

    //  public ResponseResultDto<T>(string Code, long totalCount, IReadOnlyList<T> items, string Messages);
        public virtual HttpStatusCode Code { set; get; }

        public virtual string Messages { set; get; }
        /// <summary>
        /// 表示相应成功
        /// </summary>
        public bool Success => Code == HttpStatusCode.Succeed;

        public long TimeStamp { get; } = DateTime.Now.ToUniversalTime().Ticks;

        public virtual object ResultData { set; get; }
        /// <summary>
        /// 表示响应成功
        /// </summary>
        /// <param name="message"></param>
        public void IsSuccess( object resultData=null, string message = "")
        {

            Messages = message;
            ResultData = resultData;
             Code = HttpStatusCode.Succeed;

        }
        /// <summary>
        /// 表示响应失败
        /// </summary>
        /// <param name="message"></param>
        public void IsFailed(string message="")
        {

            Messages = message;
           
            Code = HttpStatusCode.Failed;

        }
        /// <summary>
        /// 表示响应失败输出异常
        /// </summary>
        /// <param name="exception"></param>
        public void IsFailed(Exception exception)
        {

            Messages =exception.InnerException?.StackTrace;

            Code = HttpStatusCode.Failed;

        }





    }
}
