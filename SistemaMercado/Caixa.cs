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
        public SqlConnection SqlCon;
        float TotalVenda = 0;
        int i;
        #endregion

        #region método fechar, sair, limpar, datagrid e gerarcodigo

        private void NomearDatagrid()
        {

        }

        #endregion

        private void Caixa_Load(object sender, EventArgs e)
        {

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
    }
}
