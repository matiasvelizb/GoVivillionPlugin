using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PKHeX.Core;

namespace GoVivillionPlugin.Forms
{
    public partial class GoVivillionForm : Form
    {
        // Save blocks https://github.com/kwsch/PKHeX/commit/df481c9b2519a5cf8bebd2cdd16fbdea37b9cba6
        private static readonly uint KGOVivillonForm = 0x22F70BCF; // desired form between 0 and 17
        private static readonly uint KGOVivillonExpiration = 0x867F0240; // Unix time stamp for when you connected with Pokemon GO.
        private static readonly uint KGOVivillonFormEnabled = 0x0C125D5C; // Bool2 to enable

        private readonly SAV9SV sav;
        private readonly SCBlock formBlock, expirationBlock, enabledBlock;
        private byte selectedForm;

        public GoVivillionForm(SAV9SV saveFile)
        {
            sav = saveFile;
            InitializeComponent();

            formBlock = sav.Accessor.GetBlock(KGOVivillonForm);
            expirationBlock = sav.Accessor.GetBlock(KGOVivillonExpiration);
            enabledBlock = sav.Accessor.GetBlock(KGOVivillonFormEnabled);
            selectedForm = (byte)formBlock.GetValue();

        }
    }
}
