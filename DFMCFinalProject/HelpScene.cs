using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DFMCFinalProject
{
    public class HelpScene : GameScene 
    {
        private SpriteBatch spriteBatch;
        string helpText = "", headerText;
        private SpriteFont headerFont, helpFont;
        private Vector2 positionHeader, positionHelp;
        Color screenColor = Color.Red;
        public HelpScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;

            headerFont = g.Content.Load<SpriteFont>("fonts/leaderBoardHeader");
            helpFont = g.Content.Load<SpriteFont>("fonts/fontEnterPlayerName");
            headerText = "Help";
            helpText = "Purpose of the game - achieve the highest score\n\nSpace - to Jump\n\nQ - to return to the Main Menu (after game ends)\n\nEnter - to submit choice\n\nLC - select Menu option or submit choice\n\nArrow Buttons - change choice";

            positionHeader = new Vector2((int)Shared.stage.X / 2 - headerFont.MeasureString(headerText).X / 2, (int)Shared.stage.Y / 5 - headerFont.MeasureString(headerText).Y);

            positionHelp = new Vector2((int)Shared.stage.X / 2 - helpFont.MeasureString(helpText).X / 2, (int)Shared.stage.Y / 5 + headerFont.MeasureString(headerText).Y / 2 - helpFont.MeasureString(headerText).Y);

            Texture2D backgroundTex = g.Content.Load<Texture2D>("images/background");
            Rectangle srcRect = new Rectangle(0, 800, backgroundTex.Width, backgroundTex.Height - 800);
            Vector2 pos = new Vector2(0, Shared.stage.Y - srcRect.Height);
            Vector2 speed = new Vector2(2, 0);
            ScrollingBackground sb1 = new ScrollingBackground(g, spriteBatch,
                backgroundTex, pos, srcRect, speed);
            this.Components.Add(sb1);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(headerFont, headerText, positionHeader, screenColor);
            spriteBatch.DrawString(helpFont, helpText, positionHelp, Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
