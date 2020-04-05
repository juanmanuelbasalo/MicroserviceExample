using Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ApiTest.Unit.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void Home_controller_get_should_return_string_content()
        {
            var homeController = new HomeController();

            var result = homeController.Get();
            result.Should().BeOfType(typeof(ContentResult));
            var contentResult = result as ContentResult;
            contentResult.Content.Should().BeEquivalentTo("Testing Microservices.");
        }
    }
}
