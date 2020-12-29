using ControllerTests.Dtos;
using ControllerTests.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControllerTests.Controllers
{
    [ApiController]
    [Route("api/goals")]
    public class GoalController : Controller
    {
        private readonly IGoalService _goalService;

        public GoalController(IGoalService goalService)
        {
            _goalService = goalService;
        }

        [HttpPost]
        public IActionResult Post(GoalDto goal)
        {
            var isSuccessful = _goalService.AddNewGoal(goal);

            if (!isSuccessful)
            {
                return BadRequest();
            }

            return Ok(goal.Name);
        }
    }
}
