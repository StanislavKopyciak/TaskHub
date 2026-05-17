using TaskHub.Application.Services.TaskService.Command.GetAllCompletedTask;

namespace TaskHub.Test
{
    public class GetAllCompletedTaskValidatorTest
    {
        [Test]
        public void When_StateCompleted()
        {
            // Arrange
            var validator = new GetAllCompletedTaskValidator();
            var command = new GetAllCompletedTaskCommand
            {
                UserId = Guid.NewGuid(),
                State = TaskHub.Core.Enums.State.Completed
            };
            // Act
            var result = validator.Validate(command);
            // Assert
            if (!result.IsValid)
            {
                throw new Exception("Validation failed when it should have passed.");
            }
        }
    }
}
