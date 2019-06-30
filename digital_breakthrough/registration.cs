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
    public partial class registration : Form
    {
        public registration()
        {
            InitializeComponent();
        }

        Component_src com = new Component_src();

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

        private void registration_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox15.Text != "")
            {
                if (textBox3.Text == textBox4.Text)
                {
                    //регестрируем логистический центр
                    SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("INSERT INTO Logistics_centre (name_Logistics_centre, address_Logistics_centre, login_Logistics_centre, password_Logistics_centre) VALUES('" + textBox15.Text + "', '" + textBox1.Text + " " + textBox5.Text + " " + textBox6.Text + " " + textBox7.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "');", connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    connection.Close();
                    authorizations au = new authorizations();
                    au.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают");
                }
            }
            else MessageBox.Show("Заполните все поля");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true)
            {
                checkBox2.Checked = false;
                panel3.Visible = true;
                panel2.Visible = false; 
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
                panel2.Visible = true;
                panel3.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //регестрируем перевозчика
            //проверка пустых полей
            if (textBox14.Text != "" && textBox10.Text != "" && textBox9.Text != "" && textBox15.Text != "" && textBox13.Text != "" && textBox12.Text != "" && textBox11.Text != "")
            {
                //проверка паролей
                if (textBox11.Text == textBox12.Text)
                {
                    SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
                    connection.Open();
                    SQLiteCommand command = new SQLiteCommand("INSERT INTO carrier (name_carrier, ot, do, address, login_carrier, Password_carrier) VALUES('"+textBox15.Text+"', "+textBox8.Text+", "+textBox9.Text+", '"+textBox14.Text+ " " +textBox10.Text+"', '"+textBox13.Text+"', '"+textBox12.Text+"'); ", connection);
                    SQLiteDataReader reader = command.ExecuteReader();
                    connection.Close();
                    authorizations au = new authorizations();
                    au.Show();
                    Hide();
                } else
                {
                    MessageBox.Show("Пароли не совпадают");
                }
            }
            else MessageBox.Show("Заполните все поля");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
