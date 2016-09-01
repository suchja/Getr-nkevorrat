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
                            vol = AnwenderNachVolumenFragen();
                            sorte = AnwenderNachSorteFragen();
                            meinVorrat.VorratÄndern(Inhalt.Bier, vol, sorte, anz);
                            break;

                        case "2":
                            // Mit Exception
                            anz = AnwenderNachAnzahlFragen("Wein");
                            vol = AnwenderNachVolumenFragen();
                            sorte = AnwenderNachSorteFragen();
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
        
        static private Sorte sorteInStringKonvertieren(string sorte)
        {
            switch (sorte.ToLower())
            {
                case "kölsch":
                    return Sorte.Kölsch;
                case "pils":
                    return Sorte.Pils;
                case "weizen":
                    return Sorte.Weizen;
                case "weiß":
                    return Sorte.Weiß;
                case "rot":
                    return Sorte.Rot;
                case "rose":
                case "rosé":
                    return Sorte.Rosé;
                case "unbekannt":
                    return Sorte.unbekannt;
                default:
                    Console.WriteLine("Hier liegt ein Fehler vor - Exception fehlt noch");
                    return Sorte.unbekannt;
            }
        }
        
        static private Volumen volumenInStringKonvertieren(String volumen)
        {
            switch (volumen.ToLower())
            { 
                case "250":
                    return Volumen.ml_250;
                case "333":
                    return Volumen.ml_333;
                case "500":
                    return Volumen.ml_500;
                case "750":
                    return Volumen.ml_750;
                case "5000":
                    return Volumen.ml_5000;
                case "unbekannt":
                    return Volumen.unbekannt;
                default:
                    Console.WriteLine("Hier liegt ein Fehler vor - Exception fehlt noch");
                    return Volumen.unbekannt;
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

        static public Sorte AnwenderNachSorteFragen()
        {
            Console.WriteLine("Bitte geben Sie die Sorte ein \nBier: Kölsch, Pils, Weizen \nWein: Rot, Weiß, Rosé\n");
            string sorte = Console.ReadLine();
            return sorteInStringKonvertieren(sorte);
        }

        static public Volumen AnwenderNachVolumenFragen()
        {
            Console.WriteLine("Geben Sie jetzt die Füllmenge ein\nBier: 333, 500, 5000  \nWein: 250, 500, 750\n");
            string füllmenge = Console.ReadLine();
            return volumenInStringKonvertieren(füllmenge);
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
            Console.WriteLine("(E)  Ende ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("Kommando: ");
        }
    }
}
