#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Security.Permissions;
#endregion

/// MainGame-class by Marvin Karaschewski
/// Uses the XNA-Game Class
/// Represents the actual Game

namespace MemoryKidz
{
    /// <summary>
    /// This is the Main-Game-Class which handles all Operations
    /// </summary>
    public class MainGame : Game
    {
        
        #region Interactive Background Elements

        // Spritebatch for all elements
        public static SpriteBatch spriteBatch;

        // Screen resolution // STATIC // OBSOLETE
        private int screenWidth = 1920; // screenHeight = 1080;

        // All textures
        public static Texture2D skyTexture;
        public static Vector2 skyPosition;

        public static Texture2D grassTexture;
        public static Vector2 grassPosition;

        public static Texture2D flowerTexture;
        public static Vector2 flowerPosition;

        public static Texture2D sunTexture;
        public static Vector2 sunPosition;

        public static Texture2D sunraysTexture;
        public static Vector2 sunraysOrigin;
        public static Vector2 sunraysPosition;

        public static Texture2D cloudTexture;

        // Rotationtype for the sunrays
        public static float RotationAngle;

        // Random posititioning
        public static Random randomCloudX1 = new Random();
        public static Random randomCloudY1 = new Random();
        public static Vector2 cloudPosition1 = new Vector2();

        public static Random randomCloudX2 = new Random();
        public static Random randomCloudY2 = new Random();
        public static Vector2 cloudPosition2 = new Vector2();

        public static Random randomCloudX3 = new Random();
        public static Random randomCloudY3 = new Random();
        public static Vector2 cloudPosition3 = new Vector2();

        public static Random randomCloudX4 = new Random();
        public static Random randomCloudY4 = new Random();
        public static Vector2 cloudPosition4 = new Vector2();

        public static Random randomCloudX5 = new Random();
        public static Random randomCloudY5 = new Random();
        public static Vector2 cloudPosition5 = new Vector2();
        
        #endregion

        GraphicsDeviceManager graphics;
        // SpriteBatch spriteBatch;
        SpriteFont font;
        SpriteFont scoreFont;

        /// <summary>
        /// Custom Cursor by Gerben Hofman
        /// </summary>
        public static Texture2D customCursor;
        public static Vector2 cursorOrigin = new Vector2(25, 25);

        // Lists of all Buttons for all Gamestates
        List<Button> btnListMainMenuNoSession;
        List<Button> btnListMainMenuSession;
        List<Button> btnListOptionsMenu;
        List<Button> btnListChooseDifficulty;
        List<Button> btnListRunning;
        List<Button> btnListEndgame;
        List<Button> btnListHighscore;

        #region Button Instances Definitions

        // Button(s) for the Highscore
        Button btnDirectRetry;
        Button btnViewPicture1;
        Button btnViewPicture2;
        Button btnViewPicture3;
        Button btnViewPicture4;
        Button btnViewPicture5;
        Button btnViewPicture6;
        Button btnViewPicture7;
        Button btnViewPicture8;
        Button btnViewPicture9;
        Button btnViewPicture10;

        // Button(s) for the actual game
        Button btnBackToMenu;

        // Buttons for MainMenu
        Button btnNewGame;
        Button btnOptions;
        Button btnQuitGame;

        Button btnResume;
        Button btnQuitSession;

        // Buttons for OptionsMenu
        Button btnOptionsBack;
        Button btnOptionsSound;
        Button btnOptionsMusic;

        // Buttons for ChooseDifficulty
        Button btnOptionsDifficultyEasy;
        Button btnOptionsDifficultyMedium;
        Button btnOptionsDifficultyHard;

        // Buttons for the Endgame-Screen
        Button btnPhotoYes;
        Button btnPhotoNo;

        #endregion

        // Screencaps
        int hZero;
        int bZero;

        // Button-Sizes
        int btnWidthNormal;
        int btnHeightNormal;
        int btnWidthSmall;
        int btnHeightSmall;
        int btnWidthHalf;
        int btnHeightHalf;
        int btnHeightPhotoViewCommon;
        int btnWidthPhotoViewCommon;

        // List of Rectangles for position and outlines for the DeatailViewButtons
        List<Rectangle> btnViewPostions;        

        /// <summary>
        /// Marks the current state of the game. 
        /// </summary>
        private GameState gameState = GameState.None;

        // All Possible GameStates
        private Startscreen startscreen = null;
        private MainMenuNoSession mainmenuNoSession = null;
        private MainMenuSession mainmenuSession = null;
        private ChooseDifficulty choosedifficulty = null;
        private Running running = null;
        private OptionMenu optionmenu = null;
        private Highscore highscore = null;
        private Endgame endgame = null;
        private ImageDetailView imagedetailview = null;

        public MainGame(): base()
        {
            graphics = new GraphicsDeviceManager(this);

            // Set device to Fullscreen-Mode
            graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            hZero = graphics.PreferredBackBufferHeight;
            bZero = graphics.PreferredBackBufferWidth;

            btnViewPostions = Extension.GenerateViewButtonPositions(ref graphics);
            GameSpecs.SoundOn = true;

            btnHeightPhotoViewCommon = (int)(btnViewPostions[3].Height * 0.86);
            btnWidthPhotoViewCommon = (int)(btnViewPostions[3].Width * 0.97);

            GameSpecs.Difficulty = 1;
            GameSpecs.SoundOn = true;
            GameSpecs.MusicOn = true;

            btnHeightNormal = 150;
            btnWidthNormal = 350;

            btnHeightHalf = 150;
            btnWidthHalf = 150;

            btnHeightSmall = 110;
            btnWidthSmall = 110;

            #region Button Initializations

            /// <summary>
            /// Creating new Button-Objects
            /// </summary>
            
            btnNewGame = new Button(this, new Vector2((bZero / 2) - (btnWidthNormal / 2), hZero * (float)0.2), new Rectangle(0, 0, btnWidthNormal, btnHeightNormal));
            btnNewGame.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/NewGame.png"));

            btnOptions = new Button(this, new Vector2((bZero / 2) - (btnWidthNormal / 2), hZero * (float)0.45), new Rectangle(0, 0, btnWidthNormal, btnHeightNormal));
            btnOptions.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/Options.png"));

            btnQuitGame = new Button(this, new Vector2((bZero / 2) - (btnWidthNormal / 2), hZero * (float)0.7), new Rectangle(0, 0, btnWidthNormal, btnHeightNormal));
            btnQuitGame.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/Quit.png"));

            btnOptionsBack = new Button(this, new Vector2((bZero / 2) - (btnWidthNormal / 2), hZero * (float)0.2), new Rectangle(0, 0, btnWidthNormal, btnHeightNormal));
            btnOptionsBack.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/Back.png"));

            btnOptionsSound = new Button(this, new Vector2((bZero / 2) - (btnWidthNormal / 2), hZero * (float)0.45), new Rectangle(0, 0, btnWidthHalf, btnHeightHalf));
            btnOptionsSound.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/Back.png"));

            btnOptionsMusic = new Button(this, new Vector2((bZero / 2) - (btnWidthNormal / 2) + (50 + btnWidthHalf), hZero * (float)0.45), new Rectangle(0, 0, btnWidthHalf, btnHeightHalf));
            btnOptionsMusic.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/Back.png"));

            btnResume = new Button(this, new Vector2((bZero / 2) - (btnWidthNormal / 2), hZero * (float)0.2), new Rectangle(0, 0, btnWidthNormal, btnHeightNormal));
            btnResume.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/Resume.png"));

            btnQuitSession = new Button(this, new Vector2((bZero / 2) - (btnWidthNormal / 2), hZero * (float)0.7), new Rectangle(0, 0, btnWidthNormal, btnHeightNormal));
            btnQuitSession.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/Quit.png"));

            btnOptionsDifficultyEasy = new Button(this, new Vector2((bZero / 2) - (btnWidthNormal / 2), hZero * (float)0.45), new Rectangle(0, 0, btnWidthSmall, btnHeightSmall));
            btnOptionsDifficultyEasy.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/Easy.png"));

            btnOptionsDifficultyMedium = new Button(this, new Vector2(((bZero / 2) - (btnWidthNormal / 2)) + (btnWidthSmall + 9), hZero * (float)0.45), new Rectangle(0, 0, btnWidthSmall, btnHeightSmall));
            btnOptionsDifficultyMedium.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/Medium.png"));

            btnOptionsDifficultyHard = new Button(this, new Vector2(((bZero / 2) - (btnWidthNormal / 2)) + (2*btnWidthSmall + 18), hZero * (float)0.45), new Rectangle(0, 0, btnWidthSmall, btnHeightSmall));
            btnOptionsDifficultyHard.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/Hard.png"));

            btnBackToMenu = new Button(this, new Vector2((float)((bZero * 0.025)), hZero * (float)0.3), new Rectangle(0, 0, btnWidthSmall, btnHeightSmall));
            btnBackToMenu.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/BackArrow.png"));

            btnDirectRetry = new Button(this, new Vector2((float)((bZero * 0.025)), hZero * (float)0.55), new Rectangle(0, 0, btnWidthSmall, btnHeightSmall));
            btnDirectRetry.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/BackArrow.png"));

            btnPhotoYes = new Button(this, new Vector2((int)(bZero * 0.015), hZero * (float)0.5), new Rectangle(0, 0, btnWidthNormal, btnHeightNormal));
            btnPhotoYes.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/yes.png"));

            btnPhotoNo = new Button(this, new Vector2(((bZero - btnWidthNormal) - (int)(bZero * 0.015)), hZero * (float)0.5), new Rectangle(0, 0, btnWidthNormal, btnHeightNormal));
            btnPhotoNo.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/no.png"));
            
            /// PICTURE-VIEWING BUTTONS
            btnViewPicture1 = new Button(this, new Vector2(btnViewPostions[0].X, btnViewPostions[0].Y), new Rectangle(0, 0, btnViewPostions[0].Width, btnViewPostions[0].Height));
            btnViewPicture1.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Others/placeholder_player.png"));

            btnViewPicture2 = new Button(this, new Vector2(btnViewPostions[1].X, btnViewPostions[1].Y), new Rectangle(0, 0, btnWidthPhotoViewCommon, btnHeightPhotoViewCommon));
            btnViewPicture2.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/button_photo.png"));

            btnViewPicture3 = new Button(this, new Vector2(btnViewPostions[2].X, btnViewPostions[2].Y), new Rectangle(0, 0, btnWidthPhotoViewCommon, btnHeightPhotoViewCommon));
            btnViewPicture3.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/button_photo.png"));

            btnViewPicture4 = new Button(this, new Vector2(btnViewPostions[3].X, btnViewPostions[3].Y), new Rectangle(0, 0, btnWidthPhotoViewCommon, btnHeightPhotoViewCommon));
            btnViewPicture4.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/button_photo.png"));

            btnViewPicture5 = new Button(this, new Vector2(btnViewPostions[4].X, btnViewPostions[4].Y), new Rectangle(0, 0, btnWidthPhotoViewCommon, btnHeightPhotoViewCommon));
            btnViewPicture5.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/button_photo.png"));

            btnViewPicture6 = new Button(this, new Vector2(btnViewPostions[5].X, btnViewPostions[5].Y), new Rectangle(0, 0, btnWidthPhotoViewCommon, btnHeightPhotoViewCommon));
            btnViewPicture6.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/button_photo.png"));

            btnViewPicture7 = new Button(this, new Vector2(btnViewPostions[6].X, btnViewPostions[6].Y), new Rectangle(0, 0, btnWidthPhotoViewCommon, btnHeightPhotoViewCommon));
            btnViewPicture7.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/button_photo.png"));

            btnViewPicture8 = new Button(this, new Vector2(btnViewPostions[7].X, btnViewPostions[7].Y), new Rectangle(0, 0, btnWidthPhotoViewCommon, btnHeightPhotoViewCommon));
            btnViewPicture8.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/button_photo.png"));

            btnViewPicture9 = new Button(this, new Vector2(btnViewPostions[8].X, btnViewPostions[8].Y), new Rectangle(0, 0, btnWidthPhotoViewCommon, btnHeightPhotoViewCommon));
            btnViewPicture9.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/button_photo.png"));

            btnViewPicture10 = new Button(this, new Vector2(btnViewPostions[9].X, btnViewPostions[9].Y), new Rectangle(0, 0, btnWidthPhotoViewCommon, btnHeightPhotoViewCommon));
            btnViewPicture10.Texture = Texture2D.FromStream(graphics.GraphicsDevice, TitleContainer.OpenStream("Content/Textures/Buttons/button_photo.png"));

            // Adding all Buttons to their respective Lists
            btnListMainMenuNoSession = new List<Button>() { btnNewGame, btnOptions, btnQuitGame };
            btnListMainMenuSession = new List<Button>() { btnResume, btnQuitSession };
            btnListOptionsMenu = new List<Button>() { btnOptionsBack, btnOptionsSound, btnOptionsMusic };
            btnListRunning = new List<Button>() {btnBackToMenu};
            btnListChooseDifficulty = new List<Button>() { btnOptionsDifficultyEasy, btnOptionsDifficultyMedium, btnOptionsDifficultyHard };
            btnListEndgame = new List<Button>() { btnPhotoYes, btnPhotoNo };
            btnListHighscore = new List<Button>() { btnBackToMenu, btnDirectRetry, btnViewPicture1, btnViewPicture2, btnViewPicture3, btnViewPicture4, btnViewPicture5, btnViewPicture6, btnViewPicture7, btnViewPicture8, btnViewPicture9, btnViewPicture10 };

            #endregion

            // Initializing of all Gamestates
            this.startscreen = new Startscreen();
            this.mainmenuNoSession = new MainMenuNoSession(btnListMainMenuNoSession);
            this.mainmenuSession = new MainMenuSession(btnListMainMenuSession);
            this.choosedifficulty = new ChooseDifficulty(btnListChooseDifficulty);
            this.running = new Running(btnListRunning);
            this.optionmenu = new OptionMenu(btnListOptionsMenu);
            this.highscore = new Highscore(btnListHighscore, ref graphics);
            this.endgame = new Endgame(btnListEndgame);
            this.imagedetailview = new ImageDetailView();

            // INITIAL GAMESTATE // WHERE TO START THE GAME
            this.GameState = GameState.Startscreen;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Starts the process which controls the Kinect-Stream
            ProcessStartInfo psi = new ProcessStartInfo("KinectCursorController.exe");
            psi.WorkingDirectory = Path.GetFullPath("MouseControl");
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.Verb = "runas";
            Process.Start(psi);

            #region Loading Active Background-Assets

            // Loading the Textures for the interactive background
            sunraysTexture = Content.Load<Texture2D>("Textures/ActiveBackground/sunrays_3840x3840.png");
            sunraysPosition = new Vector2(0, 0);

            sunTexture = Content.Load<Texture2D>("Textures/ActiveBackground/bg_sun.png");
            sunPosition = new Vector2(-150, -150);

            skyTexture = Content.Load<Texture2D>("Textures/ActiveBackground/bg_sky.jpg");
            skyPosition = new Vector2(0, 0);

            grassTexture = Content.Load<Texture2D>("Textures/ActiveBackground/bg_grass.jpg");
            grassPosition = new Vector2(0, 780);

            flowerTexture = Content.Load<Texture2D>("Textures/ActiveBackground/bg_flowers.png");
            flowerPosition = new Vector2(0, 720);

            cloudTexture = Content.Load<Texture2D>("Textures/ActiveBackground/bg_cloud.png");

            // Generation of random starting positions      
            cloudPosition1.X = randomCloudX1.Next(0, 800);
            cloudPosition1.Y = randomCloudY1.Next(100, 550);

            cloudPosition2.X = randomCloudX1.Next(0, 800);
            cloudPosition2.Y = randomCloudY1.Next(100, 550);

            cloudPosition3.X = randomCloudX1.Next(0, 800);
            cloudPosition3.Y = randomCloudY1.Next(100, 550);

            cloudPosition4.X = randomCloudX1.Next(0, 800);
            cloudPosition4.Y = randomCloudY1.Next(100, 550);

            cloudPosition5.X = randomCloudX1.Next(0, 800);
            cloudPosition5.Y = randomCloudY1.Next(100, 550);

            #endregion

            Console.BackgroundColor = ConsoleColor.Black;
            this.IsMouseVisible = false;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Fonts/MemoryKidz");
            scoreFont = Content.Load<SpriteFont>("Fonts/MemoryKidzScoreFont108");
            customCursor = Content.Load<Texture2D>("Textures/Others/customCursor5.png");
            GameSpecs.scoreFont = scoreFont;
        }

        public GameState GameState
        {
            get { return this.gameState; }
            set
            {
                //unload old content
                switch (this.gameState)
                {
                    case GameState.None:
                        //Unload... nothing... 
                        break;
                    case GameState.Startscreen:
                    {
                        this.startscreen.Unload();
                        break;
                    }

                    case GameState.MainMenuSession:
                    {
                        this.mainmenuSession.Unload();
                        break;
                    }

                    case GameState.MainMenuNoSession:
                    {
                        this.mainmenuNoSession.Unload();
                        break;
                    }

                    case GameState.ChooseDifficulty:
                    {
                        this.choosedifficulty.Unload();
                        break;
                    }

                    case GameState.Running:
                    {
                        this.running.Unload();
                        break;
                    }

                    case GameState.OptionMenu:
                    {
                        this.optionmenu.Unload();
                        break;
                    }

                    case GameState.Highscore:
                    {
                        this.highscore.Unload();
                        break;
                    }

                    case GameState.Endgame:
                    {
                        this.endgame.Unload();
                        break;
                    }

                    case GameState.ImageDetailView:
                    {
                        this.imagedetailview.Unload();
                        break;
                    }
                }

                //load new content
                this.gameState = value;
                switch (this.gameState)
                {
                    case GameState.None:
                    {
                        this.Exit();    //Quit Game!
                        break;
                    }
                    case GameState.Startscreen:
                    {
                        this.startscreen.LoadContent();
                        break;
                    }
                    case GameState.MainMenuSession:
                    {
                        this.mainmenuSession.LoadContent();
                        break;
                    }
                    case GameState.MainMenuNoSession:
                    {
                        this.mainmenuNoSession.LoadContent();
                        break;
                    }
                    case GameState.ChooseDifficulty:
                    {
                        this.choosedifficulty.LoadContent();
                        break;
                    }
                    case GameState.Running:
                    {
                        this.running.LoadContent();
                        break;
                    }
                    case GameState.OptionMenu:
                    {
                        this.optionmenu.LoadContent();
                        break;
                    }
                    case GameState.Highscore:
                    {
                        this.highscore.LoadContent();
                        break;
                    }
                    case GameState.Endgame:
                    {
                        this.endgame.LoadContent();
                        break;
                    }
                    case GameState.ImageDetailView:
                    {
                        this.imagedetailview.LoadContent();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GameState ret;
            switch (this.gameState)
            {
                case GameState.None:
                    break;

                case GameState.Startscreen:
                {
                    ret = this.startscreen.Update(gameTime);
                    if (ret != GameState.Startscreen)
                    {
                        GameSpecs.PreviousGamestate = GameState.Startscreen;
                        this.GameState = ret;
                    }
                    break;
                }

                case GameState.MainMenuSession:
                {
                    ret = this.mainmenuSession.Update(gameTime);
                    if (ret != GameState.MainMenuSession)
                    {
                        this.GameState = ret;
                    }
                    break;
                }

                case GameState.MainMenuNoSession:
                {
                    ret = this.mainmenuNoSession.Update(gameTime);
                    if (ret != GameState.MainMenuNoSession)
                    {
                        this.GameState = ret;
                    }
                    break;
                }

                case GameState.ChooseDifficulty:
                {
                    ret = this.choosedifficulty.Update(gameTime);
                    if (ret != GameState.ChooseDifficulty)
                    {
                        this.GameState = ret;
                    }
                    break;
                }
   
                case GameState.Running:
                {
                    ret = this.running.Update(gameTime);
                    if (ret != GameState.Running)
                    {
                        this.GameState = ret;
                    }
                    break;
                }

                case GameState.OptionMenu:
                {
                    ret = this.optionmenu.Update(gameTime);
                    if (ret != GameState.OptionMenu)
                    {
                        this.GameState = ret;
                    }
                    break;
                }

                case GameState.Highscore:
                {
                    ret = this.highscore.Update(gameTime);
                    if (ret != GameState.Highscore)
                    {
                        this.GameState = ret;
                    }
                    break;
                }

                case GameState.Endgame:
                {
                    ret = this.endgame.Update(gameTime);
                    if (ret != GameState.Endgame)
                    {
                        this.GameState = ret;
                    }
                    break;
                }

                case GameState.ImageDetailView:
                {
                    ret = this.imagedetailview.Update(gameTime);
                    if (ret != GameState.ImageDetailView)
                    {
                        this.GameState = ret;
                    }
                    break;
                }
            }

            #region Cloudlogic for ActiveBackground

            // Restart postion for clouds which left the screen boundaries
            int restart = screenWidth + cloudTexture.Width;

            // Moving speed of the clouds
            float mediumRight = 0.7f;
            float fastRight = 0.9f;
            float slowLeft = -0.6f;
            float slowerLeft = -0.4f;
            float slowest = -0.2f;

            // Applying the speed parameters
            cloudPosition1.X += fastRight;
            if (cloudPosition1.X > cloudTexture.Width + screenWidth)
            {
                cloudPosition1.X = -cloudTexture.Width;
            }

            cloudPosition2.X += slowLeft;
            if (cloudPosition2.X < -cloudTexture.Width)
            {
                cloudPosition2.X = restart;
            }

            cloudPosition3.X += slowerLeft;
            if (cloudPosition3.X < -cloudTexture.Width)
            {
                cloudPosition3.X = restart;
            }

            cloudPosition4.X += slowest;
            if (cloudPosition4.X < -cloudTexture.Width)
            {
                cloudPosition4.X = restart;
            }

            cloudPosition5.X += mediumRight;
            if (cloudPosition5.X > cloudTexture.Width + screenWidth)
            {
                cloudPosition5.X = -cloudTexture.Width;
            }

            #endregion

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            switch (this.GameState) 
            {
                case GameState.None:
                    break;

                case GameState.Startscreen:
                {
                    startscreen.Draw(gameTime, ref spriteBatch, ref graphics, ref font);
                    break;
                }
                   
                case GameState.MainMenuSession:
                {
                    mainmenuSession.Draw(gameTime, ref spriteBatch, ref graphics, ref font);
                    break;
                }

                case GameState.MainMenuNoSession:
                {
                    mainmenuNoSession.Draw(gameTime, ref spriteBatch, ref graphics, ref font);
                    break;
                }

                case GameState.ChooseDifficulty:
                {
                    choosedifficulty.Draw(gameTime, ref spriteBatch, ref graphics, ref font);
                    break;
                }

                case GameState.Running:
                {
                    running.Draw(gameTime, ref spriteBatch, ref graphics, ref font);
                    break;
                }

                case GameState.OptionMenu:
                {
                    optionmenu.Draw(gameTime, ref spriteBatch, ref graphics, ref font);
                    break;
                }

                case GameState.Highscore:
                {
                    highscore.Draw(gameTime, ref spriteBatch, ref graphics, ref font);
                    break;
                }

                case GameState.Endgame:
                {
                    endgame.Draw(gameTime, ref spriteBatch, ref graphics, ref font);
                    break;
                }

                case GameState.ImageDetailView:
                {
                    imagedetailview.Draw(gameTime, ref spriteBatch, ref graphics, ref font);
                    break;
                }
            }
            spriteBatch.Begin();
            Rectangle cursorRectangle = new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 50, 50);
            spriteBatch.Draw(customCursor, cursorRectangle, null, Color.White, 0, MainGame.cursorOrigin, SpriteEffects.None, 0);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}