using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace DFMCFinalProject
{
    public class MenuComponent : DrawableGameComponent
    {
        private const string header = "Final Project";
        private const float offsetY = 50f;

        private List<RectangularWithIndex> menuRectangle;

        private SpriteBatch spriteBatch;
        private SpriteFont regularFont, hilightFont, headerFont;
        private string[] menuItems;
        public int SelectedIndex { get; set; }
        private Vector2 position;

        private Vector2 textWidth;
        private Vector2 headerWidth;
        private Vector2 finalTextPosition;

        private Color regularColor = Color.Black;
        private Color hilightColor = Color.Red;
        private Color headerColor = Color.Red;

        private KeyboardState oldState;

        private Game g;

        public MenuComponent(Game game,
            SpriteBatch spriteBatch,
            SpriteFont regularFont,
            SpriteFont hilightFont,
            SpriteFont headerFont,
            string[] menus) : base(game)
        {
            g = game;
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.hilightFont = hilightFont;
            this.headerFont = headerFont;
            menuItems = menus;
            position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 3.6f);
            textWidth = new Vector2(Shared.stage.X, Shared.stage.Y);
            menuRectangle = new List<RectangularWithIndex>();
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                SelectedIndex++;
                if (SelectedIndex == menuItems.Length)
                {
                    SelectedIndex = 0;
                }
            }

            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                SelectedIndex--;
                if (SelectedIndex == -1)
                {
                    SelectedIndex = menuItems.Length - 1;
                }
            }
            MouseState ms = Mouse.GetState();
            if (ms.LeftButton == ButtonState.Pressed)
            {
                Rectangle mouseRect = new Rectangle(ms.X, ms.Y, 1, 1);
                for(int i =0; i < menuRectangle.Count; i++)
                { 
                    if (mouseRect.Intersects(menuRectangle[i].rectangle))
                    {
                        SelectedIndex = menuRectangle[i].index;
                        break;
                    }
                }
            }

                oldState = ks;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;
            spriteBatch.Begin();
            headerWidth = new Vector2(tempPos.X - headerFont.MeasureString(header).X/2, offsetY);
            spriteBatch.DrawString(headerFont, header, headerWidth, headerColor);
            bool toAdd = false, existInList = false;

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (SelectedIndex == i)
                {
                    Vector2 tempWidth = hilightFont.MeasureString(menuItems[i]);
                    if (textWidth.X > tempWidth.X)
                        textWidth = tempWidth;

                    finalTextPosition = new Vector2(tempPos.X - textWidth.X, tempPos.Y - textWidth.Y);

                    spriteBatch.DrawString(hilightFont, menuItems[i], finalTextPosition, hilightColor);

                    foreach (RectangularWithIndex item in menuRectangle.ToList())
                    {
                        if (item.index == i)
                        {
                            existInList = true;
                        }
                    }
                    if (existInList)
                    {
                        foreach (RectangularWithIndex item in menuRectangle.ToList())
                        {
                            if (item.index == i && item.rectangle.X != (int)finalTextPosition.X)
                            {
                                menuRectangle.Remove(item);
                                toAdd = true;
                            }
                        }
                    }

                    if (!existInList || toAdd)
                        menuRectangle.Add(new RectangularWithIndex(new Rectangle((int)finalTextPosition.X, (int)finalTextPosition.Y, (int)tempWidth.X, (int)tempWidth.Y), i));

                    tempPos.Y += hilightFont.LineSpacing;
                }
                else
                {
                    Vector2 tempWidth = hilightFont.MeasureString(menuItems[i]);
                    if (textWidth.X > hilightFont.MeasureString(menuItems[i]).X)
                        textWidth = hilightFont.MeasureString(menuItems[i]);

                    finalTextPosition = new Vector2(tempPos.X - textWidth.X, tempPos.Y - textWidth.Y);

                    spriteBatch.DrawString(regularFont, menuItems[i], finalTextPosition, regularColor);

                    foreach (RectangularWithIndex item in menuRectangle.ToList())
                    {
                        if (item.index == i)
                        {
                            existInList = true;
                        }
                    }
                    if (existInList)
                    {
                        foreach (RectangularWithIndex item in menuRectangle.ToList())
                        {
                            if (item.index == i && item.rectangle.X != (int)finalTextPosition.X)
                            {
                                menuRectangle.Remove(item);
                                toAdd = true;
                            }
                        }
                    }

                    if(!existInList || toAdd)
                    menuRectangle.Add(new RectangularWithIndex(new Rectangle((int)finalTextPosition.X, (int)finalTextPosition.Y, (int)tempWidth.X, (int)tempWidth.Y), i));

                    tempPos.Y += regularFont.LineSpacing;
                }
                toAdd = false; 
                existInList = false;
            }
            spriteBatch.End();



            base.Draw(gameTime);
        }
    }
}
