using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace StockSpace
{
    internal static class DashboardNavigation
    {
        internal static void WireSectionPages(Form current, Button transferGoods, Button releaseGoods, Button history, Button reports, Button settings)
        {
            transferGoods.Click += (sender, e) => Navigate(current, "Transfer Goods");
            releaseGoods.Click += (sender, e) => Navigate(current, "Release Goods");
            history.Click += (sender, e) => Navigate(current, "History");
            reports.Click += (sender, e) => Navigate(current, "Reports");
            settings.Click += (sender, e) => Navigate(current, "Settings");
        }

        internal static void Navigate(Form current, string page)
        {
            Form destination;

            switch (page)
            {
                case "Dashboard":
                    if (current is Dashboardp) return;
                    destination = new Dashboardp();
                    break;
                case "Storages":
                    if (current is storages) return;
                    destination = new storages();
                    break;
                case "Receive Goods":
                    if (current is receivegood) return;
                    destination = new receivegood();
                    break;
                case "Stored Goods":
                    if (current is StoredGoods) return;
                    destination = new StoredGoods();
                    break;
                case "Transfer Goods":
                    if (current is TransferGoodsPage) return;
                    destination = new TransferGoodsPage();
                    break;
                case "Release Goods":
                    if (current is ReleaseGoodsPage) return;
                    destination = new ReleaseGoodsPage();
                    break;
                case "History":
                    if (current is HistoryPage) return;
                    destination = new HistoryPage();
                    break;
                case "Reports":
                    if (current is ReportsPage) return;
                    destination = new ReportsPage();
                    break;
                case "Settings":
                    if (current is SettingsPage) return;
                    destination = new SettingsPage();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(page), page, "Unknown dashboard page.");
            }

            destination.WindowState = FormWindowState.Maximized;
            destination.Show();
            current.Hide();
        }
    }

    public abstract class DashboardSectionPage : Form
    {
        protected DashboardSectionPage(string pageTitle)
        {
            Text = pageTitle + " - StockSpace";
            WindowState = FormWindowState.Maximized;
            BackColor = Color.FromArgb(224, 224, 224);
            MinimumSize = new Size(1000, 650);

            var resources = new ComponentResourceManager(typeof(Dashboardp));
            var root = new TableLayoutPanel
            {
                ColumnCount = 2,
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                RowCount = 1
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 207F));
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var sidebar = CreateSidebar(resources);
            var content = new Panel { Dock = DockStyle.Fill };
            var contentLayout = new TableLayoutPanel
            {
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                RowCount = 2
            };
            contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 49F));
            contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var topBar = new Panel
            {
                BackColor = Color.Black,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill,
                Name = "topBar"
            };
            var pageContent = new Panel
            {
                BackColor = Color.FromArgb(224, 224, 224),
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 24, 30, 24)
            };
            pageContent.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 23F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(0, 0),
                Text = pageTitle
            });

            contentLayout.Controls.Add(topBar, 0, 0);
            contentLayout.Controls.Add(pageContent, 0, 1);
            content.Controls.Add(contentLayout);

            root.Controls.Add(sidebar, 0, 0);
            root.Controls.Add(content, 1, 0);
            Controls.Add(root);
        }

        private static Panel CreateSidebar(ComponentResourceManager resources)
        {
            var sidebar = new Panel
            {
                BackColor = Color.Black,
                Dock = DockStyle.Fill,
                Name = "sidebar"
            };

            var decorativeImage = new PictureBox
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom,
                Image = Properties.Resources.yellow_line_copy,
                Location = new Point(-61, 0),
                Name = "sidebarAccent",
                Size = new Size(341, 311),
                SizeMode = PictureBoxSizeMode.Zoom,
                TabStop = false
            };
            decorativeImage.Top = sidebar.ClientSize.Height - decorativeImage.Height;
            sidebar.Controls.Add(decorativeImage);
            sidebar.Resize += (sender, e) => decorativeImage.Top = sidebar.ClientSize.Height - decorativeImage.Height;

            var logo = new PictureBox
            {
                Dock = DockStyle.Top,
                Image = resources.GetObject("pictureBox1.Image") as Image,
                Name = "logo",
                Size = new Size(207, 51),
                SizeMode = PictureBoxSizeMode.Zoom,
                TabStop = false
            };
            sidebar.Controls.Add(logo);

            AddNavigationButton(sidebar, resources, "Dashboard", "Dashboard.Image", 54);
            AddNavigationButton(sidebar, resources, "Storages", "Storages.Image", 109);
            AddNavigationButton(sidebar, resources, "Receive Goods", "Receive_Goods.Image", 164);
            AddNavigationButton(sidebar, resources, "Stored Goods", "Stored_Goods.Image", 219);
            AddNavigationButton(sidebar, resources, "Transfer Goods", "Transfer_Goods.Image", 274);
            AddNavigationButton(sidebar, resources, "Release Goods", "Release_Goods.Image", 329);
            AddNavigationButton(sidebar, resources, "History", "History.Image", 384);
            AddNavigationButton(sidebar, resources, "Reports", "Reports.Image", 439);
            AddNavigationButton(sidebar, resources, "Settings", "Settings.Image", 494);

            return sidebar;
        }

        private static void AddNavigationButton(Panel sidebar, ComponentResourceManager resources, string page, string imageKey, int top)
        {
            var button = new Button
            {
                BackColor = Color.Black,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Popup,
                Font = new Font("Franklin Gothic Medium", 10.2F),
                ForeColor = Color.White,
                Image = resources.GetObject(imageKey) as Image,
                ImageAlign = ContentAlignment.MiddleLeft,
                Location = new Point(0, top),
                Name = page.Replace(" ", "_"),
                Size = new Size(207, 49),
                Text = "       " + page,
                TextAlign = ContentAlignment.MiddleLeft,
                UseVisualStyleBackColor = false
            };
            button.FlatAppearance.BorderSize = 0;
            button.Click += (sender, e) => DashboardNavigation.Navigate((Form)sidebar.TopLevelControl, page);
            sidebar.Controls.Add(button);
        }
    }

    public sealed class TransferGoodsPage : DashboardSectionPage
    {
        public TransferGoodsPage() : base("Transfer Goods") { }
    }

    public sealed class ReleaseGoodsPage : DashboardSectionPage
    {
        public ReleaseGoodsPage() : base("Release Goods") { }
    }

    public sealed class HistoryPage : DashboardSectionPage
    {
        public HistoryPage() : base("History") { }
    }

    public sealed class ReportsPage : DashboardSectionPage
    {
        public ReportsPage() : base("Reports") { }
    }

    public sealed class SettingsPage : DashboardSectionPage
    {
        public SettingsPage() : base("Settings") { }
    }
}
