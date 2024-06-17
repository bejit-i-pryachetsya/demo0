using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Npgsql.Internal;
using SixLabors.Fonts;
using ZXing;
using ZXing.Common;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Tovar_shtrihkod.avtoriz;
using Npgsql;
using Tovar_shtrihkod;
using Org.BouncyCastle.Math;
using System.Diagnostics.Contracts;
using System.Reflection.Emit;
using System.Net.Mail;
using System.Net;

namespace Praktika1
{
    public partial class Form2 : Form
    {





        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }





        public Form2()
        {
            InitializeComponent();

            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
            dataGridView2.DataError += new DataGridViewDataErrorEventHandler(dataGridView2_DataError);
            dataGridView3.DataError += new DataGridViewDataErrorEventHandler(dataGridView3_DataError);


            label1.Text = UserData.Username;

            DB db = new DB();
            dataGridView3.DataSource = db.GetTovar();
            dataGridView3.AutoResizeColumns();
            dataGridView3.Columns[0].HeaderText = "Идентификатор";
            dataGridView3.Columns[1].HeaderText = "Название";
            dataGridView3.Columns[2].HeaderText = "Цена";
            dataGridView3.Columns[3].HeaderText = "Артикул";
            dataGridView3.Columns[4].HeaderText = "Количество на складе";
            dataGridView3.RowHeadersVisible = false;

            dataGridView2.DataSource = db.GetUser();
            dataGridView2.AutoResizeColumns();
            dataGridView2.Columns[0].HeaderText = "Идентификатор";
            dataGridView2.Columns[1].HeaderText = "Идентификатор роли";
            dataGridView2.Columns[2].HeaderText = "Логиин";
            dataGridView2.Columns[3].HeaderText = "Пароль";        
            dataGridView2.RowHeadersVisible = false;


            dataGridView1.DataSource = db.GetZakaz();
            dataGridView1.AutoResizeColumns();
            dataGridView1.Columns[0].HeaderText = "Идентификатор";
            dataGridView1.Columns[1].HeaderText = "ФИО заказчика";
            dataGridView1.Columns[2].HeaderText = "Адрес здания ремонта";
            dataGridView1.Columns[3].HeaderText = "Название товара";
            dataGridView1.Columns[4].HeaderText = "Стоимость товара";
            dataGridView1.Columns[5].HeaderText = "Количество товара";
            dataGridView1.Columns[6].HeaderText = "Итоговая цена";
            dataGridView1.RowHeadersVisible = false;
           
















        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form1 = new avtoriz();
            form1.Closed += (s, args) => this.Close();
            form1.Show();
        }

        string x_z;
        string x_t;
        string x_u;

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int x = e.RowIndex;
                x_u = dataGridView2.Rows[x].Cells[0].Value.ToString();   
            }
            catch
            {
                MessageBox.Show("Выберите пользователя!", "Ошибка!");
            }
        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int x = e.RowIndex;
                x_z = dataGridView1.Rows[x].Cells[0].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Выберите заказ!", "Ошибка!");
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int x = e.RowIndex;
                x_t = dataGridView3.Rows[x].Cells[0].Value.ToString();
            }
            catch
            {
                MessageBox.Show("Выберите товар!", "Ошибка!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                DB dB = new DB();
                dB.delete("Usеr", x_u);
                dataGridView2.DataSource = dB.GetUser();
                dataGridView2.AutoResizeColumns();
                dataGridView2.Columns[0].HeaderText = "Идентификатор";
                dataGridView2.Columns[1].HeaderText = "Идентификатор роли";
                dataGridView2.Columns[2].HeaderText = "Логиин";
                dataGridView2.Columns[3].HeaderText = "Пароль";
                dataGridView2.RowHeadersVisible = false;
                MessageBox.Show("Пользователь успешно удалён.");
            }
            catch 
            {
                MessageBox.Show("Выберите пользователя!", "Ошибка!");
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DB dB = new DB();
                dB.delete("Zakaz", x_z);

                dataGridView1.DataSource = dB.GetZakaz();
                dataGridView1.AutoResizeColumns();
                dataGridView1.Columns[0].HeaderText = "Идентификатор";
                dataGridView1.Columns[1].HeaderText = "ФИО заказчика";
                dataGridView1.Columns[2].HeaderText = "Адрес здания ремонта";
                dataGridView1.Columns[3].HeaderText = "Название товара";
                dataGridView1.Columns[4].HeaderText = "Стоимость товара";
                dataGridView1.Columns[5].HeaderText = "Количество товара";
                dataGridView1.Columns[6].HeaderText = "Итоговая цена";
                dataGridView1.RowHeadersVisible = false;
                MessageBox.Show("Заказ успешно удалён.");
            }
            catch
            {
                MessageBox.Show("Выберите заказ!", "Ошибка!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DB dB = new DB();
                dB.delete("Tovar", x_t);
                dataGridView3.DataSource = dB.GetTovar();
                dataGridView3.AutoResizeColumns();
                dataGridView3.Columns[0].HeaderText = "Идентификатор";
                dataGridView3.Columns[1].HeaderText = "Название";
                dataGridView3.Columns[2].HeaderText = "Цена";
                dataGridView3.Columns[3].HeaderText = "Артикул";
                dataGridView3.Columns[4].HeaderText = "Количество на складе";
                dataGridView3.RowHeadersVisible = false;

                MessageBox.Show("Товар успешно удалён.");
            }
            catch
            {
                MessageBox.Show("Выберите товар!", "Ошибка!");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DB db = new DB();






            dataGridView3.DataSource = db.GetTovar();
            dataGridView3.AutoResizeColumns();
            dataGridView3.Columns[0].HeaderText = "Идентификатор";
            dataGridView3.Columns[1].HeaderText = "Название";
            dataGridView3.Columns[2].HeaderText = "Цена";
            dataGridView3.Columns[3].HeaderText = "Артикул";
            dataGridView3.Columns[4].HeaderText = "Количество на складе";
            dataGridView3.RowHeadersVisible = false;

            dataGridView2.DataSource = db.GetUser();
            dataGridView2.AutoResizeColumns();
            dataGridView2.Columns[0].HeaderText = "Идентификатор";
            dataGridView2.Columns[1].HeaderText = "Идентификатор роли";
            dataGridView2.Columns[2].HeaderText = "Логиин";
            dataGridView2.Columns[3].HeaderText = "Пароль";
            dataGridView2.RowHeadersVisible = false;


            dataGridView1.DataSource = db.GetZakaz();
            dataGridView1.AutoResizeColumns();
            dataGridView1.Columns[0].HeaderText = "Идентификатор";
            dataGridView1.Columns[1].HeaderText = "ФИО заказчика";
            dataGridView1.Columns[2].HeaderText = "Адрес здания ремонта";
            dataGridView1.Columns[3].HeaderText = "Название товара";
            dataGridView1.Columns[4].HeaderText = "Стоимость товара";
            dataGridView1.Columns[5].HeaderText = "Количество товара";
            dataGridView1.Columns[6].HeaderText = "Итоговая цена";
            dataGridView1.RowHeadersVisible = false;
        }
        
        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                dataGridView1.AllowUserToAddRows = false;
                dataGridView2.AllowUserToAddRows = false;
                dataGridView3.AllowUserToAddRows = false;

                bool f = true;


                foreach (DataGridViewRow row in this.dataGridView1.Rows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Value == null || row.Cells[i].Value == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[i].Value.ToString()) || row.Cells[i].Value.ToString() == string.Empty)
                        {
                            f = false;
                            break;

                        }
                    }
                }


                foreach (DataGridViewRow row in this.dataGridView2.Rows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Value == null || row.Cells[i].Value == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[i].Value.ToString()) || row.Cells[i].Value.ToString() == string.Empty)
                        {
                            f = false;
                            break;

                        }
                    }
                }

                foreach (DataGridViewRow row in this.dataGridView3.Rows)
                {
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (row.Cells[i].Value == null || row.Cells[i].Value == DBNull.Value || String.IsNullOrWhiteSpace(row.Cells[i].Value.ToString()) || row.Cells[i].Value.ToString() == string.Empty)
                        {
                            f = false;
                            break;

                        }
                    }
                }

                if (f == true)
                {
                    MessageBox.Show("Данные успешно обновлены."); //ДЕЙСТВИЯ НСЛИ ВСЁ ОК

                    DB dB = new DB();
                    dB.delete_table("Zakaz");
                    dB.delete_table("Tovar");
                    dB.delete_table("Usеr");

                    foreach (DataGridViewRow row in this.dataGridView1.Rows)
                    {
                        dB.insert_zakaz(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString(), row.Cells[5].Value.ToString(), row.Cells[6].Value.ToString());
                    }

                    foreach (DataGridViewRow row in this.dataGridView2.Rows)
                    {
                        dB.insert_user(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString());
                    }

                    foreach (DataGridViewRow row in this.dataGridView3.Rows)
                    {
                        dB.insert_tovar(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString());
                    }



                    DB db = new DB();
                    dataGridView3.DataSource = db.GetTovar();
                    dataGridView3.AutoResizeColumns();
                    dataGridView3.Columns[0].HeaderText = "Идентификатор";
                    dataGridView3.Columns[1].HeaderText = "Название";
                    dataGridView3.Columns[2].HeaderText = "Цена";
                    dataGridView3.Columns[3].HeaderText = "Артикул";
                    dataGridView3.Columns[4].HeaderText = "Количество на складе";
                    dataGridView3.RowHeadersVisible = false;

                    dataGridView2.DataSource = db.GetUser();
                    dataGridView2.AutoResizeColumns();
                    dataGridView2.Columns[0].HeaderText = "Идентификатор";
                    dataGridView2.Columns[1].HeaderText = "Идентификатор роли";
                    dataGridView2.Columns[2].HeaderText = "Логиин";
                    dataGridView2.Columns[3].HeaderText = "Пароль";
                    dataGridView2.RowHeadersVisible = false;


                    dataGridView1.DataSource = db.GetZakaz();
                    dataGridView1.AutoResizeColumns();
                    dataGridView1.Columns[0].HeaderText = "Идентификатор";
                    dataGridView1.Columns[1].HeaderText = "ФИО заказчика";
                    dataGridView1.Columns[2].HeaderText = "Адрес здания ремонта";
                    dataGridView1.Columns[3].HeaderText = "Название товара";
                    dataGridView1.Columns[4].HeaderText = "Стоимость товара";
                    dataGridView1.Columns[5].HeaderText = "Количество товара";
                    dataGridView1.Columns[6].HeaderText = "Итоговая цена";
                    dataGridView1.RowHeadersVisible = false;


                    dataGridView1.AllowUserToAddRows = true;
                    dataGridView2.AllowUserToAddRows = true;
                    dataGridView3.AllowUserToAddRows = true;

                }



                else if (f == false)
                {
                    MessageBox.Show("При Добавлении/Обновлении строк, должны быть заполнены все поля новой строки!", "Ошибка!");
                    DB db = new DB();
                    dataGridView3.DataSource = db.GetTovar();
                    dataGridView3.AutoResizeColumns();
                    dataGridView3.Columns[0].HeaderText = "Идентификатор";
                    dataGridView3.Columns[1].HeaderText = "Название";
                    dataGridView3.Columns[2].HeaderText = "Цена";
                    dataGridView3.Columns[3].HeaderText = "Артикул";
                    dataGridView3.Columns[4].HeaderText = "Количество на складе";
                    dataGridView3.RowHeadersVisible = false;

                    dataGridView2.DataSource = db.GetUser();
                    dataGridView2.AutoResizeColumns();
                    dataGridView2.Columns[0].HeaderText = "Идентификатор";
                    dataGridView2.Columns[1].HeaderText = "Идентификатор роли";
                    dataGridView2.Columns[2].HeaderText = "Логиин";
                    dataGridView2.Columns[3].HeaderText = "Пароль";
                    dataGridView2.RowHeadersVisible = false;


                    dataGridView1.DataSource = db.GetZakaz();
                    dataGridView1.AutoResizeColumns();
                    dataGridView1.Columns[0].HeaderText = "Идентификатор";
                    dataGridView1.Columns[1].HeaderText = "ФИО заказчика";
                    dataGridView1.Columns[2].HeaderText = "Адрес здания ремонта";
                    dataGridView1.Columns[3].HeaderText = "Название товара";
                    dataGridView1.Columns[4].HeaderText = "Стоимость товара";
                    dataGridView1.Columns[5].HeaderText = "Количество товара";
                    dataGridView1.Columns[6].HeaderText = "Итоговая цена";
                    dataGridView1.RowHeadersVisible = false;
                    dataGridView1.AllowUserToAddRows = true;
                    dataGridView2.AllowUserToAddRows = true;
                    dataGridView3.AllowUserToAddRows = true;
                }
            }
            catch
            {
                MessageBox.Show("При Добавлении/Обновлении строк, должны быть заполнены все поля новой строки!", "Ошибка!");
                DB db = new DB();
                dataGridView3.DataSource = db.GetTovar();
                dataGridView3.AutoResizeColumns();
                dataGridView3.Columns[0].HeaderText = "Идентификатор";
                dataGridView3.Columns[1].HeaderText = "Название";
                dataGridView3.Columns[2].HeaderText = "Цена";
                dataGridView3.Columns[3].HeaderText = "Артикул";
                dataGridView3.Columns[4].HeaderText = "Количество на складе";
                dataGridView3.RowHeadersVisible = false;

                dataGridView2.DataSource = db.GetUser();
                dataGridView2.AutoResizeColumns();
                dataGridView2.Columns[0].HeaderText = "Идентификатор";
                dataGridView2.Columns[1].HeaderText = "Идентификатор роли";
                dataGridView2.Columns[2].HeaderText = "Логиин";
                dataGridView2.Columns[3].HeaderText = "Пароль";
                dataGridView2.RowHeadersVisible = false;


                dataGridView1.DataSource = db.GetZakaz();
                dataGridView1.AutoResizeColumns();
                dataGridView1.Columns[0].HeaderText = "Идентификатор";
                dataGridView1.Columns[1].HeaderText = "ФИО заказчика";
                dataGridView1.Columns[2].HeaderText = "Адрес здания ремонта";
                dataGridView1.Columns[3].HeaderText = "Название товара";
                dataGridView1.Columns[4].HeaderText = "Стоимость товара";
                dataGridView1.Columns[5].HeaderText = "Количество товара";
                dataGridView1.Columns[6].HeaderText = "Итоговая цена";
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.AllowUserToAddRows = true;
                dataGridView2.AllowUserToAddRows = true;
                dataGridView3.AllowUserToAddRows = true;
            }


            




        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is System.FormatException)
            {
                MessageBox.Show("Некорректный формат ввода в ячейке. Пожалуйста, введите корректные данные.", "Ошибка!");
            }
            else
            {
                // другие типы ошибок, если необходимо обрабатывать
                MessageBox.Show("Произошла ошибка ввода данных. Пожалуйста, введите корректные данные.", "Ошибка!");
            }

            // Отменяем событие ошибки, чтобы избежать возникновения исключения
            e.Cancel = true;
        }
        private void dataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is System.FormatException)
            {
                MessageBox.Show("Некорректный формат ввода в ячейке. Пожалуйста, введите корректные данные.", "Ошибка!");
            }
            else
            {
                // другие типы ошибок, если необходимо обрабатывать
                MessageBox.Show("Произошла ошибка ввода данных. Пожалуйста, введите корректные данные.", "Ошибка!");
            }

            // Отменяем событие ошибки, чтобы избежать возникновения исключения
            e.Cancel = true;
        }
        private void dataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is System.FormatException)
            {
                MessageBox.Show("Некорректный формат ввода в ячейке. Пожалуйста, введите корректные данные.", "Ошибка!");
            }
            else
            {
                // другие типы ошибок, если необходимо обрабатывать
                MessageBox.Show("Произошла ошибка ввода данных. Пожалуйста, введите корректные данные.", "Ошибка!");
            }

            // Отменяем событие ошибки, чтобы избежать возникновения исключения
            e.Cancel = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string to = "kandreeva@cchgeu.ru";
            string from = "vaslol32@mail.ru";
            string subject = "Реклама «СтройМодерн»";
            string body = "Уважаемый клиент! Только 29 февраля вся продукция будет со скидкой – 20%, при указании кодового слова «Дэмоэкзамен 2023».";

            MailMessage message = new MailMessage(from, to, subject, body);

            SmtpClient client = new SmtpClient("smtp.mail.ru", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("vaslol32@mail.ru", "C4wMUagDJDZLD4vj3Udm");

            try
            {
                client.Send(message);
                MessageBox.Show("Email отправлен успешно!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при отправке сообщения: " + ex.Message);
            }
        }
    }
}
