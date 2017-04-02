using System;
using System.Collections.Generic;
using System.Text;
// 此处引用的是win ce版本的dll，还要普通windows版本的，那个dll用来远程连接扫描枪
using Symbol.RFID3;
using System.Threading;
using RfidMobile.Service.Util;

namespace RfidMobile.Service.Reader
{
    /// <summary>
    /// Symbol MC319Z RFID读取服务
    /// </summary>
    public class ReaderService : IRfidReaderService
    {
        private RFIDReader rfidReader;

        private string host;
        private int port;

        public static readonly string TAG = "reader";

        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                MessageReceivedEventArgs args = new MessageReceivedEventArgs();
                args.Message = message;
                onMessageReceived(args);
            }
        }
        private bool isConnect;
        public bool IsConnect
        {
            get { return isConnect; }
            set
            {
                isConnect = value;

                string info = "connect:" + isConnect;
                LogService.Info(TAG, info);

                IsConnectChangedEventArgs args = new IsConnectChangedEventArgs();
                args.IsConnect = isConnect;
                onIsConnectChanged(args);
            }
        }

        public event EventHandler<IsConnectChangedEventArgs> IsConnectChanged;
        public event EventHandler<TagDataReceivedEventArgs> TagDataReceived;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public ReaderService(string host, int port)
        {
            this.host = host;
            this.port = port;

            LogService.Info(TAG, "status:initial");
            string info = "host:" + host + ",port:" + port;
            LogService.Info(TAG, info);

            rfidReader = new RFIDReader(host, (uint)port, 0);
            isConnect = rfidReader.IsConnected;
        }

        public void Start()
        {
            try
            {
                string info = "tatus:start";
                LogService.Info(TAG, info);

                if (rfidReader == null || rfidReader.IsConnected == false)
                {
                    rfidReader.Connect();
                    IsConnect = rfidReader.IsConnected;

                    rfidReader.Events.NotifyInventoryStartEvent = true;
                    rfidReader.Events.NotifyAccessStartEvent = true;
                    rfidReader.Events.NotifyAccessStopEvent = true;
                    rfidReader.Events.NotifyInventoryStopEvent = true;
                    rfidReader.Events.NotifyAntennaEvent = true;
                    rfidReader.Events.NotifyBufferFullWarningEvent = true;
                    rfidReader.Events.NotifyBufferFullEvent = true;
                    rfidReader.Events.NotifyGPIEvent = true;
                    rfidReader.Events.NotifyReaderDisconnectEvent = true;
                    rfidReader.Events.NotifyReaderExceptionEvent = true;
                    // 不通知tag读到事件，改由扫描按钮松开时，获取读到的标签信息
                    rfidReader.Events.AttachTagDataWithReadEvent = false;

                    // 注册扫描枪状态变化事件
                    rfidReader.Events.StatusNotify += new Events.StatusNotifyHandler(Events_StatusNotify);

                    // rssi值过滤配置，此处取最大最小值，因此没有过滤作用
                    PostFilter posFilter = new PostFilter();
                    posFilter.UseRSSIRangeFilter = true;
                    posFilter.RssiRangeFilter.MatchRange = MATCH_RANGE.WITHIN_RANGE;
                    posFilter.RssiRangeFilter.PeakRSSILowerLimit = -128;
                    posFilter.RssiRangeFilter.PeakRSSIUpperLimit = 127;

                    // 扫描按钮信息配置
                    TriggerInfo triggerInfo = new TriggerInfo();
                    // 此参数配置为0，扫描枪在接收到扫描按钮松开事件后，停止扫描
                    triggerInfo.TagReportTrigger = 0;

                    triggerInfo.StartTrigger.Type = START_TRIGGER_TYPE.START_TRIGGER_TYPE_HANDHELD;
                    triggerInfo.StartTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_PRESSED;
                    // 当扫描按钮松开后，经过一段时间出发扫描枪停止扫描事件（不过这里超时时间设为0，没有起到延迟触发事件的效果）
                    triggerInfo.StopTrigger.Type = STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_HANDHELD_WITH_TIMEOUT;
                    triggerInfo.StopTrigger.Handheld.Timeout = 0;

                    triggerInfo.StopTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_RELEASED;

                    rfidReader.Actions.Inventory.Perform(posFilter, triggerInfo, null);
                }

            }
            catch (Exception ex)
            {
                Message = "无法正常打开阅读器";
                LogService.Error(TAG, ex.Message);
            }
        }


        public void Stop()
        {
            try
            {
                string info = "status:stop";
                LogService.Info(TAG, info);

                if (rfidReader != null && rfidReader.IsConnected == true)
                {
                    rfidReader.Disconnect();
                    IsConnect = rfidReader.IsConnected;
                }
            }
            catch (Exception ex)
            {
                Message = "无法正常关闭阅读器";
                LogService.Error(TAG, ex.Message);
            }
        }


        protected void onIsConnectChanged(IsConnectChangedEventArgs e)
        {
            if (IsConnectChanged != null)
            {
                IsConnectChanged(this, e);
            }
        }
        protected void onTagDataReceived(TagDataReceivedEventArgs e)
        {
            if (TagDataReceived != null)
            {
                TagDataReceived(this, e);
            }
        }

        protected void onMessageReceived(MessageReceivedEventArgs e)
        {
            if (MessageReceived != null)
            {
                MessageReceived(this, e);
            }
        }


        private void Events_StatusNotify(object sender, Events.StatusEventArgs e)
        {
            // 如果是扫描按钮松开事件
            if (e.StatusEventData.StatusEventType == Symbol.RFID3.Events.STATUS_EVENT_TYPE.INVENTORY_STOP_EVENT)
            {
                TagDataReceivedEventArgs args = new TagDataReceivedEventArgs();

                // 读取扫描到的tag
                TagData[] tagData = rfidReader.Actions.GetReadTags(1000);
                args.TagData = new List<TagData>();
                foreach (TagData item in tagData)
                {
                    args.TagData.Add(item);
                }

                // 触发tag读取到事件（readerService的tag捕获事件）
                onTagDataReceived(args);
            }
        }

    }
}
