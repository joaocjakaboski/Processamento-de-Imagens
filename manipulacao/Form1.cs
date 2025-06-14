using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

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
            guia1ComboBoxElementoEstruturante.SelectedIndex = 2;
            guia1ComboBoxTamanhoMatriz.SelectedIndex = 0;
        }


        // -----------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Guia 1

        
        private void guia1BotaoImportarImagem_Click(object sender, EventArgs e) {
            esconderGraficos();
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
            esconderGraficos();
            guia1CaixaImagemEditada.Image = guia1CaixaImagemImportada.Image;
        }

        private void guia1BotaoAdicionarBrilho_Click(object sender, EventArgs e) {
            esconderGraficos();
            if (guia1CaixaImagemEditada.Image != null) {
                guia1CaixaImagemEditada.Image = adicionarBrilho(guia1CaixaImagemEditada, (int)guia1EntradaValorBrilho.Value);
            } else if (guia1CaixaImagemImportada.Image != null) {
                guia1CaixaImagemEditada.Image = adicionarBrilho(guia1CaixaImagemImportada, (int)guia1EntradaValorBrilho.Value);
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoRemoverBrilho_Click(object sender, EventArgs e) {
            esconderGraficos();
            if (guia1CaixaImagemEditada.Image != null) {
                guia1CaixaImagemEditada.Image = removerBrilho(guia1CaixaImagemEditada, (int)guia1EntradaValorBrilho.Value);
            } else if (guia1CaixaImagemImportada.Image != null) {
                guia1CaixaImagemEditada.Image = removerBrilho(guia1CaixaImagemImportada, (int)guia1EntradaValorBrilho.Value);
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoConverterParaCinza_Click(object sender, EventArgs e) {
            esconderGraficos();
            if (guia1CaixaImagemEditada.Image != null) {
                guia1CaixaImagemEditada.Image = converterParaEscalaDeCinza(guia1CaixaImagemEditada);
            } else if (guia1CaixaImagemImportada.Image != null) {
                guia1CaixaImagemEditada.Image = converterParaEscalaDeCinza(guia1CaixaImagemImportada);
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoEspelharHorizontalmente_Click(object sender, EventArgs e) {
            esconderGraficos();
            if (guia1CaixaImagemEditada.Image != null) {
                guia1CaixaImagemEditada.Image = espelharHorizontalmente(guia1CaixaImagemEditada);
            } else if (guia1CaixaImagemImportada.Image != null) {
                guia1CaixaImagemEditada.Image = espelharHorizontalmente(guia1CaixaImagemImportada);
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoEspelharVerticalmente_Click(object sender, EventArgs e) {
            esconderGraficos();
            if (guia1CaixaImagemEditada.Image != null) {
                guia1CaixaImagemEditada.Image = espelharVerticalmente(guia1CaixaImagemEditada);
            } else if (guia1CaixaImagemImportada.Image != null) {
                guia1CaixaImagemEditada.Image = espelharVerticalmente(guia1CaixaImagemImportada);
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoAdicionarContraste_Click(object sender, EventArgs e) {
            esconderGraficos();
            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1EntradaValorContraste.Value > 0) {
                    if (guia1CaixaImagemEditada.Image != null) {
                        guia1CaixaImagemEditada.Image = adicionarContraste(guia1CaixaImagemEditada, (float)guia1EntradaValorContraste.Value);
                    } else {
                        guia1CaixaImagemEditada.Image = adicionarContraste(guia1CaixaImagemImportada, (float)guia1EntradaValorContraste.Value);
                    }
                } else {
                    MessageBox.Show("O valor do contraste deve ser maior que 0");
                }
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoRemoverContraste_Click(object sender, EventArgs e) {
            esconderGraficos();
            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1EntradaValorContraste.Value > 0) {
                    if (guia1CaixaImagemEditada.Image != null) {
                        guia1CaixaImagemEditada.Image = removerContraste(guia1CaixaImagemEditada, (float)guia1EntradaValorContraste.Value);
                    } else {
                        guia1CaixaImagemEditada.Image = removerContraste(guia1CaixaImagemImportada, (float)guia1EntradaValorContraste.Value);
                    }
                } else {
                    MessageBox.Show("O valor do contraste deve ser maior que 0");
                }
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoLimiarizar_Click(object sender, EventArgs e) {
            esconderGraficos();
            if (guia1CaixaImagemEditada.Image != null) {
                guia1CaixaImagemEditada.Image = limiarizar(guia1CaixaImagemEditada, (int)guia1EntradaLimiarizacao.Value);
            } else if (guia1CaixaImagemImportada.Image != null) {
                guia1CaixaImagemEditada.Image = limiarizar(guia1CaixaImagemImportada, (int)guia1EntradaLimiarizacao.Value);
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }


        private void guia1BotaoEqualizar_Click(object sender, EventArgs e) {
            esconderGraficos();
            string elementoEstruturante = guia1ComboBoxElementoEstruturante.Text;

            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1CaixaImagemEditada.Image != null) {
                    guia1CaixaImagemEditada.Image = equalizar(guia1CaixaImagemEditada);
                    guia1ChartHistrogramaOriginal.Visible = true;
                    guia1ChartHistrogramaFinal.Visible = true;
                } else {
                    guia1CaixaImagemEditada.Image = equalizar(guia1CaixaImagemImportada);
                    guia1ChartHistrogramaOriginal.Visible = true;
                    guia1ChartHistrogramaFinal.Visible = true;
                }
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoMin_Click(object sender, EventArgs e) {
            esconderGraficos();
            int valorBorda = 0;
            if(guia1RadioButtonDuplicar.Checked) {
                valorBorda = 1;
            } else {
                valorBorda = 0;
            }

            int valorOffSet = int.Parse(guia1ComboBoxTamanhoMatriz.Text);

            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1CaixaImagemEditada.Image != null) {
                    guia1CaixaImagemEditada.Image = realce(guia1CaixaImagemEditada, "MIN", valorBorda, valorOffSet);
                } else {
                    guia1CaixaImagemEditada.Image = realce(guia1CaixaImagemImportada, "MIN", valorBorda, valorOffSet);
                }
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoMean_Click(object sender, EventArgs e) {
            esconderGraficos();
            int valorBorda = 0;
            if (guia1RadioButtonDuplicar.Checked) {
                valorBorda = 1;
            } else {
                valorBorda = 0;
            }

            int valorOffSet = int.Parse(guia1ComboBoxTamanhoMatriz.Text);

            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1CaixaImagemEditada.Image != null) {
                    guia1CaixaImagemEditada.Image = realce(guia1CaixaImagemEditada, "MEAN", valorBorda, valorOffSet);
                } else {
                    guia1CaixaImagemEditada.Image = realce(guia1CaixaImagemImportada, "MEAN", valorBorda, valorOffSet);
                }
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoMax_Click(object sender, EventArgs e) {
            esconderGraficos();
            int valorBorda = 0;
            if (guia1RadioButtonDuplicar.Checked) {
                valorBorda = 1;
            } else {
                valorBorda = 0;
            }

            int valorOffSet = int.Parse(guia1ComboBoxTamanhoMatriz.Text);

            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1CaixaImagemEditada.Image != null) {
                    guia1CaixaImagemEditada.Image = realce(guia1CaixaImagemEditada, "MAX", valorBorda, valorOffSet);
                } else {
                    guia1CaixaImagemEditada.Image = realce(guia1CaixaImagemImportada, "MAX", valorBorda, valorOffSet);
                }
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoMediana_Click(object sender, EventArgs e) {
            esconderGraficos();
            int valorBorda = 0;
            if (guia1RadioButtonDuplicar.Checked) {
                valorBorda = 1;
            } else {
                valorBorda = 0;
            }

            int valorOffSet = int.Parse(guia1ComboBoxTamanhoMatriz.Text);

            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1CaixaImagemEditada.Image != null) {
                    guia1CaixaImagemEditada.Image = realce(guia1CaixaImagemEditada, "MEDIANA", valorBorda, valorOffSet);
                } else {
                    guia1CaixaImagemEditada.Image = realce(guia1CaixaImagemImportada, "MEDIANA", valorBorda, valorOffSet);
                }
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoGaussiano_Click(object sender, EventArgs e) {
            esconderGraficos();
            int valorBorda = 0;
            if (guia1RadioButtonDuplicar.Checked) {
                valorBorda = 1;
            } else {
                valorBorda = 0;
            }

            int valorOffSet = int.Parse(guia1ComboBoxTamanhoMatriz.Text);

            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1NumeroValorGaussiano.Value != 0) {
                    if (guia1CaixaImagemEditada.Image != null) {
                        guia1CaixaImagemEditada.Image = gaussiano(guia1CaixaImagemEditada, valorBorda, valorOffSet, (double)guia1NumeroValorGaussiano.Value);
                    } else {
                        guia1CaixaImagemEditada.Image = gaussiano(guia1CaixaImagemImportada, valorBorda, valorOffSet, (double)guia1NumeroValorGaussiano.Value);
                    }
                } else {
                    MessageBox.Show("É necessário informar o valor do desvio padrão.");
                }
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoOrdem_Click(object sender, EventArgs e) {
            esconderGraficos();
            int valorBorda = 0;
            if (guia1RadioButtonDuplicar.Checked) {
                valorBorda = 1;
            } else {
                valorBorda = 0;
            }

            int valorOffSet = int.Parse(guia1ComboBoxTamanhoMatriz.Text);

            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1CaixaImagemEditada.Image != null) {
                    guia1CaixaImagemEditada.Image = filtragemPorOrdem(guia1CaixaImagemEditada, valorOffSet, (int)guia1NumeroValorIdOrdem.Value, valorBorda);
                } else {
                    guia1CaixaImagemEditada.Image = filtragemPorOrdem(guia1CaixaImagemImportada, valorOffSet, (int)guia1NumeroValorIdOrdem.Value, valorBorda);
                }
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoSuavConservativa_Click(object sender, EventArgs e) {
            esconderGraficos();
            int valorBorda = 0;
            if (guia1RadioButtonDuplicar.Checked) {
                valorBorda = 1;
            } else {
                valorBorda = 0;
            }

            int valorOffSet = int.Parse(guia1ComboBoxTamanhoMatriz.Text);

            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1CaixaImagemEditada.Image != null) {
                    guia1CaixaImagemEditada.Image = suavizacaoConsevativa(guia1CaixaImagemEditada, valorOffSet, valorBorda);
                } else {
                    guia1CaixaImagemEditada.Image = suavizacaoConsevativa(guia1CaixaImagemImportada, valorOffSet, valorBorda);
                }
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1ComboBoxTamanhoMatriz_TextChanged(object sender, EventArgs e) {
            int maximo = int.Parse(guia1ComboBoxTamanhoMatriz.Text);
            maximo = maximo * maximo - 1;
            guia1NumeroValorIdOrdem.Maximum = maximo;
        }

        private void guia1BotaoPrewitt_Click(object sender, EventArgs e) {
            esconderGraficos();
            int valorBorda = 0;
            if (guia1RadioButtonDuplicar2.Checked) {
                valorBorda = 1;
            } else {
                valorBorda = 0;
            }

            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1CaixaImagemEditada.Image != null) {
                    guia1CaixaImagemEditada.Image = prewitt(guia1CaixaImagemEditada, valorBorda);
                } else {
                    guia1CaixaImagemEditada.Image = prewitt(guia1CaixaImagemImportada, valorBorda);
                }
                guia1GroupBoxHorizontal.Visible = true;
                guia1GroupBoxVertical.Visible = true;
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoSobel_Click(object sender, EventArgs e) {
            esconderGraficos();
            int valorBorda = 0;
            if (guia1RadioButtonDuplicar2.Checked) {
                valorBorda = 1;
            } else {
                valorBorda = 0;
            }

            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1CaixaImagemEditada.Image != null) {
                    guia1CaixaImagemEditada.Image = sobel(guia1CaixaImagemEditada, valorBorda);
                } else {
                    guia1CaixaImagemEditada.Image = sobel(guia1CaixaImagemImportada, valorBorda);
                }
                guia1GroupBoxHorizontal.Visible = true;
                guia1GroupBoxVertical.Visible = true;
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoLaplaciano_Click(object sender, EventArgs e) {
            esconderGraficos();
            int valorBorda = 0;
            if (guia1RadioButtonDuplicar2.Checked) {
                valorBorda = 1;
            } else {
                valorBorda = 0;
            }

            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1CaixaImagemEditada.Image != null) {
                    guia1CaixaImagemEditada.Image = laplaciano(guia1CaixaImagemEditada, valorBorda);
                } else {
                    guia1CaixaImagemEditada.Image = laplaciano(guia1CaixaImagemImportada, valorBorda);
                }
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoDilatacao_Click(object sender, EventArgs e) {
            esconderGraficos();
            string elementoEstruturante = guia1ComboBoxElementoEstruturante.Text;

            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1CaixaImagemEditada.Image != null) {
                    guia1CaixaImagemEditada.Image = dilatacao(guia1CaixaImagemEditada, elementoEstruturante);
                } else {
                    guia1CaixaImagemEditada.Image = dilatacao(guia1CaixaImagemImportada, elementoEstruturante);
                }
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }


        private void guia1BotaoErosao_Click(object sender, EventArgs e) {
            esconderGraficos();
            string elementoEstruturante = guia1ComboBoxElementoEstruturante.Text;

            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1CaixaImagemEditada.Image != null) {
                    guia1CaixaImagemEditada.Image = erosao(guia1CaixaImagemEditada, elementoEstruturante);
                } else {
                    guia1CaixaImagemEditada.Image = erosao(guia1CaixaImagemImportada, elementoEstruturante);
                }
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoAbertura_Click(object sender, EventArgs e) {
            esconderGraficos();
            string elementoEstruturante = guia1ComboBoxElementoEstruturante.Text;

            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1CaixaImagemEditada.Image != null) {
                    guia1CaixaImagemEditada.Image = abertura(guia1CaixaImagemEditada, elementoEstruturante);
                } else {
                    guia1CaixaImagemEditada.Image = abertura(guia1CaixaImagemImportada, elementoEstruturante);
                }
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoFechamento_Click(object sender, EventArgs e) {
            esconderGraficos();
            string elementoEstruturante = guia1ComboBoxElementoEstruturante.Text;

            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1CaixaImagemEditada.Image != null) {
                    guia1CaixaImagemEditada.Image = fechamento(guia1CaixaImagemEditada, elementoEstruturante);
                } else {
                    guia1CaixaImagemEditada.Image = fechamento(guia1CaixaImagemImportada, elementoEstruturante);
                }
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }

        private void guia1BotaoContorno_Click(object sender, EventArgs e) {
            esconderGraficos();
            string elementoEstruturante = guia1ComboBoxElementoEstruturante.Text;
            string metodo = "";

            if (guia1RadioButtonDiferenca.Checked) {
                metodo = "diferenca";
            } else {
                metodo = "gradiente";
            }

            if (guia1CaixaImagemImportada.Image != null) {
                if (guia1CaixaImagemEditada.Image != null) {
                    guia1CaixaImagemEditada.Image = contorno(guia1CaixaImagemEditada, elementoEstruturante, metodo);
                } else {
                    guia1CaixaImagemEditada.Image = contorno(guia1CaixaImagemImportada, elementoEstruturante, metodo);
                }
            } else {
                MessageBox.Show("É necessario importar uma imagem primeiro.");
            }
        }


        // -----------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Guia 2

        private void guia2BotaoImportarImagem1_Click(object sender, EventArgs e) {
            guia2Label1.Visible = false;
            guia2Label2.Visible = false;
            guia2CaixaImagemC.Visible = false;
            guia2CaixaImagemD.Visible = false;
            carregarImagem(guia2CaixaImagemImportada1);
            if (guia2CaixaImagemImportada1.Image != null) {
                if (eBinario(guia2CaixaImagemImportada1)) {
                    guia2ComboBoxOperacoesLogicas.Enabled = true;
                    guia2BotaoExecutar.Enabled = true;
                } else {
                    guia2ComboBoxOperacoesLogicas.Enabled = false;
                    guia2BotaoExecutar.Enabled = false;
                }
            }
            if (guia2ComboBoxOperacoesLogicas.Text == "NOT") {
                if (eBinario(guia2CaixaImagemImportada1)) {
                    guia2RadioButtonImg1.Enabled = true;
                }
            } else {
                guia2RadioButtonImg1.Enabled = false;
            }
        }

        private void guia2BotaoImportarImagem2_Click(object sender, EventArgs e) {
            guia2Label1.Visible = false;
            guia2Label2.Visible = false;
            guia2CaixaImagemC.Visible = false;
            guia2CaixaImagemD.Visible = false;
            carregarImagem(guia2CaixaImagemImportada2);
            if (guia2CaixaImagemImportada2.Image != null) {
                if (eBinario(guia2CaixaImagemImportada2)) {
                    guia2ComboBoxOperacoesLogicas.Enabled = true;
                    guia2BotaoExecutar.Enabled = true;
                } else {
                    guia2ComboBoxOperacoesLogicas.Enabled = false;
                    guia2BotaoExecutar.Enabled = false;
                }
            }
            if (guia2ComboBoxOperacoesLogicas.Text == "NOT") {
                if (eBinario(guia2CaixaImagemImportada2)) {
                    guia2RadioButtonImg2.Enabled = true;
                }
            } else {
                guia2RadioButtonImg2.Enabled = false;
            }
        }

        private void guia2BotaoSalvarImagem_Click(object sender, EventArgs e) {
            salvarImagem(guia2CaixaImagemEditada);
        }

        private void guia2BotaoSoma_Click(object sender, EventArgs e) {
            guia2Label1.Visible = false;
            guia2Label2.Visible = false;
            guia2CaixaImagemC.Visible = false;
            guia2CaixaImagemD.Visible = false;
            if (guia2CaixaImagemImportada1.Image != null && guia2CaixaImagemImportada2.Image != null) {
                if (guia2CaixaImagemImportada1.Image.Size == guia2CaixaImagemImportada2.Image.Size) {
                    guia2CaixaImagemEditada.Image = somarImagens(guia2CaixaImagemImportada1, guia2CaixaImagemImportada2);
                } else {
                    MessageBox.Show("As imagens devem ter o mesmo tamanho.");
                }
            } else {
                MessageBox.Show("É necessário importar duas imagens primeiro.");
            }
        }

        private void guia2BotaoSubtracao_Click(object sender, EventArgs e) {
            guia2Label1.Visible = false;
            guia2Label2.Visible = false;
            guia2CaixaImagemC.Visible = false;
            guia2CaixaImagemD.Visible = false;
            if (guia2CaixaImagemImportada1.Image != null && guia2CaixaImagemImportada2.Image != null) {
                if (guia2CaixaImagemImportada1.Image.Size == guia2CaixaImagemImportada2.Image.Size) {
                    guia2CaixaImagemEditada.Image = subtrairImagens(guia2CaixaImagemImportada1, guia2CaixaImagemImportada2);
                } else {
                    MessageBox.Show("As imagens devem ter o mesmo tamanho.");
                }
            } else {
                MessageBox.Show("É necessário importar duas imagens primeiro.");
            }
        }

        private void guia2BotaoDiferenca_Click(object sender, EventArgs e) {
            guia2Label1.Visible = false;
            guia2Label2.Visible = false;
            guia2CaixaImagemC.Visible = false;
            guia2CaixaImagemD.Visible = false;
            if (guia2CaixaImagemImportada1.Image != null && guia2CaixaImagemImportada2.Image != null) {
                if (guia2CaixaImagemImportada1.Image.Size == guia2CaixaImagemImportada2.Image.Size) {
                    guia2CaixaImagemC.Image = subtrairImagens(guia2CaixaImagemImportada1, guia2CaixaImagemImportada2);
                    guia2CaixaImagemD.Image = subtrairImagens(guia2CaixaImagemImportada2, guia2CaixaImagemImportada1);
                    guia2CaixaImagemEditada.Image = somarImagens(guia2CaixaImagemC, guia2CaixaImagemD);
                    guia2Label1.Visible = true;
                    guia2Label2.Visible = true;
                    guia2CaixaImagemC.Visible = true;
                    guia2CaixaImagemD.Visible = true;
                } else {
                    MessageBox.Show("As imagens devem ter o mesmo tamanho.");
                }
            } else {
                MessageBox.Show("É necessário importar duas imagens primeiro.");
            }
        }

        private void guia2ComboBoxOperacoesLogicas_TextChanged(object sender, EventArgs e) {
            guia2Label1.Visible = false;
            guia2Label2.Visible = false;
            guia2CaixaImagemC.Visible = false;
            guia2CaixaImagemD.Visible = false;
            if (guia2ComboBoxOperacoesLogicas.Text == "NOT") {
                if (guia2CaixaImagemImportada1.Image != null) {
                    if (eBinario(guia2CaixaImagemImportada1)) {
                        guia2RadioButtonImg1.Enabled = true;
                    }
                }
                if (guia2CaixaImagemImportada2.Image != null) {
                    if (eBinario(guia2CaixaImagemImportada2)) {
                        guia2RadioButtonImg2.Enabled = true;
                    }
                }
            } else {
                guia2RadioButtonImg1.Enabled = false;
                guia2RadioButtonImg2.Enabled = false;
            }
        }

        private void guia2BotaoExecutar_Click(object sender, EventArgs e) {
            guia2CaixaImagemC.Visible = false;
            guia2CaixaImagemD.Visible = false;
            guia2Label1.Visible = false;
            guia2Label2.Visible = false;
            if (guia2ComboBoxOperacoesLogicas.Text != "") {
                String operacao = guia2ComboBoxOperacoesLogicas.Text;
                int idImagem = 0;
                if (operacao != "NOT") {
                    if (guia2CaixaImagemImportada1.Image != null && guia2CaixaImagemImportada2.Image != null) {
                        if (guia2CaixaImagemImportada1.Image.Size == guia2CaixaImagemImportada2.Image.Size) {
                            if (eBinario(guia2CaixaImagemImportada1) && eBinario(guia2CaixaImagemImportada2)) {
                                guia2CaixaImagemEditada.Image = operacaoLogica(guia2CaixaImagemImportada1, guia2CaixaImagemImportada2, operacao, idImagem);
                            } else {
                                MessageBox.Show("As imagens devem ser binárias para realizar operações lógicas.");
                            }
                        } else {
                            MessageBox.Show("As imagens devem ter o mesmo tamanho.");
                        }
                    } else {
                        MessageBox.Show("É necessário importar duas imagens primeiro.");
                    }
                } else {
                    if (guia2RadioButtonImg1.Checked) {
                        idImagem = 1;
                        if (guia2CaixaImagemImportada1.Image == null) {
                            MessageBox.Show("Imagem 1 está vazia.");
                        } else {
                            guia2CaixaImagemEditada.Image = operacaoLogica(guia2CaixaImagemImportada1, guia2CaixaImagemImportada2, operacao, idImagem);
                        }
                    } else if (guia2RadioButtonImg2.Checked) {
                        idImagem = 2;
                        if (guia2CaixaImagemImportada2.Image == null) {
                            MessageBox.Show("Imagem 2 está vazia.");
                        } else {
                            guia2CaixaImagemEditada.Image = operacaoLogica(guia2CaixaImagemImportada1, guia2CaixaImagemImportada2, operacao, idImagem);
                        }
                    } else {
                        MessageBox.Show("Selecione uma imagem para a operação NOT.");
                    }
                }
            } else {
                MessageBox.Show("Selecione uma operação lógica.");
            }
        }

        private void guia2BotaoBlending_Click(object sender, EventArgs e) {
            guia2Label1.Visible = false;
            guia2Label2.Visible = false;
            guia2CaixaImagemC.Visible = false;
            guia2CaixaImagemD.Visible = false;
            if (guia2CaixaImagemImportada1.Image != null && guia2CaixaImagemImportada2.Image != null) {
                if (guia2CaixaImagemImportada1.Image.Size == guia2CaixaImagemImportada2.Image.Size) {
                    if (guia2EntradaBlending.Value > 0) {
                        guia2CaixaImagemEditada.Image = combinacaoLinearBlending(guia2CaixaImagemImportada1, guia2CaixaImagemImportada2, (float)guia2EntradaBlending.Value);
                    } else {
                        MessageBox.Show("O valor de entrada deve ser maior que 0.");
                    }
                } else {
                    MessageBox.Show("As imagens devem ter o mesmo tamanho;");
                }
            } else {
                MessageBox.Show("É necessário importar duas imagens primeiro.");
            }
        }

        private void guia2BotaoMedia_Click(object sender, EventArgs e) {
            guia2Label1.Visible = false;
            guia2Label2.Visible = false;
            guia2CaixaImagemC.Visible = false;
            guia2CaixaImagemD.Visible = false;
            if (guia2CaixaImagemImportada1.Image != null && guia2CaixaImagemImportada2.Image != null) {
                if (guia2CaixaImagemImportada1.Image.Size == guia2CaixaImagemImportada2.Image.Size) {
                    guia2CaixaImagemEditada.Image = combinacaoLinearMedia(guia2CaixaImagemImportada1, guia2CaixaImagemImportada2);
                } else {
                    MessageBox.Show("As imagens devem ter o mesmo tamanho;");
                }
            } else {
                MessageBox.Show("É necessário importar duas imagens primeiro.");
            }
        }

        // -----------------------------------------------------------------------------------------------------------------------------------------------------------------
        // Funções 

        private void esconderGraficos() {
            guia1ChartHistrogramaOriginal.Visible = false;
            guia1ChartHistrogramaFinal.Visible = false;
            guia1GroupBoxVertical.Visible = false;
            guia1GroupBoxHorizontal.Visible = false;
        }

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

            return saida;
        }

        private Bitmap subtrairImagens(PictureBox imagemEntrada1, PictureBox imagemEntrada2) {
            Bitmap imagem1 = (Bitmap)imagemEntrada1.Image;
            Bitmap imagem2 = (Bitmap)imagemEntrada2.Image;
            Bitmap saida = new Bitmap(imagem1.Width, imagem1.Height);

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

        private Bitmap combinacaoLinearBlending(PictureBox imagemEntrada1, PictureBox imagemEntrada2, float entradaBlending) {
            Bitmap imagem1 = (Bitmap)imagemEntrada1.Image;
            Bitmap imagem2 = (Bitmap)imagemEntrada2.Image;
            Bitmap saida = new Bitmap(imagem1.Width, imagem1.Height);
            float taxaImagem1 = entradaBlending / 100;
            float taxaImagem2 = 1 - taxaImagem1;

            for (int i = 0; i < imagem1.Width; i++) {
                for (int j = 0; j < imagem2.Width; j++) {
                    Color pixel1 = imagem1.GetPixel(i, j);
                    Color pixel2 = imagem2.GetPixel(i, j);

                    int valorR = Math.Min((int)((taxaImagem1 * pixel1.R) + (taxaImagem2 * pixel2.R)), 255);
                    int valorG = Math.Min((int)((taxaImagem1 * pixel1.G) + (taxaImagem2 * pixel2.G)), 255);
                    int valorB = Math.Min((int)((taxaImagem1 * pixel1.B) + (taxaImagem2 * pixel2.B)), 255);

                    Color cor = Color.FromArgb(255, valorR, valorG, valorB);

                    saida.SetPixel(i, j, cor);
                }
            }
            return saida;
        }

        private Bitmap combinacaoLinearMedia(PictureBox imagemEntrada1, PictureBox imagemEntrada2) {
            Bitmap imagem1 = (Bitmap)imagemEntrada1.Image;
            Bitmap imagem2 = (Bitmap)imagemEntrada2.Image;
            Bitmap saida = new Bitmap(imagem1.Width, imagem1.Height);

            for (int i = 0; i < imagem1.Width; i++) {
                for (int j = 0; j < imagem2.Width; j++) {
                    Color pixel1 = imagem1.GetPixel(i, j);
                    Color pixel2 = imagem2.GetPixel(i, j);

                    int valorR = (pixel1.R + pixel2.R) / 2;
                    int valorG = (pixel1.G + pixel2.G) / 2;
                    int valorB = (pixel1.B + pixel2.B) / 2;

                    Color cor = Color.FromArgb(255, valorR, valorG, valorB);

                    saida.SetPixel(i, j, cor);
                }
            }
            return saida;
        }

        private Bitmap limiarizar(PictureBox imagemEntrada, int valorLimiar) {
            Bitmap imagem = (Bitmap)imagemEntrada.Image;
            Bitmap saida = new Bitmap(imagem.Width, imagem.Height);

            // Se a imagem carregou perfeitamente...
            if (bLoadImgOK == true) {
                // Percorre todos os pixels da imagem...
                for (int i = 0; i < imagem.Width; i++) {
                    for (int j = 0; j < imagem.Height; j++) {
                        Color pixel = imagem.GetPixel(i, j);

                        int corCinza = (pixel.R + pixel.G + pixel.B) / 3;

                        if (corCinza >= valorLimiar) {
                            corCinza = 255;
                        } else {
                            corCinza = 0;
                        }

                        Color cor = Color.FromArgb(255, corCinza, corCinza, corCinza);

                        saida.SetPixel(i, j, cor);
                    }
                }
            }
            return saida;
        }

        private Bitmap operacaoLogica(PictureBox imagemEntrada1, PictureBox imagemEntrada2, String operacao, int idImagem) {
            Bitmap imagem1 = (Bitmap)imagemEntrada1.Image;
            Bitmap imagem2 = (Bitmap)imagemEntrada2.Image;

            Bitmap saida = new Bitmap(imagem1.Width, imagem1.Height);

            for (int i = 0; i < imagem1.Width; i++) {
                for (int j = 0; j < imagem1.Height; j++) {
                    Color pixel1 = Color.Empty;
                    Color pixel2 = Color.Empty;

                    switch (operacao) {
                        case "AND":
                        pixel1 = imagem1.GetPixel(i, j);
                        pixel2 = imagem2.GetPixel(i, j);
                        int varCorAnd = (pixel1.R == 255 && pixel2.R == 255) ? 255 : 0;
                        Color corAnd = Color.FromArgb(255, varCorAnd, varCorAnd, varCorAnd);
                        saida.SetPixel(i, j, corAnd);
                        break;
                        case "OR":
                        pixel1 = imagem1.GetPixel(i, j);
                        pixel2 = imagem2.GetPixel(i, j);
                        int varCorOr = (pixel1.R == 255 || pixel2.R == 255) ? 255 : 0;
                        Color corOr = Color.FromArgb(255, varCorOr, varCorOr, varCorOr);
                        saida.SetPixel(i, j, corOr);
                        break;
                        case "XOR":
                        pixel1 = imagem1.GetPixel(i, j);
                        pixel2 = imagem2.GetPixel(i, j);
                        int varCorXor = (pixel1.R != pixel2.R) ? 255 : 0;
                        Color corXor = Color.FromArgb(255, varCorXor, varCorXor, varCorXor);
                        saida.SetPixel(i, j, corXor);
                        break;
                        case "NOT":
                        if (idImagem == 1) {
                            pixel1 = imagem1.GetPixel(i, j);
                            int valor = (pixel1.R == 255) ? 0 : 255;
                            Color cor = Color.FromArgb(255, valor, valor, valor);
                            saida.SetPixel(i, j, cor);
                        } else if (idImagem == 2) {
                            pixel2 = imagem2.GetPixel(i, j);
                            int valor = (pixel2.R == 255) ? 0 : 255;
                            Color cor = Color.FromArgb(255, valor, valor, valor);
                            saida.SetPixel(i, j, cor);
                        }
                        break;
                    }
                }
            }
            return saida;
        }

        private Boolean eBinario(PictureBox imagemEntrada) {
            Bitmap imagem = (Bitmap)imagemEntrada.Image;

            for (int i = 0; i < imagem.Width; i++) {
                for (int j = 0; j < imagem.Height; j++) {
                    Color pixel = imagem.GetPixel(i, j);
                    if (pixel.R != 0 && pixel.R != 255) {
                        return false;
                    }
                }
            }
            return true;
        }

        private Bitmap equalizar(PictureBox imagemEntrada) {
            Bitmap imagem = (Bitmap)imagemEntrada.Image;
            Bitmap saida = new Bitmap(imagem.Width, imagem.Height);
            double[] histogramaOriginal = new double[256];
            double[] cfd = new double[256];
            int[] histogramaFinal = new int[256];

            for (int i = 0; i < imagem.Width; i++) {
                for (int j = 0; j < imagem.Height; j++) {
                    Color pixel1 = imagem.GetPixel(i, j);
                    histogramaOriginal[pixel1.R] += 1;
                }
            }

            cfd[0] = histogramaOriginal[0];

            for (int i = 1; i < 256; i++) {
                cfd[i] = histogramaOriginal[i] + cfd[i - 1];
            }

            for (int i = 0; i < imagem.Width; i++) {
                for (int j = 0; j < imagem.Height; j++) {
                    Color pixel1 = imagem.GetPixel(i, j);

                    int varCor = (int)Math.Floor(((cfd[pixel1.R] - cfd[0]) / (imagem.Width * imagem.Height - cfd[0])) * 255);
                    histogramaFinal[varCor] += 1;
                    saida.SetPixel(i, j, Color.FromArgb(varCor, varCor, varCor));

                }
            }

            guia1ChartHistrogramaOriginal.Series.Clear();
            Series serie = new Series("Histograma Original");
            serie.ChartType = SeriesChartType.Column;
            for (int i = 0; i < histogramaOriginal.Length; i++) {
                serie.Points.AddXY(i + 1, histogramaOriginal[i]);
            }
            guia1ChartHistrogramaOriginal.Series.Add(serie);

            guia1ChartHistrogramaFinal.Series.Clear();
            serie = new Series("Histograma Original");
            serie.ChartType = SeriesChartType.Column;
            for (int i = 0; i < histogramaFinal.Length; i++) {
                serie.Points.AddXY(i + 1, histogramaFinal[i]);
            }
            guia1ChartHistrogramaFinal.Series.Add(serie);

            return saida;
        }

        private Bitmap gaussiano(PictureBox imagemEntrada, int borda, int offSet, double sigma) {
            
            Bitmap imagemOriginal = (Bitmap)imagemEntrada.Image;
            offSet = offSet / 2;

            Bitmap saida = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);

            if (borda ==0) {
                double[,] kernel = new double[offSet * 2 + 1, offSet * 2 + 1];
                double somaKernel = 0.0;

                // Atribuir valores ao kernel
                for (int x = -offSet; x <= offSet; x++) {
                    for (int y = -offSet; y <= offSet; y++) {
                        double valor = (1.0 / (2.0 * Math.PI * Math.Pow(sigma, 2))) *
                            Math.Exp(-(Math.Pow(x, 2) + Math.Pow(y, 2)) / (2.0 * Math.Pow(sigma, 2)));

                        kernel[x + offSet, y + offSet] = valor;
                        somaKernel += valor;
                    }
                }

                // Normalizar o kernel
                for (int x = 0; x < kernel.GetLength(0); x++) {
                    for (int y = 0; y < kernel.GetLength(1); y++) {
                        kernel[x, y] /= somaKernel;
                    }
                }

                // Aplicar o filtro gaussiano
                for (int i = offSet; i < imagemOriginal.Width - offSet; i++) {
                    for (int j = offSet; j < imagemOriginal.Height - offSet; j++) {
                        double somaR = 0.0, somaG = 0.0, somaB = 0.0;

                        for (int x = -offSet; x <= offSet; x++) {
                            for (int y = -offSet; y <= offSet; y++) {
                                Color p = imagemOriginal.GetPixel(i + x, j + y);
                                somaR += p.R * kernel[x + offSet, y + offSet];
                                somaG += p.G * kernel[x + offSet, y + offSet];
                                somaB += p.B * kernel[x + offSet, y + offSet];

                            }
                        }

                        // Limitar os valores de somaR, somaG e somaB entre 0 e 255
                        somaR = Math.Max(0, Math.Min(255, somaR));
                        somaG = Math.Max(0, Math.Min(255, somaG));
                        somaB = Math.Max(0, Math.Min(255, somaB));
                        saida.SetPixel(i, j, Color.FromArgb(255, (int)somaR, (int)somaG, (int)somaB));
                    }
                }

            } else {
                Bitmap imagem = expandirImagemComBorda(imagemOriginal, offSet);

                double[,] kernel = new double[offSet * 2 + 1, offSet * 2 + 1];
                double somaKernel = 0.0;
                double[,] imagemKernel = new double[offSet * 2 + 1, offSet * 2 + 1];

                // Atribuir valores ao kernel
                for (int x = -offSet; x <= offSet; x++) {
                    for (int y = -offSet; y <= offSet; y++) {
                        double valor = (1.0 / (2.0 * Math.PI * Math.Pow(sigma, 2))) *
                            Math.Exp(-(Math.Pow(x, 2) + Math.Pow(y, 2)) / (2.0 * Math.Pow(sigma, 2)));

                        kernel[x + offSet, y + offSet] = valor;
                        somaKernel += valor;
                    }
                }


                // Normalizar o kernel
                for (int x = 0; x < kernel.GetLength(0); x++) {
                    for (int y = 0; y < kernel.GetLength(1); y++) {
                        kernel[x, y] /= somaKernel;
                    }
                }

                // Criar imagem do kernel
                for (int x = 0; x < kernel.GetLength(0); x++) {
                    for (int y = 0; y < kernel.GetLength(1); y++) {
                        int valor = (int)(kernel[x, y] * 255);
                        valor = Math.Max(0, Math.Min(255, valor));
                        imagemKernel[x, y] = valor;
                    }
                }

                salvarKernelComoImagem(imagemKernel);

                // Aplicar o filtro gaussiano
                for (int i = 0; i < imagemOriginal.Width; i++) {
                    for (int j = 0; j < imagemOriginal.Height; j++) {
                        double somaR = 0.0, somaG = 0.0, somaB = 0.0;

                        for (int x = -offSet; x <= offSet; x++) {
                            for (int y = -offSet; y <= offSet; y++) {
                                Color p = imagem.GetPixel(i + x + offSet, j + y + offSet);
                                somaR += p.R * kernel[x + offSet, y + offSet];
                                somaG += p.G * kernel[x + offSet, y + offSet];
                                somaB += p.B * kernel[x + offSet, y + offSet];

                            }
                        }

                        // Limitar os valores de somaR, somaG e somaB entre 0 e 255
                        somaR = Math.Max(0, Math.Min(255, somaR));
                        somaG = Math.Max(0, Math.Min(255, somaG));
                        somaB = Math.Max(0, Math.Min(255, somaB));
                        saida.SetPixel(i, j, Color.FromArgb(255, (int)somaR, (int)somaG, (int)somaB));
                    }
                }

                
            }

            return saida;
        }

        private Bitmap realce(PictureBox imagemEntrada, String operacao, int borda, int offSet) {
            Bitmap imagemOriginal = (Bitmap)imagemEntrada.Image;
            offSet = offSet / 2;

            Bitmap saida = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);

            if (borda == 0) {
                // SEM TRATAMENTO DE BORDA: aplica filtro somente na área segura
                for (int i = offSet; i < imagemOriginal.Width - offSet; i++) {
                    for (int j = offSet; j < imagemOriginal.Height - offSet; j++) {
                        switch (operacao) {
                            case "MAX":
                                int maxR = 0, maxG = 0, maxB = 0;
                                for (int x = -offSet; x <= offSet; x++) {
                                    for (int y = -offSet; y <= offSet; y++) {
                                        Color p = imagemOriginal.GetPixel(i + x, j + y);
                                        maxR = Math.Max(maxR, p.R);
                                        maxG = Math.Max(maxG, p.G);
                                        maxB = Math.Max(maxB, p.B);
                                    }
                                }
                                saida.SetPixel(i, j, Color.FromArgb(255, maxR, maxG, maxB));
                            break;

                            case "MIN":
                                int minR = 255, minG = 255, minB = 255;
                                for (int x = -offSet; x <= offSet; x++) {
                                    for (int y = -offSet; y <= offSet; y++) {
                                        Color p = imagemOriginal.GetPixel(i + x, j + y);
                                        minR = Math.Min(minR, p.R);
                                        minG = Math.Min(minG, p.G);
                                        minB = Math.Min(minB, p.B);
                                    }
                                }
                                saida.SetPixel(i, j, Color.FromArgb(255, minR, minG, minB));
                            break;

                            case "MEAN":
                                int somaR = 0, somaG = 0, somaB = 0;
                                for (int x = -offSet; x <= offSet; x++) {
                                    for (int y = -offSet; y <= offSet; y++) {
                                        Color p = imagemOriginal.GetPixel(i + x, j + y);
                                        somaR += p.R;
                                        somaG += p.G;
                                        somaB += p.B;
                                    }
                                }
                                int area = (2 * offSet + 1) * (2 * offSet + 1);
                                saida.SetPixel(i, j, Color.FromArgb(255, somaR / area, somaG / area, somaB / area));
                            break;

                            case "MEDIANA":
                                int tamanhoJanela = (2 * offSet + 1) * (2 * offSet + 1);
                                int[] vizinhosR = new int[tamanhoJanela];
                                int[] vizinhosG = new int[tamanhoJanela];
                                int[] vizinhosB = new int[tamanhoJanela];

                                int indice = 0;
                                for (int a = -offSet; a <= offSet; a++) {
                                    for (int b = -offSet; b <= offSet; b++) {
                                        Color p = imagemOriginal.GetPixel(i + a, j + b);
                                        vizinhosR[indice] = p.R;
                                        vizinhosG[indice] = p.G;
                                        vizinhosB[indice] = p.B;
                                        indice++;
                                    }
                                }

                                Array.Sort(vizinhosR);
                                Array.Sort(vizinhosG);
                                Array.Sort(vizinhosB);
                                int meio = vizinhosR.Length / 2;
                                saida.SetPixel(i, j, Color.FromArgb(255, vizinhosR[meio], vizinhosG[meio], vizinhosB[meio]));
                            break;
                        }
                    }
                }
            } else {
                // COM TRATAMENTO DE BORDA: expande a imagem
                Bitmap imagem = expandirImagemComBorda(imagemOriginal, offSet);

                for (int i = 0; i < imagemOriginal.Width; i++) {
                    for (int j = 0; j < imagemOriginal.Height; j++) {
                        switch (operacao) {
                            case "MAX":
                                int maxR = 0, maxG = 0, maxB = 0;
                                for (int x = -offSet; x <= offSet; x++) {
                                    for (int y = -offSet; y <= offSet; y++) {
                                        Color p = imagem.GetPixel(i + x + offSet, j + y + offSet);
                                        maxR = Math.Max(maxR, p.R);
                                        maxG = Math.Max(maxG, p.G);
                                        maxB = Math.Max(maxB, p.B);
                                    }
                                }
                                saida.SetPixel(i, j, Color.FromArgb(255, maxR, maxG, maxB));
                            break;

                            case "MIN":
                                int minR = 255, minG = 255, minB = 255;
                                for (int x = -offSet; x <= offSet; x++) {
                                    for (int y = -offSet; y <= offSet; y++) {
                                        Color p = imagem.GetPixel(i + x + offSet, j + y + offSet);
                                        minR = Math.Min(minR, p.R);
                                        minG = Math.Min(minG, p.G);
                                        minB = Math.Min(minB, p.B);
                                    }
                                }
                                saida.SetPixel(i, j, Color.FromArgb(255, minR, minG, minB));
                            break;

                            case "MEAN":
                                int somaR = 0, somaG = 0, somaB = 0;
                                for (int x = -offSet; x <= offSet; x++) {
                                    for (int y = -offSet; y <= offSet; y++) {
                                        Color p = imagem.GetPixel(i + x + offSet, j + y + offSet);
                                        somaR += p.R;
                                        somaG += p.G;
                                        somaB += p.B;
                                    }
                                }
                                int area = (2 * offSet + 1) * (2 * offSet + 1);
                                saida.SetPixel(i, j, Color.FromArgb(255, somaR / area, somaG / area, somaB / area));
                            break;

                            case "MEDIANA":
                                int tamanhoJanela = (2 * offSet + 1) * (2 * offSet + 1);
                                int[] vizinhosR = new int[tamanhoJanela];
                                int[] vizinhosG = new int[tamanhoJanela];
                                int[] vizinhosB = new int[tamanhoJanela];

                                int indice = 0;
                                for (int a = -offSet; a <= offSet; a++) {
                                    for (int b = -offSet; b <= offSet; b++) {
                                        Color p = imagem.GetPixel(i + a + offSet, j + b + offSet);
                                        vizinhosR[indice] = p.R;
                                        vizinhosG[indice] = p.G;
                                        vizinhosB[indice] = p.B;
                                        indice++;
                                    }
                                }

                                Array.Sort(vizinhosR);
                                Array.Sort(vizinhosG);
                                Array.Sort(vizinhosB);
                                int meio = vizinhosR.Length / 2;
                                saida.SetPixel(i, j, Color.FromArgb(255, vizinhosR[meio], vizinhosG[meio], vizinhosB[meio]));
                            break;
                        }
                    }
                }
            }

            return saida;
        }

        private Bitmap filtragemPorOrdem(PictureBox imagemEntrada, int offSet, int idDaOrdem, int borda) {
            Bitmap imagemOriginal = (Bitmap)imagemEntrada.Image;
            Bitmap saida = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);
            offSet = offSet / 2;

            if (borda == 0) {
                for (int i = offSet; i < imagemOriginal.Width - offSet; i++) {
                    for (int j = offSet; j < imagemOriginal.Height - offSet; j++) {
                        int tamanhoJanela = (2 * offSet + 1) * (2 * offSet + 1);
                        int[] vizinhosR = new int[tamanhoJanela];
                        int[] vizinhosG = new int[tamanhoJanela];
                        int[] vizinhosB = new int[tamanhoJanela];

                        int indice = 0;
                        for (int a = -offSet; a <= offSet; a++) {
                            for (int b = -offSet; b <= offSet; b++) {
                                Color p = imagemOriginal.GetPixel(i + a, j + b);
                                vizinhosR[indice] = p.R;
                                vizinhosG[indice] = p.G;
                                vizinhosB[indice] = p.B;
                                indice++;
                            }
                        }

                        Array.Sort(vizinhosR);
                        Array.Sort(vizinhosG);
                        Array.Sort(vizinhosB);

                        saida.SetPixel(i, j, Color.FromArgb(255, vizinhosR[idDaOrdem], vizinhosG[idDaOrdem], vizinhosB[idDaOrdem]));
                    }
                }
            } else {
                // COM TRATAMENTO DE BORDA: expande a imagem
                Bitmap imagem = expandirImagemComBorda(imagemOriginal, offSet);

                for (int i = 0; i < imagemOriginal.Width; i++) {
                    for (int j = 0; j < imagemOriginal.Height; j++) {
                        int tamanhoJanela = (2 * offSet + 1) * (2 * offSet + 1);
                        int[] vizinhosR = new int[tamanhoJanela];
                        int[] vizinhosG = new int[tamanhoJanela];
                        int[] vizinhosB = new int[tamanhoJanela];

                        int indice = 0;
                        for (int a = -offSet; a <= offSet; a++) {
                            for (int b = -offSet; b <= offSet; b++) {
                                Color p = imagem.GetPixel(i + a + offSet, j + b + offSet);
                                vizinhosR[indice] = p.R;
                                vizinhosG[indice] = p.G;
                                vizinhosB[indice] = p.B;
                                indice++;
                            }
                        }

                        Array.Sort(vizinhosR);
                        Array.Sort(vizinhosG);
                        Array.Sort(vizinhosB);

                        saida.SetPixel(i, j, Color.FromArgb(255, vizinhosR[idDaOrdem], vizinhosG[idDaOrdem], vizinhosB[idDaOrdem]));
                    }
                }
            }

            return saida;
        }


        private Bitmap suavizacaoConsevativa(PictureBox imagemEntrada, int offSet, int borda) {
            Bitmap imagemOriginal = (Bitmap)imagemEntrada.Image;
            Bitmap saida = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);
            offSet = offSet / 2;

            if (borda == 0) {
                for (int i = offSet; i < imagemOriginal.Width - offSet; i++) {
                    for (int j = offSet; j < imagemOriginal.Height - offSet; j++) {
                        int tamanhoJanela = (2 * offSet + 1) * (2 * offSet + 1);
                        int[] vizinhosR = new int[tamanhoJanela];
                        int[] vizinhosG = new int[tamanhoJanela];
                        int[] vizinhosB = new int[tamanhoJanela];

                        int indice = 0;
                        for (int a = -offSet; a <= offSet; a++) {
                            for (int b = -offSet; b <= offSet; b++) {
                                Color p = imagemOriginal.GetPixel(i + a, j + b);
                                vizinhosR[indice] = p.R;
                                vizinhosG[indice] = p.G;
                                vizinhosB[indice] = p.B;
                                indice++;
                            }
                        }

                        Array.Sort(vizinhosR);
                        int minR = vizinhosR[0];
                        int maxR = vizinhosR[vizinhosR.Length-1];

                        Array.Sort(vizinhosG);
                        int minG = vizinhosG[0];
                        int maxG = vizinhosG[vizinhosG.Length - 1];
                        
                        Array.Sort(vizinhosB);
                        int minB = vizinhosB[0];
                        int maxB = vizinhosB[vizinhosB.Length - 1];


                        if (vizinhosR[offSet] < minR) {
                            vizinhosR[offSet] = minR;
                        } else if (vizinhosR[offSet] > maxR) {
                            vizinhosR[offSet] = maxR;
                        }

                        if (vizinhosG[offSet] < minG) {
                            vizinhosG[offSet] = minG;
                        } else if (vizinhosG[offSet] > maxG) {
                            vizinhosG[offSet] = maxG;
                        }

                        if (vizinhosB[offSet] < minB) {
                            vizinhosB[offSet] = minB;
                        } else if (vizinhosB[offSet] > maxB) {
                            vizinhosB[offSet] = maxB;
                        }

                        saida.SetPixel(i, j, Color.FromArgb(255, vizinhosR[offSet], vizinhosG[offSet], vizinhosB[offSet]));
                    }
                }
            } else {
                // COM TRATAMENTO DE BORDA: expande a imagem
                Bitmap imagem = expandirImagemComBorda(imagemOriginal, offSet);

                for (int i = 0; i < imagemOriginal.Width; i++) {
                    for (int j = 0; j < imagemOriginal.Height; j++) {
                        int tamanhoJanela = (2 * offSet + 1) * (2 * offSet + 1);
                        int[] vizinhosR = new int[tamanhoJanela];
                        int[] vizinhosG = new int[tamanhoJanela];
                        int[] vizinhosB = new int[tamanhoJanela];

                        int indice = 0;
                        for (int a = -offSet; a <= offSet; a++) {
                            for (int b = -offSet; b <= offSet; b++) {
                                Color p = imagem.GetPixel(i + a + offSet, j + b + offSet);
                                vizinhosR[indice] = p.R;
                                vizinhosG[indice] = p.G;
                                vizinhosB[indice] = p.B;
                                indice++;
                            }
                        }

                        Array.Sort(vizinhosR);
                        int minR = vizinhosR[0];
                        int maxR = vizinhosR[vizinhosR.Length - 1];

                        Array.Sort(vizinhosG);
                        int minG = vizinhosG[0];
                        int maxG = vizinhosG[vizinhosG.Length - 1];

                        Array.Sort(vizinhosB);
                        int minB = vizinhosB[0];
                        int maxB = vizinhosB[vizinhosB.Length - 1];


                        if (vizinhosR[offSet] < minR) {
                            vizinhosR[offSet] = minR;
                        } else if (vizinhosR[offSet] > maxR) {
                            vizinhosR[offSet] = maxR;
                        }

                        if (vizinhosG[offSet] < minG) {
                            vizinhosG[offSet] = minG;
                        } else if (vizinhosG[offSet] > maxG) {
                            vizinhosG[offSet] = maxG;
                        }

                        if (vizinhosB[offSet] < minB) {
                            vizinhosB[offSet] = minB;
                        } else if (vizinhosB[offSet] > maxB) {
                            vizinhosB[offSet] = maxB;
                        }

                        saida.SetPixel(i, j, Color.FromArgb(255, vizinhosR[offSet], vizinhosG[offSet], vizinhosB[offSet]));
                    }
                }
            }
            return saida;
        }

        private Bitmap prewitt(PictureBox imagemEntrada, int valorBorda) {
            Bitmap imagemOriginal = (Bitmap)imagemEntrada.Image;
            Bitmap saida = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);

            Bitmap bordasVerticais = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);
            Bitmap bordasHorizontais = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);

            int[,] filtroHorizontal = { { -1, 0, 1 }, { -1, 0, 1 }, { -1, 0, 1 } };
            int[,] filtroVertical = { { -1, -1, -1 }, { 0, 0, 0 }, { 1, 1, 1 } };
            int offSet = 1;

            if (valorBorda == 0) {
                for (int i = offSet; i < imagemOriginal.Width - offSet; i++) {
                    for (int j = offSet; j < imagemOriginal.Height - offSet; j++) {
                        ProcessarPixelPrewitt(imagemOriginal, saida, bordasVerticais, bordasHorizontais,
                                            filtroHorizontal, filtroVertical, i, j);
                    }
                }
            } else {
                Bitmap imagemExpandida = expandirImagemComBorda(imagemOriginal, offSet);

                for (int i = 0; i < imagemOriginal.Width; i++) {
                    for (int j = 0; j < imagemOriginal.Height; j++) {
                        ProcessarPixelPrewitt(imagemExpandida, saida, bordasVerticais, bordasHorizontais,
                                            filtroHorizontal, filtroVertical, i + offSet, j + offSet);
                    }
                }
            }

            guia1CaixaImagemHorizontal.Image = bordasVerticais;
            guia1CaixaImagemVertical.Image = bordasHorizontais; 

            return saida;
        }

        private void ProcessarPixelPrewitt(Bitmap imagem, Bitmap saida, Bitmap horizontal, Bitmap vertical,
                                         int[,] prewittH, int[,] prewittV, int x, int y) {
            int gH = 0, gV = 0; 

            for (int i = -1; i <= 1; i++) {
                for (int j = -1; j <= 1; j++) {
                    Color p = imagem.GetPixel(x + i, y + j);
                    int grayValue = (int)(p.R * 0.3 + p.G * 0.59 + p.B * 0.11);

                    gH += grayValue * prewittH[i + 1, j + 1]; 
                    gV += grayValue * prewittV[i + 1, j + 1];
                }
            }

            int magnitude = (int)Math.Sqrt(gH * gH + gV * gV);
            magnitude = Math.Max(0, Math.Min(255, magnitude));

            int normalizedGH = (gH + 765) / 6;
            int normalizedGV = (gV + 765) / 6;
            normalizedGH = Math.Max(0, Math.Min(255, normalizedGH));
            normalizedGV = Math.Max(0, Math.Min(255, normalizedGV));

            int outputX = x - (imagem.Width > saida.Width ? 1 : 0);
            int outputY = y - (imagem.Height > saida.Height ? 1 : 0);

            if (outputX >= 0 && outputX < saida.Width && outputY >= 0 && outputY < saida.Height) {
                saida.SetPixel(outputX, outputY, Color.FromArgb(magnitude, magnitude, magnitude));
                horizontal.SetPixel(outputX, outputY, Color.FromArgb(normalizedGH, normalizedGH, normalizedGH));
                vertical.SetPixel(outputX, outputY, Color.FromArgb(normalizedGV, normalizedGV, normalizedGV));    
            }
        }

        private Bitmap sobel(PictureBox imagemEntrada, int valorBorda) {
            Bitmap imagemOriginal = (Bitmap)imagemEntrada.Image;
            Bitmap saida = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);
            Bitmap horizontal = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);  
            Bitmap vertical = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);   

            int[,] sobelHorizontal = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } }; 
            int[,] sobelVertical = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };   

            int offSet = 1;

            if (valorBorda == 0) {
                for (int i = offSet; i < imagemOriginal.Width - offSet; i++) {
                    for (int j = offSet; j < imagemOriginal.Height - offSet; j++) {
                        ProcessarPixelSobel(imagemOriginal, saida, horizontal, vertical,
                                           sobelHorizontal, sobelVertical, i, j);
                    }
                }
            } else {
                Bitmap imagemExpandida = expandirImagemComBorda(imagemOriginal, offSet);

                for (int i = 0; i < imagemOriginal.Width; i++) {
                    for (int j = 0; j < imagemOriginal.Height; j++) {
                        ProcessarPixelSobel(imagemExpandida, saida, horizontal, vertical,
                                          sobelHorizontal, sobelVertical, i + offSet, j + offSet);
                    }
                }
            }

            guia1CaixaImagemHorizontal.Image = horizontal;  
            guia1CaixaImagemVertical.Image = vertical;     

            return saida;
        }

        private void ProcessarPixelSobel(Bitmap imagem, Bitmap saida, Bitmap horizontal, Bitmap vertical,
                                       int[,] sobelH, int[,] sobelV, int x, int y) {
            int gH = 0, gV = 0;

            for (int i = -1; i <= 1; i++) {
                for (int j = -1; j <= 1; j++) {
                    Color p = imagem.GetPixel(x + i, y + j);
                    int grayValue = (int)(p.R * 0.3 + p.G * 0.59 + p.B * 0.11);

                    gH += grayValue * sobelH[i + 1, j + 1];
                    gV += grayValue * sobelV[i + 1, j + 1];
                }
            }

            int magnitude = (int)Math.Sqrt(gH * gH + gV * gV);
            magnitude = Math.Max(0, Math.Min(255, magnitude));

            int normalizedGH = (gH + 1020) / 8;
            int normalizedGV = (gV + 1020) / 8;
            normalizedGH = Math.Max(0, Math.Min(255, normalizedGH));
            normalizedGV = Math.Max(0, Math.Min(255, normalizedGV));

            int outputX = x - (imagem.Width > saida.Width ? 1 : 0);
            int outputY = y - (imagem.Height > saida.Height ? 1 : 0);

            if (outputX >= 0 && outputX < saida.Width && outputY >= 0 && outputY < saida.Height) {
                saida.SetPixel(outputX, outputY, Color.FromArgb(magnitude, magnitude, magnitude));
                horizontal.SetPixel(outputX, outputY, Color.FromArgb(normalizedGH, normalizedGH, normalizedGH));  
                vertical.SetPixel(outputX, outputY, Color.FromArgb(normalizedGV, normalizedGV, normalizedGV)); 
            }
        }

        private Bitmap laplaciano(PictureBox imagemEntrada, int valorBorda) {
            Bitmap imagemOriginal = (Bitmap)imagemEntrada.Image;
            Bitmap saida = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);

            int[,] laplaciano4 = { { 0, -1, 0 }, { -1, 4, -1 }, { 0, -1, 0 } };      
            int[,] laplaciano8 = { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };  

            int[,] kernel = laplaciano8;

            int offSet = 1;

            if (valorBorda == 0) {
                for (int i = offSet; i < imagemOriginal.Width - offSet; i++) {
                    for (int j = offSet; j < imagemOriginal.Height - offSet; j++) {
                        ProcessarPixelLaplaciano(imagemOriginal, saida, kernel, i, j);
                    }
                }
            } else {
                Bitmap imagemExpandida = expandirImagemComBorda(imagemOriginal, offSet);

                for (int i = 0; i < imagemOriginal.Width; i++) {
                    for (int j = 0; j < imagemOriginal.Height; j++) {
                        ProcessarPixelLaplaciano(imagemExpandida, saida, kernel, i + offSet, j + offSet);
                    }
                }
            }

            return saida;
        }

        private void ProcessarPixelLaplaciano(Bitmap imagem, Bitmap saida, int[,] kernel, int x, int y) {
            int valorLaplaciano = 0;

            for (int i = -1; i <= 1; i++) {
                for (int j = -1; j <= 1; j++) {
                    Color p = imagem.GetPixel(x + i, y + j);
                    int grayValue = (int)(p.R * 0.3 + p.G * 0.59 + p.B * 0.11);
                    valorLaplaciano += grayValue * kernel[i + 1, j + 1];
                }
            }

            valorLaplaciano = Math.Max(0, Math.Min(255, valorLaplaciano));

            int outputX = x - (imagem.Width > saida.Width ? 1 : 0);
            int outputY = y - (imagem.Height > saida.Height ? 1 : 0);

            if (outputX >= 0 && outputX < saida.Width && outputY >= 0 && outputY < saida.Height) {
                saida.SetPixel(outputX, outputY, Color.FromArgb(valorLaplaciano, valorLaplaciano, valorLaplaciano));
            }
        }

        /*private Bitmap dilatacao(PictureBox imagemEntrada, string elementoEstruturante) {
            Bitmap imagemOriginal = converterParaEscalaDeCinza(imagemEntrada);
            Bitmap saida = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);

            // Definição dos elementos estruturantes
            int[,] eeQuadrado3x3 = {
                {1, 1, 1},
                {1, 1, 1},
                {1, 1, 1}
            };

            int[,] eeCruz3x3 = {
                {0, 1, 0},
                {1, 1, 1},
                {0, 1, 0}
            };

            int[,] eeDisco5x5 = {
                {0, 1, 1, 1, 0},
                {1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1},
                {0, 1, 1, 1, 0}
            };

            int[,] elemento = null;
            int tamanhoBorda = 0;

            // Seleciona o elemento estruturante apropriado
            switch (elementoEstruturante) {
                case "Quadrado 3x3":
                elemento = eeQuadrado3x3;
                tamanhoBorda = 1;
                break;
                case "Cruz 3x3":
                elemento = eeCruz3x3;
                tamanhoBorda = 1;
                break;
                case "Disco 5x5":
                elemento = eeDisco5x5;
                tamanhoBorda = 2;
                break;
                default:
                throw new ArgumentException("Elemento estruturante não reconhecido");
            }

            Bitmap imagemExpandida = expandirImagemComBorda(imagemOriginal, tamanhoBorda);

            // Percorre cada pixel da imagem original
            for (int i = 0; i < imagemOriginal.Height; i++) {
                for (int j = 0; j < imagemOriginal.Width; j++) // Corrigido: estava i++
                {
                    int maiorPixel = 0;

                    // Percorre a vizinhança definida pelo elemento estruturante
                    for (int x = -tamanhoBorda; x <= tamanhoBorda; x++) {
                        for (int y = -tamanhoBorda; y <= tamanhoBorda; y++) {
                            // Verifica se o elemento estruturante tem 1 nesta posição
                            if (elemento[x + tamanhoBorda, y + tamanhoBorda] == 1) {
                                Color pixel = imagemExpandida.GetPixel(j + x + tamanhoBorda, i + y + tamanhoBorda);
                                maiorPixel = Math.Max(maiorPixel, pixel.R);
                            }
                        }
                    }

                    saida.SetPixel(j, i, Color.FromArgb(maiorPixel, maiorPixel, maiorPixel));
                }
            }

            return saida;
        }*/

        private int[,] ObterElementoEstruturante(string tipo, out int tamanhoBorda) {
            tamanhoBorda = 0;

            switch (tipo) {
                case "Quadrado 3x3":
                tamanhoBorda = 1;
                return new int[,] {
                {1, 1, 1},
                {1, 1, 1},
                {1, 1, 1}
            };

                case "Cruz 3x3":
                tamanhoBorda = 1;
                return new int[,] {
                {0, 1, 0},
                {1, 1, 1},
                {0, 1, 0}
            };

                case "Disco 5x5":
                tamanhoBorda = 2;
                return new int[,] {
                {0, 1, 1, 1, 0},
                {1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1},
                {1, 1, 1, 1, 1},
                {0, 1, 1, 1, 0}
            };

                default:
                throw new ArgumentException("Elemento estruturante não reconhecido");
            }
        }

        private Bitmap AplicarOperacaoMorfologica(Bitmap imagem, string elementoEstruturante,
                                                Func<Color[], int> operacaoPixel) {
            int[,] elemento;
            int tamanhoBorda;

            elemento = ObterElementoEstruturante(elementoEstruturante, out tamanhoBorda);
            Bitmap imagemExpandida = expandirImagemComBorda(imagem, tamanhoBorda);
            Bitmap saida = new Bitmap(imagem.Width, imagem.Height);

            for (int i = 0; i < imagem.Height; i++) {
                for (int j = 0; j < imagem.Width; j++) {
                    List<Color> pixelsVizinhança = new List<Color>();

                    for (int x = -tamanhoBorda; x <= tamanhoBorda; x++) {
                        for (int y = -tamanhoBorda; y <= tamanhoBorda; y++) {
                            if (elemento[x + tamanhoBorda, y + tamanhoBorda] == 1) {
                                pixelsVizinhança.Add(imagemExpandida.GetPixel(j + x + tamanhoBorda, i + y + tamanhoBorda));
                            }
                        }
                    }

                    int novoValor = operacaoPixel(pixelsVizinhança.ToArray());
                    saida.SetPixel(j, i, Color.FromArgb(novoValor, novoValor, novoValor));
                }
            }

            return saida;
        }

        private Bitmap erosao(PictureBox imagemEntrada, string elementoEstruturante) {
            Bitmap imagemOriginal = converterParaEscalaDeCinza(imagemEntrada);

            return AplicarOperacaoMorfologica(imagemOriginal, elementoEstruturante, pixels => {
                int menorPixel = 255;
                foreach (Color p in pixels) {
                    menorPixel = Math.Min(menorPixel, p.R);
                }
                return menorPixel;
            });
        }

        private Bitmap abertura(PictureBox imagemEntrada, string elementoEstruturante) {
            Bitmap imagemOriginal = converterParaEscalaDeCinza(imagemEntrada);

            Bitmap imagemErodida = erosao(imagemEntrada, elementoEstruturante);

            return dilatacao(new PictureBox() { Image = imagemErodida }, elementoEstruturante);
        }

        private Bitmap fechamento(PictureBox imagemEntrada, string elementoEstruturante) {
            Bitmap imagemOriginal = converterParaEscalaDeCinza(imagemEntrada);

            Bitmap imagemDilatada = dilatacao(imagemEntrada, elementoEstruturante);
            return erosao(new PictureBox() { Image = imagemDilatada }, elementoEstruturante);
        }

        private Bitmap contorno(PictureBox imagemEntrada, string elementoEstruturante, string metodo) {
            Bitmap imagemOriginal = converterParaEscalaDeCinza(imagemEntrada);
            Bitmap contorno = new Bitmap(imagemOriginal.Width, imagemOriginal.Height);

            if (metodo == "diferenca") {
                Bitmap imagemErodida = erosao(imagemEntrada, elementoEstruturante);

                for (int i = 0; i < imagemOriginal.Height; i++) {
                    for (int j = 0; j < imagemOriginal.Width; j++) {
                        int valorOriginal = imagemOriginal.GetPixel(j, i).R;
                        int valorErodido = imagemErodida.GetPixel(j, i).R;
                        int diferenca = Math.Max(0, valorOriginal - valorErodido);
                        contorno.SetPixel(j, i, Color.FromArgb(diferenca, diferenca, diferenca));
                    }
                }
            } else if (metodo == "gradiente")
              {
                Bitmap imagemDilatada = dilatacao(imagemEntrada, elementoEstruturante);
                Bitmap imagemErodida = erosao(imagemEntrada, elementoEstruturante);

                for (int i = 0; i < imagemOriginal.Height; i++) {
                    for (int j = 0; j < imagemOriginal.Width; j++) {
                        int valorDilatado = imagemDilatada.GetPixel(j, i).R;
                        int valorErodido = imagemErodida.GetPixel(j, i).R;
                        int diferenca = Math.Max(0, valorDilatado - valorErodido);
                        contorno.SetPixel(j, i, Color.FromArgb(diferenca, diferenca, diferenca));
                    }
                }
            }
                return contorno;
        }

        private Bitmap dilatacao(PictureBox imagemEntrada, string elementoEstruturante) {
            Bitmap imagemOriginal = converterParaEscalaDeCinza(imagemEntrada);

            return AplicarOperacaoMorfologica(imagemOriginal, elementoEstruturante, pixels => {
                int maiorPixel = 0;
                foreach (Color p in pixels) {
                    maiorPixel = Math.Max(maiorPixel, p.R);
                }
                return maiorPixel;
            });
        }

        private Bitmap expandirImagemComBorda(Bitmap original, int offSet) {
            int larguraNova = original.Width + 2 * offSet;
            int alturaNova = original.Height + 2 * offSet;

            Bitmap expandida = new Bitmap(larguraNova, alturaNova);

            for (int x = 0; x < larguraNova; x++) {
                for (int y = 0; y < alturaNova; y++) {
                    int srcX = Math.Max(0, Math.Min(x - offSet, original.Width - 1));
                    int srcY = Math.Max(0, Math.Min(y - offSet, original.Height - 1));
                    Color cor = original.GetPixel(srcX, srcY);
                    expandida.SetPixel(x, y, cor);
                }
            }
            return expandida;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///
        private void carregarImagem(PictureBox pictureBox) {
            // Configurações iniciais da OpenFileDialogBox
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            var filePath = string.Empty;
            openFileDialog1.InitialDirectory = "C:\\Users\\João\\Documents\\Faculdade\\5_semestre\\Processamento de Imagens\\Material Matlab\\Matlab";
            openFileDialog1.Filter = "TIFF image (*.tif)|*.tif|JPG image (*.jpg)|*.jpg|BMP image (*.bmp)|*.bmp|PNG image (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 5;
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
                MessageBox.Show("Nenhuma imagem para salvar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void salvarKernelComoImagem(double[,] kernel, int zoom = 40) {
            int altura = kernel.GetLength(0);
            int largura = kernel.GetLength(1);

            double min = kernel[0, 0], max = kernel[0, 0];
            for (int y = 0; y < altura; y++)
                for (int x = 0; x < largura; x++) {
                    if (kernel[y, x] < min) min = kernel[y, x];
                    if (kernel[y, x] > max) max = kernel[y, x];
                }

            Bitmap bmp = new Bitmap(largura * zoom, altura * zoom);

            for (int y = 0; y < altura; y++) {
                for (int x = 0; x < largura; x++) {
                    double normalizado = (kernel[y, x] - min) / (max - min);
                    int cor = (int)(normalizado * 255);
                    cor = Math.Min(Math.Max(cor, 0), 255);
                    Color corPixel = Color.FromArgb(cor, cor, cor);

                    // Replicar o pixel para formar o "zoom"
                    for (int dy = 0; dy < zoom; dy++) {
                        for (int dx = 0; dx < zoom; dx++) {
                            bmp.SetPixel(x * zoom + dx, y * zoom + dy, corPixel);
                        }
                    }
                }
            }

            PictureBox salvamento = new PictureBox();
            salvamento.Image = bmp;

            salvarImagem(salvamento);
        }
    }
}