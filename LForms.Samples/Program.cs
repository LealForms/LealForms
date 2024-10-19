using LForms.Enums;
using LForms.Extensions;

namespace LForms.Samples;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        try
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new StickyNoteForm());
        }
        catch (Exception ex)
        {
            // Handle critical exceptions to ensure the application exits gracefully,
            // showing an informative message box to the user.
            _ = ex.HandleException(ErrorType.Critical);
        }
    }
}