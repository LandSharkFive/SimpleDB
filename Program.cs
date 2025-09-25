using System.Diagnostics;

namespace SimpleDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // T2();
        }


        static void T1()
        {
            var timer = new Stopwatch();
            timer.Start();

            // example

            timer.Stop();

            TimeSpan myTime = timer.Elapsed;
            Console.Write(myTime.TotalMilliseconds);
            Console.WriteLine(" ms");
        }

        static void T2()
        {
            var a = IndexUtil.MemIndexById("b.csv", 1);
            foreach (string s in a)
            {
                Console.WriteLine(s);
            }
        }

        static void T3()
        {
            IndexUtil.IndexById("b.csv", "c.csv", 1);
        }

        static void M1()
        {
            string[] x = { "e", "c", "a", "d", "g", "s" };
            File.WriteAllLines("a.txt", x);
        }

        static void M2()
        {
            string[] x = { "e|g|d", "c|y|u", "a|w|e", "d|n|k", "g|q|c", "s|a|p" };
            File.WriteAllLines("a.txt", x);
        }

        static void M3()
        {
            List<string> x = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                x.Add(i.ToString());
            }
            File.WriteAllLines("a.txt", x);
        }

        static void M4()
        {
            string input = "a.csv";
            string output = "b.csv";

            TestUtil.GetRandomFile(input, 4, 100);
            FileUtil.SortById(input, output);
        }

        static void M5()
        {
            string input = "a.csv";
            string output = "b.csv";

            TestUtil.GetRandomFileInt(input, 4, 100);
            FileUtil.SortByIntId(input, output);
        }

        static void M6()
        {
            List<Person> list = new List<Person>();
            list.Add(new Person(1, "Bob", "Barker", "123 Windy Hill Rd", "Salt Lake", "NV", "US", "21343", "(212) 432-8333"));
            list.Add(new Person(2, "Sam", "Smith", "42 Albemarle Way", "Washington", "DC", "US", "23912", "(433) 933-9343"));
            list.Add(new Person(3, "Barbera", "Walters", "148 Hulbert Fields", "Dalewood", "VA", "US", "34331", "(833) 341-1243"));
            list.Add(new Person(4, "Homer", "Simpson", "5301 Simpson Way", "Las Vegas", "NV", "US", "39393", "(813) 421-4201"));
            list.Add(new Person(5, "Sam", "Donaldson", "1103 Shady lane", "Binford", "VT", "US", "31313", "(401) 201-2402"));

            List<string> row = new List<string>();
            foreach (var p in list)
            {
                row.Add(PersonHelper.WriteLine(p));
            }

            File.WriteAllLines("people.csv", row);
        }

    }
}
