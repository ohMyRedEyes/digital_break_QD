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
    public partial class Chat : Form
    {
        public Chat()
        {
            InitializeComponent();
        }

        #region Переменные для передвижения формы
        int a,a1;
        string a2;
        #endregion


        Component_src com = new Component_src(); //путь к БД
        public string logist;
        public string carier;
        public string ID_carrier;
        public string ID_freelance;
        public int actiV;

        private void Chat_Load(object sender, EventArgs e)
        {
            label17.Text = "Номер заказа " + ID_freelance;
            if (actiV==0)
            {
                label1.Text = "Собеседник " + ID_carrier;
            }
            else
            {
                label1.Text = "Собеседник " + logist;
            }



            //открытие в правом нижнем углу экрана
            Rectangle r = Screen.PrimaryScreen.Bounds;
            Left = r.Width - Width;
            Top = r.Height - Height-25;
            timer1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            //Проверка на наличие ключевых слов
            string subString = "позвоните";
            int indexOfSubstring = -1;
            indexOfSubstring = textBox1.Text.IndexOf(subString);
            if (textBox1.Text.IndexOf(subString)!=-1)
            {
                MessageBox.Show("Отправлять личные данные запрещено сервисом");
                textBox1.Text = "";
            }else
            {
                //добавляем данные в БД
                SQLiteConnection connection1 = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
                connection1.Open();
                if (actiV == 0)
                {

                    SQLiteCommand command1 = new SQLiteCommand("INSERT INTO Chat (id_freelance, text_messange, " +
                        "id_Logistics_centre, id_carrier) VALUES('" + ID_freelance + "', '" + textBox1.Text + "', '" +
                        logist + "','0')", connection1);
                    SQLiteDataReader reader1 = command1.ExecuteReader();

                }
                else
                {
                    SQLiteCommand command1 = new SQLiteCommand("INSERT INTO Chat (id_freelance, text_messange, " +
                    "id_Logistics_centre, id_carrier) VALUES('" + ID_freelance + "', '" + textBox1.Text + "', '0','"+ID_carrier+"')", connection1);
                    SQLiteDataReader reader1 = command1.ExecuteReader();
                }

                connection1.Close();
                //таблицу
                dataGridView1.Rows.Add("", textBox1.Text);
            }

        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            a2 = "Зажата";
            a = Cursor.Position.X - Left;
            a1 = Cursor.Position.Y - Top;
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (a2 == "Зажата")
            {
                Left = Cursor.Position.X - a;
                Top = Cursor.Position.Y - a1;
            }
        }

        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            a2 = "Мышка не зажата";
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            //подгружаем данные 
            dataGridView1.Rows.Clear();
            SQLiteConnection connection1 = new SQLiteConnection(string.Format("Data Source={0};", com.file_db));
            connection1.Open();
            SQLiteCommand command1 = new SQLiteCommand("SELECT text_messange, id_Logistics_centre, id_carrier From Chat Where id_freelance='2'", connection1);
            SQLiteDataReader reader1 = command1.ExecuteReader();
            foreach (DbDataRecord record1 in reader1)
            {
                if (actiV == 0)
                {
                    if (record1["id_Logistics_centre"].ToString() == "0")
                    {
                        dataGridView1.Rows.Add(record1["text_messange"].ToString(), "");
                    }
                    else
                    {
                        dataGridView1.Rows.Add("", record1["text_messange"].ToString());
                    }
                }else
                {
                    if (record1["id_Logistics_centre"].ToString() != "0")
                    {
                        dataGridView1.Rows.Add(record1["text_messange"].ToString(), "");
                    }
                    else
                    {
                        dataGridView1.Rows.Add("", record1["text_messange"].ToString());
                    }
                }

            }
            connection1.Close();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
