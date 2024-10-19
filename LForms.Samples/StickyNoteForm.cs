using LForms.Controls.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LForms.Samples;

public class StickyNoteForm : LealForm
{
    // create the list of notes
    public List<StickyNote> Notes = [];

    public override void LoadComponents()
    {
        // Set a fixed size to the form for a consistent sticky note experience
        SetFixedSize(500, 800); // You can unlock the size later using "FreeUpFixedSize();"

        // Disable the maximize button as the form size is fixed
        MaximizeBox = false;

        // Add a new stickynote and display it
        Notes.Add(new StickyNote());
        Notes[0].Show();
    }
}