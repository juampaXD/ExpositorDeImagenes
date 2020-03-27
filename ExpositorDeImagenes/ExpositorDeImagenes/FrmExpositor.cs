﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using AudioSwitcher.AudioApi.CoreAudio; //Nuget para el control del volumen
using System.IO;
using NAudio.Wave;
using System.Linq;

namespace ExpositorDeImagenes

{
    public partial class FrmExpositor : Form
    {
        private List<bool> ListaRevision = new List<bool>();
        private ImageList ListaImagenes;
        private Random Rand = new Random();
        public int N;
        private bool Estado = false, NoRepetir = true; //nos permite revisar si se debe repetir la musica y si esta reproduciendo o no
        private SoundPlayer SoundPlayer;
        private CoreAudioDevice VolumenControl;

        public FrmExpositor()//habilitar TrbVolumen cuando se detecte el archivo y el volumen
        {
            InitializeComponent();
            Iniciar();
        }
        private void Iniciar()
        {
            CrearCarpetas();
            CargarImagenes();
            GenerarLista();
            RellenarLista();
            PrepararAudios();
        }

        private void PrepararAudios()
        {
            try
            {
                ConvertiraWav(Directory.GetFiles(Environment.SpecialFolder.MyMusic.ToString()));
                SoundPlayer = new SoundPlayer(Directory.GetFiles(Environment.SpecialFolder.MyMusic.ToString(), "*.wav")[0]);
                VolumenControl = new CoreAudioController().DefaultPlaybackDevice;
                TrbVolumen.Enabled = true;
                TrbVolumen.Value = int.Parse(VolumenControl.Volume.ToString());
            }
            catch (NullReferenceException) { MessageBox.Show("Dispositivo de reprodución no encontrado o no instalado"); }
            catch (Exception e)
            {
                if (e is ArgumentOutOfRangeException || e is IndexOutOfRangeException)
                {
                    TrbVolumen.Value = 0;
                }
            }
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
            List<string> path = Directory.GetFiles(Environment.SpecialFolder.MyPictures.ToString(), "*.jpg").ToList<string>();
            int i = 0;
            foreach (var item in path)
            {
                ListaImagenes.Images.Add(i.ToString(), Image.FromFile(item));
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
                CklLista.Items.Add(Directory.GetFiles(Environment.SpecialFolder.MyPictures.ToString(), "*.jpg")[i].Split('\\')[1] + (i + 1), CheckState.Unchecked);
            }
        }

        private void BtnMostrarImagen_Click(object sender, EventArgs e)
        {
            if (BtnMostrarImagen.Text == "Mostrar imagen")
            {
                BtnMostrarImagen.Text = "Cambiar imagen";
            }
            EscogerNumero(CklLista.Items.Count); //elige un numero
            MostrarImagen(N);
        }
        public void EscogerNumero(int x)
        {
            try
            {
                if (NoRepetir)
                {
                    do
                    {
                        N = Rand.Next(0, x);
                        if (ListaRevision[N].Equals(false)) {
                            break;
                        }
                    } while (true);
                }
                else {
                    N = Rand.Next(0, x);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                BtnMostrarImagen.Text = "Mostrar imagen";
            }
        }

        private void MostrarImagen(int N)
        {
            int contador = 0;
            try
            {
                PicExpositor.BackgroundImage = ListaImagenes.Images[N];
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
            { MessageBox.Show("imagenes no encontradas"); }

        }
        private void BtnMusica_Click(object sender, EventArgs e)
        {//añadir un buscador
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
                TrbVolumen.Value = VolumenControl.Volume;
            }
            catch (Exception ex)
            {
                if (ex is NullReferenceException || ex is FileNotFoundException)
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
                TrbVolumen.Value = 0;
            }
            catch (NullReferenceException)
            { }
        }
        private void ConvertiraWav(string[] x)
        {
            if (x[0].ToLower().Contains(".mp3"))
            {
                Mp3FileReader mp3 = new Mp3FileReader(x[0]);
                WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3);
                WaveFileWriter.CreateWaveFile("Musica.wav", pcm);//crea un archivo wav
                File.Move(Application.StartupPath.ToString() + @"\Musica.wav", Environment.SpecialFolder.MyMusic.ToString() + @"\Musica.wav");
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            Actulizar();
        }
        private void Actulizar()
        {
            PararMusica();//paramos en caso de que este reproduciendo
            ListaRevision.Clear();
            CklLista.Items.Clear();

            if (PicExpositor.Image != null)
            {
                PicExpositor.Image.Dispose();
            }
            GenerarLista();
            RellenarLista();
            PicExpositor.BackColor = Color.White;
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

            LblPorcentaje.Text = TrbVolumen.Value + "%";
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

        private void CklLista_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                if (CklLista.SelectedItem.Equals(true))

                    ListaRevision[CklLista.SelectedIndex] = false;
                else
                    ListaRevision[CklLista.SelectedIndex] = true;
            }
            catch (NullReferenceException)
            {
            }
        }

        private void ChkRepetir_CheckedChanged(object sender, EventArgs e)
        {
            NoRepetir = (ChkRepetir.Checked.Equals(true)) ? true : false;
        }
    }
}