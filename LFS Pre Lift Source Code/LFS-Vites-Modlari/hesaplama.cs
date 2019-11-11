using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LFS_Vites_Modlari
{
    public partial class hesaplama : Form
    {
        public int hiz = 0;
        public int vites = 1;
        public hesaplama()
        {
            InitializeComponent();
        }

        public void VitesDegistir(int yeniDeger)
        {
            vites = yeniDeger;
        }
        public void Hizlan(int artis)
        {
            hiz = hiz + artis;
        }
        public void FrenYap(int azalma)
        {
            hiz = hiz - azalma;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            hiz = Convert.ToInt32(textBox1.Text);
            vites = Convert.ToInt32(textBox2.Text);

            label6.Text = "" + hiz;
            label7.Text = "" + vites;

        }
    }
}
