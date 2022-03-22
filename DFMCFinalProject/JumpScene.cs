using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace DFMCFinalProject
{
    
    class JumpScene : GameScene
    {
        private SpriteBatch spriteBatch;
        static string resultsPath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).Parent.FullName, "results.txt"); //global public class-level variable to store a path to the file where we store all the data
        const int mainCharOffsetX = 50, mainCharOffsetY = 150, backgroundOffsetY = 25, birdOffsetY = 20, scoreOffsetX = 50, scoreOffsetY = 10; // mainCharOffsetY 
        int delay = 200, delayCount = 0;

        string playerName;

        int objectToBeGenerated;
        Random rnd;

        Texture2D road;
        Texture2D texMainChar;
        Rectangle srcRectRoad;
        Score score;

        Game1 g;
        MainCharacter mainChar;
        Rat rat;
        TrashBin trashBin;
        Bird bird;
        FinalMessage finalMessage;
        StartMessage startMessage;
        List<Result> resultsList;


        SoundEffect jumpSound;
        SoundEffect hitSound;

        bool resultSaved = false, gameIsOver = false;

        public JumpScene(Game game) : base(game)
        {
            
            g = (Game1)game;
            this.spriteBatch = g._spriteBatch;
            road = g.Content.Load<Texture2D>("images/road");

          

            resultsList = new List<Result>();

            jumpSound = this.g.Content.Load<SoundEffect>("sounds/maro-jump-sound-effect_1");
            hitSound = this.g.Content.Load<SoundEffect>("sounds/sfx-defeat");

            Texture2D backgroundTex = g.Content.Load<Texture2D>("images/background");

            Rectangle srcRect = new Rectangle(0, 800, backgroundTex.Width, backgroundTex.Height - 800);
            Vector2 pos = new Vector2(0, backgroundOffsetY);
            Vector2 speed = new Vector2(2, 0);

            ScrollingBackground sb1 = new ScrollingBackground(g, spriteBatch,
                backgroundTex, pos, srcRect, speed);
            this.Components.Add(sb1);
            srcRectRoad = new Rectangle(0, 200, road.Width, road.Height);

      


            texMainChar = g.Content.Load<Texture2D>("images/frame-1-still");
            Vector2 position = new Vector2(mainCharOffsetX, (int)Shared.stage.Y - mainCharOffsetY);
            mainChar = new MainCharacter(g, spriteBatch, texMainChar, position, 20, jumpSound);
            this.Components.Add(mainChar);

            mainChar.restart();

            Vector2 positionScore = new Vector2((int)Shared.stage.X - scoreOffsetX, scoreOffsetY);
            score = new Score(g, spriteBatch, backgroundTex, positionScore); ;
            this.Components.Add(score);
            score.restart();

            string gameStartingText = "To start the game press Space button...";
            Vector2 positionFinalText = new Vector2(0, 0);
            startMessage = new StartMessage(g, spriteBatch, gameStartingText, positionFinalText);
            this.Components.Add(startMessage);
            startMessage.restart();
  
        }

     

        public override void Update(GameTime gameTime)
        {
            if (mainChar.GetStarted() == true)
            {     
                startMessage.Hide();
                score.StartCount();
                if (delayCount > delay)
                {
                    delayCount = 0;
                    rnd = new Random();
                    objectToBeGenerated = rnd.Next(3);

                    if (objectToBeGenerated == 0)
                    {
                        Texture2D texRat = g.Content.Load<Texture2D>("images/frame-1-rat");
                        Vector2 positionRat = new Vector2((int)Shared.stage.X - 100, (int)Shared.stage.Y - mainCharOffsetY + texMainChar.Height - texRat.Height);
                        rat = new Rat(g, spriteBatch, texRat, positionRat, 5);
                        this.Components.Add(rat);
                        rat.restart();
                        ColissionManager cm = new ColissionManager(g, mainChar, rat);
                        this.Components.Add(cm);
                    }
                    else if (objectToBeGenerated == 1)
                    {
                        Texture2D textBin = g.Content.Load<Texture2D>("images/TrashCan");
                        Vector2 positionBin = new Vector2((int)Shared.stage.X - 100, (int)Shared.stage.Y - mainCharOffsetY + texMainChar.Height - textBin.Height);
                        trashBin = new TrashBin(g, spriteBatch, textBin, positionBin);
                        this.Components.Add(trashBin);
                        trashBin.restart();
                        ColissionManager cm = new ColissionManager(g, mainChar, trashBin);
                        this.Components.Add(cm);
                    }
                    else if (objectToBeGenerated == 2)
                    {
                        Texture2D textBird = g.Content.Load<Texture2D>("images/frame1-bird");
                        Vector2 positionBird = new Vector2((int)Shared.stage.X - 100, (int)Shared.stage.Y - mainCharOffsetY - texMainChar.Height + textBird.Height - birdOffsetY);
                        bird = new Bird(g, spriteBatch, textBird, positionBird, 5);
                        this.Components.Add(bird);
                        bird.restart();
                        ColissionManager cm = new ColissionManager(g, mainChar, bird);
                        this.Components.Add(cm);
                    }
                }
                delayCount++;
            }
            else if (mainChar.GetFinished() == true)
            {
                if (bird != null)
                {  
                    bird.Enabled = false;
                    bird.Visible = false;
                    bird.Dispose();
                }
                if (trashBin != null)
                {
                    trashBin.Enabled = false;
                    trashBin.Visible = false;
                    trashBin.Dispose();
                }
                if (rat != null)
                {
                    rat.Enabled = false;
                    rat.Visible = false;
                    rat.Dispose();
                }
                //mainChar.restart();

                score.EndCount();
                if (resultSaved == false)
                {
                    SaveResultToFile(); 
                    hitSound.Play();
                    resultSaved = true;
                }
                mainChar.Enabled = false;
                mainChar.Visible = false;
                
                gameIsOver = true;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Begin();

            spriteBatch.Draw(road, srcRectRoad, Color.White);

             if (gameIsOver == true)
            {
                string gameIsOverText = "Game is Over!\nYour score is: " + score.GetFinalResult() + "\nPress Q button to return to Main Menu...";
                Vector2 positionFinalText = new Vector2(0,0);
                finalMessage = new FinalMessage(g, spriteBatch, gameIsOverText, positionFinalText);
                this.Components.Add(finalMessage);
                finalMessage.restart();
            }

     
          

            spriteBatch.End();
            base.Draw(gameTime);
            
        }

        private void SaveResultToFile()
        {
            string errors = ""; // setting number of errors to 0 
            StreamWriter writer;
            SetResult();
            try // to catch any errors while saving a file
            {
                using (writer = new StreamWriter(resultsPath)) // adding to the file new information
                {
                    foreach (Result item in resultsList)
                    {
                        writer.WriteLine(item.Name); // writing total number of rows information
                        writer.WriteLine(item.ResultScore); // writing total number of columns information
                    }
                }
            }
            catch (FileNotFoundException ex) // catching all the possible errors
            {
                errors += "Error occurred while adding:\nFileNotFoundException: " + ex.Message;
            }
            catch (DirectoryNotFoundException ex)
            {
                errors += "Error occurred while adding:\nDirectoryNotFoundException: " + ex.Message;
            }
            catch (IOException ex)
            {
                errors += "Error occurred while adding:\nIOException: " + ex.Message;
            }
            catch (Exception ex)
            {
                errors += "Error occurred while adding:\nUnexpected Exception: " + ex.Message;
            }
        }
        private void SetResult()
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

                if (resultsList.Count() < 5)
                {
                    resultsList.Add(new Result(playerName, score.GetFinalResult()));
                }
                else 
                {
                 resultsList = resultsList.OrderByDescending(item => item.ResultScore).ToList();
                    if (resultsList.Last().ResultScore < score.GetFinalResult())
                    {
                        resultsList.RemoveAt(resultsList.Count - 1);
                        resultsList.Add(new Result(playerName, score.GetFinalResult()));
                    }
                 resultsList = resultsList.OrderByDescending(item => item.ResultScore).ToList();
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

        void CheckFile(string pathRoute) // I used this code from my final exam in PROG1815
        {
            if (!(File.Exists(pathRoute)))
            {
                File.CreateText(pathRoute).Close();
            }
        }

        public void SetPlayerName(string textBoxValue)
        {
            playerName = textBoxValue;
        }

        public bool GetGameIsOver()
        {
            return gameIsOver;
        }
        public void SetGameIsOver()
        {
             gameIsOver = false;
        }
    }
}
