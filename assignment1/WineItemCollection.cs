//Author: David Barnes
//CIS 237
//Assignment 1
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment1
{
    class WineItemCollection : IWineCollection
    {
        //Instantiate Entities class
        BeverageRCooleyEntities beverageEntities;

        //Private Variables
        WineItem[] wineItems;
        int wineItemsLength;

        //Constuctor. Must pass the size of the collection.
        public WineItemCollection(int size)
        {
            wineItems = new WineItem[size];
            wineItemsLength = 0;
        }

        //Add a new item to the collection
        public void AddNewItem(string id, string description, string pack)
        {
            //Add a new WineItem to the collection. Increase the Length variable.
            wineItems[wineItemsLength] = new WineItem(id, description, pack);
            wineItemsLength++;
        }
        
        //Get The Print String Array For All Items
        public string[] GetPrintStringsForAllItems()
        {
            //Create and array to hold all of the printed strings
            string[] allItemStrings = new string[wineItemsLength];
            //set a counter to be used
            int counter = 0;

            //If the wineItemsLength is greater than 0, create the array of strings
            if (wineItemsLength > 0)
            {
                //For each item in the collection
                foreach (WineItem wineItem in wineItems)
                {
                    //if the current item is not null.
                    if (wineItem != null)
                    {
                        //Add the results of calling ToString on the item to the string array.
                        allItemStrings[counter] = wineItem.ToString();
                        counter++;
                    }
                }
            }
            //Return the array of item strings
            return allItemStrings;
        }

        //Find an item by it's Id
        public string FindById(string id)
        {
            //initialzie the Entities class
            beverageEntities = new BeverageRCooleyEntities();
            //Declare return string for the possible found item
            string returnString = null;

            //For each WineItem in wineItems
            foreach (Beverage beverage in beverageEntities.Beverages)
            {
                //If the beverage is not null
                if (beverage != null)
                {
                    //if the beverage Id is the same as the search id
                    if (beverage.id == id)
                    {
                        //Set the return string to the result of the beverage's ToString method
                        returnString = beverage.ToString();
                    }
                }
            }
            //Return the returnString
            return returnString;
        }
        /// <summary>
        /// Method to obtain all the beverage records in the database and write them to 
        /// //an array to be passed to the UI to be printed out.
        /// </summary>
        /// <returns>beverage array</returns>
        public Beverage[] PrintDatabaseBeverageList()
        {
            //Initialize the EF class.
            beverageEntities = new BeverageRCooleyEntities();

            //Create the beverage array.
            Beverage[] beverageArray=new Beverage[1000];
            int IndexCounter=0;

            //Store each beverage in an array to be passed to the UI.
            foreach (Beverage beverage in beverageEntities.Beverages)
            {
                beverageArray[IndexCounter] = beverage;
                IndexCounter++;
            }
            return beverageArray;
        }
        /// <summary>
        /// Method used to delete a record from the database of wine items.
        /// </summary>
        public bool DeleteRecord(string Input)
        {
            bool success = false;

            //initializing instantiation of the EF class.
            beverageEntities = new BeverageRCooleyEntities();

            //Search through database for given ID
            if (FindById(Input)==null)
            {
                //Find a record based the PK.
                Beverage beverageToDelete = beverageEntities.Beverages.Find(Input);

                //Delete the record assuming the PK is good.
                beverageEntities.Beverages.Remove(beverageToDelete);

                //Save the changes
                beverageEntities.SaveChanges();

                //return success
                return success = true;
            }
            return success;         
        }
        /// <summary>
        /// Searches the Beverage database for the index provided.
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public string[] SearchForItem(string Input)
        {
            //Initialize the EF class.
            beverageEntities = new BeverageRCooleyEntities();

            //call SearchById to search the beverage database.
            FindById(Input);
            //search for a beverage using Find with user input being the search parameter.
            Beverage foundBeverage = beverageEntities.Beverages.Find(Input);

            //Return found item
            return new string[] { foundBeverage.id, foundBeverage.name, foundBeverage.pack, foundBeverage.price.ToString() };

            //try
            //{
                
            //}
            ////Error Message if ID is invalid
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //    Console.WriteLine("ID not found.");
            //}
            ////Notifies user that the query is complete.
            //finally
            //{
            //    Console.WriteLine("Search Query Complete.");
            //}
        }
        /// <summary>
        /// Method used to update an existing item except the PK.
        /// </summary>
        public void UpdateExistingItem(string Input)
        {
            //variable to store the incoming data
            string inputString;

            //initialization
            beverageEntities = new BeverageRCooleyEntities();

            //Prompt user for Input
            Console.WriteLine("Update an item, via given ID");

            //Acquire input
            inputString = Console.ReadLine();

            if(inputString!=null)
            {
                if()
                {

                }
            }

            //Search for the wine item using the input as a search parameter.
            Beverage updateBeverage = beverageEntities.Beverages.Find(inputString);

            try
            {
                ////Search for the wine item using the input as a search parameter.
                //updateBeverage = beverageEntities.Beverages.Find(inputString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Search for Item failed.");
            }
            
            //Wine item found, and announced.
            Console.WriteLine("This beverage will be updated.");
            //Pre-Modification data.
            Console.WriteLine(updateBeverage.id + " " + updateBeverage.name + " " + updateBeverage.pack + " " +
                            updateBeverage.price.ToString("n2") + " " + updateBeverage.active + " ");
            //BEGIN alterations
            updateBeverage.name = "[wine name]";
            updateBeverage.pack = "12";
            updateBeverage.price = 120;
            updateBeverage.active = true;

            //save updates
            beverageEntities.SaveChanges();

            //Search for modified item
            beverageEntities.Beverages.Find(inputString);

            //Display updated item
            Console.WriteLine("This is the wine item post-change.");
            Console.WriteLine(updateBeverage.id + " " + updateBeverage.name + " " + updateBeverage.pack + " " +
                            updateBeverage.price.ToString("n2") + " " + updateBeverage.active + " ");

        }
    }
}
