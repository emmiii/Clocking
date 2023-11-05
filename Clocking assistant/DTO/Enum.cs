using System.ComponentModel;

namespace DTO
{
    public class Enum
    {
        public enum Clocking 
        { 
            [Description("Clocking in")]
            In = 0,
            [Description("Clocking out")]
            Out = 1
        }
    }
}
