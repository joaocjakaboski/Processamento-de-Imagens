namespace manipulacao {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.CaixaImagemImportada = new System.Windows.Forms.PictureBox();
            this.botaoImportarImagem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CaixaImagemImportada)).BeginInit();
            this.SuspendLayout();
            // 
            // CaixaImagemImportada
            // 
            this.CaixaImagemImportada.Location = new System.Drawing.Point(12, 55);
            this.CaixaImagemImportada.Name = "CaixaImagemImportada";
            this.CaixaImagemImportada.Size = new System.Drawing.Size(200, 200);
            this.CaixaImagemImportada.TabIndex = 0;
            this.CaixaImagemImportada.TabStop = false;
            // 
            // botaoImportarImagem
            // 
            this.botaoImportarImagem.Location = new System.Drawing.Point(52, 12);
            this.botaoImportarImagem.Name = "botaoImportarImagem";
            this.botaoImportarImagem.Size = new System.Drawing.Size(116, 37);
            this.botaoImportarImagem.TabIndex = 1;
            this.botaoImportarImagem.Text = "Importar imagem";
            this.botaoImportarImagem.UseVisualStyleBackColor = true;
            this.botaoImportarImagem.Click += new System.EventHandler(this.botaoImportarImagem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.botaoImportarImagem);
            this.Controls.Add(this.CaixaImagemImportada);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.CaixaImagemImportada)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox CaixaImagemImportada;
        private System.Windows.Forms.Button botaoImportarImagem;
    }
}

