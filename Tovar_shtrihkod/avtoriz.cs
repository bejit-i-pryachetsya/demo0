using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using Praktika1;

namespace Tovar_shtrihkod
{
    public partial class avtoriz : Form
    {

        public static class UserData
        {
            public static string Username { get; set; }
        }

        private System.Windows.Forms.Timer unlockTimer;
        public bool DB(string x, string c)
        {

            NpgsqlConnection conn = new NpgsqlConnection("User ID = postgres; Password = 12345; Host = localhost; Port = 5432;  Database = modern;");
            conn.Open();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            string sql = ($"SELECT * FROM usеr WHERE loginn = '{x}' and passwоrd = '{c}';");
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            ds.Reset();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

          



        }


        public avtoriz()
        {
            InitializeComponent();
            unlockTimer = new System.Windows.Forms.Timer();
            unlockTimer.Tick += unlockTimer_Tick;
        }

        private int invalidAttempts = 0;
        private DateTime lockoutEndTime;

        private void unlockTimer_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now >= lockoutEndTime)
            {
                // Тайм-аут окончен
               
                button1.Enabled = true;
                label1.Text = "Авторизация";
                unlockTimer.Stop();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == default ^ textBox1.Text == string.Empty)
            {

                MessageBox.Show("Введите логин и пароль!", "Ошибка!");
            }
            else if (textBox2.Text == default ^ textBox2.Text == string.Empty)
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка!");

            }
            else
            {


                bool x = DB(textBox1.Text, textBox2.Text);
                if (x == true)
                {
                   


                        NpgsqlConnection conn = new NpgsqlConnection("User ID = postgres; Password = 12345; Host = localhost; Port = 5432;  Database = modern;");
                        DataSet dataSet = new DataSet();
                        DataTable dataTable = new DataTable();
                        string command = $"Select ID From usеr Where loginn = '{textBox1.Text}' and passwоrd = '{textBox2.Text}' ;";
                        NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command, conn);
                        dataSet.Reset();
                        dataAdapter.Fill(dataTable);
                        int id_rights = Convert.ToInt32(dataTable.Rows[0][0]);


                    UserData.Username = textBox1.Text;
                    if (id_rights == 3)
                    {
                        this.Hide();
                        var form3 = new Form1();
                        form3.Closed += (s, args) => this.Close();
                        form3.Show();

                    }
                    else if (id_rights == 2)
                    {
                        this.Hide();
                        var form2 = new Form3();
                        form2.Closed += (s, args) => this.Close();
                        form2.Show();

                    }
                    else if (id_rights == 1)
                    {
                        this.Hide();
                        var form2 = new Form2();
                        form2.Closed += (s, args) => this.Close();
                        form2.Show();

                    }



                }





                else if (x == false)
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка!");
                    invalidAttempts++;
                    textBox1.Clear();
                    textBox2.Clear();
                    if (invalidAttempts >= 4)
                    {
                        label1.Text = "Введите символы";
                        label2.Text = "";
                        pictureBox1.Visible = true;
                        textBox3.Visible = true;
                        button3.Visible = true;
                        button4.Visible = true;
                        pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);


                    }
                    else if (invalidAttempts >= 3)
                    {
                        lockoutEndTime = DateTime.Now.AddSeconds(30);
                        button1.Enabled = false;

                        // Запуск таймера для разблокировки через 20 секунд
                        label1.Text = "Ошибка! Попробуйте позже.";

                        unlockTimer.Start();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form2 = new Form1();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }








        private string text = String.Empty;

        private Bitmap CreateImage(int Width, int Height)
        {
            Random rnd = new Random();

            //Создадим изображение
            Bitmap result = new Bitmap(Width, Height);

            //Вычислим позицию текста
            int Xpos = rnd.Next(0, Width - 50);
            int Ypos = rnd.Next(15, Height - 15);

            //Добавим различные цвета
            Brush[] colors = { Brushes.Black,
                     Brushes.Red,
                     Brushes.RoyalBlue,
                     Brushes.Green };

            //Укажем где рисовать
            Graphics g = Graphics.FromImage((Image)result);

            //Пусть фон картинки будет серым
            g.Clear(Color.Gray);

            //Сгенерируем текст
            text = String.Empty;
            string ALF = "1234567890QWERTYUIOPASDFGHJKLqwertyuiopasdfghjklzxcvbnmZXCVBNM";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];

            //Нарисуем сгенирируемый текст
            g.DrawString(text,
                         new Font("Arial", 15),
                         colors[rnd.Next(colors.Length)],
                         new PointF(Xpos, Ypos));

            //Добавим немного помех
            /////Линии из углов
            g.DrawLine(Pens.Black,
                       new Point(0, 0),
                       new Point(Width - 1, Height - 1));
            g.DrawLine(Pens.Black,
                       new Point(0, Height - 1),
                       new Point(Width - 1, 0));
            ////Белые точки
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);

            return result;
        }














        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = this.CreateImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == this.text)
            {
                label1.Text = "Авторизация";
                pictureBox1.Visible= false;
                textBox3.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                invalidAttempts = 2;
                label2.Text = "Логин";
                textBox3.Clear();
            }
                
            else
            {
                textBox3.Clear();
                MessageBox.Show("Неверно!");
            }
               
        }
    }
}
