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
    public partial class authorizations : Form
    {
        public authorizations()
        {
            InitializeComponent();
        }


        Component_src com = new Component_src();

        #region Переменные для передвижения формы
        int a, a1;
        string a2;
        #endregion

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
                textBox2.UseSystemPasswordChar = false;
            else
                textBox2.UseSystemPasswordChar = true;
        }

        private void Label1_Click_1(object sender, EventArgs e)
        {
            registration reg = new registration();
            reg.Show();
            Hide();
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ID_users=-1;
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("select ID_Logistics_centre From Logistics_centre Where login_Logistics_centre = '" + textBox1.Text+"' and Password_Logistics_centre='"+textBox2.Text+"'", connection);
                SQLiteDataReader reader = command.ExecuteReader();
                foreach (DbDataRecord record in reader)
                {
                    ID_users = Convert.ToInt32(record["ID_Logistics_centre"]);
                }
                connection.Close();
                if (ID_users != -1)
                {
                    logical_center log = new logical_center();
                    log.ID_users = ID_users.ToString();
                    log.Show();
                    Hide();
                }
                else
                {
                    SQLiteConnection connection1 = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
                    connection1.Open();
                    SQLiteCommand command1 = new SQLiteCommand("select ID_carrier From carrier Where login_carrier = '"+textBox1.Text+"' and Password_carrier='"+textBox2.Text+"'", connection1);
                    SQLiteDataReader reader1 = command1.ExecuteReader();
                    foreach (DbDataRecord record1 in reader1)
                    {
                        ID_users = Convert.ToInt32(record1["ID_carrier"]);
                    }
                    connection1.Close();
                    if(ID_users==0)
                    {
                        MessageBox.Show("Неверный логин или пароль");
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }else
                    {
                        Сarrier car = new Сarrier();
                        car.ID_users = ID_users.ToString();
                        car.Show();
                        Hide();
                    }
                }
            }
        }
    }
}
