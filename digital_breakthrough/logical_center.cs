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
    public partial class logical_center : Form
    {
        public logical_center()
        {
            InitializeComponent();
        }
        #region Стандарт
        Component_src com = new Component_src(); //путь к БД
        public string ID_users = ""; // номер пользователя
        #endregion

        #region Переменные для передвижения формы
        int a, 
a1;
        string a2;
        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox14.Text != "" && textBox10.Text != "" && textBox1.Text != "" && textBox2.Text != "" && textBox8.Text != "" && textBox9.Text != "")
            {

                SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("INSERT INTO freelance (id_Logistics_centre, price_tovar, weight_tovar, address_tovar, date_start, date_fin, additionally) VALUES('" + ID_users + "', '"+textBox9.Text+"', '"+textBox8.Text+"', '"+textBox14.Text+ " область, г."+  textBox10.Text+ ", улица "+textBox1.Text+", дом "+textBox2.Text+ "', '"+ DateTime.Now.ToLongDateString()+"', '"+dateTimePicker1.Value.ToLongDateString()+"', '"+textBox3.Text+"'); ; ", connection);
                SQLiteDataReader reader = command.ExecuteReader();
                connection.Close();

                SQLiteConnection connection1 = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
                connection1.Open();
                SQLiteCommand command1 = new SQLiteCommand("SELECT id_freelance FROM freelance ORDER BY id_freelance DESC LIMIT 1", connection1);
                SQLiteDataReader reader1 = command1.ExecuteReader();
                foreach (DbDataRecord record1 in reader1)
                {
                    MessageBox.Show("Код товара: "+ record1["id_freelance"].ToString());
                } 
                connection1.Close();
            }
        }

        private void новыйЗаказToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel3.Visible = false;
            panel4.Visible = false;
        }

        private void заказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;
            //выводим все данные по таблице
            dataGridView1.Rows.Clear();
            SQLiteConnection connection1 = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
            connection1.Open();
            SQLiteCommand command1 = new SQLiteCommand("Select  ID_freelance, id_Logistics_centre, price_tovar, weight_tovar, address_tovar, date_start, date_fin, additionally From freelance Where id_Logistics_centre='"+ID_users+"'", connection1);
            SQLiteDataReader reader1 = command1.ExecuteReader();
            foreach (DbDataRecord record1 in reader1)
            {
                dataGridView1.Rows.Add(record1["ID_freelance"].ToString(), record1["address_tovar"].ToString(), record1["weight_tovar"].ToString(), record1["price_tovar"].ToString(), record1["additionally"].ToString(), record1["date_start"].ToString(), record1["date_fin"].ToString());
            }
            connection1.Close();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = true;
            panel4.Visible = false;
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            //новый заказ
            if (textBox13.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox16.Text != "" && textBox12.Text != "" && textBox11.Text != "" && textBox4.Text != "")
            {
                if(textBox11.Text==textBox4.Text)
                {
                    SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("UPDATE Logistics_centre SET name_Logistics_centre='"+textBox16.Text+"',  address_Logistics_centre='"+textBox13.Text +" "+ textBox5.Text+" "+ textBox6.Text+" "+ textBox7.Text+"', login_Logistics_centre='"+textBox12.Text+"', password_Logistics_centre='"+textBox11.Text+"' WHERE id_Logistics_centre='"+ ID_users + "';", connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    connection.Close();
                    MessageBox.Show("Данные были изменены");
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают");
                    textBox11.Text = "";
                    textBox4.Text = "";
                }
            }
            else MessageBox.Show("Не все поля заполнены"); 

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            label6.Text = "";
            dataGridView2.Rows.Clear();
            //проверяем историю

            SQLiteConnection connection1 = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
            connection1.Open();
            SQLiteCommand command1 = new SQLiteCommand("Select  statys From history Where id_freelance='" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString() +"'", connection1);
            SQLiteDataReader reader1 = command1.ExecuteReader();
            foreach (DbDataRecord record1 in reader1)
            {
                label6.Text = "Статус заказа: " + record1["statys"].ToString();
            }
            connection1.Close();
            if(label6.Text=="")
            {
                dataGridView2.Visible = true;
                button3.Visible = true;
                button4.Visible = false;
                label6.Text = "Статус заказа: " + "Поиск кондидата";
                //загружаем всех кондидатов
                SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("Select  id_orders,  name_carrier, price_orders, additionally From orders, carrier Where id_freelance='" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString() + "' and orders.id_carrier = carrier.ID_carrier", connection);
                SQLiteDataReader reader = command.ExecuteReader();
                foreach (DbDataRecord record in reader)
                {
                    dataGridView2.Rows.Add(record["id_orders"].ToString(), record["name_carrier"].ToString(), record["price_orders"].ToString(), record["additionally"].ToString());// record["statys"].ToString();
                }
                connection.Close();
            }else
            {
                dataGridView2.Visible = false;
                button3.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Chat chat = new Chat();
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("Select  id_carrier From carrier Where name_carrier='" + dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value.ToString() + "' ", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                chat.ID_carrier = record["id_carrier"].ToString();
            }

            connection.Close();


            chat.logist = ID_users;
            chat.carier = dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[1].Value.ToString();
            chat.ID_freelance = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();
            chat.actiV = 0;
            chat.Show();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            button4.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //добавляем в историю
            Component_src com = new Component_src();
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("INSERT INTO history (id_freelance, id_orders, statys) VALUES('"+dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString() + "', '"+dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells[0].Value.ToString() +"', 'Производится доставка')", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            connection.Close();
            dataGridView2.Rows.Clear();
            label6.Text = "Статус заказа: " + "Производится доставка";

            dataGridView2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;

        }
    }
}
