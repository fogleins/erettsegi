using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cegesauto
{
    struct REK
    {
        public int nap;
        public string ido;
        public string rsz;
        public int id;
        public int km;
        public int irany;
        public int kintvagybent;
    }
    class Program
    {
        static void Main(string[] args)
        {
            string egysor;
            string[] darab;
            REK[] tomb = new REK[500];
            int n = 0;
            StreamReader beolv = new StreamReader("autok.txt");
            while ((egysor = beolv.ReadLine()) != null)
            {
                darab = egysor.Split(' ');
                tomb[n].nap = int.Parse(darab[0]);
                tomb[n].ido = darab[1];
                tomb[n].rsz = darab[2];
                tomb[n].id = int.Parse(darab[3]);
                tomb[n].km = int.Parse(darab[4]);
                tomb[n].irany = int.Parse(darab[5]);
                if (int.Parse(darab[5]) == 0)
                    tomb[n].kintvagybent++; //ennyi van kint
                else
                    tomb[n].kintvagybent--;
                n++;
            }

            //2
            Console.WriteLine("2. feladat");
            for (int i = n - 1; i >= 0; i--)
            {
                if (tomb[i].irany == 0)
                {
                    Console.WriteLine($"{tomb[i].nap}. nap rendszám: {tomb[i].rsz}");
                    break;
                }
            }

            //3
            string iranystr = "";
            Console.Write("3.feladat\nNap: ");
            int megadottNap = int.Parse(Console.ReadLine());
            Console.WriteLine("Forgalom a(z) " + megadottNap + ". napon:");
            for (int i = 0; i < n; i++)
            {
                if (tomb[i].nap == megadottNap)
                {
                    if (tomb[i].irany == 0)
                    {
                        iranystr = "ki";
                    }
                    else
                    {
                        iranystr = "be";
                    }
                    Console.WriteLine(tomb[i].ido + " " + tomb[i].rsz + " " + tomb[i].id + " " + iranystr);
                }
            }

            //4
            Console.WriteLine("4. feladat");
            int nemHoztakVissza = 0;
            List<string> volt = new List<string>();
            for (int i = n - 1; i >= 0; i--)
            {
                if (!volt.Contains(tomb[i].rsz))
                {
                    if (tomb[i].kintvagybent == 1)
                    {
                        nemHoztakVissza++;
                        volt.Add(tomb[i].rsz);
                    }
                    else
                    {
                        nemHoztakVissza--;
                    }
                }
            }
            Console.WriteLine("A hónap végén " + nemHoztakVissza + " autót nem hoztak vissza.");

            //5
            Console.WriteLine("5. feladat");
            int kezdoallas = 0, befejezoallas = 0;
            string[] autok = { "CEG300", "CEG301", "CEG302", "CEG303", "CEG304", "CEG305", "CEG306", "CEG307", "CEG308", "CEG309" };
            for (int i = 0; i < autok.Length; i++) //autoIndexer
            {
                for (int j = 0; j < n; j++) //első megjelenés
                {
                    if (tomb[j].rsz == autok[i])
                    {
                        kezdoallas = tomb[j].km;
                        break;
                    }
                }
                for (int k = n - 1; k >= 0; k--) //utolsó megjelenés
                {
                    if (tomb[k].rsz == autok[i])
                    {
                        befejezoallas = tomb[k].km;
                        break;
                    }
                }
                int megtettTav = befejezoallas - kezdoallas;
                Console.WriteLine(autok[i] + " " + megtettTav + " km");
            }

            //6
            Console.WriteLine("6. feladat");
            int max = 0, tav = 0, indexer = 0;
            bool talalt = false;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (tomb[i].irany == 0 && tomb[j].rsz == tomb[i].rsz && tomb[j].irany == 1)
                    {
                        tav = tomb[j].km - tomb[i].km;
                        talalt = true;
                        if (max < tav)
                        {
                            max = tav;
                            indexer = i;
                        }
                        if (talalt)
                        {
                            break;
                        }
                    }
                }
            }
            Console.WriteLine($"Leghosszabb út: {max} km, személy: {tomb[indexer].id}");

            //7
            Console.Write("7. feladat\nRendszám: ");
            string megadottRSZ = Console.ReadLine();
            string fajlnev = megadottRSZ + "_menetlevel.txt";
            StreamWriter kiir = new StreamWriter(fajlnev);
            for (int i = 0; i < n; i++)
            {
                if (tomb[i].rsz == megadottRSZ && tomb[i].irany == 0)
                {
                    if (tomb[i].km < 10000) //megfelelő formázáshoz
                        kiir.Write(tomb[i].id + "\t" + tomb[i].nap + ". " + tomb[i].ido + "\t" + tomb[i].km + " km\t\t");
                    else
                        kiir.Write(tomb[i].id + "\t" + tomb[i].nap + ". " + tomb[i].ido + "\t" + tomb[i].km + " km\t");
                }
                else if (tomb[i].rsz == megadottRSZ && tomb[i].irany == 1)
                {
                    if (tomb[i].nap < 10) //megfelelő formázáshoz
                        kiir.WriteLine(tomb[i].nap + ". " + tomb[i].ido + "\t"+ tomb[i].km);
                    else
                        kiir.WriteLine(tomb[i].nap + ". " + tomb[i].ido + "\t" + tomb[i].km);
                }
            }
            kiir.Close();
            Console.WriteLine("Menetlevél kész.");
        }
    }
}
