using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaMercado
{
    public partial class Caixa : Form
    {
        public Caixa()
        {
            InitializeComponent();
        }

        SqlConnection sqlcon = null;
        private string strCon = "Data Source=DESKTOP-D3EIMK3;Initial Catalog=Trabalho;Integrated Security=True";
        private string strsql = string.Empty;

        #region variaveis
        public string strcon = "Data Source=DESKTOP-D3EIMK3;Initial Catalog=Trabalho;Integrated Security=True";
        public SqlConnection sqlCon;
        float TotalVenda = 0;
        int i;
        #endregion

        #region método fechar, sair, limpar, datagrid e gerarcodigo

        private void NomearDatagrid()
        {
            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Código";
            dataGridView1.Columns[1].Name = "Produto";
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Name = "Valor Unitário";
            dataGridView1.Columns[3].Name = "Quantidade_";
            dataGridView1.Columns[4].Name = "Total";

        }

        private void GerarCodigo()
        {
            sqlCon = new SqlConnection(strcon);
            string sintaxe = "select max (IdVenda) from Caixa";

            try
            {
                sqlCon.Open();
                SqlCommand cmdcodvenda = new SqlCommand(sintaxe, sqlCon);

                if (cmdcodvenda.ExecuteScalar() == DBNull.Value)
                {
                    label9.Text = "1";
                }
                else
                {
                    Int32 ra = Convert.ToInt32(cmdcodvenda.ExecuteScalar()) + 1;
                    label9.Text = ra.ToString();

                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }

        private void FecharFormulario(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }

            else if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("(TAB)");
            }
        }

        private void Limpar()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            pictureBox1.Image = null;
            textBox1.Focus();
        }


        #endregion

        #region Método de Consultar, gravar venda e itens de venda

        private void ConsultarProduto()
        {
            string strsql = "select * from produtos where idprod=@idprod";

            sqlcon = new SqlConnection(strCon);

            SqlCommand cmdprod = new SqlCommand(strsql, sqlcon);

            cmdprod.Parameters.Add("@idprod", SqlDbType.Int).Value = int.Parse(textBox1.Text);

            try
            {
                if (sqlcon.State == ConnectionState.Closed)
                {
                    sqlcon.Open();
                }

                SqlDataReader drprod = cmdprod.ExecuteReader();
                drprod.Read();

                if (!drprod.HasRows)
                {
                    MessageBox.Show("Produto não encontrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    textBox2.Text = drprod["nome"].ToString();
                    textBox3.Text = drprod["preco"].ToString();
                    byte[] imagem = (byte[])(drprod["fotos"]);

                    if (imagem == null)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlcon.Close();
            }
        }

            private void GravarVenda()
            {
                string sqlvenda = "insert into Caixa(IdVenda,ValorTotal) values @idvenda,@valortotal";
                sqlcon = new SqlConnection(strCon);
                SqlCommand cmdvenda = new SqlCommand(sqlvenda, sqlcon);

                cmdvenda.Parameters.AddWithValue("@idvenda", Convert.ToInt32(label2.Text));
                cmdvenda.Parameters.AddWithValue("@valortotal", float.Parse(textBox5.Text));

                try
                {
                    sqlcon.Open();
                    cmdvenda.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                finally
                {
                    Inserir();
                    dataGridView1.Rows.Clear();
                    TotalVenda = 0;
                    textBox1.Focus();
                    sqlcon.Close(); }
            }

            private void Inserir()
            {
                string sqlItens = "insert into Intermediaria (IdVenda,idprod,qt,ValorT) values (@codvenda,@codprod,@quantidade,@total)";
                sqlcon = new SqlConnection(strCon);

            try
            {
                    SqlCommand cmdinserir = new SqlCommand(sqlItens, sqlcon);

                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        cmdinserir.Parameters.Clear();
                        cmdinserir.Parameters.AddWithValue("@codvenda", label2.Text);
                        cmdinserir.Parameters.AddWithValue("@codprod", dataGridView1.Rows[i].Cells[0].Value);
                        cmdinserir.Parameters.AddWithValue("@quantidade", Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value));
                        cmdinserir.Parameters.AddWithValue("@total", Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value));

                        cmdinserir.ExecuteNonQuery();
                    }
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
        
    
        #endregion

        private void Caixa_Load(object sender, EventArgs e)
        {
            NomearDatagrid();
            GerarCodigo();
        }

        private void alteraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void voltarToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Caixa_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Deseja realmente sair?","Aviso",MessageBoxButtons.YesNo,MessageBoxIcon.Information)==DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);

            TotalVenda += float.Parse(textBox5.Text);

            textBox6.Text = TotalVenda.ToString();
            Limpar();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if(textBox1.Text != string.Empty)
            {
                ConsultarProduto();

            }

            else
            {
                textBox1.Focus();
            }
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            textBox5.Text = (float.Parse(textBox3.Text) * (float.Parse(textBox4.Text))).ToString();

            button1.Focus();
        }
    }
}

