using System.Reflection;
using System.Windows.Forms;

// ReSharper disable LocalizableElement

namespace DemoManager.App
{
    internal partial class FrmAbout : Form
    {
        public FrmAbout()
        {
            InitializeComponent();
            labelVersion.Text = $"Version {AssemblyVersion}";
        }

        #region Assembly Attribute Accessors

        public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        #endregion
    }
}