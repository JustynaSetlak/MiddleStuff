using ControllerTests.Controllers;
using ControllerTests.Dtos;
using ControllerTests.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ControllerTests.Tests.ControllerTests
{
    public class GoalControllerTests
    {
        private Mock<IGoalService> _goalServiceMock;

        private GoalController _sut;

        [SetUp]
        public void Setup()
        {
            _goalServiceMock = new Mock<IGoalService>();

            _sut = new GoalController(_goalServiceMock.Object);
        }

        [Test]
        public void Post_Should_ReturnCallServiceMethod_When_PostExecuted()
        {
            //arrange
            var goalData = new GoalDto
            {
                Name = "new goal",
                Description = "new description"
            };

            //act
            var response = _sut.Post(goalData);

            //assert
            _goalServiceMock.Verify(x => x.AddNewGoal(It.Is<GoalDto>(g => g.Name == goalData.Name)), Times.Once);
        }

        [Test]
        public void Post_Should_ReturnBadRequestResult_When_AddingNotSuccessful()
        {
            //arrange
            var goalData = new GoalDto
            {
                Name = "new goal",
                Description = "new description"
            };

            _goalServiceMock
                .Setup(x => x.AddNewGoal(It.IsAny<GoalDto>()))
                .Returns(false);

            //act
            var response = _sut.Post(goalData);

            //assert
            response.Should().BeOfType<BadRequestResult>();
        }

        [Test]
        public void Post_Should_ReturnOKResult_When_AddingSuccessful()
        {
            //arrange
            var goalData = new GoalDto
            {
                Name = "new goal",
                Description = "new description"
            };

            _goalServiceMock
                .Setup(x => x.AddNewGoal(It.IsAny<GoalDto>()))
                .Returns(true);

            //act
            var response = _sut.Post(goalData) as OkObjectResult;

            //assert
            response.Value.Should().Be(goalData.Name);
        }
    }
}
