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
        private List<bool> ListaDirecciones = new List<bool>();
        private ImageList ListaImagenes;
        private Random rand = new Random();
        private int N;
        private bool estado = false;
        private SoundPlayer soundPlayer;
        private CoreAudioDevice VolumenControl;


        public FrmExpositor()
        {
            InitializeComponent();
            Iniciar();
            PrepararAudios();
        }

        private void PrepararAudios()
        {
            try
            {
                soundPlayer = new SoundPlayer(Directory.GetFiles(Environment.SpecialFolder.MyMusic.ToString(), "*.wav")[0]);
                VolumenControl = new CoreAudioController().DefaultPlaybackDevice;
                LblPorcentaje.Text = VolumenControl.Volume.ToString();
                TrbVolumen.Value = int.Parse(VolumenControl.Volume.ToString());
            }
            catch (IndexOutOfRangeException) { }
            catch (ArgumentOutOfRangeException) { LblPorcentaje.Text = "0"; }
            catch (NullReferenceException) { }
        }
        public void Iniciar()
        {
            RevisarCarpetas();
            CargarImagenes();
            GenerarLista();
            RellenarLista();
        }
        private void RevisarCarpetas()
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

            for (int i = 0; i < path.Length; i++)
            {
                ListaImagenes.Images.Add(i.ToString(), Image.FromFile(path[i]));
            }

        }
        private void CargarImagenes2()
        {
            ListaImagenes = new ImageList//se simplifica y configura de manera mas sencilla
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = new Size(250, 250)
            };
            List<string> path2 = new List<string>();

            for (int i = 0; i < Directory.GetFiles(Environment.SpecialFolder.MyPictures.ToString(), "*.jpg").Length; i++)
            {
                path2.Add(Directory.GetFiles(Environment.SpecialFolder.MyPictures.ToString(), "*.jpg").ToString());
                ListaImagenes.Images.Add(i.ToString(), Image.FromFile(path2[i]));
            }
        }

        private void GenerarLista()
        {
            if (ListaDirecciones.Count == 0)
            {
                for (int i = 0; i < Directory.GetFiles(Environment.SpecialFolder.MyPictures.ToString(), "*.jpg").Length; i++)
                {
                    ListaDirecciones.Add(false);
                }
            }
            else
            {
                for (int i = 0; i < Directory.GetFiles(Environment.SpecialFolder.MyPictures.ToString(), "*.jpg").Length; i++)
                {
                    ListaDirecciones[i] = false;
                }
            }
        }

        private void RellenarLista()
        {
            for (int i = 0; i < ListaDirecciones.Count; i++)
            {
                checkedListBox1.Items.Add("imagen " + (i + 1), CheckState.Unchecked);
            }
        }

        private void BtnMostrarImagen_Click(object sender, EventArgs e)
        {
            if (BtnMostrarImagen.Text == "Mostrar imagen")
            {
                BtnMostrarImagen.Text = "Cambiar imagen";
            }
            GenerarNumero();
            Revisar(N);
        }
        private void GenerarNumero()
        {
            try
            {
                //se revisa el numero del check si esta activo o no, para buscar otro no usado
                do//usar metodo recursivo GenerarNumero();
                {
                    N = rand.Next(0, checkedListBox1.Items.Count);
                    if (checkedListBox1.GetItemCheckState(N) == CheckState.Unchecked)
                    {
                        break;
                    }
                } while (true);
            }
            catch (ArgumentOutOfRangeException)
            {
                BtnMostrarImagen.Text = "Mostrar imagen";
                MessageBox.Show("No se encontraron archivos");
            }
        }

        private void Revisar(int N)
        {
            int contador = 0;
            try
            {
                checkedListBox1.SetItemChecked(N, true);
                ListaDirecciones[N] = true;

                pictureBox1.BackgroundImage = ListaImagenes.Images[N];

                foreach (var item in ListaDirecciones)
                {
                    if (item == true)
                    {
                        contador++;
                    }
                }
                if (contador == ListaDirecciones.Count)
                {
                    MessageBox.Show("Todas las imagenes se mostraron (" + ListaDirecciones.Count + ")");
                    GenerarLista();
                    pictureBox1.BackgroundImage = null;
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        checkedListBox1.SetItemChecked(i, false);
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }
        private void BtnMusica_Click(object sender, EventArgs e)
        {
            if (estado == false)
            {
                PonerMusica();
            }
            else
            {
                estado = false;
                soundPlayer.Stop();
            }
        }
        private void PonerMusica()
        {
            try
            {
                estado = true;
                soundPlayer.PlayLooping();
                LblPorcentaje.Text = VolumenControl.Volume.ToString();
                TrbVolumen.Value = int.Parse(VolumenControl.Volume.ToString());
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Archivo de musica no encontrado, añada el archivo con el nombre de cancion.wav");
                estado = false;
            }
            catch (NullReferenceException)
            {
                estado = false;
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Iniciar();
        }
        private void Limpiar()
        {
            try
            {
                soundPlayer.Stop();
                soundPlayer.Dispose();
                estado = false;
            }
            catch (NullReferenceException)
            { }
            ListaDirecciones.Clear();
            checkedListBox1.Items.Clear();

            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }

            pictureBox1.BackColor = Color.White;
            try
            {
                soundPlayer = new SoundPlayer(Directory.GetFiles(Environment.SpecialFolder.MyMusic.ToString(), "*.wav")[0]);
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
    }
}