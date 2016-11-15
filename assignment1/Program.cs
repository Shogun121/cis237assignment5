//Author: David Barnes
//CIS 237
//Assignment 1
/*
 * The Menu Choices Displayed By The UI
 * 1. Load Wine List From CSV
 * 2. Print The Entire List Of Items
 * 3. Search For An Item
 * 4. Add New Item To The List
 * 5. Exit Program
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set a constant for the size of the collection
            const int wineItemCollectionSize = 4000;

            //Set a constant for the path to the CSV File
            const string pathToCSVFile = "../../../datafiles/winelist.csv";

            //Create an instance of the UserInterface class
            UserInterface userInterface = new UserInterface();

            //Create an instance of the WineItemCollection class
            IWineCollection wineItemCollection = new WineItemCollection(wineItemCollectionSize);

            //Create an instance of the CSVProcessor class
            CSVProcessor csvProcessor = new CSVProcessor();

            //Create an instance of the Entities class
            BeverageRCooleyEntities beverageEntities = new BeverageRCooleyEntities();

            //Display the Welcome Message to the user
            userInterface.DisplayWelcomeGreeting();

            //Display the Menu and get the response. Store the response in the choice integer
            //This is the 'primer' run of displaying and getting.
            int choice = userInterface.DisplayMenuAndGetResponse();

            while (choice != 6)
            {
                switch (choice)
                {
                    case 1:
                        //Print Entire List Of Items

                        //Using a foreach loop print out each item in the database
                        foreach (Beverage beverage in beverageEntities.Beverages)
                        {
                            Console.WriteLine(beverage.id + " " + beverage.name + " " + beverage.pack + " " +
                            beverage.price.ToString("n2") + " " + beverage.active + " ");
                        }
                        break;

                    case 2:
                        //Search For An Item
                        try
                        {
                            Beverage foundBeverage = beverageEntities.Beverages.Find(Console.ReadLine());

                            Console.WriteLine(foundBeverage.id + " " + foundBeverage.name + " " + foundBeverage.pack + " " +
                                foundBeverage.price.ToString("n2") + " " + foundBeverage.active + " ");
                        }
                        //Error Message if ID is invalid
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("ID not found.");
                        }
                        //Notifies user that the query is complete.
                        finally
                        {
                            Console.WriteLine("Search Query Complete.");
                        }                
                        break;

                    case 3:
                        //Add A New Item To The List
                        string[] newItemInformation = userInterface.GetNewItemInformation();
                        if (wineItemCollection.FindById(newItemInformation[0]) == null)
                        {
                            wineItemCollection.AddNewItem(newItemInformation[0], newItemInformation[1], newItemInformation[2]);
                            userInterface.DisplayAddWineItemSuccess();
                        }
                        else
                        {
                            userInterface.DisplayItemAlreadyExistsError();
                        }
                        break;

                    case 4:
                        //Update an existing item(Not ID)

                        break;
                    case 5:
                        //Delete an Existing Item(By ID)
                        try
                        {
                            Console.WriteLine("Enter the ID of the Item you wish to delete.");
                            string deleteString=Console.ReadLine();
                            beverageEntities.Beverages.Remove(beverageEntities.Beverages[deleteString]);

                            
                        }
                        //Error Message if ID is invalid
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("ID not found.");
                        }
                        //Notifies user that the query is complete.
                        finally
                        {
                            Console.WriteLine("Search Query Complete.");
                        }   
                        break;
                }

                //Get the new choice of what to do from the user
                choice = userInterface.DisplayMenuAndGetResponse();
            }

        }
    }
}
/**
 * TO DO LIST
 * 1)Move functionality to UI where it belongs.
 **/
