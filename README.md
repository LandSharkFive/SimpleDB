# Simple DB: C# CSV & Flat File Engine

**Simple DB** is a beginner-friendly yet powerful C# library designed to manage CSV files—the "bread and butter" of the IT industry. It provides a straightforward way to handle data persistence without the overhead of a full database server.



## 🚀 Overview
While simple to implement, this engine uses **LINQ for batch processing** and supports both **Internal and External Sorting** to handle files that exceed system memory.

* **Easy to Use:** Minimal setup for CRUD operations.
* **Light Indexing:** Optimized for "Light Indexing" (1 index per 100 rows) to balance speed and memory.
* **Powerful Features:** Includes column extraction, row selection, and file duplication.

---

## 📊 Performance & Complexity

| Rows | Inserts/Sec | Memory |
| :--- | :--- | :--- |
| 100 | 1,000 | 5 MB |
| 5K | 333 | 20 MB |
| 10K | 320 | 20 MB |
| 50K | 200 | 20 MB |
| 100K | 50 | 20 MB |

### Technical Specs
* **Time Complexity:** Search/Index/Insert/Delete is $O(N)$.
* **Space Complexity:** $O(N)$ where $N$ = Rows.Count.
* **Optimal File Size:** Works best for files **< 5 MB**. For larger datasets, processing speed may decrease due to linear scanning.

---

## 🛠 Installation & Requirements
This is a **C# Console-Mode Project**. 

1. Clone the repository.
2. Open the solution in **Visual Studio 2022** or later.
3. Build and Run.

---

## 💡 Quick Start: Optimized Implementation

Using a `Dictionary` allows you to move from simple row-reading to high-speed data retrieval.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class SimpleDBExample
{
    public void ProcessData()
    {
        // 1. Read and Batch Process with LINQ
        var rows = File.ReadLines("data.csv")
                       .Skip(1) // Skip Header
                       .Select(line => line.Split(','))
                       .ToList();

        // 2. Optimization: Use a Dictionary for O(1) Lookups
        Dictionary<string, string[]> dbMap = rows.ToDictionary(r => r[0], r => r);

        // 3. Fast Retrieval
        if (dbMap.TryGetValue("ID_123", out string[] record))
        {
            Console.WriteLine($"Found: {string.Join(" | ", record)}");
        }
    }
}
