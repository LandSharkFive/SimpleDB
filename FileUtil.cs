using System.Text;

namespace SimpleDB
{
    public static class FileUtil
    {

        // Large Files
        public static void AppendFile(string filePath, string data)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(data);
            }
        }

        /// <summary>
        /// Insert data by Id.
        /// </summary>
        /// <param name="inputFile">string</param>
        /// <param name="id">string</param>
        /// <param name="data">string</param>
        public static void InsertById(string inputFile, string id, string data)
        {
            if (!File.Exists(inputFile))
            {
                return;
            }

            string outputFile = "TEMP1.txt";
            File.Delete(outputFile);

            using (StreamReader reader = new StreamReader(inputFile))
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                bool found = false;
                int count = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    count++;
                    string result = GetId(line);
                    if (found)
                    {
                        writer.WriteLine(line);
                    }
                    else
                    {
                        int k = result.CompareTo(id);
                        if (k == 0)
                        {
                            // no duplicates
                            found = true;
                            writer.WriteLine(line);
                        }
                        else if (k == 1)
                        {
                            // insert
                            found = true;
                            writer.WriteLine(data);
                            writer.WriteLine(line);
                        }
                        else
                        {
                            writer.WriteLine(line);
                        }
                    }
                }

                if (!found)
                {
                    // End of file
                    writer.WriteLine(data);
                }
            }

            // save changes
            File.Delete(inputFile);
            File.Move(outputFile, inputFile);
        }


        /// <summary>
        /// Update by Id.
        /// </summary>
        /// <param name="inputFile">string</param>
        /// <param name="id">string</param>
        /// <param name="column">int[]</param>
        /// <param name="data">string[]</param>
        public static void UpdateById(string inputFile, string id, int[] column, string[] data)
        {
            if (!File.Exists(inputFile))
            {
                return;
            }

            string outputFile = "TEMP1.txt";
            File.Delete(outputFile);

            using (StreamReader reader = new StreamReader(inputFile))
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                int count = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    count++;
                    string result = GetId(line);
                    if (result.CompareTo(id) == 0)
                    {
                        string row = ReplaceFields(line, column, data);
                        writer.WriteLine(row);
                    }
                    else
                    {
                        writer.WriteLine(line);
                    }
                }
            }

            // save changes
            File.Delete(inputFile);
            File.Move(outputFile, inputFile);
        }


        /// <summary>
        /// Delete by Id.
        /// </summary>
        /// <param name="inputFile">string</param>
        /// <param name="id">string</param>
        public static void DeleteById(string inputFile, string id)
        {
            if (!File.Exists(inputFile))
            {
                return;
            }

            string outputFile = "TEMP1.txt";
            File.Delete(outputFile);

            using (StreamReader reader = new StreamReader(inputFile))
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                int count = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    count++;
                    string result = GetId(line);
                    if (result.CompareTo(id) != 0)
                    {
                        writer.WriteLine(line);
                    }
                }
            }

            // save changes
            File.Delete(inputFile);
            File.Move(outputFile, inputFile);
        }

        public static void InsertOrUpdateById(string inputFile, string id, string data)
        {
            if (!File.Exists(inputFile))
            {
                return;
            }

            string outputFile = "TEMP1.txt";
            File.Delete(outputFile);

            using (StreamReader reader = new StreamReader(inputFile))
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                int count = 0;
                bool found = false;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    count++;
                    if (found)
                    {
                        writer.WriteLine(line);
                    }
                    else
                    {
                        string result = GetId(line);
                        int k = result.CompareTo(id);
                        if (k == 0)
                        {
                            // update
                            found = true;
                            writer.WriteLine(data);
                        }
                        else if (k == 1)
                        {
                            // insert
                            found = true;
                            writer.WriteLine(data);
                            writer.WriteLine(line);
                        }
                        else
                        {
                            writer.WriteLine(line);
                        }
                    }
                }

                if (!found)
                {
                    // Emd of File
                    writer.WriteLine(data);
                }
            }

            // save changes
            File.Delete(inputFile);
            File.Move(outputFile, inputFile);
        }


        /// <summary>
        /// Replace Fields. 
        /// </summary>
        /// <param name="input">string</param>
        /// <param name="column">int[]</param>
        /// <param name="data">data[]</param>
        /// <returns></returns>
        public static string ReplaceFields(string input, int[] column, string[] data)
        {
            string[] field = input.Split('|');
            for (int i = 0; i < column.Length; i++)
            {
                field[column[i]] = data[i];
            }
            return string.Join('|', field);
        }

        /// <summary>
        /// Get Count.
        /// </summary>
        /// <param name="filePath">string</param>
        /// <returns>int</returns>
        public static int GetCount(string filePath)
        {
            int count = 0;
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Find By Id.
        /// </summary>
        /// <param name="filePath">string</param>
        /// <param name="id">string</param>
        /// <returns>bool</returns>
        public static bool FindById(string filePath, string id)
        {
            int count = 0;
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    count++;
                    string result = GetId(line);
                    if (result.CompareTo(id) == 0)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Get Id
        /// </summary>
        /// <param name="input">string</param>
        /// <returns>string</returns>
        public static string GetId(string input)
        {
            int t = input.IndexOf('|');
            if (t != -1)
            {
                return input.Substring(0, t);
            }
            return input;
        }


        public static string GetColumn(string input, int column)
        {
            string[] field = input.Split('|');
            if (column < field.Length)
            {
                return field[column];
            }
            return string.Empty;
        }

        /// <summary>
        /// Search By Column.
        /// </summary>
        /// <param name="filePath">string</param>
        /// <param name="target">string</param>
        /// <param name="column">int</param>
        /// <returns>bool</returns>
        public static bool SearchByColumn(string filePath, string target, int column)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                int counter = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    counter++;
                    string[] field = line.Split('|');
                    if (column < field.Length && field[column].Trim().CompareTo(target) == 0)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Show file.
        /// </summary>
        /// <param name="filePath"></param>
        public static void Show(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return;
            }

            int count = 0;
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    count++;
                    Console.WriteLine(line);
                }
            }
        }

        public static void SelectById(string inputFile, string outputFile, string id)
        {
            if (!File.Exists(inputFile))
            {
                return;
            }

            int count = 0;
            using (StreamReader reader = new StreamReader(inputFile))
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    count++;
                    string result = GetId(line);
                    if (result.CompareTo(id) == 0)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
        }

        public static void SelectByColumn(string inputFile, string outputFile, string target, int column)
        {
            if (!File.Exists(inputFile))
            {
                return;
            }

            using (StreamReader reader = new StreamReader(inputFile))
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                string line;
                int counter = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    counter++;
                    string[] field = line.Split('|');
                    if (column < field.Length && field[column].Trim().CompareTo(target) == 0)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
        }

        /// <summary>
        /// Print Tabs
        /// </summary>
        /// <param name="line">string</param>
        public static void PrintTabs(string line)
        {
            string row = line.Replace('|', '\t');
            Console.WriteLine(row);
        }

        /// <summary>
        /// Sort By Id.
        /// </summary>
        /// <param name="inputFile">string</param>
        /// <param name="outputFile">string</param>
        public static void SortById(string inputFile, string outputFile)
        {
            if (!File.Exists(inputFile))
            {
                return;
            }

            var list = new List<KeyValuePair<string, string>>();
            using (StreamReader reader = new StreamReader(inputFile))
            {
                int counter = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    counter++;
                    list.Add(new KeyValuePair<string, string>(GetId(line), line));
                }
            }

            var row = list.DistinctBy(x => x.Key).OrderBy(x => x.Key).Select(x => x.Value).ToList();
            File.Delete(outputFile);

            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                foreach (var value in row)
                {
                    writer.WriteLine(value);
                }
            }
        }

        /// <summary>
        /// Sort by integer Id.
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="outputFile"></param>
        public static void SortByIntId(string inputFile, string outputFile)
        {
            if (!File.Exists(inputFile))
            {
                return;
            }

            var list = new List<KeyValuePair<int, string>>();
            using (StreamReader reader = new StreamReader(inputFile))
            {
                int counter = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    counter++;
                    list.Add(new KeyValuePair<int, string>(Convert.ToInt32(GetId(line)), line));
                }
            }

            var row = list.DistinctBy(x => x.Key).OrderBy(x => x.Key).Select(x => x.Value).ToList();
            File.Delete(outputFile);

            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                foreach (var value in row)
                {
                    writer.WriteLine(value);
                }
            }
        }


        /// <summary>
        /// Sort By Column.
        /// </summary>
        /// <param name="inputFile">string</param>
        /// <param name="outputFile">string</param>
        /// <param name="column">int</param>
        public static void SortByColumn(string inputFile, string outputFile, int column)
        {
            if (!File.Exists(inputFile))
            {
                return;
            }

            var list = new List<KeyValuePair<string, string>>();
            using (StreamReader reader = new StreamReader(inputFile))
            {
                int counter = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    counter++;
                    list.Add(new KeyValuePair<string, string>(GetColumn(line, column), line));
                }
            }

            var row = list.OrderBy(x => x.Key).Select(x => x.Value).ToList();
            File.Delete(outputFile);

            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                foreach (var value in row)
                {
                    writer.WriteLine(value);
                }
            }
        }

        /// <summary>
        /// Sort by integer column.
        /// </summary>
        /// <param name="inputFile">string</param>
        /// <param name="outputFile">string</param>
        /// <param name="column">int</param>
        public static void SortByIntColumn(string inputFile, string outputFile, int column)
        {
            if (!File.Exists(inputFile))
            {
                return;
            }

            var list = new List<KeyValuePair<int, string>>();
            using (StreamReader reader = new StreamReader(inputFile))
            {
                int counter = 0;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    counter++;
                    list.Add(new KeyValuePair<int, string>(Convert.ToInt32(GetColumn(line, column)), line));
                }
            }

            var row = list.OrderBy(x => x.Key).Select(x => x.Value).ToList();
            File.Delete(outputFile);

            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                foreach (var value in row)
                {
                    writer.WriteLine(value);
                }
            }
        }

        /// <summary>
        /// Project columns.
        /// </summary>
        /// <param name="inputFile">string</param>
        /// <param name="outputFile">string</param>
        /// <param name="column">int[]</param>
        public static void ExtractColumns(string inputFile, string outputFile, int[] column)
        {
            if (!File.Exists(inputFile))
            {
                return;
            }

            using (StreamReader reader = new StreamReader(inputFile))
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                string line;
                int counter = 0;
                StringBuilder sb = new StringBuilder();

                while ((line = reader.ReadLine()) != null)
                {
                    counter++;
                    sb.Clear();
                    for (int i = 0; i < column.Length; i++)
                    {
                        sb.Append(GetColumn(line, column[i]));
                        if (i < column.Length - 1)
                        {
                            sb.Append("|");
                        }
                    }
                    writer.WriteLine(sb.ToString());
                }
            }
        }


    }
}
