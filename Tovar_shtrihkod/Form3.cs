using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tovar_shtrihkod;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Tovar_shtrihkod.avtoriz;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.VisualBasic.Devices;
using System.IO;
using System.Text.Encodings;

namespace Praktika1
{


    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            label1.Text = UserData.Username; //УСТАНОВКА ИМЕНИ
            DB db = new DB(); //СОЗДАНИЕ КЛАССА           
            dataGridView2.DataSource = db.GetTovar(); //УСТАНОВКА ЗНАЧЕНИЙ ДАТА ГРИДА
            dataGridView2.AutoResizeColumns(); //АВТОМАТИЧЕСКИЙ РАЗМЕР КОЛЛОН
            //УСТАНОВКА НАЗАНИЙ КОЛОН
            dataGridView2.Columns[0].HeaderText = "Идентификатор"; 
            dataGridView2.Columns[1].HeaderText = "Название";
            dataGridView2.Columns[2].HeaderText = "Цена";
            dataGridView2.Columns[3].HeaderText = "Артикул";
            dataGridView2.Columns[4].HeaderText = "Количество на складе";
            dataGridView2.RowHeadersVisible = false; //УБРАТЬ КОЛОННУ СЛЕВА
            dataGridView2.AllowUserToAddRows = false; //УБРАТЬ СТРОКУ ДОБАВЛЕНИЯ
        }

        string title_tovar;

        double prise;


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int x = e.RowIndex;
                title_tovar = dataGridView2.Rows[x].Cells[1].Value.ToString();
                string prs = dataGridView2.Rows[x].Cells[2].Value.ToString();
                prise = Convert.ToDouble(prs);
            }
            catch
            {
                MessageBox.Show("Выберите товар!", "Ошибка!");
            }




        }



        private bool IsSrokValid(string x)
        {
            if (x.AsEnumerable().Any(ch => char.IsLetter(ch)) == true)
                return false;

            // Проверка длины пароля
            if (x.Length < 2)
                return false;


            if (Convert.ToInt32(x) > 12)
                return false;
            // Проверка количества букв, цифр и знаков

            int symbolCount = Regex.Matches(x, "[@#%)(.<]").Count;

            if (symbolCount > 0)
                return false;

            // Пароль соответствует требованиям
            return true;
        }

        private bool IsCostValid(string x)
        {
            if (x.AsEnumerable().Any(ch => char.IsLetter(ch)) == true)
                return false;


            int symbolCount = Regex.Matches(x, "[@#%)(.<]").Count;

            if (symbolCount > 0)
                return false;

            // Пароль соответствует требованиям
            return true;
        }


        private void button3_Click(object sender, EventArgs e)
        {

            bool val = true;

            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка!");
                val = false;
            }
            else if (textBox2.Text == string.Empty)
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка!");

                val = false;

            }
            else if (textBox3.Text == string.Empty)
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка!");

                val = false;

            }
            else if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка!");

                val = false;

            }
            else if (IsCostValid(textBox3.Text) == false)
            {
                MessageBox.Show("Введите корректное количество товара!", "Ошибка!");
                textBox3.Clear();
                val = false;

            }
            else if (val == true)
            {


                try
                {
                    DB dB = new DB();
                    int n = Convert.ToInt32(textBox3.Text);

                    double costs = prise * n;

                    dB.SetZakaz(textBox1.Text, textBox2.Text, title_tovar, prise, n, costs);








                    using (FileStream stream = new FileStream($@"D:\лабы дойникова\Tovar_shtrihkod\PDF\{textBox1.Text}.pdf", FileMode.Create))
                    {
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                        Document document = new Document(PageSize.A4, 0, 0, 0, 0);
                        PdfWriter.GetInstance(document, stream);
                        document.Open();


                        BaseFont baseFont = BaseFont.CreateFont(@"D:\лабы дойникова\Tovar_shtrihkod\Tovar_shtrihkod\timesnrcyrmt.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);

                        // Создание таблицы
                        PdfPTable tabe = new PdfPTable(4);

                        // Устанавливаем ширину столбцов
                        float[] columnWidths = { 20f, 20f, 20f, 20f };
                        tabe.SetWidths(columnWidths);



                        document.Add((new Paragraph($"                                                                       Чек ООО СтройМоодерн", font)));
                        String pr = Environment.NewLine;
                        document.Add(new Paragraph(pr, font));





                        // Добавление заголовка
                        PdfPCell qrCell = new PdfPCell(new Phrase("Название товара", font));
                        qrCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        qrCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabe.AddCell(qrCell);




                        PdfPCell dataCell = new PdfPCell(new Phrase("Количество", font));
                        dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        dataCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabe.AddCell(dataCell);

                        PdfPCell dataCell1 = new PdfPCell(new Phrase("Цена", font));
                        dataCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                        dataCell1.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabe.AddCell(dataCell1);

                        PdfPCell dataCell2 = new PdfPCell(new Phrase("Итоговая стоимость", font));
                        dataCell2.HorizontalAlignment = Element.ALIGN_CENTER;
                        dataCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabe.AddCell(dataCell2);






                        PdfPCell qrCell11 = new PdfPCell(new Phrase(title_tovar, font));
                        qrCell11.HorizontalAlignment = Element.ALIGN_CENTER;
                        qrCell11.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabe.AddCell(qrCell11);




                        PdfPCell dataCell11 = new PdfPCell(new Phrase(textBox3.Text, font));
                        dataCell11.HorizontalAlignment = Element.ALIGN_CENTER;
                        dataCell11.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabe.AddCell(dataCell11);




                        PdfPCell dataCell22 = new PdfPCell(new Phrase($"{Convert.ToString(prise)}", font));
                        dataCell22.HorizontalAlignment = Element.ALIGN_CENTER;
                        dataCell22.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabe.AddCell(dataCell22);

                        PdfPCell dataCell33 = new PdfPCell(new Phrase($"{Convert.ToString(costs)}", font));
                        dataCell33.HorizontalAlignment = Element.ALIGN_CENTER;
                        dataCell33.VerticalAlignment = Element.ALIGN_MIDDLE;
                        tabe.AddCell(dataCell33);




                        document.Add(tabe);

                        document.Close();




                    }
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    dB.UpdateKolvo(n, title_tovar);
                    dB.UpdateKolvo1();
                    MessageBox.Show("Заказ успешно сформирован!");
                }
                catch
                {
                    MessageBox.Show("Проверьте корректность введённых данных!", "Что - то пошло не так!");
                }






            }




        }

        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            dataGridView2.DataSource = db.GetTovar();
            dataGridView2.AutoResizeColumns();
            dataGridView2.Columns[0].HeaderText = "Идентификатор";
            dataGridView2.Columns[1].HeaderText = "Название";
            dataGridView2.Columns[2].HeaderText = "Цена";
            dataGridView2.Columns[3].HeaderText = "Артикул";
            dataGridView2.Columns[4].HeaderText = "Количество на складе";
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.AllowUserToAddRows = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form1 = new avtoriz();
            form1.Closed += (s, args) => this.Close();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }



        

    }
}

