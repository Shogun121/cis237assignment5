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
        Beverage[] beverageArray;
        int beverageItemLength;

        //Constuctor. Must pass the size of the collection.
        public WineItemCollection(int size)
        {
            beverageArray = new Beverage[size];
            beverageItemLength = 0;
        }

        //Add a new item to the collection
        public void AddNewItem(string id, string name, string pack,string price, string active)
        {
            decimal placeholder;

            //Add Method on EF class to update when a new beverage is added
            Beverage addBeverage=beverageEntities.Beverages.Find(id);
            //ADD a TRY CATCH to handle improper input
            try
            {
                if (addBeverage.id != null)
                {
                    addBeverage.id = id;
                    addBeverage.name = name;
                    addBeverage.pack = pack;
                    decimal.TryParse(price, out placeholder);
                    addBeverage.price = placeholder;
                    if (active == "T")
                    {
                        addBeverage.active = true;
                    }
                    else
                    {
                        addBeverage.active = false;
                    }

                    beverageEntities.Beverages.Add(addBeverage);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            

            
        }
        
        //Get The Print String Array For All Items
        public string[] GetPrintStringsForAllItems()
        {
            //Create and array to hold all of the printed strings
            string[] allItemStrings = new string[beverageItemLength];
            //set a counter to be used
            int counter = 0;

            //If the wineItemsLength is greater than 0, create the array of strings
            if (beverageItemLength > 0)
            {
                //For each item in the collection
                foreach (Beverage beverage in beverageArray)
                {
                    //if the current item is not null.
                    if (beverage != null)
                    {
                        //Add the results of calling ToString on the item to the string array.
                        allItemStrings[counter] = beverage.ToString();
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
            foreach (Beverage beverage in beverageArray)
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
            //Beverage[] beverageArray=new Beverage[1000];
            int IndexCounter=0;

            //Store each beverage in an array to be passed to the UI.
            foreach (Beverage beverage in beverageEntities.Beverages)
            {
                //Old add
                //beverageArray[IndexCounter] = beverage;
                //IndexCounter++;

                //Work in Progress add
                Beverage addBeverageToList = beverage;
                beverageArray[IndexCounter] = addBeverageToList;
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
            if (FindById(Input)!=null)
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
            if(foundBeverage.id!=null)
            {
                //Return found item
                return new string[] { foundBeverage.id, foundBeverage.name, foundBeverage.pack, foundBeverage.price.ToString() };
            }

            return new string[] { };
        }
        /// <summary>
        /// Method used to update an existing item except the PK.
        /// </summary>
        public bool UpdateExistingItem(string Input)
        {
            bool success = false;

            ////variable to store the incoming data
            //string inputString;

            //EF class initialization
            beverageEntities = new BeverageRCooleyEntities();

            //Pass the query through FindById to determine if it exists.
            if(FindById(Input)!=null)
            {
                success = true;
            }
            return success = false;
            ////Search for the wine item using the input as a search parameter.
            //Beverage updateBeverage = beverageEntities.Beverages.Find(inputString);

            //try
            //{
            //    ////Search for the wine item using the input as a search parameter.
            //    //updateBeverage = beverageEntities.Beverages.Find(inputString);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //    Console.WriteLine("Search for Item failed.");
            //}
            
            ////Wine item found, and announced.
            //Console.WriteLine("This beverage will be updated.");
            ////Pre-Modification data.
            //Console.WriteLine(updateBeverage.id + " " + updateBeverage.name + " " + updateBeverage.pack + " " +
            //                updateBeverage.price.ToString("n2") + " " + updateBeverage.active + " ");
            ////BEGIN alterations
            //updateBeverage.name = "[wine name]";
            //updateBeverage.pack = "12";
            //updateBeverage.price = 120;
            //updateBeverage.active = true;

            ////save updates
            //beverageEntities.SaveChanges();

            ////Search for modified item
            //beverageEntities.Beverages.Find(inputString);

            ////Display updated item
            //Console.WriteLine("This is the wine item post-change.");
            //Console.WriteLine(updateBeverage.id + " " + updateBeverage.name + " " + updateBeverage.pack + " " +
            //                updateBeverage.price.ToString("n2") + " " + updateBeverage.active + " ");

        }/**
          * Remember to Adjust the array size in the Delete Method to reflect the size.
          * */
    }
}
