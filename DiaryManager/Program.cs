using System;

namespace DiaryManager
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "mydiary.txt" ;
            DailyDiary diary = new DailyDiary(filePath);
            diary.Run();
        }
    }
}
