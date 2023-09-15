using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaMercado
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cadastrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CadProduto CadastrarProduto = new CadProduto();
            CadastrarProduto.Show();
        }

        private void alteraçãoExclusãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AltExcProduto AlteracaoExclusao = new AltExcProduto();
            AlteracaoExclusao.Show();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void caixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Caixa FormCaixa = new Caixa();
            FormCaixa.Show();
        }
    }
}
