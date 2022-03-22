using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DFMCFinalProject
{
    public class StartScene : GameScene
    {
        public MenuComponent Menu { get; set; }

        private SpriteBatch spriteBatch;

        string[] menuItems = {"Start game",
                                "High Score",
                                "Help",
                                "About",
                                "Quit"};

        public StartScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            SpriteFont regularFont = g.Content.Load<SpriteFont>("fonts/highlightedMenuFont");
            SpriteFont hilightFont = g.Content.Load<SpriteFont>("fonts/regularMenuFont");
            SpriteFont headerFont = g.Content.Load<SpriteFont>("fonts/headerMenuFont");

            Menu = new MenuComponent(game, spriteBatch, regularFont, hilightFont, headerFont, menuItems);
            this.Components.Add(Menu);

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
            this.Components.Add(sb2);
            this.Components.Add(sb1);
        }





    }
}
