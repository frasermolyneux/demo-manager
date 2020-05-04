using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DemoManager.App.Configuration;
using DemoManager.App.Helpers;
using DemoManager.App.Objects;
using DemoManager.App.Properties;
using Newtonsoft.Json;

// ReSharper disable LocalizableElement

namespace DemoManager.App
{
    public partial class FrmMain : Form
    {
        private bool _isDemoRunning;
        private GameVersion _runningGameVersion;

        public FrmMain()
        {
            InitializeComponent();
        }

        private async Task ReloadRepositories()
        {
            localDemoPanel.Enabled = false;
            remoteDemoPanel.Enabled = false;

            ToggleControls(false);

            statusLabel.Text = Resources.frmMainReloadRepositoriesReloading;
            progressBar.Value = 0;

            try
            {
                await localRepositoryView.Reload();

                foreach (var repositoryDemo in localRepositoryView.Repository.Demos)
                {
                    if (repositoryDemo == null)
                        continue;

                    var jsonString = JsonConvert.SerializeObject(repositoryDemo);
                    LogHelper.Instance.LogInfo(jsonString);
                }
            }
            catch (Exception e)
            {
                LogHelper.Instance.LogException("Failed to reload local demo repository", e);

                MessageBox.Show(this,
                    $"Failed to reload local demo repository.\n{e.GetDeepestException().Message}",
                    "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            progressBar.Value = 50;

            try
            {
                await remoteRepositoryView.Reload();

                foreach (var repositoryDemo in remoteRepositoryView.Repository.Demos)
                {
                    if (repositoryDemo == null)
                        continue;

                    var jsonString = JsonConvert.SerializeObject(repositoryDemo);
                    LogHelper.Instance.LogInfo(jsonString);
                }
            }
            catch (Exception e)
            {
                LogHelper.Instance.LogException("Failed to reload remote demo repository", e);

                MessageBox.Show(this,
                    $"Failed to reload remote demo repository.\n{e.GetDeepestException().Message}",
                    "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            ResetStatus();
        }

        private async Task Play(LocalDemo demo)
        {
            if (demo == null) throw new ArgumentNullException(nameof(demo));
            var game = demo.Version.GetGame();

            // Check whether the game is (still) installed.
            if (game == null)
            {
                LogHelper.Instance.LogException("You need to install {0} in order to play this demo.",
                    demo.Version.GetFullName());

                // Should never happen as the demo is in the local repository (in the game path).
                MessageBox.Show(this,
                    $"You need to install {demo.Version.GetFullName()} in order to play this demo.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            // Check whether the game is already running.
            if (game.IsRunning)
            {
                LogHelper.Instance.LogException(
                    "{0} is already running.\nYou need to close it in order to play this demo.",
                    demo.Version.GetFullName());

                MessageBox.Show(this,
                    $"{demo.Version.GetFullName()} is already running.\nYou need to close it in order to play this demo.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            // Check whether the demo file still exists.
            if (!demo.IsValid)
            {
                LogHelper.Instance.LogException("This demo file is corrupted or no longer exists.");

                MessageBox.Show(this, "This demo file is corrupted or no longer exists.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                await ReloadRepositories();
                return;
            }

            // Check for missing files.
            var missingFiles = game.GetMissingFiles(demo).ToList();
            if (missingFiles.Any())
            {
                var fileCount = missingFiles.Count();

                if (
                    MessageBox.Show(this,
                        $"You need to download {fileCount} missing files in order to play this demo:\n{string.Join("\n", missingFiles)}\n\nDo you want to download these now?",
                        "Missing Files", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    ToggleControls(false);

                    var processed = 0;
                    foreach (var missingFile in missingFiles)
                    {
                        var url = missingFile.Item1;
                        var path = missingFile.Item2;
                        statusLabel.Text = $"Downloading {Path.GetFileName(path)}...";

                        // Create the directory if it doesn't exist already.
                        if (!Directory.Exists(Path.GetDirectoryName(path)))
                            Directory.CreateDirectory(Path.GetDirectoryName(path) ??
                                                      throw new InvalidOperationException());

                        try
                        {
                            // Download the file.
                            var processed1 = processed;
                            await RedirectHelper.DownloadFile(url, path,
                                (sender, args) =>
                                {
                                    progressBar.Value =
                                        processed1 * 100 / fileCount + args.ProgressPercentage / fileCount;
                                });

                            if (game.Version == GameVersion.CallOfDuty2)
                            {
                                var directory = Path.GetDirectoryName(path);
                                var hunkusage = Path.Combine(directory ?? throw new InvalidOperationException(),
                                    "hunkusage.dat");
                                var hunkusageBackup = Path.Combine(directory, "hunkusage.dat.bak");

                                if (File.Exists(hunkusage))
                                {
                                    if (File.Exists(hunkusageBackup))
                                        File.Delete(hunkusageBackup);
                                    File.Move(hunkusage, hunkusageBackup);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            LogHelper.Instance.LogException(
                                $"Failed to download {Path.GetFileName(path)}.\n{e.GetDeepestException().Message}", e);

                            MessageBox.Show(this,
                                $"Failed to download {Path.GetFileName(path)}.\n{e.GetDeepestException().Message}",
                                "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                            return;
                        }

                        // Updates the progress bar.
                        processed++;
                        progressBar.Value = processed * 100 / fileCount;
                    }
                }
                else
                {
                    return;
                }
            }

            // Disable the controls in the form and update the status label.
            ToggleControls(false);
            statusLabel.Text = $"Waiting for {demo.Version.GetFullName()} to close...";

            // Bind the playback keys in the configuration of the demo's game.
            Factory.ConfigEditor.BindPlaybackKeys(game, demo.Mod);

            // Play the demo and wait for the game to close again.
            _isDemoRunning = true;
            _runningGameVersion = demo.Version;

            await Task.Run(() =>
            {
                Factory.DemoPlayback.Start(demo)
                    .WaitForExit();
            });

            _isDemoRunning = false;

            // Unbind the playback keys again.
            Factory.ConfigEditor.UnbindPlaybackKeys(game, demo.Mod);

            // Enable the controls in the form and reset the status label.
            ResetStatus();
        }

        private async Task<RemoteDemo> Upload(LocalDemo demo)
        {
            if (demo == null) throw new ArgumentNullException(nameof(demo));

            ToggleControls(false);
            statusLabel.Text = $"Uploading demo {demo.Name}...";

            try
            {
                var newDemo = await Factory.RemoteDemoRepository.Store(demo,
                    p =>
                    {
                        statusStrip1.Invoke((MethodInvoker) (() =>
                        {
                            if (p < 0) p = 0;
                            progressBar.Value = p;
                        }));
                    });

                await ReloadRepositories();
                remoteRepositoryView.SelectedDemo = newDemo;
                remoteRepositoryView.Focus();

                ResetStatus();

                return newDemo as RemoteDemo;
            }
            catch (Exception exception)
            {
                LogHelper.Instance.LogException("Failed to upload demo", exception);

                var deepsetException = exception.GetDeepestException();
                MessageBox.Show(this,
                    $"Failed to upload demo.\n{(deepsetException == null ? "An unknown error occured" : deepsetException.Message)}",
                    "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                ResetStatus();

                return null;
            }
        }

        private async Task<LocalDemo> Download(IDemo demo)
        {
            if (demo == null) throw new ArgumentNullException(nameof(demo));

            if (demo.Version.GetGame() == null)
            {
                MessageBox.Show(this,
                    $"To download this demo, you need to install {demo.Version.GetFullName()}.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }

            ToggleControls(false);
            statusLabel.Text = $"Downloading demo {demo.Name}...";

            try
            {
                var newDemo = await Factory.LocalDemoRepository.Store(demo,
                    p => { statusStrip1.Invoke((MethodInvoker) (() => { progressBar.Value = p; })); });

                await ReloadRepositories();
                localRepositoryView.SelectedDemo = newDemo;
                localRepositoryView.Focus();

                ResetStatus();

                return newDemo as LocalDemo;
            }
            catch (Exception e)
            {
                LogHelper.Instance.LogException("Failed to download demo", e);

                MessageBox.Show(this,
                    $"Failed to download demo.\n{e.GetDeepestException().Message}",
                    "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                ResetStatus();

                return null;
            }
        }

        private void ToggleControls(bool toggle)
        {
            mainSplitContainer.Enabled = toggle;
            reloadToolStripMenuItem.Enabled = toggle;
        }

        private void ResetStatus()
        {
            ToggleControls(true);

            statusLabel.Text = "Ready";
            progressBar.Value = 0;
        }

        private async void frmMain_Load(object sender, EventArgs e)
        {
            Text = $"Demo Manager - {typeof(FrmMain).Assembly.GetName().Version}";

            if (string.IsNullOrWhiteSpace(DemoManagerConfiguration.AuthKey))
                using (var frm = new FrmAuthKey())
                {
                    frm.ShowDialog();
                }

            if (string.IsNullOrWhiteSpace(DemoManagerConfiguration.AuthKey))
            {
                MessageBox.Show(this, "You have not entered a Auth Key so the application cannot run - closing now",
                    "No Auth Key",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Application.Exit();
            }

            localRepositoryView.Repository = Factory.LocalDemoRepository;
            remoteRepositoryView.Repository = Factory.RemoteDemoRepository;

            await ReloadRepositories();
        }

        private async void authKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmAuthKey())
            {
                frm.ShowDialog();
            }

            await ReloadRepositories();
        }

        #region User Interaction

        private async void play_Click(object sender, EventArgs e)
        {
            var demo = localRepositoryView.SelectedDemo as LocalDemo;

            if (demo == null)
                return;

            await Play(demo);
        }

        private async void uploadButton_Click(object sender, EventArgs e)
        {
            var demo = localRepositoryView.SelectedDemo as LocalDemo;

            if (demo == null)
                return;

            // Demo still has a default name - require it to be renamed.
            if (demo.Name.StartsWith("demo") && demo.Name.Skip(4).All(c => '0' <= c && c <= '9'))
            {
                if (
                    MessageBox.Show(this, "You are trying to upload a demo which has a default demo name.\n" +
                                          "Before you can upload this demo, you'll need to rename it.", "Upload demo",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning) == DialogResult.Cancel)
                    return;

                var form = new FrmRename("demo", demo.Name);

                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    demo.Name = form.Value;

                    localRepositoryView.ReloadLabels();
                    localRepositoryView.Focus();
                }
                else
                {
                    return;
                }
            }

            await Upload(demo);
        }

        private async void downloadButton_Click(object sender, EventArgs e)
        {
            var demo = remoteRepositoryView.SelectedDemo as RemoteDemo;

            if (demo == null)
                return;

            await Download(demo);
        }

        private async void downloadAndPlayButton_Click(object sender, EventArgs e)
        {
            var demo = remoteRepositoryView.SelectedDemo as RemoteDemo;

            if (demo == null)
                return;

            var localDemo = await Download(demo);

            if (localDemo != null)
                await Play(localDemo);
        }

        private void localRepositoryView_SelectedIndexChanged(object sender, EventArgs e)
        {
            localDemoPanel.Enabled = localRepositoryView.SelectedDemo != null;
        }

        private void remoteRepositoryView_SelectedIndexChanged(object sender, EventArgs e)
        {
            remoteDemoPanel.Enabled = remoteRepositoryView.SelectedDemo != null;
        }

        private async void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await ReloadRepositories();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void keyBindsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FrmKeybinds();
            form.ShowDialog(this);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmAbout().ShowDialog(this);
        }

        private void renameButton_Click(object sender, EventArgs e)
        {
            var localDemo = localRepositoryView.SelectedDemo as LocalDemo;
            if (localDemo == null) return;

            var form = new FrmRename("demo", localDemo.Name);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                localDemo.Name = form.Value;

                localRepositoryView.ReloadLabels();
                localRepositoryView.Focus();
            }
        }

        private async void deleteButton_Click(object sender, EventArgs e)
        {
            var localDemo = localRepositoryView.SelectedDemo as LocalDemo;

            if (localDemo == null)
                return;

            if (
                MessageBox.Show(this, "Are you sure you wish to delete this file?", "Confirm", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                return;

            localDemo.Delete();

            await ReloadRepositories();
        }

        #endregion

        #region Overrides of Form

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Forms.Form.Shown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnShown(EventArgs e)
        {
            var configEditor = Factory.ConfigEditor;
            if (configEditor.RecordingBinds.Any(b => b.Key == null))
                if (
                    MessageBox.Show(this,
                        "You have not yet bound keys for recording demos. Would you like to configure this now?",
                        "Recording", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var form = new FrmKeybinds();
                    form.ShowDialog(this);
                }

            base.OnShown(e);
        }

        /// <summary>
        ///     Raises the <see cref="E:System.Windows.Forms.Form.Closing" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (_isDemoRunning)
                if (
                    MessageBox.Show(this,
                        $"A demo is still playing. Your key bindings will only be restored if quit {_runningGameVersion.GetFullName()} before you close this application.\n" +
                        "Are you sure you want to close this application?",
                        "Demo is running", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }

            base.OnClosing(e);
        }

        #endregion
    }
}