using System.Windows.Forms;

namespace LForms.Extensions;

public static class FormExtensions
{
    /// <summary>
    /// Switches between two forms, hiding the current form and displaying the next form.
    /// </summary>
    /// <param name="from">The current form to hide.</param>
    /// <param name="to">The new form to display.</param>
    public static void SwitchView(this Form from, Form to)
    {
        from.SetVisibility(false);
        to.Closed += (s, e) => from.Close();
        to.SetVisibility(true);
    }
}