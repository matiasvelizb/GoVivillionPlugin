using System;
using System.Windows.Forms;
using GoVivillionPlugin.Forms;
using PKHeX.Core;

namespace GoVivillionPlugin;

public class GoVivillionPlugin : IPlugin
{
    public string Name => nameof(GoVivillionPlugin);
    public int Priority => 1;
    public ISaveFileProvider SaveFileEditor { get; private set; } = null!;

    private ToolStripMenuItem? ctrl;

    public void Initialize(params object[] args)
    {
        Console.WriteLine($"Loading {Name}...");
        SaveFileEditor = (ISaveFileProvider)Array.Find(args, z => z is ISaveFileProvider)!;
        LoadMenuStrip((ToolStrip?)Array.Find(args, z => z is ToolStrip));
    }

    private void LoadMenuStrip(ToolStrip? menuStrip)
    {
        AddPluginControl((ToolStripDropDownItem?)menuStrip?.Items.Find("Menu_Tools", false)[0]);
    }

    private void AddPluginControl(ToolStripDropDownItem? tools)
    {
        ctrl = new ToolStripMenuItem(Name)
        {
            Visible = false,
            Image = Properties.Resources.V18_Fantasia
        };

        ctrl.Click += new EventHandler(OpenGoVivillionForm);
        _ = (tools?.DropDownItems.Add(ctrl));
    }

    private void OpenGoVivillionForm(object? sender, EventArgs e)
    {
        _ = new GoVivillionForm((SAV9SV)SaveFileEditor.SAV).ShowDialog();
    }

    public void NotifySaveLoaded()
    {
        if (ctrl != null)
        {
            ctrl.Visible = SaveFileEditor.SAV is SAV9SV;
        }
    }

    public bool TryLoadFile(string filePath) => false;
}
