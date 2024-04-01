using System;
using System.IO;

/// <summary>
/// Assignment 3
/// 
/// Author: Fatima Khalif
/// Date: 
/// Purpose: Allows user to enter/save/load/edit/view daily sales values
///          from a file. Allows and displays simple data analysis
///          (mean/max/min/graph) of sales values for a given month.
/// </summary>

class Program
{
    static string mainMenuChoice;
    static string analysisMenuChoice;
    static bool displayMainMenu = true;
    static bool displayAnalysisMenu;
    static bool quit;
    // TODO: declare a constant to represent the max size of the sales
    // and dates arrays. The arrays must be large enough to store
    // sales for an entire month.
    const int maxSize = 31;
    // TODO: create a double array named 'sales', use the max size constant you declared
    // above to specify the physical size of the array.
    static double[] sales = new double[maxSize];

    // TODO: create a string array named 'dates', use the max size constant you declared
    // above to specify the physical size of the array.
    static string[] dates = new string[maxSize];

    static string month;
    static string year;
    static string filename;
    static int count = 0;
    static bool proceed;
    static double mean;
    static double largest;
    static double smallest;

    static void Main(string[] args)
    {
        DisplayProgramIntro();

        // TODO: call the DisplayMainMenu method
        DisplayMainMenu();

        while (displayMainMenu)
        {
            mainMenuChoice = Prompt("Enter MAIN MENU option ('D' to display menu): ").ToUpper();
            Console.WriteLine();

            //MAIN MENU Switch statement
            switch (mainMenuChoice)
            {
                case "N": //[N]ew Daily Sales Entry

                    proceed = NewEntryDisclaimer();

                    if (proceed)
                    {
                        // TODO: uncomment the following and call the EnterSales method below
                        count = EnterSales();
                        Console.WriteLine();
                        Console.WriteLine($"Entries completed. {count} records in temporary memory.");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Cancelling new data entry. Returning to MAIN MENU.");
                    }
                    break;
                case "S": //[S]ave Entries to File
                    if (count == 0)
                    {
                        Console.WriteLine("Sorry, LOAD data or enter NEW data before SAVING.");
                    }
                    else
                    {
                        proceed = SaveEntryDisclaimer();

                        if (proceed)
                        {
                            filename = PromptForFilename();
                            // TODO: call the SaveSalesFile method here
                            SaveSalesFile();

                        }
                        else
                        {
                            Console.WriteLine("Cancelling save operation. Returning to MAIN MENU.");
                        }
                    }
                    break;
                case "E": //[E]dit Sales Entries
                    if (count == 0)
                    {
                        Console.WriteLine("Sorry, LOAD data or enter NEW data before EDITING.");
                    }
                    else
                    {
                        proceed = EditEntryDisclaimer();

                        if (proceed)
                        {
                            // TODO: call the EditEntries method here
                            EditEntries();

                        }
                        else
                        {
                            Console.WriteLine("Cancelling EDIT operation. Returning to MAIN MENU.");
                        }
                    }
                    break;
                case "L": //[L]oad Sales File
                    proceed = LoadEntryDisclaimer();
                    if (proceed)
                    {
                        filename = Prompt("Enter name of file to load: ");
                        // TODO: uncomment the following and call the LoadSalesFile method below
                        count = LoadSalesFile();
                        Console.WriteLine($"{count} records were loaded.");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Cancelling LOAD operation. Returning to MAIN MENU.");
                    }
                    break;
                case "V":
                    if (count == 0)
                    {
                        Console.WriteLine("Sorry, LOAD data or enter NEW data before VIEWING.");
                    }
                    else
                    {
                        // TODO: call the DisplayEntries method here
                        DisplayEntries();

                    }
                    break;
                case "M": //[M]onthly Statistics
                    if (count == 0)
                    {
                        Console.WriteLine("Sorry, LOAD data or enter NEW data before ANALYSIS.");
                    }
                    else
                    {
                        displayAnalysisMenu = true;
                        while (displayAnalysisMenu)
                        {
                            // TODO: call the DisplayAnalysisMenu here
                            DisplayAnalysisMenu();


                            analysisMenuChoice = Prompt("Enter ANALYSIS sub-menu option: ").ToUpper();
                            Console.WriteLine();

                            switch (analysisMenuChoice)
                            {
                                case "A": //[A]verage Sales
                                          // TODO: uncomment the following and call the Mean method below
                                    mean = Mean(sales, count);
                                    month = dates[0].Substring(0, 3);
                                    year = dates[0].Substring(7, 4);
                                    Console.WriteLine($"The mean sales for {month} {year} is: {mean:C}");
                                    Console.WriteLine();
                                    break;
                                case "H": //[H]ighest Sales
                                          // TODO: uncomment the following and call the Largest method below
                                    largest = Largest(sales);
                                    month = dates[0].Substring(0, 3);
                                    year = dates[0].Substring(7, 4);
                                    Console.WriteLine($"The largest sales for {month} {year} is: {largest:C}");
                                    Console.WriteLine();
                                    break;
                                case "L": //[L]owest Sales
                                          // TODO: uncomment the following and call the Smallest method below
                                    smallest = Smallest(sales, count);
                                    month = dates[0].Substring(0, 3);
                                    year = dates[0].Substring(7, 4);
                                    Console.WriteLine($"The smallest sales for {month} {year} is: {smallest:C}");
                                    Console.WriteLine();
                                    break;
                                case "G": //[G]raph Sales
                                          // TODO: call the DisplayChart method below
                                    DisplayChart();
                                    Prompt("Press <enter> to continue...");
                                    break;
                                case "R": //[R]eturn to MAIN MENU
                                    displayAnalysisMenu = false;
                                    break;
                                default: //invalid entry. Reprompt.
                                    Console.WriteLine("Invalid reponse. Enter one of the letters to choose a submenu option.");
                                    break;
                            }
                        }
                    }
                    break;
                case "D": //[D]isplay Main Menu
                          // TODO: call the DisplayMainMenu method
                    DisplayMainMenu();

                    break;
                case "Q": //[Q]uit Program
                    quit = Prompt("Are you sure you want to quit (y/N)? ").ToLower().Equals("y");
                    Console.WriteLine();
                    if (quit)
                    {
                        displayMainMenu = false;
                    }
                    break;
                default: //invalid entry. Reprompt.
                    Console.WriteLine("Invalid reponse. Enter one of the letters to choose a menu option.");
                    break;
            }
        }

        DisplayProgramOutro();
    }

    // ================================================================================================ //
    //                                                                                                  //
    //                                              METHODS                                             //
    //                                                                                                  //
    // ================================================================================================ //

    // ++++++++++++++++++++++++++++++++++++ Difficulty 1 ++++++++++++++++++++++++++++++++++++

    // TODO: create the Prompt method
    static string Prompt(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }



    // TODO: create the PromptDouble method
    // The method must always return a double and should not crash the program.
    static double PromptDouble(string message)
    {
        double result;
        while (!double.TryParse(Prompt(message), out result))
        {
            Console.WriteLine("Please enter a valid number.");
        }
        return result;
    }



    // TODO: create the DisplayMainMenu method
    // the menu must consist of the following options:

    static void DisplayMainMenu()
    {
        Console.WriteLine("[N] ew Daily Sales Entry");
        Console.WriteLine("[S] ave Entries to File");
        Console.WriteLine("[E] dit Sales Entries");
        Console.WriteLine(" [L] oad Sales File");
        Console.WriteLine("[V] iew Entered/Loaded Sales");
        Console.WriteLine("[M] onthly Statistics");
        Console.WriteLine("[D]isplay Main Menu");
        Console.WriteLine("[Q] uit Program");
    }


    // TODO: create the DisplayAnalysisMenu method
    // the menu must consist of the following options:
    static void DisplayAnalysisMenu()
    {


        Console.WriteLine("[A] verage Sales");
        Console.WriteLine("[H]ighest Sales");
        Console.WriteLine("[L] owest Sales");
        Console.WriteLine("[G]raph Sales");
        Console.WriteLine("[R] eturn to MAIN MENU");
    }


    // TODO: create the Largest method

    static double Largest(double[] sales)
    {
        double largest = double.MinValue;
        for (int i = 0; i < sales.Length; i++)
        {
            if (sales[i] > largest)
            {
                largest = sales[i];
            }
        }
        return largest;
    }


    // TODO: create the Smallest method
    static double Smallest(double[] sales, int count)
    {
        double smallest = double.MaxValue;
        for (int i = 0; i < count; i++)
        {
            if (sales[i] < smallest)
            {
                smallest = sales[i];
            }
        }
        return smallest;
    }


    // TODO: create the Mean method
    static double Mean(double[] sales, int count)
    {
        double sum = 0;
        for (int i = 0; i < count; i++)
        {
            sum += sales[i];
        }
        return count > 0 ? sum / count : 0;
    }


    // ++++++++++++++++++++++++++++++++++++ Difficulty 2 ++++++++++++++++++++++++++++++++++++


    // COMPLETED: create the DisplayEntries method
    static void DisplayEntries()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("            Entered/Loaded Sales        ");
        Console.WriteLine("========================================");
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"{dates[i]}, {sales[i]:C}");
        }
        Console.WriteLine();
    }

    // COMPLETED: create the EnterSales method
    static int EnterSales()
    {

        Array.Clear(dates, 0, dates.Length);
        Array.Clear(sales, 0 , dates.Length);

        int year;

        do
        {
            year = PromptInt("Enter sales year: ");

            if (year > 2024 || year < 2010)
            {
                Console.WriteLine("Invalid year range provided, year must be between 2010 and 2024.");
                year = 0;
            }
        } while (year == 0);



        string month;
        string[] calendarMonth = {"JAN","FEB","MAR","APR","MAY","JUN","JUL","AUG","SEP","OCT","NOV", "DEC" };
        bool monthIsValid;

        do
        {
            monthIsValid = false;
            month = Prompt("Enter sales month: ").ToUpper();
            foreach (var mth in calendarMonth)
            {
                if(mth == month)
                {
                    monthIsValid = true;
                }
            }

            if(monthIsValid == false)
            {
                Console.Write("Invalid month provided, month must be in its 3-Abbrevated letters e.g. JAN, FEB, MAR ...");
            }

        } while (monthIsValid == false);


        

        int index = 0;
        bool continueEntering = true;
        while (continueEntering && index < maxSize)
        {
            Console.WriteLine($"Enter sales for day {index + 1} (or '-1' to exit): ");
            string input = Console.ReadLine().Trim();
            if (input.ToLower() == "-1")
            {
                continueEntering = false;
            }
            else if (double.TryParse(input, out double sale))
            {
                sales[index] = sale;
                dates[index] = $"{month}-{(index+1).ToString().PadLeft(2,'0')}-{year}";   //DateTime.Now.AddDays(index).ToString("MM/dd/yyyy");      //FEB-01-2024
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid sales value or '-1' to exit.");
            }
            index ++;
        }

        return index - 1;
    }

    // COMPLETED: create the LoadSalesFile method
    static int LoadSalesFile()
    {

        Array.Clear(dates);
        Array.Clear(sales);

        try
        {
            // Assuming the file contains sales data in the format: Date, Sales
            string[] lines = File.ReadAllLines(filename);
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].ToString().Split(',');
                dates[i - 1] = parts[0].Trim();
                sales[i-1] = double.Parse(parts[1]);
            }
            return lines.Length - 1;
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"File '{filename}' not found.");
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading the file: {ex.Message}");
            return 0;
        }
    }

    // COMPLETED: create the SaveSalesFile method
    static void SaveSalesFile()
    {


        File.WriteAllText(filename, "Dates,Sales" + Environment.NewLine);  //clear all content in previous file if previously existed.
      

        try
        {
            using (StreamWriter writer = new (filename, true))
            {
                for (int i = 0; i < count; i++)
                {
                    writer.WriteLine($"{dates[i]}, {sales[i]}");
                }
            }
            Console.WriteLine($"Data saved to '{filename}' successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while saving the file: {ex.Message}");
        }
    }



    // ++++++++++++++++++++++++++++++++++++ Difficulty 3 ++++++++++++++++++++++++++++++++++++

    // TODO: create the EditEntries method

    static void EditEntries()
    {
        Console.WriteLine("Which entry would you like to edit?");
        int entryNumber = PromptInt("Enter the entry number: ") - 1;
        if (entryNumber >= 0 && entryNumber < count)
        {
            Console.WriteLine($"Current Date: {dates[entryNumber]}, Current Sales: {sales[entryNumber]:C}");
            sales[entryNumber] = PromptDouble("Enter the new sales value: ");
            Console.WriteLine("Edit successful!");
        }
        else
        {
            Console.WriteLine("Invalid entry number.");
        }
    }


    // ++++++++++++++++++++++++++++++++++++ Difficulty 4 ++++++++++++++++++++++++++++++++++++

    static void DisplayChart()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("=            Sales Chart               =");
        Console.WriteLine("========================================");
        Console.WriteLine();

        // Display headers
        Console.WriteLine("Day\t\tSales");
        Console.WriteLine("----------------------------------------");

        // Display sales data
        int count = 0;
        var highestSales = Largest(sales);

        string[] data = new string[((int)highestSales / 50) + 1];

        //draw y axis
        string yData;
        Console.WriteLine("Dollars");
        for (int i = data.Length; i >= 0; i--)
        {
            //Console.WriteLine($"{i * 50,7}|");

            yData = $"{i * 50,7}|";
            for (int x = 0; x < dates.Length; x++)
            {
                if (dates[x] != null)
                {
                    if ( i * 50 >= sales[x] && sales[x] > (i * 50) - 50)
                    {
                        yData += $"\t{sales[x]}";
                    }
                    else
                    {
                        yData += $"\t";
                    }
                }
            }
            Console.WriteLine(yData);
        }


        //draw x axis
        int totalRecords = 0;
        string xAxis = "Days   |";
        for (int i = 0; i < sales.Length; i++)
        {
            if (dates[i] != null)
            {
                xAxis += $"\t{(i + 1).ToString().PadLeft(2, '0')}";
                totalRecords++;
            }
        }
        Console.WriteLine($"-".PadLeft(xAxis.Length + 8 + (totalRecords * 5), '-'));
        Console.WriteLine(xAxis);

        Console.WriteLine();
    }


    // NOTE: Many of the following methods depend on the Prompt method and will operate correctly once
    // that method has been implemented.

    /// <summary>
    /// Displays the Program intro.
    /// </summary>
    static void DisplayProgramIntro()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("=                                      =");
        Console.WriteLine("=            Monthly  Sales            =");
        Console.WriteLine("=                                      =");
        Console.WriteLine("========================================");
        Console.WriteLine();
    }

    /// <summary>
    /// Displays the Program outro.
    /// </summary>
    static void DisplayProgramOutro()
    {
        Console.Write("Program terminated. Press ENTER to exit program...");
        Console.ReadLine();
    }

    /// <summary>
    /// Displays a disclaimer for NEW entry option.
    /// </summary>
    /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
    static bool NewEntryDisclaimer()
    {
        bool response;
        Console.WriteLine("Disclaimer: proceeding will overwrite all unsaved data.");
        Console.WriteLine("Hint: Select EDIT from the main menu instead, to change individual days.");
        Console.WriteLine("Hint: You'll need to enter data for the whole month.");
        Console.WriteLine();
        response = Prompt("Do you wish to proceed anyway? (y/N) ").ToLower().Equals("y");
        Console.WriteLine();
        return response;
    }

    /// <summary>
    /// Displays a disclaimer for SAVE entry option.
    /// </summary>
    /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
    static bool SaveEntryDisclaimer()
    {
        bool response;
        Console.WriteLine("Disclaimer: saving to an EXISTING file will overwrite data currently on that file.");
        Console.WriteLine("Hint: Files will be saved to this program's directory by default.");
        Console.WriteLine("Hint: If the file does not yet exist, it will be created.");
        Console.WriteLine();
        response = Prompt("Do you wish to proceed anyway? (y/N) ").ToLower().Equals("y");
        Console.WriteLine();
        return response;
    }

    /// <summary>
    /// Displays a disclaimer for EDIT entry option.
    /// </summary>
    /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
    static bool EditEntryDisclaimer()
    {
        bool response;
        Console.WriteLine("Disclaimer: editing will overwrite unsaved sales values.");
        Console.WriteLine("Hint: Save to a file before editing.");
        Console.WriteLine();
        response = Prompt("Do you wish to proceed anyway? (y/N) ").ToLower().Equals("y");
        Console.WriteLine();
        return response;
    }

    /// <summary>
    /// Displays a disclaimer for LOAD entry option.
    /// </summary>
    /// <returns>Boolean, if user wishes to proceed (true) or not (false).</returns>
    static bool LoadEntryDisclaimer()
    {
        bool response;
        Console.WriteLine("Disclaimer: proceeding will overwrite all unsaved data.");
        Console.WriteLine("Hint: If you entered New Daily sales entries, save them first!");
        Console.WriteLine();
        response = Prompt("Do you wish to proceed anyway? (y/N) ").ToLower().Equals("y");
        Console.WriteLine();
        return response;
    }

    /// <summary>
    /// Displays prompt for a filename, and returns a valid filename. 
    /// Includes exception handling.
    /// </summary>
    /// <returns>User-entered string, representing valid filename (.txt or .csv)</returns>
    static string PromptForFilename()
    {
        string filename;
        bool isValidFilename;
        const string CsvFileExtension = ".csv";
        const string TxtFileExtension = ".txt";

        do
        {
            filename = Prompt("Enter name of .csv or .txt file to save to (e.g. JAN-2024-sales.csv): ");
            if (filename == "")
            {
                isValidFilename = false;
                Console.WriteLine("Please try again. The filename cannot be blank or just spaces.");
            }
            else
            {
                if (!filename.EndsWith(CsvFileExtension) && !filename.EndsWith(TxtFileExtension)) //if filename does not end with .txt or .csv.
                {
                    filename = filename + CsvFileExtension; //append .csv to filename
                    Console.WriteLine("It looks like your filename does not end in .csv or .txt, so it will be treated as a .csv file.");
                    isValidFilename = true;
                }
                else
                {
                    Console.WriteLine("It looks like your filename ends in .csv or .txt, which is good!");
                    isValidFilename = true;
                }
            }
        } while (!isValidFilename);
        return filename;
    }

    static int PromptInt(string message)
    {
        int result;
        while (!int.TryParse(Prompt(message), out result))
        {
            Console.WriteLine("Please enter a valid integer.");
        }
        return result;
    }
}


