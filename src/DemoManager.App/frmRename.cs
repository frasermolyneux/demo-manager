using System;
using System.Windows.Forms;

namespace DemoManager.App
{
    public partial class FrmRename : Form
    {
        public FrmRename(string valueName, string oldValue)
        {
            InitializeComponent();

            groupBox1.Text = string.Format(groupBox1.Text, valueName);
            textBox1.Text = oldValue;
        }

        public string Value
        {
            get => textBox1.Text;
            set => textBox1.Text = value;
        }

        #region Overrides of Form

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Forms.Form.Shown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            textBox1.Focus();
            textBox1.SelectAll();
            textBox1.Focus();
        }

        #endregion

        private void saveButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}