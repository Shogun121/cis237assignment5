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
    class UserInterface
    {
        const int maxMenuChoice = 6;
        //---------------------------------------------------
        //Public Methods
        //---------------------------------------------------

        //instantiate enties class
        BeverageRCooleyEntities beverageEntities;

        //Display Welcome Greeting
        public void DisplayWelcomeGreeting()
        {
            Console.WriteLine("Welcome to the wine program");
        }

        //Display Menu And Get Response
        public int DisplayMenuAndGetResponse()
        {
            //declare variable to hold the selection
            string selection;

            //Display menu, and prompt
            this.displayMenu();
            this.displayPrompt();

            //Get the selection they enter
            selection = this.getSelection();

            //While the response is not valid
            while (!this.verifySelectionIsValid(selection))
            {
                //display error message
                this.displayErrorMessage();

                //display the prompt again
                this.displayPrompt();

                //get the selection again
                selection = this.getSelection();
            }
            //Return the selection casted to an integer
            return Int32.Parse(selection);
        }

        //Get the search query from the user
        public string GetSearchQuery()
        {
            Console.WriteLine();
            Console.WriteLine("What would you like to search for?");
            Console.Write("> ");
            return Console.ReadLine();
        }

        //Get New Item Information From The User.
        public string[] GetNewItemInformation()
        {
            Console.WriteLine();
            Console.WriteLine("What is the new items Id?");
            Console.Write("> ");
            string id = Console.ReadLine();
            //instance created to allow for testing ID location
            Beverage foundBeverage = beverageEntities.Beverages.Find(Console.ReadLine());

            //prompt user until a unique ID is given.
            while(id == foundBeverage.id)
            {
                Console.WriteLine("That ID is already in use");
                Console.WriteLine("What is the new items Id?");
                id = Console.ReadLine();
            }
           //If the ID is unique, ask for the rest of the information.
            Console.WriteLine("What is the new items Description?");
            Console.Write("> ");
            string description = Console.ReadLine();
            Console.WriteLine("What is the new items Pack?");
            Console.Write("> ");
            string pack = Console.ReadLine();

            return new string[] { id, description, pack };
            
            
        }

        //Display Import Success
        public void DisplayImportSuccess()
        {
            Console.WriteLine();
            Console.WriteLine("Wine List Has Been Imported Successfully");
        }

        //Display Import Error
        public void DisplayImportError()
        {
            Console.WriteLine();
            Console.WriteLine("There was an error importing the CSV");
        }

        //Display All Items
        public void DisplayAllItems(string[] allItemsOutput)
        {
            Console.WriteLine();
            foreach (string itemOutput in allItemsOutput)
            {
                Console.WriteLine(itemOutput);
            }
        }

        //Display All Items Error
        public void DisplayAllItemsError()
        {
            Console.WriteLine();
            Console.WriteLine("There are no items in the list to print");
        }

        //Display Item Found Success
        public void DisplayItemFound(string itemInformation)
        {
            Console.WriteLine();
            Console.WriteLine("Item Found!");
            Console.WriteLine(itemInformation);
        }

        //Display Item Found Error
        public void DisplayItemFoundError()
        {
            Console.WriteLine();
            Console.WriteLine("A Match was not found");
        }

        //Display Add Wine Item Success
        public void DisplayAddWineItemSuccess()
        {
            Console.WriteLine();
            Console.WriteLine("The Item was successfully added");
        }

        //Display Item Already Exists Error
        public void DisplayItemAlreadyExistsError()
        {
            Console.WriteLine();
            Console.WriteLine("An Item With That Id Already Exists");
        }
        /// <summary>
        /// Method that prints the entire list of beverages in the database.
        /// </summary>
        public void PrintDatabaseBeverageList()
        {
            beverageEntities = new BeverageRCooleyEntities();

            //Using a foreach loop print out each item in the database
            foreach (Beverage beverage in beverageEntities.Beverages)
            {
                Console.WriteLine(beverage.id + " " + beverage.name + " " + beverage.pack + " " +
                beverage.price.ToString("n2") + " " + beverage.active + " ");
            }
        }
        /// <summary>
        /// Search for an item within the beverage database.
        /// </summary>
        public void SearchForItem()
        {
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
        /// <summary>
        /// Method used to delete a record from the database of wine items.
        /// </summary>
        public void DeleteRecord()
        {
            //input variables
            string inputString;
            //initializing instantiation of the EF class.
            beverageEntities = new BeverageRCooleyEntities();
            //Attempt to delete a record from the database.
            Console.WriteLine("Enter a string for the ID of the Item you wish to delete.");
            inputString = Console.ReadLine();
            //Find a record based the PK.
            Beverage beverageToDelete = beverageEntities.Beverages.Find(inputString);

            //Delete the record assuming the PK is good.
            beverageEntities.Beverages.Remove(beverageToDelete);
            Console.WriteLine("Beverage found");

            //Save the changes
            beverageEntities.SaveChanges();

            try
            {
                beverageToDelete = beverageEntities.Beverages.Find(inputString);
                Console.WriteLine(beverageToDelete.id + " " + beverageToDelete.name + " " + beverageToDelete.pack + " ");
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
        /// <summary>
        /// Method used to update an existing item except the PK.
        /// </summary>
        public void UpdateExistingItem()
        {
            //variable to store the incoming data
            string inputString;
            //initialization
            beverageEntities = new BeverageRCooleyEntities();
            //Prompt user for Input
            Console.WriteLine("Update an item, via given ID");
            //Acquire input
            inputString = Console.ReadLine();
            //try
            //{
            //    //Search for the wine item using the input as a search parameter.
            //    Beverage updateBeverage = beverageEntities.Beverages.Find(inputString);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //    Console.WriteLine("Search for Item failed.");
            //}
            //Search for the wine item using the input as a search parameter.
            Beverage updateBeverage = beverageEntities.Beverages.Find(inputString);
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

        //---------------------------------------------------
        //Private Methods
        //---------------------------------------------------

        //Display the Menu
        private void displayMenu()
        {
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine();
            Console.WriteLine("1. Print the Entire Wine List");
            Console.WriteLine("2. Search for an Item");
            Console.WriteLine("3. Add a New Item To The List");
            Console.WriteLine("4. Update an Existing Item");
            Console.WriteLine("5. Delete an Existing item");
            Console.WriteLine("6. Exit");
        }

        //Display the Prompt
        private void displayPrompt()
        {
            Console.WriteLine();
            Console.Write("Enter Your Choice: ");
        }

        //Display the Error Message
        private void displayErrorMessage()
        {
            Console.WriteLine();
            Console.WriteLine("That is not a valid option. Please make a valid choice");
        }

        //Get the selection from the user
        private string getSelection()
        {
            return Console.ReadLine();
        }

        //Verify that a selection from the main menu is valid
        private bool verifySelectionIsValid(string selection)
        {
            //Declare a returnValue and set it to false
            bool returnValue = false;

            try
            {
                //Parse the selection into a choice variable
                int choice = Int32.Parse(selection);

                //If the choice is between 0 and the maxMenuChoice
                if (choice > 0 && choice <= maxMenuChoice)
                {
                    //set the return value to true
                    returnValue = true;
                }
            }
            //If the selection is not a valid number, this exception will be thrown
            catch (Exception e)
            {
                //set return value to false even though it should already be false
                returnValue = false;
            }

            //Return the reutrnValue
            return returnValue;
        }
        
    }
}
