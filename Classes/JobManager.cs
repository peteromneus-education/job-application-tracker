using static job_application_tracker.Classes.JobApplication;

namespace job_application_tracker.Classes
{

    public class JobManager
    {
        // Properties
        public List<JobApplication> ApplicationsList = []; // initialiserar en lista av typen Applications som kommer att innehålla alla jobbansökningar

        // Metoder
        public void AddJob(string companyName, string positionTitle, int salaryExpectation) // metod som tar in tre parametrar: companyName, positionTitle och salaryExpectation. Den skapar en ny instans av Applications-klassen med dessa värden och lägger till den i ApplicationsList.
        {
            JobApplication application = new(companyName, positionTitle, salaryExpectation);
            ApplicationsList.Add(application);
        }

        public static bool ConfirmAddApplication(string companyName, string positionTitle, int salaryExpectation) // metod som visar användaren den information som har matats in och frågar om den är korrekt. Om användaren bekräftar med 'Y' returnerar metoden true, annars returnerar den false.
        {
            {
                Console.WriteLine("\n" +
                    "You entered:");
                Console.Write(
                    $"Company: {companyName}\n" +
                    $"Job title: {positionTitle}\n" +
                    $"Salary expectation: {salaryExpectation} kr\n");

                while (true)
                {
                    Utilities.FormatPrompt($"\nIs this information correct? (Y/N)");

                    char confirmKey = char.ToUpperInvariant(Console.ReadKey(true).KeyChar);

                    switch (confirmKey)
                    {
                        default:
                            Utilities.FormatPrompt("\nPlease press Y or N.");
                            break;

                        case 'Y':
                            return true;

                        case 'N':
                            Utilities.FormatPrompt("\nPress any key to return to menu...");
                            Console.ReadKey(true);
                            return false;
                    }
                }
            }
        }

        public void UpdateStatus() // metod som uppdaterar statusen för en ansökan. Den visar alla ansökningar, låter användaren välja en ansökan och sedan välja en ny status. Om statusen ändras från "Applied" till något annat och ResponseDate är null, uppdateras ResponseDate till dagens datum.
        {
            Utilities.FormatHeading("Update application status:");

            if (ApplicationsList.Count == 0)
            {
                Utilities.FormatError("The applications list is empty.");
                return;
            }

            ShowAll(); // anropar metoden ShowAll() för att visa alla ansökningar i listan

            int choice = Utilities.ValidateInteger("\nEnter a number and press 'Enter': ");
            int index = choice - 1; // subtraherar 1 från användarens val för att få rätt index i listan (eftersom listor är nollindexerade)

            if (index < 0 || index >= ApplicationsList.Count) // kontrollerar om indexet är utanför listans gränser. Om det är det, visas ett felmeddelande och metoden avslutas med return.
            {
                Utilities.FormatError("There's no application with that number");
                return;
            }

            JobApplication selectedApplication = ApplicationsList[index]; // hämtar den valda ansökan från listan baserat på indexet

            Console.WriteLine(
                "\n1. Applied" +
                "\n2. Interviewed" +
                "\n3. Offered" +
                "\n4. Rejected");

            char statusChoice = Utilities.ReadCharacter("\nPress a number to modify status.");

            switch (statusChoice)
            {
                default:
                    Utilities.FormatError("\nInvalid choice. Status was not changed.");
                    return;

                case '1':
                    selectedApplication.Status = ApplicationStatus.Applied;
                    Utilities.FormatSuccess($"Status updated to {selectedApplication.Status}.");
                    break;

                case '2':
                    selectedApplication.Status = ApplicationStatus.Interview;
                    Utilities.FormatSuccess($"Status updated to {selectedApplication.Status}.");
                    break;

                case '3':
                    selectedApplication.Status = ApplicationStatus.Offer;
                    Utilities.FormatSuccess($"Status updated to {selectedApplication.Status}.");
                    break;

                case '4':
                    selectedApplication.Status = ApplicationStatus.Rejected;
                    Utilities.FormatSuccess($"Status updated to {selectedApplication.Status}.");
                    break;
            }

            // ändrar ResponseDate till dagens datum om status ändras från Applied och ResponseDate fortfarande är null
            if (selectedApplication.Status != ApplicationStatus.Applied && selectedApplication.ResponseDate == null)
            {
                selectedApplication.ResponseDate = DateTime.Today;
                Console.WriteLine($"Response date updated to {selectedApplication.ResponseDate:yyyy-MM-dd}");
            }
        }

        public void DeleteApplication() // metod som tar bort en ansökan från listan. Den visar alla ansökningar, låter användaren välja en ansökan att ta bort och ber om bekräftelse innan den tas bort.
        {
            Utilities.FormatHeading("Delete application:");

            if (ApplicationsList.Count == 0)
            {
                Utilities.FormatError("The applications list is empty.");
                return;
            }

            ShowAll();

            int choice = Utilities.ValidateInteger("\nChoose an application to delete: ");
            int index = choice - 1;

            if (index < 0 || index >= ApplicationsList.Count)
            {
                Utilities.FormatError("\nThere's no application with that number");
                return;
            }

            while (true)
            {
                Utilities.FormatPrompt($"\nAre you sure you want to delete the application at index {index + 1}? (Y/N)\n");
                char confirmKey = char.ToUpperInvariant(Console.ReadKey(true).KeyChar);

                switch (confirmKey)
                {
                    default:
                        Utilities.FormatPrompt("Please press Y or N.");
                        break;

                    case 'Y':
                        ApplicationsList.RemoveAt(index);
                        Utilities.FormatSuccess($"\nApplication deleted successfully!");
                        return;

                    case 'N':
                        return;
                }
            }
        }

        public void ShowAll() // metod som visar alla ansökningar i listan. Om listan är tom visas ett felmeddelande. Annars skrivs varje ansökan ut med dess index och en sammanfattning.
        {
            if (ApplicationsList.Count == 0)
            {
                Utilities.FormatError("\nThe applications list is empty.");
                return;
            }

            Console.Clear();
            Utilities.FormatHeading("All applications:\n");
            Console.ResetColor();

            for (int i = 0; i < ApplicationsList.Count; i++)
            {
                JobApplication application = ApplicationsList[i];
                Console.WriteLine($"{i + 1} - {application.GetSummary()}"); // använder GetSummary-metoden för att skriva ut en kort sammanfattning av varje ansökan
            }
        }

        public void FilterByStatus(ApplicationStatus status) // metod som filtrerar ansökningarna baserat på deras status. Den tar in en parameter av typen ApplicationStatus och skapar en lista med ansökningar som matchar den statusen. Om inga ansökningar matchar visas ett felmeddelande. Annars skrivs varje matchande ansökan ut med dess index och en sammanfattning.
        {
            List<JobApplication> filteredApplications = ApplicationsList
                .Where(application => application.Status == status)
                .ToList(); // använder ToList för att skapa en ny lista med ansökningar som matchar den valda statusen

            Console.Clear();
            Utilities.FormatHeading($"Applications with status: {status}\n");

            if (filteredApplications.Count == 0)
            {
                Utilities.FormatError($"No applications match the status {status}.\n");
                return;
            }

            foreach (JobApplication application in filteredApplications) // loopar igenom varje ansökan i den filtrerade listan och skriver ut dess index och sammanfattning
            {
                int index = ApplicationsList.IndexOf(application);
                Console.WriteLine($"{index + 1} - {application.GetSummary()}");
            }
        }

        public void SortList() // metod som sorterar ansökningarna i listan baserat på deras ansökningsdatum. Den använder LINQ för att skapa en ny lista med ansökningar sorterade i stigande ordning efter ApplicationDate.
        {
            Console.Clear();
            Utilities.FormatHeading($"Applications by date:\n");

            if (ApplicationsList.Count == 0)
            {
                Utilities.FormatError("The applications list is empty.");
                return;
            }

            List<JobApplication> filteredApplications = ApplicationsList.OrderBy(application => application.ApplicationDate).ToList();

            foreach (JobApplication application in filteredApplications)
            {
                int index = ApplicationsList.IndexOf(application);
                Console.WriteLine($"{index + 1} - {application.GetSummary()}");
            }
        }

        public void ShowStatistics() // metod som visar statistik om ansökningarna i listan. Den beräknar totalt antal ansökningar, genomsnittlig svarstid i dagar för ansökningar som har ett svar, samt antal ansökningar i varje statuskategori (Applied, Interviewed, Offered, Rejected). Om inga ansökningar har ett svar visas ett meddelande om det.
        {
            Console.Clear();
            Utilities.FormatHeading("Application statistics:\n");

            int countOfApplications = ApplicationsList.Count;

            double averageResponseDays = ApplicationsList // beräknar genomsnittlig svarstid i dagar för alla ansökningar som har ett svar (ResponseDate != null)
                .Where(application => application.ResponseDate != null) // har svar
                .Select(application => (application.ResponseDate.Value - application.ApplicationDate).Days)
                .DefaultIfEmpty(0) // returnerar 0 om ansökningar med svar saknas, för att undvika fel vid beräkning av genomsnittet
                .Average();

            double averageResponseDaysRounded = Math.Round(averageResponseDays, 0);
            bool hasResponses = ApplicationsList.Any(application => application.ResponseDate.HasValue); // kontrollerar om en ansökan har ett svar och använder bool-variabeln i villkoret på rad 253

            int applied = ApplicationsList
                .Count(application => application.Status == ApplicationStatus.Applied);
            int interviewed = ApplicationsList
                .Count(application => application.Status == ApplicationStatus.Interview);
            int offered = ApplicationsList
                .Count(application => application.Status == ApplicationStatus.Offer);
            int rejected = ApplicationsList
                .Count(application => application.Status == ApplicationStatus.Rejected);

            Console.WriteLine($"Total applications: {countOfApplications}");
            Console.WriteLine($"Applied: {applied}");
            Console.WriteLine($"Interviewed: {interviewed}");
            Console.WriteLine($"Offered: {offered}");
            Console.WriteLine($"Rejected: {rejected}");

            if (!hasResponses) // kontrollerar om det finns några ansökningar med svar. Om det inte finns några skrivs ett meddelande ut om att det inte finns några svar än.
            {
                Console.WriteLine($"No responses yet");
            }
            else
            {
                Console.WriteLine($"Average response time in days: {averageResponseDaysRounded}");
            }
        }

        public void ShowUnansweredApplications() // metod som visar alla ansökningar som inte har fått något svar och som är äldre än 14 dagar. Den beräknar ett cutoff-datum genom att subtrahera 14 dagar från dagens datum och filtrerar sedan listan med ansökningar baserat på detta datum och om ResponseDate är null. Om inga ansökningar matchar filtret visas ett meddelande om det.
        {
            DateTime cutoffDate = DateTime.Today.AddDays(-14);

            List<JobApplication> unansweredApplications = ApplicationsList // filtrerar listan med ansökningar för att hitta de som inte har fått något svar (ResponseDate == null) och som är äldre än 14 dagar (ApplicationDate < cutoffDate). Resultatet lagras i en ny lista med ToList().
                .Where(application => application.ResponseDate == null && application.ApplicationDate.Date < cutoffDate)
                .ToList();

            Console.Clear();
            Utilities.FormatHeading("Unanswered applications older than 14 days:\n");

            if (unansweredApplications.Count == 0)
            {
                Console.WriteLine("No applications match this filter.\n");
                return;
            }

            foreach (JobApplication application in unansweredApplications)
            {
                int index = ApplicationsList.IndexOf(application);
                Console.WriteLine($"{index + 1} - {application.GetSummary()}");
            }
        }
    }
}
