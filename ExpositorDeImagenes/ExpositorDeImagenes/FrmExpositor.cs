using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;//Nuget para reproducir la música
using AudioSwitcher.AudioApi.CoreAudio; //Nuget para el control del volumen
using System.IO;
using NAudio.Wave;//Nuget para convertir de wav a mp3
using System.Linq;
using System.Diagnostics;

namespace ExpositorDeImagenes

{
    public partial class FrmExpositor : Form
    {
        //private int[] size = new int[] { 0, 0 };
        private Screen screen = Screen.PrimaryScreen;
        private List<bool> ListaRevision = new List<bool>();
        List<string> path;
        private Image imagen;
        private bool Estado = false, NoRepetir = true; //nos permite revisar si se debe repetir la musica y si esta reproduciendo o no
        private SoundPlayer SoundPlayer;
        private CoreAudioDevice VolumenControl;
        private Random Rand = new Random();

        public FrmExpositor()
        {
            IsProcessOpen("ExpositorDeImagenes");
            InitializeComponent();
            Iniciar();
        }

        public void IsProcessOpen(string name)
        {
            int N = 0;
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (clsProcess.ProcessName.Contains(name))
                {
                    N++;
                }
            }
            if (N >= 2)
            {
                MessageBox.Show("Aplicación ya se encuentra abierta");
                this.Close();
                Application.Exit();
            }
            return;
        }

        private void Iniciar()
        {
            CrearCarpetas();
            CargarPath();
            GenerarLista();
            RellenarLista();
            PrepararAudios();
        }

        private void PrepararAudios()
        {
            try
            {
                SoundPlayer = new SoundPlayer(Directory.GetFiles(Environment.SpecialFolder.MyMusic.ToString(), "*.wav")[0]);
                VolumenControl = new CoreAudioController().DefaultPlaybackDevice;
                TrbVolumen.Enabled = true;
                TrbVolumen.Value = int.Parse(VolumenControl.Volume.ToString());
            }
            catch (NullReferenceException) { MessageBox.Show("Dispositivo de reprodución no encontrado o no instalado"); }
            catch (UnauthorizedAccessException)
            {
                AccesoDenegado();
            }
            catch (Exception e)
            {
                if (e is ArgumentOutOfRangeException || e is IndexOutOfRangeException)
                {
                    TrbVolumen.Value = 0;
                    LblPorcentaje.Text = TrbVolumen.Value + " %";
                }
            }
        }

        private void CrearCarpetas()
        {//Crea los directorios de musica e imagenes y si existe no se ejecuta
            Directory.CreateDirectory(Environment.SpecialFolder.MyPictures.ToString());
            Directory.CreateDirectory(Environment.SpecialFolder.MyMusic.ToString());
        }

        private void CargarPath()
        {//obtiene los directorios de las imagenes
            try
            {
                path = Directory.GetFiles(Environment.SpecialFolder.MyPictures.ToString()).Where(f => f.EndsWith(".GIF", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".JPG", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".JPEG", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".BMP", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".WMF", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".PNG", StringComparison.OrdinalIgnoreCase)).ToList<string>();
            }
            catch (UnauthorizedAccessException)
            {
                AccesoDenegado();
            }
        }

        private void GenerarLista()
        {//rellena la lista de comprobación
            if (path.Count != ListaRevision.Count)
            {
                CklLista.Items.Clear();
                ListaRevision.Clear();
            }
            if (CklLista.Items.Count == 0)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    ListaRevision.Add(false);
                }
            }
            else
            {
                for (int i = 0; i < path.Count; i++)
                {
                    ListaRevision[i] = false;
                }
            }
        }

        private void RellenarLista()
        {//llena la lista
            for (int i = 0; i < ListaRevision.Count; i++)
            {
                CklLista.Items.Add(path[i].Split('\\')[1], CheckState.Unchecked);
            }
        }

        private void BtnMostrarImagen_Click(object sender, EventArgs e)
        {
            BtnMostrarImagen.Text = "Cambiar imagen";

            int N = EscogerNumero(CklLista.Items.Count, ListaRevision, NoRepetir);
            if (N == -1)
            {
                BtnMostrarImagen.Text = "Mostrar imagen";
                MessageBox.Show("No hay elementos en la lista", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (ListaRevision.Count(n => n.Equals(true)) >= CklLista.Items.Count - 1)
            {
                if (ListaRevision.Count == 0)
                {
                    BtnMostrarImagen.Text = "Mostrar imagen";
                    MessageBox.Show("No hay imagenes disponibles para mostrar, añadalas por favor o actualice la lista");
                    return;
                }
                MostrarImagen(N);
                for (int i = 0; i < CklLista.Items.Count; i++)
                {
                    CklLista.SetItemChecked(i, false);//reinicia los checks
                }
                MessageBox.Show($"Todas las imagenes se mostraron");
                BtnMostrarImagen.Text = "Mostrar imagen";
                GenerarLista();//en este limpia la lista de revision
                PicExpositor.BackgroundImage = null;
                imagen.Dispose();
                return;
            }
            MostrarImagen(N);
        }

        public int EscogerNumero(int x, List<bool> lista, bool r)
        {
            int N;
            if (r)
            {
                do
                {
                    N = Rand.Next(0, x);
                    try
                    {
                        if (lista[N].Equals(false))
                        {
                            break;
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        return -1;
                    }
                } while (true);
            }
            else
            {
                return Rand.Next(0, x);
            }
            return N;
        }

        private void MostrarImagen(int x)
        {
            if (imagen != null)
            {
                PicExpositor.BackgroundImage = null;
                imagen.Dispose();
            }
            try
            {
                imagen = Image.FromFile(path[x]);
                PicExpositor.BackgroundImage = imagen;
                CklLista.SetItemChecked(x, true);
                ListaRevision[x] = true;
            }
            catch (ArgumentOutOfRangeException)
            { MessageBox.Show("imagenes no encontradas"); }
            catch (FileNotFoundException)
            {
                MessageBox.Show("imagenes no encontradas");
            }
        }

        private void PonerMusica()
        {
            try
            {
                if (!File.Exists(Environment.SpecialFolder.MyMusic.ToString() + @"\Musica.wav"))
                {
                    OpenFileDialog file = new OpenFileDialog
                    {
                        Filter = "Archivos de música(*.mp3) | *.mp3",
                        Title = "Selecciona tu archivo mp3"
                    };
                    if (file.ShowDialog() == DialogResult.OK)
                    {
                        ConvertiraWav(file.FileName);
                    }
                    file.Dispose();
                }
                PrepararAudios();
                Estado = true;
                SoundPlayer.PlayLooping();
                TrbVolumen.Value = VolumenControl.Volume;
            }
            catch (NullReferenceException)
            { return; }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException)
                {
                    TrbVolumen.Value = 0;
                    LblPorcentaje.Text = TrbVolumen.Value + "%";
                    MessageBox.Show("Archivo de musica no encontrado");
                    Estado = false;
                }
            }
        }

        private void PararMusica()
        {
            try
            {
                SoundPlayer.Stop();
                SoundPlayer.Dispose();
                Estado = false;
            }
            catch (NullReferenceException)
            { }
        }

        private void ConvertiraWav(string x)
        {
            try
            {
                if (x.ToLower().Contains(".mp3"))
                {
                    Mp3FileReader mp3 = new Mp3FileReader(x);
                    WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3);
                    WaveFileWriter.CreateWaveFile("Musica.wav", pcm);//crea un archivo wav
                    File.Move(Application.StartupPath.ToString() + @"\Musica.wav", Environment.SpecialFolder.MyMusic.ToString() + @"\Musica.wav");
                }
                else { MessageBox.Show("Extensión invalida"); }
            }
            catch (UnauthorizedAccessException)
            {
                AccesoDenegado();
            }

        }

        private void AccesoDenegado()
        {
            MessageBox.Show("Acceso denegado para esta acción, verifique si tiene permisos suficientes", "Sin permisos necesarios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void CambiarTamaño()
        {
            int[] frmporc = new int[2];
            frmporc[0] = (this.Width * 100) / screen.Bounds.Width;
            frmporc[1] = (this.Height * 100) / screen.Bounds.Height;

            LblPorcentaje.Text = frmporc[0] + " - " + frmporc[1];

            if (frmporc[0] < 50 || frmporc[1] < 56 || frmporc[0] >= 101 || frmporc[1] >= 98)
            {
                if (frmporc[0] < 50 || frmporc[1] < 56)//tamaño inicial
                {
                    if (frmporc[0] < 50)
                    {
                        PicExpositor.Width = 332;
                    }
                    if (frmporc[1] < 56)
                    {
                        PicExpositor.Height = 322;
                    }
                    return;
                }
                //tamaño pantalla completa
                if (frmporc[0] >= 101 || frmporc[1] >= 98)
                {
                    if (frmporc[0] >= 101)
                    {
                        PicExpositor.Width = (this.Width * 75) / 100;
                    }
                    if (frmporc[1] >= 98)
                    {
                        PicExpositor.Height = (this.Height * 75) / 100;
                    }
                    return;
                    //101 98 en pantalla completa
                }
            }
            else
            {
                PicExpositor.Width = (644 * frmporc[0]) / 100;
                PicExpositor.Height = (644 * frmporc[1]) / 100;
            }
        }

        private void FrmExpositor_Resize(object sender, EventArgs e)
        {
            CambiarTamaño();
            //PicExpositor.Width = this.Width - 332;
            //PicExpositor.Height = this.Height - 322;
            //CklLista.Size = new Size(this.Width - 332, this.Height - 322);
            //BtnActualizar.Size = new Size(this.Width - 332, this.Height - 322);
            //BtnMostrarImagen.Size = new Size(this.Width - 332, this.Height - 322);
            //BtnMusica.Size = new Size(this.Width - 332, this.Height - 322);
            //ChkMarcadoManual.Size = new Size(this.Width - 332, this.Height - 322);
            //ChkRepetir.Size = new Size(this.Width - 332, this.Height - 322);
            //LblPorcentaje.Size = new Size(this.Width - 332, this.Height - 322);
            //TrbVolumen.Size = new Size(this.Width - 332, this.Height - 322);
        }

        private void BtnMusica_Click(object sender, EventArgs e)
        {
            if (Estado == false)
            {
                PonerMusica();
            }
            else
            {
                PararMusica();
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            if (File.Exists(Environment.SpecialFolder.MyMusic.ToString() + @"\Musica.wav"))
            {
                if (MessageBox.Show("¿Desea actualizar la música de fondo?", "actualizar musica", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(Environment.SpecialFolder.MyMusic.ToString() + @"\Musica.wav");
                        OpenFileDialog file = new OpenFileDialog()
                        {
                            Multiselect = false,
                            Filter = "Archivos de música(*.mp3) | *.mp3",
                            Title = "Selecciona tu archivo mp3"
                        };
                        file.ShowDialog();
                        ConvertiraWav(file.FileName);
                        file.Dispose();
                    }
                    catch (UnauthorizedAccessException)
                    {
                        AccesoDenegado();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error no controlado" + ex.Message);
                    }
                    Actualizar();
                    PonerMusica();
                    if (TrbVolumen.Value == 0)
                    {
                        TrbVolumen.Value = 5;
                    }
                }
            }

        }

        private void Actualizar()
        {
            PararMusica();
            PicExpositor.BackColor = Color.White;
            ListaRevision.Clear();
            CklLista.Items.Clear();
            path.Clear();
            CargarPath();
            GenerarLista();
            RellenarLista();
        }

        private void TrbVolumen_Scroll(object sender, EventArgs e)
        {
            try
            {
                VolumenControl.Volume = TrbVolumen.Value;
            }
            catch (Exception ex)
            {
                if (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException)
                {
                    VolumenControl.Volume = 0;
                }
            }

            LblPorcentaje.Text = TrbVolumen.Value + " %";
        }

        private void ChkMarcadoManual_CheckedChanged(object sender, EventArgs e)
        {//permite establecer si se puede chequear de forma manual
            if (CklLista.SelectionMode is SelectionMode.One)
            {
                CklLista.SelectionMode = SelectionMode.None;
            }
            else
                CklLista.SelectionMode = SelectionMode.One;
        }

        private void BtnMostrarImagen_MouseHover(object sender, EventArgs e)
        {
            TipExpositor.SetToolTip(BtnMostrarImagen, "Muestra las imagenes de la lista");
        }

        private void BtnActualizar_MouseHover(object sender, EventArgs e)
        {
            TipExpositor.SetToolTip(BtnActualizar, "Actualiza la lista del expositor y limpia la música");
        }

        private void BtnMusica_MouseHover(object sender, EventArgs e)
        {
            TipExpositor.SetToolTip(BtnMusica, "añade una canción en formato mp3 a la carpeta MyMusic y podrás escuchar música");
        }

        private void ChkRepetir_MouseHover(object sender, EventArgs e)
        {
            TipExpositor.SetToolTip(ChkRepetir, "Indica si se pueden repetir imágenes o no");
        }

        private void ChkMarcadoManual_MouseHover(object sender, EventArgs e)
        {
            TipExpositor.SetToolTip(ChkMarcadoManual, "Indica si deseas marcar alguna imagen como vista  o no vista");
        }

        private void TrbVolumen_MouseHover(object sender, EventArgs e)
        {
            TipExpositor.SetToolTip(TrbVolumen, "sube y baja el Volumen para la música");
        }

        private void PicExpositor_MouseHover(object sender, EventArgs e)
        {
            TipExpositor.SetToolTip(PicExpositor, "Presentación de las imagenes");
        }

        private void LblPorcentaje_MouseHover(object sender, EventArgs e)
        {
            TipExpositor.SetToolTip(LblPorcentaje, "Volumen para la música");
        }

        private void CklLista_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue.Equals(13) && CklLista.SelectionMode == SelectionMode.One && CklLista.GetItemChecked(CklLista.SelectedIndex).Equals(true))
                {
                    CklLista.SetItemChecked(CklLista.SelectedIndex, false);
                    ListaRevision[CklLista.SelectedIndex] = false;
                }
                else if (e.KeyValue.Equals(13) && CklLista.SelectionMode == SelectionMode.One && CklLista.GetItemChecked(CklLista.SelectedIndex).Equals(false))
                {
                    CklLista.SetItemChecked(CklLista.SelectedIndex, true);
                    ListaRevision[CklLista.SelectedIndex] = true;
                }
            }
            catch (ArgumentOutOfRangeException) { }
        }

        private void ChkRepetir_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.Equals(13) && ChkRepetir.Checked.Equals(false))
                ChkRepetir.Checked = true;
            else
                ChkRepetir.Checked = false;
        }

        private void ChkMarcadoManual_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.Equals(13) && ChkMarcadoManual.Checked.Equals(false))
                ChkMarcadoManual.Checked = true;
            else
                ChkMarcadoManual.Checked = false;
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AgregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog
            {
                Filter = "Imagenes a añadir(*.GIF;*.JPG;*.JPEG;*.BMP;*.WMF;*.PNG)|*.GIF;*.JPG;*.JPEG;*.BMP;*.WMF;*.PNG",
                Title = "Selecciona tus imagenes a añadir",
                Multiselect = true
            };
            try
            {
                if (file.ShowDialog() == DialogResult.OK)
                {
                    string name;
                    foreach (var item in file.FileNames)
                    {
                        name = item.Split('\\')[item.Split('\\').Count() - 1];
                        File.Copy(item, Environment.SpecialFolder.MyPictures.ToString() + @"\" + name, true);
                    }
                }
                file.Dispose();
                Actualizar();
            }
            catch (UnauthorizedAccessException)
            {
                AccesoDenegado();
            }

        }

        private void ImágenesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Quieres vaciar las imagenes y la lista?", "Limpieza", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    PicExpositor.BackgroundImage.Dispose();
                }
                catch (NullReferenceException)
                { }
                PicExpositor.BackgroundImage = null;
                PicExpositor.BackColor = Color.White;
                List<string> s = Directory.GetFiles(Environment.SpecialFolder.MyPictures.ToString()).ToList();
                try
                {
                    path.Clear();
                    ListaRevision.Clear();
                    CklLista.Items.Clear();
                    foreach (var item in s)
                    {
                        File.Delete(item);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    AccesoDenegado();
                }

            }
        }

        private void MúsicaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(Environment.SpecialFolder.MyMusic.ToString() + @"\Musica.wav");
            }
            catch (UnauthorizedAccessException)
            {
                AccesoDenegado();
            }
        }

        private void ChkRepetir_CheckedChanged(object sender, EventArgs e)
        {
            NoRepetir = (ChkRepetir.Checked.Equals(true)) ? true : false;
        }

        private void FrmExpositor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Seguro(a) que desea cerrar la aplicación?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}