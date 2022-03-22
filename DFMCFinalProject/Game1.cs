using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DFMCFinalProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private StartScene startScene;
        private LoginScene loginScene;
        private JumpScene jumpScene;
        private HighScoreScene highScoreScene;
        private HelpScene helpScene;
        private AboutScene aboutScene;
        SoundEffect clickSound;
        Song mainMenuSong;
        Song gameTheme;
        private void hideAllScenes()
        {
            foreach (GameScene item in Components)
            {
                item.hide();
            }
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            Shared.stage = new Vector2(_graphics.PreferredBackBufferWidth,
                _graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            startScene = new StartScene(this);
            loginScene = new LoginScene(this);
            jumpScene = new JumpScene(this);
            highScoreScene = new HighScoreScene(this);
            helpScene = new HelpScene(this);
            aboutScene = new AboutScene(this);
            this.Components.Add(startScene);
            this.Components.Add(loginScene);
            this.Components.Add(jumpScene);
            this.Components.Add(highScoreScene);
            this.Components.Add(helpScene);
            this.Components.Add(aboutScene);
            startScene.show();//Set's starting screen

            clickSound = this.Content.Load<SoundEffect>("sounds/button-21");
            mainMenuSong = this.Content.Load<Song>("sounds/menuTheme");
            gameTheme = this.Content.Load<Song>("sounds/gameTheme");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(mainMenuSong);
        }

        protected override void Update(GameTime gameTime)
        {
            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();
            MouseState ms = Mouse.GetState();

            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    clickSound.Play();
                    hideAllScenes();
                    loginScene.Dispose();
                    loginScene = new LoginScene(this);
                    this.Components.Add(loginScene);
                    loginScene.show();
                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    clickSound.Play();
                    hideAllScenes();
                    highScoreScene.Dispose();
                    highScoreScene = new HighScoreScene(this);
                    this.Components.Add(highScoreScene);
                    highScoreScene.show();
                }
                else if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    clickSound.Play();
                    hideAllScenes();
                    helpScene.Dispose();
                    helpScene = new HelpScene(this);
                    this.Components.Add(helpScene);
                    helpScene.show();
                }
                else if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    clickSound.Play();
                    hideAllScenes();
                    aboutScene.Dispose();
                    aboutScene = new AboutScene(this);
                    this.Components.Add(aboutScene);
                    aboutScene.show();
                }
                else if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    clickSound.Play();
                    Exit();
                }
            }
            if (loginScene.Enabled)
                loginScene.textbox.Update(gameTime);

            if (loginScene.Enabled)
            {
                if (ms.LeftButton == ButtonState.Pressed)
                {
                    Rectangle mouseRect = new Rectangle(ms.X, ms.Y, 1, 1);
                     if (mouseRect.Intersects(loginScene.srcRectArrow) && !string.IsNullOrEmpty(loginScene.textbox.Text.ToString()))
                        {
                            clickSound.Play();
                            hideAllScenes();
                            jumpScene.Dispose();
                            jumpScene = new JumpScene(this);
                            this.Components.Add(jumpScene);
                            jumpScene.show();
                            jumpScene.SetPlayerName(loginScene.textbox.Text.ToString());

                        MediaPlayer.Stop();
                        MediaPlayer.Play(gameTheme);
                    }
                }
                else if (ks.IsKeyDown(Keys.Enter) && !string.IsNullOrEmpty(loginScene.textbox.Text.ToString()))
                {
                    clickSound.Play();
                    hideAllScenes();
                    jumpScene.Dispose();
                    jumpScene = new JumpScene(this);
                    this.Components.Add(jumpScene);
                    jumpScene.show();
                    jumpScene.SetPlayerName(loginScene.textbox.Text.ToString());

                    MediaPlayer.Stop();
                    MediaPlayer.Play(gameTheme);
                }
                if (ks.IsKeyDown(Keys.Escape))
                {
                    clickSound.Play();
                    hideAllScenes();
                    startScene.show();
                }
            }
            if (highScoreScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    clickSound.Play();
                    hideAllScenes();
                    startScene.show();
                }
            }
            if (helpScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    clickSound.Play();
                    hideAllScenes();
                    startScene.show();
                }
            }
            if (aboutScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    clickSound.Play();
                    hideAllScenes();
                    startScene.show();
                }
            }
            if (jumpScene.Enabled)
            {

                if (jumpScene.GetGameIsOver() == true && ks.IsKeyDown(Keys.Q))
                {
                    clickSound.Play();
                    jumpScene.SetGameIsOver();
                    hideAllScenes();
                    startScene.show();
                    MediaPlayer.Stop();
                    MediaPlayer.Play(mainMenuSong);
                    return;
                }
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (loginScene.Enabled)
            {
                loginScene.textbox.PreDraw();
                GraphicsDevice.Clear(Color.Black);
                loginScene.textbox.Draw();
            }

            GraphicsDevice.Clear(Color.CornflowerBlue);


            base.Draw(gameTime);
        }
    }
}
