using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DFMCFinalProject
{
    public class TrashBin : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private Vector2 position;
        private Rectangle frameRect;
        private Game g;
        float speed = 9f;
        public TrashBin(Game game,
         SpriteBatch spriteBatch,
         Texture2D tex,
         Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;
            this.texture = tex;
            this.position = position;
            this.g = game;
            this.Enabled = false;
            this.Visible = false;
            frameRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        public override void Update(GameTime gameTime)
        {
            position.X -= speed;
            if (position.X < Shared.stage.X)
            {
                this.Dispose();
            }
            base.Update(gameTime);
        }
        public void restart()
        {
            this.Enabled = true;
            this.Visible = true;
            frameRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            frameRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            spriteBatch.Draw(texture, frameRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
    }
}
