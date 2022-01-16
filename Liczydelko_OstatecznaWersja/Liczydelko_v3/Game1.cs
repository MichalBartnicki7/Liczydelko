using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Liczydelko_v3
{

    public partial class Game1 : Game //! Glowna klasa gry, ktora odpowaida za poszczegolne sceny tzn co i kiedy i jak ma byc rysowane. 
    {

        public enum CurrentScene //! podzial gry na sceny
        {
            Menu,
            Graj,
            Opis,
            szsekund,
            ztncz,
            Ranking,
            RankingZtncz,
            RankingSzsekund
        }


        public static DateTime Now { get; }
        DateTime localDate = DateTime.Now;
        Texture2D scifi, sekund, ztncz, graj, OPIS, Ranking, tlo2, rozpocznijponownie, menu, A, B, C;
        Rectangle buttonGraj, buttonOpis, buttonRanking, buttonSekund, buttonztncz, buttonrp, buttonmenu, buttonA, buttonB, buttonC;
        classgraj cg = new classgraj();
        CurrentScene scene = CurrentScene.Menu;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public MouseState mouseState, lastMouseState;
        public Rectangle Cursor;
        Random rnd1_3 = new Random(); //! zmienna sluzaca do losowania miejsca, w ktorym powinna byc poprawna odpowiedz(albo w A, B lub C)
        SoundEffect soundclick;
        SoundEffectInstance soundclickInstance;
        public List<double> ranking_szsekund = new List<double>();
        public List<double> ranking_ztncz = new List<double>();
        public List<string> data_szsekund = new List<String>();
        public List<string> data_ztncz = new List<String>(); //! zmienna zostanie uzyta w przyszlosci, kiedy bede chcial rozwijac projekt. bedzie zapisywala date rankingu
        List<double> tab = new List<double>(); //! przechowuje tymczasowo wyniki z rankingu 60 sekund
        List<double> tab_ztncz = new List<double>(); //! przechowuje tymczasowo wyniki z rankingu zrob to na czas
        List<double> save_szsekund = new List<double>();  //!zapis wyniku z gry do rankingu 60 sekund
        List<double> save_ztncz = new List<double>();//!zapis wyniku z gry do rankingu ztncz
        int temp = 0;
        int lastindex = 0; //! ostatni indeks listy, w celu wyswietlania rankingu 60sekund
        int lastindex_ztncz = 0; //! ostatni indeks listy, w celu wyswietlania rankingu zrob to na czas 

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


        }

        private void checkMenu() //! metoda odpowiedzialna na sceny w MENU
        {
            click c = new click();
            c.Cursor = Cursor;
            c.mouseState = mouseState;
            c.lastMouseState = lastMouseState; // zapis poprzedniej pozycji kursora, zeby nie bylo double-clicka

            if (scene == CurrentScene.Menu)
            {
                if (c.g1_glick(buttonGraj) == true)
                {
                    scene = CurrentScene.Graj;
                    soundclickInstance.Play();

                }
                if (c.g1_glick(buttonOpis) == true)
                {
                    scene = CurrentScene.Opis;
                    soundclickInstance.Play();
                }
                if (c.g1_glick(buttonRanking) == true)
                {
                    scene = CurrentScene.Ranking;
                    soundclickInstance.Play();
                }
            }

            lastMouseState = mouseState;
        }
        private void checkGraj() //! Metoda, ktora jest odpowiedzialna za dzialanie scen w GRAJ
        {
            UpdateCursorPosition();
            click c = new click();
            c.Cursor = Cursor;
            c.mouseState = mouseState;
            c.lastMouseState = lastMouseState;

            if (c.g1_glick(buttonSekund) == true)
            {
                scene = CurrentScene.szsekund;
                soundclickInstance.Play();
            }
            else if (c.g1_glick(buttonztncz) == true)
            {
                scene = CurrentScene.ztncz;
                soundclickInstance.Play();
            }
            lastMouseState = mouseState;
        }

        private void UpdateCursorPosition()
        {
            mouseState = Mouse.GetState();
            Cursor.X = mouseState.X;
            Cursor.Y = mouseState.Y;
        }

        protected override void Initialize() //! w tej metodzie odbywa sie odczyt z pliku
        {
            string[] lines;
            string[] lines_ztncz;
            if (File.Exists("Rankingszsekund.txt") == true) 
            {
                lines = System.IO.File.ReadAllLines(@"Rankingszsekund.txt");
                foreach (string line in lines)
                {
                    ranking_szsekund.Add(Convert.ToDouble(line));
                }
            }
            if (File.Exists("Rankingszsekund.txt") == false)
            {
                File.WriteAllText("Rankingszsekund.txt", "0"); 
            }
            if (File.Exists("Rankingztncz.txt") == true)
            {
                lines_ztncz = System.IO.File.ReadAllLines(@"Rankingztncz.txt");
                foreach (string line in lines_ztncz) 
                {

                    ranking_ztncz.Add(Convert.ToDouble(line));
                }
            }
            if (File.Exists("Rankingztncz.txt") == false)
            {
                File.WriteAllText("Rankingztncz.txt", "0");
            }

            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            File.WriteAllText(".txt", "Test"); 
            for (int i = 0; i < 11; i++) 
            {
                ranking_szsekund.Add(0);
                ranking_ztncz.Add(0);
            }
            /*!
           
             <code>
            @code
             string[] lines;<br/>
            string[] lines_ztncz;<br/>
            if (File.Exists("Rankingszsekund.txt") == true) //jesli jest plik to zapisuje do rankingu szsekund 
            {
                lines = System.IO.File.ReadAllLines(@"Rankingszsekund.txt");
                foreach (string line in lines)// odczyt z pliku i zapis do zmienej rankingu
                {
                    ranking_szsekund.Add(Convert.ToDouble(line));
                }
            }
            if (File.Exists("Rankingszsekund.txt") == false)
            {
                File.WriteAllText("Rankingszsekund.txt", "0"); // jesli nie ma pliku z rankigiem to tworzy plik z 0, zeby poza tablice nie wyszedl
            }
            if (File.Exists("Rankingztncz.txt") == true)
            {
                lines_ztncz = System.IO.File.ReadAllLines(@"Rankingztncz.txt");
                foreach (string line in lines_ztncz) // odczyt z pliku i zapis do zmienej rankingu
                {

                    ranking_ztncz.Add(Convert.ToDouble(line));
                }
            }
            if (File.Exists("Rankingztncz.txt") == false)
            {
                File.WriteAllText("Rankingztncz.txt", "0");
            }

            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            File.WriteAllText(".txt", "Test"); //test
            for (int i = 0; i < 11; i++) //dodaje 0 zeby nie wywalil sie poza tablice jak ktos kliknie ranking
            {
                ranking_szsekund.Add(0);
                ranking_ztncz.Add(0);
            }
            @endcode
            </code>
           
            */
           
            ranking_szsekund.Sort();
            ranking_ztncz.Sort();

            base.Initialize();
        }

        protected override void LoadContent() //! metoda odpowiedzialna za zaldaowanie tekstur 
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            graj = Content.Load<Texture2D>("graj");
            OPIS = Content.Load<Texture2D>("OPIS");
            scifi = Content.Load<Texture2D>("scifi");
            ztncz = Content.Load<Texture2D>("zrobtonaczas");
            sekund = Content.Load<Texture2D>("60sekund");
            tlo2 = Content.Load<Texture2D>("tlo2");
            rozpocznijponownie = Content.Load<Texture2D>("rozpocznijponownie");
            menu = Content.Load<Texture2D>("MENU");
            A = Content.Load<Texture2D>("A");
            B = Content.Load<Texture2D>("B");
            C = Content.Load<Texture2D>("C");
            Ranking = Content.Load<Texture2D>("Ranking");
            soundclick = Content.Load<SoundEffect>("sound");
            soundclickInstance = soundclick.CreateInstance();
        }

        protected override void Update(GameTime gameTime) //! metoda odpowiedzialna za "aktualizacje" scen jak i pozycje/rozmiar przyciskow
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            buttonGraj.X = GraphicsDevice.Viewport.Width / 2 - 75 - buttonGraj.Size.X / 3; // pozycja przycisku
            buttonGraj.Y = GraphicsDevice.Viewport.Height / 2 - 100 - buttonGraj.Size.Y / 3; //pozycja przycisku


            buttonOpis.X = GraphicsDevice.Viewport.Width / 2 - 75 - buttonOpis.Size.X / 3;// pozycja przycisku
            buttonOpis.Y = GraphicsDevice.Viewport.Height / 2 - buttonOpis.Size.Y / 3;// pozycja przycisku


            buttonGraj.Height = GraphicsDevice.Viewport.Height / 7;  // rozmiar przycisku
            buttonGraj.Width = GraphicsDevice.Viewport.Width / 7;  // rozmiar przycisku

            buttonOpis.Height = GraphicsDevice.Viewport.Height / 8;  // rozmiar przycisku
            buttonOpis.Width = GraphicsDevice.Viewport.Width / 7;  // rozmiar przycisku

            buttonRanking.X = buttonGraj.X;
            buttonRanking.Y = buttonOpis.Y + 100;

            buttonRanking.Height = GraphicsDevice.Viewport.Height / 7;
            buttonRanking.Width = GraphicsDevice.Viewport.Width / 7; ;

            buttonSekund.X = buttonGraj.X;
            buttonztncz.X = buttonOpis.X;
            buttonSekund.Y = buttonGraj.Y;
            buttonztncz.Y = buttonOpis.Y;
            buttonSekund.Height = buttonGraj.Height;// 0 
            buttonztncz.Height = buttonOpis.Height;// 0 
            buttonSekund.Width = buttonGraj.Width * 2; //*2
            buttonztncz.Width = buttonOpis.Width * 2; //*2

            if (scene == CurrentScene.Menu)
            {
                UpdateMenu();
                UpdateCursorPosition();
                lastindex = 0;
            }

            else if (scene == CurrentScene.Graj)
            {
                UpdateGraj();
                UpdateCursorPosition();
            }
            else if (scene == CurrentScene.szsekund)
            {
                Updateszsekund();
                UpdateCursorPosition();
            }
            else if (scene == CurrentScene.ztncz)
            {
                Updateztncz();
                UpdateCursorPosition();
            }
            else if (scene == CurrentScene.Opis)
            {
                UpdateOpis();
                UpdateCursorPosition();
            }
            else if (scene == CurrentScene.Ranking)
            {
                UpdateRanking();
                UpdateCursorPosition();
            }
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime) //! metoda odpowiedzialna za pokazywanie poszczegolnych scen
        {
            click c = new click();
            c.Cursor = Cursor;
            c.mouseState = mouseState;

            SpriteFont font;
            font = Content.Load<SpriteFont>("File");


            if (scene == CurrentScene.Menu)
                DrawMenu();
            if (scene == CurrentScene.Graj)
                DrawGraj();
            if (scene == CurrentScene.szsekund)
                Drawszsekund();
            if (scene == CurrentScene.ztncz)
                Drawztncz();
            if (scene == CurrentScene.Opis)
                DrawOpis();
            if (scene == CurrentScene.Ranking)
                DrawRanking();

            base.Draw(gameTime);
        }
    }


}
