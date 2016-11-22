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
            //Query the ID
            Console.WriteLine();
            Console.WriteLine("What is the new item's {0}?","id");
            Console.Write("> ");
            string id = Console.ReadLine();
            //Query the Name
            Console.WriteLine("What is the new item's {0}?","name");
            Console.Write("> ");
            string name = Console.ReadLine();
            //Query the Pack
            Console.WriteLine("What is  the {1} for {0}?",name,"pack");
            Console.Write("> ");
            string pack = Console.ReadLine();
            //Query the Price
            Console.WriteLine("What is the {0} for {1}","price",name);
            Console.WriteLine("> ");
            decimal price;
            decimal.TryParse(Console.ReadLine(),out price);
            //Query whether or not it is active.
            Console.WriteLine("Is {0} {1}? T/F",name,"active");
            Console.WriteLine("> ");
            string active=Console.ReadLine().ToUpper();

            //return all the collected information to Main for processing.
            return new string[] { id, name, pack, price.ToString(),active };           
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
            Console.WriteLine("{0} Found!","beverage");
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
            Console.WriteLine("The {0} was successfully added","beverage");
        }

        //Display Item Already Exists Error
        public void DisplayItemAlreadyExistsError()
        {
            Console.WriteLine();
            Console.WriteLine("A(n) {0} with that id already exists","beverage");
        }
        /// <summary>
        /// Method that prints the entire list of beverages in the database.
        /// </summary>
        public void PrintDatabaseBeverageList(Beverage[] BeverageArray)
        {
            //Using a foreach loop print out each item in the database
            foreach (Beverage beverage in BeverageArray)
            {
                try
                {
                    Console.WriteLine(beverage.id + " " + beverage.name + " " + beverage.pack + " " +
                beverage.price.ToString("n2") + " " + beverage.active + " ");
                }
                catch(Exception e)
                {
                    
                }

            }
        }
        /// <summary>
        /// Method used to display success when deleting an item.
        /// </summary>
        public void DisplayDeleteSuccess()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("{0} successfully deleted!","beverage");
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
