using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaMercado
{
    public partial class AltExcProduto : Form
    {
        public AltExcProduto()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void AltExcProduto_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void voltarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        SqlConnection sqlcon = null;
        private string strCon = "Data Source=DESKTOP-D3EIMK3;Initial Catalog=Trabalho;Integrated Security=True";
        private string strsql = string.Empty;

        private void button3_Click(object sender, EventArgs e)
        {
            string strsql = "select * from produtos where idprod=@id";

            sqlcon = new SqlConnection(strCon);

            SqlCommand cmdprod = new SqlCommand(strsql, sqlcon);

            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataReader drprod = cmdprod.ExecuteReader();
                drprod.Read();

                if(!drprod.HasRows)
                {
                    MessageBox.Show("Produto não encontrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    textBox1.Text = drprod["idprod"].ToString();
                    textBox2.Text = drprod["nome"].ToString();
                    textBox4.Text = drprod["preco"].ToString();
                    textBox3.Text = drprod["quant"].ToString();
                    byte[] imagem = (byte[])(drprod["fotos"]);

                    if(imagem == null)
                    {
                        pictureBox1.Image = null;
                    }
                    else
                    {
                        MemoryStream mstream = new MemoryStream(imagem);
                        pictureBox1.Image = System.Drawing.Image.FromStream(mstream);
                    }
                }
            }
            catch (Exception ex) { 
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
        }
    }
}
