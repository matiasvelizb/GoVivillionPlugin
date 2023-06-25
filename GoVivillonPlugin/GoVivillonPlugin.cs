using GoVivillonPlugin.Forms;
using PKHeX.Core;

namespace GoVivillonPlugin;

public class GoVivillonPlugin : IPlugin
{
    public string Name => "Go Vivillon Plugin";
    public int Priority => 1;
    public ISaveFileProvider SaveFileEditor { get; private set; } = null!;

    private ToolStripMenuItem? ctrl;

    public void Initialize(params object[] args)
    {
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
        tools?.DropDownItems.Add(ctrl);

        var c2 = new ToolStripMenuItem("Edit Vivillon Form Spawn");
        c2.Click += new EventHandler(OpenGoVivillonForm);
        var c3 = new ToolStripMenuItem("Generate Vivillon Living Dex");
        c3.Click += new EventHandler(GenerateLivingDex);
        ctrl.DropDownItems.Add(c2);
        ctrl.DropDownItems.Add(c3);
    }

    private void OpenGoVivillonForm(object? sender, EventArgs e)
    {
        new GoVivillonForm((SAV9SV)SaveFileEditor.SAV).ShowDialog();
    }

    private void GenerateLivingDex(object? sender, EventArgs e)
    {
        MessageBox.Show("WIP", Name);
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
