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

        Bitmap img1;
        Bitmap img2;
        byte[,] vImg1Gray;

        byte[,] vImg1R;
        byte[,] vImg1G;
        byte[,] vImg1B;
        byte[,] vImg1A;

        bool bLoadImgOK = false;

        public Form1() {
            InitializeComponent();
        }

        private void botaoImportarImagem_Click(object sender, EventArgs e) {
            Boolean imagemCarregada = carregarImagem();

            if (imagemCarregada == true) {
                guia1CaixaImagemImportada.Image = img1;
            }                     
        }

        private void botaoAdicionarBrilho_Click(object sender, EventArgs e) {

            Bitmap imagem = (Bitmap)guia1CaixaImagemImportada.Image;

            // Se a imagem carregou perfeitamente...
            if (bLoadImgOK == true) {
                vImg1Gray = new byte[imagem.Width, imagem.Height];

                // Percorre todos os pixels da imagem...
                for (int i = 0; i < imagem.Width; i++) {
                    for (int j = 0; j < imagem.Height; j++) {
                        Color pixel = imagem.GetPixel(i, j);

                        int valorR = pixel.R + (int)guia1EntradaValorBrilho.Value;
                        int valorG = pixel.G + (int)guia1EntradaValorBrilho.Value;
                        int valorB = pixel.B + (int)guia1EntradaValorBrilho.Value;

                        if (valorR > 255) {
                            valorR = 255;
                        }

                        if (valorG > 255) {
                            valorG = 255;
                        }

                        if (valorB > 255) {
                            valorB = 255;
                        }

                        Color cor = Color.FromArgb(255, valorR, valorG, valorB);

                        img2.SetPixel(i, j, cor);
                    }
                }
                guia1CaixaImagemEditada.Image = img2;
            }
            
        }

        private void botaoRemoverBrilho_Click(object sender, EventArgs e) {

            Bitmap imagem = (Bitmap)guia1CaixaImagemImportada.Image;

            // Se a imagem carregou perfeitamente...
            if (bLoadImgOK == true) {
                vImg1Gray = new byte[imagem.Width, imagem.Height];

                // Percorre todos os pixels da imagem...
                for (int i = 0; i < imagem.Width; i++) {
                    for (int j = 0; j < imagem.Height; j++) {
                        Color pixel = imagem.GetPixel(i, j);

                        int valorR = pixel.R - (int)guia1EntradaValorBrilho.Value;
                        int valorG = pixel.G - (int)guia1EntradaValorBrilho.Value;
                        int valorB = pixel.B - (int)guia1EntradaValorBrilho.Value;

                        if (valorR < 0) {
                            valorR = 0;
                        }

                        if (valorG < 0) {
                            valorG = 0;
                        }

                        if (valorB < 0) {
                            valorB = 0;
                        }

                        Color cor = Color.FromArgb(255, valorR, valorG, valorB);

                        img2.SetPixel(i, j, cor);
                    }
                }
                guia1CaixaImagemEditada.Image = img2;
            }
        }

        private void guia2BotaoImportarImagem1_Click(object sender, EventArgs e) {
            Boolean imagemCarregada = carregarImagem();

            if (imagemCarregada == true) {
                guia2CaixaImagemImportada1.Image = img1;
            }
        }

        private void guia2BotaoImportarImagem2_Click(object sender, EventArgs e) {
            Boolean imagemCarregada = carregarImagem();

            if (imagemCarregada == true) {
                guia2CaixaImagemImportada2.Image = img1;
            }
        }

        private Boolean carregarImagem() {
            // Configurações iniciais da OpenFileDialogBox
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            var filePath = string.Empty;
            openFileDialog1.InitialDirectory = "C:\\Users\\João\\Documents\\Faculdade\\5_semestre\\Processamento de Imagens\\Material Matlab\\Matlab";
            openFileDialog1.Filter = "TIFF image (*.tif)|*.tif|JPG image (*.jpg)|*.jpg|BMP image (*.bmp)|*.bmp|PNG image (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            // Se um arquivo foi localizado com sucesso...
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                // Armnazena o path do arquivo de imagem
                filePath = openFileDialog1.FileName;

                try {
                    img1 = new Bitmap(filePath);
                    img2 = new Bitmap(img1.Width, img1.Height);
                    bLoadImgOK = true;
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message, "Erro ao abrir imagem...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bLoadImgOK = false;
                }
            }
            return bLoadImgOK;
        }

        private void guia2BotaoSoma_Click(object sender, EventArgs e) {
            Bitmap imagem1 = (Bitmap)guia2CaixaImagemImportada1.Image;
            Bitmap imagem2 = (Bitmap)guia2CaixaImagemImportada2.Image;

            if (imagem1.Width == imagem2.Width && imagem1.Height == imagem2.Height) {
                // Percorre todos os pixels da imagem...
                for (int i = 0; i < imagem1.Width; i++) {
                    for (int j = 0; j < imagem1.Height; j++) {
                        Color pixel = imagem1.GetPixel(i, j);
                        Color pixel2 = imagem2.GetPixel(i, j);

                        int valorR = pixel.R + pixel2.R;
                        int valorG = pixel.G + pixel2.G;
                        int valorB = pixel.B + pixel2.B;

                        if (valorR > 255) {
                            valorR = 255;
                        }

                        if (valorG > 255) {
                            valorG = 255;
                        }

                        if (valorB > 255) {
                            valorB = 255;
                        }

                        Color cor = Color.FromArgb(255, valorR, valorG, valorB);

                        img2.SetPixel(i, j, cor);
                    }
                }
                guia2CaixaImagemEditada.Image = img2;
            }
        }

        private void guia2BotaoSubtracao_Click(object sender, EventArgs e) {
            Bitmap imagem1 = (Bitmap)guia2CaixaImagemImportada1.Image;
            Bitmap imagem2 = (Bitmap)guia2CaixaImagemImportada2.Image;

            if (imagem1.Width == imagem2.Width && imagem1.Height == imagem2.Height) {
                // Percorre todos os pixels da imagem...
                for (int i = 0; i < imagem1.Width; i++) {
                    for (int j = 0; j < imagem1.Height; j++) {
                        Color pixel = imagem1.GetPixel(i, j);
                        Color pixel2 = imagem2.GetPixel(i, j);

                        int valorR = pixel.R - pixel2.R;
                        int valorG = pixel.G - pixel2.G;
                        int valorB = pixel.B - pixel2.B;

                        if (valorR < 0) {
                            valorR = 0;
                        }

                        if (valorG < 0) {
                            valorG = 0;
                        }

                        if (valorB < 0) {
                            valorB = 0;
                        }

                        Color cor = Color.FromArgb(255, valorR, valorG, valorB);

                        img2.SetPixel(i, j, cor);
                    }
                }
                guia2CaixaImagemEditada.Image = img2;
            }
        }
    }
}
