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
        public void Iniciar()
        {
            CrearCarpetas();
            CargarImagenes();
            GenerarLista();
            RellenarLista();
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
            //List<string> path2 = new List<string>();
            //for (int i = 0; i < path.Length; i++)
            //{
            //    ListaImagenes.Images.Add(i.ToString(), Image.FromFile(path[i]));
            //}
            int i = 0;
            foreach (var item in path)
            {
                //path2.Add(Directory.GetFiles(Environment.SpecialFolder.MyPictures.ToString(), "*.jpg")[i]);
                ListaImagenes.Images.Add(i.ToString(), Image.FromFile(path[i]));
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
            EscogerNumero(); //elige un numero
            MostrarImagen(N);
        }
        private void EscogerNumero()
        {
            try
            {
                N = rand.Next(0, CklLista.Items.Count);
                /**Cuando este activo la repeticion este se llama constantemente a si mismo
                 * hasta tener uno que no este en la lista**/
                if (CklLista.GetItemCheckState(N) == CheckState.Checked && ChkRepetir.Checked)
                { //se revisa el numero del check si esta activo o no, para buscar otro no usado
                    EscogerNumero();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                BtnMostrarImagen.Text = "Mostrar imagen";
                MessageBox.Show("No se encontraron archivos");
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
            if (estado == false)
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
        private void PararMusica()
        {
            try
            {
                soundPlayer.Stop();
                soundPlayer.Dispose();
                estado = false;
            }
            catch (NullReferenceException)
            { }
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