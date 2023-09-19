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

namespace SistemaMercado
{
    public partial class Caixa : Form
    {
        public Caixa()
        {
            InitializeComponent();
        }

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
            dataGridView1.Columns[3].Name = "Quantidade";
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
            }catch(Exception ex)
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
            if(e.KeyCode==Keys.Escape)
            {
                Close();
            }

            else if(e.KeyCode == Keys.Enter)
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
    }
}
