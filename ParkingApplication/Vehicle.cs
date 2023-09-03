namespace ParkingApplication;

public class Vehicle
{
    public string RegistrationNumber { get; }
    public string Color { get; }
    public string VehicleType { get; }

    public Vehicle(string registrationNumber, string color, string vehicleType)
    {
        RegistrationNumber = registrationNumber;
        Color = color;
        VehicleType = vehicleType;
    }
}