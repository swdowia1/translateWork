using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace translator
{
    class Program
    {
        static string eol1 = Environment.NewLine;
        static string eol2 = "";

        static string nameTranslate = "nSysTranslationInfo";

        static void Main(string[] args)
        {
            if (args.Length==1)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("wersja z plikiem parametry");
                Console.WriteLine("\tnazwa root");
                Console.WriteLine("\tnazwa pliku");
                Console.WriteLine("\tod ktorego numeru(domyslnie 1)");
                Console.WriteLine("\n\n\tw pliku kolumny:t");
                Console.WriteLine("\tnazwa kontrolki");
                Console.WriteLine("\twersja polska");
                Console.WriteLine("\twersja angielska");
                Console.WriteLine("\tWłasciwość: (c)aption (t)ext inna");
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n\nPojedyncza property");
                Console.WriteLine("\tnazwa root:");
                Console.WriteLine("\tnazwa kontrolki:");
                Console.WriteLine("\tod ktorego numeru");
                Console.WriteLine("\twersja polska");

                Console.WriteLine("\twersja angielska");
                Console.WriteLine("\tWłasciwość:Caption,Text");

                Console.ResetColor();
                return;
            }
            /*
             
             args

            0 nazwa root
            1 nazwa danych
            2 od ktoregonumeru
             
             */

            int numerstart = 2;
            if (args.Length>2)
            {
                numerstart = int.Parse(args[2]);
            }

            
            eol2 = eol1 + eol1;
            StringBuilder sb = new StringBuilder();

            string root = args[0];
            Localization l = new Localization();
            l.LocalizationUi = new List<Item>();

            List<Item> listaTranslate = new List<Item>();

            if (args.Length < 3)
            {
                string[] linie = File.ReadAllLines(args[1]);
                /*
                 * nazwa controlki
                 * wersja polska 
                 * wersja angielska
                 * property

                 */
                foreach (var item in linie)
                {

                    string[] kolumny = item.Split(';');
                    string property = kolumny[0];
                    string def = kolumny[1];
                    string tr = kolumny[2];
                    int ilosc = kolumny.Length;
                    if (ilosc < 4)
                    {
                        System.Windows.Forms.MessageBox.Show("error :" + item);
                    }
                    for (int i = 3; i < ilosc; i++)
                    {
                        Item a = new Item();
                        a.Name = property;
                        a.Root = root;
                        a.Def = def;
                        a.Translation = tr;
                        string type = kolumny[i].ToLower();
                        switch (type)
                        {
                            case "c":
                                a.Property = "Caption";
                                break;
                            case "t":
                                a.Property = "Text";
                                break;
                            default:
                                break;
                        }
                        listaTranslate.Add(a);

                    }

                }
                
            }
            else
                {
                /*

            args

           0 nazwa root
           1 nazwa danych
           2 od ktoregonumeru
           3 wersja polska
           4 wersja angielska
           5 property;

            */
                Item ItemOne = new Item();
                numerstart = int.Parse(args[2]);
                ItemOne.Root = root;
                ItemOne.Name = args[1];
                ItemOne.Def = args[3];
                ItemOne.Translation = args[4];
                ItemOne.Property = args[5];
                listaTranslate.Add(ItemOne);
                }

           
            File.WriteAllText("tlumaczenia.txt", listaTranslate.Serialize());

            sb = new StringBuilder();
            int numer = numerstart;
            foreach (Item item in listaTranslate)
            {
                sb.Append("Lsi.Nvm.Lib.nSys.Translation.NSysTranslationInfo " + nameTranslate + numer + " = new Lsi.Nvm.Lib.nSys.Translation.NSysTranslationInfo();" + eol1);
                numer++;

            }
            File.WriteAllText("deklaracja.txt", sb.ToString());

            sb = new StringBuilder();
            numer = numerstart;

            foreach (Item item in listaTranslate)
            {
                sb.Append(defTranslate(numer, item));
                sb.Append("this.nSysTranslator.Translations.Add(" + nameTranslate + numer + ");" + eol1);
                sb.Append(eol1);
                numer++;
            }
            File.WriteAllText("definicja.txt", sb.ToString());

            System.Windows.Forms.MessageBox.Show("Test");

        }

        private static string defTranslate(int i, Item _item)
        {
            string naglowek = nameTranslate + i;
            string result = naglowek + ".AutoGenerated = true;" + eol1;
            result += naglowek + ".Default = \"" + _item.Def + "\";" + eol1;
            result += naglowek + ".Description = \"\";" + eol1;
            result += naglowek + ".Name = \"" + _item.Name + "\";" + eol1;
            result += naglowek + ".Property = \"" + _item.Property + "\";" + eol1;
            result += naglowek + ".Root = \"" + _item.Root + "\"; " + eol1;
            return result;
        }


    }
}
