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

        public Scoring(string str1, string str2, string str3, string str4, string str5, string str6)
        {
            IList<string> lList = new List<string>();
            lList.Add(str1);
            lList.Add(str2);
            lList.Add(str3);
            lList.Add(str4);
            lList.Add(str5);
            lList.Add(str6);

            Score = lList;
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