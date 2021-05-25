using System;
namespace ToolLibrary
{
    public class ToolCollection : iToolCollection
    {
        private Tool[] tools;
        private int totalHeld;
        // The number of the types of tools in the collection
        private int number;
        private string name;

        public string Name { get => name; }

        public int Number { get => number; }

        public ToolCollection(int number, string name)
        {
            this.name = name;
            totalHeld = 0;
            this.number = number;
            tools = new Tool[Number];
        }

        // Add a given tool to this tool collection
        public void add(Tool aTool, int quantity)
        {
            if(totalHeld < number)
            {
                foreach (Tool t in tools)
                {
                    if (t != null)
                    {
                        if (t.Name == aTool.Name)
                        {
                            t.Quantity += quantity;
                            t.AvailableQuantity++;
                            Console.WriteLine(aTool.Name + " has been added to your tools");
                            return;
                        }
                    }

                }
                tools[totalHeld] = aTool;
                totalHeld++;
            }
            else
            {
                Console.WriteLine("Tool borrowing capacity reached - you cannot add any more tools.");
            }
        }
            

        public void delete(Tool aTool)
        {
            delete(aTool, 1);
        }

        // Delete a given tool from this tool collection
        public void delete(Tool aTool, int quantity)
        {
            // If there are still some tools left, just reduce quantity
            if (quantity < aTool.Quantity)
            {
                aTool.Quantity -= quantity;
                aTool.AvailableQuantity -= quantity;
            }
            else
            {
                // If there are no tools left, then delete tool from system
                for (int i = 0; i < totalHeld; i++)
                {
                    if (tools[i].Name == aTool.Name)
                    {
                        for (int ii = i; ii < totalHeld; ii++)
                        {
                            tools[ii] = tools[ii + 1];
                        }
                    }
                }
            }
            
            Console.WriteLine("Deleted " + quantity + " of " + aTool.Name);
        }

        // Search a given tool in this tool collection. Return true if this tool is in the tool collection; return false otherwise
        public bool search(Tool aTool)
        {
            foreach (Tool t in tools)
            {
                if (t != null)
                {
                    if (t.Name == aTool.Name)
                    {
                        Console.WriteLine("Found: " + aTool.Name);
                        return true;
                    }
                }
            }
            return false;
        }

        // Output the tools in this tool collection to an array of Tool with a capacity equal to the current capacity of the tool collection.
        public Tool[] toArray()
        {
            Tool[] arr = new Tool[totalHeld];
            for(int i = 0; i < totalHeld; i++)
            {
                arr[i] = tools[i];
            }
            return arr;
        }
    }
}
