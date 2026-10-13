namespace job_application_tracker.Classes
{
    public class JobApplication(string name, string title, int salary) // klass som representerar en jobbansökan
    {
        // Enum
        public enum ApplicationStatus // enum som representerar statusen för en jobbansökan
        {
            Applied, Interview, Offer, Rejected
        }

        // Properties
        public string CompanyName { get; set; } = name;
        public string PositionTitle { get; set; } = title;
        public int SalaryExpectation { get; set; } = salary;
        public DateTime ApplicationDate { get; set; } = DateTime.Today;
        public DateTime? ResponseDate { get; set; } = null; // ResponseDate är nullable eftersom det kan vara null om man inte har fått något svar än. Datumet ändras automatiskt när man uppdaterar status.
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied; // standerdvärdet är satt till Applied

        // Methods
        public static int GetDaysSinceApplied(DateTime applicationDate) // enkel metod som räknar ut hur många dagar det har gått sedan man skickade in sin ansökan
        {
            return (DateTime.Today - applicationDate).Days;
        }

        public string GetSummary() // metod som returnerar en sträng med information om jobbansökan
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
