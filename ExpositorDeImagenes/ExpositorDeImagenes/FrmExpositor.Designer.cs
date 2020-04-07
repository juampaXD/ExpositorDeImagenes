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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExpositor));
            this.PicExpositor = new System.Windows.Forms.PictureBox();
            this.BtnMostrarImagen = new System.Windows.Forms.Button();
            this.ChkRepetir = new System.Windows.Forms.CheckBox();
            this.BtnMusica = new System.Windows.Forms.Button();
            this.BtnActualizar = new System.Windows.Forms.Button();
            this.TrbVolumen = new System.Windows.Forms.TrackBar();
            this.CklLista = new System.Windows.Forms.CheckedListBox();
            this.LblPorcentaje = new System.Windows.Forms.Label();
            this.ChkMarcadoManual = new System.Windows.Forms.CheckBox();
            this.TipExpositor = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.agregarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.PicExpositor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrbVolumen)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PicExpositor
            // 
            this.PicExpositor.BackColor = System.Drawing.Color.White;
            this.PicExpositor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PicExpositor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicExpositor.Location = new System.Drawing.Point(15, 37);
            this.PicExpositor.Name = "PicExpositor";
            this.PicExpositor.Size = new System.Drawing.Size(332, 322);
            this.PicExpositor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PicExpositor.TabIndex = 0;
            this.PicExpositor.TabStop = false;
            this.PicExpositor.MouseHover += new System.EventHandler(this.PicExpositor_MouseHover);
            // 
            // BtnMostrarImagen
            // 
            this.BtnMostrarImagen.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BtnMostrarImagen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnMostrarImagen.Location = new System.Drawing.Point(374, 41);
            this.BtnMostrarImagen.Name = "BtnMostrarImagen";
            this.BtnMostrarImagen.Size = new System.Drawing.Size(68, 34);
            this.BtnMostrarImagen.TabIndex = 1;
            this.BtnMostrarImagen.Text = "Mostrar imagen";
            this.BtnMostrarImagen.UseVisualStyleBackColor = false;
            this.BtnMostrarImagen.Click += new System.EventHandler(this.BtnMostrarImagen_Click);
            this.BtnMostrarImagen.MouseHover += new System.EventHandler(this.BtnMostrarImagen_MouseHover);
            // 
            // ChkRepetir
            // 
            this.ChkRepetir.AutoSize = true;
            this.ChkRepetir.Checked = true;
            this.ChkRepetir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkRepetir.ForeColor = System.Drawing.Color.White;
            this.ChkRepetir.Location = new System.Drawing.Point(374, 91);
            this.ChkRepetir.Name = "ChkRepetir";
            this.ChkRepetir.Size = new System.Drawing.Size(77, 17);
            this.ChkRepetir.TabIndex = 2;
            this.ChkRepetir.Text = "No Repetir";
            this.ChkRepetir.UseVisualStyleBackColor = true;
            this.ChkRepetir.CheckedChanged += new System.EventHandler(this.ChkRepetir_CheckedChanged);
            this.ChkRepetir.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChkRepetir_KeyDown);
            this.ChkRepetir.MouseHover += new System.EventHandler(this.ChkRepetir_MouseHover);
            // 
            // BtnMusica
            // 
            this.BtnMusica.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BtnMusica.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnMusica.Image = ((System.Drawing.Image)(resources.GetObject("BtnMusica.Image")));
            this.BtnMusica.Location = new System.Drawing.Point(484, 92);
            this.BtnMusica.Name = "BtnMusica";
            this.BtnMusica.Size = new System.Drawing.Size(68, 36);
            this.BtnMusica.TabIndex = 6;
            this.BtnMusica.UseVisualStyleBackColor = false;
            this.BtnMusica.Click += new System.EventHandler(this.BtnMusica_Click);
            this.BtnMusica.MouseHover += new System.EventHandler(this.BtnMusica_MouseHover);
            // 
            // BtnActualizar
            // 
            this.BtnActualizar.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BtnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("BtnActualizar.Image")));
            this.BtnActualizar.Location = new System.Drawing.Point(484, 41);
            this.BtnActualizar.Name = "BtnActualizar";
            this.BtnActualizar.Size = new System.Drawing.Size(68, 36);
            this.BtnActualizar.TabIndex = 5;
            this.BtnActualizar.UseVisualStyleBackColor = false;
            this.BtnActualizar.Click += new System.EventHandler(this.BtnActualizar_Click);
            this.BtnActualizar.MouseHover += new System.EventHandler(this.BtnActualizar_MouseHover);
            // 
            // TrbVolumen
            // 
            this.TrbVolumen.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TrbVolumen.Enabled = false;
            this.TrbVolumen.Location = new System.Drawing.Point(570, 81);
            this.TrbVolumen.Maximum = 100;
            this.TrbVolumen.Name = "TrbVolumen";
            this.TrbVolumen.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.TrbVolumen.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TrbVolumen.Size = new System.Drawing.Size(45, 291);
            this.TrbVolumen.TabIndex = 7;
            this.TrbVolumen.Tag = "";
            this.TrbVolumen.Scroll += new System.EventHandler(this.TrbVolumen_Scroll);
            this.TrbVolumen.MouseHover += new System.EventHandler(this.TrbVolumen_MouseHover);
            // 
            // CklLista
            // 
            this.CklLista.FormattingEnabled = true;
            this.CklLista.HorizontalScrollbar = true;
            this.CklLista.Location = new System.Drawing.Point(374, 145);
            this.CklLista.Name = "CklLista";
            this.CklLista.ScrollAlwaysVisible = true;
            this.CklLista.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.CklLista.Size = new System.Drawing.Size(178, 214);
            this.CklLista.TabIndex = 4;
            this.CklLista.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CklLista_KeyDown);
            // 
            // LblPorcentaje
            // 
            this.LblPorcentaje.AutoSize = true;
            this.LblPorcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPorcentaje.ForeColor = System.Drawing.Color.White;
            this.LblPorcentaje.Location = new System.Drawing.Point(562, 41);
            this.LblPorcentaje.Name = "LblPorcentaje";
            this.LblPorcentaje.Size = new System.Drawing.Size(59, 24);
            this.LblPorcentaje.TabIndex = 8;
            this.LblPorcentaje.Text = "100%";
            this.LblPorcentaje.MouseHover += new System.EventHandler(this.LblPorcentaje_MouseHover);
            // 
            // ChkMarcadoManual
            // 
            this.ChkMarcadoManual.AutoSize = true;
            this.ChkMarcadoManual.BackColor = System.Drawing.Color.Black;
            this.ChkMarcadoManual.ForeColor = System.Drawing.Color.White;
            this.ChkMarcadoManual.Location = new System.Drawing.Point(374, 111);
            this.ChkMarcadoManual.Name = "ChkMarcadoManual";
            this.ChkMarcadoManual.Size = new System.Drawing.Size(105, 17);
            this.ChkMarcadoManual.TabIndex = 3;
            this.ChkMarcadoManual.Tag = "ss";
            this.ChkMarcadoManual.Text = "Marcado manual";
            this.ChkMarcadoManual.UseVisualStyleBackColor = false;
            this.ChkMarcadoManual.CheckedChanged += new System.EventHandler(this.ChkMarcadoManual_CheckedChanged);
            this.ChkMarcadoManual.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChkMarcadoManual_KeyDown);
            this.ChkMarcadoManual.MouseHover += new System.EventHandler(this.ChkMarcadoManual_MouseHover);
            // 
            // TipExpositor
            // 
            this.TipExpositor.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.TipExpositor.ToolTipTitle = "Guía";
            this.TipExpositor.UseAnimation = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(628, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // agregarToolStripMenuItem
            // 
            this.agregarToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.agregarToolStripMenuItem.Name = "agregarToolStripMenuItem";
            this.agregarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.agregarToolStripMenuItem.Text = "Agregar";
            this.agregarToolStripMenuItem.Click += new System.EventHandler(this.agregarToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.SalirToolStripMenuItem_Click);
            // 
            // FrmExpositor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(628, 374);
            this.Controls.Add(this.ChkMarcadoManual);
            this.Controls.Add(this.LblPorcentaje);
            this.Controls.Add(this.CklLista);
            this.Controls.Add(this.TrbVolumen);
            this.Controls.Add(this.BtnActualizar);
            this.Controls.Add(this.BtnMusica);
            this.Controls.Add(this.ChkRepetir);
            this.Controls.Add(this.BtnMostrarImagen);
            this.Controls.Add(this.PicExpositor);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FrmExpositor";
            this.Text = "Expositor";
            ((System.ComponentModel.ISupportInitialize)(this.PicExpositor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrbVolumen)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.CheckBox ChkMarcadoManual;
        private System.Windows.Forms.ToolTip TipExpositor;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem agregarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
    }
}

