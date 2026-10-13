using job_application_tracker;
using job_application_tracker.Classes;
using static job_application_tracker.Classes.JobApplication;

internal class Program
{

    private static void Main(string[] args)
    {
        Console.Title = "Job Application Tracker";

        char mainMenuChoice;
        bool keepDisplayingMenu = true;

        JobManager manager = new(); // skapar en instans av JobManager-klassen som hanterar listan med ansökningar

        // åtta exempel som skapas vid varje programstart, så att man slipper skapa nya varje gång programmet körs 🙂
        AddExample("Microshift", ".NET Developer", 38000,
            3, ApplicationStatus.Applied, null);
        AddExample("Hogio", "Junior Developer", 35000,
            14, ApplicationStatus.Applied, null);
        AddExample("Orange", "Backend Developer", 39000,
            15, ApplicationStatus.Applied, null);
        AddExample("LoQ", "C# Developer", 37000,
            30, ApplicationStatus.Applied, null);
        AddExample("2bits", "Software Developer", 40000,
            20, ApplicationStatus.Interview, 12);
        AddExample("Barnfire", "Fullstack Developer", 41000,
            35, ApplicationStatus.Offer, 10);
        AddExample("InfiNOT Code", "Integration Developer", 39000,
            25, ApplicationStatus.Rejected, 18);
        AddExample("IT Dreamz", "CTO", 3000000,
            0, ApplicationStatus.Interview, 0);

        // Huvudmeny
        while (keepDisplayingMenu)
        {
            DisplayMenu();

            mainMenuChoice = Console.ReadKey(true).KeyChar;

            switch (mainMenuChoice)
            {
                default:
                    Utilities.FormatError("\nPlease choose an option from the menu...\n");
                    Console.ReadKey();
                    break;

                case '1': // lägg till en ny ansökning
                    Console.Clear();
                    Utilities.FormatHeading("Add new application\n");

                    string companyName = Utilities.ValidateString("Company: ");
                    string positionTitle = Utilities.ValidateString("Title: ");
                    int salaryExpectation = Utilities.ValidateInteger("Expected salary: ");
                    bool isConfirmed = JobManager.ConfirmAddApplication(companyName, positionTitle, salaryExpectation);

                    if (isConfirmed)
                    {
                        manager.AddJob(companyName, positionTitle, salaryExpectation);
                        Utilities.FormatSuccess("\nApplication added successfully!");
                        ReturnMessage();
                    }
                    break;

                case '2': // ändra status på en ansökning
                    Console.Clear();
                    manager.UpdateStatus();
                    ReturnMessage();
                    break;

                case '3': // radera en ansökning
                    manager.DeleteApplication();
                    ReturnMessage();
                    break;

                case '4': // visa alla ansökningar och låt användaren filtrera eller sortera
                    ShowApplicationsMenu(manager);
                    break;

                case '5': // visa statistik
                    manager.ShowStatistics();
                    ReturnMessage();
                    break;

                case '0': // avsluta programmet
                    keepDisplayingMenu = false;
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\nPress a key to exit the program...");
                    Console.ResetColor();
                    Console.ReadKey(true);
                    break;
            }
        }

        // Metoder
        static void DisplayMenu() // metoden rensar konsolen och visar huvudmenyn med alternativ
        {
            Console.Clear();

            Utilities.FormatHeading("===== Job Application Tracker =====\n");

            Console.WriteLine("1. Add new application" +
                "\n2. Update an application status" +
                "\n3. Delete an application" +
                "\n4. Show all applications" +
                "\n5. Show statistics" +
                "\n0. Exit");
            Utilities.FormatPrompt("\nChoose an option by pressing the corresponding number key.");
        }

        static void ReturnMessage()
        {
            Utilities.FormatPrompt("\nPress a key to return to the main menu...");
            Console.ReadKey(true);
        }

        static void ShowApplicationsMenu(JobManager manager) // fungerar som en undermeny till "Show all applications" och låter användaren filtrera eller sortera i listan med ansökningar
        {
            if (manager.ApplicationsList.Count == 0)
            {
                Utilities.FormatError("\nThe applications list is empty.");
                Utilities.FormatPrompt("\nPress a key to return to the main menu...");
                Console.ReadKey(true);
                return;
            }

            while (true)
            {
                manager.ShowAllApplications();

                Console.WriteLine("\nFiltering and sorting:");
                Console.WriteLine(
                    "\n1. Filter by status" +
                    "\n2. Sort by date" +
                    "\n3. Show applications due for a follow-up" +
                    "\n0. Return to main menu\n");

                char choice = Utilities.ReadCharacter("Choose an option: ");

                switch (choice)
                {
                    case '1':
                        ShowStatusFilterMenu(manager); // kallar in metoden ShowStatusFilterMenu som låter användaren filtrera ansökningarna efter status
                        break;

                    case '2':
                        manager.SortList(); // kallar in metoden SortList som låter användaren sortera ansökningarna efter datum
                        break;

                    case '3':
                        manager.ShowUnansweredApplications();
                        break;

                    case '0':
                        return;

                    default:
                        Utilities.FormatError("Please choose a number from the menu\n");
                        break;
                }

                Utilities.FormatPrompt("\nPress a key to return...");
                Console.ReadKey(true);
            }
        }

        static void ShowStatusFilterMenu(JobManager manager)
        {
            Console.Clear();
            Utilities.FormatHeading("Filter by status:\n");

            Console.WriteLine(
                "1. Applied\n" +
                "2. Interviewed\n" +
                "3. Offered\n" +
                "4. Rejected\n" +
                "0. Back");

            while (true)
            {
                char choice = Utilities.ReadCharacter("\nChoose status:");

                switch (choice)
                {
                    case '1':
                        manager.FilterByStatus(JobApplication.ApplicationStatus.Applied);
                        return; // jag använder return här för att gå tillbaka till ShowApplicationsMenu efter att ha filtrerat listan

                    case '2':
                        manager.FilterByStatus(JobApplication.ApplicationStatus.Interview);
                        return;

                    case '3':
                        manager.FilterByStatus(JobApplication.ApplicationStatus.Offer);
                        return;

                    case '4':
                        manager.FilterByStatus(JobApplication.ApplicationStatus.Rejected);
                        return;

                    case '0':
                        return;

                    default:
                        Utilities.FormatError("Please choose a number from the menu\n");
                        break;
                }
            }
        }

        void AddExample(string companyName, string positionTitle, int salaryExpectation, int appliedDaysAgo, ApplicationStatus status, int? respondedDaysAgo)
        {
            JobApplication application = new(companyName, positionTitle, salaryExpectation) // skapar en ny instans av JobApplication-klassen
            {
                ApplicationDate = DateTime.Today.AddDays(-appliedDaysAgo),
                Status = status,
                ResponseDate = respondedDaysAgo.HasValue ? DateTime.Today.AddDays(-respondedDaysAgo.Value) : null
            };

            manager.ApplicationsList.Add(application);
        }
    }
}
