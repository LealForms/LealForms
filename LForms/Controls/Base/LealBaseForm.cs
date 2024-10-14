using LForms.Extensions;
using System.Drawing;
using System.Windows.Forms;

namespace LForms.Controls.Base;

/// <summary>
/// Serves as a base class for forms, with built-in support for dark mode and an abstract method for loading components.
/// </summary>
public partial class LealBaseForm : Form
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LealBaseForm"/> class.
    /// Applies theme mode settings based on user's conputer theme and invokes the component loading process.
    /// </summary>
    public LealBaseForm()
    {
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(Constants.DEFAULT_WIDTH, Constants.DEFAULT_HEIGHT);
        this.TrySetDarkMode();
        InitializeComponent();
        LoadComponents();
    }

    /// <summary>
    /// When implemented in a derived class, provides a method to initialize and configure components for the form.
    /// </summary>
    public virtual void LoadComponents() { }

    /// <summary>
    /// Redraws the component, organizing it according to the controls
    /// </summary>
    public virtual void ReDraw() { }
}