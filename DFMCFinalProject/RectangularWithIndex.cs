using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DFMCFinalProject
{
    public class RectangularWithIndex
    {
        public Rectangle rectangle;
        public int index;
        public RectangularWithIndex(Rectangle _rectangle, int _index)
        {
            rectangle = _rectangle;
            index = _index;
        }
    }
}
