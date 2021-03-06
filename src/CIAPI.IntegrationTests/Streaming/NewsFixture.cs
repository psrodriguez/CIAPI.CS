using System;
using System.Threading;
using CIAPI.DTO;
using CIAPI.Streaming;
using Newtonsoft.Json;
using NUnit.Framework;
using StreamingClient;
using IStreamingClient = CIAPI.Streaming.IStreamingClient;

namespace CIAPI.IntegrationTests.Streaming
{
    [TestFixture]
    public class NewsFixture
    {
        public static IStreamingClient BuildStreamingClient(
            string userName = "xx189949",
            string password = "password")
        {
            const string apiUrl = "https://ciapipreprod.cityindextest9.co.uk/TradingApi/";

            var authenticatedClient = new CIAPI.Rpc.Client(new Uri(apiUrl));
            authenticatedClient.LogIn(userName, password);

            var streamingUri = new Uri("https://pushpreprod.cityindextest9.co.uk");

            return StreamingClientFactory.CreateStreamingClient(streamingUri, userName, authenticatedClient.Session);
        }

        [Test]
        public void CanConsumeNewsStream()
        {
            var gate = new ManualResetEvent(false);

            var streamingClient = BuildStreamingClient();
            streamingClient.Connect();

            var newsListener = streamingClient.BuildNewsHeadlinesListener("UK");
            newsListener.Start();

            NewsDTO actual = null;

            //Trap the Price given by the update event for checking
            newsListener.MessageReceived += (s, e) =>
            {
                actual = e.Data;
                Console.WriteLine(actual);
                gate.Set();
            };

            bool timedOut = false;

            if (!gate.WaitOne(TimeSpan.FromSeconds(15)))
            {
                timedOut = true;
            }

            newsListener.Stop();
            streamingClient.Disconnect();

            Assert.IsFalse(timedOut,"timed out");
            Assert.IsNotNull(actual);
            Assert.IsNotEmpty(actual.Headline);

            Assert.Greater(actual.PublishDate, DateTime.UtcNow.AddMonths(-1));
            Assert.Greater(actual.StoryId, 0);
        }
    }
}
