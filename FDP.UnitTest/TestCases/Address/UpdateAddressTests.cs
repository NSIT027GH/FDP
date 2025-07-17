using FDP.Application.Address;
using FDP.Shared;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace FDP.UnitTest.TestCases.Address
{
    public class UpdateAddressTests : IClassFixture<TestAppFactory>
    {
        private readonly HttpClient _client;
        private readonly string httpURL = "https://localhost:44320/api/Address/";
        public UpdateAddressTests(TestAppFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task UpdateAddress_ReturnsOk_WhenCommandIsValid()
        {
            var command = new UpdateAddressCommand
            {
                AddressId = 2,
                UserId = 1,
                Location = "location",
                Area = "area",
                City = "city",
                StateId = 16,
                CountryId = 1,
                Pincode = 100001,
                Status = (int)TaskStatusEnum.AddressStatus.DefaultAddress
            };

            var response = await _client.PutAsJsonAsync(httpURL + "UpdateAddressDetails", command);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadAsStringAsync();
            result.Should().Contain("1");
        }

        [Fact]
        public async Task UpdateAddress_ReturnsBadRequest_WhenValidationFails()
        {
            var command = new UpdateAddressCommand
            {
                AddressId = 3,
                UserId = 1,
                Location = null,
                Area = "area",
                City = "city",
                StateId = 16,
                CountryId = 1,
                Pincode = 100003
            };

            var response = await _client.PutAsJsonAsync(httpURL + "UpdateAddressDetails", command);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var result = await response.Content.ReadAsStringAsync();
        }

    }
}
