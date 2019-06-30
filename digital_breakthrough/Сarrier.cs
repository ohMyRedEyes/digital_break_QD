using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//SQLite
using System.Data.SQLite;
using System.Data.Common;

namespace digital_breakthrough
{
    public partial class Сarrier : Form
    {
        public Сarrier()
        {
            InitializeComponent();
        }
        #region Стандарт
        Component_src com = new Component_src(); //путь к БД
        public string ID_users = ""; // номер пользователя
        #endregion

        #region Переменные для передвижения формы
        int a, a1;
        string a2;
        #endregion

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (a2 == "Зажата")
            {
                Left = Cursor.Position.X - a;
                Top = Cursor.Position.Y - a1;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            a2 = "Мышка не зажата";
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            a2 = "Зажата";
            a = Cursor.Position.X - Left;
            a1 = Cursor.Position.Y - Top;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void аукцонЗаказовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;
            dataGridView1.Rows.Clear();
            SQLiteConnection connection1 = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
            connection1.Open();
            SQLiteCommand command1 = new SQLiteCommand("Select id_freelance, Logistics_centre.address_Logistics_centre, freelance.address_tovar, freelance.weight_tovar, price_tovar, freelance.additionally, freelance.date_start, freelance.date_fin From freelance, carrier, Logistics_centre Where  ot <= freelance.weight_tovar and do>= freelance.weight_tovar and carrier.ID_carrier = '"+ID_users+"' and Logistics_centre.ID_Logistics_centre = freelance.id_Logistics_centre and freelance.id_freelance is not (Select id_freelance FROM history Where id_freelance) Group By freelance.id_freelance", connection1);
            SQLiteDataReader reader1 = command1.ExecuteReader();
            foreach (DbDataRecord record1 in reader1)
            {
                dataGridView1.Rows.Add(record1["id_freelance"].ToString(), record1["address_Logistics_centre"].ToString(), record1["address_tovar"].ToString(), record1["weight_tovar"].ToString(), record1["price_tovar"].ToString(), record1["additionally"].ToString(), record1["date_start"].ToString(), record1["date_fin"].ToString());
            }
            connection1.Close();

        }

        private void моиЗаказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = true;
            panel4.Visible = false;
            Visiblepanel();
            //отображаем все заказы, которые в работе у данного поставщика
            dataGridView2.Rows.Clear();
            SQLiteConnection connection1 = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
            connection1.Open();
            SQLiteCommand command1 = new SQLiteCommand("SELECT freelance.id_freelance, Logistics_centre.address_Logistics_centre, freelance.address_tovar, freelance.weight_tovar, freelance.price_tovar, freelance.additionally, freelance.date_start, freelance.date_fin  FROM orders, freelance, history, Logistics_centre where orders.id_freelance = history.id_freelance and history.id_freelance = freelance.id_freelance and Logistics_centre.ID_Logistics_centre = freelance.id_Logistics_centre and orders.id_carrier = '"+ID_users+"' and history.statys = 'Производится доставка'", connection1);
            SQLiteDataReader reader1 = command1.ExecuteReader();
            foreach (DbDataRecord record1 in reader1)
            {
                dataGridView2.Rows.Add(record1["id_freelance"].ToString(), record1["address_Logistics_centre"].ToString(), record1["address_tovar"].ToString(), record1["weight_tovar"].ToString(), record1["price_tovar"].ToString(), record1["additionally"].ToString(), record1["date_start"].ToString(), record1["date_fin"].ToString());
            }
            connection1.Close();

        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;
            Visiblepanel();
        }

        private void Visiblepanel()
        {
            textBox14.Visible = false;
            textBox1.Visible = false;
            label17.Visible = false;
            label1.Visible = false;
            button2.Visible = false;
            button4.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox10.Text != "" && textBox8.Text != "" && textBox9.Text != "" && textBox13.Text != "" && textBox12.Text != "" && textBox11.Text != "")
            {
                if (textBox12.Text == textBox11.Text)
                {
                    SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("UPDATE carrier SET name_carrier='" + textBox15.Text + "',  ot='" + textBox8.Text +"', do='" + textBox9.Text + "', address='" + textBox2.Text + " " + textBox10.Text+"', login_carrier='"+textBox13.Text+ "', password_carrier='"+textBox12.Text+"' WHERE id_carrier='" + ID_users + "';", connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    connection.Close();

                    MessageBox.Show("Данные были изменены");
                }else
                {
                    MessageBox.Show("Пароли не совпадают");

                }
            }
            else MessageBox.Show("Введите все необходимые данные");
        }

        private void Сarrier_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //отправляем заявление
            if(textBox1.Text!="" && textBox14.Text!="")
            {
                SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("INSERT INTO orders (id_freelance, id_carrier, price_orders, additionally) VALUES('" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString() + "', '" + ID_users+ "', '"+textBox14.Text+"', '"+textBox1.Text+"')", connection);
                SQLiteDataReader reader = command.ExecuteReader();
                connection.Close();
                MessageBox.Show("Запрос отправлен");
                textBox14.Text = "";
                textBox1.Text = "";
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

            //проверяем данные

            textBox14.Visible = true;
            textBox1.Visible = true;
            label17.Visible = true;
            label1.Visible = true;
            button2.Visible = true;
            button4.Visible = true;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //выполнение заказа
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("UPDATE history SET statys='Выполнено' WHERE id_freelance='" + dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[0].Value.ToString() + "';", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            connection.Close();
            dataGridView2.Rows.RemoveAt(dataGridView2.CurrentRow.Index);
            MessageBox.Show("Вы подтвердили выполнение заказа"); 
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Chat chat = new Chat();
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("Select  id_Logistics_centre From freelance Where id_freelance='" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString() + "' ", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                chat.logist = record["id_Logistics_centre"].ToString();
            }
            connection.Close();

            chat.ID_carrier = ID_users;
            chat.ID_freelance = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            chat.actiV = 1;
            chat.Show();
        }

        private void Label11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
    }
}
