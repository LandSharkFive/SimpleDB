using System.Text;

namespace SimpleDB
{
    public class TestUtil
    {
        private static Random random = new Random();

        public static int GetRandomInt(int a)
        {
            return random.Next(a);
        }


        public static string GetRandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(chars[random.Next(chars.Length)]);
            }
            return sb.ToString();
        }



        public static void GetRandomFile(string outFile, int column, int length)
        {
            using (StreamWriter writer = new StreamWriter(outFile))
            {
                for (int i = 0; i < length; i++)
                {
                    writer.WriteLine(GetRandomLine(column));
                }
            }
        }

        public static void GetRandomFileInt(string outFile, int column, int length)
        {
            using (StreamWriter writer = new StreamWriter(outFile))
            {
                for (int i = 0; i < length; i++)
                {
                    writer.WriteLine(GetRandomLineInt(column));
                }
            }
        }


        public static string GetRandomLine(int column)
        {
            StringBuilder sb = new StringBuilder();
            int k = column - 1;
            for (int i = 0; i < k; i++)
            {
                sb.Append(GetRandomString(8));
                sb.Append('|');
            }
            sb.Append(GetRandomString(8));
            return sb.ToString();
        }

        public static string GetRandomLineInt(int column)
        {
            StringBuilder sb = new StringBuilder();
            int k = column - 1;
            for (int i = 0; i < k; i++)
            {
                sb.Append(GetRandomInt(10000));
                sb.Append('|');
            }
            sb.Append(GetRandomInt(10000));
            return sb.ToString();
        }

        public static void GetDistinctFile(string outFile)
        {
            List<string> list = new List<string>();
            for (int i=0; i < 40; i++)
            {
                list.Add(GetRandomString(2));
            }

            using (StreamWriter writer = new StreamWriter(outFile))
            {
                for (int i = 0; i < 200; i++)
                {
                    string a = list[GetRandomInt(10)];
                    string b = list[GetRandomInt(20)];
                    string c = list[GetRandomInt(30)];
                    string value = string.Format("{0}|{1}|{2}", a, b, c);
                    writer.WriteLine(value);
                }
            }
        }

        public static void GetDistinctFileInt(string outFile)
        {
            using (StreamWriter writer = new StreamWriter(outFile))
            {
                for (int i = 0; i < 200; i++)
                {
                    int a = random.Next(15);
                    int b = random.Next(19);
                    int c = random.Next(27);
                    string value = string.Format("{0}|{1}|{2}", a, b, c);
                    writer.WriteLine(value);
                }
            }
        }


    }
}
