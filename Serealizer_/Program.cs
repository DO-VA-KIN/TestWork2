using System;
using System.IO;

namespace Serealizer_
{
    class Program
    {
        static void Main(string[] args)
        {
            ListRandom listRandom = new ListRandom();

            Example ex = new Example();
            listRandom.Head = ex.Root;
            listRandom.Tail = ex.Tail;
            listRandom.Count = ex.Count;

            using (FileStream stream = File.Open(Environment.CurrentDirectory + @"\serealize.txt", FileMode.Create))
                listRandom.Serealize(stream);

           // using (Stream stream = File.Open(Environment.CurrentDirectory + @"\serealize.txt", FileMode.Open))
                //listRandom.Deserealize(stream);

        }





    }

    public class Example
    {
        public Example()
        {
            ListNode node1 = new ListNode();
            ListNode node2 = new ListNode();
            ListNode node3 = new ListNode();
            ListNode node4 = new ListNode();
            ListNode node5 = new ListNode();

            node1.Previous = null;
            node1.Next = node2;
            node1.Random = null;
            node1.Data = "AB";

            node2.Previous = node1;
            node2.Next = node3;
            node2.Random = node3;
            node2.Data = "BC";

            node3.Previous = node2;
            node3.Next = node4;
            node3.Random = node5;
            node3.Data = "CD";

            node4.Previous = node3;
            node4.Next = node5;
            node4.Random = node1;
            node4.Data = "DE";

            node5.Previous = node4;
            node5.Next = null;
            node5.Random = node1;
            node5.Data = "EF";

            Root = node1;
            Tail = node5;
            Count = 5;
        }

        public ListNode Root;
        public ListNode Tail;
        public int Count;
    }
}

