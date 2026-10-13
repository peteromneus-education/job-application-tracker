namespace job_application_tracker
{

    internal class Utilities // klass som innehåller metoder för att läsa in och validera användarinput, samt formatera text i konsolen
    {
        // Metoder
        public static char ReadCharacter(string prompt) // metod som läser in ett tecken (char) från användaren och returnerar det
        {
            FormatPrompt(prompt);
            char value = Console.ReadKey(true).KeyChar;
            Console.WriteLine();
            return value;
        }

        public static int ValidateInteger(string prompt) // metod som läser in ett heltal (int) från användaren och returnerar det
        {
            while (true)
            {
                FormatPrompt(prompt); // prompt är vad som ska skrivas ut i konsolen innan användaren skriver in sitt svar
                string? input = Console.ReadLine(); // input är det som användaren skriver in i konsolen

                if (int.TryParse(input, out int value) // value är det som användaren skriver in i konsolen, och TryParse försöker konvertera det till ett heltal (int)
                    && value > 0)
                {
                    return value;
                }
                FormatError("Enter a whole number.\n");
            }
        }

        public static string ValidateString(string prompt) // metod som läser in en sträng (string) från användaren och returnerar det
        {
            while (true)
            {
                FormatPrompt(prompt);
                string? text = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(text))
                {
                    return text;
                }
                else
                {
                    FormatError("Please enter some text.");
                }
            }
        }

        public static string FormatError(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(prompt);
            Console.ResetColor();
            return prompt;
        }

        public static string FormatSuccess(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(prompt);
            Console.ResetColor();
            return prompt;
        }

        public static string FormatHeading(string prompt)
        {
            string header = prompt.ToUpper();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(header);
            Console.ResetColor();
            return prompt;
        }

        public static string FormatPrompt(string prompt)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(prompt);
            Console.ResetColor();
            return prompt;
        }
    }
}
