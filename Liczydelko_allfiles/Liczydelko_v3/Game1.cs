using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Liczydelko_v3
{
    public class Game1 : Game
    {
        Texture2D scifi;
        Texture2D graj;
        Texture2D OPIS;
        Rectangle buttonGraj, buttonOpis;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.IsFullScreen =false;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        MouseState mouseState;
        Rectangle Cursor;
        private void UpdateCursorPosition()
        {
            /* Pozycja kwaratu podąża za pozycją kursora */
            mouseState = Mouse.GetState();
            Cursor.X = mouseState.X; Cursor.Y = mouseState.Y;
        }
       

        protected override void Initialize()
        {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            graj = Content.Load<Texture2D>("graj");
            OPIS = Content.Load<Texture2D> ("OPIS");
            scifi = Content.Load<Texture2D>("scifi");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //wyznaczenie polozenia po nowej klatce
            buttonGraj.X = GraphicsDevice.Viewport.Width / 2-75 - buttonGraj.Size.X/3 ;
            buttonGraj.Y = GraphicsDevice.Viewport.Height / 2-100 - buttonGraj.Size.Y/3;

            buttonOpis.X = GraphicsDevice.Viewport.Width / 2 - 75 - buttonOpis.Size.X / 3;
            buttonOpis.Y = GraphicsDevice.Viewport.Height / 2  - buttonOpis.Size.Y / 3;
            // rozmiar przycisku
            buttonGraj.Height = GraphicsDevice.Viewport.Height/7;
            buttonGraj.Width = GraphicsDevice.Viewport.Width/7;

            buttonOpis.Height = GraphicsDevice.Viewport.Height / 8;
            buttonOpis.Width = GraphicsDevice.Viewport.Width / 8;


            UpdateCursorPosition();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(scifi, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.White);
            //_spriteBatch.Draw(graj, new Vector2((_graphics.PreferredBackBufferWidth/2-75), (_graphics.PreferredBackBufferHeight/2-100)), Color.White);
            _spriteBatch.Draw(graj, buttonGraj, Color.White);
            //_spriteBatch.Draw(OPIS, new Vector2((_graphics.PreferredBackBufferWidth / 2 - 75), (_graphics.PreferredBackBufferHeight / 2)), Color.White);
            _spriteBatch.Draw(OPIS, buttonOpis, Color.White);
            _spriteBatch.End();




            base.Draw(gameTime);
        }
    }
}
