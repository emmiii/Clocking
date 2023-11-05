using DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static DTO.Enum;

namespace TimeKeeper
{
    // This class saves the time logs to the file
    public static class TimePersister
    { 
        private static readonly string filePath = Path.Combine("C:\\ProgramData\\TimeLog.txt");

        public static void WriteTimeToFile(TimeStamp timeStamp)
        {
            File.AppendAllText(filePath, $"{timeStamp}\n");
        }

        private static string ReadTimesFromFile()
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            else 
            {
                return string.Empty;
            }
        }

        public static (List<TimeStamp>, List<TimeStamp>) GetTimeStamps()
        {
            var timeLog = new List<TimeStamp>();
            
            var timeStampsInDatabase = ReadTimesFromFile();
            if (timeStampsInDatabase.Length == 0)
            {
                return (new List<TimeStamp>(), new List<TimeStamp>());
            }

            foreach (var timeStamp in timeStampsInDatabase.Split('\n').Where(x => x.Length > 0).ToList())
            {
                timeLog.Add(TimeStamp.FromString(timeStamp));
            }

            var clockingIn = timeLog.Where(x => x.Clocking == Clocking.In).ToList();
            var clockingout = timeLog.Where(x => x.Clocking == Clocking.Out).ToList();
            return (clockingIn, clockingout);
        }
    }
}
