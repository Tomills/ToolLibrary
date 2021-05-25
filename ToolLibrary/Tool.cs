using System;

namespace ToolLibrary
{
    public class Tool : iTool
    {
        private string name;
        private int quantity;
        private int availableQuantity;
        private int noBorrowings;
        private MemberCollection getBorrowers;
        private ToolCategory category;
        private string toolType;

        public Tool(string name, int quantity, ToolCategory category, string toolType)
        {
            this.toolType = toolType;
            this.category = category;
            this.name = name;
            this.quantity = quantity;
            this.availableQuantity = quantity;
            this.noBorrowings = 0;
            getBorrowers = new MemberCollection();
        }

        public string Name // get and set the name of this tool
        {
            get { return name; }
            set { name = value; }
        }

        public int Quantity //get and set the quantity of this tool
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public int AvailableQuantity //get and set the quantity of this tool currently available to lend
        {
            get { return availableQuantity; }
            set { availableQuantity = value; }
        }

        public int NoBorrowings //get and set the number of times that this tool has been borrowed
        {
            get { return noBorrowings; }
            set { noBorrowings = value; }
        }

        public MemberCollection GetBorrowers  //get all the members who are currently holding this tool
        {
            get { return getBorrowers; }
        }

        public ToolCategory Category { get => category; }

        public string ToolType { get => toolType;}


        //add a member to the borrower list
        public void addBorrower(Member aMember)
        {
            getBorrowers.add(aMember);
        }

        //delete a member from the borrower list
        public void deleteBorrower(Member aMember)
        {
            getBorrowers.delete(aMember);
        }

        //return a string containing the name and the available quantity quantity this tool 
        public override string ToString()
        {
            return (name + ": " + availableQuantity.ToString());
        }

    }
}
