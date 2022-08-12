using APIOne;
using APIOne.Controllers;
using EasyNetQ;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace tests;

public class UnitTest1
{
    public UnitTest1()
    {

    }


    [Fact]
    public async Task GetAsync_With_No_Item_ReturnsString()
    {
        // Arrange
        var logger = new Mock<ILogger<WeatherForecastController>>();
        var bus = new Mock<IBus>();
        var jwt = new Mock<IJwtAuthenticator>();
        var controller = new WeatherForecastController(logger.Object, bus.Object, jwt.Object);

        // Act
        var result = await controller.Get2Async();

        // Assert

        result.Should().BeOfType<string>();
    }

    [Fact]
    public void GetStringAsync_ShouldReturnData()
    {
        // Arrange
        var logger = new Mock<ILogger<WeatherForecastController>>();
        var bus = new Mock<IBus>();
        var jwt = new Mock<IJwtAuthenticator>();
        var controller = new WeatherForecastController(logger.Object, bus.Object, jwt.Object);

        // Act
        var result = controller.GetString("test");

        // Assert
        result.Should().NotBeNull();
    }
}