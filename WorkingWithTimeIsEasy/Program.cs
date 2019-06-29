using System;
using static System.FormattableString;
using NodaTime;

namespace WorkingWithTimeIsEasy
{
    class Program
    {
        static void Main(string[] args)
        {
            var start = new LocalDateTime(2017, 1, 27, 9, 0, 0); //27.01.2017 09:00:00

            /**
             * Date arithmetic
             * Same values are used in both examples, just the brackets are put on different positions
             */

            //Example 1: adding a period of [a month and 3 days]
            //First: one month and it will get to 27.02
            //Then: 3 days - will get to 02.03
            //Output: 3/2/2017 09:00:00
            var end1 = start + (Period.FromDays(3) + Period.FromMonths(1));

            //Example 2:
            //First: adding 3 days - will get to 30.01
            //Then: a month - will get to 28.02
            //Output: 2/28/2017 09:00:00
            var end2 = (start + Period.FromDays(3)) + Period.FromMonths(1);

            Console.WriteLine(end1);
            Console.WriteLine(end2);

            //Example 3:
            Console.WriteLine("==========");
            GettingTimeWithSystemDateTime();
            Console.WriteLine("==========");
            GettingTimeWithNodaTime();

            Console.Read();
        }


        static void GettingTimeWithSystemDateTime()
        {
            DateTime timestamp = DateTime.UtcNow; //Never use DateTime.Now unless you want to display it to the user on the same system
            Console.WriteLine($"Event happened at {timestamp:yyyy-MM-dd'T'HH:mm:ss.FFF}");
            Console.WriteLine(Invariant($"Event happened at {timestamp:yyyy-MM-dd'T'HH:mm:ss.FFF}")); //Always use Invariant culture (even if running on a machine with Arabic culture as default)
        }

        static void GettingTimeWithNodaTime()
        {
            //Get the current moment in time:
            // Instant now = SystemClock.Instance.Now;               // NodaTime 1.x
            Instant now = SystemClock.Instance.GetCurrentInstant();  // NodaTime 2.x
            Console.WriteLine(now);

            //Get the system's time zone
            DateTimeZone tz = DateTimeZoneProviders.Bcl.GetSystemDefault();
            Console.WriteLine(tz);

            //Apply the time zone to the instant:
            ZonedDateTime zdt = now.InZone(tz);
            Console.WriteLine(zdt);
        }
    }
}
