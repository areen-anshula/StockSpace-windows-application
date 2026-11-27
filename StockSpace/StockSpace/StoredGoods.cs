using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace StockSpace
{
    public partial class StoredGoods : Form
    {
        private static readonly Color SidebarColor = Color.FromArgb(24, 27, 32);
        private static readonly Color AccentColor = Color.FromArgb(245, 158, 11);
        private static readonly Color PageColor = Color.FromArgb(244, 246, 248);
        private static readonly Color TextColor = Color.FromArgb(31, 41, 55);
        private static readonly Color MutedColor = Color.FromArgb(107, 114, 128);

        private readonly List<StoredGood> goods;

        public StoredGoods()
        {
            goods = CreateSampleGoods();

            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            //DashboardNavigation.WireSectionPages(this, Transfer_Goods, Release_Goods, History, Reports, Settings);
            PopulateGrid(goods);
            UpdateSummary();
        }

        private void InitializeScreen()
        {
            Text = "Stored Goods - StockSpace";
            StartPosition = FormStartPosition.CenterScreen;
            WindowState = FormWindowState.Maximized;
            MinimumSize = new Size(1000, 650);
            BackColor = PageColor;
            Font = new Font("Segoe UI", 9F);

            Controls.Add(CreateSidebar());
            Controls.Add(CreateMainContent());
        }

        private Control CreateSidebar()
        {
            var sidebar = new Panel
            {
                BackColor = SidebarColor,
                Dock = DockStyle.Left,
                Width = 220
            };

            var brand = new Label
            {
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 17F, FontStyle.Bold),
                Location = new Point(24, 22),
                Text = "StockSpace"
            };

            var brandSubtitle = new Label
            {
                AutoSize = true,
                ForeColor = Color.FromArgb(170, 176, 184),
                Font = new Font("Segoe UI", 8.5F),
                Location = new Point(26, 54),
                Text = "WAREHOUSE MANAGEMENT"
            };

            sidebar.Controls.Add(brand);
            sidebar.Controls.Add(brandSubtitle);
            sidebar.Controls.Add(CreateNavigationButton("Dashboard", false, NavigateToDashboard, 108));
            sidebar.Controls.Add(CreateNavigationButton("Storages", false, NavigateToStorages, 160));
            sidebar.Controls.Add(CreateNavigationButton("Receive Goods", false, NavigateToReceiving, 212));
            sidebar.Controls.Add(CreateNavigationButton("Stored Goods", true, null, 264));

            var footer = new Label
            {
                AutoSize = false,
                ForeColor = Color.FromArgb(148, 153, 161),
                Font = new Font("Segoe UI", 8F),
                Location = new Point(24, 0),
                Size = new Size(175, 42),
                Text = "Inventory at a glance",
                TextAlign = ContentAlignment.MiddleLeft,
                Anchor = AnchorStyles.Left | AnchorStyles.Bottom
            };
            sidebar.Controls.Add(footer);
            sidebar.Resize += (sender, e) => footer.Top = sidebar.ClientSize.Height - footer.Height - 16;

            return sidebar;
        }

        private Button CreateNavigationButton(string text, bool active, EventHandler click, int top)
        {
            var button = new Button
            {
                BackColor = active ? Color.FromArgb(55, 48, 36) : SidebarColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, active ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = active ? Color.White : Color.FromArgb(213, 216, 221),
                Location = new Point(12, top),
                Size = new Size(196, 44),
                Text = "   " + text,
                TextAlign = ContentAlignment.MiddleLeft,
                UseVisualStyleBackColor = false
            };
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(43, 47, 54);

            if (active)
            {
                var marker = new Panel
                {
                    BackColor = AccentColor,
                    Dock = DockStyle.Left,
                    Width = 4
                };
                button.Controls.Add(marker);
            }

            if (click != null)
            {
                button.Click += click;
            }

            return button;
        }

        private Control CreateMainContent()
        {
            var content = new Panel
            {
                BackColor = PageColor,
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 22, 30, 24)
            };

            var layout = new TableLayoutPanel
            {
                ColumnCount = 1,
                Dock = DockStyle.Fill,
                RowCount = 4
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 94F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 132F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            layout.Controls.Add(CreateHeader(), 0, 0);
            layout.Controls.Add(CreateSummaryCards(), 0, 1);
            layout.Controls.Add(CreateFilterBar(), 0, 2);
            layout.Controls.Add(CreateGoodsGrid(), 0, 3);
            content.Controls.Add(layout);

            return content;
        }

        private Control CreateHeader()
        {
            var header = new Panel { Dock = DockStyle.Fill };
            header.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 23F, FontStyle.Bold),
                ForeColor = TextColor,
                Location = new Point(0, 0),
                Text = "Stored Goods"
            });
            header.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = MutedColor,
                Location = new Point(2, 45),
                Text = "Sample inventory for preview - not connected to live stock data."
            });

            var receiveButton = new Button
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = AccentColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.White,
                Height = 38,
                Text = "Receive goods",
                Width = 142
            };
            receiveButton.FlatAppearance.BorderSize = 0;
            receiveButton.Location = new Point(header.ClientSize.Width - receiveButton.Width, 10);
            receiveButton.Click += NavigateToReceiving;
            header.Resize += (sender, e) =>
                receiveButton.Left = header.ClientSize.Width - receiveButton.Width;
            header.Controls.Add(receiveButton);

            return header;
        }

        private Control CreateSummaryCards()
        {
            var cards = new TableLayoutPanel
            {
                BackColor = PageColor,
                ColumnCount = 3,
                Dock = DockStyle.Fill,
                RowCount = 1
            };
            cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.334F));
            cards.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            cards.Controls.Add(CreateSummaryCard("TOTAL SKUs", skuCountValue), 0, 0);
            cards.Controls.Add(CreateSummaryCard("UNITS IN STOCK", unitsValue), 1, 0);
            cards.Controls.Add(CreateSummaryCard("LOW STOCK ITEMS", lowStockValue), 2, 0);
            return cards;
        }

        private Control CreateSummaryCard(string title, Label value)
        {
            var card = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 14, 8)
            };
            card.Controls.Add(new Panel
            {
                BackColor = AccentColor,
                Dock = DockStyle.Left,
                Width = 4
            });
            card.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                ForeColor = MutedColor,
                Location = new Point(18, 17),
                Text = title
            });
            value.AutoSize = true;
            value.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            value.ForeColor = TextColor;
            value.Location = new Point(17, 43);
            card.Controls.Add(value);
            return card;
        }

        private Control CreateFilterBar()
        {
            var bar = new Panel { Dock = DockStyle.Fill };
            bar.Controls.Add(new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = TextColor,
                Location = new Point(0, 22),
                Text = "Search"
            });

            searchBox.BorderStyle = BorderStyle.FixedSingle;
            searchBox.Font = new Font("Segoe UI", 9.5F);
            searchBox.Location = new Point(62, 16);
            searchBox.Size = new Size(280, 28);
            searchBox.TextChanged += (sender, e) => ApplyFilters();
            bar.Controls.Add(searchBox);

            bar.Controls.Add(new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = TextColor,
                Location = new Point(390, 22),
                Text = "Status"
            });
            statusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            statusFilter.Font = new Font("Segoe UI", 9F);
            statusFilter.Items.AddRange(new object[] { "All statuses", "In stock", "Low stock", "Out of stock" });
            statusFilter.Location = new Point(448, 16);
            statusFilter.Size = new Size(160, 28);
            statusFilter.SelectedIndex = 0;
            statusFilter.SelectedIndexChanged += (sender, e) => ApplyFilters();
            bar.Controls.Add(statusFilter);

            return bar;
        }

        private Control CreateGoodsGrid()
        {
            goodsGrid.AllowUserToAddRows = false;
            goodsGrid.AllowUserToDeleteRows = false;
            goodsGrid.AllowUserToResizeRows = false;
            goodsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            goodsGrid.BackgroundColor = Color.White;
            goodsGrid.BorderStyle = BorderStyle.None;
            goodsGrid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            goodsGrid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            goodsGrid.ColumnHeadersHeight = 46;
            goodsGrid.EnableHeadersVisualStyles = false;
            goodsGrid.GridColor = Color.FromArgb(232, 235, 239);
            goodsGrid.ReadOnly = true;
            goodsGrid.RowHeadersVisible = false;
            goodsGrid.RowTemplate.Height = 44;
            goodsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            goodsGrid.Dock = DockStyle.Fill;
            goodsGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 251);
            goodsGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            goodsGrid.ColumnHeadersDefaultCellStyle.ForeColor = MutedColor;
            goodsGrid.DefaultCellStyle.BackColor = Color.White;
            goodsGrid.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            goodsGrid.DefaultCellStyle.ForeColor = TextColor;
            goodsGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 247, 230);
            goodsGrid.DefaultCellStyle.SelectionForeColor = TextColor;
            goodsGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(252, 252, 253);
            goodsGrid.Columns.Add("Sku", "SKU");
            goodsGrid.Columns.Add("Item", "ITEM");
            goodsGrid.Columns.Add("Category", "CATEGORY");
            goodsGrid.Columns.Add("Storage", "STORAGE");
            goodsGrid.Columns.Add("Quantity", "QUANTITY");
            goodsGrid.Columns.Add("Unit", "UNIT");
            goodsGrid.Columns.Add("Status", "STATUS");
            goodsGrid.Columns.Add("LastReceived", "LAST RECEIVED");
            goodsGrid.Columns["Sku"].FillWeight = 75F;
            goodsGrid.Columns["Item"].FillWeight = 130F;
            goodsGrid.Columns["Category"].FillWeight = 95F;
            goodsGrid.Columns["Storage"].FillWeight = 85F;
            goodsGrid.Columns["Quantity"].FillWeight = 70F;
            goodsGrid.Columns["Unit"].FillWeight = 55F;
            goodsGrid.Columns["Status"].FillWeight = 80F;
            goodsGrid.Columns["LastReceived"].FillWeight = 95F;
            goodsGrid.CellFormatting += GoodsGrid_CellFormatting;
            return goodsGrid;
        }

        private void PopulateGrid(IEnumerable<StoredGood> items)
        {
            goodsGrid.Rows.Clear();
            foreach (var item in items)
            {
                goodsGrid.Rows.Add(
                    item.Sku,
                    item.Name,
                    item.Category,
                    item.Storage,
                    item.Quantity,
                    item.Unit,
                    GetStatus(item.Quantity),
                    item.LastReceived.ToString("MMM dd, yyyy"));
            }
        }

        private void ApplyFilters()
        {
            var query = searchBox.Text.Trim();
            var selectedStatus = statusFilter.SelectedItem as string;
            var filtered = goods.Where(item =>
                (string.IsNullOrEmpty(query) ||
                 item.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                 item.Sku.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                 item.Category.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0 ||
                 item.Storage.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0) &&
                (selectedStatus == null || selectedStatus == "All statuses" ||
                 GetStatus(item.Quantity).Equals(selectedStatus, StringComparison.OrdinalIgnoreCase)));

            PopulateGrid(filtered);
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void StatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void UpdateSummary()
        {
            skuCountValue.Text = goods.Count.ToString();
            unitsValue.Text = goods.Sum(item => item.Quantity).ToString("N0");
            lowStockValue.Text = goods.Count(item => item.Quantity > 0 && item.Quantity <= 10).ToString();
        }

        private void GoodsGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (goodsGrid.Columns[e.ColumnIndex].Name != "Status" || e.Value == null)
            {
                return;
            }

            var status = e.Value.ToString();
            if (status == "In stock")
            {
                e.CellStyle.ForeColor = Color.FromArgb(21, 128, 61);
                e.CellStyle.BackColor = Color.FromArgb(240, 253, 244);
            }
            else if (status == "Low stock")
            {
                e.CellStyle.ForeColor = Color.FromArgb(180, 83, 9);
                e.CellStyle.BackColor = Color.FromArgb(255, 247, 237);
            }
            else
            {
                e.CellStyle.ForeColor = Color.FromArgb(185, 28, 28);
                e.CellStyle.BackColor = Color.FromArgb(254, 242, 242);
            }
        }

        private string GetStatus(int quantity)
        {
            if (quantity == 0)
            {
                return "Out of stock";
            }

            return quantity <= 10 ? "Low stock" : "In stock";
        }

        private void NavigateToDashboard(object sender, EventArgs e)
        {
            OpenForm(new Dashboardp());
        }

        private void NavigateToStorages(object sender, EventArgs e)
        {
            OpenForm(new storages());
        }

        private void NavigateToReceiving(object sender, EventArgs e)
        {
            OpenForm(new receivegood());
        }

        private void OpenForm(Form form)
        {
            form.WindowState = FormWindowState.Maximized;
            form.Show();
            Hide();
        }

        private static List<StoredGood> CreateSampleGoods()
        {
            return new List<StoredGood>
            {
                new StoredGood("SKU-1001", "Packaging boxes", "Packaging", "Storage A", 240, "pcs", new DateTime(2026, 11, 24)),
                new StoredGood("SKU-1002", "Protective gloves", "Safety", "Storage B", 86, "pairs", new DateTime(2026, 11, 23)),
                new StoredGood("SKU-1003", "Stretch wrap", "Packaging", "Storage A", 8, "rolls", new DateTime(2026, 11, 20)),
                new StoredGood("SKU-1004", "Pallet labels", "Supplies", "Storage C", 0, "packs", new DateTime(2026, 11, 18)),
                new StoredGood("SKU-1005", "Shipping tape", "Packaging", "Storage B", 132, "rolls", new DateTime(2026, 11, 22)),
                new StoredGood("SKU-1006", "Safety vests", "Safety", "Storage C", 6, "pcs", new DateTime(2026, 11, 19))
            };
        }

        private sealed class StoredGood
        {
            public StoredGood(string sku, string name, string category, string storage, int quantity, string unit, DateTime lastReceived)
            {
                Sku = sku;
                Name = name;
                Category = category;
                Storage = storage;
                Quantity = quantity;
                Unit = unit;
                LastReceived = lastReceived;
            }

            public string Sku { get; private set; }
            public string Name { get; private set; }
            public string Category { get; private set; }
            public string Storage { get; private set; }
            public int Quantity { get; private set; }
            public string Unit { get; private set; }
            public DateTime LastReceived { get; private set; }
        }
        private void resivegood_Click(object sender, EventArgs e)
        {
            receivegood f2 = new receivegood();
            f2.Show();
            f2.WindowState = FormWindowState.Maximized;
            this.Hide();
        }
        private void Stored_Goods_Click(object sender, EventArgs e)
        {
            StoredGoods storedGoods = new StoredGoods();
            storedGoods.Show();
            this.Hide();
        }

     

        private void Dashboard_Click(object sender, EventArgs e)
        {
            Dashboardp d1 = new Dashboardp();
            d1.Show();
            d1.WindowState = FormWindowState.Maximized;
            this.Hide();
        }

        private void Storages_Click_1(object sender, EventArgs e)
        {
            storages s1 = new storages();
            s1.Show();
            s1.WindowState = FormWindowState.Maximized;
            this.Hide();
        }

        private void Receive_Goods_Click_1(object sender, EventArgs e)
        {
            receivegood r1 = new receivegood();
            r1.Show();
            r1.WindowState = FormWindowState.Maximized;
            this.Hide();
        }

        private void goodsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
