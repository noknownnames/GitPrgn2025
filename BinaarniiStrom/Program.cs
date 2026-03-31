using System.Collections;
using System.Xml.Linq;

namespace BinaarniiStrom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree<Student> tree = new BinarySearchTree<Student>();

            using (StreamReader streamReader = new StreamReader("../../../../studenti_shuffled.csv"))
            {
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    string[] studentData = line.Split(',');

                    Student student = new Student(
                        Convert.ToInt32(studentData[0]),    // Id
                        studentData[1],                     // Jméno
                        studentData[2],                     // Příjmení
                        Convert.ToInt16(studentData[3]),    // Věk
                        studentData[4]);                    // Třída

                    // vložíme studenta do stromu, jako klíč slouží jeho Id
                    tree.Insert(student.Id, student);
                    line = streamReader.ReadLine();
                }
            }
            // Najděte studenta s ID 20 (David Urban (ID: 20) ze třídy 4.A)
            Student zak = tree.Find(20).Value;
            Console.WriteLine($"Student s ID 20 je:{zak.FirstName} {zak.LastName} ze třídy {zak.ClassName}"); //TOSLEUTHOUT proč se neukazuje třída?????
            // Najděte studenta s nejnižším ID (Kateřina Sedláček (ID: 1) ze třídy 1.B)
            zak = tree.Min().Value;
            Console.WriteLine($"Student s nejnižším IDje:{zak.FirstName} {zak.LastName} ze třídy {zak.ClassName}");
            // Vložte vlastního studenta s ID > 100 (je potřeba vytvořit nový objekt typu Student) a zkuste ho pak najít
            zak = new Student(101, "Martin", "Mastný", 13, "AlbaquerqueNM");
            tree.Insert(101, zak);
            zak = tree.Find(101).Value;
            Console.WriteLine($"Student s ID 20 je:{zak.FirstName} {zak.LastName} ze třídy {zak.ClassName}");
            //(ne)Smažte všechny studenty se sudým ID
            for (int i = 0; i < 101; i += 2)
            {
                if (tree.Exists(i))
                    {
                        tree.Remove(i);
                    }
            }
            // Vypište strom (měli byste vidět jen ID lichá a seřazená)
            tree.Write();
        }
    }

    class BinarySearchTree<T>
    {
        public Node<T> Root;

        public void Insert(int newKey, T newValue)
        {
            void _insert(Node<T> node, int newKey, T newValue)
            {
                if (newKey < node.Key) // jdeme doleva
                    if (node.LeftSon == null)
                    {
                        node.LeftSon = new Node<T>(newKey, newValue);
                        node.LeftSon.Father = node;
                    }
                    else
                        _insert(node.LeftSon, newKey, newValue);
                else if (newKey > node.Key) // jdeme doprava
                    if (node.RightSon == null)
                    {
                        node.RightSon = new Node<T>(newKey, newValue);
                        node.RightSon.Father = node;
                    }
                    else
                        _insert(node.RightSon, newKey, newValue);
                else // našli jsme náš klíč, což bychom neměli, mají být unikátní.... :/
                    throw new Exception(); // vyhodíme chybu
            }

            if (Root == null) // pokud ještě není definován kořen
                Root = new Node<T>(newKey, newValue);
            else
                _insert(Root, newKey, newValue);
        }
        public bool Exists(int searchedNodeKey)
        {
            bool exists = false;
            void _find(Node<T> node, int searchedKey)
            {
                if (node.Key == searchedKey)
                    exists = true;
                if (searchedKey < node.Key) 
                    if (node.LeftSon != null)
                        _find(node.LeftSon,searchedKey);
                if (searchedKey > node.Key) 
                    if (node.RightSon != null)
                        _find(node.RightSon, searchedKey);
            }
            if(Root!= null)
                _find(Root, searchedNodeKey);
            return exists;
        }
        public Node<T> Find(int searchedNodeKey)
        {
            Node<T> found = default(Node<T>);
            void _find(Node<T> node, int searchedKey)
            {
                if (node.Key == searchedKey)
                    found = node;
                if (searchedKey < node.Key)
                    if (node.LeftSon != null)
                        _find(node.LeftSon, searchedKey);
                if (searchedKey > node.Key)
                    if (node.RightSon != null)
                        _find(node.RightSon, searchedKey);
            }
            _find(Root, searchedNodeKey);
            return found;
        }
        public void Write()
        {
            int maxkey = Max().Key;
            for(int i =0;i<=maxkey;i++)
            {
                if(Exists(i))
                {
                    Node<T> node = Find(i);
                    Console.WriteLine($"{node.Key};{node.Value}");
                }
            }
        }
        public void Remove(int removeKey)
        {
            if(!Exists(removeKey))
                throw new Exception("L(> ~ <)Γ ERROR: nelze odstranit prvek, který se nenachází ve stromu v první řadě!");
            else
            {
                Queue<Node<T>> orphans = new Queue<Node<T>>();
                _kill(Root, removeKey);
                while (orphans.Count > 0)
                {
                    Node<T> node = orphans.Dequeue();
                    Insert(node.Key, node.Value);
                }
                void _enqueueChildren(Node<T> node)
                {
                    if (node.LeftSon != null)
                    {
                        orphans.Enqueue(node.LeftSon);
                        _enqueueChildren(node.LeftSon);
                    }
                    if (node.RightSon != null)
                    {
                        orphans.Enqueue(node.RightSon);
                        _enqueueChildren(node.RightSon);
                    }
                }
                void _kill(Node<T> node, int removekey)
                {
                    if(node.Key == removekey)
                    {
                        _enqueueChildren(node);
                        if (node.Father != null)
                        {
                            if (node==node.Father.LeftSon)
                            {
                                node.Father.LeftSon = null;
                            }
                            else
                            {
                                node.Father.RightSon = null;
                            }
                        }
                        else
                            Root = null;
                    }
                    else if (node.Key < removekey)
                    {
                        if(node.LeftSon != null)
                        {
                            _kill(node.LeftSon, removekey);
                        }
                    }
                    else if (node.Key > removekey)
                    {
                        if (node.RightSon != null)
                        {
                            _kill(node.RightSon, removekey);
                        }
                    }
                }
            }
        }
        public Node<T> Min()
        {
            Node<T> _min(Node<T> node)
            {
                if (node.LeftSon == null)
                    return node;
                return _min(node.LeftSon);
            }

            return _min(Root);

        }
        public Node<T> Max()
        {
            Node<T> _max(Node<T> node)
            {
                if (node.RightSon == null)
                    return node;
                return _max(node.RightSon);
            }

            return _max(Root);

        }
    }
    class Node<T> // T může být libovolný typ
    {
        public Node(int key, T value)
        {
            Key = key;
            Value = value;
        }
        public int Key;
        public T Value;

        public Node<T> LeftSon;
        public Node<T> RightSon;
        public Node<T> Father;



    }
    class Student
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }

        public string ClassName { get; }

        public Student(int id, string firstName, string lastName, int age, string className)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            ClassName = className;
        }

        // aby se nám při Console.WriteLine(student) nevypsala jen nějaká adresa v paměti,
        // upravíme výpis objektu typu student na něco čitelného
        public override string ToString()
        {
            return string.Format("{0} {1} (ID: {2}) ze třídy {3}", FirstName, LastName, Id, ClassName);
        }
    }


}

