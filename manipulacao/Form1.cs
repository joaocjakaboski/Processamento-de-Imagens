using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace manipulacao {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void botaoImportarImagem_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Imagem|*.png;*.jpg;*.jpeg;*.bmp;*.tif";

            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                string imagePath = openFileDialog.FileName;

                Bitmap imagem = new Bitmap(imagePath);
                CaixaImagemImportada.Image = imagem;
            }
        }
    }
}
