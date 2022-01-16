using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;



namespace Liczydelko_v3
{

    public partial class Game1 : Game 
    {
        public void UpdateGraj()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            UpdateCursorPosition();
            checkGraj();
        }

       public void DrawGraj()//! Rysuje scene po kliknieciu GRAJ
        {

            _spriteBatch.Begin();

            _spriteBatch.Draw(scifi, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            _spriteBatch.Draw(sekund, buttonSekund, Color.White);
            _spriteBatch.Draw(ztncz, buttonztncz, Color.White);
            
            _spriteBatch.End();
        }
    }
}
