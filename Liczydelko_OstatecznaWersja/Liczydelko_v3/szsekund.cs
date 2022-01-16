using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;



namespace Liczydelko_v3
{
    public partial class Game1 : Game
    {
        private static int count; //! ilosc poprawnych odpowiedzi
        private static int endfor = 0; //! zmienna, ktora zmienia wartosc kiedy uzytknowik udzieli odpowiedzi, wtedy nastepuje przejscie do kolejnego dzialania
        private static int a, b, c, x, y, which_button;
        private static double poprawne, niepoprawne; //! zmienna odpowiedzialna za ilosc porawnych albo niepoprawnych wartosci
        private static int time = 0; ////! moj timer, program wykonuje sie 60fps wiec potem trzeba dzielic po prostu przez 60 i mamy 1 sekunde :)
        private string jakiedzialanie = "dodawanie";
        int[] wynik = new int[3];

        public void Updateszsekund()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            buttonmenu.X = GraphicsDevice.Viewport.Width / 8 * 7 - 135;
            buttonrp.X = GraphicsDevice.Viewport.Width / 8 * 7 - 380;
            buttonmenu.Y = GraphicsDevice.Viewport.Height / 7 * 6 - 90;
            buttonrp.Y = GraphicsDevice.Viewport.Height / 7 * 6 - 90;
            buttonmenu.Height = GraphicsDevice.Viewport.Height / 4 * (3 / 2) + 110;
            buttonrp.Height = GraphicsDevice.Viewport.Height / 4 * (3 / 2) + 110;
            buttonmenu.Width = GraphicsDevice.Viewport.Height / 4 * (5 / 3) + 150;
            buttonrp.Width = GraphicsDevice.Viewport.Height / 4 * (5 / 3) + 150;

            buttonA.X = GraphicsDevice.Viewport.Width / 4 - 150;
            buttonC.X = GraphicsDevice.Viewport.Width / 4 * 3 - 150;
            buttonB.X = GraphicsDevice.Viewport.Width / 4 * 2 - 150;

            buttonA.Y = GraphicsDevice.Viewport.Height / 4 * 2 - 100;
            buttonC.Y = GraphicsDevice.Viewport.Height / 4 * 2 - 100;
            buttonB.Y = GraphicsDevice.Viewport.Height / 4 * 2 - 100;

            buttonA.Height = GraphicsDevice.Viewport.Height / 3;
            buttonC.Height = GraphicsDevice.Viewport.Height / 3;
            buttonB.Height = GraphicsDevice.Viewport.Height / 3;

            buttonA.Width = GraphicsDevice.Viewport.Height / 3;
            buttonC.Width = GraphicsDevice.Viewport.Height / 3;
            buttonB.Width = GraphicsDevice.Viewport.Height / 3;


            UpdateCursorPosition();
        }

        public void Drawszsekund() //! wszystko odpowiedzialne za rozgrywke gry 60 sekund
        {
            SpriteFont font;
            font = Content.Load<SpriteFont>("File");
            dzialanie d = new dzialanie(count);
            d.rozwiaz();
            click c = new click();
            c.Cursor = Cursor;
            c.mouseState = mouseState;
            c.lastMouseState = lastMouseState;
            double dorankingu = 1;


            // Timer();
            _spriteBatch.Begin();
            time++; 
            

            if (endfor == (endfor % 2)) // Petl
            {
                if (count == 57) count = 20;
                count++;
                jakiedzialanie = d.jakiedzialanie;
                d.count = count;
                x = d.x;
                y = d.y;
                which_button = rnd1_3.Next(0, 3);
                wynik[which_button] = d.wynik; //przypisuje w losowym miejscu tablicy wynik
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
            if (time < 3600) // jesli czas jest mniejszy niz 60s
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
                if (jakiedzialanie == "dzielenie")
                {
                    _spriteBatch.DrawString(font, "Podaj wynik dzialania :  " + y + " : " + x, new Vector2(480, 50), Color.CornflowerBlue);
                }
                if (time < 3000)
                {
                    _spriteBatch.DrawString(font, "Pozostalo Ci  :  " + (60 - time / 60) + " sekund", new Vector2(90, 525), Color.CornflowerBlue); // metoda draw jest wywolywana 60fps, wiec 60 - czas
                }
                if (time > 3000)
                {
                    _spriteBatch.DrawString(font, "Pozostalo Ci  :  " + (60 - time / 60) + " sekund", new Vector2(90, 525), Color.Red); // na czerwony zeby swiecil w ostatnie sekundy
                }
                _spriteBatch.DrawString(font, "Poprawne odpowiedzi  :  " + poprawne, new Vector2(90, 600), Color.CornflowerBlue);
                _spriteBatch.DrawString(font, "Niepooprawne odpowiedzi  :  " + niepoprawne, new Vector2(90, 650), Color.CornflowerBlue);
                _spriteBatch.DrawString(font, "" + wynik[0], new Vector2(buttonA.X + 110, buttonA.Y + 85), Color.CornflowerBlue);
                _spriteBatch.DrawString(font, "" + wynik[1], new Vector2(buttonB.X + 110, buttonB.Y + 85), Color.CornflowerBlue);
                _spriteBatch.DrawString(font, "" + wynik[2], new Vector2(buttonC.X + 110, buttonC.Y + 85), Color.CornflowerBlue);
                temp = 0;
            }
            if (time > 3600)
            {

                string[] dorankingustr = { "" };
                _spriteBatch.DrawString(font, "KONIEC CZASU TWOJ WYNIK TO \n NIEPOPRAWNE ODPOWIEDZI :  " + niepoprawne + "\n POPRAWNE ODPOWIEDZI : " + poprawne, new Vector2(480, 50), Color.Red);
                if (temp == 0)
                {
                    if (niepoprawne > 0)
                    {
                        dorankingu = (double)(poprawne / niepoprawne);
                    }
                    if (niepoprawne == 0)
                    {
                        dorankingu = poprawne;
                    }

                    dorankingustr[0] = dorankingu.ToString("G3"); //przyblizenie do 3 miejsc po przecinku
                  
                    ranking_szsekund.Add(dorankingu);
                    data_szsekund.Add(localDate.ToString());//data, moze kiedys to rozwine
                    File.AppendAllLines("Rankingszsekund.txt", dorankingustr); //zapis do pliku
                    temp++;// zeby if sie wykonal tylko raz, bo time>3600 caly czas leci 
                    lastindex = 0;
                }



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
            }
            if (c.g1_glick(buttonrp) == true)
            {
                soundclickInstance.Play();
                poprawne = 0;
                niepoprawne = 0;
                time = 0;
                count = 1;
                endfor = 1;//nowe losowanie

            }
            if (c.g1_glick(buttonmenu) == true)
            {
                soundclickInstance.Play();
                poprawne = 0;
                niepoprawne = 0;
                time = 0;
                count = 1;
                scene = CurrentScene.Menu;
                endfor = 1;
                
            }

            lastMouseState = mouseState; //zapisanie pozycji poprzedniego kursora, zeby nie bylo double click

            /*! 
             <code>
            @code
                  if (time > 3600)
            {

                string[] dorankingustr = { "" };
                _spriteBatch.DrawString(font, "KONIEC CZASU TWOJ WYNIK TO \n NIEPOPRAWNE ODPOWIEDZI :  " + niepoprawne + "\n POPRAWNE ODPOWIEDZI : " + poprawne, new Vector2(480, 50), Color.Red);
                if (temp == 0)
                {
                    if (niepoprawne > 0)
                    {
                        dorankingu = (double)(poprawne / niepoprawne); do rankingu zostaja zapisane wartosci do 3 miejsca po przecinku
                    }
                    if (niepoprawne == 0)
                    {
                        dorankingu = poprawne;
                    }

                    dorankingustr[0] = dorankingu.ToString("G3"); przyblizenie do 3 miejsc po przecinku
                  
                    ranking_szsekund.Add(dorankingu);
                    data_szsekund.Add(localDate.ToString());//data
                    File.AppendAllLines("Rankingszsekund.txt", dorankingustr); zapis do pliku
                    temp++;// zeby if sie wykonal tylko raz, bo time>3600 caly czas leci 
                    lastindex = 0;
                }



            
              if (c.g1_glick(buttonA) == true)
            {
                soundclickInstance.Play(); Odtworzenie dzwieku klikniecia
                if (wynik[0] == wynik[which_button])
                {
                    poprawne++;
                }
                else niepoprawne++;

                d.count++;
                endfor = 1; ponowne losowanie


            
            }
            @endcode
            </code>
            */
            _spriteBatch.End();

        }
    }
}

