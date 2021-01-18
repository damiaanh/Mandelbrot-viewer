using System;
using System.Drawing;
using System.Windows.Forms;

namespace Practicum_1
{
    class Practicumclass : Form
    {
        TextBox midden_X_box, midden_Y_box, schaal_box, max_box;
        PictureBox mandel_box;
        CheckBox meerkleur;
        ListBox presets;
        double zoomfactor;
        
        public Practicumclass() // Constructor
        {
            Layout();
        }
        new void Layout()      //Defines the GUI
        {
            //tekent scherm
            Text = "Practicum 2: Mandelbrot";
            Size = new Size(425, 565);
            BackColor = Color.LightGray;

            //invoervak midden x
            Label midden_X_text = new Label();
            midden_X_text.Text = "Midden X";
            midden_X_text.Size = new Size(60, 15);
            midden_X_text.Location = new Point(10, 10);
            Controls.Add(midden_X_text);
            midden_X_box = new TextBox();
            midden_X_box.Text = "0";
            midden_X_box.Size = new Size(100, 20);
            midden_X_box.Location = new Point(10, 30);
            midden_X_box.KeyDown += Enter_gaan;
            Controls.Add(midden_X_box);

            //invoervak midden y
            Label midden_Y_text = new Label();
            midden_Y_text.Text = "Midden Y";
            midden_Y_text.Location = new Point(120, 10);
            midden_Y_text.Size = new Size(60, 15);
            Controls.Add(midden_Y_text);
            midden_Y_box = new TextBox();
            midden_Y_box.Text = "0";
            midden_Y_box.Size = new Size(100, 20);
            midden_Y_box.Location = new Point(120, 30);
            midden_Y_box.KeyDown += Enter_gaan;
            Controls.Add(midden_Y_box);

            //invoervak schaal 
            Label schaal_text = new Label();
            schaal_text.Text = "Schaal";
            schaal_text.Location = new Point(10, 60);
            schaal_text.Size = new Size(60, 15);
            Controls.Add(schaal_text);
            schaal_box = new TextBox();
            schaal_box.Size = new Size(100, 20);
            schaal_box.Location = new Point(10, 80);
            schaal_box.Text = "0.01";
            schaal_box.KeyDown += Enter_gaan;
            Controls.Add(schaal_box);

            //invoervak max (iteration)
            Label max_label = new Label();
            max_label.Text = "Max. iteraties";
            max_label.Location = new Point(120, 60);
            max_label.Size = new Size(100, 15);
            Controls.Add(max_label);
            max_box = new TextBox();
            max_box.Size = new Size(100, 20);
            max_box.Location = new Point(120, 80);
            max_box.Text = "100";
            max_box.KeyDown += Enter_gaan;
            Controls.Add(max_box);

            //Button "OK"
            Button knopje = new Button();
            knopje.Text = "OK";
            knopje.Size = new Size(40, 40);
            knopje.Location = new Point(365, 70);
            knopje.Click += Knopklik;
            Controls.Add(knopje);

            //checkbox voor ander kleurenalgorithme
            Label checklabel = new Label();
            checklabel.Text = "Meer kleur (vertraagt programma!)";
            checklabel.Location = new Point(230,10);
            checklabel.Size = new Size(70, 40);
            Controls.Add(checklabel);
            meerkleur = new CheckBox();
            meerkleur.Size = new Size(30, 30);
            meerkleur.Location = new Point(250, 50);
            Controls.Add(meerkleur);

            //list met presets
            Label presets_text = new Label();
            presets_text.Size = new Size(50, 15);
            presets_text.Location = new Point(305, 10);
            presets_text.Text = "Presets:";
            Controls.Add(presets_text);
            presets = new ListBox();
            presets.Size = new Size(100, 50);
            presets.Location = new Point(305, 25);
            presets.Items.Add("Basis");
            presets.Items.Add("Lange laadtijd");
            presets.Items.Add("Zwart gat");
            presets.Items.Add("Sneeuwvlok");
            presets.Items.Add("Papegaai");
            presets.Click += Presethandler;
            Controls.Add(presets);

            //picturebox + mandel 
            mandel_box = new PictureBox();
            mandel_box.BackColor = Color.Blue;
            mandel_box.Location = new Point(5, 120);
            mandel_box.Width = 400;
            mandel_box.Height = 400;
            zoomfactor = double.Parse(schaal_box.Text);
            mandel_box.Image = Mandelbrot(200, 200, (double)0.01, 100); //mandelbrot waarmee het programma wordt opgestart
            mandel_box.MouseClick += Create_bitmap_click;
            Controls.Add(mandel_box);
        }
        void Enter_gaan(object obj, KeyEventArgs args)  //eventhandler om programma te laten executen bij het drukken op enter na geven van values
        {
            if (args.KeyCode == Keys.Enter)
            {
                Knopklik(obj, args);
            }
        }
        void Presethandler(object obj, EventArgs args)  //Geeft values voor bijbehorende presets en roept Knopklik aan
        {
            string value = presets.SelectedItem.ToString();
            if (value == "Basis")
            {
                midden_X_box.Text = Convert.ToString(0);                       
                midden_Y_box.Text = Convert.ToString(0);
                schaal_box.Text = Convert.ToString(0.01);
                max_box.Text = Convert.ToString(100);
                meerkleur.Checked = false;
                Knopklik(obj, args);
            }
            if (value == "Lange laadtijd")
            {
                midden_X_box.Text = Convert.ToString(-0.1578125);                       
                midden_Y_box.Text = Convert.ToString(1.0328125);
                schaal_box.Text = Convert.ToString(1.5625E-4);
                max_box.Text = Convert.ToString(200);
                meerkleur.Checked = true;
                Knopklik(obj, args);
            }
            if (value == "Zwart gat")
            {
                midden_X_box.Text = Convert.ToString(0.0228341212123635);                        
                midden_Y_box.Text = Convert.ToString(0.81489150911919);
                schaal_box.Text = Convert.ToString(7.45058059692385E-11);
                max_box.Text = Convert.ToString(200);
                meerkleur.Checked = false;
                Knopklik(obj, args);
            }
            if (value == "Sneeuwvlok")
            {
                midden_X_box.Text = Convert.ToString(0.0543181473999023);                       
                midden_Y_box.Text = Convert.ToString(0.829645195007324);
                schaal_box.Text = Convert.ToString(3.814697265625E-08);
                max_box.Text = Convert.ToString(400);
                meerkleur.Checked = true;
                Knopklik(obj, args);
            }
            if (value == "Papegaai")
            {
                midden_X_box.Text = Convert.ToString(-0.1437841796875);
                midden_Y_box.Text = Convert.ToString(0.650770263671875);
                schaal_box.Text = Convert.ToString(1.220703125E-06);
                max_box.Text = Convert.ToString(400);
                meerkleur.Checked = true;
                Knopklik(obj, args);
            }
        }
        void Knopklik(object obj, EventArgs args)         //eventhandler bij knop "OK", tekent Mandelbrot met gegeven waardes
        {
            double midden_X_value = double.Parse(midden_X_box.Text) /zoomfactor;
            double midden_Y_value = double.Parse(midden_Y_box.Text) /zoomfactor;
            double schaal_value = double.Parse(schaal_box.Text);
            double max_box_value = double.Parse(max_box.Text);
            double minX = (mandel_box.Width / 2) - midden_X_value;     //berekent nieuwe middelpunt aan de hand van gegeven punt en mandelboxgrootte
            double minY = (mandel_box.Height / 2) - midden_Y_value *-1;
            mandel_box.Image = Mandelbrot(minX, minY, schaal_value, max_box_value);
            zoomfactor = schaal_value;
        }
        void Create_bitmap_click(object obj, MouseEventArgs args) // eventhandler om mandelbrot te tekenen bij klikken. Zet variablen en roept dan Knopklik aan
        {
            double muisX = args.Location.X;
            double muisY = args.Location.Y;
            zoomfactor /= (double)2;
            double rel_muisX = ((mandel_box.Width /2) - muisX) *zoomfactor;         //geeft de relatieve muisX, dit is de klikwaarde op de bitmap omgezet naar het "mandelpunt" waarop geklikt is
            double rel_muisY = (muisY - (mandel_box.Height/2)) *zoomfactor;
            double min_X_click = (double.Parse(midden_X_box.Text) - rel_muisX);
            double min_Y_click = (double.Parse(midden_Y_box.Text) - rel_muisY);
            midden_X_box.Text = Convert.ToString(min_X_click);                        
            midden_Y_box.Text = Convert.ToString(min_Y_click);
            schaal_box.Text = Convert.ToString(zoomfactor);
            Knopklik(obj, args);
        }
        Bitmap Mandelbrot(double midden_X_coordinaat, double midden_Y_coordinaat, double zoom, double max_iteration)    //defines hoe mandelbrot wordt getekend, krijgt variabelen mee die daarvoor nodig zijn.
        {
            int rood, blauw, groen;
            Bitmap bitmap = new Bitmap(400, 400);

            for (double bitx = 0; bitx < bitmap.Width; bitx++)      //berekent mandelgetal voor elk punt
            {
                for (double bity = 0; bity < bitmap.Height; bity++)
                {
                    double mandelgetal = 0;
                    double a = 0;
                    double b = 0;
                    double afstand = 0;
                    double x = (bitx - midden_X_coordinaat) * zoom;
                    double y = (bity - midden_Y_coordinaat) * zoom;
                    int kleurx = Convert.ToInt32(bitx);
                    int kleury = Convert.ToInt32(bity);               
                    while (mandelgetal <= max_iteration)
                    {
                        double a_nieuw = a * a - b * b + x;
                        double b_nieuw = 2 * a * b + y;
                        double pythasom = (a * a) + (b * b);
                        afstand = Math.Pow(pythasom, 0.5);
                        a = a_nieuw;
                        b = b_nieuw;
                        mandelgetal++;
                        if (meerkleur.Checked)      //tekent map met meer kleur bij aangeklikt boxje
                        {
                            if (afstand > 2)
                            {
                                if (mandelgetal < max_iteration)
                                {
                                    bitmap.SetPixel((int)bitx, (int)bity, Color.FromArgb((int)mandelgetal % 64 * 4, (int)mandelgetal % 32 * 7, (int)mandelgetal % 16 * 14));
                                    break;
                                }
                            }
                            else
                            {
                                bitmap.SetPixel(kleurx, kleury, Color.Black);
                            }
                        }
                        else              //tekent map met standaardkleuren (sneller)
                        {
                            if (afstand > 2) 
                            {
                                if (mandelgetal < (max_iteration / 3))
                                {
                                    groen = (int)Math.Round((mandelgetal / (int)max_iteration) * 255);        //waarde kan max 255 zijn; vandaar (aantal/totaal*225) gedaan. Math.Round om van float integer te maken.
                                    blauw = (int)Math.Round((mandelgetal / (int)max_iteration) * 255);
                                    bitmap.SetPixel(kleurx, kleury, Color.FromArgb(0, groen, blauw));
                                    break;
                                }
                                if (mandelgetal < ((max_iteration / 3) *2) & mandelgetal > (max_iteration / 3))
                                {
                                    rood = (int)Math.Round((mandelgetal / (int)max_iteration) * 255);
                                    groen = (int)Math.Round((mandelgetal / (int)max_iteration) * 255);
                                    bitmap.SetPixel(kleurx, kleury, Color.FromArgb(rood, groen, 0));
                                    break;
                                }
                                if (mandelgetal > ((max_iteration / 3) *2) & mandelgetal < max_iteration)
                                {
                                    rood = (int)Math.Round((mandelgetal / (int)max_iteration) * 255);
                                    blauw = (int)Math.Round((mandelgetal / (int)max_iteration) * 255);
                                    bitmap.SetPixel(kleurx, kleury, Color.FromArgb(rood, 0, blauw));
                                    break;
                                }
                            }
                            if (mandelgetal == max_iteration)
                            {
                                bitmap.SetPixel(kleurx, kleury, Color.Black);
                            }
                        }                     
                    }
                }
            }
            return bitmap;
        }
    }
    class ProgramRun                //start programma
    {
        static void Main()
        {
            Application.Run(new Practicum_1.Practicumclass());
        }
    }
}
