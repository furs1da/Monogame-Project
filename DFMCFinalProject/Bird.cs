using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DFMCFinalProject
{
    public class Bird : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Vector2 position;
        private Rectangle frameRect;
        private Game g;
        private int frameIndex = -1;
        private int delay;
        private int delayCounter;
        float speed = 9f;
        public Bird(Game game,
          SpriteBatch spriteBatch,
          Texture2D tex,
          Vector2 position,
          int delay) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = tex;
            this.position = position;
            this.delay = delay;
            this.g = game;
            this.Enabled = false;
            this.Visible = false;
        }
        public override void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > 9)
                {
                    frameIndex = 0;
                }
                delayCounter = 0;
            }
            position.X -= speed;
            if (position.X < Shared.stage.X)
            {
                this.Dispose();
            }
            base.Update(gameTime);
        }
        public void restart()
        {
            frameIndex = -1;
            delayCounter = 0;
            this.Enabled = true;
            this.Visible = true;
            frameRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameIndex >= 0)
            {
                texture = g.Content.Load<Texture2D>(ListOfImages.framesBird[frameIndex]);
                frameRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
                spriteBatch.Draw(texture, frameRect, Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
    }
}
