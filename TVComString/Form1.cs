using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace TVComString
{
    public partial class Form1 : Form
    {
        public string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\TVCOMString\TVCOMNEWSTRING.MDF;Integrated Security=True;";
        public Form1()
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                InitializeComponent();
                TestConnection();
                LoadComboBoxData();
                LoadTable();
                filterRB1.Checked = true;
                dateRB2.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.WriteAllText(@"C:\TVCOMString\error.log",
                                 $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n");
            }
        }
        private void TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    MessageBox.Show("Подключение к БД успешно!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к БД: {ex.Message}");
            }
        }

        private void LoadComboBoxData()
        {
            if (Properties.Settings.Default.Save == null)
                Properties.Settings.Default.Save = new StringCollection();

            orderCB.Items.AddRange(Properties.Settings.Default.Save.Cast<string>().ToArray());
        }
        private void LoadTable()
        {
            string query = @"SELECT [Текст_объявления], [заказчик], [дата_подачи], [дата_закрытия], [цвет], [телефон] FROM [Объявления] WHERE CAST(GETDATE() AS DATE) >= [дата_подачи] AND CAST(GETDATE() AS DATE) <= [дата_закрытия]";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                // Очищаем DataGridView перед загрузкой новых данных
                dataGridView1.Rows.Clear();

                // Загружаем данные построчно
                foreach (DataRow row in table.Rows)
                {
                    dataGridView1.Rows.Add(
                        row["Текст_объявления"].ToString(),
                        row["заказчик"].ToString(),
                        Convert.ToDateTime(row["дата_подачи"]).ToShortDateString(),
                        Convert.ToDateTime(row["дата_закрытия"]).ToShortDateString(),
                        row["цвет"].ToString(),
                        row["телефон"].ToString()
                    );
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newOrder = orderCB.Text.Trim();
            if (!string.IsNullOrEmpty(newOrder) && !orderCB.Items.Contains(newOrder))
            {
                orderCB.Items.Add(newOrder);
                Properties.Settings.Default.Save.Add(newOrder);
                Properties.Settings.Default.Save();
            }


            string query = @"INSERT INTO [Объявления] 
    ([Текст_объявления], [заказчик], [дата_подачи], [дата_закрытия], [цвет], [телефон]) 
    VALUES (@text, @zakaz, @dateOpen, @dateClose, @color, @phone)";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@text", obyavlenieTB.Text);
                    cmd.Parameters.AddWithValue("@zakaz", orderCB.Text);
                    cmd.Parameters.AddWithValue("@dateOpen", dateOpen.Value.Date);
                    cmd.Parameters.AddWithValue("@dateClose", dateClose.Value.Date);
                    if (colorTB.Text != "")
                    {
                        cmd.Parameters.AddWithValue("@color", colorTB.Text);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@color", "100,143,143,143");
                    }
                    cmd.Parameters.AddWithValue("@phone", string.IsNullOrEmpty(phoneTB.Text) ? (object)DBNull.Value : phoneTB.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Успех", "объявление добавлено");
                    LoadTable();
                }
            }
            obyavlenieTB.Text = "";
            colorTB.Text = "";
            orderCB.Text = "";
            obyavlenieTB.Text = "";
        }

        private void colorBtn_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Color selectedColor = colorDialog.Color;
                    string colorFormat = $"100,{selectedColor.R},{selectedColor.G},{selectedColor.B}";
                    colorTB.Text = colorFormat;
                }
            }
        }

        private void dateRB1_CheckedChanged(object sender, EventArgs e)
        {
            dateFilter2.Visible = true;
        }
        private void dateRB2_CheckedChanged(object sender, EventArgs e)
        {
            dateFilter2.Visible = false;
        }
        private void dateRB3_CheckedChanged(object sender, EventArgs e)
        {
            dateFilter2.Visible = false;
        }
        private void dateRB4_CheckedChanged(object sender, EventArgs e)
        {
            dateFilter2.Visible = true;
        }

        private void hideButton_Click(object sender, EventArgs e)
        {
            if (groupBox1.Visible == false)
            {
                groupBox1.Visible = true;
                hideButton.Location = new Point(10, 239);
                groupBox2.Location = new Point(10, 264);
                groupBox3.Location = new Point(190, 264);
                searchButton.Location = new Point(10, 356);
                exportFileButton.Location = new Point(203, 356);
                exportExcelButton.Location = new Point(403, 356);
                dataGridView1.Location = new Point(10, 382);
                dataGridView1.Height = 223;
            }
            else
            {
                groupBox1.Visible = false;
                hideButton.Location = new Point(10, 10);
                groupBox2.Location = new Point(10, 40);
                groupBox3.Location = new Point(190, 40);
                searchButton.Location = new Point(10, 132);
                exportFileButton.Location = new Point(203, 132);
                exportExcelButton.Location = new Point(403, 132);
                dataGridView1.Location = new Point(10, 158);
                dataGridView1.Height = 447;
            }
        }

        private void exportFileButton_Click(object sender, EventArgs e)
        {
            string datetext = Convert.ToString(DateTime.Now);
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folderPath = Path.Combine(desktopPath, "Бегунки");

            // Создаем папку, если она не существует
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string dateText = datetext.Replace(" ", "_").Replace(":", "_").Replace(".", "_");
            string filePath = Path.Combine(folderPath, $"{dateText}.txt");

            using (StreamWriter begunok = new StreamWriter(filePath, true))
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string text = row.Cells["TextColumn"].Value?.ToString();
                        string phone = row.Cells["PhoneColumn"].Value?.ToString() ?? "";
                        string color = row.Cells["ColorColumn"].Value?.ToString() ?? "";

                        if (phone == "")
                        {
                            begunok.WriteLine($" {text}");
                        }
                        else
                        {
                            begunok.WriteLine($" {text}|{phone}<pb {color}>");
                        }
                    }
                }
                MessageBox.Show("Успех", $"Файл создан в папке: {folderPath}");
            }
        }


        private void exportExcelButton_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp = new Excel.Application();
            if (excelApp == null)
            {
                MessageBox.Show("Excel не установлен на этом компьютере.");
                return;
            }

            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

            // Заголовки
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
            }

            // Данные
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    if (!dataGridView1.Rows[i].IsNewRow)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value?.ToString();
                    }
                }
            }

            // Создаем папку "бегунки" на рабочем столе
            string datetext = Convert.ToString(DateTime.Now);
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string folderPath = Path.Combine(desktopPath, "Бегунки Excel");

            // Создаем папку, если она не существует
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string dateText = datetext.Replace(" ", "_").Replace(":", "_").Replace(".", "_");
            string filePath = Path.Combine(folderPath, $"{dateText}.xlsx");

            workbook.SaveAs(filePath);
            workbook.Close();
            excelApp.Quit();

            MessageBox.Show("Экспорт", $"Данные успешно экспортированы в Excel!\nФайл сохранен в: {folderPath}", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string query = "SELECT [Текст_объявления], [заказчик], [дата_подачи], [дата_закрытия], [цвет], [телефон] FROM Объявления";
            if (filterTB.Text == "" && dateRB2.Checked == true)
            {
                query = $"SELECT [Текст_объявления], [заказчик], [дата_подачи], [дата_закрытия], [цвет], [телефон] FROM [Объявления] WHERE '{dateFilter.Value.Year}-{dateFilter.Value.Month}-{dateFilter.Value.Day}' >= [дата_подачи] AND '{dateFilter.Value.Year}-{dateFilter.Value.Month}-{dateFilter.Value.Day}' <= [дата_закрытия]";
            }
            else
            {
                if (filterRB1.Checked == true)
                {
                    query += $" WHERE Текст_объявления LIKE N'%{filterTB.Text}%'";
                }
                else if (filterRB2.Checked == true)
                {
                    query += $" WHERE заказчик LIKE N'%{filterTB.Text}%'";
                }
                if (dateRB1.Checked == true)
                {
                    query += $"AND [дата_подачи] BETWEEN '{dateFilter.Value.Year}-{dateFilter.Value.Month}-{dateFilter.Value.Day}' AND '{dateFilter2.Value.Year}-{dateFilter2.Value.Month}-{dateFilter2.Value.Day}'";
                }
                if (dateRB3.Checked == true)
                {
                    query += $"AND [дата_закрытия] = '{dateFilter.Value.Year}-{dateFilter.Value.Month}-{dateFilter.Value.Day}'";
                }
                if (dateRB4.Checked == true)
                {
                    query += $"AND [дата_закрытия] BETWEEN '{dateFilter.Value.Year}-{dateFilter.Value.Month}-{dateFilter.Value.Day}' AND '{dateFilter2.Value.Year}-{dateFilter2.Value.Month}-{dateFilter2.Value.Day}'";
                }
            }
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                // Очищаем DataGridView перед загрузкой новых данных
                dataGridView1.Rows.Clear();

                // Загружаем данные построчно
                foreach (DataRow row in table.Rows)
                {
                    dataGridView1.Rows.Add(
                        row["Текст_объявления"].ToString(),
                        row["заказчик"].ToString(),
                        Convert.ToDateTime(row["дата_подачи"]).ToShortDateString(),
                        Convert.ToDateTime(row["дата_закрытия"]).ToShortDateString(),
                        row["цвет"].ToString(),
                        row["телефон"].ToString()
                    );
                }
            }
        }
    }
}