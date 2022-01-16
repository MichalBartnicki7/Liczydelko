using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace Liczydelko_v3
{
    public partial class Game1 : Game
    {
        public void UpdateMenu()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
           
            UpdateCursorPosition();
            checkMenu();
        }

        public void DrawMenu() //! Rysuje scene po kliknieciu w jakikolwiek przycisk MENU
        {

            SpriteFont font;
            font = Content.Load<SpriteFont>("File");

            _spriteBatch.Begin();

            _spriteBatch.Draw(scifi, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            _spriteBatch.Draw(graj, buttonGraj, Color.White);

            _spriteBatch.Draw(OPIS, buttonOpis, Color.White);
            _spriteBatch.Draw(Ranking, buttonRanking, Color.White);

            _spriteBatch.End();

        }
    }
}

