using InternationalSpaceStationTracker.Services;
using InternationalSpaceStationTracker.Tests.Data;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Net;

namespace InternationalSpaceStationTracker.Tests.Services
{
    public class SatelliteServiceTests
    {
        private SatelliteService _satelliteService;
        private HttpClient _httpClient;
        private Mock<HttpMessageHandler> _handlerMock;

        [SetUp]
        public void Setup()
        {
            _handlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_handlerMock.Object);
            _satelliteService = new SatelliteService(_httpClient);
        }

        [Test]
        public async Task GetSatellites_Test()
        {
            _handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(MockSatelliteData.GetSatelliteData())
                });

            var result = await _satelliteService.GetSatellites();
            var testSatellite = result.First();

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(testSatellite.Name, Is.EqualTo("testSatellite"));
            Assert.That(testSatellite.Id, Is.EqualTo(12345));
        }
    }
}
