using System.ComponentModel;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace Spojak
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList spojak = new LinkedList();

            spojak.AddToEnd(3);
            spojak.AddToEnd(3);
            spojak.AddToEnd(4);
            spojak.AddToEnd(8);
            spojak.AddToEnd(5);
            spojak.AddToEnd(6);
            spojak.AddToEnd(3);
            spojak.AddToEnd(8);
            spojak.AddToEnd(3);
            spojak.AddToEnd(3);
            spojak.AddToEnd(3);

            Console.WriteLine($"délka: { spojak.Length()}");
            //spojak.RemoveFromBeggining();
            //spojak.RemoveAll(3);
            //spojak.RemoveFromEnd();
            //Console.WriteLine($"číslo 6 je obsaženo v listu [{spojak.Exist(6)}],číslo 42 je obsaženo v listu [{spojak.Exist(42)}]");
            //Console.WriteLine($"max value = {spojak.FindMax()}");

            spojak.Print();
        }
    }
    class Node
    {
        // konstruktor
        public Node(int value)
        {
            Value = value;
            Next = null;
        }
        public int Value { get; set; }
        public Node Next { get; set; }
    }

    class LinkedList
    {
        public Node Head { get; set; }
        public void AddToEnd(int value)//O(n)
        {
            if (Head == null)
            {
                Head = new Node(value);
            }
            else
            {
                Node currentNode = Head;
                while (currentNode.Next != null)
                {
                    currentNode = currentNode.Next;
                }
                currentNode.Next = new Node(value);
            }
        }
        public void RemoveFromEnd()
        {
            Node currentNode = Head;
            if (currentNode.Next == null)
                Head = null;
            else
            {
                while (currentNode.Next.Next != null)
                {
                    currentNode = currentNode.Next;
                }
                currentNode.Next = null;
            }
        }//O(n)
        public void RemoveFromBeginning()
        {
            Node currentNode = Head;
            if (currentNode.Next == null)
                Head = null;
            else
            {
                Head = currentNode.Next;
            }
        }//O(n)
        public bool Exist(int value)
        {
            Node currentNode = Head;
            while (currentNode != null)
            {
                if (currentNode.Value == value)
                {
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }//(n)
        public void RemoveAll(int value)
        {
            Node currentNode = Head;
            while (currentNode.Value == value)
            {
                RemoveFromBeginning();
                currentNode = currentNode.Next;
            }
            if (currentNode.Next.Next != null)
            {
                while (currentNode.Next.Next != null)
                {
                    if (currentNode.Next.Value == value)
                    {
                        currentNode.Next = (currentNode.Next).Next;
                    }
                    else
                    {
                        currentNode = currentNode.Next;
                    }
                }
            }
            if (currentNode.Next.Value == value)
            {
                RemoveFromEnd();
            }
        }//(n)
        public void Print()
        {
            Node node = Head;
            while (node != null)
            {
                Console.WriteLine(node.Value);
                node = node.Next;
            }

        }//(n)
        public int? FindMax()
        {
            Node node = Head;
            if (Head == null)
            {
                return null;
            }
            int maximum = node.Value;
            while (node != null)
            {
                if (node.Value > maximum)
                {
                    maximum = node.Value;
                }
                node = node.Next;
            }
            return maximum;
        }
        public int? Length()
        {
            Node node = Head;
            int length = 0;
            while (node != null)
            {
                length++;
                node = node.Next;
            }
            return length;
        }
        public int? ValueAtPoint(int value)
        {
            Node node = Head;
            int count = 0;
            while (node != null) 
            {
                if (count == value)
                {
                    return node.Value;
                }
                else
                {
                    count ++;
                    node = node.Next;
                }
            }
            return null;
        }
        public void Intersection(LinkedList dieList)
        {
            Node node = Head;
            while (node != null)
            {
                int? length = dieList.Length();
                for (int count = 0; count < length; count++)
                {
                    int? value = ValueAtPoint(count);
                    if (node.Value == value)
                    {
                        dieList.RemoveAll(node.Value);
                        break;
                    }
                    //odstranit ten prvek
                }
            }
        }

        // DONE: Najít maximum

        // DONE: odebrat prvek z konce

        // DONE: najít prvek a vrátit True nebo False, jestli tam je (Exist)

        // DONE: odebrat prvek dané hodnoty v celém seznamu (RemoveAll)

        // TODO: destruktivní sjednocení(Union)

        // TODO: destruktivní průnik(Intersection)
    }
}
