using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using AudioSwitcher.AudioApi.CoreAudio; //api para el control del volumen
using System.IO;

namespace ExpositorDeImagenes

{
    public partial class FrmExpositor : Form
    {
        private List<bool> ListaRevision = new List<bool>();
        private ImageList ListaImagenes;
        private Random Rand = new Random();
        public int N;
        private bool Estado = false;
        private bool NoRepetir = true;
        private SoundPlayer SoundPlayer;
        private CoreAudioDevice VolumenControl;

        public FrmExpositor()
        {
            InitializeComponent();
            Iniciar();
            PrepararAudios();
        }
        private void Iniciar()
        {
            CrearCarpetas();
            CargarImagenes();
            GenerarLista();
            RellenarLista();
        }

        private void PrepararAudios()
        {
            try
            {//Aqui se puede organizar para que convierta de mp3 a wav
                SoundPlayer = new SoundPlayer(Directory.GetFiles(Environment.SpecialFolder.MyMusic.ToString(), "*.wav")[0]);
                VolumenControl = new CoreAudioController().DefaultPlaybackDevice;
                LblPorcentaje.Text = VolumenControl.Volume.ToString();
                TrbVolumen.Value = int.Parse(VolumenControl.Volume.ToString());
            }
            catch (IndexOutOfRangeException) { }
            catch (ArgumentOutOfRangeException) { LblPorcentaje.Text = "0"; }
            catch (NullReferenceException) { }
        }

        private void CrearCarpetas()
        {//Crea los directorios de musica e imagenes y si existe no se ejecuta
            Directory.CreateDirectory(Environment.SpecialFolder.MyPictures.ToString());
            Directory.CreateDirectory(Environment.SpecialFolder.MyMusic.ToString());
        }

        private void CargarImagenes()
        {
            /*Obtiene las imagenes*/
            ListaImagenes = new ImageList//se simplifica y configura de manera mas sencilla
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(250, 250)
            };
            string[] path = Directory.GetFiles(Environment.SpecialFolder.MyPictures.ToString(), "*.jpg");
            int i = 0;
            foreach (var item in path)
            {
                ListaImagenes.Images.Add(i.ToString(), Image.FromFile(path[i]));
                i++;
            }
        }

        private void GenerarLista()
        {
            if (ListaRevision.Count == 0)
            {
                for (int i = 0; i < Directory.GetFiles(Environment.SpecialFolder.MyPictures.ToString(), "*.jpg").Length; i++)
                {
                    ListaRevision.Add(false);
                }
            }
            else
            {
                for (int i = 0; i < Directory.GetFiles(Environment.SpecialFolder.MyPictures.ToString(), "*.jpg").Length; i++)
                {
                    ListaRevision[i] = false;
                }
            }
        }

        private void RellenarLista()
        {
            for (int i = 0; i < ListaRevision.Count; i++)
            {
                CklLista.Items.Add("imagen " + (i + 1), CheckState.Unchecked);
            }
        }

        private void BtnMostrarImagen_Click(object sender, EventArgs e)
        {
            if (BtnMostrarImagen.Text == "Mostrar imagen")
            {
                BtnMostrarImagen.Text = "Cambiar imagen";
            }
            EscogerNumero(ListaRevision, CklLista.Items.Count, NoRepetir); //elige un numero
            MostrarImagen(N);
        }
        public void EscogerNumero(List<bool> l, int x, bool r)
        {
            try
            {
                N = Rand.Next(0, x);
                /**
                 * l=> la lista de booleanos
                 * x=> numero maximo al azar para escoger el número
                 * r=> true si para no repetir**/
                if (l[N] == true && r)
                {
                    EscogerNumero(l, x, r);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                BtnMostrarImagen.Text = "Mostrar imagen";
                MessageBox.Show("No se encontraron imagenes para mostrar", "Aviso", MessageBoxButtons.OK);
            }
        }

        private void MostrarImagen(int N)
        {
            int contador = 0;
            PicExpositor.BackgroundImage = ListaImagenes.Images[N];
            try
            {
                CklLista.SetItemChecked(N, true);
                ListaRevision[N] = true;
                foreach (var item in ListaRevision)
                {
                    if (item == true)
                    {
                        contador++;
                    }
                }
                if (contador == ListaRevision.Count)
                {
                    MessageBox.Show($"Todas las imagenes se mostraron ({ListaRevision.Count})");//interpolación
                    GenerarLista();//en este limpia la lista de revision
                    PicExpositor.BackgroundImage = null;
                    //reinicia los checks
                    for (int i = 0; i < CklLista.Items.Count; i++)
                    {
                        CklLista.SetItemChecked(i, false);
                    }
                }

            }
            catch (ArgumentOutOfRangeException)
            { }

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
        private void PonerMusica()
        {
            try
            {
                Estado = true;
                SoundPlayer.PlayLooping();
                LblPorcentaje.Text = VolumenControl.Volume.ToString();
                TrbVolumen.Value = int.Parse(VolumenControl.Volume.ToString());
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Archivo de musica no encontrado, añada el archivo con el nombre de cancion.wav");
                Estado = false;
            }
            catch (NullReferenceException)
            {
                Estado = false;
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
        private void ConvertiraWav()
        {

        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Iniciar();
        }
        private void Limpiar()
        {
            PararMusica();
            ListaRevision.Clear();
            CklLista.Items.Clear();

            if (PicExpositor.Image != null)
            {
                PicExpositor.Image.Dispose();
            }

            PicExpositor.BackColor = Color.White;
            try
            {
                SoundPlayer = new SoundPlayer(Directory.GetFiles(Environment.SpecialFolder.MyMusic.ToString(), "*.wav")[0]);
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("No se encuentran los archivos");
            }
        }

        private void TrbVolumen_Scroll(object sender, EventArgs e)
        {
            try
            {
                VolumenControl.Volume = TrbVolumen.Value;
            }
            catch (NullReferenceException)
            { }
            LblPorcentaje.Text = TrbVolumen.Value.ToString();
        }

        private void ChkRepetir_CheckedChanged(object sender, EventArgs e)
        {
            NoRepetir = (ChkRepetir.Checked == true) ? true : false;
        }
    }
}