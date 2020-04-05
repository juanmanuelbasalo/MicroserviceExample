using Api.Controllers;
using Api.Domain.Services;
using Common.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiTest.Unit.Controllers
{
    public class ActivitiesControllerTests
    {
        [Fact]
        public async Task Activities_controller_post_should_return_accepted()
        {
            var busClientMock = new Mock<IBusClient>();
            var activityServiceMock = new Mock<IActivityService>();
            var activitiesController = new ActivitiesController(busClientMock.Object, activityServiceMock.Object);
            var userId = Guid.NewGuid();
            activitiesController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(
                        new Claim[] { new Claim(ClaimTypes.Name, userId.ToString()) }, "test"))
                }
            };

            var command = new CreateActivityCommand
            {
                Id = Guid.NewGuid(),
                UserId = userId
            };

            var result = await activitiesController.CreateActivity(command);
            var contentResult = result as AcceptedResult;
            contentResult.Should().NotBeNull();
            contentResult.Location.Should().BeEquivalentTo($"Activities/{command.Id}");
        }
    }
}
