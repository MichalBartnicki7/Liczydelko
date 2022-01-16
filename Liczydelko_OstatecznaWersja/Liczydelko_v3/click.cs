using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;



namespace Liczydelko_v3
{
    public class click //! klasa ktora bada, czy ktos kliknal myszka w przycisk
    {
        public Rectangle button, Cursor;
        public MouseState mouseState, lastMouseState;

        public click()
        {
            Cursor = new Rectangle();
            mouseState = new MouseState();
        }


        public bool g1_glick(Rectangle button) //! zwraca true jestli ktos wcisnal przycisk a false jak nie
        {

            if ((button.Intersects(Cursor)))
            {
                if (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                {
                    return true;

                }
            }
            return false;
        }


    }



}
