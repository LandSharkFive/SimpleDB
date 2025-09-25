using System.Text;

namespace SimpleDB
{
    public static class IndexUtil
    {

        public static List<string> MemIndexById(string inputFile, int frequency)
        {
            var list = new List<string>();

            int count = 0;
            using (StreamReader reader = new StreamReader(inputFile))
            {
                StringBuilder sb = new StringBuilder();
                long position = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    count++;
                    if (frequency == 1 || count % frequency == 1)
                    {
                        sb.Clear();
                        sb.Append(FileUtil.GetId(line));
                        sb.Append('|');
                        sb.Append(position);
                        list.Add(sb.ToString());
                    }
                    position += Encoding.UTF8.GetByteCount(line) + Environment.NewLine.Length;
                }
            }
            return list;
        }

        public static void IndexById(string inputFile, string outputFile, int frequency)
        {
            int count = 0;
            using (StreamReader reader = new StreamReader(inputFile))
            using (StreamWriter writer = new StreamWriter(outputFile))  
            {
                StringBuilder sb = new StringBuilder();
                long position = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    count++;
                    if (frequency == 1 || count % frequency == 1)
                    {
                        sb.Clear();
                        sb.Append(FileUtil.GetId(line));
                        sb.Append('|');
                        sb.Append(position);
                        writer.WriteLine(sb.ToString());
                    }
                    position += Encoding.UTF8.GetByteCount(line) + Environment.NewLine.Length;
                }
            }
        }


    }
}
