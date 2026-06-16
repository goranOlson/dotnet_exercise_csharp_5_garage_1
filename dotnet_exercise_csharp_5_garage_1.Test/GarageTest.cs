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

        [Fact]
        public void GarageAddVehicle_ShouldBeOK()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(10);
            Car car = new Car("YYY099", "Röd", 4, "Gasoline", 2100);

            // Act
            bool result = garage.AddVehicle(car);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GarageAddVehicle_WhenIsFull_ShouldFail()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(1);
            Car car1 = new Car("YYY099", "Röd", 4, "Gasoline", 2100);
            Car car2 = new Car("YYY099", "Röd", 4, "Gasoline", 2100);

            // Act
            garage.AddVehicle(car1);
            bool result2 = garage.AddVehicle(car2);

            // Assert
            Assert.False(result2);
        }


    }
}
