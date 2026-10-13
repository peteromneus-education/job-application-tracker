namespace job_application_tracker
{
    internal class Utilities // innehåller funktioner för validering och formatering
    {
        // Metoder
        public static char ReadCharacter(string prompt)
        {
            FormatPrompt(prompt);
            char value = Console.ReadKey(true).KeyChar;
            Console.WriteLine();
            return value;
        }

        public static int ValidateInteger(string prompt)
        {
            while (true)
            {
                FormatPrompt(prompt);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int value)
                    && value > 0)
                {
                    return value;
                }
                FormatError("Enter a whole number.\n");
            }
        }

        public static string ValidateString(string prompt)
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
