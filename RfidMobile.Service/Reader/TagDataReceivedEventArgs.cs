using System;
using System.Collections.Generic;
using System.Text;
using Symbol.RFID3;

namespace RfidMobile.Service.Reader
{
    public class TagDataReceivedEventArgs: EventArgs
    {
        public IList<TagData> TagData;
    }
}
