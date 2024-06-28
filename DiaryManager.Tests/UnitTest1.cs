using Xunit;
using System.IO;

namespace DiaryManager.Tests
{
    public class UnitTest1 : IDisposable
    {
        public const string TestDiaryFilePath = "testdiary.txt";

        public UnitTest1()
        {
            // Ensure the file is clean before each test
            ClearFileContent();
        }

        private void ClearFileContent()
        {
            if (File.Exists(TestDiaryFilePath))
            {
                File.WriteAllText(TestDiaryFilePath, string.Empty);
            }
            else
            {
                File.Create(TestDiaryFilePath).Dispose();
            }
        }

        [Fact]
        public void TestReadDiaryFile()
        {
            // Arrange
            File.WriteAllText(TestDiaryFilePath, "2024-06-28\nTest entry");

            // Act
            DailyDiary diary = new DailyDiary(TestDiaryFilePath);
            var result = diary.ReadDiary();

            // Assert
            Assert.Equal("2024-06-28\nTest entry", result);
        }

        [Fact]
        public void TestAddEntry()
        {
            // Arrange
            DailyDiary diary = new DailyDiary(TestDiaryFilePath);
            string entry = "2024-06-28\nTest entry";

            // Act
            int entriesNumberBeforeAdding = diary.CountLines();
            diary.AddEntry(entry);
            int entriesNumberAfterAdding = diary.CountLines();

            // Assert
            Assert.Equal(entriesNumberBeforeAdding + 1, entriesNumberAfterAdding);
        }

        [Fact]
        public void TestCountLines()
        {
            // Arrange
            File.WriteAllText(TestDiaryFilePath, "2024-06-28\nTest entry\n2024-06-29\nAnother entry");

            // Act
            DailyDiary diary = new DailyDiary(TestDiaryFilePath);
            int result = diary.CountLines();

            // Assert
            Assert.Equal(4, result);
        }

        public void Dispose()
        {
            // Clean up after each test
            ClearFileContent();
        }
    }
}
