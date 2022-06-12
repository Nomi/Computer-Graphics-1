using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Graphics_1.Lab5.Helpers.Filling
{
    public class EdgeTable
    {
        private Dictionary<int, List<EdgeEntry>> table;

        public EdgeTable()
        {
            table = new Dictionary<int, List<EdgeEntry>>();
        }

        public bool isEmpty()
        {
            return table.Count == 0;
        }

        public bool containsKey(int key)
        {
            return table.ContainsKey(key);
        }

        public void add(int key, EdgeEntry entry)
        {
            if (containsKey(key))
            {
                table[key].Add(entry);
            }
            else
            {
                List<EdgeEntry> list = new List<EdgeEntry>();
                list.Add(entry);
                table.Add(key, list);
            }
        }

        public List<EdgeEntry> pop(int key) //remove elment and get removed element.
        {
            List<EdgeEntry> toReturn = new List<EdgeEntry>(table[key]);
            table.Remove(key);
            return toReturn;
        }

        public int size()
        {
            return table.Count();
        }
    }
}
