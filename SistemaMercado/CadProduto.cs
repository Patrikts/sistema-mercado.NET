using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace SistemaMercado
{
    public partial class CadProduto : Form
    {
        public CadProduto()
        {
            InitializeComponent();
        }

        string imagem;

        private void CadProduto_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void voltarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        SqlConnection sqlcon = null;
        private string strcon = "Data Source=DESKTOP-D3EIMK3;Initial Catalog=Trabalho;Integrated Security=True";
        private string strsql = string.Empty;

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] imagem_byte = null;

            FileStream fs = new FileStream(imagem, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fs);

            imagem_byte = br.ReadBytes((int)fs.Length);

            strsql = ("INSERT INTO produtos (idprod,nome,preco,quant,fotos) VALUES (@id,@nome,@preco,@quantidade,@fotos)");

            sqlcon = new SqlConnection(strcon);

            SqlCommand comando = new SqlCommand(strsql, sqlcon);

            comando.Parameters.Add("@id", SqlDbType.VarChar).Value = textBox1.Text;
            comando.Parameters.Add("@nome", SqlDbType.VarChar).Value = textBox2.Text;
            comando.Parameters.Add("@preco", SqlDbType.VarChar).Value = textBox4.Text;
            comando.Parameters.Add("@quantidade", SqlDbType.VarChar).Value = textBox3.Text;
            comando.Parameters.Add(new SqlParameter("@fotos", imagem_byte));

            try
            {
                sqlcon.Open();
                comando.ExecuteNonQuery();
                MessageBox.Show("Dados foram Cadastrados");
                Limpar();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
        }

        private void Limpar()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            pictureBox1.Image = null;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imagem = dialog.FileName.ToString();
                pictureBox1.ImageLocation = imagem;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
