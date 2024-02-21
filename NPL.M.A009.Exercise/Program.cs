using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NPL.M.A009.Exercise_
{
    public abstract class Airplane
    {
        public string id;
        public string model;
        public string planeType;
        public double cruiseSpeed;
        public double emptyWeight;
        public double maxTakeoffWeight;

        public string ID { get { return id; } set { id = value; } }
        public string Model { get { return model; } set { model = value; } }
        public string PlaneType { get { return planeType; } set { planeType = value; } }
        public double CruiseSpeed { get { return cruiseSpeed; } set { cruiseSpeed = value; } }
        public double EmptyWeight { get { return emptyWeight; } set { emptyWeight = value; } }
        public double MaxTakeoffWeight { get { return maxTakeoffWeight; } set { maxTakeoffWeight = value; } }

        public abstract void Fly();
    }

    public class Fixedwing : Airplane
    {
        public double minNeededRunwaySize;

        public double MinNeededRunwaySize { get => minNeededRunwaySize; set => minNeededRunwaySize = value; }

        public Fixedwing(string id, string model, string planeType, double cruiseSpeed, double emptyWeight, double maxTakeoffWeight, double minNeededRunwaySize)
        {
            this.id = id;
            this.model = model;
            this.planeType = planeType;
            this.cruiseSpeed = cruiseSpeed;
            this.emptyWeight = emptyWeight;
            this.maxTakeoffWeight = maxTakeoffWeight;
            this.minNeededRunwaySize = minNeededRunwaySize;
        }

        public override void Fly()
        {
            Console.WriteLine("Fixed wing");
        }

        public override string ToString()
        {
            return $"ID: {ID}; Model: {Model}; Plane Type: {PlaneType}; Cruise Speed: {CruiseSpeed};" +
                $" Empty Weight: {EmptyWeight}; Max Takeoff Weight: {MaxTakeoffWeight};" +
                $" Min Needed Runway Size: {MinNeededRunwaySize}";
        }
    }

    public class Helicopter : Airplane
    {
        public double range;

        public double Range { get => range; set => range = value; }

        public Helicopter(string id, string model, string planeType, double cruiseSpeed, double emptyWeight, double maxTakeoffWeight, double range)
        {
            this.id = id;
            this.model = model;
            this.planeType = planeType;
            this.cruiseSpeed = cruiseSpeed;
            this.emptyWeight = emptyWeight;

            if (maxTakeoffWeight > 1.5 * emptyWeight)
            {
                this.maxTakeoffWeight = maxTakeoffWeight;
                this.range = range;
            }
            else
            {
                Console.WriteLine("Max takeoff weight cannot exceed 1.5 times of empty weight");
            }
            this.range = range;
        }

        public override void Fly()
        {
            Console.WriteLine("Rotated wing");
        }
        //gfdgdg
        public override string ToString()
        {
            return $"ID: {ID}; Model: {Model}; Plane Type: {PlaneType}; Cruise Speed: {CruiseSpeed};" +
                $" Empty Weight: {EmptyWeight}; Max Takeoff Weight: {MaxTakeoffWeight}; Range: {Range}";
        }
    }
    public class Helicopters
    {
        public List<Helicopter> helicopters = new List<Helicopter>();
        public void AddHelicopter(Helicopter helicopter)
        {
            int indexToAdd = this.helicopters.FindIndex(f => f.ID == helicopter.ID);
            if (indexToAdd == -1)
            {
                this.helicopters.Add(helicopter);
            }
            else
            {
                Console.WriteLine($"{helicopter.ID} already exists");
            }
        }
        public void RemoveRotatedWingByID(string id)
        {
            // Find the index of the element with the specified ID
            int indexToRemove = this.helicopters.FindIndex(f => f.ID == id);

            // Remove the element if found
            if (indexToRemove != -1)
            {
                this.helicopters.RemoveAt(indexToRemove);
                Console.WriteLine($"Fixedwing with ID {id} removed successfully.");
            }
            else
            {
                Console.WriteLine($"Fixedwing with ID {id} not found.");
            }
        }
    }
    public class Airport
    {
        public string id;
        public string name;
        public double runwaySize;
        public int maxFixedwingParkingPlace;
        public int maxRotatedwingParkingPlace;
        public List<string> listOfFixedwingAirplaneID = new List<string>();
        public List<string> listOfRotatedwingAirplaneID = new List<string>();

        public Airport()
        {
        }

        public Airport(string id, string name, double runwaySize, int maxFixedwingParkingPlace, int maxRotatedwingParkingPlace)
        {
            this.id = id;
            this.name = name;
            this.runwaySize = runwaySize;
            this.maxFixedwingParkingPlace = maxFixedwingParkingPlace;
            this.maxRotatedwingParkingPlace = maxRotatedwingParkingPlace;
        }
    }
    public class Airports
    {
        public List<Airport> airports = new List<Airport>();
        public void AddAirport()
        {
            Airport airport;
            Console.WriteLine("Airport details: ");
            do
            {
                try
                {
                    Console.Write("ID: ");
                    string id = "";
                    string pattern = @"^(FW|RW|AP)\d{5}$";
                    Regex regex = new Regex(pattern);
                    do
                    {
                        id = Console.ReadLine();

                        if (!regex.IsMatch(id))
                        {
                            Console.WriteLine("ID is not valid. Please re-enter ID");
                        }
                    }
                    while (!regex.IsMatch(id));
                    Console.Write("Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Runway Size: ");
                    double runwaySize = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Max Fixedwing Parking Place: ");
                    int maxFixedwingParkingPlace = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Max Rotated Wing Parking Place: ");
                    int maxRotatedwingParkingPlace = Convert.ToInt32(Console.ReadLine());
                    airport = new Airport(id, name, runwaySize, maxFixedwingParkingPlace, maxRotatedwingParkingPlace);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number. Re-enter airport.");
                    airport = null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "Re-enter airport.");
                    airport = null;
                }
            }
            while (airport == null);
            int indexToAdd = this.airports.FindIndex(f => f.id == airport.id);
            if (indexToAdd == -1)
            {
                this.airports.Add(airport);
            }
            else
            {
                Console.WriteLine($"{airport.id} already exists");
            }

        }
        public void RemoveAirportByID(string id)
        {
            int indexToRemove = this.airports.FindIndex(f => f.id == id);

            // Remove the element if found
            if (indexToRemove != -1)
            {
                this.airports.RemoveAt(indexToRemove);
                Console.WriteLine($"Fixedwing with ID {id} removed successfully.");
            }
            else
            {
                Console.WriteLine($"Fixedwing with ID {id} not found.");
            }
        }
        public void ShowAirports()
        {
            airports = this.airports.OrderBy(a => a.id).ToList();
            Console.WriteLine("\nList of Airports sorted by airport ID:");
            Console.WriteLine($"{"ID",-10}{"Name",-30}{"Runway Size",-15}{"Max Fixedwing Parking Place",-30}{"Max Rotated Wing Parking Place",-30}");
            foreach (var airport in airports)
            {
                Console.WriteLine($"{airport.id,-10}{airport.name,-30}{airport.runwaySize,-15}{airport.maxFixedwingParkingPlace,-30}{airport.maxRotatedwingParkingPlace,-30}");
            }
        }
    }

    public class Program
    {
        public static int EnterSelectedNumber()
        {
            int selectedNumber = -1;
            try
            {
                selectedNumber = int.Parse(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return selectedNumber;
        }

        public static void FindAirportById(Airports airports, string id)
        {
            int index = airports.airports.FindIndex(f => f.id == id);
            if (index != -1)
            {
                Airport airport = airports.airports[index];
                Console.WriteLine($"{"ID",-10}{"Name",-30}{"Runway Size",-15}{"Max Fixedwing Parking Place",-30}{"Max Rotated Wing Parking Place",-30}");
                Console.WriteLine($"{airport.id,-10}{airport.name,-30}{airport.runwaySize,-15}{airport.maxFixedwingParkingPlace,-30}{airport.maxRotatedwingParkingPlace,-30}");
            }
            else
            {
                Console.WriteLine($"Airport with ID {id} not found.");
            }

        }

        static void Main(string[] args)
        {
            int selectedNumber = -1, subSelectedNumber = -1;
            Fixedwing fixedwing1 = new Fixedwing("FW12345", "Boeing 737", "CAG", 600, 5000, 20000, 300);
            Fixedwing fixedwing2 = new Fixedwing("FW23456", "Airbus A320", "LGR", 700, 6000, 25000, 350);
            Fixedwing fixedwing3 = new Fixedwing("FW34567", "Embraer E190", "PRV", 550, 4500, 18000, 280);

            Helicopter helicopter1 = new Helicopter("RW45678", "Bell 206", "Type1", 300, 2000, 4000, 150);
            Helicopter helicopter2 = new Helicopter("RW56789", "Sikorsky UH-60 Black Hawk", "Type2", 350, 2500, 4500, 180);
            Helicopter helicopter3 = new Helicopter("RW67890", "Eurocopter AS350", "Type3", 320, 2200, 4200, 160);

            Airports airports = new Airports();
            while (true)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine("1.Input data from keyboard.");
                Console.WriteLine("2.Airport management.");
                Console.WriteLine("3.Fixed wing airplane management.");
                Console.WriteLine("4.Helicopter management group.");
                Console.WriteLine("0. Close program.");
                Console.WriteLine("------------------------");
                Console.Write("Enter your selected number: ");
                selectedNumber = EnterSelectedNumber();
                switch (selectedNumber)
                {
                    case 1:
                        Console.WriteLine("Add an aiport: ");
                        airports.AddAirport();
                        break;
                    case 2:
                        Console.WriteLine("------------------------");
                        Console.WriteLine("1. Display list of all airport information, sorted by airport ID.");
                        Console.WriteLine("2. Add to an airport one or more Fixedwing airplane(s) which currently does not participate to an airport.");
                        Console.WriteLine("3. Remove one or more Fixedwing airplane(s) from an airport.");
                        Console.WriteLine("4. Add to an airport one or more helicopter(s) which currently does not participate to an airport.");
                        Console.WriteLine("5. Remove one or more helicopter(s) from an airport.");
                        Console.WriteLine("6. Remove an airport by ID.");
                        Console.WriteLine("------------------------");
                        Console.Write("Enter your selected number: ");
                        subSelectedNumber = EnterSelectedNumber();
                        switch (subSelectedNumber)
                        {
                            case 1:
                                airports.ShowAirports();
                                break;
                            case 2:

                                break;
                            case 3:

                                break;
                            case 4:

                                break;
                            case 5:
                                break;
                            case 6:
                                Console.Write("Enter an airport ID that you want to delete: ");
                                string id = Console.ReadLine();
                                airports.RemoveAirportByID(id);
                                break;
                            default:
                                Console.WriteLine("Invalid selection, please re-enter your selected number!");
                                break;
                        }
                        break;
                    case 3:
                        Console.Write("Enter an airport ID that you want to find: ");
                        string foundId = Console.ReadLine();
                        Console.WriteLine($"Display the status of one airport, ID: {foundId}");
                        FindAirportById(airports, foundId);
                        break;
                    case 4:
                        Console.WriteLine("Fixed wing airplane management");
                        break;
                    case 5:
                        Console.WriteLine("Helicopter management group");
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Invalid selection, please re-enter your selected number!");
                        break;
                }
                if (selectedNumber == 0)
                {
                    break;
                }
            }
        }
    }
}