using System;
using System.IO;

namespace SalesAnalysisProgram
{
    class Program
    {
        static double[] sales = new double[31];
        static string[] dates = new string[31];
        static int countOfEntries = 0;

        static void Main(string[] args)
        {
            DisplayMainMenu();
        }


        static void DisplayMainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Main Menu");
                Console.WriteLine("1. Enter Daily Sales");
                Console.WriteLine("2. Load Sales File");
                Console.WriteLine("3. Save Sales File");
                Console.WriteLine("4. View/Edit Sales Entries");
                Console.WriteLine("5. View Sales Analysis");
                Console.WriteLine("6. Quit");

                int choice = int.Parse("Enter your choice:");

                switch (choice)
                {
                    case 1:
                        countOfEntries = EnterSales(sales, dates);
                        break;
                    case 2:
                        LoadSalesFile();
                        break;
                    case 3:
                        SaveSalesFile();
                        break;
                    case 4:
                        EditEntries();
                        break;
                    case 5:
                        DisplayAnalysisMenu();
                        break;
                    case 6:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void DisplayAnalysisMenu()
        {
            Console.WriteLine("Analysis Menu");
            Console.WriteLine("1. Mean Average Sales");
            Console.WriteLine("2. Median of Sales");
            Console.WriteLine("3. Highest Daily Sales Amount");
            Console.WriteLine("4. Lowest Daily Sales Amount");
            Console.WriteLine("5. Sales Chart for the Current Month");
            Console.WriteLine("6. Back to Main Menu");

            int choice = int.Parse("Enter your choice:");

            switch (choice)
            {
                case 1:
                    double meanAverage = MeanAverageSales(sales, countOfEntries);
                    Console.WriteLine($"Mean Average Sales: {meanAverage:C2}");
                    break;
                case 2:

                    break;
                case 3:
                    int highestIndex = HighestSales(sales, countOfEntries);
                    Console.WriteLine($"Highest Daily Sales Amount: {sales[highestIndex]:C2} on {dates[highestIndex]}");
                    break;
                case 4:
                    int lowestIndex = LowestSales(sales, countOfEntries);
                    Console.WriteLine($"Lowest Daily Sales Amount: {sales[lowestIndex]:C2} on {dates[lowestIndex]}");
                    break;
                case 5:
                    DisplaySalesChart(sales, dates, countOfEntries);
                    break;
                case 6:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }


        static int EnterSales(double[] sales, string[] dates)
        {
            Console.WriteLine("Enter daily sales for the month:");
            for (int i = 0; i < 31; i++)
            {
                Console.Write($"Day {i + 1}: ");
                sales[i] = Convert.ToDouble(Console.ReadLine(""));
                Console.Write($"Date (MM-dd-yyyy): ");
                dates[i] = Console.ReadLine("");
            }
            return 31;
        }


        static void LoadSalesFile()
        {
            Console.Write("Enter the filename to load:");
            string filename = Console.ReadLine("");
            if (File.Exists(filename))
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string line;
                    int index = 0;
                    while ((line = sr.ReadLine()) != null && index < 31)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 2)
                        {
                            dates[index] = parts[0];
                            sales[index] = double.Parse(parts[1]);
                            index++;
                        }
                    }
                    countOfEntries = index;
                    Console.WriteLine("File loaded successfully.");
                }
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }


        static void SaveSalesFile()
        {
            Console.Write("Enter the filename to save:");
            string filename = Console.ReadLine("");
            using (StreamWriter sw = new StreamWriter(filename))
            {
                for (int i = 0; i < countOfEntries; i++)
                {
                    sw.WriteLine($"{dates[i]},{sales[i]}");
                }
                Console.WriteLine("File saved successfully.");
            }
        }


        static void EditEntries()
        {
            Console.WriteLine("Current Sales Entries:");
            for (int i = 0; i < countOfEntries; i++)
            {
                Console.WriteLine($"{dates[i]}: {sales[i]}");
            }

            Console.Write("Enter the index of the entry to edit:");
            int index = int.Parse(Console.ReadLine(""));
            if (index >= 0 && index < countOfEntries)
            {
                Console.WriteLine($"Editing entry for {dates[index]}: {sales[index]}");
                Console.Write("Enter the new sales amount:");
                sales[index] = Convert.ToDouble(Console.ReadLine(""));
            }
            else
            {
                Console.WriteLine("Invalid index.");
            }
        }

        static double MeanAverageSales(double[] sales, int countOfEntries)
        {
            double sum = 0;
            for (int i = 0; i < countOfEntries; i++)
            {
                sum += sales[i];
            }
            return sum / countOfEntries;
        }


        static int HighestSales(double[] sales, int countOfEntries)
        {
            double maxSales = double.MinValue;
            int maxIndex = -1;
            for (int i = 0; i < countOfEntries; i++)
            {
                if (sales[i] > maxSales)
                {
                    maxSales = sales[i];
                    maxIndex = i;
                }
            }
            return maxIndex;
        }


        static int LowestSales(double[] sales, int countOfEntries)
        {
            double minSales = double.MaxValue;
            int minIndex = -1;
            for (int i = 0; i < countOfEntries; i++)
            {
                if (sales[i] < minSales)
                {
                    minSales = sales[i];
                    minIndex = i;
                }
            }
            return minIndex;
        }
        static void DisplaySalesChart(double[] sales, string[] dates, int countOfEntries)
        {
            // Find the maximum sales amount
            double maxSales = double.MinValue;
            for (int i = 0; i < countOfEntries; i++)
            {
                if (sales[i] > maxSales)
                {
                    maxSales = sales[i];
                }
            }


            int maxYValue = (int)Math.Ceiling(maxSales / 50) * 50;

            // Print the chart title
            Console.WriteLine($"=== Sales for the month of {DateTime.Parse(dates[0]).ToString("MMMM")} ===\n");

            // Print the chart
            for (int i = maxYValue; i >= 0; i -= 50)
            {
                Console.Write($"{i,5}|");
                for (int j = 0; j < countOfEntries; j++)
                {
                    if ((int)sales[j] >= i)
                    {
                        Console.Write("   $");
                    }
                    else
                    {
                        Console.Write("    ");
                    }
                }
                Console.WriteLine();
            }

            Console.Write("      -----------------------------------       ------\n");
            Console.Write("Days |");
            for (int i = 0; i < countOfEntries; i++)
            {
                Console.Write($"  {DateTime.Parse(dates[i]).Day.ToString("00")} ");
            }
            Console.WriteLine();
        }
        static double MedianOfSales(double[] sales, int countOfEntries)
        {
            Array.Sort(sales, 0, countOfEntries);
            if (countOfEntries % 2 == 0)
            {
                int mid = countOfEntries / 2;
                return (sales[mid - 1] + sales[mid]) / 2;
            }
            else
            {
                return sales[countOfEntries / 2];
            }
        }
        static void SaveSalesFile();
        static void DisplayEntries(double[] sales, string[] dates, int countOfEntries)
        {
            Console.WriteLine("Sales Entries:");
            Console.WriteLine("Date\t\tSales");
            for (int i = 0; i < countOfEntries; i++)
            {
                Console.WriteLine($"{dates[i]}\t{sales[i]:C2}");
            }
        }
        static void EditEntries()
        {
            DisplayEntries(sales, dates, countOfEntries);
            Console.Write("Enter the index of the entry to edit:");
            int index = int.Parse(Console.ReadLine(""));
            if (index >= 0 && index < countOfEntries)
            {
                Console.WriteLine($"Editing entry for {dates[index]}: {sales[index]:C2}");
                sales[index] = PromptDouble("Enter the new sales amount:");
            }
            else
            {
                Console.WriteLine("Invalid index.");
            }
        }
        static void DisplaySalesChart(double[] sales, string[] dates, int countOfEntries)
        {

            double maxSales = double.MinValue;
            for (int i = 0; i < countOfEntries; i++)
            {
                if (sales[i] > maxSales)
                {
                    maxSales = sales[i];
                }
            }


            int maxYValue = (int)Math.Ceiling(maxSales / 50) * 50;


            Console.WriteLine($"=== Sales for the month of {DateTime.Parse(dates[0]).ToString("MMMM")} ===\n");


            for (int i = maxYValue; i >= 0; i -= 50)
            {
                Console.Write($"{i,5}|");
                for (int j = 0; j < countOfEntries; j++)
                {
                    if ((int)sales[j] >= i)
                    {
                        Console.Write("   $");
                    }
                    else
                    {
                        Console.Write("    ");
                    }
                }
                Console.WriteLine();
            }


            Console.Write("      -----------------------------------       ------\n");
            Console.Write("Days |");
            for (int i = 0; i < countOfEntries; i++)
            {
                Console.Write($"  {DateTime.Parse(dates[i]).Day.ToString("00")} ");
            }
            Console.WriteLine();
        } 
       
