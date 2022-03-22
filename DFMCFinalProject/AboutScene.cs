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
    public class AboutScene : GameScene
    {
        private SpriteBatch spriteBatch;
        string aboutText = "", headerText;
        private SpriteFont headerFont, aboutFont;
        private Vector2 positionHeader, positionAbout;
        Color screenColor = Color.Red;
        public AboutScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;

            headerFont = g.Content.Load<SpriteFont>("fonts/leaderBoardHeader");
            aboutFont = g.Content.Load<SpriteFont>("fonts/fontEnterPlayerName");
            headerText = "About";
            aboutText = "The Concrete Jungle was designed and created\n by Dmytrii Furs and Max Carere.The main character's name\n is John Doe.He is trying to escape\n the endless jungle of New York, but he has\n no success with it.The inspiration\n to design this game was an old\n Google Chrome browser game with the dinosaur.";

            positionHeader = new Vector2((int)Shared.stage.X / 2 - headerFont.MeasureString(headerText).X / 2, (int)Shared.stage.Y / 5 - headerFont.MeasureString(headerText).Y);

            positionAbout = new Vector2((int)Shared.stage.X / 2 - aboutFont.MeasureString(aboutText).X / 2, (int)Shared.stage.Y / 5 + headerFont.MeasureString(headerText).Y / 2 - aboutFont.MeasureString(headerText).Y);

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
            spriteBatch.DrawString(aboutFont, aboutText, positionAbout, Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
