namespace ParkingApplication;

public class ParkingSystem
{
    private int totalSlots;
    private List<Slot> parkingSlots;

    public ParkingSystem(int totalSlots)
    {
        this.totalSlots = totalSlots;
        parkingSlots = new List<Slot>();
        for (int i = 1; i <= totalSlots; i++)
        {
            parkingSlots.Add(new Slot(i));
        }
    }

    public List<string> GetOdd()
    { 
        return parkingSlots.Where(slot => !slot.IsEmpty && IsNumberOdd(slot.ParkedVehicle.RegistrationNumber))
                .Select(slot => slot.ParkedVehicle.RegistrationNumber)
                .ToList();
    }

    public List<string> GetEven()
    {
        return parkingSlots.Where(slot => !slot.IsEmpty && IsNumberEven(slot.ParkedVehicle.RegistrationNumber))
            .Select(slot => slot.ParkedVehicle.RegistrationNumber)
            .ToList();
    }
    private bool IsNumberOdd(string registrationNumber)
    {
        string[] parts = registrationNumber.Split('-');
        if (int.TryParse(parts[1], out int numericValue))
        {
            return numericValue % 2 != 0;
        }
        return false;
    }
    private bool IsNumberEven(string registrationNumber)
    {
        string[] parts = registrationNumber.Split('-');
        if (int.TryParse(parts[1], out int numericValue))
        {
            return numericValue % 2 == 0;
        }
        return false;
    }
    public void CheckIn(string registrationNumber, string color, string vehicleType)
    {
        if (IsParkingFull())
        {
            Console.WriteLine("Sorry, parking lot is full");
            return;
        }

        var availableSlot = GetNextAvailable();
        availableSlot.CheckIn(new Vehicle(registrationNumber, color, vehicleType));

        Console.WriteLine($"Allocated slot number: {availableSlot.Number}");
    }

    public void CheckOut(int slotNumber)
    {
        var slot = parkingSlots.FirstOrDefault(s => s.Number == slotNumber);
        if (slot != null)
        {
            slot.CheckOut();
            Console.WriteLine($"Slot number {slot.Number} is free");
        }
        else
        {
            Console.WriteLine($"Slot number {slotNumber} not found");
        }
    }

    public void GetStatus()
    {
        Console.WriteLine("Slot\tNo.\tType\tRegistration No\tColour");
        foreach (var slot in parkingSlots)
        {
            if (!slot.IsEmpty)
            {
                var vehicle = slot.ParkedVehicle;
                Console.WriteLine(
                    $"{slot.Number}\t{vehicle.RegistrationNumber}\t{vehicle.VehicleType}\t{vehicle.Color}");
            }
        }
    }

    public List<int> GetByType(string type)
    {
        return parkingSlots
            .Where(slot => !slot.IsEmpty && slot.ParkedVehicle.VehicleType == type)
            .Select(slot => slot.Number)
            .ToList();
    }

    public List<string> GetRegistrationNumbersByColor(string color)
    {
        return parkingSlots
            .Where(slot => !slot.IsEmpty && slot.ParkedVehicle.Color == color)
            .Select(slot => slot.ParkedVehicle.RegistrationNumber)
            .ToList();
    }

    public List<int> GetByColor(string color)
    {
        return parkingSlots
            .Where(slot => !slot.IsEmpty && slot.ParkedVehicle.Color == color)
            .Select(slot => slot.Number)
            .ToList();
    }

    public int GetByNumber(string registrationNumber)
    {
        var slot = parkingSlots.FirstOrDefault(s => !s.IsEmpty && s.ParkedVehicle.RegistrationNumber == registrationNumber);
        return slot != null ? slot.Number : -1;
    }

    private bool IsParkingFull()
    {
        return parkingSlots.All(slot => !slot.IsEmpty);
    }

    private Slot GetNextAvailable()
    {
        return parkingSlots.FirstOrDefault(slot => slot.IsEmpty);
    }
}