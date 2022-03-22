using System;
using System.Collections.Generic;
using System.Text;

namespace DFMCFinalProject
{
    public class Result
    {
        private string name;
        public string Name   
        {
            get { return name; }   
            set { name = value; } 
        }

        private int resultScore;
        public int ResultScore
        {
            get { return resultScore; }
            set { resultScore = value; }
        }

        public Result(string _name, int _score)
        {
            name = _name;
            resultScore = _score;
        }
    }
}
