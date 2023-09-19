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

        string imagem;

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
            string strsql = "Delete from produtos where idprod=@idprod";

            sqlcon = new SqlConnection(strCon);

            SqlCommand cmdprod = new SqlCommand(strsql, sqlcon);

            cmdprod.Parameters.Add("@idprod", SqlDbType.Int).Value = int.Parse(textBox5.Text);

            if (MessageBox.Show("Deseja realmente excluir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {

            }

            else
            {
                try
                {
                    sqlcon.Open();
                    cmdprod.ExecuteNonQuery();
                    MessageBox.Show("Dados Excluidos");
                    Limpar();

                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlcon.Close();
                }
                
            }

        }

        private void Limpar()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox5.Focus();
            pictureBox1.Image = null;

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
            string strsql = "select * from produtos where idprod=@idprod";

            sqlcon = new SqlConnection(strCon);

            SqlCommand cmdprod = new SqlCommand(strsql, sqlcon);

            cmdprod.Parameters.Add("@idprod", SqlDbType.Int).Value = int.Parse(textBox5.Text);

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

        private void button1_Click(object sender, EventArgs e)
        {
            string strsql = "Update produtos set nome=@nome,preco=@preco,quant=@quantidade,fotos=@fotos where idprod=@idprod";

            sqlcon = new SqlConnection(strCon);

            SqlCommand cmdprod = new SqlCommand(strsql, sqlcon);

            cmdprod.Parameters.Add("@idprod", SqlDbType.Int).Value = int.Parse(textBox5.Text);

            cmdprod.Parameters.Add("@nome", SqlDbType.VarChar).Value = textBox2.Text;

            cmdprod.Parameters.Add("@preco", SqlDbType.VarChar).Value = textBox4.Text;

            cmdprod.Parameters.Add("@quantidade", SqlDbType.VarChar).Value = textBox3.Text;

            byte[] imagem_byte = null;
            FileStream fs = new FileStream(imagem, FileMode.Open, FileAccess.Read);

            BinaryReader br = new BinaryReader(fs);

            imagem_byte = br.ReadBytes((int)fs.Length);

            cmdprod.Parameters.Add(new SqlParameter("@fotos", imagem_byte));

            if (MessageBox.Show("Deseja realmente alterar?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)==DialogResult.No)
            {

            }

            else
            {
                try
                {
                    sqlcon.Open();
                    cmdprod.ExecuteNonQuery();
                    MessageBox.Show("Dados Alterados");
                    Limpar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    sqlcon.Close();
                }
            }
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
    }
}
