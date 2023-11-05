using System.Linq;

namespace TimeKeeper
{
    public static class TimeCalculator
    {
        private static double CalculateTimeBalanceInMinutes()
        {
            var timeBalance = 0;
            var (clockingIn, clockingOut) = TimePersister.GetTimeStamps();
            var timeStampCouples = clockingIn.Zip(clockingOut, (i, o) => new { clockingIn = i, clockingOut = o } );
            var timeStampsPerDay = timeStampCouples.GroupBy(x => x.clockingIn.Time.Date).ToList();
            foreach (var day in timeStampsPerDay)
            {
                var workedTime = 0;
                var workingHours = day.Select(x => x.clockingIn).FirstOrDefault().WorkingHours;
                foreach (var clockedTime in day)
                {
                    workedTime += (int)(clockedTime.clockingOut.Time - clockedTime.clockingIn.Time).TotalMinutes;
                }
                timeBalance += (workedTime - workingHours*60);
            }
            return timeBalance;
        }

        private static (int, int) GetHoursAndMinutes(double minutes)
        {
            var hours = (int)minutes / 60;
            var remainingMinutes = (int)(minutes - hours * 60);
            return (hours, remainingMinutes);
        }

        public static (int, int) GetTimeBalance() => GetHoursAndMinutes(CalculateTimeBalanceInMinutes());
        
    }
}
