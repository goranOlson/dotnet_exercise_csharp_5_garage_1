using dotnet_exercise_csharp_5_garage_1.Classes;

namespace dotnet_exercise_csharp_5_garage_1.Test
{
    public class GarageTest
    {
        [Fact]
        public void Garage_ConstructCapacity_ShouldCreateGarageWithRightCapacity()
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
        public void Garage_AddVehicle_ShouldBeOK()
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
        public void Garage_AddVehicle_WhenIsFull_ShouldFail()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(1);
            Car car1 = new Car("ABC123", "Röd", 4, "Gasoline", 2100);
            Car car2 = new Car("YYY099", "Röd", 4, "Gasoline", 2100);

            // Act
            garage.AddVehicle(car1);
            bool result = garage.AddVehicle(car2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Garage_RemoveVehicle_WhenNotEmpty_ShouldBeOK()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(1);
            Car car = new Car("ABC123", "Röd", 4, "Gasoline", 2100);

            // Act
            garage.AddVehicle(car);
            bool result = garage.RemoveVehicle(car);

            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public void GarageRemoveVehicle_WhenEmpty_ShouldFail()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(1);
            Car car = new Car("ABC123", "Röd", 4, "Gasoline", 2100);

            // Act
            garage.AddVehicle(car);
            garage.RemoveVehicle(car);
            bool result = garage.RemoveVehicle(car);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Garage_Count_InitValue_ShouldBeTrue()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(1);

            // Act
            uint count = garage.Count;

            // Assert
            Assert.True(count == 0);
        }

        [Fact]
        public void Garage_IsFull_WhenNotFull_ShouldFalse()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(1);

            // Act
            bool result = garage.IsFull;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Garage_IsFull_WhenFull_ShouldBeTrue()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(0);

            // Act
            bool result = garage.IsFull;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Garage_Capacity_WhenFull_ShouldBeOk()
        {
            // Arrange
            Garage<Vehicle> garage = new Garage<Vehicle>(0);

            // Act
            bool result = garage.IsFull;

            // Assert
            Assert.True(result);
        }
    }
}
