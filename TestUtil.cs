using System.Text;

namespace SimpleDB
{
    public class TestUtil
    {
        private static Random random = new Random();

        public static int GetRandomInt(int max)
        {
            return random.Next(max);
        }


        public static string GetRandomString(int length)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

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

    }
}
