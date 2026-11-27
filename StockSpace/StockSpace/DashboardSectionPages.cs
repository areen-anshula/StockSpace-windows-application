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
            pageContent.Controls.Add(CreatePageContent(pageTitle));

            contentLayout.Controls.Add(topBar, 0, 0);
            contentLayout.Controls.Add(pageContent, 0, 1);
            content.Controls.Add(contentLayout);

            root.Controls.Add(sidebar, 0, 0);
            root.Controls.Add(content, 1, 0);
            Controls.Add(root);
        }

        private static Control CreatePageContent(string pageTitle)
        {
            var layout = new TableLayoutPanel
            {
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                Margin = Padding.Empty,
                RowCount = 3
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 78F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 112F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            var header = new Panel { Dock = DockStyle.Fill };
            header.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 23F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(0, 0),
                Text = pageTitle
            });
            header.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(2, 45),
                Text = GetPageDescription(pageTitle)
            });

            layout.Controls.Add(header, 0, 0);
            layout.Controls.Add(CreateMetricCards(pageTitle), 0, 1);
            layout.Controls.Add(CreatePageOverview(pageTitle), 0, 2);
            return layout;
        }

        private static string GetPageDescription(string pageTitle)
        {
            switch (pageTitle)
            {
                case "Transfer Goods":
                    return "Review stock movement between storage locations.";
                case "Release Goods":
                    return "Review goods released from your inventory.";
                case "History":
                    return "A timeline of inventory activity.";
                case "Reports":
                    return "Inventory summaries and reporting.";
                default:
                    return "Manage your StockSpace workspace.";
            }
        }

        private static Control CreateMetricCards(string pageTitle)
        {
            string[] headings;
            string[] values;
            switch (pageTitle)
            {
                case "Transfer Goods":
                    headings = new[] { "TRANSFERS", "IN PROGRESS", "LOCATIONS" };
                    values = new[] { "—", "—", "—" };
                    break;
                case "Release Goods":
                    headings = new[] { "RELEASES", "PENDING", "UNITS RELEASED" };
                    values = new[] { "—", "—", "—" };
                    break;
                case "History":
                    headings = new[] { "RECORDED EVENTS", "DATE RANGE", "ACTIVITY SOURCE" };
                    values = new[] { "—", "All activity", "Inventory" };
                    break;
                case "Reports":
                    headings = new[] { "AVAILABLE REPORTS", "LAST GENERATED", "DATA SOURCE" };
                    values = new[] { "—", "—", "Inventory" };
                    break;
                default:
                    headings = new[] { "WORKSPACE", "PREFERENCES", "DATA CONNECTION" };
                    values = new[] { "StockSpace", "Default", "Not connected" };
                    break;
            }

            var cards = new TableLayoutPanel
            {
                ColumnCount = headings.Length,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 12),
                RowCount = 1
            };
            for (var i = 0; i < headings.Length; i++)
            {
                cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F / headings.Length));
                cards.Controls.Add(CreateMetricCard(headings[i], values[i]), i, 0);
            }
            cards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            return cards;
        }

        private static Control CreateMetricCard(string heading, string value)
        {
            var card = new Panel
            {
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 12, 0),
                Padding = new Padding(16, 12, 12, 10)
            };
            card.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(16, 12),
                Text = heading
            });
            card.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(16, 38),
                Text = value
            });
            return card;
        }

        private static Control CreatePageOverview(string pageTitle)
        {
            var panel = new Panel
            {
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                Padding = new Padding(22)
            };
            panel.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(22, 20),
                Text = GetOverviewTitle(pageTitle)
            });
            panel.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(24, 54),
                MaximumSize = new Size(800, 0),
                Text = GetOverviewDescription(pageTitle)
            });
            return panel;
        }

        private static string GetOverviewTitle(string pageTitle)
        {
            switch (pageTitle)
            {
                case "Transfer Goods":
                    return "Transfer overview";
                case "Release Goods":
                    return "Release overview";
                case "History":
                    return "Recent activity";
                case "Reports":
                    return "Report center";
                default:
                    return "Workspace preferences";
            }
        }

        private static string GetOverviewDescription(string pageTitle)
        {
            switch (pageTitle)
            {
                case "Transfer Goods":
                    return "Transfer activity will appear here when inventory movement is connected.";
                case "Release Goods":
                    return "Release activity will appear here when inventory transactions are connected.";
                case "History":
                    return "There is no activity to display yet. Inventory events will be listed here.";
                case "Reports":
                    return "Reports will be available here once inventory data is connected.";
                default:
                    return "Workspace settings will be available here as configuration options are added.";
            }
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
