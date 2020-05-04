using System.Collections;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DemoManager.App.Helpers;
using DemoManager.App.Objects;
using DemoManager.App.Properties;
using DemoManager.App.Repositories;

namespace DemoManager.App
{
    internal class RepositoryView : ListView
    {
        private int _sortColumn;

        public RepositoryView()
        {
            Columns.Add("Name").Width = 100;
            Columns.Add("Server").Width = 200;
            Columns.Add("Mod").Width = 75;
            Columns.Add("Game Type").Width = 75;
            Columns.Add("Map").Width = 100;
            Columns.Add("Date").Width = 125;
            Columns.Add("Size").Width = 100;

            View = View.Details;
            FullRowSelect = true;
            MultiSelect = false;
            AllowColumnReorder = true;
            HideSelection = false;
            Sorting = SortOrder.Descending;

            SmallImageList = new ImageList();
            SmallImageList.Images.AddRange(new[] {Resources.desc, Resources.asc});
        }

        public IDemoRepository Repository { get; set; }

        public IDemo SelectedDemo
        {
            get => SelectedItems.Count > 0 ? SelectedItems[0].Tag as IDemo : null;
            set
            {
                SelectedItems.Clear();

                var item = Items.OfType<ListViewItem>().FirstOrDefault(i => value.Equals(i.Tag));

                if (item != null)
                    item.Selected = true;
            }
        }

        #region Overrides of ListView

        protected override void OnColumnClick(ColumnClickEventArgs e)
        {
            Columns[_sortColumn].ImageIndex = -1;
            Columns[_sortColumn].TextAlign = HorizontalAlignment.Left;

            if (e.Column != _sortColumn)
            {
                _sortColumn = e.Column;
                Sorting = SortOrder.Ascending;
            }
            else
            {
                Sorting = Sorting == SortOrder.Ascending
                    ? SortOrder.Descending
                    : SortOrder.Ascending;
            }

            Columns[e.Column].ImageIndex = Sorting == SortOrder.Ascending ? 0 : 1;

            ListViewItemSorter = new ListViewItemComparer(e.Column, Sorting);
            Sort();
        }

        #endregion

        public async Task Reload()
        {
            Items.Clear();
            Groups.Clear();

            Columns[_sortColumn].ImageIndex = -1;

            if (Repository == null) return;

            IDemo[] demos = null;

            await Task.Run(() => { demos = Repository.Demos.ToArray(); });

            Groups.AddRange(demos.Select(demo => demo.Version)
                .Distinct()
                .OrderBy(version => version.ToString())
                .Select(version => new ListViewGroup(version.GetFullName()) {Tag = version})
                .ToArray());

            Items.AddRange(demos.Select(
                demo =>
                {
                    var item =
                        new ListViewItem(
                            Groups.OfType<ListViewGroup>()
                                .FirstOrDefault(group => (GameVersion) group.Tag == demo.Version))
                        {
                            Tag = demo,
                            Text = demo.Name
                        };

                    item.SubItems.Add(GameHelper.StripColors(demo.Server ?? "???"));
                    item.SubItems.Add(demo.Mod ?? "???");
                    item.SubItems.Add(GameHelper.GetFullGamemodeName(demo.GameType));
                    item.SubItems.Add(demo.Map ?? "???");
                    item.SubItems.Add(demo.Date.ToLocalTime().ToString(CultureInfo.InvariantCulture));

                    if (demo.Size < 1024 * 1024)
                        item.SubItems.Add(demo.Size / 1024 + "KB");
                    else
                        item.SubItems.Add(((double) (demo.Size / 1024) / 1024).ToString("0.00") + "MB");
                    return item;
                }).ToArray());
        }

        public void ReloadLabels()
        {
            foreach (var item in Items.OfType<ListViewItem>())
            {
                var demo = item.Tag as IDemo;

                item.SubItems[0].Text = demo.Name;
                item.SubItems[1].Text = GameHelper.StripColors(demo.Server);
                item.SubItems[2].Text = demo.Mod;
                item.SubItems[3].Text = GameHelper.GetFullGamemodeName(demo.GameType);
                item.SubItems[4].Text = demo.Map;
                item.SubItems[5].Text = demo.Date.ToLocalTime().ToString(CultureInfo.InvariantCulture);

                if (demo.Size < 1024 * 1024)
                    item.SubItems[6].Text = demo.Size / 1024 + "KB";
                else
                    item.SubItems[6].Text = ((double) (demo.Size / 1024) / 1024).ToString("0.00") + "MB";
            }
        }

        private class ListViewItemComparer : IComparer
        {
            private readonly int _column;
            private readonly SortOrder _sortOrder;

            public ListViewItemComparer(int column, SortOrder sortOrder)
            {
                _column = column;
                _sortOrder = sortOrder;
            }

            public int Compare(object x, object y)
            {
                return (_sortOrder == SortOrder.Descending ? -1 : 1) *
                       string.CompareOrdinal(((ListViewItem) x).SubItems[_column].Text,
                           ((ListViewItem) y).SubItems[_column].Text);
            }
        }
    }
}