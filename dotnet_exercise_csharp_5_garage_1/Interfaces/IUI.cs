namespace dotnet_exercise_csharp_5_garage_1.Interfaces
{
    internal interface IUI
    {
        string AskForString(string prompt, Predicate<string>? validate = null, bool emptyOk = false);
        uint AskForUInt(string prompt, uint? min = null, uint? max = null);
        void Clear();
        bool ExitMessageAction(string prompt = "Tryck på valfri tangent för att forsätta!");
        string GetInput();
        void Print(string prompt);
        void PrintLine(string prompt);
    }
}