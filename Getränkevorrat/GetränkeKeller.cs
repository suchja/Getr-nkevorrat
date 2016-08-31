using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getränkevorrat
{

    class GetränkeKeller
    {
        List<Flasche> getränkeListe = new List<Flasche>();
        List<Flasche> sortListe = new List<Flasche>();
        List<string> pufferListe = new List<string>();
        private Flasche flasche = new Flasche(Inhalt.Bier);

        public void VorratÄndern(Inhalt getrAuswahl, Volumen vol, Sorte sorte, int anz)
        {
            Flasche neueFlasche = new Flasche(getrAuswahl);
            neueFlasche.Sorte = sorte;
            neueFlasche.Volumen = vol;

            if (anz > 0)
            {
                for (int i = 0; i < anz; i++)
                {
                    getränkeListe.Add(neueFlasche);
                }
            }
            else if (anz < 0)
            {
                List<Flasche> temp = getränkeListe.FindAll(x => (x.Sorte == neueFlasche.Sorte) && (x.Volumen == neueFlasche.Volumen));
                anz = anz * (-1);
                if (anz > temp.Count())
                    throw new Exception("\n\nDas sollten Sie wissen: Von dieser Sorte sind nur noch " + temp.Count() + " Flaschen im Keller!\n\n");
                getränkeListe.RemoveAll(x => (x.Sorte == neueFlasche.Sorte) && (x.Volumen == neueFlasche.Volumen));

                for (int i = 0; i < anz; i++)
                {
                    temp.RemoveAt(anz - i);
                }
                getränkeListe.AddRange(temp);

                Console.WriteLine("Es wurden {0} Flaschen entnommen", anz);
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Der Bestand wurde nicht verändert!");
                Console.ReadLine();
                return;
            }
            DatenInDateiSchreiben();
        }

        public void DatenInListeEinlesen()
        {
            string[] readText = File.ReadAllLines(@"Beispieldaten\getränkevorrat.txt");
            for (int i = 2; i < readText.Count(); i = i + 3)
            {
                //Flasche neueFlasche = new Flasche(Inhalt.leer);
                Inhalt attribInhalt = (Inhalt)Enum.Parse(typeof(Inhalt), readText[i - 2]);
                Flasche neueFlasche = new Flasche(attribInhalt);
                Sorte attribSorte = (Sorte)Enum.Parse(typeof(Sorte), readText[i - 1]);
                neueFlasche.Sorte = attribSorte;
                Volumen attribVolumen = (Volumen)Enum.Parse(typeof(Volumen), readText[i]);
                neueFlasche.Volumen = attribVolumen;
                getränkeListe.Add(neueFlasche);
            }
        }

        public void DatenInDateiSchreiben()
        {
            getränkeListe.Count();
            foreach (var item in getränkeListe)
            {
                string inh = item.Inhalt.ToString();
                string sort = item.Sorte.ToString();
                string vol = item.Volumen.ToString();
                pufferListe.Add(inh);
                pufferListe.Add(sort);
                pufferListe.Add(vol);
            }
            File.WriteAllLines(@"Beispieldaten\getränkevorrat.txt", pufferListe);
        }

        public void BestandFilternNachSorte()
        {
            Console.WriteLine("Bitte die gewünschte Sorte eingeben (Rot, Weiß, Rosé (oder Rose), Kölsch, Pils, Weizen): ");
            string sor = Console.ReadLine();
            Sorte sort = (Sorte)Enum.Parse(typeof(Sorte), sor);
            List<Flasche> teilLIste = getränkeListe.FindAll(x => x.Sorte == sort);
            foreach (var item in teilLIste)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }

        public void BestandSortieren()
        {
            BestandSort(Sorte.Kölsch);
            BestandSort(Sorte.Pils);
            BestandSort(Sorte.Weizen);
            BestandSort(Sorte.Rot);
            BestandSort(Sorte.Weiß);
            BestandSort(Sorte.Rosé);
            getränkeListe.Clear();
            getränkeListe.AddRange(sortListe);
        }
        private void BestandSort(Sorte sor)
        {

            List<Flasche> tempListe = getränkeListe.FindAll(x => x.Sorte == sor);
            sortListe.AddRange(tempListe);
        }

        public void VorratAnzeigen()
        {
            if (getränkeListe.Count == 0)
            {
                Console.WriteLine("Alle Flaschen sind leer. Bitte Nachschub besorgen!!!");
                Console.ReadLine();
                return;
            }
            foreach (var item in getränkeListe)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }


        public void TestVorrat()
        {
            TestVorratAnlegen(Inhalt.Bier, 10, Sorte.Kölsch, Volumen.ml_333);
            TestVorratAnlegen(Inhalt.Wein, 10, Sorte.Rot, Volumen.ml_500);
            TestVorratAnlegen(Inhalt.Bier, 10, Sorte.Weizen, Volumen.ml_333);
            TestVorratAnlegen(Inhalt.Wein, 10, Sorte.Rot, Volumen.ml_750);
            TestVorratAnlegen(Inhalt.Bier, 10, Sorte.Pils, Volumen.ml_333);
            TestVorratAnlegen(Inhalt.Wein, 10, Sorte.Rot, Volumen.ml_250);
            VorratAnzeigen();
        }
        private void TestVorratAnlegen(Inhalt inh, int anz, Sorte sort, Volumen vol)
        {
            Flasche neueFlasche = new Flasche(inh, sort, vol);
            for (int i = 0; i < anz; i++)
            {
                getränkeListe.Add(neueFlasche);
            }
        }
    }
}
