using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DFMCFinalProject
{
    public class StartMessage : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont finalFont;
        private Vector2 position;
        Color finalColor = Color.White;
        string gameIsOverText;
        private Game g;
        bool hide = false;
        public StartMessage(Game game,
            SpriteBatch spriteBatch,
            string _gameIsOverText,
            Vector2 position) : base(game)
        {
            this.spriteBatch = spriteBatch;

            this.g = game;
            this.Enabled = false;
            this.Visible = false;
            gameIsOverText = _gameIsOverText;
            finalFont = g.Content.Load<SpriteFont>("fonts/regularMenuFont");
            this.position = new Vector2((int)Shared.stage.X / 2 - finalFont.MeasureString(gameIsOverText).X / 2, (int)Shared.stage.Y / 2 - finalFont.MeasureString(gameIsOverText).Y);
        }
        public void restart()
        {
            this.Enabled = true;
            this.Visible = true;

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            if (!hide)
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(finalFont, gameIsOverText, position, finalColor);
                spriteBatch.End();
            }
           
            base.Draw(gameTime);
        }
        public void Hide()
        {

            hide = true;
        }
    }

}
