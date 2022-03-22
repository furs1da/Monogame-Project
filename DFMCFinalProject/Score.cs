using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DFMCFinalProject
{
    public class Score : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont scoreFont;
        private Vector2 position;
        bool countStarted;
        Color scoreColor = Color.Red;
        private Game g;
        int finalResult;
        const int scoreOffsetY = 10;
        string scoreText = "Your score: ";
        private int delay = 20;
        private int delayCounter = 0;
        public Score(Game game,
         SpriteBatch spriteBatch,
         Texture2D tex,
         Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.g = game;
            this.Enabled = false;
            this.Visible = false;
            finalResult = 0;
            scoreFont = g.Content.Load<SpriteFont>("fonts/headerEnterPlayerName");
            countStarted = false;
            delayCounter = 0;
        }
        public override void Update(GameTime gameTime)
        {
            if (countStarted == true)
            {
                delayCounter++;
                if (delayCounter > delay)
                {
                    finalResult += 1;
                    delayCounter = 0;
                }
            }
            base.Update(gameTime);
        }
        public void restart()
        {
            this.Enabled = true;
            this.Visible = true;
        
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            position = new Vector2((int)Shared.stage.X - scoreFont.MeasureString(scoreText + finalResult.ToString()).X, scoreOffsetY);
            spriteBatch.DrawString(scoreFont, scoreText + finalResult.ToString(), position, scoreColor);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public int GetFinalResult()
        {
            return finalResult;
        }
        public void StartCount()
        {
            countStarted = true;
        }
        public void EndCount()
        {
            countStarted = false;
        }
    }
}
