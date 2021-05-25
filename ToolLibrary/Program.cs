using System;
using System.Threading;

namespace ToolLibrary
{
    class MainClass
    {
        ToolLibrarySystem tls;
        int MAX_STOCK = 1000;

        public static void Main()
        {
            Tests testClass = new Tests();
            MainClass main = new MainClass();
            main.tls = testClass.setupTests();
            main.mainMenu();
        }

        private void mainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to the Tool Library");
            Console.WriteLine("========== Main Menu ==========");
            Console.WriteLine("1. Staff Login");
            Console.WriteLine("2. Member Login");
            Console.WriteLine("0. Exit");
            Console.WriteLine("===============================");
            Console.WriteLine();
            Console.WriteLine("Please make a selection (1-2, or 0 to exit):");

            int input = readInt(0, 2);
            if (input == -1) mainMenu();
            switch (input)
            {
                case 1:
                    staffLogin();
                    break;
                case 2:
                    memberLogin();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
            }
        }

        private void writeStaffMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to the Tool Library");
            Console.WriteLine("========== Staff Menu ==========");
            Console.WriteLine("1. Add a new tool");
            Console.WriteLine("2. Add new pieces of an existing tool");
            Console.WriteLine("3. Remove some pieces of a tool");
            Console.WriteLine("4. Register a new member");
            Console.WriteLine("5. Remove a member");
            Console.WriteLine("6. Find the contact number of a member");
            Console.WriteLine("0. Return to main menu");
            Console.WriteLine("================================");
            Console.WriteLine();
            Console.WriteLine("Please make a selection (1-6, or 0 to return to main menu):");
        }


        private void writeToolCategoryMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Please make a selection (1-9):");
            Console.WriteLine("1. Gardening");
            Console.WriteLine("2. Flooring");
            Console.WriteLine("3. Fencing");
            Console.WriteLine("4. Measuring");
            Console.WriteLine("5. Cleaning");
            Console.WriteLine("6. Painting");
            Console.WriteLine("7. Electronic");
            Console.WriteLine("8. Electricity");
            Console.WriteLine("9. Automotive");
        }

        private ToolCategory convertCategoryInput(int categoryInput)
        {
            ToolCategory category = ToolCategory.Default;
            switch (categoryInput)
            {
                case 1:
                    category = ToolCategory.Gardening;
                    break;
                case 2:
                    category = ToolCategory.Flooring;
                    break;
                case 3:
                    category = ToolCategory.Fencing;
                    break;
                case 4:
                    category = ToolCategory.Measuring;
                    break;
                case 5:
                    category = ToolCategory.Cleaning;
                    break;
                case 6:
                    category = ToolCategory.Painting;
                    break;
                case 7:
                    category = ToolCategory.Electronic;
                    break;
                case 8:
                    category = ToolCategory.Electricity;
                    break;
                case 9:
                    category = ToolCategory.Automotive;
                    break;
                default:
                    break;
            }
            return category;
        }

        private void createTool()
        {
            Console.WriteLine("Add new tool");
            Console.WriteLine("Please enter tool name:");
            string name = Console.ReadLine();
            Console.WriteLine("Please enter quantity to add:");
            int quantity = readInt(0, MAX_STOCK);
            if (quantity == -1) createTool();
            writeToolCategoryMenu();
            int categoryInput = readInt(1, 9);
            if (categoryInput == -1) createTool();
            ToolCategory category = convertCategoryInput(categoryInput);
            ToolCollection tools = getToolCollection(category);
            Tool newTool = new Tool(name, quantity, category, tools.Name);
            tls.add(newTool);
        }

        private void addToExistingTool()
        {
            Console.WriteLine("Add new pieces of an existing tool");
            Console.WriteLine("Please enter name of tool:");
            string tool = Console.ReadLine();
            Console.WriteLine("Please enter quantity to add:");
            int quantity = readInt(0, MAX_STOCK);
            if (quantity == -1) addToExistingTool();
            Tool existingTool = tls.search(tool);
            if (existingTool != null)
            {
                tls.add(existingTool, quantity);
            }
        }

        private void deleteTool()
        {
            Console.WriteLine("Remove some pieces of a tool");
            Console.WriteLine("Please enter name of tool to delete:");
            string tool = Console.ReadLine();
            Console.WriteLine("Please enter quantity to delete:");
            int quantity = readInt(0, MAX_STOCK);
            if (quantity == -1) deleteTool();
            Tool existingTool = tls.search(tool);
            if (existingTool != null)
            {
                tls.delete(existingTool, quantity);
            }
        }

        private void newMember()
        {
            Console.WriteLine("Register a new member");
            Console.WriteLine("Please enter first name:");
            string fName = Console.ReadLine();
            Console.WriteLine("Please enter last name:");
            string lName = Console.ReadLine();
            Console.WriteLine("Please enter phone number:");
            string phone = Console.ReadLine();
            Console.WriteLine("Please enter PIN:");
            string pin = Console.ReadLine();
            Member newMember = new Member(fName, lName, phone, pin);
            tls.add(newMember);
            Console.WriteLine("Added new member: ");
            Console.WriteLine(fName + " " + lName + " " + phone);
        }

        private void deleteMember()
        {
            Console.WriteLine("Remove a member");
            Console.WriteLine("Please enter first name of member to be removed:");
            string fName = Console.ReadLine();
            Console.WriteLine("Please enter last name of member to be removed:");
            string lName = Console.ReadLine();
            Member member = tls.search(fName, lName);
            tls.delete(member);
        }

        private void findContactNumber()
        {
            Console.WriteLine("Find the contact number of a member");
            Console.WriteLine("Please enter first name of member:");
            string fName = Console.ReadLine();
            Console.WriteLine("Please enter last name of member:");
            string lName = Console.ReadLine();
            Member member = tls.search(fName, lName);
            Console.WriteLine("Contact number of " + member.FirstName + " " + member.LastName + ":");
            Console.WriteLine(member.ContactNumber);
        }

        private void staffLogin()
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();
            if(username.ToLower() == "staff" && password == "today123")
            {
                staffMenu();
            }
            else
            {
                Console.WriteLine("Incorrect username or password");
                Console.WriteLine("Press enter to try again or press 0 to return to main menu");
                string input = Console.ReadLine();
                if(input == "0")
                {
                    mainMenu();
                }
                else
                {
                    staffLogin();
                }
            }

        }

        private void memberLogin()
        {
            Console.WriteLine("Enter First Name:");
            string fName = Console.ReadLine();
            Console.WriteLine("Enter Last Name:");
            string lName = Console.ReadLine();
            Console.WriteLine("Enter PIN:");
            string pin = Console.ReadLine();
            Member m = tls.search(fName, lName, pin);
            if ( m != null)
            {
                memberMenu(m);
            }
            else
            {
                Console.WriteLine("Press enter to try again or press 0 to return to main menu");
                string input = Console.ReadLine();
                if (input == "0")
                {
                    mainMenu();
                }
                else
                {
                    memberLogin();
                }
            }
        }

        private void staffMenu()
        {
            writeStaffMenu();

            int input = readInt(0, 6);
            if (input == -1) staffMenu();
            switch(input)
            {
                case 1:
                    createTool();
                    staffMenu();
                    break;
                case 2:
                    addToExistingTool();
                    Thread.Sleep(500);
                    staffMenu();
                    break;
                case 3:
                    deleteTool();
                    staffMenu();
                    break;
                case 4:
                    newMember();
                    staffMenu();
                    break;
                case 5:
                    deleteMember();
                    staffMenu();
                    break;
                case 6:
                    findContactNumber();
                    staffMenu();
                    break;
                case 7:
                    Console.WriteLine("Return to main menu");
                    mainMenu();
                    break;
                default:
                    mainMenu();
                    break;

            }
        }

        private void writeMemberMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to the Tool Library");
            Console.WriteLine("========== Member Menu ==========");
            Console.WriteLine("1. Display all the tools of a tool type");
            Console.WriteLine("2. Borrow a tool");
            Console.WriteLine("3. Return a tool");
            Console.WriteLine("4. List all the tools than I am renting");
            Console.WriteLine("5. Display top three (3) most frequently rented tools");
            Console.WriteLine("0. Return to main menu");
            Console.WriteLine("=================================");
            Console.WriteLine();
            Console.WriteLine("Please make a selection (1-5, or 0 to return to main menu):");
        }

        private void displayTools()
        {
            Console.WriteLine("Display all the tools of a tool type");
            writeToolCategoryMenu();
            int categoryInput = readInt(1, 9);
            if(categoryInput == -1) displayTools();
            ToolCategory category = convertCategoryInput(categoryInput);
            ToolCollection tools = getToolCollection(category);
            if (!(category == ToolCategory.Default)) tls.displayTools(category, tools.Name);

        }

        private void confirmBorrow(Member member, Tool t)
        {
            Console.WriteLine("Tool is available, confirm borrow? Y - Yes N - No");
            string confirm = Console.ReadLine();
            if (confirm.ToLower() == "y")
            {
                tls.borrowTool(member, t);

            }
            else if (confirm.ToLower() == "n")
            {
                memberMenu(member);
            }
            else
            {
                Console.WriteLine("Please enter Y or N");
                confirmBorrow(member, t);
            }
        }

        private void borrowTool(Member member)
        {
            Console.WriteLine("Borrow a tool");
            Console.WriteLine("Please enter name of tool to borrow:");
            string tool = Console.ReadLine();
            Tool t = tls.search(tool);
            if (t != null && t.AvailableQuantity > 0)
            {
                confirmBorrow(member, t);
            }
            else
            {
                Console.WriteLine(tool + " is not available.");
            }
        }

        private void returnTool(Member member)
        {
            Console.WriteLine("Return a tool");
            Console.WriteLine("Enter name of tool to be returned: ");
            string toolStr = Console.ReadLine();
            Tool tool = tls.search(toolStr);
            if (tool != null)
            {
                tls.returnTool(member, tool);
                Console.WriteLine("Returned " + tool.Name);
            }
            else
            {
                Console.WriteLine("Invalid tool: " + toolStr);
            }
        }

        private ToolCollection getToolCollection(ToolCategory category)
        {
            Console.WriteLine("Please select a tool type:");
            ToolCollection[] toolTypes = tls.getToolTypes(category);
            for (int i = 0; i < toolTypes.Length; i++)
            {
                Console.WriteLine((i+1).ToString() + ". " + toolTypes[i].Name);
            }
            int index = readInt(1, toolTypes.Length + 1);
            ToolCollection tools = toolTypes[index - 1];
            return tools;
        }

        private void enterToContinue()
        {
            Console.WriteLine("Press enter to return to menu:");
            Console.ReadKey();
        }

        private int readInt(int min, int max)
        {
            int result = -1 ;
            string input = Console.ReadLine();
            int x;
            bool isNumeric = int.TryParse(input, out x);
            if (isNumeric && x >= min && x <= max)
            {
                result = Convert.ToInt16(input);
            }
            else
            {
                Console.WriteLine("Incorrect input: " + input);
            }
            return result;
        }

        private void memberMenu(Member member)
        {
            writeMemberMenu();
            int input = readInt(0, 5);
            if(input == -1) memberMenu(member);
            switch (input)
            {
                case 1:
                    Console.WriteLine();
                    displayTools();
                    enterToContinue();
                    memberMenu(member);
                    break;
                case 2:
                    Console.WriteLine();
                    borrowTool(member);
                    enterToContinue();
                    memberMenu(member);
                    break;
                case 3:
                    Console.WriteLine();
                    returnTool(member);
                    enterToContinue();
                    memberMenu(member);
                    break;
                case 4:
                    Console.WriteLine();
                    Console.WriteLine("List all the tools than I am renting");
                    tls.displayBorrowingTools(member);
                    enterToContinue();
                    memberMenu(member);
                    break;
                case 5:
                    Console.WriteLine();
                    Console.WriteLine("Display top three (3) most frequently rented tools");
                    tls.displayTopThree();
                    enterToContinue();
                    memberMenu(member);
                    break;
                case 0:
                    mainMenu();
                    break;
            }
        }
    }
}
