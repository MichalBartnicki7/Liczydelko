using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Liczydelko_v3
{

    public partial class Game1 : Game
    {

        public void UpdateRanking()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Updateszsekund();
            UpdateCursorPosition();

        }

        public void DrawRanking() //! Metoda, ktora pokazuje ranking
        {

            int posistion = 0;
            SpriteFont font;
            font = Content.Load<SpriteFont>("File");
            click c = new click();
            c.Cursor = Cursor;
            c.mouseState = mouseState;
            c.lastMouseState = lastMouseState;

            _spriteBatch.Begin();

            _spriteBatch.Draw(scifi, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            _spriteBatch.Draw(menu, buttonmenu, Color.White);

            SortowanieRankingu(font, ranking_szsekund, ranking_ztncz, posistion);
            if (c.g1_glick(buttonmenu) == true)
            {
                soundclickInstance.Play(); // dzwiek, przy kliknieciu
                scene = CurrentScene.Menu;

            }
            lastMouseState = mouseState;
            _spriteBatch.End();

        }
        private void SortowanieRankingu(SpriteFont font, List<double> ranking_szsekund, List<double> ranking_ztncz, int posistion) //! metoda, ktora sortuje ranking
        {


            if (lastindex == 0) // tylko raz przypisuje talbice i ostatni index
            {
                ranking_szsekund.Sort();
                ranking_ztncz.Sort();
                tab = ranking_szsekund;
                tab_ztncz = ranking_ztncz;
                lastindex_ztncz = ranking_ztncz.LastIndexOf(ranking_ztncz.Last());
                lastindex = ranking_szsekund.LastIndexOf(ranking_szsekund.Last());
                for (int i = 0; i <= lastindex; i++) // eliminacja  wyników, ktore sie powtarzaja w rankingu
                {
                    if (i < lastindex)
                    {
                        if (tab[i + 1] == tab[i] && tab[i + 1] != 0 && tab[i] != 0)
                        {
                            tab[i + 1] = 0;
                            tab.Sort(); // trzeba na nowo sortowac 
                        }
                    }
                    save_szsekund.Add(0); // dodaje bo nie moze byc pusta tablica, zeby zamienic miejscami liczby

                }
                for (int i = 0; i <= lastindex_ztncz; i++)
                {
                    if (i < lastindex_ztncz)
                    {
                        if (tab_ztncz[i + 1] == tab_ztncz[i] && tab_ztncz[i + 1] != 0 && tab_ztncz[i] != 0 && i < lastindex_ztncz)
                        {
                            tab_ztncz[i + 1] = 0;
                            tab_ztncz.Sort();
                        }
                    }
                    save_ztncz.Add(0);

                }

            }

            for (int i = 0; i <= lastindex; i++)
            {

                save_szsekund[i] = tab[lastindex - i]; // bo sortowanie jest od 1 do inf wiec musze odwrocic zeby bylo od najwiekszej do najmniejszej
            }
            for (int i = 0; i <= lastindex_ztncz; i++)
            {

                save_ztncz[i] = tab_ztncz[lastindex_ztncz - i]; // bo sortowanie jest od 1 do inf wiec musze odwrocic zeby bylo od najwiekszej do najmniejszej
            }

            for (int i = 0; i < 11; i++)
            {
                if (i == 0)
                {
                    _spriteBatch.DrawString(font, "Ranking 60 sekund:\n" + i + ". " + save_szsekund[i].ToString("G3") + "\n ", new Vector2(90, 40 + posistion), Color.DarkRed);
                    _spriteBatch.DrawString(font, "Ranking Zrob To na czas:\n" + i + ". " + save_ztncz[i].ToString("G3") + "\n ", new Vector2(900, 40 + posistion), Color.DarkRed);
                }
                else
                {
                    _spriteBatch.DrawString(font, i + ". " + save_szsekund[i].ToString("G3") + "\n ", new Vector2(90, 60 + posistion), Color.DarkRed);
                    _spriteBatch.DrawString(font, i + ". " + save_ztncz[i].ToString("G3") + "\n ", new Vector2(900, 60 + posistion), Color.DarkRed);

                }
                posistion = posistion + 60; // wyniki sa coraz nizej
            }
            /*!
             *   for (int i = 0; i <= lastindex; i++)  eliminacja  wyników, ktore sie powtarzaja w rankingu
                {
                    if (i < lastindex)
                    {
                        if (tab[i + 1] == tab[i] && tab[i + 1] != 0 && tab[i] != 0)
                        {
                            tab[i + 1] = 0;
                            tab.Sort(); trzeba na nowo sortowac 
                        }
                    }
                    save_szsekund.Add(0); dodaje bo nie moze byc pusta tablica, zeby zamienic miejscami liczby
             for (int i = 0; i <= lastindex; i++)
            {

                save_szsekund[i] = tab[lastindex - i]; odwrocenie wartosci, powinno byc od najwiekszej do najmniejszej
            }
                }
             * */
        }
    }
}
