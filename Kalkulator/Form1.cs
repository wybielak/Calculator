using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace Kalkulator
{
    public partial class Form1 : Form
    {
        private bool is_sign_active = false;
        private bool is_equal_active = false;
        private bool is_zero_active = false;
        private bool is_sep_active = false;
        private Decimal x=0, y=0;
        private string sign;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
           /* Graphics mgraphics = e.Graphics;

            Rectangle area = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush lgb = new LinearGradientBrush(area, Color.FromArgb(255,255,255), Color.FromArgb(255, 248, 225), LinearGradientMode.Vertical);
            mgraphics.FillRectangle(lgb, area); */
        }

        private void buttonNum_Click(object sender, EventArgs e)
        {
            string num = (sender as Button).Text;

            if (is_equal_active) button_clear_Click(sender, e);

            if (label_front.Text == "0" || is_sign_active)
            {
                label_front.Text = "";
                is_sign_active = false;
                is_zero_active = false;
                is_sep_active = false;
            }

            if (label_front.Text.Length < 16) label_front.Text += num;
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            if (label_front.Text[label_front.Text.Length - 1] == '.') is_sep_active = false;
            if (label_front.Text.Length > 0 && label_front.Text != "0") label_front.Text = label_front.Text.Substring(0, label_front.Text.Length-1);
            if (label_front.Text.Length == 0) label_front.Text = "0";
        }

        private void buttonSign_Click(object sender, EventArgs e)
        {
            if (is_zero_active) button_clear_Click(sender, e);

            if (label_front.Text[label_front.Text.Length - 1] == '.')
            {
                is_sep_active = false;
                label_front.Text = label_front.Text.Substring(0, label_front.Text.Length - 1);
            }
            is_equal_active = false;

            sign = (sender as Button).Text;

            string x_text = label_front.Text;

            label_back.Text = x_text + " " + sign + " ";
            Decimal.TryParse(x_text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out x);

            is_sign_active = true;
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            label_front.Text = "0";
            label_back.Text = "";
            is_sign_active = false;
            is_equal_active = false;
            is_zero_active = false;
            is_sep_active = false;
            x = 0; y = 0; 
        }

        private void button_sep_Click(object sender, EventArgs e)
        {
            if (is_equal_active) button_clear_Click(sender, e);
            if (is_sep_active == false) label_front.Text += ".";
            if (is_sign_active)
            {
                label_front.Text = "0.";
                is_sign_active = false;
            }
            is_sep_active = true;
        }

        private void button_mystery_Click(object sender, EventArgs e)
        {
            String[] tab = { "Trójkąt o bokach 3, 4, 5 jest nazywany trójkątem egipskim, ponieważ przez Egipcjan był stosowany do wyznaczenia w terenie kąta prostego.",
            "Problem nieskończoności pojawił się już w czasach starożytnej Grecji, a dokładnie w szkole pitagorejskiej, gdzie sądzono, że nieskończonością jest coś, czemu nie przypisze się żadnej wartości.",
            "Kiedy dla Greków liczenie na palcach stało się niewystarczające, wynaleźli abacus, czyli coś podobnego do naszych dzisiejszych liczydeł.",
            "Obwód podstawy piramidy Cheopsa, podzielony przez jej podwójną wysokość, wynosi 3, 1415, czyli liczbę Pi.",
            "Starożytni Sumerowie i Babilończycy używali sześćdziesiątkowego systemu liczbowego. System ten jest używany do dzisiaj do zapisu czasu.",
            "Albert Einstein urodził się w dzień liczby Pi, czyli 14 marca.",
            "Zero to liczba, której nie zapiszemy rzymskimi cyframi."
            };

            Random rnd = new Random();

            MessageBox.Show(tab[rnd.Next(0,7)],"Odpocznij od liczenia! Przeczytaj ciekawostkę!");

        }

        private void button_axis_Click(object sender, EventArgs e)
        {
            Decimal.TryParse(label_front.Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out Decimal tmp3);
            tmp3 *= -1;
            label_front.Text = tmp3.ToString(CultureInfo.GetCultureInfo("en-US"));
        }

        private void button_submit_Click(object sender, EventArgs e)
        {
            if (is_zero_active) button_clear_Click(sender, e);
            else
            {
                if (is_equal_active)
                {
                    Decimal.TryParse(label_front.Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out x);
                    label_back.Text = x.ToString(CultureInfo.GetCultureInfo("en-US")) + " " + sign + " " + y.ToString(CultureInfo.GetCultureInfo("en-US")) + " = ";
                }
                else
                {
                    Decimal.TryParse(label_front.Text, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out y);
                    label_back.Text += y.ToString(CultureInfo.GetCultureInfo("en-US")) + " = ";
                }

                string wynik="0";

                if (sign == "+") wynik = (x + y).ToString(CultureInfo.GetCultureInfo("en-US"));
                else if (sign == "-") wynik = (x - y).ToString(CultureInfo.GetCultureInfo("en-US"));
                else if (sign == "x") wynik = (x * y).ToString(CultureInfo.GetCultureInfo("en-US"));
                else
                {
                    if (y != 0)
                    {
                        wynik = (x / y).ToString(CultureInfo.GetCultureInfo("en-US"));
                    }
                    else
                    {
                        wynik = "Nie dziel przez 0!";
                        is_zero_active = true;
                        is_sep_active = true;
                    }
                }

                if (wynik != "Nie dziel przez 0!")
                {
                    Decimal.TryParse(wynik, NumberStyles.Number, CultureInfo.GetCultureInfo("en-US"), out Decimal tmp);
                    wynik = Math.Round(tmp, 16).ToString(CultureInfo.GetCultureInfo("en-US"));
                }

                label_front.Text = wynik;

                is_sign_active = false;
                is_equal_active = true;
            }
            
        }
    }
}
