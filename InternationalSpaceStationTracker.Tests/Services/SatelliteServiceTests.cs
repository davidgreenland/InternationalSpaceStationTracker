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
        public async Task GetSatellites_ReturnsDataWithExpectedProperties()
        {
            _handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                { 
                    Content = new StringContent(MockSatelliteData.GetSatellite())
                });

            var result = await _satelliteService.GetSatellites();
            var testSatellite = result.First();

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(testSatellite.Name, Is.EqualTo("testSatellite"));
            Assert.That(testSatellite.Id, Is.EqualTo(12345));
        }
        
        [Test]
        public async Task GetSatellites_WhenDataSetMoreThanOne_ReturnsAsExpected()
        {
            _handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(MockSatelliteData.GetLargerSatelliteSet())
                });

            var result = await _satelliteService.GetSatellites();
            var testSatellite = result.ElementAt(2);

            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(testSatellite.Name, Is.EqualTo("testSatellite3"));
            Assert.That(testSatellite.Id, Is.EqualTo(10003));
        }

        [Test]
        public async Task GetSatellites_WhenDataSetEmpty_ReturnsAnEmptyArray()
        {
            _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(MockSatelliteData.GetEmptySatelliteSet())
            });

            var result = await _satelliteService.GetSatellites();

            Assert.That(result.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetSingleSatellite_WhenGivenAnId_ReturnsDetailedData()
        {
            _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(MockSatelliteData.GetSingleSatellite())
            });

            var result = await _satelliteService.GetSingleSatellite(10001);

            Assert.That(result.Id, Is.EqualTo(10001));
            Assert.That(result.Latitude, Is.EqualTo(-16.0123456789));
            Assert.That(result.Longitude, Is.EqualTo(-125.0123456789));
            Assert.That(result.Velocity, Is.EqualTo(17145.0123456789));
            Assert.That(result.Units, Is.EqualTo("miles"));
        }

        [Test]
        public async Task GetSingleSatellite_WhenGivenAnInvalidId_Handles404()
        {
            _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(MockSatelliteData.GetInvalidSatelliteID())
            });

            var result = await _satelliteService.GetSingleSatellite(9999);

            Assert.That(result, Is.EqualTo(null));
        }

        [Test]
        public async Task GetLocation_WhenGivenValidCoordinates_ReturnsExpected()
        {
            _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(MockSatelliteData.GetValidLocation())
            });

            var result = await _satelliteService.GetLocation(36.892276895945m, 140.60862181833m);

            Assert.That(result.CountryCode, Is.EqualTo("JP"));
            Assert.That(result.MapUrl, Is.EqualTo("example.map.url"));
        }

        [Test]
        public async Task GetLocation_WhenGivenInValidCoordinates_Handles400()
        {
            _handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(MockSatelliteData.GetInvalidLocation())
            });

            var result = await _satelliteService.GetLocation(9999, 9999);

            Assert.That(result, Is.Null);
        }
    }
}
