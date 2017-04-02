using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using RfidMobile.Service.Util;
using System.IO;
using Newtonsoft.Json;

namespace RfidMobile.Service.StockIn
{
    /// <summary>
    /// ������
    /// </summary>
    /// <remarks>
    /// 2017-3-24 wzl
    /// 1.0.0 
    /// </remarks>
    public class StockInService
    {
        public static readonly string TAG = "stockIn";

        private static string message;
        public static string Message {
            get { return message; }
            set { message = value;
            MessageReceivedEventArgs e = new MessageReceivedEventArgs() { Message = message };
            OnMessageReceived(e);
            }
        }

        public static event EventHandler<MessageReceivedEventArgs> MessageReceived; 

        /// <summary>
        /// ������ⵥ�Ŷ������Ϣ
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public static StockIn GetBySerialNumber(string serialNumber) {
            StockIn stockIn = new StockIn();

            try
            {
                string url = "http://192.168.0.222:50000/Api/StockIn";

                string text = HttpService.GetJsonByUrl(url);

                stockIn = JsonConvert.DeserializeObject<StockIn>(text);

            }
            catch (Exception ex) {
                Message = "�����Ϣ��ȡʧ��";
                LogService.Error(TAG, ex.Message); 
            }

            return stockIn;
        }

        protected static void OnMessageReceived(MessageReceivedEventArgs e) {
            if (MessageReceived != null) {
                MessageReceived(TAG, e);
            }
        }
    }
}
