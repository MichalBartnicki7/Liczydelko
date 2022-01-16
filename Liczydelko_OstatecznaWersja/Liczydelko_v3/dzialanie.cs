using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liczydelko_v3
{
    public class dzialanie //! jest to klasa, ktora jest odpowiedzialna za algorytm tworzenia dzialan matematycznych dla uzytkownika
    {
        // dzialanie matematyczne zalezne od trudnosci
        Random rnd = new Random();

        private List<int> liczby = new List<int>();
        private List<int> rndliczba = new List<int>(); //! randomowa liczby, zakres jest ustawiany pozniej

        public dzialanie(int count)
        {
            this.count = count;
        }

        public int count { get; set; } //! zwieksza sie jak uzytkownik kliknie w poprawna odpowiedz
        public bool poprawnaodp { get; set; } //! przyjmuje true jak jest odpowiedz poprawna
        public int x { get; set; } //! x+y=wynik
        public int y { get; set; }//! x+y=wynik
        public int wynik { get; set; }//! x+y=wynik
        public int temp { get; set; }
        public string jakiedzialanie = "dodawanie";




        public void losowanie() //! Metoda odpowiedialna za losowanie 
        {

            for (int i = 1; i < 60; i++) 
            {

                liczby.Add(i);
                if (i < 11)
                {
                    rndliczba.Add(rnd.Next(1, 10));
                }
                if (i > 10 & i < 31)
                {
                    rndliczba.Add(rnd.Next(10, 100));
                }

                if (i > 30 & i < 42)
                {
                    rndliczba.Add(rnd.Next(100, 200));
                }
                if (i > 41 & i < 60)
                {
                    rndliczba.Add(rnd.Next(2, 9));
                }


            }

        }
        public void rozwiaz()
        {

            losowanie();
            if (count < 10) //dodawnie proste 
            {
                x = liczby[count] + rnd.Next(1, 4);
                y = rndliczba[count];  
                wynik = x + y;
                jakiedzialanie = "dodawanie";
            }
            if (count > 9 & count < 18)// mnozenie proste
            {
                x = liczby[count - 9] + rnd.Next(1, 4);
                y = rndliczba[count - 9]; 
                wynik = x * y;
                jakiedzialanie = "mnozenie";

            }
            if (count > 17 & count < 26)// dodawnie np 7+40
            {
                x = liczby[count - 14] + rnd.Next(1, 3);
                y = rndliczba[count - 6]; 
                wynik = x + y;
                jakiedzialanie = "dodawanie";
            }
            if (count > 25 & count < 30)// dodawnie np 15+60
            {
                x = liczby[count] + rnd.Next(6, 15);
                y = rndliczba[count - 15]; 
                wynik = x + y;
                jakiedzialanie = "dodawanie";
            }
            if (count > 29 & count < 42)//dodawanie 100+120 np 
            {
                x = rndliczba[count + 1];
                y = rndliczba[count]; 
                wynik = x + y;
                jakiedzialanie = "dodawanie";
            }
            if (count > 41)//dzielenie np 25:5 
            {
                x = rndliczba[count];
                y = x * rndliczba[count + 1];
                wynik = rndliczba[count + 1];
                jakiedzialanie = "dzielenie";

            }
            /*!
             * @code
             * losowanie();
            if (count < 10) //dodawnie proste 
            {
                x = liczby[count] + rnd.Next(1, 4);
                y = rndliczba[count];  
                wynik = x + y;
                jakiedzialanie = "dodawanie";
            }
            if (count > 9 & count < 18)// mnozenie proste
            {
                x = liczby[count - 9] + rnd.Next(1, 4);
                y = rndliczba[count - 9]; 
                wynik = x * y;
                jakiedzialanie = "mnozenie";

            }
            if (count > 17 & count < 26)// dodawnie np 7+40
            {
                x = liczby[count - 14] + rnd.Next(1, 3);
                y = rndliczba[count - 6]; 
                wynik = x + y;
                jakiedzialanie = "dodawanie";
            }
            if (count > 25 & count < 30)// dodawnie np 15+60
            {
                x = liczby[count] + rnd.Next(6, 15);
                y = rndliczba[count - 15]; 
                wynik = x + y;
                jakiedzialanie = "dodawanie";
            }
            if (count > 29 & count < 42)//dodawanie 100+120 np 
            {
                x = rndliczba[count + 1];
                y = rndliczba[count]; 
                wynik = x + y;
                jakiedzialanie = "dodawanie";
            }
            if (count > 41)//dzielenie np 25:5 
            {
                x = rndliczba[count];
                y = x * rndliczba[count + 1];
                wynik = rndliczba[count + 1];
                jakiedzialanie = "dzielenie";

            }
            @endcode
             */

        }

    }
}
