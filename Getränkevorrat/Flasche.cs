using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getränkevorrat
{
    public enum Inhalt
    { Wein, Bier, leer }

    public enum Sorte
    { Rot, Weiß, Rosé, Kölsch, Pils, Weizen, unbekannt }

    public enum Volumen
    { ml_250, ml_500, ml_750, ml_333, ml_5000, unbekannt }

    class Flasche
    {
        public Inhalt Inhalt { get; set; }
        public Sorte Sorte { get; set; }
        public Volumen Volumen { get; set; }

        public Flasche(Inhalt inhalt)
        {
            Inhalt = inhalt;
        }

        public Flasche(Inhalt inhalt, Sorte sorte, Volumen volumen) : this(inhalt)
        {
            Sorte = sorte;
            Volumen = volumen;
        }

        public override string ToString()
        {
            return Inhalt + " " + Sorte + " " + Volumen;
        }

    }
}
