using System;
using System.Collections.Generic;
using System.Text;
// �˴����õ���win ce�汾��dll����Ҫ��ͨwindows�汾�ģ��Ǹ�dll����Զ������ɨ��ǹ
using Symbol.RFID3;
using System.Threading;
using RfidMobile.Service.Util;

namespace RfidMobile.Service.Reader
{
    /// <summary>
    /// Symbol MC319Z RFID��ȡ����
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
                    // ��֪ͨtag�����¼�������ɨ�谴ť�ɿ�ʱ����ȡ�����ı�ǩ��Ϣ
                    rfidReader.Events.AttachTagDataWithReadEvent = false;

                    // ע��ɨ��ǹ״̬�仯�¼�
                    rfidReader.Events.StatusNotify += new Events.StatusNotifyHandler(Events_StatusNotify);

                    // rssiֵ�������ã��˴�ȡ�����Сֵ�����û�й�������
                    PostFilter posFilter = new PostFilter();
                    posFilter.UseRSSIRangeFilter = true;
                    posFilter.RssiRangeFilter.MatchRange = MATCH_RANGE.WITHIN_RANGE;
                    posFilter.RssiRangeFilter.PeakRSSILowerLimit = -128;
                    posFilter.RssiRangeFilter.PeakRSSIUpperLimit = 127;

                    // ɨ�谴ť��Ϣ����
                    TriggerInfo triggerInfo = new TriggerInfo();
                    // �˲�������Ϊ0��ɨ��ǹ�ڽ��յ�ɨ�谴ť�ɿ��¼���ֹͣɨ��
                    triggerInfo.TagReportTrigger = 0;

                    triggerInfo.StartTrigger.Type = START_TRIGGER_TYPE.START_TRIGGER_TYPE_HANDHELD;
                    triggerInfo.StartTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_PRESSED;
                    // ��ɨ�谴ť�ɿ��󣬾���һ��ʱ�����ɨ��ǹֹͣɨ���¼����������ﳬʱʱ����Ϊ0��û�����ӳٴ����¼���Ч����
                    triggerInfo.StopTrigger.Type = STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_HANDHELD_WITH_TIMEOUT;
                    triggerInfo.StopTrigger.Handheld.Timeout = 0;

                    triggerInfo.StopTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_RELEASED;

                    rfidReader.Actions.Inventory.Perform(posFilter, triggerInfo, null);
                }

            }
            catch (Exception ex)
            {
                Message = "�޷��������Ķ���";
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
                Message = "�޷������ر��Ķ���";
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
            // �����ɨ�谴ť�ɿ��¼�
            if (e.StatusEventData.StatusEventType == Symbol.RFID3.Events.STATUS_EVENT_TYPE.INVENTORY_STOP_EVENT)
            {
                TagDataReceivedEventArgs args = new TagDataReceivedEventArgs();

                // ��ȡɨ�赽��tag
                TagData[] tagData = rfidReader.Actions.GetReadTags(1000);
                args.TagData = new List<TagData>();
                foreach (TagData item in tagData)
                {
                    args.TagData.Add(item);
                }

                // ����tag��ȡ���¼���readerService��tag�����¼���
                onTagDataReceived(args);
            }
        }

    }
}
