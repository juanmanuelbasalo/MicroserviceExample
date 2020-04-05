using Activities.Domain.Entities;
using Activities.Domain.Services;
using Common.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ActivitiesTest.Unit.Services
{
    public class ActivityServiceTests
    {
        private readonly Mock<IMongoRepository<Activity>> activityRepositoryMock;
        private readonly Mock<IMongoRepository<Category>> categoryRepositoryMock;
        private readonly ActivityService activityService;
        public ActivityServiceTests()
        {
            this.activityRepositoryMock = new Mock<IMongoRepository<Activity>>(); ;
            this.categoryRepositoryMock = new Mock<IMongoRepository<Category>>();
            this.activityService = new ActivityService(activityRepositoryMock.Object, categoryRepositoryMock.Object);
        }

        [Theory]
        [InlineData("test")]
        public async Task Activity_sevice_addAsync_should_succeed(string category)
        {
            categoryRepositoryMock.Setup(x => x.FindAsync(It.IsAny<Expression<Func<Category, bool>>>()))
                .ReturnsAsync(new Category(category));

            var id = Guid.NewGuid();
            var activity = new Activity(id: id, userId: Guid.NewGuid(), category,
                createdAt: DateTime.UtcNow, createdBy: "juan", description: "description", name: "activity");

            await activityService.AddAsync(activity);

            categoryRepositoryMock.Verify(x => x.FindAsync(It.IsAny<Expression<Func<Category, bool>>>()), Times.Once);
            activityRepositoryMock.Verify(x => x.InsertAsync(It.IsAny<Activity>()), Times.Once);
        }
    }
}
