using CopaFilmes.Domain.Entities;
using CopaFilmes.Domain.Message;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace CopaFilmes.Integration.Test
{
    public class ChampionshipTest
    {
        private string WebAppName => "CopaFilmes.Application";
        private readonly HttpClient httpClient;
        private string testLibPath;
        private string Url => "api/ChampionShip";

        public ChampionshipTest()
        {
            var pathRoot = this.GetAppBasePath(this.WebAppName);
            var builder = new WebHostBuilder().UseContentRoot(pathRoot).UseStartup(this.WebAppName);
            var testServer = new TestServer(builder);
            this.httpClient = testServer.CreateClient();
        }

        [Fact]
        public async Task GetAsync()
        {
            var responseMessage = await this.httpClient.GetAsync(this.Url);
            var content = await responseMessage.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
            Assert.Equal("OK", content);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(7)]
        [InlineData(9)]
        public async Task PostExceptionGenerateChampionshipAsync(int quantityElements)
        {
            int numberParticipants = 8;
            List<Movie> movies = new List<Movie>();
            for (int i = 0; i < quantityElements; i++)
            {
                movies.Add(new Movie());
            }

            var stringContent = new StringContent(JsonSerializer.Serialize(movies));
            var responseMessage = await this.httpClient.PostAsync($"{this.Url}/GenerateChampionship", stringContent);

            var content = await responseMessage.Content.ReadAsStringAsync();
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(content);

            Assert.Equal(HttpStatusCode.InternalServerError, responseMessage.StatusCode);
            Assert.Equal("application/problem+json", responseMessage.Content.Headers.ContentType.ToString());
            Assert.Equal(500, problemDetails.Status);
            Assert.Equal(Message.ChampionshipMessage.NumberParticipantsShouldBeEqual(numberParticipants), problemDetails.Title);
            Assert.Equal("/api/Values/Exception", problemDetails.Instance);
            Assert.Equal("/api/Values/Exception", problemDetails.Detail); 
        }

        private string GetAppBasePath(string applicationWebSiteName)
        {
            var binPath = Environment.CurrentDirectory;
            while (!string.Equals(Path.GetFileName(binPath), "bin", StringComparison.InvariantCultureIgnoreCase))
            {
                binPath = Path.GetFullPath(Path.Combine(binPath, ".."));
                if (string.Equals(Path.GetPathRoot(binPath), binPath, StringComparison.InvariantCultureIgnoreCase))
                    throw new Exception("Could not find bin directory for test library.");
            }

            this.testLibPath = Path.GetFullPath(Path.Combine(binPath, ".."));
            var testPath = Path.GetFullPath(Path.Combine(testLibPath, ".."));
            var srcPath = Path.GetFullPath(Path.Combine(testPath, "..", "src"));

            if (!Directory.Exists(srcPath))
                throw new Exception("Could not find src directory.");

            var appBasePath = Path.Combine(srcPath, applicationWebSiteName);
            if (!Directory.Exists(appBasePath))
                throw new Exception("Could not find directory for application.");

            return appBasePath;
        }
    }
}
