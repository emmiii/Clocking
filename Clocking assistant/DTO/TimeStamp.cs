using System;
using static DTO.Enum;

namespace DTO
{
    public class TimeStamp
    {
        public Clocking Clocking { get; set; }
        public DateTime Time { get; set; }
        public int WorkingHours { get; set; }

        public override string ToString() => $"{Clocking}\t{Time}\t{WorkingHours}";

        public static TimeStamp FromString(string timeStampAsString)
        { 
            var parts = timeStampAsString.Split('\t');
            return new TimeStamp()
            {
                Clocking = parts[0].Equals("In") ? Clocking.In : Clocking.Out,
                Time = DateTime.Parse(parts[1]),
                WorkingHours = Int32.Parse(parts[2]),
            };
        }
        
    }
}
