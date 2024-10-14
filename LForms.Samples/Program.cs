using LForms.Enums;
using LForms.Extensions;

namespace LForms.Samples;

internal static class Program
{
    [STAThread]
    internal static void Main()
    {
        try
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MyCustomFormExample());
        }
        catch (Exception ex)
        {
            _ = ex.HandleException(ErrorType.Critical);
        }
    }
}