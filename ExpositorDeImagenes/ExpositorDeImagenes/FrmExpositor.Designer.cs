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
            this.PicExpositor = new System.Windows.Forms.PictureBox();
            this.BtnMostrarImagen = new System.Windows.Forms.Button();
            this.ChkRepetir = new System.Windows.Forms.CheckBox();
            this.BtnMusica = new System.Windows.Forms.Button();
            this.BtnActualizar = new System.Windows.Forms.Button();
            this.TrbVolumen = new System.Windows.Forms.TrackBar();
            this.CklLista = new System.Windows.Forms.CheckedListBox();
            this.LblPorcentaje = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PicExpositor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrbVolumen)).BeginInit();
            this.SuspendLayout();
            // 
            // PicExpositor
            // 
            this.PicExpositor.BackColor = System.Drawing.Color.White;
            this.PicExpositor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PicExpositor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicExpositor.Location = new System.Drawing.Point(18, 13);
            this.PicExpositor.Name = "PicExpositor";
            this.PicExpositor.Size = new System.Drawing.Size(332, 322);
            this.PicExpositor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicExpositor.TabIndex = 0;
            this.PicExpositor.TabStop = false;
            // 
            // BtnMostrarImagen
            // 
            this.BtnMostrarImagen.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BtnMostrarImagen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnMostrarImagen.Location = new System.Drawing.Point(394, 13);
            this.BtnMostrarImagen.Name = "BtnMostrarImagen";
            this.BtnMostrarImagen.Size = new System.Drawing.Size(68, 36);
            this.BtnMostrarImagen.TabIndex = 1;
            this.BtnMostrarImagen.Text = "Mostrar imagen";
            this.BtnMostrarImagen.UseVisualStyleBackColor = false;
            this.BtnMostrarImagen.Click += new System.EventHandler(this.BtnMostrarImagen_Click);
            // 
            // ChkRepetir
            // 
            this.ChkRepetir.AutoSize = true;
            this.ChkRepetir.Checked = true;
            this.ChkRepetir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkRepetir.ForeColor = System.Drawing.Color.White;
            this.ChkRepetir.Location = new System.Drawing.Point(468, 24);
            this.ChkRepetir.Name = "ChkRepetir";
            this.ChkRepetir.Size = new System.Drawing.Size(77, 17);
            this.ChkRepetir.TabIndex = 2;
            this.ChkRepetir.Text = "No Repetir";
            this.ChkRepetir.UseVisualStyleBackColor = true;
            this.ChkRepetir.CheckedChanged += new System.EventHandler(this.ChkRepetir_CheckedChanged);
            // 
            // BtnMusica
            // 
            this.BtnMusica.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BtnMusica.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnMusica.Image = ((System.Drawing.Image)(resources.GetObject("BtnMusica.Image")));
            this.BtnMusica.Location = new System.Drawing.Point(394, 64);
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
            this.BtnActualizar.Location = new System.Drawing.Point(477, 64);
            this.BtnActualizar.Name = "BtnActualizar";
            this.BtnActualizar.Size = new System.Drawing.Size(68, 38);
            this.BtnActualizar.TabIndex = 4;
            this.BtnActualizar.UseVisualStyleBackColor = false;
            this.BtnActualizar.Click += new System.EventHandler(this.BtnActualizar_Click);
            // 
            // TrbVolumen
            // 
            this.TrbVolumen.Enabled = false;
            this.TrbVolumen.Location = new System.Drawing.Point(573, 44);
            this.TrbVolumen.Maximum = 100;
            this.TrbVolumen.Name = "TrbVolumen";
            this.TrbVolumen.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TrbVolumen.Size = new System.Drawing.Size(45, 291);
            this.TrbVolumen.TabIndex = 5;
            this.TrbVolumen.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.TrbVolumen.Scroll += new System.EventHandler(this.TrbVolumen_Scroll);
            this.TrbVolumen.ValueChanged += new System.EventHandler(this.TrbVolumen_ValueChanged);
            // 
            // CklLista
            // 
            this.CklLista.Enabled = false;
            this.CklLista.FormattingEnabled = true;
            this.CklLista.Location = new System.Drawing.Point(394, 121);
            this.CklLista.Name = "CklLista";
            this.CklLista.Size = new System.Drawing.Size(151, 214);
            this.CklLista.TabIndex = 5;
            // 
            // LblPorcentaje
            // 
            this.LblPorcentaje.AutoSize = true;
            this.LblPorcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPorcentaje.ForeColor = System.Drawing.Color.White;
            this.LblPorcentaje.Location = new System.Drawing.Point(565, 17);
            this.LblPorcentaje.Name = "LblPorcentaje";
            this.LblPorcentaje.Size = new System.Drawing.Size(59, 24);
            this.LblPorcentaje.TabIndex = 6;
            this.LblPorcentaje.Text = "100%";
            // 
            // FrmExpositor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(639, 348);
            this.Controls.Add(this.LblPorcentaje);
            this.Controls.Add(this.CklLista);
            this.Controls.Add(this.TrbVolumen);
            this.Controls.Add(this.BtnActualizar);
            this.Controls.Add(this.BtnMusica);
            this.Controls.Add(this.ChkRepetir);
            this.Controls.Add(this.BtnMostrarImagen);
            this.Controls.Add(this.PicExpositor);
            this.Name = "FrmExpositor";
            this.ShowIcon = false;
            this.Text = "Expositor";
            ((System.ComponentModel.ISupportInitialize)(this.PicExpositor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrbVolumen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PicExpositor;
        private System.Windows.Forms.Button BtnMostrarImagen;
        private System.Windows.Forms.CheckBox ChkRepetir;
        private System.Windows.Forms.Button BtnMusica;
        private System.Windows.Forms.Button BtnActualizar;
        private System.Windows.Forms.TrackBar TrbVolumen;
        private System.Windows.Forms.CheckedListBox CklLista;
        private System.Windows.Forms.Label LblPorcentaje;
    }
}

