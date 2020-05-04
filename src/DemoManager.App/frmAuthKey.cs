using System;
using System.Windows.Forms;
using DemoManager.App.Configuration;
using DemoManager.App.Properties;

namespace DemoManager.App
{
    public partial class FrmAuthKey : Form
    {
        public FrmAuthKey()
        {
            InitializeComponent();
        }

        private void frmAuthKey_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(DemoManagerConfiguration.AuthKey))
                txtAuthKey.Text = DemoManagerConfiguration.AuthKey;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Settings.Default.AuthKey = txtAuthKey.Text;
            Settings.Default.Save();
            Settings.Default.Reload();

            Close();
        }
    }
}