using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RfidMobile.Service.Reader
{
    /// <summary>
    /// 阅读器服务接口
    /// </summary>
    /// <remarks>
    /// 2017-3-24 wzl
    /// 1.0.0
    /// </remarks>
    public interface IRfidReaderService
    {
        /// <summary>
        /// 服务最后一次通知的消息
        /// </summary>
        string Message { get; set; }
        /// <summary>
        /// 当前阅读器连接状态
        /// </summary>
        bool IsConnect { get; set; }
        /// <summary>
        /// 连接状态变化事件
        /// </summary>
        event EventHandler<IsConnectChangedEventArgs> IsConnectChanged;
        /// <summary>
        /// 数据接收事件
        /// </summary>
        event EventHandler<TagDataReceivedEventArgs> TagDataReceived;
        /// <summary>
        /// 服务通知事件，需要通知UI则往这里发消息
        /// </summary>
        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        /// <summary>
        /// 开始读取标签
        /// </summary>
        void Start();
        /// <summary>
        /// 停止读取标签
        /// </summary>
        void Stop();
    }
}
