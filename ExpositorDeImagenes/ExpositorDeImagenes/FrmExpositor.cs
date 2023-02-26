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
        List<string> path;
        private Image imagen;
        private bool Estado = false, NoRepetir = true; //nos permite revisar si se debe repetir la musica y si esta reproduciendo o no
        private SoundPlayer SoundPlayer;
        private CoreAudioController controller;
        private CoreAudioDevice VolumenControl;
        private Random Rand = new Random();

        public FrmExpositor()
        {
            IsProcessOpen("ExpositorDeImagenes");
            InitializeComponent();
            PrepararAudios();
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
        }

        private void PrepararAudios()
        {
            try
            {
                controller = new CoreAudioController();
                SoundPlayer = new SoundPlayer(Directory.GetFiles(Environment.SpecialFolder.MyMusic.ToString(), "*.wav")[0]);
                VolumenControl = controller.DefaultPlaybackDevice;
                TrbVolumen.Enabled = true;
                TrbVolumen.Value = VolumenControl.Volume;
                LblPorcentaje.Text = TrbVolumen.Value.ToString() + " %";
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
                path = Directory.GetFiles(Environment.SpecialFolder.MyPictures.ToString()).Where(f => f.EndsWith(".GIF", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".JPG", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".JPEG", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".BMP", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".WMF", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".PNG", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".TIF", StringComparison.OrdinalIgnoreCase) || f.EndsWith(".TIFF", StringComparison.OrdinalIgnoreCase)).ToList<string>();
            }
            catch (UnauthorizedAccessException)
            {
                AccesoDenegado();
            }
        }

        private void GenerarLista()
        {//rellena la lista de comprobación
            if (path.Count != CklLista.Items.Count)
            {
                CklLista.Items.Clear();
            }
            if (CklLista.Items.Count == 0)
            {
                for (int i = 0; i < path.Count; i++)
                {
                    CklLista.Items.Add(path[i].Split('\\')[1], CheckState.Unchecked);
                }
            }
            else
            {
                for (int i = 0; i < path.Count; i++)
                {
                    CklLista.Items.Add(path[i].Split('\\')[1], CheckState.Unchecked);
                }
            }
        }

        private void LimpiarLista()
        {
            MessageBox.Show("Todas las imagenes se mostraron");
            BtnMostrarImagen.Text = "Mostrar imagen";
            PicExpositor.BackgroundImage = null;
            imagen.Dispose();
            for (int i = 0; i < CklLista.Items.Count; i++)
            {
                CklLista.SetItemChecked(i, false);//reinicia los checks existentes
            }
        }

        private void BtnMostrarImagen_Click(object sender, EventArgs e)
        {
            BtnMostrarImagen.Text = "Cambiar imagen";

            if (ChkOrden.Checked == true)
            {
                if (CklLista.CheckedItems.Count == 0)
                {
                    MostrarImagen(0);
                    return;
                }
                if (CklLista.CheckedItems.Count >= CklLista.Items.Count)
                {
                    LimpiarLista();
                    MostrarImagen(0);
                    return;
                }
                foreach (var item in CklLista.CheckedItems)
                {
                    for (int i = 0; i < CklLista.Items.Count; i++)
                    {
                        if (CklLista.GetItemChecked(i) == false)
                        {
                            MostrarImagen(i);
                            return;
                        }
                    }
                    return;
                }
            }
            else
            {
                if (CklLista.CheckedItems.Count >= CklLista.Items.Count)
                {
                    LimpiarLista();
                    return;
                }
                else
                {
                    if (CklLista.Items.Count == 0)
                    {
                        BtnMostrarImagen.Text = "Mostrar imagen";
                        MessageBox.Show("No hay imagenes disponibles para mostrar, añadalas por favor o actualice la lista", "Lista vacía", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    int N = EscogerNumero(NoRepetir);
                    MostrarImagen(N);
                }
            }
        }

        public int EscogerNumero(bool r)
        {
            int x = CklLista.Items.Count;
            int N;
            if (r && CklLista.CheckedItems.Count < x)
            {
                do
                {
                    N = Rand.Next(0, x);
                    try
                    {
                        if (CklLista.GetItemChecked(N).Equals(false))
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
                        Filter = "Archivos (*.mp3;*.wav) | *.mp3;*.wav",
                        Title = "Selecciona tu archivo mp3"
                    };
                    if (file.ShowDialog() == DialogResult.OK)
                    {
                        ConvertiraWav(file.FileName);
                    }
                    file.Dispose();
                }
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
            Actualizar();
        }
        private void Actualizar()
        {
            PararMusica();
            CrearCarpetas();
            PicExpositor.BackColor = Color.White;
            Actualizar_imagenes();
            Actualizar_musica();
        }

        private void Actualizar_imagenes()
        {
            path.Clear();
            CklLista.Items.Clear();
            CargarPath();
            GenerarLista();
        }
        private void Actualizar_musica()
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
                    PonerMusica();
                    if (TrbVolumen.Value == 0)
                    {
                        TrbVolumen.Value = 0;
                    }
                }
            }

        }

        private void TrbVolumen_Scroll(object sender, EventArgs e)
        {
            try
            {
                VolumenControl.Volume = TrbVolumen.Value;
                LblPorcentaje.Text = TrbVolumen.Value + " %";
            }
            catch (Exception ex)
            {
                if (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException)
                {
                    //VolumenControl.Volume = 0;
                }
            }
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
                }
                else if (e.KeyValue.Equals(13) && CklLista.SelectionMode == SelectionMode.One && CklLista.GetItemChecked(CklLista.SelectedIndex).Equals(false))
                {
                    CklLista.SetItemChecked(CklLista.SelectedIndex, true);
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

        private void FrmExpositor_Deactivate(object sender, EventArgs e)
        {
            try
            {
                TrbVolumen.Value = VolumenControl.Volume;
                LblPorcentaje.Text = TrbVolumen.Value + " %";
            }
            catch (Exception ex)
            {
                if (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException)
                {
                    return;
                }
            }
        }

        private void FrmExpositor_Activated(object sender, EventArgs e)
        {
            try
            {
                TrbVolumen.Value = VolumenControl.Volume;
                LblPorcentaje.Text = TrbVolumen.Value + " %";
            }
            catch (Exception ex)
            {
                if (ex is ArgumentOutOfRangeException || ex is IndexOutOfRangeException)
                {
                    return;
                }
            }
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