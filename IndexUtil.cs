using System.Text;

namespace SimpleDB
{
    public static class IndexUtil
    {

        public static void IndexById(string inputFile, string outputFile, int frequency)
        {
            if (frequency <= 0)
            {
                return;
            }

            if (!File.Exists(inputFile))
            {
                return;
            }

            using (StreamReader reader = new StreamReader(inputFile))
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                int count = 0;
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

        public static List<Index> GetIndex(string inputFile, int frequency)
        {
            if (frequency <= 0)
            {
                return new List<Index>();
            }

            if (!File.Exists(inputFile))
            {
                return new List<Index>();
            }

            List<Index> list = new List<Index>();
            using (StreamReader reader = new StreamReader(inputFile))
            {
                int count = 0;
                long position = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    count++;
                    if (frequency == 1 || count % frequency == 1)
                    {
                        string[] field = line.Split('|');
                        string id = field[0];
                        list.Add(new Index(id, position));
                    }
                    position += Encoding.UTF8.GetByteCount(line) + Environment.NewLine.Length;
                }
            }
            return list;
        }

        public static List<IndexInt> GetIndexInt(string inputFile, int frequency)
        {
            if (frequency <= 0)
            {
                return new List<IndexInt>();
            }

            if (!File.Exists(inputFile))
            {
                return new List<IndexInt>();
            }

            List<IndexInt> list = new List<IndexInt>();
            using (StreamReader reader = new StreamReader(inputFile))
            {
                int count = 0;
                long position = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    count++;
                    if (frequency == 1 || count % frequency == 1)
                    {
                        string[] field = line.Split('|');
                        int id = Convert.ToInt32(field[0]);
                        list.Add(new IndexInt(id, position));
                    }
                    position += Encoding.UTF8.GetByteCount(line) + Environment.NewLine.Length;
                }
            }
            return list;
        }

        public static bool SearchById(List<Index> a, string id)
        {
            return a.Exists(x => string.Compare(x.Id, id, true) == 0);
        }

        public static long GetPosition(List<Index> a, string id)
        {
            long position = 0;
            foreach (Index item in a)
            {
                int k = string.Compare(item.Id, id, true);
                if (k == 0)
                {
                    position = item.Position;
                    break;
                }
                else if (k == 1)
                {
                    break;
                }
                position = item.Position;
            }
            return position;
        }

        public static bool SearchById(List<IndexInt> a, int id)
        {
            return a.Exists(x => x.Id == id);
        }

        public static long GetPosition(List<IndexInt> a, int id)
        {
            long position = 0;
            foreach (IndexInt item in a)
            {
                if (item.Id == id)
                {
                    position = item.Position;
                    break;
                }
                else if (item.Id > id)
                {
                    break;
                }
                position = item.Position;
            }
            return position;
        }

        public static bool SearchById(string inputFile, string id)
        {
            using (StreamReader reader = new StreamReader(inputFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.Compare(FileUtil.GetId(line), id, true) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static long GetPosition(string inputFile, string id)
        {
            long position = 0;
            using (StreamReader reader = new StreamReader(inputFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] field = line.Split('|');
                    int k = string.Compare(field[0], id, true);
                    if (k == 0)
                    {
                        position = Convert.ToInt64(field[1]);
                        return position;
                    }
                    else if (k == 1)
                    {
                        break;
                    }
                    position = Convert.ToInt64(field[1]);
                }
            }
            return position;
        }


    }
}
