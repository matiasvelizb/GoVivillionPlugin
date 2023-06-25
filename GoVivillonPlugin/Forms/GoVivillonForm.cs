using PKHeX.Core;

namespace GoVivillonPlugin.Forms
{
    public partial class GoVivillonForm : Form
    {
        // Pokémon GO Connectivity
        private const uint KGOVivillonForm = 0x22F70BCF; // BibiyonFormNoSave_formNo
        private const uint KGOVivillonFormEnabled = 0x0C125D5C; // BibiyonFormNoSave_isValid
        private const uint KGOVivillonPostcardSent = 0x867F0240; // BibiyonFormNoSave_accessedRealTime (Unix timestamp for when a postcard was sent from GO)

        private readonly SAV9SV saveFile;
        private readonly SCBlockAccessor scBlockAccessor;
        private byte selectedForm;

        public GoVivillonForm(SAV9SV sAV9SV)
        {
            InitializeComponent();
            saveFile = sAV9SV;
            scBlockAccessor = saveFile.Accessor;
            selectedForm = (byte)scBlockAccessor.GetBlockValue(KGOVivillonForm);

            RadioButton initialForm = (RadioButton)formsGroup.Controls.Find($"form{selectedForm:00}", true).First();
            initialForm.Checked = true;
        }

        private void formSelected(object sender, EventArgs e)
        {
            if (sender is RadioButton radioButton && radioButton.Checked)
            {
                selectedForm = byte.Parse((string)radioButton.Tag!);
            }
        }

        private void leaveForm(object sender, EventArgs e) => Close();

        private void saveForm(object sender, EventArgs e)
        {
            scBlockAccessor.GetBlock(KGOVivillonFormEnabled).ChangeBooleanType(SCTypeCode.Bool2);
            scBlockAccessor.SetBlockValue(KGOVivillonForm, selectedForm);
            scBlockAccessor.SetBlockValue(KGOVivillonPostcardSent, (ulong)DateTimeOffset.Now.ToUnixTimeSeconds());
            saveFile.State.Edited = true;
            Close();
        }
    }
}
