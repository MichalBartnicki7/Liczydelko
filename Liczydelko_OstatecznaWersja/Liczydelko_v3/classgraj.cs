using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;



namespace Liczydelko_v3
{

    public class classgraj //! rysowanie tla 
    {
        public void Draw(SpriteBatch _spriteBatch,  Texture2D txt, GraphicsDeviceManager _graphics)
        {
            _spriteBatch.Draw(txt, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
        }
    }
}
