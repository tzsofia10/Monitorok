
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
    //Monitor vásár 
    //Egy hardver cég többféle monitort árul. A monitorokról a következő adatokat tároljuk: a monitor gyártója; típusa; mérete; ára; 
    //illetve amelyik kifejezetten játékra való, ott még megadják azt is, hogy gamer. 
    //A méret colban van, az ár nettó és forintban értjük. 
    //Forrásfájl tartalma (a tartalmat használd így, ahogy van, az első sort majd nem kell figyelembe venni beolvasáskor): 

    //Keszleten levo monitorok 
    //Samsung;S24D330H;24;33000  
    //Acer;V227Qbi;21.5;31000  
    //AOC;24G2U;23.8;66000  
    //Samsung;Odyssey G9 C49g95TSSU;49;449989;gamer 
    //LG;25UM58-P;25;56000  
    //Samsung;C27JG50QQU;27.5;91000  

    //Feladatok: 
    //Lehetőleg minden kiírást a főprogram végezzen el. Próbálj minél több kódot újrahasznosítani. 
    //1. Hozz létre egy osztályt a monitorok adatai számára. Olvasd be a fájl tartalmát.
    //2. Írd ki a monitorok összes adatát, soronként egy monitort a képernyőre. A kiírás így nézzen ki: 
    //Gyártó: Samsung; Típus: S24D330H; Méret: 24 col; Nettó ár: 33000 Ft 
    //2. Tárold az osztálypéldányokban a bruttó árat is (ÁFA: 27%, konkrétan a 27-tel számolj, ne 0,27-tel vagy más megoldással.) 
    //3. Tételezzük fel, hogy mindegyik monitorból 15 db van készleten, ez a nyitókészlet. Mekkora a nyitó raktárkészlet bruttó (tehát áfával növelt) értéke?
    //Írj egy metódust, ami meghívásakor kiszámolja a raktárkészlet aktuális bruttó értékét. A főprogram írja ki az értéket. 
    //4. Írd ki egy új fájlba, és a képernyőre az 50.000 Ft feletti nettó értékű monitorok összes adatát (a darabszámmal együtt) úgy,
    //hogy a szöveges adatok nagybetűsek legyenek, illetve az árak ezer forintba legyenek átszámítva.
    //Az ezer forintba átszámítást egy külön függvényben valósítsd meg. 
    //5. Egy vevő keresi a HP EliteDisplay E242 monitort. Írd ki neki a képernyőre, hogy hány darab ilyen van a készleten.
    //Ha nincs a készleten, ajánlj neki egy olyan monitort, aminek az ára az átlaghoz fölülről közelít. Ehhez használd az átlagszámító függvényt (később lesz feladat). 
    //6. Egy újabb vevőt csak az ár érdekli. Írd ki neki a legolcsóbb monitor méretét, és árát. 
    //7. A cég akciót hirdet. A 70.000 Ft fölötti árú Samsung monitorok bruttó árából 5%-ot elenged.
    //Írd ki, hogy mennyit veszítene a cég az akcióval, ha az összes akciós monitort eladná. 
    //8. Írd ki a képernyőre minden monitor esetén, hogy az adott monitor nettó ára a nettó átlag ár alatt van-e, vagy fölötte, 
    //esetleg pontosan egyenlő az átlag árral. Ezt is a főprogram írja ki. 
    //9. Modellezzük, hogy megrohamozták a vevők a boltot. 5 és 15 közötti random számú vásárló 1 vagy 2 random módon kiválasztott monitort vásárol,
    //ezzel csökkentve az eredeti készletet. Írd ki, hogy melyik monitorból mennyi maradt a boltban. 
    //Vigyázz, hogy nulla darab alá ne mehessen a készlet. Ha az adott monitor éppen elfogyott, ajánlj neki egy másikat (lásd fent). 
    //10. Írd ki a képernyőre, hogy a vásárlások után van-e olyan monitor, amelyikből mindegyik elfogyott. 
    //11. Írd ki a gyártókat abc sorrendben a képernyőre. Oldd meg úgy is, hogy a metódus írja ki, és úgy is, hogy a főprogram. 
    //12. Csökkentsd a legdrágább monitor bruttó árát 10%-kal, írd ki ezt az értéket a képernyőre. 

    //A feladatsor egy lehetséges megoldása:

namespace monitorAlapOOP
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. Hozz létre egy osztályt a monitorok adatai számára. Olvasd be a fájl tartalmát. Oldd meg, hogy bármennyi monitort is tudj kezelni. 
            //Tárold az osztálypéldányokban a bruttó árat is (ÁFA: 27 %, konkrétan a 27 - tel számolj.)

            //ez a lista fogja tárolni az osztálypéldányokat, tehát az összes adatot,
            //ami a fájlból származik
            //Ez a hagyományos inicializálás:
            //List<Monitor> monitorok = new List<Monitor>();

            //Ez a fajta inicializálás C# 9.0-tól kezdve használható ilyen esetben:
            List<Monitor> monitorok = new();

            //ezzel is lehet strukturálni a kódot (region):
            #region 1. Feladat: Beolvasás 
            Console.WriteLine("\n1. Feladat");
            foreach (var i in File.ReadAllLines(@"..\..\..\src\monitorok.txt").Skip(1))
            {
                monitorok.Add(new Monitor(i));
            }
            Console.WriteLine("Beolvasás kész.");
            #endregion

            //2. Írd ki az adatokat a képernyőre.
            Console.WriteLine("\n2. Feladat");
            foreach (var i in monitorok)
            {
                Console.WriteLine(i);
            }

            //3. Tételezzük fel, hogy mindegyik monitorból 15 db van készleten, ez a nyitókészlet. Mekkora a nyitó raktárkészlet bruttó (tehát áfával növelt) értéke?
            //Írj egy metódust, ami meghívásakor kiszámolja a raktárkészlet aktuális bruttó értékét. A főprogram írja ki az értéket. 
            Console.WriteLine("\n3. Feladat");
            Console.WriteLine($"A raktárkészlet összértéke {Raktar(monitorok):0} Ft");

            //4. Írd ki egy új fájlba, és a képernyőre az 50000 Ft feletti nettó értékű monitorok összes adatát (a darabszámmal együtt) úgy,
            //hogy a szöveges adatok nagybetűsek legyenek, illetve az árak ezer forintba legyenek átszámítva.
            //Az ezer forintba átszámítást egy külön függvényben valósítsd meg. 

            Console.WriteLine("\n4. Feladat");
            Console.WriteLine("Mentés...");
            FajlbaIras(monitorok);
            Console.WriteLine("Mentve\n");

            Console.WriteLine("\n4. Feladat másképp");
            Console.WriteLine("Mentés másképp...");
            FajlbaIras2(monitorok);
            Console.WriteLine("Mentve\n");

            //5. Egy vevő keresi a HP EliteDisplay E242 monitort. Írd ki neki a képernyőre, hogy hány darab ilyen van a készleten.
            //Ha nincs a készleten, ajánlj neki egy olyan monitort, aminek az ára az átlaghoz fölülről közelít. Ehhez használd az átlagszámító függvényt (később lesz feladat). 
            Console.WriteLine("\n5. Feladat");
            Console.WriteLine("\nAjánlás");
            MonitorKereso(monitorok);

            //6. Egy újabb vevőt csak az ár érdekli. Írd ki neki a legolcsóbb monitor méretét, és árát. 
            Console.WriteLine("\n6. Feladat");
            int index = Legolcsobb(monitorok);
            Console.WriteLine($"Legolcsóbb monitor {monitorok[index].Ar} Ft; {monitorok[index].Meret} col");

            //7. A cég akciót hirdet. A 70000 Ft fölötti árú Samsung monitorok bruttó árából 5%-ot elenged.
            //Írd ki, hogy mennyit veszítene a cég az akcióval, ha az összes akciós monitort eladná.                        
            Console.WriteLine("\n7. Feladat");
            Console.WriteLine($"A cég {SamsungAkcio(monitorok):0} Ft-ot veszítene.");

            //8. Írd ki a képernyőre minden monitor esetén, hogy az adott monitor nettó ára a nettó átlag ár alatt van-e, vagy fölötte, 
            //esetleg pontosan egyenlő az átlag árral. Ezt is a főprogram írja ki. 
            Console.WriteLine("\n8. Feladat");
            AtlagAlattiFolotti(monitorok);

            //9. Modellezzük, hogy megrohamozták a vevők a boltot. 5 és 15 közötti random számú vásárló
            //1 vagy 2 random módon kiválasztott monitort vásárol,
            //ezzel csökkentve az eredeti készletet. Írd ki, hogy melyik monitorból mennyi maradt a boltban. 
            //Vigyázz, hogy nulla darab alá ne mehessen a készlet. Ha az adott monitor éppen elfogyott,
            //ajánlj neki egy másikat (lásd fent). 
            Console.WriteLine("\n9. Feladat");
            Vasarlas(monitorok);

            //10. Írd ki a képernyőre, hogy a vásárlások után van-e olyan monitor, amelyikből mindegyik elfogyott. 
            Console.WriteLine("\n10. Feladat");
            Elfogyott(monitorok);

            //11. Írd ki a gyártókat abc sorrendben a képernyőre. Oldd meg úgy is, hogy a metódus írja ki, és úgy is, hogy a főprogram. 
            Console.WriteLine("\n11. Feladat/1");
            Console.WriteLine("A gyártók abc sorrendben, mindegyikből egy:");
            Console.WriteLine(AbcSorrend1(monitorok));

            Console.WriteLine("\n11. Feladat/2");
            AbcSorrend2(monitorok);

            //12. Csökkentsd a legdrágább monitor bruttó árát 10%-kal, írd ki ezt az értéket a képernyőre. 
            Console.WriteLine("\n12. Feladat");
            Legdragabb(monitorok);

            Console.ReadLine();
        }
        static double NettoAtlag(List<Monitor> adatok)
        {
            //1. megoldás, ez is jó:
            //double atlag = 0;
            //for (int i = 0; i < adatok.Count; i++)
            //{
            //    atlag += adatok[i].Ar;
            //}
            //return atlag / adatok.Count;

            //2. megoldás
            List<double> arLista = new List<double>();
            for (int i = 0; i < adatok.Count; i++)
            {
                arLista.Add(adatok[i].Ar);
            }
            return arLista.Average();
        }

        static int Ajanlo(List<Monitor> adatok)
        {
            List<double> arakTavolsag = new List<double>();
            double atlag = NettoAtlag(adatok);
            for (int i = 0; i < adatok.Count; i++)
            {
                arakTavolsag.Add(adatok[i].Ar - atlag);
            }

            double min = double.MaxValue;
            int monitorIndex = 0;
            for (int i = 0; i < arakTavolsag.Count; i++)
            {
                if (arakTavolsag[i] > 0)
                {
                    if (arakTavolsag[i] < min)
                    {
                        min = arakTavolsag[i];
                        monitorIndex = i;
                    }
                }
            }
            return monitorIndex;
        }

        static string AbcSorrend1(List<Monitor> adatok)
        {
            List<string> gyartok = new List<string>();

            foreach (var item in adatok)
            {
                if (!gyartok.Contains(item.Gyarto))
                {
                    gyartok.Add(item.Gyarto);
                }
            }
            gyartok.Sort();
            return string.Join("\n", gyartok);
        }

        static void AbcSorrend2(List<Monitor> adatok)
        {
            List<string> gyartok = new List<string>();
            for (int i = 0; i < adatok.Count; i++)
            {
                if (!gyartok.Contains(adatok[i].Gyarto))
                {
                    gyartok.Add(adatok[i].Gyarto);
                }
            }
            gyartok.Sort();
            foreach (var item in gyartok)
            {
                Console.WriteLine(item);
            }
        }

        static double Raktar(List<Monitor> adatok)
        {
            double osszeg = 0;
            for (int i = 0; i < adatok.Count; i++)
            {
                osszeg += adatok[i].BruttoAr * adatok[i].Darab;
            }
            return osszeg;
        }

        static void AtlagAlattiFolotti(List<Monitor> adatok)
        {
            double atlagErteke = NettoAtlag(adatok);
            for (int i = 0; i < adatok.Count; i++)
            {
                Console.Write($"{adatok[i].Tipus} monitor ára az ");
                if (adatok[i].Ar < atlagErteke)
                {
                    Console.WriteLine("átlag alatt van.");
                }
                else
                    if (adatok[i].Ar > atlagErteke)
                {
                    Console.WriteLine("átlag fölött van.");
                }
                else
                {
                    Console.WriteLine("átlaggal egyenlő.");
                }
            }
        }

        static int Legolcsobb(List<Monitor> adatok)
        {
            List<double> arak = new List<double>();
            foreach (var item in adatok)
            {
                arak.Add(item.Ar);
            }
            return arak.IndexOf(arak.Min());
        }

        static void Legdragabb(List<Monitor> adatok)
        {
            List<double> arLista = new List<double>();
            for (int i = 0; i < adatok.Count; i++)
            {
                arLista.Add(adatok[i].BruttoAr);
            }
            double legdragabb = arLista.Max();
            double brutto = 0;
            foreach (var item in adatok)
            {
                brutto = item.BruttoAr;
                if (brutto == legdragabb)
                {
                    Console.WriteLine($"{item.Gyarto} {item.Tipus} gép csökkentett bruttó ára {brutto - brutto * 0.1:0} Ft");
                    //itt nem változott meg az eredeti érték, és mindegyik monitorra vonatkozóan kiírjuk
                }
            }
        }

        static void Elfogyott(List<Monitor> adatok)
        {
            bool log = false;
            for (int i = 0; i < adatok.Count; i++)
            {
                if (adatok[i].Darab == 0)
                {
                    Console.WriteLine($"{adatok[i].Gyarto} {adatok[i].Tipus} monitor teljesen elfogyott.");
                    log = true;
                }
            }
            if (!log)
            {
                Console.WriteLine("Minden monitorból van még készleten minimum 1 db.");
            }
        }

        static void Vasarlas(List<Monitor> adatok)
        {
            Random rnd = new Random();
            int randomInt;
            int randomSzam1 = rnd.Next(5, 16); //Teszteld 100 vásárlóval, akkor biztos lesz, amelyik elfogy.
            int randomSzam2 = rnd.Next(1, 3);
            for (int i = 0; i < randomSzam1; i++)
            {
                for (int j = 0; j < randomSzam2; j++)
                {
                    randomInt = rnd.Next(0, adatok.Count);

                    if (adatok[randomInt].Darab > 0)
                    {
                        adatok[randomInt].Darab--;
                        Console.WriteLine($"{adatok[randomInt].Gyarto} {adatok[randomInt].Tipus} monitorból {adatok[randomInt].Darab} db maradt készleten.");
                    }
                    else
                    {
                        int index = Ajanlo(adatok);
                        Console.WriteLine($"A keresett monitor elfogyott. Ajánlott monitor: {adatok[index].Gyarto} {adatok[index].Tipus}");
                    }
                }
            }
        }

        static double SamsungAkcio(List<Monitor> monitorok)
        {
            List<double> hetvenPluszArak = new List<double>();
            for (int i = 0; i < monitorok.Count; i++)
            {
                if (monitorok[i].Ar >= 70000 && monitorok[i].Gyarto == "Samsung")
                {
                    hetvenPluszArak.Add((monitorok[i].BruttoAr - (monitorok[i].BruttoAr * 0.05)) * monitorok[i].Darab);
                }
            }
            return hetvenPluszArak.Sum();
        }

        static void MonitorKereso(List<Monitor> monitorok)
        {
            List<double> arak = new List<double>();
            List<string> adatok = new List<string>();
            double atlagErtek = NettoAtlag(monitorok);
            string kerMonitor = "HP EliteDisplay E242";

            for (int i = 0; i < monitorok.Count; i++)
            {
                adatok.Add(monitorok[i].Gyarto + " " + monitorok[i].Tipus);
            }
            if (adatok.Contains(kerMonitor))
            {
                Console.WriteLine($"{kerMonitor} monitorból {monitorok[adatok.IndexOf(kerMonitor)].Darab} darab van készleten.");
            }
            else
            {
                int index = Ajanlo(monitorok);
                Console.WriteLine($"{monitorok[index].Gyarto} {monitorok[index].Tipus} monitort ajánlom, ennek ára: {monitorok[index].Ar} Ft");
            }
        }

        static void FajlbaIras(List<Monitor> monitorok)
        {
            //ez nem eléggé tiszta kód:
            //StreamWriter sw = new StreamWriter("out.txt");
            //string ki = String.Empty;
            //for (int i = 0; i < monitorok.Count; i++)
            //{
            //    if (monitorok[i].ar > 50000)
            //    {
            //        ki = monitorok[i].kiir();
            //        Console.WriteLine(ki);
            //        sw.WriteLine(ki);
            //        Console.WriteLine();
            //    }
            //}
            //sw.Close();

            using (StreamWriter sw = new StreamWriter("out.txt"))
            {
                string ki = String.Empty;
                for (int i = 0; i < monitorok.Count; i++)
                {
                    if (monitorok[i].Ar > 50000)
                    {
                        ki = monitorok[i].Kiir();
                        Console.WriteLine(ki);
                        sw.WriteLine(ki);
                        Console.WriteLine();
                    }
                }
            }
        }

        static void FajlbaIras2(List<Monitor> m)
        {
            List<string> toFile = new List<string>();
            string kiIr = String.Empty;
            for (int i = 0; i < m.Count; i++)
            {
                if (m[i].Ar > 50000)
                {
                    kiIr = m[i].Kiir();
                    toFile.Add(kiIr);
                    Console.WriteLine(kiIr);
                    Console.WriteLine();
                }
            }
            File.WriteAllLines("out1.txt", toFile);
        }
    }
}


