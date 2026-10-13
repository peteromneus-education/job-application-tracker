namespace job_application_tracker.Classes
{
    public class JobApplication(string name, string title, int salary)
    {
        // Enum
        public enum ApplicationStatus
        {
            Applied, Interview, Offer, Rejected
        }

        // Properties
        public string CompanyName { get; set; } = name;
        public string PositionTitle { get; set; } = title;
        public int SalaryExpectation { get; set; } = salary;
        public DateTime ApplicationDate { get; set; } = DateTime.Today;
        public DateTime? ResponseDate { get; set; } = null;
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;

        // Methods
        public static int GetDaysSinceApplied(DateTime applicationDate)
        {
            return (DateTime.Today - applicationDate).Days;
        }

        public string GetSummary()
        {
            return
                $"{CompanyName} – " +
                $"{PositionTitle} – " +
                $"{SalaryExpectation} - " +
                $"{Status} - " +
                $"Date: {ApplicationDate:yyyy-MM-dd} - " +
                $"Response date: {ResponseDate?.ToString("yyyy-MM-dd") ?? "No response yet"}";
        }
    }
}
