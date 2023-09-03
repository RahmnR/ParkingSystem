namespace ParkingApplication;

public class Slot
{
    public int Number { get; }
    public Vehicle ParkedVehicle { get; private set; }
    public bool IsEmpty => ParkedVehicle == null;

    public Slot(int number)
    {
        Number = number;
    }

    public void CheckIn(Vehicle vehicle)
    {
        ParkedVehicle = vehicle;
    }

    public void CheckOut()
    {
        ParkedVehicle = null;
    }
}