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
        private bool isUpdating = false; // Флаг для предотвращения рекурсии

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

                // Подключаем обработчики событий для автосохранения
                SetupAutoSaveEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}",
                               "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.WriteAllText(@"C:\TVCOMString\error.log",
                                 $"{DateTime.Now}: {ex.Message}\n{ex.StackTrace}\n");
            }
        }

        private void SetupAutoSaveEvents()
        {
            // Событие изменения значения ячейки
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;

            // Событие завершения редактирования ячейки
            dataGridView1.CellEndEdit += DataGridView1_CellEndEdit;
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Игнорируем если это системное обновление или некорректный индекс
            if (isUpdating || e.RowIndex < 0 || e.ColumnIndex < 0) return;

            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

            // Проверяем, что это не новая строка
            if (row.IsNewRow) return;

            AutoSaveRowChanges(row, e.RowIndex);
        }

        private void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Дополнительная проверка при завершении редактирования
            DataGridView1_CellValueChanged(sender, e);
        }

       

        private void AutoSaveRowChanges(DataGridViewRow row, int rowIndex)
        {
            try
            {
                isUpdating = true;

                // Получаем код объявления для идентификации записи
                string codeValue = row.Cells["Код_объявления"].Value?.ToString();

                if (string.IsNullOrEmpty(codeValue))
                {
                    MessageBox.Show("Не удалось определить код объявления для сохранения.", "Ошибка");
                    return;
                }

                // Собираем данные из строки (используйте правильные имена столбцов)
                string textValue = row.Cells["Текст_объявления"].Value?.ToString() ?? "";
                string customerValue = row.Cells["Заказчик"].Value?.ToString() ?? "";
                string phoneValue = row.Cells["Телефон"].Value?.ToString() ?? "";
                string colorValue = row.Cells["Цвет"].Value?.ToString() ?? "";

                // Обработка дат
                DateTime dateOpen, dateClose;
                if (!DateTime.TryParse(row.Cells["Дата_подачи"].Value?.ToString(), out dateOpen))
                {
                    MessageBox.Show("Некорректная дата подачи объявления.", "Ошибка");
                    return;
                }

                if (!DateTime.TryParse(row.Cells["Дата_закрытия"].Value?.ToString(), out dateClose))
                {
                    MessageBox.Show("Некорректная дата закрытия объявления.", "Ошибка");
                    return;
                }

                // Обновляем запись в базе данных
                string updateQuery = @"UPDATE [Объявления] 
                                     SET [Текст_объявления] = @text, 
                                         [заказчик] = @customer, 
                                         [дата_подачи] = @dateOpen, 
                                         [дата_закрытия] = @dateClose, 
                                         [цвет] = @color, 
                                         [телефон] = @phone 
                                     WHERE [Код_объявления] = @code";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@text", textValue);
                        cmd.Parameters.AddWithValue("@customer", customerValue);
                        cmd.Parameters.AddWithValue("@dateOpen", dateOpen.Date);
                        cmd.Parameters.AddWithValue("@dateClose", dateClose.Date);
                        cmd.Parameters.AddWithValue("@color", string.IsNullOrEmpty(colorValue) ? "100,143,143,143" : colorValue);
                        cmd.Parameters.AddWithValue("@phone", string.IsNullOrEmpty(phoneValue) ? (object)DBNull.Value : phoneValue);
                        cmd.Parameters.AddWithValue("@code", codeValue);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // Можно показать небольшое уведомление об успешном сохранении
                            // MessageBox.Show("Изменения сохранены", "Автосохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Не удалось сохранить изменения.", "Ошибка");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка автосохранения: {ex.Message}", "Ошибка");

                // Логируем ошибку
                try
                {
                    File.AppendAllText(@"C:\TVCOMString\error.log",
                                      $"{DateTime.Now}: Ошибка автосохранения - {ex.Message}\n{ex.StackTrace}\n");
                }
                catch { }
            }
            finally
            {
                isUpdating = false;
            }
        }

        private void TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    MessageBox.Show("Подключение к БД успешно! Версия 1.2.5");
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
            string query = @"SELECT [Текст_объявления], [заказчик], [дата_подачи], [дата_закрытия], [цвет], [телефон], [Код_объявления] FROM [Объявления] WHERE CAST(GETDATE() AS DATE) >= [дата_подачи] AND CAST(GETDATE() AS DATE) <= [дата_закрытия]";
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
                        row["телефон"].ToString(),
                        row["Код_объявления"].ToString()
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
            phoneTB.Text = "";
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
                deleteButton.Location = new Point(885, 356);
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
                deleteButton.Location = new Point(885, 132);
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
                        string text = row.Cells["Текст_объявления"].Value?.ToString();
                        string phone = row.Cells["Телефон"].Value?.ToString() ?? "";
                        string color = row.Cells["Цвет"].Value?.ToString() ?? "";

                        if (phone == "")
                        {
                            begunok.WriteLine($" {text}");
                        }
                        else
                        {
                            if (color == "")
                            {
                                color = "100,143,143,143";
                            }
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
            string query = "SELECT [Текст_объявления], [заказчик], [дата_подачи], [дата_закрытия], [цвет], [телефон], [Код_объявления] FROM Объявления";
            if (filterTB.Text == "" && dateRB2.Checked == true)
            {
                query = $"SELECT [Текст_объявления], [заказчик], [дата_подачи], [дата_закрытия], [цвет], [телефон], [Код_объявления] FROM [Объявления] WHERE '{dateFilter.Value.Year}-{dateFilter.Value.Month}-{dateFilter.Value.Day}' >= [дата_подачи] AND '{dateFilter.Value.Year}-{dateFilter.Value.Month}-{dateFilter.Value.Day}' <= [дата_закрытия]";
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
                        row["телефон"].ToString(),
                        row["Код_объявления"].ToString()
                    );
                }
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            // Проверяем, выделена ли строка
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выделите строку для удаления.", "Внимание",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем выделенную строку
            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

            // Проверяем, что это не новая строка
            if (selectedRow.IsNewRow)
            {
                MessageBox.Show("Нельзя удалить пустую строку.", "Внимание",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Получаем код объявления для удаления из столбца "Код_объявления"
            string codeValue = selectedRow.Cells["Код_объявления"].Value?.ToString();

            if (string.IsNullOrEmpty(codeValue))
            {
                MessageBox.Show("Не удалось определить код объявления для удаления.", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Запрашиваем подтверждение удаления
            string advertisementText = selectedRow.Cells["TextColumn"].Value?.ToString() ?? "Неизвестное объявление";
            DialogResult result = MessageBox.Show(
                $"Вы уверены, что хотите удалить объявление:\n\n\"{advertisementText}\"?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
                return;

            try
            {
                // Удаляем запись из базы данных
                string deleteQuery = "DELETE FROM [Объявления] WHERE [Код_объявления] = @code";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@code", codeValue);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Объявление успешно удалено.", "Успех",
                                           MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Обновляем таблицу
                            LoadTable();
                        }
                        else
                        {
                            MessageBox.Show("Объявление не найдено в базе данных.", "Внимание",
                                           MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении объявления: {ex.Message}", "Ошибка",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Логируем ошибку
                try
                {
                    File.AppendAllText(@"C:\TVCOMString\error.log",
                                      $"{DateTime.Now}: Ошибка удаления - {ex.Message}\n{ex.StackTrace}\n");
                }
                catch { }
            }
        }
    }
}