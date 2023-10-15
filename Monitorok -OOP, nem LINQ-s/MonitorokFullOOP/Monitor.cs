
using System;
using System.Collections.Generic;
using System.Linq;

namespace monitorAlapOOP
{
    class Monitor
    {
        public string Gyarto { get; private set; } // ezt a propertyt nem dolgoztuk ki, továbbfejlesztésre vár
        public string Tipus { get; set; } //ha nincs private a set-nél, nem olyan erős a védelem
        public double Meret { get; set; }
        public double Ar { get; set; }
        public bool Gamer { get; set; }
        private double bruttoAr; //ennél az adattagnál kidolgozzuk a propertyt, így ő rejtett láthatóságú       
        public int Darab; //ez egy "sima" adattag, nem property (nem ajánlott ilyet írni)
        const double Afa = 27;

        public double BruttoAr  //ez egy property, ami a bruttoAr adattag "értékére vigyáz"
        {
            get { return bruttoAr; }
            set
            {
                if (value > Ar)
                {
                    bruttoAr = value;
                }
                else
                {
                    throw new Exception("HIBA! A bruttó érték nem nagyobb a nettónál! Az érték nulla marad.");
                }
            }
        }  

        public Monitor(string sor) //ez a konstruktor
        {
            List<string> kezelo = sor.Split(';').ToList();
            this.Gyarto = kezelo[0];
            this.Tipus = kezelo[1];
            this.Meret = Convert.ToDouble(kezelo[2].Replace('.', ','));
            this.Ar = Convert.ToDouble(kezelo[3]);
            if (kezelo.Contains("gamer")) //vagy: kezelo.Count == 5
            {
                this.Gamer = true;
            }
            else
            {
                this.Gamer = false;
            }
            this.Darab = 15;
            //KIVÉTELKEZELÉS (Hibakezelés):
            try //"próbáld meg ezt a részt végrehajtani":
            {
                this.BruttoAr = Convert.ToDouble(kezelo[3]) * (Afa / 100 + 1);

                //ha a hibakezelést akarjuk tesztelni, akkor vegyük ki ezt az egy sort megjegyzésből:
                //brutto = 1; //miután ez hibás értékadás, nem fog végrehajtódni, az érték nulla marad
                //és megjelenik a hibaüzenet
                //figyeljük meg 12. feladatot, ez a hiba ott is jelentkezik ugyanígy
            }
            catch (Exception ex) //"ha volt feldobva hiba futás közben, kapd el, kezeld helyesen és írd ki a feldobott üzenetet"
            {
                Console.WriteLine(ex.Message);
            }
            //itt nem közvetlenül az adattagba írunk, hiszen a <BruttoAr> propertyt használjuk itt,
            //ami a rejtett bruttoAr adattagba ír
            //ha itt rosszul számoljuk a bruttó árat, nem engedi beletenni az adattagba
            //(nyilván itt ugyanaz az ember írja mindkettőt, de megeshet, hogy ez nem így van)
        }


        public string GamerErteke(bool gmr)  //ez egy paraméteres függvény
        {
            if (gmr) return "játék monitor";
            else return "nem játék monitor";
        }

        public override string ToString() //virtuális metódus felülírása
        {
            //így is jó:
            //return "Gyártó: " + Gyarto + "; | Típus: " + Tipus + "; | Méret: " + Meret + " col;" + " | Nettó ár: " + Ar +" Ft\n";
            return $"Gyártó: {Gyarto}; | Típus: {Tipus} | Méret: {Meret} col; | Nettó ár: {Ar} Ft\n";
        }

        public double Ezer(double a)
        {
            return a / 1000;
        }

        public string Kiir() //ez a virtuális metódus helyett van, de meg kell tanulni inkább a virtuális metódust
        {
            return $"Gyártó: {Gyarto.ToUpper()};\nTípus: {Tipus.ToUpper()} ;\nMéret: {Meret} col;\nNettó ár (ezer ft): {Ezer(Ar):0}\nBruttó ár (ezer ft): {Ezer(BruttoAr):0}\nRaktárkészlet: {Darab} darab\n{GamerErteke(Gamer)}";
        }
    }
}


