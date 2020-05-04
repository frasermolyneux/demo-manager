using System.Linq;
using System.Windows.Forms;

namespace DemoManager.App
{
    internal class KeysBox : ComboBox
    {
        private static readonly KeyItem[] Keys =
        {
            new KeyItem(null, "None"),
            new KeyItem("A", true),
            new KeyItem("B", true),
            new KeyItem("C", true),
            new KeyItem("D", true),
            new KeyItem("E", true),
            new KeyItem("F", true),
            new KeyItem("G", true),
            new KeyItem("H", true),
            new KeyItem("I", true),
            new KeyItem("J", true),
            new KeyItem("K", true),
            new KeyItem("L", true),
            new KeyItem("M", true),
            new KeyItem("N", true),
            new KeyItem("O", true),
            new KeyItem("P", true),
            new KeyItem("Q", true),
            new KeyItem("R", true),
            new KeyItem("S", true),
            new KeyItem("T", true),
            new KeyItem("U", true),
            new KeyItem("V", true),
            new KeyItem("W", true),
            new KeyItem("X", true),
            new KeyItem("Y", true),
            new KeyItem("Z", true),
            new KeyItem("~", true),
            new KeyItem("`", true),
            new KeyItem("1", true),
            new KeyItem("2", true),
            new KeyItem("3", true),
            new KeyItem("4", true),
            new KeyItem("5", true),
            new KeyItem("6", true),
            new KeyItem("7", true),
            new KeyItem("8", true),
            new KeyItem("9", true),
            new KeyItem("0", true),
            new KeyItem("-", true),
            new KeyItem("=", true),
//                new KeyItem("ESCAPE", "Escape"),  // Can't be bound
            new KeyItem("F1"),
            new KeyItem("F2"),
            new KeyItem("F3"),
            new KeyItem("F4"),
            new KeyItem("F5"),
            new KeyItem("F6"),
            new KeyItem("F7"),
            new KeyItem("F8"),
            new KeyItem("F9"),
            new KeyItem("F10"),
            new KeyItem("F11"),
            new KeyItem("F12"),
            new KeyItem("CTRL", "Control"),
            new KeyItem("SHIFT", "Shift"),
            new KeyItem("TAB", "Tab", true),
            new KeyItem("SPACE", "Space bar", true),
            new KeyItem("INS", "Insert"),
            new KeyItem("DEL", "Delete"),
            new KeyItem("PGDN", "Page Down"),
            new KeyItem("PGUP", "Page Up"),
            new KeyItem("HOME", "Home"),
            new KeyItem("End", "End"),
            new KeyItem("PAUSE", "Pause"),
            new KeyItem("KP_END", "Numpad 1"),
            new KeyItem("KP_DOWNARROW", "Numpad 2"),
            new KeyItem("KP_PGDN", "Numpad 3"),
            new KeyItem("KP_LEFTARROW", "Numpad 4"),
            new KeyItem("KP_5", "Numpad 5"),
            new KeyItem("KP_RIGHTARROW", "Numpad 6"),
            new KeyItem("KP_HOME", "Numpad 7"),
            new KeyItem("KP_UPARROW", "Numpad 8"),
            new KeyItem("KP_PGUP", "Numpad 9"),
            new KeyItem("KP_INS", "Numpad 0"),
            new KeyItem("KP_SLASH", "Numpad /"),
            new KeyItem("KP_MINUS", "Numpad -"),
            new KeyItem("KP_PLUS", "Numpad +"),
            new KeyItem("KP_DEL", "Numpad ."),
            new KeyItem("KP_ENTER", "Numpad Enter"),
            new KeyItem(",", true),
            new KeyItem(".", true),
            new KeyItem("/", true),
            new KeyItem("\\", true),
            new KeyItem("'", true),
            new KeyItem("[", true),
            new KeyItem("]", true),
            new KeyItem("SEMICOLON", ";", true),
            new KeyItem("MOUSE1", "Mouse Button 1", true),
            new KeyItem("MOUSE2", "Mouse Button 2", true),
            new KeyItem("MOUSE3", "Mouse Button 3"),
            new KeyItem("MOUSE4", "Mouse Button 4"),
            new KeyItem("MOUSE5", "Mouse Button 5"),
            new KeyItem("MWHEELDOWN", "Mouse Wheel Down"),
            new KeyItem("MWHEELUP", "Mouse Wheel Up")
        };

        private bool hideKeysUnavailableInCod2;

        public KeysBox()
        {
            DropDownStyle = ComboBoxStyle.DropDownList;

            // ReSharper disable once CoVariantArrayConversion
            Items.AddRange(Keys);
            SelectedItem = Items[0];
        }

        public bool HideKeysUnavailableInCod2
        {
            get => hideKeysUnavailableInCod2;
            set
            {
                if (hideKeysUnavailableInCod2 == value)
                    return;

                var selectedItem = SelectedItem;

                Items.Clear();
                // ReSharper disable CoVariantArrayConversion
                Items.AddRange(value ? Keys.Where(k => !k.UnavailableInCod2).ToArray() : Keys);
                // ReSharper restore CoVariantArrayConversion

                if (Items.Contains(selectedItem))
                    SelectedItem = selectedItem;
                else
                    SelectedIndex = 0;

                hideKeysUnavailableInCod2 = value;
            }
        }

        public string SelectedKey
        {
            get => (SelectedItem as KeyItem)?.Value;
            set { SelectedItem = Items.OfType<KeyItem>().FirstOrDefault(i => i.Value == value) ?? Items[0]; }
        }

        public static bool IsKeyAvailableInCod2(string key)
        {
            return !(Keys.FirstOrDefault(k => k.Value == key)?.UnavailableInCod2 ?? false);
        }

        private class KeyItem
        {
            public KeyItem(string value, string displayName, bool unavailableInCod2 = false)
            {
                DisplayName = displayName;
                UnavailableInCod2 = unavailableInCod2;
                Value = value;
            }

            public KeyItem(string value)
                : this(value, value)
            {
            }

            public KeyItem(string value, bool unavailableInCod2)
                : this(value, value, unavailableInCod2)
            {
            }

            public string Value { get; }
            private string DisplayName { get; }
            public bool UnavailableInCod2 { get; }

            #region Overrides of ValueType

            public override string ToString()
            {
                return DisplayName;
            }

            #endregion
        }
    }
}