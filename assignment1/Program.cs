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

            //Create an instance of the UserInterface class
            UserInterface userInterface = new UserInterface();

            //Create an instance of the WineItemCollection class
            WineItemCollection wineItemCollection = new WineItemCollection(wineItemCollectionSize);

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
                        userInterface.PrintDatabaseBeverageList(wineItemCollection.PrintDatabaseBeverageList());
                        break;
                    case 2:
                        //Search For An Item
                        
                        //Query user for search string, send return string to the search item method.
                        //returns the found item, if it exists, and prepares to output it to the console.
                        string[] searchBeverage=
                            wineItemCollection.SearchForItem(userInterface.GetSearchQuery());

                        //Display Item Found
                        if(searchBeverage[0]!=null)
                        {
                            userInterface.DisplayItemFound(searchBeverage[1]);
                        }
                        //Display Item not found.
                        else
                        {
                            userInterface.DisplayItemFoundError();
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

                        //Query user for item to be updated, then call then Update method.
                        //If the
                        if (wineItemCollection.UpdateExistingItem(userInterface.GetSearchQuery()))
                        {

                        }
                        break;
                    case 5:
                        //Delete an Existing Item(By ID)

                        //Query the user for id to delete an item, pass it through the delete method.
                        //If it returns true, the beverage was successfully deleted.
                        if (wineItemCollection.DeleteRecord(userInterface.GetSearchQuery()))
                        {
                            userInterface.DisplayDeleteSuccess();
                        }
                        //if not, display error.
                        else
                        {
                            userInterface.DisplayItemFoundError();
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
 * 1)Move functionality to Proper class(es) where it belongs.
 * 2)Add appropriate Exception Handling.
 * 3)Look into using the other methods in WineItemCollection.
 * 4)Rename WIC
 * 5)Work on Update, and Add Methods.
 **/
