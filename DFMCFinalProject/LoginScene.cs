using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DFMCFinalProject
{
    public class LoginScene : GameScene
    {
        const int textboxWidth = 350, offsetY = 50, offsetX = 125;
        const string header = "Enter Player Name";
        Color headerColor = Color.Red;
        private SpriteBatch spriteBatch;
        public TextBox textbox;
        SpriteFont headerFont;
        Vector2 headerWidth;
        Texture2D arrow;
        public Rectangle srcRectArrow;
        Game1 g;
        public LoginScene(Game game) : base(game)
        {
            g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            SpriteFont regularFont = g.Content.Load<SpriteFont>("fonts/fontEnterPlayerName");
            textbox = new TextBox(GraphicsDevice, textboxWidth, regularFont)
            {
                ForegroundColor = Color.Red,
                BackgroundColor = Color.LightSlateGray,
                position = new Vector2(Shared.stage.X / 2 - textboxWidth / 2, Shared.stage.Y / 3.5f),
                HasFocus = true
            };
            Texture2D backgroundTex = g.Content.Load<Texture2D>("images/background");

       


            Rectangle srcRect = new Rectangle(0, 800, backgroundTex.Width, backgroundTex.Height - 800);
            Vector2 pos = new Vector2(0, Shared.stage.Y - srcRect.Height);
            Vector2 speed = new Vector2(2, 0);
            ScrollingBackground sb1 = new ScrollingBackground(g, spriteBatch,
                backgroundTex, pos, srcRect, speed);

            Vector2 pos2 = new Vector2(0, Shared.stage.Y - srcRect.Height - 50);
            Vector2 speed2 = new Vector2(1, 0);
            ScrollingBackground sb2 = new ScrollingBackground(g, spriteBatch,
                backgroundTex, pos2, srcRect, speed2);

            headerFont = g.Content.Load<SpriteFont>("fonts/headerEnterPlayerName");
            headerWidth = new Vector2(Shared.stage.X / 2 - headerFont.MeasureString(header).X / 2, offsetY);

            arrow = g.Content.Load<Texture2D>("images/arrow");
            srcRectArrow = new Rectangle((int)Shared.stage.X-offsetX, offsetY, arrow.Width, arrow.Height);

            this.Components.Add(sb2);
            this.Components.Add(sb1);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(headerFont, header, headerWidth, headerColor);
            textbox.PreDraw();
            textbox.Draw();

            spriteBatch.Draw(arrow, srcRectArrow, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
     
    }
}
