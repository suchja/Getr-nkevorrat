using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Getränkevorrat
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                GetränkeKeller meinVorrat = new GetränkeKeller();
                meinVorrat.DatenInListeEinlesen();
                bool weiter = true;

                while (weiter)
                {
                    Console.Clear();
                    //Console.SetWindowSize(140, 62);
                    MenueAnzeigen();
                    Volumen vol;
                    Sorte sorte;
                    int anz;
                    string auswahl = Console.ReadLine();

                    switch (auswahl)
                    {
                        case "1":
                            // Mit Exception
                            anz = AnwenderNachAnzahlFragen("Bier");
                            vol = AnwenderNachVolumenFragen("Bier");
                            sorte = AnwenderNachSorteFragen("Bier");
                            meinVorrat.VorratÄndern(Inhalt.Bier, vol, sorte, anz);
                            break;

                        case "2":
                            // Mit Exception
                            anz = AnwenderNachAnzahlFragen("Wein");
                            vol = AnwenderNachVolumenFragen("Wein");
                            sorte = AnwenderNachSorteFragen("Wein");
                            meinVorrat.VorratÄndern(Inhalt.Wein, vol, sorte, anz);
                            break;

                        case "3":
                            meinVorrat.VorratAnzeigen();
                            break;

                        case "4":
                            meinVorrat.BestandSortieren();
                            break;

                        case "5":
                            // Mit Exception
                            meinVorrat.BestandFilternNachSorte();
                            break;

                        case "6":
                            int anzahl = meinVorrat.BerechneAnzahlFlaschen(flasche => flasche.Sorte == Sorte.Kölsch);
                            Console.WriteLine("Anzahl der Flaschen: {0}", anzahl);
                            Console.ReadLine();
                            break;

                        case "E":
                        case "e":
                            Environment.Exit(0);
                            break;

                        default:
                            break;
                    } 
                }  
            } 

            catch (System.FormatException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Sie können an dieser Stelle nur ganze Zahlen eingeben!");
                Console.ReadLine();
            }

            catch (System.ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(" Bitte nur die folgenden Bezeichnungen verwenden: Rot, Weiß, Rosé(oder Rose), Kölsch, Pils, Weizen");
                Console.ReadLine();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

            finally
            {
                Console.WriteLine("Das Programm wird beendet!");
                Console.ReadLine();
            }
        }

        static public int AnwenderNachAnzahlFragen(string auswahl)
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

        static public Sorte AnwenderNachSorteFragen(string auswahl)
        {
            Sorte sorte;
            String srt;
            if (auswahl == "Bier")
            {
                Console.WriteLine("Bitte geben Sie die Biersorte ein (Kölsch, Pils, Weizen):");
                srt = Console.ReadLine();
                if (srt.ToLower() == "weizen")
                    sorte = Sorte.Weizen;
                else if (srt.ToLower() == "pils")
                    sorte = Sorte.Pils;
                else if (srt.ToLower() == "kölsch")
                    sorte = Sorte.Kölsch;
                else
                {
                    throw new Exception("\n\nFehlerhafte Eingabe! \n\nNur die folgenden Sorten sind erlaubt: Kölsch, Pils, Weizen, kölsch, pils, weizen  \n\n");
                }
            }
            else
            {
                Console.WriteLine("Bitte geben Sie die Weinsorte ein (Rot, Weiß, Rosé):");
                srt = Console.ReadLine();
                if (srt.ToLower() == "rot")
                    sorte = Sorte.Rot;
                else if (srt.ToLower() == "weiß")
                    sorte = Sorte.Weiß;
                else if (srt.ToLower() == "rosé" || srt.ToLower() == "rose")
                    sorte = Sorte.Rosé;
                else
                {
                    throw new Exception("\n\nFehlerhafte Eingabe! \n\nNur die folgenden Sorten sind erlaubt: Rot, Weiß, Rosé, Rose, rot, weiß, rosé, rose   \n\n");
                }
            }
            return sorte;
        }

        static public Volumen AnwenderNachVolumenFragen(string auswahl)
        {
            Volumen größe;
            string füllm;

            if (auswahl == "Bier")
            {
                Console.WriteLine("Geben Sie jetzt die Füllmenge ein (333, 500, 5000): ");
                füllm = Console.ReadLine();
                if (füllm.ToLower() == "333")
                    größe = Volumen.ml_333;
                else if (füllm.ToLower() == "500")
                    größe = Volumen.ml_500;
                else if (füllm.ToLower() == "5000")
                    größe = Volumen.ml_5000;
                else
                {
                    throw new Exception("\n\nFehlerhafte Eingabe! \n\nNur die folgenden Füllmengen sind erlaubt: 333, 500, 5000 \n\n");
                }
            }
            else
            {
                Console.WriteLine("Geben Sie jetzt die Füllmenge ein (250, 500, 750): ");
                füllm = Console.ReadLine();
                if (füllm.ToLower() == "250")
                    größe = Volumen.ml_250;
                else if (füllm.ToLower() == "500")
                    größe = Volumen.ml_500;
                else if (füllm.ToLower() == "750")
                    größe = Volumen.ml_750;
                else
                {
                    throw new Exception("\n\nFehlerhafte Eingabe! \n\nNur die folgenden Füllmengen sind erlaubt: 250, 500, 750 \n\n");
                }
            }
            return größe;
        }

        static public void MenueAnzeigen()
        {
            Console.WriteLine("*****  Getränkevorrat (Bier und Wein)  *****");
            Console.WriteLine(" ");
            Console.WriteLine("(1)  Bestand der Bierflaschen ändern (+ / -)");
            Console.WriteLine(" ");
            Console.WriteLine("(2)  Bestand der Weinflaschen ändern (+ / -)");
            Console.WriteLine(" ");
            Console.WriteLine("(3)  Bestand anzeigen");
            Console.WriteLine(" ");
            Console.WriteLine("(4)  Bestand sortieren");
            Console.WriteLine(" ");
            Console.WriteLine("(5)  Bestand nach Sorte filtern");
            Console.WriteLine(" ");
            Console.WriteLine("(6)  Berechne Flaschenanzahl");
            Console.WriteLine(" ");
            Console.WriteLine("(E)  Ende ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("Kommando: ");
        }
    }
}
