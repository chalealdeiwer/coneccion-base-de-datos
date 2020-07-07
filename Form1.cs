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
using NpgsqlTypes;

namespace coneccion_base_de_datos
{
    public partial class Form1 : Form
    {
        NpgsqlConnection conexion = new NpgsqlConnection();
        string cadenaConexion;
        public Form1()
        {
            InitializeComponent();
            cadenaConexion = "Server = 127.0.0.1; Port = 5432 ; Database = primeraconexion; ";
            cadenaConexion += "User Id = postgres;";
            cadenaConexion += "Password = postgres1";
            conexion.ConnectionString = cadenaConexion;
            try
            {
                conexion.Open();
            }
            catch
            {
                MessageBox.Show("Usuario o contraseña invalido");
                conexion.Close();
            }
            if (conexion.State.ToString() == "Open")
            {
                MessageBox.Show("Conexion realizada con exito");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NpgsqlCommand cmd = new NpgsqlCommand();
            string cadenaComando;
            cmd.Connection = conexion;
            cadenaComando = "INSERT INTO public.cliente(codigo,nombre,domicilio,telefono)";
            cadenaComando += "VALUES (@codigo,@nombre,@domicilio,@telefono);";
            cmd.CommandText = cadenaComando;
            cmd.Parameters.Add("@codigo", NpgsqlDbType.Varchar, 13).Value = textBox1.Text;
            cmd.Parameters.Add("@nombre", NpgsqlDbType.Varchar, 50).Value = textBox2.Text;
            cmd.Parameters.Add("@domicilio", NpgsqlDbType.Varchar, 50).Value = textBox3.Text;
            cmd.Parameters.Add("@telefono", NpgsqlDbType.Varchar, 20).Value = textBox4.Text;
            
            int res = cmd.ExecuteNonQuery();
            if (res > 0)
            {
                MessageBox.Show("Datos Ingresados Con exito");
            }

            else
            {
                MessageBox.Show("No se cargaron lo datos a la base de datos");
            }
            cmd.Parameters.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            string sql = ("SELECT * FROM cliente ");
            using (NpgsqlCommand command = new NpgsqlCommand(sql, conexion))
            {
                NpgsqlDataReader reader = command.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    listView1.Items.Add(reader[0].ToString());
                    listView1.Items[i].SubItems.Add(reader[1].ToString());
                    listView1.Items[i].SubItems.Add(reader[2].ToString());
                    listView1.Items[i].SubItems.Add(reader[3].ToString());
                    listView1.Update();
                    i++;
                }
                reader.Close();
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
