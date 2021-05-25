using System;
namespace ToolLibrary
{
    public class Tests
    {
        public Tests()
        {
            
        }

        public ToolLibrarySystem setupTests()
        {

            ToolLibrarySystem tls = new ToolLibrarySystem();

            Member[] members = new Member[6];

            members[0] = new Member("John", "Smith", "1111111111", "1111");
            members[1] = new Member("Tom", "Mills", "2222222222", "2222");  
            members[2] = new Member("James", "Smith", "3333333333", "3333");
            members[3] = new Member("Tim", "Mills", "4444444444", "4444");  
            members[4] = new Member("Alex", "Xavier", "5555555555", "5555");
            members[5] = new Member("David", "Cross", "6666666666", "6666");



            //foreach ( Tool t in tools)
            //{
            //    tls.add(t);
            //}

            foreach(Member m in members)
            {
                tls.add(m);
            }

            Tool tool1 = new Tool("Irwin 125mm Orbital Sander", 18, ToolCategory.Painting, "sanding tools");
            Tool tool2 = new Tool("Rocket Sanding Block Holder", 9, ToolCategory.Painting, "sanding tools");
            Tool tool3 = new Tool("PowerFit 120 Triangular Sander ", 12, ToolCategory.Painting, "sanding tools");

            tls.add(tool1);
            tls.add(tool2);
            tls.add(tool3);

            tls.borrowTool(members[0], tool1);
            tls.borrowTool(members[1], tool1);
            tls.borrowTool(members[2], tool1);
            tls.borrowTool(members[0], tool2);
            tls.borrowTool(members[0], tool3);
            tls.borrowTool(members[1], tool2);


            //for (int i = 0; i < 10; i++)
            //{
            //    Random r = new Random();
            //    int rInt = r.Next(0, 49);
            //    int r2Int = r.Next(0, 5);
            //    Console.WriteLine("R1: " + rInt);
            //    Console.WriteLine("R2: " + r2Int);
            //    tls.borrowTool(members[r2Int], tools[rInt]);
            //}




            //tls.borrowTool(aMember, hammer);
            //tls.displayBorrowingTools(aMember);
            //tls.displayTools("Flooring");
            //tls.displayTopThree();
            ////tls.delete(hammer);
            //tls.search(anotherMember);
            //tls.delete(anotherMember);
            //tls.search(anotherMember);
            //tls.returnTool(aMember, hammer);
            //tls.displayBorrowingTools(aMember);

            return tls;
        }

        //private static Tool testTool()
        //{
        //    Tool hammer = new Tool("Hammer", 5, ToolCategory.Flooring);
        //    return hammer;
        //}

        //    private static ToolCollection testToolColl(Tool tool)
        //    {
        //        ToolCollection toolCollection = new ToolCollection(100);
        //        toolCollection.add(tool);
        //        toolCollection.search(tool);
        //        Console.WriteLine("Tools: ");
        //        foreach (Tool t in toolCollection.toArray())
        //        {
        //            Console.WriteLine(t.Name);
        //        }
        //        toolCollection.delete(tool, 1);
        //        toolCollection.search(tool);
        //        Console.WriteLine("Tools: ");
        //        foreach (Tool t in toolCollection.toArray())
        //        {
        //            if (t != null)
        //            {
        //                Console.WriteLine(t.Name);
        //            }
        //        }

        //        return toolCollection;
        //    }

        //    private static Member testMember(string fName, string lName, string phone, string pin)
        //    {
        //        Member member = new Member(fName, lName, phone, pin);
        //        Console.WriteLine("New member: " + member.ToString());
        //        return member;
        //    }

        //    private static MemberCollection testMemberColl(Member aMember, Member anotherMember)
        //    {
        //        MemberCollection aMemberColl = new MemberCollection();
        //        aMemberColl.add(aMember);
        //        aMemberColl.add(anotherMember);
        //        if (aMemberColl.search(aMember))
        //        {
        //            Console.WriteLine("Searched and found: " + aMember.FirstName + " " + aMember.LastName);
        //        }
        //        Member[] memberArr = aMemberColl.toArray();
        //        Console.WriteLine("Members: ");
        //        foreach (Member m in memberArr)
        //        {
        //            if (m != null)
        //            {
        //                Console.WriteLine(m);
        //            }
        //        }
        //        aMemberColl.delete(aMember);
        //        memberArr = aMemberColl.toArray();
        //        Console.WriteLine("Members: ");
        //        foreach (Member m in memberArr)
        //        {
        //            if (m != null)
        //            {
        //                Console.WriteLine(m);
        //            }
        //        }
        //        return aMemberColl;
        //    }
    }
}
