namespace ExpositorDeImagenes
{
    partial class FrmExpositor
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExpositor));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BtnMostrarImagen = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.BtnMusica = new System.Windows.Forms.Button();
            this.BtnActualizar = new System.Windows.Forms.Button();
            this.TrbVolumen = new System.Windows.Forms.TrackBar();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.LblPorcentaje = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrbVolumen)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 250);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // BtnMostrarImagen
            // 
            this.BtnMostrarImagen.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BtnMostrarImagen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnMostrarImagen.Location = new System.Drawing.Point(282, 13);
            this.BtnMostrarImagen.Name = "BtnMostrarImagen";
            this.BtnMostrarImagen.Size = new System.Drawing.Size(68, 36);
            this.BtnMostrarImagen.TabIndex = 1;
            this.BtnMostrarImagen.Text = "Mostrar imagen";
            this.BtnMostrarImagen.UseVisualStyleBackColor = false;
            this.BtnMostrarImagen.Click += new System.EventHandler(this.BtnMostrarImagen_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.ForeColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(356, 24);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(97, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Repetir imagen";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // BtnMusica
            // 
            this.BtnMusica.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BtnMusica.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnMusica.Image = ((System.Drawing.Image)(resources.GetObject("BtnMusica.Image")));
            this.BtnMusica.Location = new System.Drawing.Point(282, 52);
            this.BtnMusica.Name = "BtnMusica";
            this.BtnMusica.Size = new System.Drawing.Size(68, 38);
            this.BtnMusica.TabIndex = 3;
            this.BtnMusica.UseVisualStyleBackColor = false;
            this.BtnMusica.Click += new System.EventHandler(this.BtnMusica_Click);
            // 
            // BtnActualizar
            // 
            this.BtnActualizar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BtnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("BtnActualizar.Image")));
            this.BtnActualizar.Location = new System.Drawing.Point(356, 52);
            this.BtnActualizar.Name = "BtnActualizar";
            this.BtnActualizar.Size = new System.Drawing.Size(68, 38);
            this.BtnActualizar.TabIndex = 3;
            this.BtnActualizar.UseVisualStyleBackColor = false;
            this.BtnActualizar.Click += new System.EventHandler(this.BtnActualizar_Click);
            // 
            // TrbVolumen
            // 
            this.TrbVolumen.Location = new System.Drawing.Point(430, 58);
            this.TrbVolumen.Maximum = 100;
            this.TrbVolumen.Name = "TrbVolumen";
            this.TrbVolumen.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TrbVolumen.Size = new System.Drawing.Size(45, 211);
            this.TrbVolumen.TabIndex = 4;
            this.TrbVolumen.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.TrbVolumen.Scroll += new System.EventHandler(this.TrbVolumen_Scroll);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(282, 94);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(142, 169);
            this.checkedListBox1.TabIndex = 5;
            // 
            // LblPorcentaje
            // 
            this.LblPorcentaje.AutoSize = true;
            this.LblPorcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPorcentaje.ForeColor = System.Drawing.Color.White;
            this.LblPorcentaje.Location = new System.Drawing.Point(429, 42);
            this.LblPorcentaje.Name = "LblPorcentaje";
            this.LblPorcentaje.Size = new System.Drawing.Size(43, 24);
            this.LblPorcentaje.TabIndex = 6;
            this.LblPorcentaje.Text = "100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(466, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 24);
            this.label2.TabIndex = 6;
            this.label2.Text = "%";
            // 
            // FrmExpositor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(495, 276);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LblPorcentaje);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.TrbVolumen);
            this.Controls.Add(this.BtnActualizar);
            this.Controls.Add(this.BtnMusica);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.BtnMostrarImagen);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FrmExpositor";
            this.ShowIcon = false;
            this.Text = "Expositor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrbVolumen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BtnMostrarImagen;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button BtnMusica;
        private System.Windows.Forms.Button BtnActualizar;
        private System.Windows.Forms.TrackBar TrbVolumen;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label LblPorcentaje;
        private System.Windows.Forms.Label label2;
    }
}

