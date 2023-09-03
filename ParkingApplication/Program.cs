// See https://aka.ms/new-console-template for more information

using ParkingApplication;

class Program
{
    static void Main()
    {
        Console.WriteLine("Parking System Console Application Start");

        ParkingSystem parkingSystem = null;

        while (true)
        {
            Console.Write("Enter a command: ");
            string input = Console.ReadLine().Trim().ToLower();

            if (input.StartsWith("create_parking_lot"))
            {
                int totalSlots = int.Parse(input.Split(' ')[1]);
                parkingSystem = new ParkingSystem(totalSlots);
                Console.WriteLine($"Created a parking lot with {totalSlots} slots");
            }
            else if (parkingSystem == null)
            {
                Console.WriteLine("Please create a parking lot first.");
            }
            else if (input.StartsWith("park"))
            {
                string[] parts = input.Split(' ');
                string registrationNumber = parts[1];
                string color = parts[2];
                string vehicleType = parts[3];
                parkingSystem.CheckIn(registrationNumber, color, vehicleType);
            }
            else if (input.StartsWith("leave"))
            {
                int slotNumber;
                if (int.TryParse(input.Split(' ')[1], out slotNumber))
                {
                    parkingSystem.CheckOut(slotNumber);
                }
                else
                {
                    Console.WriteLine("Invalid slot number.");
                }
            }
            else if (input == "status")
            {
                parkingSystem.GetStatus();
            }
            else if (input.StartsWith("type_of_vehicles"))
            {
                string type = input.Split(' ')[1];
                List<int> typeList = parkingSystem.GetByType(type);
                if (typeList.Count>0)
                {
                    Console.WriteLine(string.Join(", ", typeList));
                }
                else
                {
                    Console.WriteLine("Not found");
                }
            }
            else if (input.StartsWith("registration_numbers_for_vehicles_with_colour"))
            {
                string color = input.Split(' ')[1];
                var registrationNumbers = parkingSystem.GetRegistrationNumbersByColor(color);
                Console.WriteLine(string.Join(", ", registrationNumbers));
            }
            else if (input.StartsWith("slot_numbers_for_vehicles_with_colour"))
            {
                String color = input.Split(' ')[1];
                var slotNumbers = parkingSystem.GetByColor(color);
                if (slotNumbers.Count>0)
                {
                    Console.WriteLine(string.Join(", ", slotNumbers));
                }
                else
                {
                    Console.WriteLine("Not found");
                }
            }
            else if (input.StartsWith("slot_number_for_registration_number"))
            {
                string registrationNumber = input.Split(' ')[1];
                int slotNumber = parkingSystem.GetByNumber(registrationNumber);
                if (slotNumber != -1)
                {
                    Console.WriteLine(slotNumber);
                }
                else
                {
                    Console.WriteLine("Not found");
                }
            }
            else if (input.StartsWith("registration_numbers_for_vehicles_with_ood_plate"))
            {
                List<string> vehiclesOddNumber = parkingSystem.GetOdd();
                if (vehiclesOddNumber.Count > 0)
                {
                    Console.WriteLine(string.Join(", ", vehiclesOddNumber));
                }
                else
                {
                    Console.WriteLine("Not Found");
                }
                
            }
            else if (input.StartsWith("registration_numbers_for_vehicles_with_event_plate"))
            {
                List<string> vehiclesEvenNumber = parkingSystem.GetEven();
                if (vehiclesEvenNumber.Count > 0)
                {
                    Console.WriteLine(string.Join(", ", vehiclesEvenNumber));
                }
                else
                {
                    Console.WriteLine("Not Found");
                }
                
            }
            else if (input == "exit")
            {
                Console.WriteLine("Good Bye!");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid command. Please try again.");
            }
        }
    }
}   