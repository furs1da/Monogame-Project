using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace DFMCFinalProject
{
    public class MainCharacter : DrawableGameComponent
    {

        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Vector2 position;
        private Rectangle frameRect;
        private Game g;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;
        int frameUp = 0, frameDown = 2;
        bool startPos, jumpPos, runPos;
        bool gameStarted, gameFinished;
        KeyboardState oldState;
        private SoundEffect jumpSound;
        public MainCharacter(Game game,
          SpriteBatch spriteBatch,
          Texture2D tex,
          Vector2 position,
          int delay, SoundEffect jumpSound) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = tex;
            this.position = position;
            this.delay = delay;
            this.g = game;
            this.Enabled = false;
            this.Visible = false;
            startPos = true;
            gameStarted = false;
            gameFinished = false;
            this.jumpSound = jumpSound;
        }

        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (startPos == true && frameIndex > 1)
                {
                    frameIndex = 0;
                }
                else if (jumpPos == true && frameIndex > 3)
                {
                    frameIndex = 0;
                    runPos = true;
                    jumpPos = false;
                    ListOfImages.framesJump.RemoveAt(0);
                    ListOfImages.framesJump.RemoveAt(2);
                    position.X -= 96f;
                }
                else if (runPos == true && frameIndex > 5)
                {
                    frameIndex = 0;
                }

                else if (jumpPos == true)
                {
                    if (frameUp != 2)
                    {
                        position.X += 24f;
                        position.Y -= 90f;
                        frameUp++;
                        if (frameUp == 2)
                        {
                            frameDown = 0;
                        }
                    }
                 
                    if (frameDown != 2)
                    {
                        position.X += 24f;
                        position.Y += 90f;
                        frameDown++;
                        if (frameDown == 2)
                        {
                            frameUp = 0;
                        }
                    }
                  
                }
                delayCounter = 0;
            }
         
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space) && jumpPos == false && gameStarted==true)
            {
                jumpSound.Play();
                jumpPos = true;
                runPos = false;
                ListOfImages.framesJump.Insert(0, ListOfImages.framesRun[frameIndex]);
                ListOfImages.framesJump.Insert(3, ListOfImages.framesRun[frameIndex]);
                frameIndex = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && gameStarted == false)
            {
                runPos = true;
                startPos = false;
                gameStarted = true;
                frameIndex = 0;
            }
            oldState = Keyboard.GetState();
            base.Update(gameTime);
        }
        public void restart()
        {
            frameIndex = -1;
            delayCounter = 0;
            this.Enabled = true;
            this.Visible = true;
            startPos = true;
            frameRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndex >= 0)
            {
                if (startPos == true)
                {
                    texture = g.Content.Load<Texture2D>(ListOfImages.framesStill[frameIndex]);
                    frameRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                    delay = 20;
                }
                else if (jumpPos == true)
                {
                    texture = g.Content.Load<Texture2D>(ListOfImages.framesJump[frameIndex]);
                    frameRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                    delay = 7;
                }
                else 
                {
                    texture = g.Content.Load<Texture2D>(ListOfImages.framesRun[frameIndex]);
                    frameRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                    delay = 10;
                }
                spriteBatch.Draw(texture, frameRect, Color.White); 
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        public bool GetStarted()
        {
            return gameStarted;
        }
        public void FinishGame()
        {
            gameStarted = false;
            gameFinished = true;
        }
        public void MakeAfk()
        {
            gameStarted = false;
        }
        public bool GetFinished()
        {
            return gameFinished;
        }
    }
}
