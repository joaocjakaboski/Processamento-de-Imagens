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

// -----------------------------------------------------------------------------------------------------------------------------------------------------------------
// Guia 1
        private void guia1BotaoImportarImagem_Click(object sender, EventArgs e) {
            carregarImagem(guia1CaixaImagemImportada);
            guia1CaixaImagemEditada.Image = guia1CaixaImagemImportada.Image;
        }
        private void guia1BotaoSalvarImagem_Click(object sender, EventArgs e) {
            if (guia1CaixaImagemEditada.Image != null) {
                salvarImagem(guia1CaixaImagemEditada);
            } else {
                MessageBox.Show("Nenhuma imagem disponível para salvar");
            }
        }
       
        private void guia1BotaoReiniciarImagem_Click(object sender, EventArgs e) {
            guia1CaixaImagemEditada.Image = guia1CaixaImagemImportada.Image;
        }

        private void guia1BotaoAdicionarBrilho_Click(object sender, EventArgs e) {

            if (guia1CaixaImagemEditada.Image != null) {
                guia1CaixaImagemEditada.Image = adicionarBrilho(guia1CaixaImagemEditada, (int)guia1EntradaValorBrilho.Value);
            } else if (guia1CaixaImagemImportada.Image != null) {
                guia1CaixaImagemEditada.Image = adicionarBrilho(guia1CaixaImagemImportada, (int)guia1EntradaValorBrilho.Value);
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro!");
            }
        }

        private void guia1BotaoRemoverBrilho_Click(object sender, EventArgs e) {

            if (guia1CaixaImagemEditada.Image != null) {
                guia1CaixaImagemEditada.Image = removerBrilho(guia1CaixaImagemEditada, (int)guia1EntradaValorBrilho.Value);
            } else if (guia1CaixaImagemImportada.Image != null) {
                guia1CaixaImagemEditada.Image = removerBrilho(guia1CaixaImagemImportada, (int)guia1EntradaValorBrilho.Value);
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro!");
            }
        }

        private void guia1BotaoConverterParaCinza_Click(object sender, EventArgs e) {

            if (guia1CaixaImagemEditada.Image != null) {
                guia1CaixaImagemEditada.Image = converterParaEscalaDeCinza(guia1CaixaImagemEditada);
            } else if (guia1CaixaImagemImportada.Image != null) {
                guia1CaixaImagemEditada.Image = converterParaEscalaDeCinza(guia1CaixaImagemImportada);
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro!");
            }
        }

        private void guia1BotaoEspelharHorizontalmente_Click(object sender, EventArgs e) {
            if (guia1CaixaImagemEditada.Image != null) {
                guia1CaixaImagemEditada.Image = espelharHorizontalmente(guia1CaixaImagemEditada);
            } else if (guia1CaixaImagemImportada.Image != null) {
                guia1CaixaImagemEditada.Image = espelharHorizontalmente(guia1CaixaImagemImportada);
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro!");
            }
        }

        private void guia1BotaoEspelharVerticalmente_Click(object sender, EventArgs e) {
            if (guia1CaixaImagemEditada.Image != null) {
                guia1CaixaImagemEditada.Image = espelharVerticalmente(guia1CaixaImagemEditada);
            } else if (guia1CaixaImagemImportada.Image != null) {
                guia1CaixaImagemEditada.Image = espelharVerticalmente(guia1CaixaImagemImportada);
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro!");
            }
        }


        private void guia1BotaoAdicionarContraste_Click(object sender, EventArgs e) {
            if (guia1CaixaImagemEditada.Image != null && guia1EntradaValorContraste.Value > 0) {
                guia1CaixaImagemEditada.Image = adicionarContraste(guia1CaixaImagemEditada, (float)guia1EntradaValorContraste.Value);
            } else if (guia1CaixaImagemImportada.Image != null && guia1EntradaValorContraste.Value > 0) {
                guia1CaixaImagemEditada.Image = adicionarContraste(guia1CaixaImagemImportada, (float)guia1EntradaValorContraste.Value);
            } else {
                if (guia1EntradaValorContraste.Value == 0 && (guia1CaixaImagemImportada.Image != null || guia1CaixaImagemEditada.Image != null)) {
                    MessageBox.Show("O valor do contraste deve ser maior que 0");
                } else { 
                    MessageBox.Show("É necessario importar uma imagem primeiro!");
                }
            }
        }

        private void guia1BotaoRemoverContraste_Click(object sender, EventArgs e) {
            if (guia1CaixaImagemEditada.Image != null && guia1EntradaValorContraste.Value > 0) {
                guia1CaixaImagemEditada.Image = removerContraste(guia1CaixaImagemEditada, (float)guia1EntradaValorContraste.Value);
            } else if (guia1CaixaImagemImportada.Image != null && guia1EntradaValorContraste.Value > 0) {
                guia1CaixaImagemEditada.Image = removerContraste(guia1CaixaImagemImportada, (float)guia1EntradaValorContraste.Value);
            } else {
                if (guia1EntradaValorContraste.Value == 0 && (guia1CaixaImagemImportada.Image != null || guia1CaixaImagemEditada.Image != null)) {
                    MessageBox.Show("O valor do contraste deve ser maior que 0");
                } else {
                    MessageBox.Show("É necessario importar uma imagem primeiro!");
                }
            }
        }

// -----------------------------------------------------------------------------------------------------------------------------------------------------------------
// Guia 2

        private void guia2BotaoImportarImagem1_Click(object sender, EventArgs e) {
            carregarImagem(guia2CaixaImagemImportada1);
        }

        private void guia2BotaoImportarImagem2_Click(object sender, EventArgs e) {
            carregarImagem(guia2CaixaImagemImportada2);
        }
        private void guia2BotaoSalvarImagem_Click(object sender, EventArgs e) {
            salvarImagem(guia2CaixaImagemEditada);
        }

        private void guia2BotaoSoma_Click(object sender, EventArgs e) {
            guia2Label1.Visible = false;
            guia2Label2.Visible = false;
            guia2CaixaImagemC.Visible = false;
            guia2CaixaImagemD.Visible = false;
            guia2CaixaImagemEditada.Image = somarImagens(guia2CaixaImagemImportada1, guia2CaixaImagemImportada2);
        }

        private void guia2BotaoSubtracao_Click(object sender, EventArgs e) {
            guia2Label1.Visible = false;
            guia2Label2.Visible = false;
            guia2CaixaImagemC.Visible = false;
            guia2CaixaImagemD.Visible = false;
            guia2CaixaImagemEditada.Image = subtrairImagens(guia2CaixaImagemImportada1, guia2CaixaImagemImportada2);
        }

        private void guia2BotaoDiferenca_Click(object sender, EventArgs e) {
            guia2CaixaImagemC.Image = subtrairImagens(guia2CaixaImagemImportada1, guia2CaixaImagemImportada2);
            guia2CaixaImagemD.Image = subtrairImagens(guia2CaixaImagemImportada2, guia2CaixaImagemImportada1);
            guia2CaixaImagemEditada.Image = somarImagens(guia2CaixaImagemC, guia2CaixaImagemD);
            guia2Label1.Visible = true;
            guia2Label2.Visible = true;
            guia2CaixaImagemC.Visible = true;
            guia2CaixaImagemD.Visible = true;
        }

// -----------------------------------------------------------------------------------------------------------------------------------------------------------------
// Funções 

        private Bitmap adicionarBrilho(PictureBox imagemEntrada, int valorBrilho) {
            Bitmap imagem = (Bitmap)imagemEntrada.Image;
            Bitmap saida = new Bitmap(imagem.Width, imagem.Height);

            // Se a imagem carregou perfeitamente...
            if (bLoadImgOK == true) {
                
                // Percore todos os pixels da imagem...
                for (int i = 0; i < imagem.Width; i++) {
                    for (int j = 0; j < imagem.Height; j++) {
                        Color pixel = imagem.GetPixel(i, j);

                        int valorR = Math.Min(pixel.R + valorBrilho, 255);
                        int valorG = Math.Min(pixel.G + valorBrilho, 255);
                        int valorB = Math.Min(pixel.B + valorBrilho, 255);

                        Color cor = Color.FromArgb(255, valorR, valorG, valorB);

                        saida.SetPixel(i, j, cor);
                    }
                }
            }
            return saida;
        }

        private Bitmap removerBrilho(PictureBox imagemEntrada, int valorBrilho) {
            Bitmap imagem = (Bitmap)imagemEntrada.Image;
            Bitmap saida = new Bitmap(imagem.Width, imagem.Height);


            // Se a imagem carregou perfeitamente...
            if (bLoadImgOK == true) {

                // Percorre todos os pixels da imagem...
                for (int i = 0; i < imagem.Width; i++) {
                    for (int j = 0; j < imagem.Height; j++) {
                        Color pixel = imagem.GetPixel(i, j);

                        int valorR = Math.Max(pixel.R - valorBrilho, 0);
                        int valorG = Math.Max(pixel.G - valorBrilho, 0);
                        int valorB = Math.Max(pixel.B - valorBrilho, 0);

                        Color cor = Color.FromArgb(255, valorR, valorG, valorB);

                        saida.SetPixel(i, j, cor);
                    }
                }
            }
            return saida;
        }

        private Bitmap adicionarContraste(PictureBox imagemEntrada, float valorContraste) {
            Bitmap imagem = (Bitmap)imagemEntrada.Image;
            Bitmap saida = new Bitmap(imagem.Width, imagem.Height);
            float fatorContraste = 1.0f + (valorContraste / 100f);

            // Se a imagem carregou perfeitamente...
            if (bLoadImgOK == true) {

                // Percore todos os pixels da imagem...
                for (int i = 0; i < imagem.Width; i++) {
                    for (int j = 0; j < imagem.Height; j++) {
                        Color pixel = imagem.GetPixel(i, j);

                        int valorR = (int)Math.Min(pixel.R * fatorContraste, 255);
                        int valorG = (int)Math.Min(pixel.G * fatorContraste, 255);
                        int valorB = (int)Math.Min(pixel.B * fatorContraste, 255);

                        Color cor = Color.FromArgb(255, valorR, valorG, valorB);

                        saida.SetPixel(i, j, cor);
                    }
                }
            }
            return saida;
        }

        private Bitmap removerContraste(PictureBox imagemEntrada, float valorContraste) {
            Bitmap imagem = (Bitmap)imagemEntrada.Image;
            Bitmap saida = new Bitmap(imagem.Width, imagem.Height);
            float fatorContraste = 1.0f / (1.0f + (valorContraste / 100f));

            // Se a imagem carregou perfeitamente...
            if (bLoadImgOK == true) {

                // Percore todos os pixels da imagem...
                for (int i = 0; i < imagem.Width; i++) {
                    for (int j = 0; j < imagem.Height; j++) {
                        Color pixel = imagem.GetPixel(i, j);

                        int valorR = (int)Math.Max(pixel.R * fatorContraste, 0);
                        int valorG = (int)Math.Max(pixel.G * fatorContraste, 0);
                        int valorB = (int)Math.Max(pixel.B * fatorContraste, 0);

                        Color cor = Color.FromArgb(255, valorR, valorG, valorB);

                        saida.SetPixel(i, j, cor);
                    }
                }
            }
            return saida;
        }

        private Bitmap somarImagens(PictureBox imagemEntrada1, PictureBox imagemEntrada2) {
            Bitmap imagem1 = (Bitmap)imagemEntrada1.Image;
            Bitmap imagem2 = (Bitmap)imagemEntrada2.Image;
            Bitmap saida = new Bitmap(imagem1.Width, imagem1.Height);

            if (imagem1.Width == imagem2.Width && imagem1.Height == imagem2.Height) {
                for (int i = 0; i < imagem1.Width; i++) {
                    for (int j = 0; j < imagem1.Height; j++) {
                        Color pixel = imagem1.GetPixel(i, j);
                        Color pixel2 = imagem2.GetPixel(i, j);

                        int valorR = Math.Min(pixel.R + pixel2.R, 255);
                        int valorG = Math.Min(pixel.G + pixel2.G, 255);
                        int valorB = Math.Min(pixel.B + pixel2.B, 255);

                        Color cor = Color.FromArgb(255, valorR, valorG, valorB);

                        saida.SetPixel(i, j, cor);
                    }
                }
            }
            return saida;
        }

        private Bitmap subtrairImagens(PictureBox imagemEntrada1, PictureBox imagemEntrada2) {
            Bitmap imagem1 = (Bitmap)imagemEntrada1.Image;
            Bitmap imagem2 = (Bitmap)imagemEntrada2.Image;
            Bitmap saida = new Bitmap(imagem1.Width, imagem1.Height);

            if (imagem1.Width == imagem2.Width && imagem1.Height == imagem2.Height) {
                for (int i = 0; i < imagem1.Width; i++) {
                    for (int j = 0; j < imagem1.Height; j++) {
                        Color pixel = imagem1.GetPixel(i, j);
                        Color pixel2 = imagem2.GetPixel(i, j);

                        int valorR = Math.Max(pixel.R - pixel2.R, 0);
                        int valorG = Math.Max(pixel.G - pixel2.G, 0);
                        int valorB = Math.Max(pixel.B - pixel2.B, 0);

                        Color cor = Color.FromArgb(255, valorR, valorG, valorB);

                        saida.SetPixel(i, j, cor);
                    }
                }
            }
            return saida;
        }

        private Bitmap converterParaEscalaDeCinza(PictureBox imagemEntrada) {

            Bitmap imagem = (Bitmap)imagemEntrada.Image;
            Bitmap saida = new Bitmap(imagem.Width, imagem.Height);

            // Se a imagem carregou perfeitamente...
            if (bLoadImgOK == true) {
                // Percorre todos os pixels da imagem...
                for (int i = 0; i < imagem.Width; i++) {
                    for (int j = 0; j < imagem.Height; j++) {
                        Color pixel = imagem.GetPixel(i, j);

                        int corCinza = (pixel.R + pixel.G + pixel.B) / 3;

                        Color cor = Color.FromArgb(255, corCinza, corCinza, corCinza);

                        saida.SetPixel(i, j, cor);
                    }
                }
            }
            return saida;
        }

        private Bitmap espelharHorizontalmente(PictureBox imagemEntrada) {

            Bitmap imagem = (Bitmap)imagemEntrada.Image;
            Bitmap saida = new Bitmap(imagem.Width, imagem.Height);

            // Se a imagem carregou perfeitamente...
            if (bLoadImgOK == true) {
                // Percorre todos os pixels da imagem...
                for (int i = 0; i < imagem.Width; i++) {
                    for (int j = 0; j < imagem.Height; j++) {
                        Color pixel = imagem.GetPixel(imagem.Width - 1 - i, j);

                        saida.SetPixel(i, j, pixel);
                    }
                }
            }
            return saida;
        }

        private Bitmap espelharVerticalmente(PictureBox imagemEntrada) {

            Bitmap imagem = (Bitmap)imagemEntrada.Image;
            Bitmap saida = new Bitmap(imagem.Width, imagem.Height);

            // Se a imagem carregou perfeitamente...
            if (bLoadImgOK == true) {
                // Percorre todos os pixels da imagem...
                for (int i = 0; i < imagem.Width; i++) {
                    for (int j = 0; j < imagem.Height; j++) {
                        Color pixel = imagem.GetPixel(i, imagem.Height - 1 - j);

                        saida.SetPixel(i, j, pixel);
                    }
                }
            }
            return saida;
        }


        private void carregarImagem(PictureBox pictureBox) {
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
            if (bLoadImgOK == true) {
                pictureBox.Image = img1;
            }
        }

        private void salvarImagem(PictureBox pictureBox) {
            if (pictureBox.Image != null) {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Salvar Imagem";
                saveFileDialog.Filter = "PNG Image|.png|JPEG Image|.jpg|Bitmap Image|*.bmp";
                saveFileDialog.DefaultExt = "png"; // Define PNG como padrão
                saveFileDialog.FileName = "imagem"; // Nome sugerido

                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    // Obtém o formato de acordo com a extensão escolhida
                    System.Drawing.Imaging.ImageFormat formato = System.Drawing.Imaging.ImageFormat.Png; // Padrão

                    if (saveFileDialog.FileName.EndsWith(".jpg"))
                        formato = System.Drawing.Imaging.ImageFormat.Jpeg;
                    else if (saveFileDialog.FileName.EndsWith(".bmp"))
                        formato = System.Drawing.Imaging.ImageFormat.Bmp;

                    try {
                        pictureBox.Image.Save(saveFileDialog.FileName, formato);
                        MessageBox.Show("Imagem salva com sucesso em:\n" + saveFileDialog.FileName, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    } catch (Exception ex) {
                        MessageBox.Show("Erro ao salvar a imagem:\n" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            } else {
                MessageBox.Show("Nenhuma imagem para salvar!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }      
    }
}