using FDP.API.Controllers;
using FDP.Application.Address;
using FDP.Lib;
using FDP.Shared;
using FluentAssertions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace FDP.UnitTest.TestCases.Address
{
    public class CreateAddressTests : IClassFixture<TestAppFactory>
    {
        private readonly HttpClient _client;
        private readonly string httpURL = "https://localhost:44320/api/Address/";
        public CreateAddressTests(TestAppFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task AddNewAddress_ReturnsOk_WhenCommandIsValid()
        {
            //// Arrange
            //var command = FakeAddressData.GetValidCommand();

            //var mockService = new Mock<IAddressService>();
            //var mockHandler = new Mock<CreateAddressCommandHandler>(MockBehavior.Strict, mockService.Object);
            //var mockValidator = new Mock<CreateAddressCommandValidator>();

            //mockHandler.Setup(h => h.Handle(command, It.IsAny<CancellationToken>()))
            //    .ReturnsAsync("Address Created");

            //mockValidator
            //    .Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
            //    .ReturnsAsync(new ValidationResult());

            //var controller = new AddressController(mockService.Object, mockHandler.Object, mockValidator.Object);

            //// Act
            //var result = await controller.AddNewAddress(command);

            //// Assert
            //result.Should().BeOfType<OkObjectResult>();


            var command = new CreateAddressCommand
            {
                UserId = 2,
                Location = "Temp",
                Area = "areaTemp",
                City = "cityTemp",
                StateId = 16,
                CountryId = 1,
                Pincode = 100001
            };

            var response = await _client.PostAsJsonAsync(httpURL + "AddNewAddress", command);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadAsStringAsync();
            result.Should().Contain("1");
        }

        [Fact]
        public async Task AddNewAddress_ReturnsBadRequest_WhenValidationFails()
        {
            // Arrange
            var command = new CreateAddressCommand
            {
                UserId = 2,
                Location = null,
                Area = "area",
                City = "city",
                StateId = 16,
                CountryId = 1,
                Pincode = 100001
            };

            var response = await _client.PostAsJsonAsync(httpURL + "AddNewAddress", command);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    
    }
}
