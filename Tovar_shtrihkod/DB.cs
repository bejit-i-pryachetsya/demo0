using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Npgsql.Internal;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Praktika1
{
    internal class DB
    {
        NpgsqlConnection connection = new NpgsqlConnection("User ID = postgres; Password = 12345; Host = localhost; Port = 5432; database = modern;");



        public void UpdateKolvo(int kolvo, string title)
        {


            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            string command = $"UPDATE Tovar SET quantity = quantity - {kolvo} where title = '{title}'";
            
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command, connection);
            dataSet.Reset();
            dataAdapter.Fill(dataTable);

           

            connection.Close();


        }

        public void UpdateKolvo1()
        {

            DataSet dataSet1 = new DataSet();
            DataTable dataTable1 = new DataTable();
            string command1 = $"update tovar set quantity = 0 where quantity < 0;";

            NpgsqlDataAdapter dataAdapter1 = new NpgsqlDataAdapter(command1, connection);
            dataSet1.Reset();
            dataAdapter1.Fill(dataTable1);

            connection.Close();


        }

        public DataTable GetTovar()
        {
           

            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            string command = $"Select * From Tovar";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command, connection);
            dataSet.Reset();
            dataAdapter.Fill(dataTable);

            return dataTable;

            connection.Close();

        }

        public void SetZakaz(string Fio, string adres, string title_tovar, double costs_tovar, int kollvo, double costs)
        {


            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            string command = $"Insert Into Zakaz Values (default,'{Fio}','{adres}','{title_tovar}',{costs_tovar},{kollvo},{costs})";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command, connection);
            dataSet.Reset();
            dataAdapter.Fill(dataTable);
          
            connection.Close();


        }


        public DataTable GetZakaz()
        {
           

            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            string command = $"Select * From Zakaz";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command, connection);
            dataSet.Reset();
            dataAdapter.Fill(dataTable);

            return dataTable;
           

        }

        public DataTable GetUser()
        {
           
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            string command = $"Select * From Usеr";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command, connection);
            dataSet.Reset();
            dataAdapter.Fill(dataTable);

            return dataTable;
           


        }



        public void delete(string name, string id)
        {

            DataSet dataSet1 = new DataSet();
            DataTable dataTable1 = new DataTable();
            string command1 = $"delete from {name} where id = {id};";

            NpgsqlDataAdapter dataAdapter1 = new NpgsqlDataAdapter(command1, connection);
            dataSet1.Reset();
            dataAdapter1.Fill(dataTable1);

            connection.Close();


        }


        public void delete_table(string name)
        {

            DataSet dataSet1 = new DataSet();
            DataTable dataTable1 = new DataTable();
            string command1 = $"delete from {name}";

            NpgsqlDataAdapter dataAdapter1 = new NpgsqlDataAdapter(command1, connection);
            dataSet1.Reset();
            dataAdapter1.Fill(dataTable1);

            connection.Close();


        }

        public void insert_user(string id, string id_role, string loginn, string pass)
        {

            DataSet dataSet1 = new DataSet();
            DataTable dataTable1 = new DataTable();
            string command1 = $"insert into usеr values ({id}, {id_role}, '{loginn}', '{pass}')";

            NpgsqlDataAdapter dataAdapter1 = new NpgsqlDataAdapter(command1, connection);
            dataSet1.Reset();
            dataAdapter1.Fill(dataTable1);

            connection.Close();


        }

        public void insert_zakaz(string id, string Fio, string adres, string title_tovar, string costs_tovar, string kollvo, string costs)
        {


            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            string command = $"Insert Into Zakaz Values ({id},'{Fio}','{adres}','{title_tovar}',{costs_tovar},{kollvo},{costs})";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command, connection);
            dataSet.Reset();
            dataAdapter.Fill(dataTable);

            connection.Close();


        }





        public void insert_tovar(string id, string title, string price, string articul, string quantity)
        {


            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable();
            string command = $"Insert Into tovar (id, title, prise, articul, quantity) VALUES ({id}, '{title}',{price},'{articul}',{quantity})";
            NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter(command, connection);
            dataSet.Reset();
            dataAdapter.Fill(dataTable);

            connection.Close();


        }



        public void insert_tovar1(string id, string title, string prise, string article, string kolvo)
        {

            DataSet dataSet1 = new DataSet();
            DataTable dataTable1 = new DataTable();
            string command1 = $"Insert Into Tovar Values ({id},'{title}',{prise},{article},{kolvo})";

            NpgsqlDataAdapter dataAdapter1 = new NpgsqlDataAdapter(command1, connection);
            dataSet1.Reset();
            dataAdapter1.Fill(dataTable1);

            connection.Close();


        }

       

    }
}
