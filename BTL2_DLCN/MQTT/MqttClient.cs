using MQTTnet.Client;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL2_DLCN.MQTT
{
    public class MqttClient
    {
        public MqttOptions Options { get; set; }
        public bool IsConnected => _mqttClient is not null && _mqttClient.IsConnected;

#pragma warning disable CS0067 // The event 'MqttClient.ApplicationMessageReceived' is never used
        public event Func<MqttApplicationMessageReceivedEventArgs, Task>? ApplicationMessageReceived;
#pragma warning restore CS0067 // The event 'MqttClient.ApplicationMessageReceived' is never used

        private IMqttClient? _mqttClient;

        public MqttClient()
        {
            _mqttClient = new MqttFactory().CreateMqttClient();
            Options = new MqttOptions()
            {
                //40.82.154.13
                CommunicationTimeout = 30,
                Host = "test.mosquitto.org",
                Port = 1883,
                KeepAliveInterval = 10
            };
        }

        public async Task ConnectAsync()
        {
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(Options.Host, Options.Port)
                .WithTimeout(TimeSpan.FromSeconds(Options.CommunicationTimeout))
                .WithKeepAlivePeriod(TimeSpan.FromSeconds(Options.KeepAliveInterval));

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            //_mqttClient.ApplicationMessageReceivedAsync += ApplicationMessageReceived;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(Options.CommunicationTimeout));
            var result = await _mqttClient.ConnectAsync(mqttClientOptions.Build(), timeout.Token);

            if (result.ResultCode != MqttClientConnectResultCode.Success)
            {
                throw new MqttConnectionException($"{result.ResultCode}: {result.ReasonString}");
            }
        }

        public async Task DisconnectAsync()
        {
            await _mqttClient.DisconnectAsync();
        }

        public async Task Subscribe(string topic)
        {
            if (_mqttClient is null)
            {
                throw new InvalidOperationException("MQTT Client is not connected.");
            }

            var topicFilter = new MqttTopicFilterBuilder()
                .WithTopic(topic)
                .Build();

            var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
                .WithTopicFilter(topicFilter)
                .Build();

            var result = await _mqttClient.SubscribeAsync(subscribeOptions);

            foreach (var subscription in result.Items)
            {
                if (subscription.ResultCode != MqttClientSubscribeResultCode.GrantedQoS0 &&
                    subscription.ResultCode != MqttClientSubscribeResultCode.GrantedQoS1 &&
                    subscription.ResultCode != MqttClientSubscribeResultCode.GrantedQoS2)
                {
                    Console.WriteLine($"MQTT Client Subscription {subscription.TopicFilter.Topic} Failed: {subscription.ResultCode}");
                }
            }
        }

        public async Task Publish(string topic, string payload, bool retainFlag)
        {
            if (_mqttClient is null)
            {
                throw new InvalidOperationException("MQTT Client is not connected.");
            }

            var applicationMessageBuilder = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithRetainFlag(retainFlag)
                .WithPayload(payload);

            var applicationMessage = applicationMessageBuilder.Build();

            var result = await _mqttClient.PublishAsync(applicationMessage);

            if (result.ReasonCode != MqttClientPublishReasonCode.Success)
            {
                Console.WriteLine($"MQTT Client Publish {applicationMessage.Topic} Failed: {result.ReasonCode}");
            }
        }

    }
}
