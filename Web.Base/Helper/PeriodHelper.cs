using System;
using WebBase.Enum; // Ensure to include the namespace where ReportRequestType is defined

namespace WebBase.Helper
{
    public class PeriodHelper
    {
        public DateTime Calculate(ReportTimePeriod period)
        {

            switch (period)
            {
                case ReportTimePeriod.Daily:
                    return DateTime.Now.AddDays(-1);
                case ReportTimePeriod.Weekly:
                    return DateTime.Now.AddDays(-7);
                case ReportTimePeriod.Monthly:
                    return DateTime.Now.AddMonths(-1);
                default:
                    return DateTime.Now;
            }
        }
    }
}