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

                    string auswahl = Console.ReadLine();

                    switch (auswahl)
                    {
                        case "1":
                            // Mit Exception
                            meinVorrat.VorratÄndern("Bier");
                            break;

                        case "2":
                            // Mit Exception
                            meinVorrat.VorratÄndern("Wein");
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
