using job_application_tracker;
using job_application_tracker.Classes;
using static job_application_tracker.Classes.JobApplication;

internal class Program
{

    private static void Main(string[] args)
    {
        Console.Title = "Job Application Tracker"; // sätter titeln på konsolfönstret

        char mainMenuChoice;
        bool keepDisplayingMenu = true;

        JobManager manager = new(); // skapar en instans av JobManager-klassen som hanterar listan med ansökningar

        // åtta exempel som skapas vid varje programstart, så att man slipper skapa nya varje gång programmet körs
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
            DisplayMenu(); // kallar in metoden DisplayMenu som rensar konsolen och visar huvudmenyn med alternativ

            mainMenuChoice = Console.ReadKey(true).KeyChar;

            switch (mainMenuChoice)
            {
                default:
                    Utilities.FormatError("\nPlease choose an option from the menu...\n");
                    Console.ReadKey();
                    break;

                case '1':
                    Console.Clear();
                    Utilities.FormatHeading("Add new application\n"); // kallar in metoden FormatHeading som formaterar rubriken

                    string companyName = Utilities.ValidateString("Company: "); // kallar in metoden ValidateString som validerar att användaren skriver in text
                    string positionTitle = Utilities.ValidateString("Title: ");
                    int salaryExpectation = Utilities.ValidateInteger("Expected salary: "); // kallar in metoden ValidateInteger som validerar att användaren skriver in ett heltal
                    bool isConfirmed = JobManager.ConfirmAddApplication(companyName, positionTitle, salaryExpectation); // kallar in metoden ConfirmAddApplication som ber användaren att bekräfta innan hen lägger in ansökningen i listan

                    if (isConfirmed)
                    {
                        manager.AddJob(companyName, positionTitle, salaryExpectation); // kallar in metoden AddJob som lägger till ansökningen i listan
                        Utilities.FormatSuccess("\nApplication added successfully!");
                        ReturnMessage();
                    }
                    break;

                case '2':
                    Console.Clear();
                    manager.UpdateStatus(); // kallar in metoden UpdateStatus som låter användaren ändra status på en ansökning
                    ReturnMessage();
                    break;

                case '3':
                    manager.DeleteApplication(); // kallar in metoden DeleteApplication som låter användaren ta bort en ansökning
                    ReturnMessage();
                    break;

                case '4':
                    ShowApplicationsMenu(manager); // kallar in metoden ShowApplicationsMenu som fungerar som en undermeny till "Show all applications" och låter användaren filtrera eller sortera i listan med ansökningar
                    break;

                case '5':
                    manager.ShowStatistics(); // kallar in metoden ShowStatistics som visar diverse statistik över ansökningarna
                    ReturnMessage();
                    break;

                case '0':
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

        static void ReturnMessage() // gjorde en egen metod av detta eftersom meddelandet används ofta
        {
            Utilities.FormatPrompt("\nPress a key to return to the main menu...");
            Console.ReadKey(true);
        }

        static void ShowApplicationsMenu(JobManager manager)
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
                manager.ShowAll(); // kallar in metoden ShowAll som visar alla ansökningar i listan

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
                        manager.ShowUnansweredApplications(); // kallar in metoden ShowUnansweredApplications som visar ansökningar som inte har fått svar på mer än 14 dagar
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

        static void ShowStatusFilterMenu(JobManager manager) // metoden låter användaren filtrera ansökningarna efter status
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
                        manager.FilterByStatus(ApplicationStatus.Applied); // kallar in metoden FilterByStatus som filtrerar ansökningarna efter status
                        return; // använder return här för att gå tillbaka till ShowApplicationsMenu-metoden efter att ha filtrerat ansökningarna

                    case '2':
                        manager.FilterByStatus(ApplicationStatus.Interview);
                        return;

                    case '3':
                        manager.FilterByStatus(ApplicationStatus.Offer);
                        return;

                    case '4':
                        manager.FilterByStatus(ApplicationStatus.Rejected);
                        return;

                    case '0':
                        return;

                    default:
                        Utilities.FormatError("Please choose a number from the menu\n");
                        break;
                }
            }
        }

        void AddExample(string companyName, string positionTitle, int salaryExpectation, int appliedDaysAgo, ApplicationStatus status, int? respondedDaysAgo) // metoden skapar exempelansökningar som läggs till i listan med ansökningar
        {
            JobApplication application = new(companyName, positionTitle, salaryExpectation) // skapar en ny instans av JobApplication-klassen
            {
                ApplicationDate = DateTime.Today.AddDays(-appliedDaysAgo),
                Status = status,
                ResponseDate = respondedDaysAgo.HasValue ? DateTime.Today.AddDays(-respondedDaysAgo.Value) : null // villkor som sätter ResponseDate till null om respondedDaysAgo är null, annars sätter den ResponseDate till dagens datum minus respondedDaysAgo
            };

            manager.ApplicationsList.Add(application); // lägger till ansökningen i listan med ansökningar
        }
    }
}
