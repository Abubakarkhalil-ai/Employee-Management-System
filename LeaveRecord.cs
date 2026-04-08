using System;

namespace EmployeeManagementSystem
{
    /// <summary>
    /// Represents a leave record for an employee.
    /// Stores the type of leave, date range, and reason.
    /// </summary>
    public class LeaveRecord
    {
        // Type of leave (e.g. Annual Leave, Sick Leave)
        public string LeaveType { get; set; }

        // Start date of the leave period (format: YYYY-MM-DD)
        public string StartDate { get; set; }

        // End date of the leave period (format: YYYY-MM-DD)
        public string EndDate { get; set; }

        // Reason for taking the leave
        public string Reason { get; set; }

        /// <summary>
        /// Constructor to create a new LeaveRecord with the given details.
        /// </summary>
        public LeaveRecord(string leaveType, string startDate, string endDate, string reason)
        {
            LeaveType = leaveType;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }

        /// <summary>
        /// Returns a formatted string representation of the leave record.
        /// </summary>
        public override string ToString()
        {
            return $"Type: {LeaveType}, Start: {StartDate}, End: {EndDate}, Reason: {Reason}";
        }
    }
}
