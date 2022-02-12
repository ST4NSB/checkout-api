using checkout;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using Xunit;

namespace IntegrationTests
{
    public class TestClientProvider
    {
        public HttpClient Client { get; private set; }

        public TestClientProvider()
        {
            var server = new TestServer(new WebHostBuilder().UseStartup(typeof(Startup)));

            Client = server.CreateClient();
        }
    }
}
