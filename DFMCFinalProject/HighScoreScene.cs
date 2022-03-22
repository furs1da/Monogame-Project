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
    public class HighScoreScene : GameScene
    {
        private SpriteBatch spriteBatch;
        List<Result> resultsList;
        string leaderboardText = "", headerText;
        private SpriteFont headerFont, resultFont;
        private Vector2 positionHeader, positionResult;
        Color screenColor = Color.Red;
        static string resultsPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, "results.txt");
        public HighScoreScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            resultsList = new List<Result>();
            headerFont = g.Content.Load<SpriteFont>("fonts/leaderBoardHeader");
            resultFont = g.Content.Load<SpriteFont>("fonts/regularMenuFont");
            headerText = "Leaderboard";
            leaderboardText = "";
            
            GetResult();
            if (resultsList.Count == 0)
            {
                leaderboardText = "No records to be displayed..";
            }
         
            positionHeader = new Vector2((int)Shared.stage.X / 2 - headerFont.MeasureString(headerText).X / 2, (int)Shared.stage.Y / 5 - headerFont.MeasureString(headerText).Y);
            positionResult = new Vector2((int)Shared.stage.X / 2 - resultFont.MeasureString(headerText).X / 2, (int)Shared.stage.Y / 5 + headerFont.MeasureString(headerText).Y - resultFont.MeasureString(headerText).Y);
            
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
            spriteBatch.DrawString(resultFont, leaderboardText, positionResult, Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        void CheckFile(string pathRoute) // I used this code from my final exam in PROG1815
        {
            if (!(File.Exists(pathRoute)))
            {
                File.CreateText(pathRoute).Close();
            }
        }
        private void GetResult()
        {
            StreamReader reader; // stream reader to read data from the file
            string stringToBeParsed = "";
            int scoreToBeParsed = 0;
            resultsList.Clear();
            try
            {
                CheckFile(resultsPath);

                using (reader = new StreamReader(resultsPath)) // using streamReader
                {
                    if (new FileInfo(resultsPath).Length != 0) // checking if the file is empty
                        while (!reader.EndOfStream) // filling listOfStocks with objects from the file
                        {
                            stringToBeParsed = reader.ReadLine();
                            scoreToBeParsed = Convert.ToInt32(reader.ReadLine());
                            if (!string.IsNullOrWhiteSpace(stringToBeParsed))
                            {
                                resultsList.Add(new Result(stringToBeParsed, scoreToBeParsed)); // parsing string from the file to an object     
                            }
                        }
                }   
                resultsList = resultsList.OrderByDescending(item => item.ResultScore).ToList();
                for(int i = 0; i < resultsList.Count; i++)
                {
                    leaderboardText += (i+1) + ". "+ resultsList[i].Name + " - " + resultsList[i].ResultScore.ToString() + "\n";
                }
            }
            catch (FileNotFoundException ex) // catching all possible errors
            {
                throw new FileNotFoundException("Error occurred while parsing record from the file:\nFileNotFoundException: " + ex.Message);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new DirectoryNotFoundException("Error occurred while parsing record from the file:\nDirectoryNotFoundException: " + ex.Message);
            }
            catch (IOException ex)
            {
                throw new IOException("Error occurred while parsing record from the file:\nIOException: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while parsing record from the file:\nUnexpected Exception: " + ex.Message);
            }
        }
    }
}
