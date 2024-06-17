using Npgsql;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using static Tovar_shtrihkod.avtoriz;

namespace Tovar_shtrihkod
{
    public partial class Form1 : Form
    {





        private Bitmap DrawBarcode(string code, int resolution = 20) // resolution - пикселей на миллиметр
        {
            int numberCount = 15; // количество цифр
            float height = 25.93f * resolution; // высота штрих кода
            float lineHeight = 22.85f * resolution; // высота штриха
            float leftOffset = 3.63f * resolution; // свободная зона слева
            float rightOffset = 2.31f * resolution; // свободная зона справа
                                                    //штрихи, которые образуют правый и левый ограничивающие знаки,
                                                    //а также центральный ограничивающий знак должны быть удлинены вниз на 1,65мм
            float longLineHeight = lineHeight + 1.65f * resolution;
            float fontHeight = 2.75f * resolution; // высота цифр
            float lineToFontOffset = 0.165f * resolution; // минимальный размер от верхнего края цифр до нижнего края штрихов
            float lineWidthDelta = 0.15f * resolution; // ширина 0.15*{цифра}
            float lineWidthFull = 1.35f * resolution; // ширина белой полоски при 0 или 0.15*9
            float lineOffset = 0.2f * resolution; // между штрихами должно быть расстояние в 0.2мм

            float width = leftOffset + rightOffset + 6 * (lineWidthDelta + lineOffset) + numberCount * (lineWidthFull + lineOffset); // ширина штрих-кода

            Bitmap bitmap = new Bitmap((int)width, (int)height); // создание картинки нужных размеров
            Graphics g = Graphics.FromImage(bitmap); // создание графики

            System.Drawing.Font font = new System.Drawing.Font("Arial", fontHeight, FontStyle.Regular, GraphicsUnit.Pixel); // создание шрифта

            StringFormat fontFormat = new StringFormat(); // Центрирование текста
            fontFormat.Alignment = StringAlignment.Center;
            fontFormat.LineAlignment = StringAlignment.Center;

            float x = leftOffset; // позиция рисования по x
            for (int i = 0; i < numberCount; i++)
            {
                int number = Convert.ToInt32(code[i].ToString()); // число из кода
                if (number != 0)
                {
                    g.FillRectangle(Brushes.Black, x, 0, number * lineWidthDelta, lineHeight); // рисуем штрих
                }
                RectangleF fontRect = new RectangleF(x, lineHeight + lineToFontOffset, lineWidthFull, fontHeight); // рамки для буквы
                g.DrawString(code[i].ToString(), font, Brushes.Black, fontRect, fontFormat); // рисуем букву
                x += lineWidthFull + lineOffset; // смещаем позицию рисования по x

                if (i == 0 || i == numberCount / 2 || i == numberCount - 1) // если это начало, середина или конец кода рисуем разделители


                {
                    for (int j = 0; j < 2; j++) // рисуем 2 линии разделителя
                    {
                        g.FillRectangle(Brushes.Black, x, 0, lineWidthDelta, longLineHeight); // рисуем длинный штрих
                        x += lineWidthDelta + lineOffset; // смещаем позицию рисования по x
                    }
                }

  

            }
            return bitmap;
        }














        public DataTable GetProd()
        {
            NpgsqlConnection conn = new NpgsqlConnection("User ID = postgres; password = 12345; Host = localhost; Port = 5432; database = modern;");

            conn.Open();

            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            string cmd = "Select * From tovar;";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(cmd, conn);
            ds.Reset();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }




        public DataTable GetMouse()
        {
            NpgsqlConnection conn = new NpgsqlConnection("User ID = postgres; password = 12345; Host = localhost; Port = 5432; database = modern;");

            conn.Open();

            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            string cmd = "Select * From tovar where type_id = 1;";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(cmd, conn);
            ds.Reset();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable GetKlav()
        {
            NpgsqlConnection conn = new NpgsqlConnection("User ID = postgres; password = 12345; Host = localhost; Port = 5432; database = modern;");

            conn.Open();

            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            string cmd = "Select * From tovar where type_id = 2;";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(cmd, conn);
            ds.Reset();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }


        















        public Form1()
        {
            InitializeComponent();
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            label3.Text = str.ToString();
            button1.Enabled = false;

          
            pictureBox1.Image = Properties.Resources.stroymat4;
            pictureBox2.Image = Properties.Resources.stroymat4;
            pictureBox3.Image = Properties.Resources.stroymat4;
            pictureBox4.Image = Properties.Resources.stroymat4;
            pictureBox5.Image = Properties.Resources.stroymat4;

            dataGridView1.DataSource = GetProd();
          
            label6.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
            label5.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
            label7.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();

          
            label12.Text = dataGridView1.Rows[1].Cells[3].Value.ToString();
            label13.Text = dataGridView1.Rows[1].Cells[1].Value.ToString();
            label11.Text = dataGridView1.Rows[1].Cells[2].Value.ToString();

         
            label15.Text = dataGridView1.Rows[2].Cells[3].Value.ToString();
            label16.Text = dataGridView1.Rows[2].Cells[1].Value.ToString();
            label14.Text = dataGridView1.Rows[2].Cells[2].Value.ToString();

         
            label18.Text = dataGridView1.Rows[3].Cells[3].Value.ToString();
            label19.Text = dataGridView1.Rows[3].Cells[1].Value.ToString();
            label17.Text = dataGridView1.Rows[3].Cells[2].Value.ToString();


           
            label21.Text = dataGridView1.Rows[4].Cells[3].Value.ToString();
            label22.Text = dataGridView1.Rows[4].Cells[1].Value.ToString();
            label20.Text = dataGridView1.Rows[4].Cells[2].Value.ToString();

        }

        public int itemstr1 = 0;
        public int itemstr2 = 1;
        public int itemstr3 = 2;
        public int itemstr4 = 3;
        public int itemstr5 = 4;
        public int str = 1;







        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;


            itemstr1 += 5;
            itemstr2 += 5;
            itemstr3 += 5;
            itemstr4 += 5;
            itemstr5 += 5;
            if (itemstr1 >= 0 && itemstr1 < dataGridView1.Rows.Count && dataGridView1.Rows[itemstr1].Cells[1].Value != null)
            {
                pictureBox1.Image = Properties.Resources.stroymat4;
                label6.Text = dataGridView1.Rows[itemstr1].Cells[3].Value.ToString();
                label5.Text = dataGridView1.Rows[itemstr1].Cells[1].Value.ToString();
                label7.Text = dataGridView1.Rows[itemstr1].Cells[2].Value.ToString();

            }
            else
            {


                pictureBox1.Image = null;
                label6.Text = "";
                label5.Text = "";
                label7.Text = "";

            }


            if (itemstr2 >= 0 && itemstr2 < dataGridView1.Rows.Count && dataGridView1.Rows[itemstr2].Cells[1].Value != null)
            {

              
                pictureBox2.Image = Properties.Resources.stroymat4;
                label12.Text = dataGridView1.Rows[itemstr2].Cells[3].Value.ToString();
                label13.Text = dataGridView1.Rows[itemstr2].Cells[1].Value.ToString();
                label11.Text = dataGridView1.Rows[itemstr2].Cells[2].Value.ToString();


            }
            else
            {
                pictureBox2.Image = null;
                label12.Text = "";
                label13.Text = "";
                label11.Text = "";
            }

            if (itemstr3 >= 0 && itemstr3 < dataGridView1.Rows.Count && dataGridView1.Rows[itemstr3].Cells[1].Value != null)
            {

                
                pictureBox3.Image = Properties.Resources.stroymat4;
                label15.Text = dataGridView1.Rows[itemstr3].Cells[3].Value.ToString();
                label16.Text = dataGridView1.Rows[itemstr3].Cells[1].Value.ToString();
                label14.Text = dataGridView1.Rows[itemstr3].Cells[2].Value.ToString();


            }
            else
            {
                pictureBox3.Image = null;
                label15.Text = "";
                label16.Text = "";
                label14.Text = "";
            }

            if (itemstr4 >= 0 && itemstr4 < dataGridView1.Rows.Count && dataGridView1.Rows[itemstr4].Cells[1].Value != null)
            {

               
                pictureBox4.Image = Properties.Resources.stroymat4;
                label18.Text = dataGridView1.Rows[itemstr4].Cells[3].Value.ToString();
                label19.Text = dataGridView1.Rows[itemstr4].Cells[1].Value.ToString();
                label17.Text = dataGridView1.Rows[itemstr4].Cells[2].Value.ToString();


            }
            else
            {
                pictureBox4.Image = null;
                label18.Text = "";
                label19.Text = "";
                label17.Text = "";

            }



            if (itemstr5 >= 0 && itemstr5 < dataGridView1.Rows.Count && dataGridView1.Rows[itemstr5].Cells[1].Value != null)
            {

             
                pictureBox5.Image = Properties.Resources.stroymat4;
                label21.Text = dataGridView1.Rows[itemstr5].Cells[3].Value.ToString();
                label22.Text = dataGridView1.Rows[itemstr5].Cells[1].Value.ToString();
                label20.Text = dataGridView1.Rows[itemstr5].Cells[2].Value.ToString();


            }
            else
            {
                pictureBox5.Image = null;
                label21.Text = "";
                label22.Text = "";
                label20.Text = "";
            }


            if (pictureBox5.Image == null) { button2.Enabled= false; }




            str++;
            label3.Text = str.ToString();
















        }





        private void button1_Click(object sender, EventArgs e)
        {


            button2.Enabled = true;
            if (str == 2)
            {
                button1.Enabled = false;
            }

            itemstr1 -= 5;
            itemstr2 -= 5;
            itemstr3 -= 5;
            itemstr4 -= 5;
            itemstr5 -= 5;
            if (itemstr1 >= 0 && itemstr1 < dataGridView1.Rows.Count && dataGridView1.Rows[itemstr1].Cells[1].Value != null)
            {

                pictureBox1.Image = Properties.Resources.stroymat4;
                label6.Text = dataGridView1.Rows[itemstr1].Cells[3].Value.ToString();
                label5.Text = dataGridView1.Rows[itemstr1].Cells[1].Value.ToString();
                label7.Text = dataGridView1.Rows[itemstr1].Cells[2].Value.ToString();
            }
            else
            {
                pictureBox1.Image = null;
                label6.Text = "";
                label5.Text = "";
                label7.Text = "";
            }


            if (itemstr2 >= 0 && itemstr2 < dataGridView1.Rows.Count && dataGridView1.Rows[itemstr2].Cells[1].Value != null)
            {

                
                pictureBox2.Image = Properties.Resources.stroymat4;
                label12.Text = dataGridView1.Rows[itemstr2].Cells[3].Value.ToString();
                label13.Text = dataGridView1.Rows[itemstr2].Cells[1].Value.ToString();
                label11.Text = dataGridView1.Rows[itemstr2].Cells[2].Value.ToString();
            }
            else
            {
                pictureBox2.Image = null;
                label12.Text = "";
                label13.Text = "";
                label11.Text = "";
            }

            if (itemstr3 >= 0 && itemstr3 < dataGridView1.Rows.Count && dataGridView1.Rows[itemstr3].Cells[1].Value != null)
            {
                
                pictureBox3.Image = Properties.Resources.stroymat4;

                label15.Text = dataGridView1.Rows[itemstr3].Cells[3].Value.ToString();
                label16.Text = dataGridView1.Rows[itemstr3].Cells[1].Value.ToString();
                label14.Text = dataGridView1.Rows[itemstr3].Cells[2].Value.ToString();
            }
            else
            {
                pictureBox3.Image = null;
                label15.Text = "";
                label16.Text = "";
                label14.Text = "";
            }

            if (itemstr4 >= 0 && itemstr4 < dataGridView1.Rows.Count && dataGridView1.Rows[itemstr4].Cells[1].Value != null)
            {
              
                pictureBox4.Image = Properties.Resources.stroymat4;

                label18.Text = dataGridView1.Rows[itemstr4].Cells[3].Value.ToString();
                label19.Text = dataGridView1.Rows[itemstr4].Cells[1].Value.ToString();
                label17.Text = dataGridView1.Rows[itemstr4].Cells[2].Value.ToString();
            }
            else
            {
                pictureBox4.Image = null;
                label18.Text = "";
                label19.Text = "";
                label17.Text = "";
            }



            if (itemstr5 >= 0 && itemstr5 < dataGridView1.Rows.Count && dataGridView1.Rows[itemstr5].Cells[1].Value != null)
            {
                
                pictureBox5.Image = Properties.Resources.stroymat4;

                label21.Text = dataGridView1.Rows[itemstr5].Cells[3].Value.ToString();
                label22.Text = dataGridView1.Rows[itemstr5].Cells[1].Value.ToString();
                label20.Text = dataGridView1.Rows[itemstr5].Cells[2].Value.ToString();
            }
            else
            {
                pictureBox5.Image = null;
                label21.Text = "";
                label22.Text = "";
                label20.Text = "";
            }


            str--;
            if (str < 1) { str = 1; }
            label3.Text = str.ToString();











        }
































        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {




            if (comboBox2.SelectedItem.ToString() == "Все типы")
            {
                dataGridView1.DataSource = GetProd();
                
                  if (comboBox1.SelectedItem == null)
                {
                    pictureBox1.Image = Properties.Resources.stroymat4;
                    pictureBox2.Image = Properties.Resources.stroymat4;
                    pictureBox3.Image = Properties.Resources.stroymat4;
                    pictureBox4.Image = Properties.Resources.stroymat4;
                    pictureBox5.Image = Properties.Resources.stroymat4;

                    label6.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                    label5.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                    label7.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();


                    label12.Text = dataGridView1.Rows[1].Cells[3].Value.ToString();
                    label13.Text = dataGridView1.Rows[1].Cells[1].Value.ToString();
                    label11.Text = dataGridView1.Rows[1].Cells[2].Value.ToString();


                    label15.Text = dataGridView1.Rows[2].Cells[3].Value.ToString();
                    label16.Text = dataGridView1.Rows[2].Cells[1].Value.ToString();
                    label14.Text = dataGridView1.Rows[2].Cells[2].Value.ToString();


                    label18.Text = dataGridView1.Rows[3].Cells[3].Value.ToString();
                    label19.Text = dataGridView1.Rows[3].Cells[1].Value.ToString();
                    label17.Text = dataGridView1.Rows[3].Cells[2].Value.ToString();



                    label21.Text = dataGridView1.Rows[4].Cells[3].Value.ToString();
                    label22.Text = dataGridView1.Rows[4].Cells[1].Value.ToString();
                    label20.Text = dataGridView1.Rows[4].Cells[2].Value.ToString();
                }



               else if (comboBox1.SelectedItem.ToString() == "Наименование")
                {
                    dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
                    pictureBox1.Image = Properties.Resources.stroymat4;
                    pictureBox2.Image = Properties.Resources.stroymat4;
                    pictureBox3.Image = Properties.Resources.stroymat4;
                    pictureBox4.Image = Properties.Resources.stroymat4;
                    pictureBox5.Image = Properties.Resources.stroymat4;
                    label6.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                    label5.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                    label7.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();


                    label12.Text = dataGridView1.Rows[1].Cells[3].Value.ToString();
                    label13.Text = dataGridView1.Rows[1].Cells[1].Value.ToString();
                    label11.Text = dataGridView1.Rows[1].Cells[2].Value.ToString();


                    label15.Text = dataGridView1.Rows[2].Cells[3].Value.ToString();
                    label16.Text = dataGridView1.Rows[2].Cells[1].Value.ToString();
                    label14.Text = dataGridView1.Rows[2].Cells[2].Value.ToString();


                    label18.Text = dataGridView1.Rows[3].Cells[3].Value.ToString();
                    label19.Text = dataGridView1.Rows[3].Cells[1].Value.ToString();
                    label17.Text = dataGridView1.Rows[3].Cells[2].Value.ToString();



                    label21.Text = dataGridView1.Rows[4].Cells[3].Value.ToString();
                    label22.Text = dataGridView1.Rows[4].Cells[1].Value.ToString();
                    label20.Text = dataGridView1.Rows[4].Cells[2].Value.ToString();
                }

                

                else if (comboBox1.SelectedItem.ToString() == "Минимальная стоимость")
                {
                    dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Ascending);
                    pictureBox1.Image = Properties.Resources.stroymat4;
                    pictureBox2.Image = Properties.Resources.stroymat4;
                    pictureBox3.Image = Properties.Resources.stroymat4;
                    pictureBox4.Image = Properties.Resources.stroymat4;
                    pictureBox5.Image = Properties.Resources.stroymat4;
                    label6.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                    label5.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                    label7.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();


                    label12.Text = dataGridView1.Rows[1].Cells[3].Value.ToString();
                    label13.Text = dataGridView1.Rows[1].Cells[1].Value.ToString();
                    label11.Text = dataGridView1.Rows[1].Cells[2].Value.ToString();


                    label15.Text = dataGridView1.Rows[2].Cells[3].Value.ToString();
                    label16.Text = dataGridView1.Rows[2].Cells[1].Value.ToString();
                    label14.Text = dataGridView1.Rows[2].Cells[2].Value.ToString();


                    label18.Text = dataGridView1.Rows[3].Cells[3].Value.ToString();
                    label19.Text = dataGridView1.Rows[3].Cells[1].Value.ToString();
                    label17.Text = dataGridView1.Rows[3].Cells[2].Value.ToString();



                    label21.Text = dataGridView1.Rows[4].Cells[3].Value.ToString();
                    label22.Text = dataGridView1.Rows[4].Cells[1].Value.ToString();
                    label20.Text = dataGridView1.Rows[4].Cells[2].Value.ToString();
                }
                str = 1;
                button1.Enabled = false;
                button2.Enabled = true;
                label3.Text = str.ToString();
            }









                
            
                if (comboBox2.SelectedItem.ToString() == "Прочее")
                {
                dataGridView1.DataSource = GetMouse();

                if (comboBox1.SelectedItem == null)
                {
                    pictureBox1.Image = Properties.Resources.stroymat4;
                    pictureBox2.Image = Properties.Resources.stroymat4;
                    pictureBox3.Image = Properties.Resources.stroymat4;
                    pictureBox4.Image = Properties.Resources.stroymat4;
                    pictureBox5.Image = Properties.Resources.stroymat4;
                    label6.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                    label5.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                    label7.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();


                    label12.Text = dataGridView1.Rows[1].Cells[3].Value.ToString();
                    label13.Text = dataGridView1.Rows[1].Cells[1].Value.ToString();
                    label11.Text = dataGridView1.Rows[1].Cells[2].Value.ToString();


                    label15.Text = dataGridView1.Rows[2].Cells[3].Value.ToString();
                    label16.Text = dataGridView1.Rows[2].Cells[1].Value.ToString();
                    label14.Text = dataGridView1.Rows[2].Cells[2].Value.ToString();


                    label18.Text = dataGridView1.Rows[3].Cells[3].Value.ToString();
                    label19.Text = dataGridView1.Rows[3].Cells[1].Value.ToString();
                    label17.Text = dataGridView1.Rows[3].Cells[2].Value.ToString();



                    label21.Text = dataGridView1.Rows[4].Cells[3].Value.ToString();
                    label22.Text = dataGridView1.Rows[4].Cells[1].Value.ToString();
                    label20.Text = dataGridView1.Rows[4].Cells[2].Value.ToString();
                }


               


                else if (comboBox1.SelectedItem.ToString() == "Наименование")
                {
                    dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
                    pictureBox1.Image = Properties.Resources.stroymat4;
                    pictureBox2.Image = Properties.Resources.stroymat4;
                    pictureBox3.Image = Properties.Resources.stroymat4;
                    pictureBox4.Image = Properties.Resources.stroymat4;
                    pictureBox5.Image = Properties.Resources.stroymat4;
                    label6.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                    label5.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                    label7.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();


                    label12.Text = dataGridView1.Rows[1].Cells[3].Value.ToString();
                    label13.Text = dataGridView1.Rows[1].Cells[1].Value.ToString();
                    label11.Text = dataGridView1.Rows[1].Cells[2].Value.ToString();


                    label15.Text = dataGridView1.Rows[2].Cells[3].Value.ToString();
                    label16.Text = dataGridView1.Rows[2].Cells[1].Value.ToString();
                    label14.Text = dataGridView1.Rows[2].Cells[2].Value.ToString();


                    label18.Text = dataGridView1.Rows[3].Cells[3].Value.ToString();
                    label19.Text = dataGridView1.Rows[3].Cells[1].Value.ToString();
                    label17.Text = dataGridView1.Rows[3].Cells[2].Value.ToString();



                    label21.Text = dataGridView1.Rows[4].Cells[3].Value.ToString();
                    label22.Text = dataGridView1.Rows[4].Cells[1].Value.ToString();
                    label20.Text = dataGridView1.Rows[4].Cells[2].Value.ToString();
                }
              
                else if (comboBox1.SelectedItem.ToString() == "Минимальная стоимость")
                {
                    dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Ascending);
                    pictureBox1.Image = Properties.Resources.stroymat4;
                    pictureBox2.Image = Properties.Resources.stroymat4;
                    pictureBox3.Image = Properties.Resources.stroymat4;
                    pictureBox4.Image = Properties.Resources.stroymat4;
                    pictureBox5.Image = Properties.Resources.stroymat4;
                    label6.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                    label5.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                    label7.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();


                    label12.Text = dataGridView1.Rows[1].Cells[3].Value.ToString();
                    label13.Text = dataGridView1.Rows[1].Cells[1].Value.ToString();
                    label11.Text = dataGridView1.Rows[1].Cells[2].Value.ToString();


                    label15.Text = dataGridView1.Rows[2].Cells[3].Value.ToString();
                    label16.Text = dataGridView1.Rows[2].Cells[1].Value.ToString();
                    label14.Text = dataGridView1.Rows[2].Cells[2].Value.ToString();


                    label18.Text = dataGridView1.Rows[3].Cells[3].Value.ToString();
                    label19.Text = dataGridView1.Rows[3].Cells[1].Value.ToString();
                    label17.Text = dataGridView1.Rows[3].Cells[2].Value.ToString();



                    label21.Text = dataGridView1.Rows[4].Cells[3].Value.ToString();
                    label22.Text = dataGridView1.Rows[4].Cells[1].Value.ToString();
                    label20.Text = dataGridView1.Rows[4].Cells[2].Value.ToString();
                }

                str = 1;
                button1.Enabled = false;
                button2.Enabled = true;
                label3.Text = str.ToString();
                }




            else if (comboBox2.SelectedItem.ToString() == "Обои")
            {
                dataGridView1.DataSource = GetKlav();

                if (comboBox1.SelectedItem == null)
                {
                    pictureBox1.Image = Properties.Resources.stroymat4;
                    pictureBox2.Image = Properties.Resources.stroymat4;
                    pictureBox3.Image = Properties.Resources.stroymat4;
                    pictureBox4.Image = Properties.Resources.stroymat4;
                    pictureBox5.Image = Properties.Resources.stroymat4;
                    label6.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                    label5.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                    label7.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();


                    label12.Text = dataGridView1.Rows[1].Cells[3].Value.ToString();
                    label13.Text = dataGridView1.Rows[1].Cells[1].Value.ToString();
                    label11.Text = dataGridView1.Rows[1].Cells[2].Value.ToString();


                    label15.Text = dataGridView1.Rows[2].Cells[3].Value.ToString();
                    label16.Text = dataGridView1.Rows[2].Cells[1].Value.ToString();
                    label14.Text = dataGridView1.Rows[2].Cells[2].Value.ToString();


                    label18.Text = dataGridView1.Rows[3].Cells[3].Value.ToString();
                    label19.Text = dataGridView1.Rows[3].Cells[1].Value.ToString();
                    label17.Text = dataGridView1.Rows[3].Cells[2].Value.ToString();



                    label21.Text = dataGridView1.Rows[4].Cells[3].Value.ToString();
                    label22.Text = dataGridView1.Rows[4].Cells[1].Value.ToString();
                    label20.Text = dataGridView1.Rows[4].Cells[2].Value.ToString();
                }


              
                else if (comboBox1.SelectedItem.ToString() == "Наименование")
                {
                    pictureBox1.Image = Properties.Resources.stroymat4;
                    pictureBox2.Image = Properties.Resources.stroymat4;
                    pictureBox3.Image = Properties.Resources.stroymat4;
                    pictureBox4.Image = Properties.Resources.stroymat4;
                    pictureBox5.Image = Properties.Resources.stroymat4;
                    dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
                    label6.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                    label5.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                    label7.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();


                    label12.Text = dataGridView1.Rows[1].Cells[3].Value.ToString();
                    label13.Text = dataGridView1.Rows[1].Cells[1].Value.ToString();
                    label11.Text = dataGridView1.Rows[1].Cells[2].Value.ToString();


                    label15.Text = dataGridView1.Rows[2].Cells[3].Value.ToString();
                    label16.Text = dataGridView1.Rows[2].Cells[1].Value.ToString();
                    label14.Text = dataGridView1.Rows[2].Cells[2].Value.ToString();


                    label18.Text = dataGridView1.Rows[3].Cells[3].Value.ToString();
                    label19.Text = dataGridView1.Rows[3].Cells[1].Value.ToString();
                    label17.Text = dataGridView1.Rows[3].Cells[2].Value.ToString();



                    label21.Text = dataGridView1.Rows[4].Cells[3].Value.ToString();
                    label22.Text = dataGridView1.Rows[4].Cells[1].Value.ToString();
                    label20.Text = dataGridView1.Rows[4].Cells[2].Value.ToString();
                }
               
                else if (comboBox1.SelectedItem.ToString() == "Минимальная стоимость")
                {
                    dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Ascending);

                    pictureBox1.Image = Properties.Resources.stroymat4;
                    pictureBox2.Image = Properties.Resources.stroymat4;
                    pictureBox3.Image = Properties.Resources.stroymat4;
                    pictureBox4.Image = Properties.Resources.stroymat4;
                    pictureBox5.Image = Properties.Resources.stroymat4;

                    label6.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                    label5.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                    label7.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();


                    label12.Text = dataGridView1.Rows[1].Cells[3].Value.ToString();
                    label13.Text = dataGridView1.Rows[1].Cells[1].Value.ToString();
                    label11.Text = dataGridView1.Rows[1].Cells[2].Value.ToString();


                    label15.Text = dataGridView1.Rows[2].Cells[3].Value.ToString();
                    label16.Text = dataGridView1.Rows[2].Cells[1].Value.ToString();
                    label14.Text = dataGridView1.Rows[2].Cells[2].Value.ToString();


                    label18.Text = dataGridView1.Rows[3].Cells[3].Value.ToString();
                    label19.Text = dataGridView1.Rows[3].Cells[1].Value.ToString();
                    label17.Text = dataGridView1.Rows[3].Cells[2].Value.ToString();



                    label21.Text = dataGridView1.Rows[4].Cells[3].Value.ToString();
                    label22.Text = dataGridView1.Rows[4].Cells[1].Value.ToString();
                    label20.Text = dataGridView1.Rows[4].Cells[2].Value.ToString();
                }

                str = 1;
                button1.Enabled = false;
                button2.Enabled = true;
                label3.Text = str.ToString();
            }
           
        }












        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            itemstr1 = 0;
            itemstr2 = 1;
            itemstr3 = 2;
            itemstr4 = 3;
            itemstr5 = 4;

            if (comboBox1.SelectedItem.ToString() == "Наименование")
            {
                dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
            }
           
            else if (comboBox1.SelectedItem.ToString() == "Минимальная стоимость")
            {
                dataGridView1.Sort(dataGridView1.Columns[2], ListSortDirection.Ascending);
            }

            pictureBox1.Image = Properties.Resources.stroymat4;


            label6.Text = dataGridView1.Rows[itemstr1].Cells[3].Value.ToString();
            label5.Text = dataGridView1.Rows[itemstr1].Cells[1].Value.ToString();
            label7.Text = dataGridView1.Rows[itemstr1].Cells[2].Value.ToString();

            pictureBox2.Image = Properties.Resources.stroymat4;

            label12.Text = dataGridView1.Rows[itemstr2].Cells[3].Value.ToString();
            label13.Text = dataGridView1.Rows[itemstr2].Cells[1].Value.ToString();
            label11.Text = dataGridView1.Rows[itemstr2].Cells[2].Value.ToString();


            pictureBox3.Image = Properties.Resources.stroymat4;

            label15.Text = dataGridView1.Rows[itemstr3].Cells[3].Value.ToString();
            label16.Text = dataGridView1.Rows[itemstr3].Cells[1].Value.ToString();
            label14.Text = dataGridView1.Rows[itemstr3].Cells[2].Value.ToString();


            pictureBox4.Image = Properties.Resources.stroymat4;

            label18.Text = dataGridView1.Rows[itemstr4].Cells[3].Value.ToString();
            label19.Text = dataGridView1.Rows[itemstr4].Cells[1].Value.ToString();
            label17.Text = dataGridView1.Rows[itemstr4].Cells[2].Value.ToString();

            
            pictureBox5.Image = Properties.Resources.stroymat4;

            label21.Text = dataGridView1.Rows[itemstr5].Cells[3].Value.ToString();
            label22.Text = dataGridView1.Rows[itemstr5].Cells[1].Value.ToString();
            label20.Text = dataGridView1.Rows[itemstr5].Cells[2].Value.ToString();
        



            str = 1;
            button1.Enabled = false;
            button2.Enabled = true;

            label3.Text = str.ToString();
        }






        private void Shtrih(object sender, EventArgs e)
        {

            Label clickedLabel = sender as Label;
            string labelText = clickedLabel.Text;


            string outputFilePath = $@"C:\\Users\\Acer\\Desktop\\shtihkod_{labelText}.pdf";

            // Создать новый документ PDF
            Document document = new Document();
            // Создать экземпляр PdfWriter и привязать его к документу
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outputFilePath, FileMode.Create));
            // Открыть документ для записи
            document.Open();
            // Загрузить изображение Bitmap из существующей картинки
            Bitmap image = DrawBarcode(labelText);
            // Преобразовать Bitmap в iTextSharp-объект Image
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            iTextSharp.text.Image pdfImage = iTextSharp.text.Image.GetInstance(ms.GetBuffer());
            pdfImage.ScaleToFit(PageSize.A4.Width, PageSize.A4.Height);
            // Добавить изображение на страницу PDF
            document.Add(pdfImage);
            // Закрыть документ и очистить ресурсы
            document.Close();
            writer.Close();
            MessageBox.Show($"Штрихкод распечатан, файл: shtihkod_{labelText}.pdf");



        }




    }
}
        
    
