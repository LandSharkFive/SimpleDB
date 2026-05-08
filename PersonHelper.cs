using System.Text;

namespace SimpleDB
{
    public static class PersonHelper
    {
        public static Person ReadLine(string input)
        {
            Person a = new Person();
            string[] field = input.Split('|');
            if (field.Length >= 0)
            {
                a.Id = Convert.ToInt32(field[0]);
            }
            if (field.Length >= 1)
            {
                a.FirstName = field[1].Trim();
            }
            if (field.Length >= 2)
            {
                a.LastName = field[2].Trim();
            }
            if (field.Length >= 3)
            {
                a.Address = field[3].Trim();
            }
            if (field.Length >= 4)
            {
                a.City = field[4].Trim();
            }
            if (field.Length >= 5)
            { 
                a.State = field[5].Trim();
            }
            if (field.Length >= 6)
            {
                a.Country = field[6].Trim();
            }
            if (field.Length >= 7)
            {
                a.PostalCode = field[7].Trim();
            }
            if (field.Length >= 8)
            {
                a.Phone = field[8].Trim();
            }
            return a;
        }

        public static string WriteLine(Person a)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(a.Id);
            sb.Append('|');
            sb.Append(a.FirstName);
            sb.Append('|');
            sb.Append(a.LastName);
            sb.Append('|');
            sb.Append(a.Address);
            sb.Append('|');
            sb.Append(a.City);
            sb.Append('|');
            sb.Append(a.State);
            sb.Append('|');
            sb.Append(a.Country);
            sb.Append('|');
            sb.Append(a.PostalCode);
            sb.Append('|');
            sb.Append(a.Phone);
            return sb.ToString();
        }

    }
}
