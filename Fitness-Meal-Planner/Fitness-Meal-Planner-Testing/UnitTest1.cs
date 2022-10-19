using Application.Interfaces;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using WebAPI.Controllers;
using Microsoft.Extensions.Logging;
using FakeItEasy;
using Application.Dtos.MealDtos;
using FluentAssertions;

namespace Fitness_Meal_Planner_Testing
{
    public class VerifiableLogger : ILogger<MealsController>
    {
        public int calledCount { get; set; }

        public IDisposable BeginScope<TState>(TState state) => throw new NotImplementedException();
        public bool IsEnabled(LogLevel logLevel) => throw new NotImplementedException();
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            this.calledCount++;
        }
    }
    public class MealsControllerTests
    { 
        private readonly IMealsService _mealsService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly VerifiableLogger _logger;
       
        public MealsControllerTests()
        {
            _mealsService = A.Fake<IMealsService>();
            _webHostEnvironment = A.Fake<IWebHostEnvironment>();
            _logger = new VerifiableLogger();
        }

        [Fact]
        public void MealsController_GetAllMeals_ReturnQueryableMealsDto()
        {
            var meals = A.Fake<IQueryable<MealDto>>();
            A.CallTo(() => _mealsService.GetAllMeals()).Returns(meals);
            var controller = new MealsController(_mealsService, _webHostEnvironment, _logger);

            var result = controller.GetAllMeals();

            result.Should().BeOfType<IQueryable<MealDto>?>();
        }
    }
}