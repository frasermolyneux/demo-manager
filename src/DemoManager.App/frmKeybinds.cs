using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DemoManager.App.Objects;

// ReSharper disable LocalizableElement

namespace DemoManager.App
{
    public partial class FrmKeybinds : Form
    {
        public FrmKeybinds()
        {
            InitializeComponent();

            var configEditor = Factory.ConfigEditor;

            // Populate the group boxes with the existing key bindings.
            FillGroupBox(configEditor.PlaybackBinds, playbackPanel);
            FillGroupBox(configEditor.RecordingBinds, recordingPanel);

            hideUnavailableCheckBox.Checked = configEditor.PlaybackBinds.All(b => KeysBox.IsKeyAvailableInCod2(b.Key));
        }

        private static void FillGroupBox(IEnumerable<Bind> binds, Control panel)
        {
            const int keyBoxOffset = 260;
            const int keyBoxWidth = 125;

            var x = panel.Padding.Left;
            var y = panel.Padding.Top;
            var i = 0;
            var columns = (panel.Width - panel.Padding.Horizontal) / (keyBoxOffset + keyBoxWidth);
            var enumerable = binds.ToList();
            var bindsCount = enumerable.Count();
            var rows = bindsCount / columns + Math.Min(bindsCount % columns, 1);

            // Create a list of labels and keysboxes in the specified groupbox. Tag the groupbox with the bind it
            // represents.
            foreach (var bind in enumerable)
            {
                // Create the controls.
                var label = new Label
                {
                    Text = bind.Description + ":",
                    AutoSize = true
                };
                var keyBox = new KeysBox
                {
                    SelectedKey = bind.Key,
                    Width = keyBoxWidth,
                    Tag = bind
                };

                // Center the controls using the y-coordinate and set their locations.
                y += keyBox.Margin.Top;

                label.Location = new Point(x, y + 3);
                keyBox.Location = new Point(x + keyBoxOffset, y);

                y += keyBox.Height + keyBox.Margin.Bottom;
                i++;

                // If the coordinates for the next row are outside the boundaries of the group box, move the coordinates
                // to a new column.
                if (i >= rows)
                {
                    x += keyBoxOffset + keyBoxWidth;
                    y = panel.Padding.Top;
                    i = 0;
                }

                // Add the controls to the groupbox.
                panel.Controls.Add(label);
                panel.Controls.Add(keyBox);
            }
        }

        private void SaveBinds(Panel panel)
        {
            // Store the selected key of every keysbox in the specified groupbox in the bind it represents.
            foreach (var keyBox in panel.Controls.OfType<KeysBox>())
            {
                var bind = keyBox.Tag as Bind;

                if (bind != null)
                    bind.Key = keyBox.SelectedKey;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // Save bindings to config editor.
            SaveBinds(playbackPanel);
            SaveBinds(recordingPanel);

            var configEditor = Factory.ConfigEditor;

            // Save bindings to configuration.
            configEditor.SaveBinds();

            // Re bind the recording keys in every installed game.
            foreach (var game in Factory.GameDetection.GetInstalledGames())
            {
                // Unbind any previously bound recording keys.
                configEditor.UnbindKeys(game, configEditor.RecordingBinds);

                // Re bind the recording keys.
                if (!configEditor.BindKeysInAllMods(game, configEditor.RecordingBinds))
                    MessageBox.Show(this, $"Failed to bind recording keys for {game}.\n" +
                                          "Have you not selected an activate player profile?",
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void hideUnavailableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (
                var keysBox in
                playbackPanel.Controls.OfType<KeysBox>().Concat(recordingPanel.Controls.OfType<KeysBox>()))
                keysBox.HideKeysUnavailableInCod2 = hideUnavailableCheckBox.Checked;
        }
    }
}