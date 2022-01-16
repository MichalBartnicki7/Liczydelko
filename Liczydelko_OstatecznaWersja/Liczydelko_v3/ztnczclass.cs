using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;
using System.Diagnostics;
using System.IO;



namespace Liczydelko_v3
{
    public partial class Game1 : Game
    {
        /*! \mainpage Dokumentacja HTML Michal Bartnicki </br>
         *  W tej dokumentacji skupie sie na najwazniejszych metodach, zmiennych czy funkcjach. 
         */

        private int Timeout(int count) //! Czas potrzebny na wykonanie dzialania matematycznego, w zaleznosci od poziomu gry
        {
            if (count < 6)
                return 10;
            if (count > 5 & count < 10)
                return 8;
            if (count > 9 && count < 18)
                return 12;
            if (count > 17 && count < 26)
                return 8;
            if (count > 25 & count < 30)
                return 12;
            if (count > 29 & count < 35)
                return 16;
            if (count > 34 & count < 42)
                return 12;
            if (count > 41 & count < 49)
                return 12;
            if (count > 48 & count < 59)
                return 10;
            else return 10;

        }

        public void Updateztncz()
        {
            Updateszsekund();
            UpdateCursorPosition();

        }

        public void Drawztncz()
        {
            SpriteFont font;
            font = Content.Load<SpriteFont>("File");
            dzialanie d = new dzialanie(count);
            d.rozwiaz();
            click c = new click();
            c.Cursor = Cursor;
            c.mouseState = mouseState;
            c.lastMouseState = lastMouseState;


            // Timer();
            _spriteBatch.Begin();
            time++;


            if (endfor == (endfor % 2))
            {
                if (count == 58) count = 20;
                count = count + 2; // algorytm dzialan taki sam, tylko ze trudniejsze szybciej sie pojawia
                d.count = count;
                x = d.x;
                y = d.y;
                which_button = rnd1_3.Next(0, 3);
                wynik[which_button] = d.wynik; //przypisuje w losowym miejscu tablicy wynik
                jakiedzialanie = d.jakiedzialanie;
                if (which_button == 0) // tu zmiana innych odpowiedzi 
                {
                    wynik[1] = d.wynik + rnd1_3.Next(4, 7);
                    wynik[2] = d.wynik + rnd1_3.Next(1, 3);
                }
                if (which_button == 1)
                {
                    wynik[0] = d.wynik + rnd1_3.Next(4, 7);
                    wynik[2] = d.wynik + rnd1_3.Next(1, 3);
                }

                if (which_button == 2)
                {
                    wynik[0] = d.wynik + rnd1_3.Next(4, 7);
                    wynik[1] = d.wynik + rnd1_3.Next(1, 3);
                }

            }


            cg.Draw(_spriteBatch, tlo2, _graphics);
            _spriteBatch.Draw(menu, buttonmenu, Color.White);
            _spriteBatch.Draw(rozpocznijponownie, buttonrp, Color.White);

            if ((time < Timeout(count) * 60) && (niepoprawne == 0))
            {

                _spriteBatch.Draw(A, buttonA, Color.White);
                _spriteBatch.Draw(B, buttonB, Color.White);
                _spriteBatch.Draw(C, buttonC, Color.White);
                if (jakiedzialanie == "dodawanie")
                {
                    _spriteBatch.DrawString(font, "Podaj wynik dzialania :  " + x + " + " + y, new Vector2(480, 50), Color.CornflowerBlue);
                }
                if (jakiedzialanie == "mnozenie")
                {
                    _spriteBatch.DrawString(font, "Podaj wynik dzialania :  " + x + " * " + y, new Vector2(480, 50), Color.CornflowerBlue);
                }
                _spriteBatch.DrawString(font, "Pozostalo Ci  :  " + ((Timeout(count)) - time / 60) + " sekund", new Vector2(90, 525), Color.CornflowerBlue); // metoda draw jest wywolywana 60Hz
                _spriteBatch.DrawString(font, "Poprawne odpowiedzi  :  " + poprawne, new Vector2(90, 600), Color.CornflowerBlue);
                _spriteBatch.DrawString(font, "" + wynik[0], new Vector2(buttonA.X + 110, buttonA.Y + 85), Color.CornflowerBlue);
                _spriteBatch.DrawString(font, "" + wynik[1], new Vector2(buttonB.X + 110, buttonB.Y + 85), Color.CornflowerBlue);
                _spriteBatch.DrawString(font, "" + wynik[2], new Vector2(buttonC.X + 110, buttonC.Y + 85), Color.CornflowerBlue);
                temp = 0; // zeby wchodzilo do petli po wybraniu odpowiedzi tylko raz
            }
            if (time > Timeout(count) * 60 && (niepoprawne == 0))
            {
                string[] dorankingustr = { "" };
                _spriteBatch.DrawString(font, "KONIEC CZASU TWOJ WYNIK TO \n  POPRAWNE ODPOWIEDZI : " + poprawne, new Vector2(480, 50), Color.Red);
                if (temp == 0)
                {
                    ranking_ztncz.Add(poprawne);
                    dorankingustr[0] = poprawne.ToString("G3");
                    File.AppendAllLines("Rankingztncz.txt", dorankingustr); // to tlumacze wszystko w klasie szsekund
                }
                ranking_ztncz.Sort();
                temp++;

            }
            if (niepoprawne == 1)
            {
                _spriteBatch.DrawString(font, "WYBRALES NIEPOPRAWNA ODPOWIEDZ \n  POPRAWNE ODPOWIEDZI : " + poprawne, new Vector2(480, 50), Color.Red);
                string[] dorankingustr = { "" };
                
                if (temp == 0)
                {
                    ranking_ztncz.Add(poprawne);
                    dorankingustr[0] = poprawne.ToString("G3");
                    File.AppendAllLines("Rankingztncz.txt", dorankingustr);
                }
                ranking_ztncz.Sort();
                temp++;
            }


            endfor = 2; // zeby nie zmieniało wartosci co chwile 
            if (c.g1_glick(buttonA) == true)
            {
                soundclickInstance.Play();
                if (wynik[0] == wynik[which_button])
                {
                    poprawne++;
                }
                else niepoprawne++;

                d.count++;
                endfor = 1; // zeby wrocil do ponownego losowania
                time = 0;


            }
            if (c.g1_glick(buttonB) == true)
            {
                soundclickInstance.Play();
                if (wynik[1] == wynik[which_button])
                {
                    poprawne++;
                }
                else niepoprawne++;
                d.count++;
                endfor = 1;
                time = 0;
            }
            if (c.g1_glick(buttonC) == true)
            {
                soundclickInstance.Play();
                if (wynik[2] == wynik[which_button])
                {
                    poprawne++;
                }
                else niepoprawne++;
                d.count++;
                endfor = 1;
                time = 0;
            }
            if (c.g1_glick(buttonrp) == true)
            {
                soundclickInstance.Play();
                poprawne = 0;
                niepoprawne = 0;
                time = 0;
                count = 1;
                endfor = 1;
            }
            if (c.g1_glick(buttonmenu) == true)
            {
                soundclickInstance.Play();
                scene = CurrentScene.Menu;
                poprawne = 0;
                niepoprawne = 0;
                time = 0;
                count = 1;
                endfor = 1;
            }

            lastMouseState = mouseState; //zapisanie pozycji poprzedniego kursora 

            _spriteBatch.End();
        }
    }
}

