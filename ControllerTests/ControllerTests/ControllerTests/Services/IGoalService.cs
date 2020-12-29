using ControllerTests.Dtos;

namespace ControllerTests.Services
{
    public interface IGoalService
    {
        bool AddNewGoal(GoalDto goal);
    }
}
