using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace DFMCFinalProject
{
     public class TextBox
    {
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont;
        public StringBuilder Text;
        public Vector2 position;
        public Color ForegroundColor;
        public Color BackgroundColor;
        public bool HasFocus;
        GraphicsDevice graphicsDevice;
        RenderTarget2D renderTarget;
        KeyboardState lastKeyboard;
        bool renderIsDirty = true;
        public TextBox(GraphicsDevice graphicsDevice, int width, SpriteFont font)
        {
            this.regularFont = font;
            Vector2 fontMeasurements = font.MeasureString("dfgjlJL");
            int height = (int)fontMeasurements.Y;
            PresentationParameters pp = graphicsDevice.PresentationParameters;
            renderTarget = new RenderTarget2D(graphicsDevice, width,height,
                false, pp.BackBufferFormat, pp.DepthStencilFormat);
            Text = new StringBuilder();
            this.graphicsDevice = graphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void Update(GameTime gameTime)
        {
            if (!HasFocus)
            {
                return;
            }
            var keyboard = Keyboard.GetState();
            foreach (var key in keyboard.GetPressedKeys())
            {
                if (!lastKeyboard.IsKeyUp(key))
                {
                    continue;
                }
                if (key == Keys.Delete || key == Keys.Back)
                {
                    if (Text.Length == 0)
                    {
                        continue;
                    }
                    Text.Length--;
                    renderIsDirty = true;
                    continue;
                }
                char character;
                if (!KeyBoardList.characterByKey.TryGetValue(key, out character))
                {
                    continue;
                }
                if (keyboard.IsKeyDown(Keys.LeftShift) ||
                keyboard.IsKeyDown(Keys.RightShift))
                {
                    character = Char.ToUpper(character);
                }
                Text.Append(character);
                renderIsDirty = true;
            }

            lastKeyboard = keyboard;
        }
        public void PreDraw()
        {
            if (!renderIsDirty)
            {
                return;
            }
            renderIsDirty = false;
            var existingRenderTargets = graphicsDevice.GetRenderTargets();
            graphicsDevice.SetRenderTarget(renderTarget);
            spriteBatch.Begin();
            graphicsDevice.Clear(BackgroundColor);
            spriteBatch.DrawString(
                regularFont, Text,
                Vector2.Zero, ForegroundColor);
            spriteBatch.End();
            graphicsDevice.SetRenderTargets(existingRenderTargets);
        }
        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(renderTarget, position, Color.White);
            spriteBatch.End();
        }
    }
}

