using System;

namespace EmployeeManagementSystem
{
    public class LeaveRecord
    {
        public string LeaveType { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Reason { get; set; }

        public LeaveRecord(string leaveType, string startDate, string endDate, string reason)
        {
            LeaveType = leaveType;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }

        public override string ToString()
        {
            return $"Type: {LeaveType}, Start: {StartDate}, End: {EndDate}, Reason: {Reason}";
        }
    }
}
