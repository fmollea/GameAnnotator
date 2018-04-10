using System;
using System.Collections.Generic;

namespace Anotador
{
    public class Scoring
    {
        public IList<string> Score = new List<string>();
        const int cantMax = 6;

        public Scoring()
        {

        }

        public Scoring(List<string> pList)
        {
                Score = pList;
        }

        public string GetScoring(int i)
        {
            return Score[i];
        }

        public void SetScoring(int i, string scr)
        {
            Score[i] = scr;
        }
    }
}