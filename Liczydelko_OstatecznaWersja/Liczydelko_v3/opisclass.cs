using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;



namespace Liczydelko_v3
{

    public partial class Game1 : Game
    {

        public void UpdateOpis()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Updateszsekund();
            buttonSekund.Y = buttonSekund.Y - 200;


            UpdateCursorPosition();
        }

        public void DrawOpis() //! Metoda, ktora wyswietla scene opis, tzn opisuje gre
        {
            SpriteFont font;
            font = Content.Load<SpriteFont>("File");
            click c = new click();
            c.Cursor = Cursor;
            c.mouseState = mouseState;
            c.lastMouseState = lastMouseState;

            _spriteBatch.Begin();

            _spriteBatch.Draw(scifi, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            _spriteBatch.Draw(sekund, buttonSekund, Color.White);
            _spriteBatch.Draw(ztncz, buttonztncz, Color.White);
            _spriteBatch.Draw(menu, buttonmenu, Color.White);
            _spriteBatch.DrawString(font, "Tryb ten polega na wykonaniu jak najwiekszej ilosci dzialan w ciagu 60 sekund.\n Poziom trudnosci dzialan wzrasta wraz z udzielona  " +
                "odpowiedzia. W rankingu \nliczy sie stosunek poprawnych odpowiedzi do niepoprawnych.",
                new Vector2(buttonSekund.X - 400, (buttonSekund.Y + 100)), Color.Chocolate);
            _spriteBatch.DrawString(font, "W tym trybie nalezy wykonac dzialanie w okreslonym czasie. Poziom trudnosci\n dzialania jak i czas zmieniaja sie" +
                " wraz z udzielona odpowiedzia. W rankingu jest\n zapisywana ilosc poprawnych odpowiedzi.",
                new Vector2(buttonSekund.X - 400, buttonztncz.Y + 100), Color.Chocolate);


            if (c.g1_glick(buttonmenu) == true)
            {
                soundclickInstance.Play();
                scene = CurrentScene.Menu;
            }

            lastMouseState = mouseState;
            _spriteBatch.End();

        }
    }
}
