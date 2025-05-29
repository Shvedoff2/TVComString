using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TVComString
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.obyavlenieTB = new System.Windows.Forms.TextBox();
            this.dateClose = new System.Windows.Forms.DateTimePicker();
            this.dateOpen = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.phoneTB = new System.Windows.Forms.TextBox();
            this.orderCB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.filterTB = new System.Windows.Forms.TextBox();
            this.filterRB2 = new System.Windows.Forms.RadioButton();
            this.filterRB1 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dateFilter2 = new System.Windows.Forms.DateTimePicker();
            this.dateRB4 = new System.Windows.Forms.RadioButton();
            this.dateRB3 = new System.Windows.Forms.RadioButton();
            this.dateFilter = new System.Windows.Forms.DateTimePicker();
            this.dateRB2 = new System.Windows.Forms.RadioButton();
            this.dateRB1 = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.TextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZakazColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColorColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhoneColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.searchButton = new System.Windows.Forms.Button();
            this.exportFileButton = new System.Windows.Forms.Button();
            this.exportExcelButton = new System.Windows.Forms.Button();
            this.hideButton = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.colorTB = new System.Windows.Forms.TextBox();
            this.colorBtn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.colorBtn);
            this.groupBox1.Controls.Add(this.colorTB);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.addButton);
            this.groupBox1.Controls.Add(this.obyavlenieTB);
            this.groupBox1.Controls.Add(this.dateClose);
            this.groupBox1.Controls.Add(this.dateOpen);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.phoneTB);
            this.groupBox1.Controls.Add(this.orderCB);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(519, 224);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Добавить объявление";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(342, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Цвет";
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(5, 198);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(509, 20);
            this.addButton.TabIndex = 12;
            this.addButton.Text = "Добавить объявление";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // obyavlenieTB
            // 
            this.obyavlenieTB.Location = new System.Drawing.Point(5, 112);
            this.obyavlenieTB.Multiline = true;
            this.obyavlenieTB.Name = "obyavlenieTB";
            this.obyavlenieTB.Size = new System.Drawing.Size(510, 82);
            this.obyavlenieTB.TabIndex = 11;
            // 
            // dateClose
            // 
            this.dateClose.Location = new System.Drawing.Point(180, 77);
            this.dateClose.Name = "dateClose";
            this.dateClose.Size = new System.Drawing.Size(135, 20);
            this.dateClose.TabIndex = 10;
            // 
            // dateOpen
            // 
            this.dateOpen.Location = new System.Drawing.Point(5, 77);
            this.dateOpen.Name = "dateOpen";
            this.dateOpen.Size = new System.Drawing.Size(135, 20);
            this.dateOpen.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(180, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Дата закрытия";
            // 
            // phoneTB
            // 
            this.phoneTB.Location = new System.Drawing.Point(180, 32);
            this.phoneTB.Name = "phoneTB";
            this.phoneTB.Size = new System.Drawing.Size(127, 20);
            this.phoneTB.TabIndex = 5;
            // 
            // orderCB
            // 
            this.orderCB.CausesValidation = false;
            this.orderCB.FormattingEnabled = true;
            this.orderCB.Items.AddRange(new object[] {
            "Налоговая",
            "Оптика Сэсэг",
            "Тивиком"});
            this.orderCB.Location = new System.Drawing.Point(5, 32);
            this.orderCB.Name = "orderCB";
            this.orderCB.Size = new System.Drawing.Size(156, 21);
            this.orderCB.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Дата подачи";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(180, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Телефон";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Заказчик";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.filterTB);
            this.groupBox2.Controls.Add(this.filterRB2);
            this.groupBox2.Controls.Add(this.filterRB1);
            this.groupBox2.Location = new System.Drawing.Point(10, 264);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(171, 87);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Поиск";
            // 
            // filterTB
            // 
            this.filterTB.Location = new System.Drawing.Point(5, 62);
            this.filterTB.Name = "filterTB";
            this.filterTB.Size = new System.Drawing.Size(162, 20);
            this.filterTB.TabIndex = 13;
            // 
            // filterRB2
            // 
            this.filterRB2.AutoSize = true;
            this.filterRB2.Location = new System.Drawing.Point(5, 41);
            this.filterRB2.Name = "filterRB2";
            this.filterRB2.Size = new System.Drawing.Size(94, 17);
            this.filterRB2.TabIndex = 1;
            this.filterRB2.TabStop = true;
            this.filterRB2.Text = "По заказчику";
            this.filterRB2.UseVisualStyleBackColor = true;
            // 
            // filterRB1
            // 
            this.filterRB1.AutoSize = true;
            this.filterRB1.Location = new System.Drawing.Point(5, 19);
            this.filterRB1.Name = "filterRB1";
            this.filterRB1.Size = new System.Drawing.Size(148, 17);
            this.filterRB1.TabIndex = 0;
            this.filterRB1.TabStop = true;
            this.filterRB1.Text = "По тексту в объявлении";
            this.filterRB1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dateFilter2);
            this.groupBox3.Controls.Add(this.dateRB4);
            this.groupBox3.Controls.Add(this.dateRB3);
            this.groupBox3.Controls.Add(this.dateFilter);
            this.groupBox3.Controls.Add(this.dateRB2);
            this.groupBox3.Controls.Add(this.dateRB1);
            this.groupBox3.Location = new System.Drawing.Point(190, 264);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(339, 87);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Поиск";
            // 
            // dateFilter2
            // 
            this.dateFilter2.Location = new System.Drawing.Point(166, 59);
            this.dateFilter2.Name = "dateFilter2";
            this.dateFilter2.Size = new System.Drawing.Size(135, 20);
            this.dateFilter2.TabIndex = 16;
            this.dateFilter2.Visible = false;
            // 
            // dateRB4
            // 
            this.dateRB4.AutoSize = true;
            this.dateRB4.Location = new System.Drawing.Point(165, 41);
            this.dateRB4.Name = "dateRB4";
            this.dateRB4.Size = new System.Drawing.Size(151, 17);
            this.dateRB4.TabIndex = 15;
            this.dateRB4.TabStop = true;
            this.dateRB4.Text = "Между датами закрытия";
            this.dateRB4.UseVisualStyleBackColor = true;
            this.dateRB4.CheckedChanged += new System.EventHandler(this.dateRB4_CheckedChanged);
            // 
            // dateRB3
            // 
            this.dateRB3.AutoSize = true;
            this.dateRB3.Location = new System.Drawing.Point(165, 19);
            this.dateRB3.Name = "dateRB3";
            this.dateRB3.Size = new System.Drawing.Size(103, 17);
            this.dateRB3.TabIndex = 14;
            this.dateRB3.TabStop = true;
            this.dateRB3.Text = "По дате подачи";
            this.dateRB3.UseVisualStyleBackColor = true;
            this.dateRB3.CheckedChanged += new System.EventHandler(this.dateRB3_CheckedChanged);
            // 
            // dateFilter
            // 
            this.dateFilter.Location = new System.Drawing.Point(5, 59);
            this.dateFilter.Name = "dateFilter";
            this.dateFilter.Size = new System.Drawing.Size(135, 20);
            this.dateFilter.TabIndex = 13;
            // 
            // dateRB2
            // 
            this.dateRB2.AutoSize = true;
            this.dateRB2.Location = new System.Drawing.Point(5, 41);
            this.dateRB2.Name = "dateRB2";
            this.dateRB2.Size = new System.Drawing.Size(86, 17);
            this.dateRB2.TabIndex = 1;
            this.dateRB2.TabStop = true;
            this.dateRB2.Text = "Актуальные";
            this.dateRB2.UseVisualStyleBackColor = true;
            this.dateRB2.CheckedChanged += new System.EventHandler(this.dateRB2_CheckedChanged);
            // 
            // dateRB1
            // 
            this.dateRB1.AutoSize = true;
            this.dateRB1.Location = new System.Drawing.Point(5, 19);
            this.dateRB1.Name = "dateRB1";
            this.dateRB1.Size = new System.Drawing.Size(137, 17);
            this.dateRB1.TabIndex = 0;
            this.dateRB1.TabStop = true;
            this.dateRB1.Text = "Между датами подачи";
            this.dateRB1.UseVisualStyleBackColor = true;
            this.dateRB1.CheckedChanged += new System.EventHandler(this.dateRB1_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TextColumn,
            this.ZakazColumn,
            this.DateColumn1,
            this.DateColumn2,
            this.ColorColumn,
            this.PhoneColumn});
            this.dataGridView1.Location = new System.Drawing.Point(10, 382);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(1064, 223);
            this.dataGridView1.TabIndex = 15;
            // 
            // TextColumn
            // 
            this.TextColumn.HeaderText = "Текст объявления";
            this.TextColumn.Name = "TextColumn";
            this.TextColumn.Width = 450;
            // 
            // ZakazColumn
            // 
            this.ZakazColumn.HeaderText = "Заказчик";
            this.ZakazColumn.Name = "ZakazColumn";
            this.ZakazColumn.Width = 150;
            // 
            // DateColumn1
            // 
            this.DateColumn1.HeaderText = "Дата подачи";
            this.DateColumn1.Name = "DateColumn1";
            // 
            // DateColumn2
            // 
            this.DateColumn2.HeaderText = "Дата закрытия";
            this.DateColumn2.Name = "DateColumn2";
            // 
            // ColorColumn
            // 
            this.ColorColumn.HeaderText = "Цвет";
            this.ColorColumn.Name = "ColorColumn";
            // 
            // PhoneColumn
            // 
            this.PhoneColumn.HeaderText = "Телефон";
            this.PhoneColumn.Name = "PhoneColumn";
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(10, 356);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(127, 20);
            this.searchButton.TabIndex = 16;
            this.searchButton.Text = "Найти";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // exportFileButton
            // 
            this.exportFileButton.Location = new System.Drawing.Point(203, 356);
            this.exportFileButton.Name = "exportFileButton";
            this.exportFileButton.Size = new System.Drawing.Size(127, 20);
            this.exportFileButton.TabIndex = 17;
            this.exportFileButton.Text = "Экспорт в файл";
            this.exportFileButton.UseVisualStyleBackColor = true;
            this.exportFileButton.Click += new System.EventHandler(this.exportFileButton_Click);
            // 
            // exportExcelButton
            // 
            this.exportExcelButton.Location = new System.Drawing.Point(403, 356);
            this.exportExcelButton.Name = "exportExcelButton";
            this.exportExcelButton.Size = new System.Drawing.Size(127, 20);
            this.exportExcelButton.TabIndex = 18;
            this.exportExcelButton.Text = "Экспорт в Excel";
            this.exportExcelButton.UseVisualStyleBackColor = true;
            this.exportExcelButton.Click += new System.EventHandler(this.exportExcelButton_Click);
            // 
            // hideButton
            // 
            this.hideButton.Location = new System.Drawing.Point(10, 239);
            this.hideButton.Name = "hideButton";
            this.hideButton.Size = new System.Drawing.Size(519, 20);
            this.hideButton.TabIndex = 13;
            this.hideButton.Text = "Скрыть";
            this.hideButton.UseVisualStyleBackColor = true;
            this.hideButton.Click += new System.EventHandler(this.hideButton_Click);
            // 
            // colorTB
            // 
            this.colorTB.Location = new System.Drawing.Point(345, 32);
            this.colorTB.Name = "colorTB";
            this.colorTB.Size = new System.Drawing.Size(100, 20);
            this.colorTB.TabIndex = 15;
            // 
            // colorBtn
            // 
            this.colorBtn.Location = new System.Drawing.Point(345, 57);
            this.colorBtn.Name = "colorBtn";
            this.colorBtn.Size = new System.Drawing.Size(100, 23);
            this.colorBtn.TabIndex = 16;
            this.colorBtn.Text = "Изменить цвет";
            this.colorBtn.UseVisualStyleBackColor = true;
            this.colorBtn.Click += new System.EventHandler(this.colorBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1086, 614);
            this.Controls.Add(this.hideButton);
            this.Controls.Add(this.exportExcelButton);
            this.Controls.Add(this.exportFileButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "TVComNewString";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private ComboBox orderCB;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox phoneTB;
        private Button addButton;
        private TextBox obyavlenieTB;
        private DateTimePicker dateClose;
        private DateTimePicker dateOpen;
        private Label label4;
        private GroupBox groupBox2;
        private TextBox filterTB;
        private RadioButton filterRB2;
        private RadioButton filterRB1;
        private GroupBox groupBox3;
        private RadioButton dateRB4;
        private RadioButton dateRB3;
        private DateTimePicker dateFilter;
        private RadioButton dateRB2;
        private RadioButton dateRB1;
        private DataGridView dataGridView1;
        private Button searchButton;
        private Button exportFileButton;
        private Button exportExcelButton;
        private DataGridViewTextBoxColumn TextColumn;
        private DataGridViewTextBoxColumn ZakazColumn;
        private DataGridViewTextBoxColumn DateColumn1;
        private DataGridViewTextBoxColumn DateColumn2;
        private DataGridViewTextBoxColumn ColorColumn;
        private DataGridViewTextBoxColumn PhoneColumn;
        private Button hideButton;
        private DateTimePicker dateFilter2;
        private ColorDialog colorDialog1;
        private Label label5;
        private TextBox colorTB;
        private Button colorBtn;
    }
}