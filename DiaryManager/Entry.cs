using System;

namespace DiaryManager
{
    public class Entry
    {
        public DateTime Date { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return $"\n{Date:yyyy-MM-dd}\n{Content}\n";
        }
    }
}
