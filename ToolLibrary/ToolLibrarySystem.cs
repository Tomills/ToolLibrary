using System;
using System.Collections;
using System.Collections.Generic;

namespace ToolLibrary
{
    public class ToolLibrarySystem : iToolLibrarySystem
    {
        private Dictionary<string, ToolCollection[]> toolLibrary;
        private MemberCollection members;
        private int MAX_TOOL_TYPES = 50;

        public ToolLibrarySystem()
        {
            members = new MemberCollection();
            toolLibrarySetup();
        }

        public void add(Tool aTool)
        {
            add(aTool, 1);
        }

        public void add(Tool aTool, int quantity)
        {
            ToolCollection tools = getToolTypeCollection(aTool);
            tools.add(aTool, quantity);

            Console.WriteLine("Added: " + aTool.Name);
            
        }

        public void add(Member aMember)
        {
            members.add(aMember);
        }

        public void borrowTool(Member aMember, Tool aTool)
        {
            aTool.addBorrower(aMember);
            aMember.addTool(aTool);
            aTool.AvailableQuantity--;
            aTool.NoBorrowings++;
        }

        public void delete(Tool aTool)
        {
            delete(aTool, 1);
        }

        public void delete(Tool aTool, int quantity)
        {
            ToolCollection tools = getToolTypeCollection(aTool);
            tools.delete(aTool, quantity);
        }

        public void delete(Member aMember)
        {
            members.delete(aMember);
        }

        public void displayBorrowingTools(Member aMember)
        {
            Tool[] tools = aMember.Tools.toArray();

            if(tools.Length > 0 && tools[0] != null)
            {
                Console.WriteLine(aMember.FirstName + " " + aMember.LastName + "'s tools:");
                foreach (Tool t in tools)
                {
                    Console.WriteLine(t.Name);
                }
            }
            else
            {
                Console.WriteLine(aMember.FirstName + " hasn't borrowed any tools");
            }
            Console.WriteLine();
        }

        public void displayTools(ToolCategory category, string aToolType)
        {
            ToolCollection[] toolTypes = getToolTypes(category);
            bool empty = true;
            foreach( ToolCollection tc in toolTypes)
            {
                if( tc.Name.ToLower() == aToolType.ToLower())
                {
                    Console.WriteLine(tc.Name + ":");
                    foreach (Tool t in tc.toArray())
                    {
                        if (t != null)
                        {
                            Console.WriteLine(t.Name + " - Quantity: " + t.AvailableQuantity);
                            empty = false;
                        }
                    }
                }
            }
            if (empty)
            {
                Console.WriteLine("The tool list is empty.");

            }
            Console.WriteLine();

        }

        public void displayTopThree()
        {
            Tool[] toolArray = getToolsArr();
            Tool[] sortedArray = heapSort(toolArray, toolArray.Length);
            Console.WriteLine("Top Tools: ");
            if(sortedArray[0] != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i >= 0 && i < sortedArray.Length)
                    {
                        Console.Write(i + 1 + ". ");
                        Console.Write(sortedArray[i].Name + " - Borrowed " + sortedArray[i].NoBorrowings.ToString() + " times.\n");
                    }
                }
            }
            else
            {
                Console.WriteLine("The tool list is empty.");
            }            
        }

        // Uses the heap sort algorithm to sort the array A, with length n, from largest to smallest borrowings.
        // This algorithm has been modified from a tutorialspoint HeapSort tutorial.
        // https://www.tutorialspoint.com/heap-sort-in-chash#:~:text=Heap%20Sort%20is%20a%20sorting,then%20the%20heap%20is%20reestablished.

        private static Tool[] heapSort(Tool[] tools, int n)
        {
            for (int i = n / 2 - 1; i >= 0; i--)
                heapify(tools, n, i);
            for (int i = n - 1; i >= 0; i--)
            {
                Tool temp = tools[0];
                tools[0] = tools[i];
                tools[i] = temp;
                heapify(tools, i, 0);
            }
            return tools;
        }

        private static void heapify(Tool[] tools, int n, int i)
        {
            int smallest = i;
            int l = 2 * i + 1;
            int r = 2 * i + 2;
            if (l < n && tools[l].NoBorrowings < tools[smallest].NoBorrowings)
                smallest = l;
            if (r < n && tools[r].NoBorrowings < tools[smallest].NoBorrowings)
                smallest = r;
            if (smallest != i)
            {
                Tool swap = tools[i];
                tools[i] = tools[smallest];
                tools[smallest] = swap;
                heapify(tools, n, smallest);
            }
        }

        public string[] listTools(Member aMember)
        {
            Tool[] toolArr = aMember.Tools.toArray();
            String[] listTools = new String[toolArr.Length];
            for(int i = 0; i < toolArr.Length; i++)
            {
                listTools[i] = toolArr[i].Name;
            }
            return listTools;
        }

        public void search(Member aMember)
        {
            bool found = members.search(aMember);
            if (found)
            {
                Console.WriteLine(aMember.FirstName + " " + aMember.LastName + " is a member.");
            }
            else
            {
                Console.WriteLine(aMember.FirstName + " " + aMember.LastName + " is not a member.");

            }
        }

        public Tool search(string toolName)
        {
            Tool tool = null;
            foreach(Tool t in getToolsArr())
            {
                if (t != null)
                {
                    if (t.Name == toolName)
                    {
                        tool = t;
                    }
                }
            }
            return tool;
        }

        public Member search(string memberFName, string memberLName)
        {
            Member member = null;
            foreach (Member m in members.toArray())
            {
                if (m != null)
                {
                    if (m.FirstName == memberFName && m.LastName == memberLName)
                    {
                        member = m;
                    }
                }
            }
            return member;
        }

        public Member search(string memberFName, string memberLName, string pin)
        {
            Member member = null;
            Member[] membersArr = members.toArray();
            foreach (Member m in membersArr)
            {
                if (m != null)
                {
                    if (m.FirstName.ToLower() == memberFName.ToLower() && m.LastName.ToLower() == memberLName.ToLower() && m.PIN == pin)
                    {
                        member = m;
                    }
                }
            }
            return member;
        }

        public void returnTool(Member aMember, Tool aTool)
        {
            aMember.Tools.delete(aTool);
            aTool.AvailableQuantity++;
        }

        // Returns the Tool Collection containing given Tool 
        private ToolCollection getToolTypeCollection(Tool aTool)
        {
            string categoryStr = aTool.Category.ToString().ToLower();
            ToolCollection[] toolTypes;
            ToolCollection tools = null;
            toolTypes = (ToolCollection[])toolLibrary[categoryStr];
            for (int i = 0; i < toolTypes.Length; i++)
            {
                if (toolTypes[i].Name.ToLower() == aTool.ToolType.ToLower())
                {
                    tools = toolTypes[i];
                }
            }
            return tools;
        }

        public ToolCollection[] getToolTypes(ToolCategory category)
        {
            return (ToolCollection[])toolLibrary[category.ToString().ToLower()];
        }

        private Tool[] getToolsArr()
        {
            List<Tool> toolsList = new List<Tool>();
            foreach(KeyValuePair<string, ToolCollection[]> entry in toolLibrary)
            {
                foreach (ToolCollection tc in entry.Value)
                {
                    foreach (Tool t in tc.toArray())
                    {
                        toolsList.Add(t);
                    }
                }
            }
            return toolsList.ToArray();

        }

        private void toolLibrarySetup()
        {
            toolLibrary = new Dictionary<string, ToolCollection[]>()
            {
                {"gardening", new ToolCollection[5]
                    {
                        new ToolCollection(MAX_TOOL_TYPES, "Line Trimmers"),
                        new ToolCollection(MAX_TOOL_TYPES, "Lawn Mowers"),
                        new ToolCollection(MAX_TOOL_TYPES, "Garden Hand Tools"),
                        new ToolCollection(MAX_TOOL_TYPES, "Wheelbarrows"),
                        new ToolCollection(MAX_TOOL_TYPES, "Garden Power Tools")
                    }
                },
                {"flooring", new ToolCollection[6]
                    {
                        new ToolCollection(MAX_TOOL_TYPES, "Scrapers"),
                        new ToolCollection(MAX_TOOL_TYPES, "Floor Lasers"),
                        new ToolCollection(MAX_TOOL_TYPES, "Floor Levelling Tools"),
                        new ToolCollection(MAX_TOOL_TYPES, "Floor Levelling Material"),
                        new ToolCollection(MAX_TOOL_TYPES, "Floor Hand Tools"),
                        new ToolCollection(MAX_TOOL_TYPES, "Tiling Tools")
                }
                },{"fencing", new ToolCollection[5]
                    {
                        new ToolCollection(MAX_TOOL_TYPES, "Fencing Hand Tools"),
                        new ToolCollection(MAX_TOOL_TYPES, "Electric Fencing"),
                        new ToolCollection(MAX_TOOL_TYPES, "Steel Fencing Tools"),
                        new ToolCollection(MAX_TOOL_TYPES, "Power Tools"),
                        new ToolCollection(MAX_TOOL_TYPES, "Fencing Accessories")
                    }
                },{"measuring", new ToolCollection[6]
                    {
                        new ToolCollection(MAX_TOOL_TYPES, "Distance Tools"),
                        new ToolCollection(MAX_TOOL_TYPES, "Laser Measurers"),
                        new ToolCollection(MAX_TOOL_TYPES, "Measuring Jugs"),
                        new ToolCollection(MAX_TOOL_TYPES, "Floor Levelling Material"),
                        new ToolCollection(MAX_TOOL_TYPES, "Temperature & Humidity Tools"),
                        new ToolCollection(MAX_TOOL_TYPES, "Levelling Tools")
                    }
                },{"cleaning", new ToolCollection[6]
                    {
                        new ToolCollection(MAX_TOOL_TYPES, "Draining"),
                        new ToolCollection(MAX_TOOL_TYPES, "Car Cleaning"),
                        new ToolCollection(MAX_TOOL_TYPES, "Vacuums"),
                        new ToolCollection(MAX_TOOL_TYPES, "Pressure Cleaners"),
                        new ToolCollection(MAX_TOOL_TYPES, "Pool Cleaning"),
                        new ToolCollection(MAX_TOOL_TYPES, "Floor Cleaning")
                    }
                },{"painting", new ToolCollection[6]
                    {
                        new ToolCollection(MAX_TOOL_TYPES, "Sanding Tools"),
                        new ToolCollection(MAX_TOOL_TYPES, "Brushes"),
                        new ToolCollection(MAX_TOOL_TYPES, "Rollers"),
                        new ToolCollection(MAX_TOOL_TYPES, "Paint Removal Tools"),
                        new ToolCollection(MAX_TOOL_TYPES, "Paint Scrapers"),
                        new ToolCollection(MAX_TOOL_TYPES, "Sprayers")
                    }
                },{"electronic", new ToolCollection[5]
                    {
                        new ToolCollection(MAX_TOOL_TYPES, "Voltage Testers"),
                        new ToolCollection(MAX_TOOL_TYPES, "Oscilloscope"),
                        new ToolCollection(MAX_TOOL_TYPES, "Thermal Imaging"),
                        new ToolCollection(MAX_TOOL_TYPES, "Data Test Tools"),
                        new ToolCollection(MAX_TOOL_TYPES, "Insulation Testers")
                    }
                },{"electricity", new ToolCollection[5]
                    {
                        new ToolCollection(MAX_TOOL_TYPES, "Test Equipment"),
                        new ToolCollection(MAX_TOOL_TYPES, "Safety Equipment"),
                        new ToolCollection(MAX_TOOL_TYPES, "Basic Hand tools"),
                        new ToolCollection(MAX_TOOL_TYPES, "Circuit Protection"),
                        new ToolCollection(MAX_TOOL_TYPES, "Cable Tool")
                    }
                },{"automotive", new ToolCollection[6]
                    {
                        new ToolCollection(MAX_TOOL_TYPES, "Jacks"),
                        new ToolCollection(MAX_TOOL_TYPES, "Air Compressors"),
                        new ToolCollection(MAX_TOOL_TYPES, "Battery Chargers"),
                        new ToolCollection(MAX_TOOL_TYPES, "Socket Tools"),
                        new ToolCollection(MAX_TOOL_TYPES, "Braking"),
                        new ToolCollection(MAX_TOOL_TYPES, "Drivetrain")
                    }
                },
            };
    }

    }
}
