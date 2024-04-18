using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL2_DLCN.MQTT
{
    public class MqttConnectionException : Exception
    {
        public MqttConnectionException(string? message) : base(message)
        {
        }
    }
}
