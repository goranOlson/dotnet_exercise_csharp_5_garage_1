namespace dotnet_exercise_csharp_5_garage_1.UI
{
    internal class ConsoleUI
    {
        

        public string AskForString(string prompt, Predicate<string>? validate = null)
        {
            string answer;
            bool success = false;

            do
            {
                Print($"{prompt}: ");
                answer = GetInput();

                if (string.IsNullOrWhiteSpace(answer))
                {
                    Print($"Felaktig inmating{Environment.NewLine}");
                }
                else if (validate != null && !validate(answer))
                {
                    Print($"You must enter a valid 2{Environment.NewLine}");
                }
                else
                    success = true;

            } while (!success);

            return answer;
        }

        public uint AskForUInt(string prompt, uint? min = null, uint? max = null)
        {
            return uint.Parse(AskForString(prompt, (input) =>
            {
                if (!uint.TryParse(input, out uint result))
                    return false;

                if (min.HasValue && result < min.Value)
                    return false;

                if (max.HasValue && result > max.Value)
                    return false;

                return true;
            }
            ));
        }

        public string GetInput()
        {
            return Console.ReadLine() ?? string.Empty;
        }

        public void Print(string prompt)
        {
            Console.Write(prompt);
        }
    }
}
