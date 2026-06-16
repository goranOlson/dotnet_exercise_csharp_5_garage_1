using dotnet_exercise_csharp_5_garage_1.Classes;

namespace dotnet_exercise_csharp_5_garage_1.Test
{
    public class GarageTest
    {
        [Fact]
        public void GarageConstruct_CapacityInput_ShouldCreateGarageWithRightCapacity()
        {
            // Arrange
            uint capacity = 10;
            Garage<Vehicle> garage = new Garage<Vehicle>(capacity);

            // Act
            uint result = garage.Capacity;

            // Assert
            Assert.Equal(capacity, result);
        }
    }
}
