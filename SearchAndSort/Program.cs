using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

// Created By: Daniel Swain.
// Date: 11/09/2016.
//
// A search and sort assignment for Tafe using C#, using Merge Sort and Binary Search.
namespace SearchAndSort
{
    class Program
    {
        // Program variables
        static string welcomeMessage = "Welcome, please pick an option from the list below.\n";
        static string[] options = { "Enter an array.", "Sort using mergeSort.", "Search for a number in the array.", "Exit."};
        static bool repeat = true;
        static string originalNumbers;
        static List<int> numbers = new List<int>();
        
        static void Main(string[] args)
        {
            // Greet the user and display the program options for them until the user specifically chooses to exit the program.
            Console.WriteLine(welcomeMessage);
            // Loop the options forever until the user chooses to exit.
            while (repeat)
            {
                // Print the options for the user.
                printOptions();
                // Get and handle the input from the user.
                getInput();
            }
        }

        // Print the options for the user to pick from.
        static void printOptions()
        {
            int count = 1;
            foreach (string option in options)
            {
                Console.WriteLine("{0}: {1}", count, option);
                count++;
            }
        }

        // Get the user's input choice and handle the input.
        static void getInput()
        {
            // Print out helper text to prompt the user for the input.
            Console.Write("\nChoice: ");
            string inputString = Console.ReadLine();
            // Print out a blank line for formatting.
            Console.WriteLine();

            // Parse the inputString to an int and handle the input.
            switch (inputString)
            {
                case "1":
                    // User wishes to add an array to memory.
                    initialiseArray();
                    break;

                case "2":
                    // Sort the array using mergeSort.
                    sortArray();
                    break;

                case "3":
                    // Search for a number in the array using binary search.
                    searchArray();
                    break;

                case "4":
                    // User wishes to exit the program, show the choice and then exit the program by setting repeat to false.
                    exitProgram();
                    break;

                default:
                    // Unable to identify the users choice.
                    Console.WriteLine("Please enter a number representing the option you wish to complete.");
                    break;
            }
        }

        // Helper method to get the user's array from the console.
        static void initialiseArray()
        {
            Console.WriteLine("Please enter space separated numbers for the array (i.e. 1 2 3 4 5 6 will become [1, 2, 3, 4, 5, 6])\n");
            // Get the array string
            originalNumbers = Console.ReadLine();
            // Parse this into our numbers list.
            numbers = new List<int>(Array.ConvertAll(originalNumbers.Split(), int.Parse));
            // Let the user know it was added by showing them their numbers list.
            Console.WriteLine("\nThe array you entered was: [" + String.Join(", ", numbers) + "]\n");
            Console.WriteLine("If this wasn't what you entered, please try again, remembering to space separate your numbers.\n");
        }

        // Helper method to sort the array and print the sorted output.
        static void sortArray()
        {
            // No array, warn the user.
            if (!isThereArray())
            {
                Console.WriteLine("There's currently no array stored, please pick option 1 and add an array before trying to sort.\n");
                return;
            }

            // Show the old array.
            // Let the user know it was added by showing them their numbers list. Use the originalNumbers string so the initial array is shown.
            Console.WriteLine("\nThe array you entered was: [" + String.Join(", ", new List<int>(Array.ConvertAll(originalNumbers.Split(), int.Parse))) + "]");

            // There is an array so lets sort it.
            mergeSort(numbers);

            // Show the sorted list.
            Console.WriteLine("\nThe sorted array is: [" + String.Join(", ", numbers) + "]\n");

        }

        // Helper method to search for an number in the user's array.
        static void searchArray()
        {
            // No array, warn the user.
            if (!isThereArray())
            {
                Console.WriteLine("There's currently no array stored, please pick option 1 and add an array before trying to search.\n");
                return;
            }

            // There is an array so lets sort it and handle the user searching for a number
            mergeSort(numbers);

            // Get the user's number that they want to find.
            Console.WriteLine("Please enter the number you want to search for in the list.");

            // Get the user's desired node data.
            Console.Write("\nNumber to find: ");

            // Parse the user's input into an integer, otherwise return a warning.
            int usersKeyInput = parseUsersInputToInt(Console.ReadLine());
            if (usersKeyInput != -1)
            {
                // The input was a valid string integer representation and could be parsed.
                
            }
            else
            {
                // Couldn't parse the number into an int.
                Console.WriteLine("\nCouldn't get a valid number from what you entered.\n");
            }
        }

        // The user has chosen to exit the program, handle this action in this method.
        static void exitProgram()
        {
            Console.WriteLine("Exiting...");
            // Use a delay to allow the application to notify the user.
            Thread.Sleep(1000);
            // Set the repeat variable to false so the while loop running the program will exit.
            repeat = false;
        }
        
        // Check if the user has initialised the array or not.
        static bool isThereArray()
        {
            if (numbers.Count > 0)
            {
                return true;
            }

            return false;
        }

        // Worker that sorts the List using the Merge Sort Algorithm.
        static void mergeSort(List<int> alist)
        {
            if (alist.Count() > 1)
            {
                // Get the middle of the list and split into two sublists
                double mid = alist.Count() / 2;
                int middleOfList = (int) Math.Floor(mid);

                // Left list, split to the middle
                List<int> leftList = new List<int>();
                for (int x = 0; x < middleOfList; x++)
                {
                    leftList.Add(alist[x]);
                }

                // Right list, split from the middle
                List<int> rightList = new List<int>();
                for (int y = middleOfList; y < alist.Count(); y++)
                {
                    rightList.Add(alist[y]);
                }

                // Recursively split, sort and merge the sublists.
                mergeSort(leftList);
                mergeSort(rightList);

                // Reset/set the counters for the indexes for iterating through the sublists.
                int i = 0;
                int j = 0;
                int k = 0;

                // While both lists are present, compare elements from the left list to the right list
                // and store the smaller element in the combined list as the two left and right lists
                // will be sorted.
                while (i < leftList.Count() && j < rightList.Count())
                {
                    if (leftList[i] < rightList[j])
                    {
                        alist[k] = leftList[i];
                        i += 1;
                    }
                    else
                    {
                        alist[k] = rightList[j];
                        j += 1;
                    }
                    k += 1;
                }

                // We only have the left list remaining, so as it's a sub list it's already sorted so put
                // it in the combined list.
                while (i < leftList.Count())
                {
                    alist[k] = leftList[i];
                    i += 1;
                    k += 1;
                }

                // We only have the right list remaining... (see above loop).
                while (j < rightList.Count())
                {
                    alist[k] = rightList[j];
                    j += 1;
                    k += 1;
                }
            }
        }

        // Helper class to parse the user's input and return an int if possible.
        static int parseUsersInputToInt(string inputString)
        {
            int inputNumber = 0;
            if (Int32.TryParse(inputString, out inputNumber))
            {
                // input string could be parsed to a number, return it to the calling method.
                return inputNumber;
            }
            else
            {
                // Input string couldn't be parsed to a number, return -1 to the calling method.
                return -1;
            }
        }
    }
}
