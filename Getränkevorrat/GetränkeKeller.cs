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
        private int anzahl;
        private string getränkeAuswahl = "unbekannt";
        private Inhalt auswahlInhalt = Inhalt.leer;
        private Flasche flasche = new Flasche(Inhalt.Bier);

        public void VorratÄndern(string getrAuswahl)
        {
            getränkeAuswahl = getrAuswahl;
            auswahlInhalt = (Inhalt)Enum.Parse(typeof(Inhalt), getrAuswahl);
            anzahl = AnwenderNachAnzahlFragen(getränkeAuswahl);

            Flasche neueFlasche = new Flasche(auswahlInhalt);

            AnwenderNachSorteFragen(getränkeAuswahl);
            AnwenderNachVolumenFragen(getränkeAuswahl);

            neueFlasche.Sorte = flasche.Sorte;
            neueFlasche.Volumen = flasche.Volumen;

            if (anzahl > 0)
            {
                for (int i = 0; i < anzahl; i++)
                {
                    getränkeListe.Add(neueFlasche);
                }
            }
            else if (anzahl < 0)
            {
                List<Flasche> temp = getränkeListe.FindAll(x => (x.Sorte == neueFlasche.Sorte) && (x.Volumen == neueFlasche.Volumen));
                anzahl = anzahl * (-1);
                if (anzahl > temp.Count())
                    throw new Exception("\n\nDas sollten Sie wissen: Von dieser Sorte sind nur noch " + temp.Count() + " Flaschen im Keller!\n\n");
                getränkeListe.RemoveAll(x => (x.Sorte == neueFlasche.Sorte) && (x.Volumen == neueFlasche.Volumen));

                for (int i = 0; i < anzahl; i++)
                {
                    temp.RemoveAt(anzahl - i);
                }
                getränkeListe.AddRange(temp);

                Console.WriteLine("Es wurden {0} Flaschen entnommen", anzahl);
                Console.ReadLine();
            }
        }


        public void DatenInListeEinlesen()
        {
            string[] readText = File.ReadAllLines(@"c:\temp\getränkevorrat.txt");
            for (int i = 2; i < readText.Count(); i = i + 3)
            {
                Flasche neueFlasche = new Flasche(Inhalt.leer);
                Inhalt attribInhalt = (Inhalt)Enum.Parse(typeof(Inhalt), readText[i - 2]);
                neueFlasche.Inhalt = attribInhalt;
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
            File.WriteAllLines(@"C:\Temp\GetränkeVorrat.txt", pufferListe);
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

        public int AnwenderNachAnzahlFragen(string auswahl)
        {
            if (auswahl == "Bier")
            {
                Console.WriteLine("Sie ändern nun den Bestand der Bierflaschen.");
                Console.WriteLine("Bitte geben Sie die Anzahl ein (+ Bestand erhöhen / - Bestand verringern):");
                return Convert.ToInt32(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("Sie ändern nun den Bestand der Weinflaschen.");
                Console.WriteLine("Bitte geben Sie die Anzahl ein (+ Bestand erhöhen / - Bestand verringern):");
                return Convert.ToInt32(Console.ReadLine());
            }

        }

        public void AnwenderNachSorteFragen(string auswahl)
        {
            if (auswahl == "Bier")
            {
                Console.WriteLine("Bitte geben Sie die Biersorte ein (Kölsch, Pils, Weizen):");
                string sorte = Console.ReadLine();
                if (sorte.ToLower() == "weizen")
                    flasche.Sorte = Sorte.Weizen;
                else if (sorte.ToLower() == "pils")
                    flasche.Sorte = Sorte.Pils;
                else if (sorte.ToLower() == "kölsch")
                    flasche.Sorte = Sorte.Kölsch;
                else
                {
                    throw new Exception("\n\nFehlerhafte Eingabe! \n\nNur die folgenden Sorten sind erlaubt: Kölsch, Pils, Weizen, kölsch, pils, weizen  \n\n");
                }
            }
            else
            {
                Console.WriteLine("Bitte geben Sie die Weinsorte ein (Rot, Weiß, Rosé):");
                string sorte = Console.ReadLine();
                if (sorte.ToLower() == "rot")
                    flasche.Sorte = Sorte.Rot;
                else if (sorte.ToLower() == "weiß")
                    flasche.Sorte = Sorte.Weiß;
                else if (sorte.ToLower() == "rosé" || sorte.ToLower() == "rose")
                    flasche.Sorte = Sorte.Rosé;
                else
                {
                    throw new Exception("\n\nFehlerhafte Eingabe! \n\nNur die folgenden Sorten sind erlaubt: Rot, Weiß, Rosé, Rose, rot, weiß, rosé, rose   \n\n");
                }
            }
        }

        public void AnwenderNachVolumenFragen(string auswahl)
        {
            if (auswahl == "Bier")
            {
                Console.WriteLine("Geben Sie jetzt die Füllmenge ein (333, 500, 5000): ");
                string füllm = Console.ReadLine();
                if (füllm.ToLower() == "333")
                    flasche.Volumen = Volumen.ml_333;
                else if (füllm.ToLower() == "500")
                    flasche.Volumen = Volumen.ml_500;
                else if (füllm.ToLower() == "5000")
                    flasche.Volumen = Volumen.ml_5000;
                else
                {
                    throw new Exception("\n\nFehlerhafte Eingabe! \n\nNur die folgenden Füllmengen sind erlaubt: 333, 500, 5000 \n\n");
                }
            }
            else
            {
                Console.WriteLine("Geben Sie jetzt die Füllmenge ein (250, 500, 750): ");
                string füllm = Console.ReadLine();
                if (füllm.ToLower() == "250")
                    flasche.Volumen = Volumen.ml_250;
                else if (füllm.ToLower() == "500")
                    flasche.Volumen = Volumen.ml_500;
                else if (füllm.ToLower() == "750")
                    flasche.Volumen = Volumen.ml_750;
                else
                {
                    throw new Exception("\n\nFehlerhafte Eingabe! \n\nNur die folgenden Füllmengen sind erlaubt: 250, 500, 750 \n\n");
                }
            }
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
