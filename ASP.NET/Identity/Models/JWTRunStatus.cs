namespace Identity.Models
{
   public class RunStatus
    {
        /// <summary>
        /// Token是否有成功產生狀態
        /// </summary>
        /// <value>布林值</value>
        public bool isSuccess { set; get; }

        /// <summary>
        /// Token
        /// </summary>
        /// <value>字串</value>
        public string jwt { set; get; }

        /// <summary>
        /// 處理訊息
        /// </summary>
        /// <value>字串</value>
        public string msg { set; get; }
    }
}