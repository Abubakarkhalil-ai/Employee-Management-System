using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagementSystem;

namespace EmployeeManagementSystem.Tests
{
    [TestClass]
    public class LeaveRecordTests
    {
        [TestMethod]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange & Act
            LeaveRecord leave = new LeaveRecord("Annual Leave", "2026-03-10", "2026-03-15", "Family trip");

            // Assert
            Assert.AreEqual("Annual Leave", leave.LeaveType);
            Assert.AreEqual("2026-03-10", leave.StartDate);
            Assert.AreEqual("2026-03-15", leave.EndDate);
            Assert.AreEqual("Family trip", leave.Reason);
        }

        [TestMethod]
        public void ToString_ReturnsFormattedString()
        {
            // Arrange
            LeaveRecord leave = new LeaveRecord("Sick Leave", "2026-03-05", "2026-03-06", "Flu");

            // Act
            string result = leave.ToString();

            // Assert
            Assert.AreEqual("Type: Sick Leave, Start: 2026-03-05, End: 2026-03-06, Reason: Flu", result);
        }

        [TestMethod]
        public void Constructor_HandlesEmptyReason()
        {
            // Arrange & Act
            LeaveRecord leave = new LeaveRecord("Annual Leave", "2026-04-01", "2026-04-05", "");

            // Assert
            Assert.AreEqual("", leave.Reason);
        }
    }
}
