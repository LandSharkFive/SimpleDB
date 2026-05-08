
namespace SimpleDB
{
    public static class SortUtil
    {
        // External Sorting Functions
        // Ref: "Sorting enormous files using C# external merge sort", Chris Hulbert, Dec 2011, Splinter.com, github.com/chrishulbert/ExternalMergeSort

        #region Sort By String

        /// <summary>
        /// External Merge Sort For Strings
        /// </summary>
        /// <param name="inputFile">string</param>
        /// <param name="outputFile">string</param>
        public static void MergeSort(string inputFile, string outputFile)
        {
            DeleteTempFiles();
            SplitFile(inputFile);
            MergeFiles(outputFile);
            DeleteTempFiles();
        }

        private static void SplitFile(string inputFile)
        {
            int splitCount = 1;
            int MaxRows = 100000;
            string outputFile = string.Format("TEMP{0:d5}.txt", splitCount);
            List<string> list = new List<string>();
            long count = 0;
            using (StreamReader sr = new StreamReader(inputFile))
            {
                while (sr.Peek() >= 0)
                {
                    count++;
                    list.Add(sr.ReadLine());

                    if (count > MaxRows && sr.Peek() >= 0)
                    {
                        count = 0;
                        list = list.OrderBy(x => GetId(x)).ToList();
                        File.WriteAllLines(outputFile, list);
                        list.Clear();
                        splitCount++;
                        outputFile = string.Format("TEMP{0:d5}.txt", splitCount);
                    }
                }
                list = list.OrderBy(x => GetId(x)).ToList();
                File.WriteAllLines (outputFile, list);
                list.Clear();
            }
        }

        /// <summary>
        /// Get Id
        /// </summary>
        /// <param name="input">string</param>
        /// <returns>string</returns>
        private static string GetId(string input)
        {
            string[] field = input.Split('|');
            return field[0];
        }


        private static void MergeFiles(string outputFile)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string[] paths = Directory.GetFiles(currentDirectory, "TEMP*.txt");
            int chunks = paths.Length; // Number of chunks

            // Open the files
            StreamReader[] readers = new StreamReader[chunks];
            for (int i = 0; i < chunks; i++)
            {
                readers[i] = new StreamReader(paths[i]);
            }

            // Make queues
            Queue<string>[] queues = new Queue<string>[chunks];
            for (int i = 0; i < chunks; i++)
            {
                queues[i] = new Queue<string>();
            }

            // Load queues
            for (int i = 0; i < chunks; i++)
            {
                LoadQueue(queues[i], readers[i]);
            }

            // Merge
            StreamWriter sw = new StreamWriter(outputFile);
            bool done = false;
            int lowest_index;
            string low;
            while (!done)
            {
                // Find the chunk with the lowest value
                lowest_index = -1;
                low = string.Empty;
                for (int j = 0; j < chunks; j++)
                {
                    if (queues[j] != null)
                    {
                        if (lowest_index < 0 || String.CompareOrdinal(queues[j].Peek(), low) < 0)
                        {
                            lowest_index = j;
                            low = queues[j].Peek();
                        }
                    }
                }

                // Nothing found? We must be done.
                if (lowest_index == -1) 
                { 
                    done = true; 
                    break; 
                }

                // Write to output file.
                sw.WriteLine(low);

                // Remove item from queue.
                queues[lowest_index].Dequeue();
                // if queue is empty, reload it.
                if (queues[lowest_index].Count == 0)
                {
                    LoadQueue(queues[lowest_index], readers[lowest_index]);
                    // If queue is empty?
                    if (queues[lowest_index].Count == 0)
                    {
                        queues[lowest_index] = null;
                    }
                }
            }
            sw.Close();

            // Close and delete the files
            for (int i = 0; i < chunks; i++)
            {
                readers[i].Close();
                File.Delete(paths[i]);
            }
        }

        #endregion

        #region Sort by Integers

        /// <summary>
        /// External Merge Sort For Integers
        /// </summary>
        /// <param name="inputFile">string</param>
        /// <param name="outputFile">string</param>
        public static void MergeSortInt(string inputFile, string outputFile)
        {
            DeleteTempFiles();
            SplitFileInt(inputFile);
            MergeFilesInt(outputFile);
            DeleteTempFiles();
        }

        private static void SplitFileInt(string inputFile)
        {
            int splitCount = 1;
            int MaxRows = 100000;
            string outputFile = string.Format("TEMP{0:d5}.txt", splitCount);
            List<string> list = new List<string>();
            long count = 0;
            using (StreamReader sr = new StreamReader(inputFile))
            {
                while (sr.Peek() >= 0)
                {
                    count++;
                    list.Add(sr.ReadLine());

                    if (count > MaxRows && sr.Peek() >= 0)
                    {
                        count = 0;
                        list = list.OrderBy(x => GetIdInt(x)).ToList();
                        File.WriteAllLines(outputFile, list);
                        list.Clear();
                        splitCount++;
                        outputFile = string.Format("TEMP{0:d5}.txt", splitCount);
                    }
                }
                list = list.OrderBy(x => GetIdInt(x)).ToList();
                File.WriteAllLines(outputFile, list);
                list.Clear();
            }
        }

        /// <summary>
        /// Get Id Integer
        /// </summary>
        /// <param name="input">string</param>
        /// <returns>int</returns>
        private static int GetIdInt(string input)
        {
            string[] field = input.Split('|');
            return Convert.ToInt32(field[0]);
        }


        private static void MergeFilesInt(string outputFile)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string[] paths = Directory.GetFiles(currentDirectory, "TEMP*.txt");
            int chunks = paths.Length; // Number of chunks

            // Open the files
            StreamReader[] readers = new StreamReader[chunks];
            for (int i = 0; i < chunks; i++)
            {
                readers[i] = new StreamReader(paths[i]);
            }

            // Make queues
            Queue<string>[] queues = new Queue<string>[chunks];
            for (int i = 0; i < chunks; i++)
            {
                queues[i] = new Queue<string>();
            }

            // Load queues
            for (int i = 0; i < chunks; i++)
            {
                LoadQueue(queues[i], readers[i]);
            }

            // Merge
            StreamWriter sw = new StreamWriter(outputFile);
            bool done = false;
            int lowest_index;
            string low = string.Empty;
            while (!done)
            {
                // Find the chunk with the lowest value
                lowest_index = -1;
                low = string.Empty;
                for (int j = 0; j < chunks; j++)
                {
                    if (queues[j] != null)
                    {
                        if (lowest_index < 0 || GetIdInt(queues[j].Peek()) < GetIdInt(low))
                        {
                            lowest_index = j;
                            low = queues[j].Peek();
                        }
                    }
                }

                // If nothing is found then we are done.
                if (lowest_index == -1)
                {
                    done = true;
                    break;
                }

                // Write to output file.
                sw.WriteLine(low);

                // Remove item from queue.
                queues[lowest_index].Dequeue();
                // if queue is empty, reload it.
                if (queues[lowest_index].Count == 0)
                {
                    LoadQueue(queues[lowest_index], readers[lowest_index]);
                    // If queue is empty?
                    if (queues[lowest_index].Count == 0)
                    {
                        queues[lowest_index] = null;
                    }
                }
            }
            sw.Close();

            // Close and delete the files
            for (int i = 0; i < chunks; i++)
            {
                readers[i].Close();
                File.Delete(paths[i]);
            }
        }


        #endregion

        private static void DeleteTempFiles()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string[] paths = Directory.GetFiles(currentDirectory, "TEMP*.txt");
            foreach (string name in paths)
            {
                File.Delete(name);
            }
        }

        private static void LoadQueue(Queue<string> queue, StreamReader file)
        {
            int MaxQueueSize = 20;
            for (int i = 0; i < MaxQueueSize; i++)
            {
                if (file.Peek() < 0)
                    break;
                queue.Enqueue(file.ReadLine());
            }
        }


    }
}
