using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;


namespace DFMCFinalProject
{
    public class ColissionManager : GameComponent
    {
        private MainCharacter mainChar;
        private Bird bird;
        private TrashBin trashBin;
        private Rat rat;
        public ColissionManager(Game game,
           MainCharacter mainChar,
           Bird bird) : base(game)
        {
            this.mainChar = mainChar;
            this.bird = bird;

        }
        public ColissionManager(Game game,
           MainCharacter mainChar,
           TrashBin trashBin ) : base(game)
        {
            this.mainChar = mainChar;
            this.trashBin = trashBin;
       
        }
        public ColissionManager(Game game,
           MainCharacter mainChar,
           Rat rat) : base(game)
        {
            this.mainChar = mainChar;
            this.rat = rat;

        }
        public override void Update(GameTime gameTime)
        {
            Rectangle mainCharRect = mainChar.getBounds();
            if (bird != null)
            {
                Rectangle birdRect = bird.getBounds();
                if (mainCharRect.Intersects(birdRect))
                {
                    mainChar.FinishGame();
                    this.Dispose();
                    //hitSound.Play();
                }
            }
            else if (trashBin != null)
            {
                Rectangle trashBinRect = trashBin.getBounds();
                if (mainCharRect.Intersects(trashBinRect))
                {
                    mainChar.FinishGame();
                    this.Dispose();
                    //hitSound.Play();
                }
            }
            else if (rat != null)
            {
                Rectangle ratRect = rat.getBounds();
                if (mainCharRect.Intersects(ratRect))
                {
                    mainChar.FinishGame();
                    this.Dispose();
                    //hitSound.Play();
                }
            }
            


            base.Update(gameTime);
        }

    }
}
