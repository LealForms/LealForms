using LForms.Controls.Forms;
using LForms.Enums.MessageBox;
using LForms.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LForms.Controls.MessageBox;

public static class LealMessageBox
{


    private static DialogResult DisplayMessage(Control owner, string title, string message, IconType iconType, ButtonType buttonType, string customButtonText)
    {
        var form = owner.FindForm();
        var messageBoxForm = new LealMessageDisplay(new Size(600, 400), tryDarkMode: true)
        {
            Text = title,

        };

        return form != null ? messageBoxForm.ShowDialog(form) : messageBoxForm.ShowDialog();
    }
}