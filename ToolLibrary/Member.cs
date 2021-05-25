using System;
namespace ToolLibrary
{
    public class Member : iMember
    {
        private string firstName;
        private string lastName;
        private string contactNumber;
        private string pin;
        private ToolCollection tools;
        private Member lChild;
        private Member rChild;

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string ContactNumber { get => contactNumber; set => contactNumber = value; }
        public string PIN { get => pin; set => pin = value; }
        public ToolCollection Tools { get => tools; set => tools = value; }
        public Member LChild { get => lChild; set => lChild = value; }
        public Member RChild { get => rChild; set => rChild = value; }

        public Member(string firstName, string lastName, string contactNumber, string pin)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.contactNumber = contactNumber;
            this.pin = pin;
            tools = new ToolCollection(3, (firstName + " " + lastName + " tools"));
        }

        public void addTool(Tool aTool)
        {
            tools.add(aTool, 1);
        }

        public void deleteTool(Tool aTool)
        {
            tools.delete(aTool);
        }

        //return a string containing the first name, lastname, and contact phone number of this member
        public override string ToString()
        {
            return firstName + " " + lastName + " " + contactNumber;
        }

        public int CompareTo(Object obj)
        {
            Member another = (Member)obj;
            if (this.lastName.CompareTo(another.LastName) < 0)
            {
                return -1;
            }
            else
            {
                if (this.lastName.CompareTo(another.LastName) == 0)
                {
                    return this.firstName.CompareTo(another.FirstName);
                }
                else
                {
                    return 1;
                }

            }
        }
                

    }
}
