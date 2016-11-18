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
            foreach (WineItem wineItem in wineItems)
            {
                //If the wineItem is not null
                if (wineItem != null)
                {
                    //if the wineItem Id is the same as the search id
                    if (wineItem.Id == id)
                    {
                        //Set the return string to the result of the wineItem's ToString method
                        returnString = wineItem.ToString();
                    }
                }
            }
            //Return the returnString
            return returnString;
        }
        public void SearchForItem()
        {
            beverageEntities = new BeverageRCooleyEntities();
            try
            {
                //search for a beverage using Find with user input being the search parameter.
                Beverage foundBeverage = beverageEntities.Beverages.Find(Console.ReadLine());

                //Display found item
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
        }
    }
}
