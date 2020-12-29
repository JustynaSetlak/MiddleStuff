using ControllerTests.Dtos;

namespace ControllerTests.Services
{
    public class GoalService : IGoalService
    {
        public bool AddNewGoal(GoalDto goal)
        {
            if(goal == null)
            {
                return false;
            }

            //some logic

            return true;
        }
    }
}
