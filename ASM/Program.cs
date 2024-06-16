using System;

class Program
{
    const int Max_Customers = 100;
    static string[] customerNames = new string[Max_Customers];
    static int[] lastMonthReadings = new int[Max_Customers];
    static int[] thisMonthReadings = new int[Max_Customers];
    static string[] customerTypes = new string[Max_Customers];
    static int[] numberOfPeople = new int[Max_Customers];
    static int[] consumptions = new int[Max_Customers];  
    static double[] vats = new double[Max_Customers];
    static double[] totalWaterBills = new double[Max_Customers];
    static int[] PerCapitaConsumption = new int[Max_Customers];

    static void Main(string[] args)
    {
        int customerCount = 0;
        string continueInput = "yes";

        while (continueInput.ToLower() == "yes" && customerCount < Max_Customers)
        {
            customerCount = GetCustomer(customerCount);

            Console.WriteLine("Do you want to enter details for another customer? (yes/no): ");
            continueInput = Console.ReadLine();
        }

        DisplayCustomer(customerCount);
        Console.ReadLine();
    }
   
    static int GetCustomer(int customerCount)
    {
        Console.WriteLine("Enter customer name: ");
        customerNames[customerCount] = Console.ReadLine();

        Console.WriteLine("Enter last month's water meter readings: ");
        lastMonthReadings[customerCount] = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter this month's water meter readings: ");
        thisMonthReadings[customerCount] = int.Parse(Console.ReadLine());

        while (thisMonthReadings[customerCount] - lastMonthReadings[customerCount] < 0)
        {
            Console.WriteLine("(!) Warning: This month's water meter readings cannot be lower than last month's");
            Console.WriteLine("Enter last month's water meter readings: ");
            lastMonthReadings[customerCount] = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter this month's water meter readings: ");
            thisMonthReadings[customerCount] = int.Parse(Console.ReadLine());
        }


        DisplayMenu();


        int customerTypeSelection = int.Parse(Console.ReadLine());
        customerTypes[customerCount] = GetCustomerType(customerTypeSelection);

        if (customerTypes[customerCount] == null)
        {
            Console.WriteLine("(!) Warning: Invalid selection.");
            return customerCount;
        }

        if (customerTypes[customerCount].ToLower() == "household")
        {
            Console.WriteLine("Enter number of people in the household: ");
            numberOfPeople[customerCount] = int.Parse(Console.ReadLine());
        }
        else
        {
            numberOfPeople[customerCount] = 1;
        }
     

        consumptions[customerCount] = thisMonthReadings[customerCount] - lastMonthReadings[customerCount];
        PerCapitaConsumption[customerCount] = consumptions[customerCount] / numberOfPeople[customerCount];

        CalculateBill(customerCount);

        return customerCount + 1;
    }
    


    static void DisplayMenu()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine();
        Console.WriteLine("================ USER TYPE ================");
        Console.WriteLine("Select type of customer: ");
        Console.WriteLine("1. Household");
        Console.WriteLine("2. Administrative agency");
        Console.WriteLine("3. Production units");
        Console.WriteLine("4. Business services");
        Console.WriteLine("===========================================");
        Console.Write("Enter your selection [1-4] : ");
        Console.ResetColor();
    }

    static string GetCustomerType(int selection)
    {
        switch (selection)
        {
            case 1:
                return "Household";
            case 2:
                return "Administrative agency";
            case 3:
                return "Production units";
            case 4:
                return "Business services";
            default:
                return null;
        }
    }

   

    static void CalculateBill(int customerCount)
    {
        double pricePerM3 = GetPricePerM3(customerCount);
        double waterbill = pricePerM3 * PerCapitaConsumption[customerCount];
        double environmentProtectionFee = waterbill * 0.1;

        vats[customerCount] = waterbill * 0.1;
        totalWaterBills[customerCount] = waterbill + vats[customerCount] + environmentProtectionFee;
    }

    static double GetPricePerM3(int customerCount)
    {
        double pricePerM3 = 0;

        switch (customerTypes[customerCount].ToLower())
        {
            case "household":
                if (PerCapitaConsumption[customerCount] <= 10 * numberOfPeople[customerCount])
                {
                    pricePerM3 = 5.973;
                }
                else if (PerCapitaConsumption[customerCount] <= 20 * numberOfPeople[customerCount])
                {
                    pricePerM3 = 7.052;
                }
                else if (PerCapitaConsumption[customerCount] <= 30 * numberOfPeople[customerCount])
                {
                    pricePerM3 = 8.699;
                }
                else
                {
                    pricePerM3 = 15.929;
                }
                break;
            case "administrative agency":
                pricePerM3 = 9.955;
                break;
            case "production units":
                pricePerM3 = 11.615;
                break;
            case "business services":
                pricePerM3 = 22.068;
                break;
        }

        return pricePerM3;
    }

    static void DisplayCustomer(int customerCount)
    {
        
        for (int i = 0; i < customerCount; i++)
        {
            Console.WriteLine("\nCustomer Name: " + customerNames[i]);
            Console.WriteLine("Last Month's Water Meter Readings: " + lastMonthReadings[i]);
            Console.WriteLine("This Month's Water Meter Readings: " + thisMonthReadings[i]);
            Console.WriteLine("Amount of Consumption: " + PerCapitaConsumption[i]);
            Console.WriteLine("VAT (10%): " + vats[i].ToString("N2") + " VND");
            Console.WriteLine("Total Water Bill (including VAT): " + totalWaterBills[i].ToString("N2") + " VND");
        }
        Console.ReadLine(); 
    }
}
