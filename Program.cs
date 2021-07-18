using System;
namespace Polybius_Verschlüsselung
{
    class Program
    {
        static void Main()
        {
            Console.Write("Zu verschlüsselnden Text eingeben: ");
            string text = Console.ReadLine();
            Console.Write("Schlüssel eingeben: ");
            string schlüssel = Console.ReadLine();
            int[] ausgabe = Verschlüsselung(text, schlüssel);
            for (int index = 0; index < ausgabe.Length; index++)
            {
                Console.Write(ausgabe[index] + " ");
            }
            Console.ReadKey();
        }
        static int[] Verschlüsselung(string text, string key)
        {
            text = text.ToUpper();
            int[] ergebnis = new int[text.Length];
            string[,] alphabet = CreateKey(key);
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    for (int l = 0; l < 5; l++)
                    {
                        if (alphabet[j, l] == text[i].ToString())
                        {
                            ergebnis[i] = Convert.ToInt32($"{j}{l}");
                        }
                    }
                }
            }
            return ergebnis;
        }
        //static string Entschlüsselung(int[] code, string key)
        //{
        //    string[,] zahlen = { { "0", "1", "2", "3", "4" },
        //                         { "10", "11", "12", "13", "14" },
        //                         { "20", "21", "22", "23", "24" },
        //                         { "30", "31", "32", "33", "34" },
        //                         { "40", "41", "42", "43", "44" },
        //                         { "50", "51", "52", "53", "54" } };
        //    string[,] alphabet = CreateKey(key);
        //    string[] ergebnis = new string[code.Length];
        //    for (int i = 0; i < code.Length; i++)
        //    {
        //        ergebnis[i] = Array.IndexOf(zahlen, code[i]);
        //    }
        //    return ergebnis;
        //}
        static string[,] CreateKey(string schlüssel)
        {
            schlüssel = schlüssel.ToUpper();
            string schlüsselUnique = "";
            for (int i = 0; i < schlüssel.Length; i++)
            {
                if (!schlüsselUnique.Contains(schlüssel[i]))
                {
                    schlüsselUnique = schlüsselUnique + schlüssel[i];
                }
            }
            char[] wortzeichen = schlüsselUnique.ToCharArray();
            char[] wholeABC = "ABCDEFGHIJKLMNOPQRSTUVWXYZ .!?".ToCharArray();
            char[] alphabet = new char[wholeABC.Length];
            int durchrücken = 0;
            for (int i = 0; i < wholeABC.Length; i++)
            {
                if (Array.IndexOf(wortzeichen, wholeABC[i]) == -1)
                {
                    alphabet[i + wortzeichen.Length - durchrücken] = wholeABC[i];
                }
                else
                {
                    durchrücken += 1;
                }
            }
            for (int i = 0; i < wortzeichen.Length; i++)
            {
                alphabet[i] = wortzeichen[i];
            }
            int[] teilermenge = new int[alphabet.Length];
            int teilerwert = 0;
            for (int i = 1; i < alphabet.Length / 2; i++)
            {
                if (Array.IndexOf(teilermenge, i) == -1)
                {
                    if (alphabet.Length % i == 0)
                    {
                    teilermenge[teilerwert] = i;
                    teilerwert++;
                    teilermenge[teilerwert] = alphabet.Length / i;
                    teilerwert++;
                    }
                }
                else
                {
                    i = alphabet.Length;
                }
            }
            int leerwerte = 0;
            for (int i = 0; i < teilermenge.Length; i++)
            {
                if (teilermenge[i] == 0)
                {
                    leerwerte++;
                }
            }
            Array.Resize(ref teilermenge, teilermenge.Length - leerwerte);
            string[,] key = new string[teilermenge[teilermenge.Length - 1], teilermenge[teilermenge.Length - 2]];
            int ABCcursor = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    key[i, j] = Convert.ToString(alphabet[ABCcursor]);
                    ABCcursor += 1;
                }
            }
            return key;
        }
    }
}