using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplication_1.Model
{
   enum Difficulty {Easy, Medium, Hard}
    internal class QuestionPack
    {
        public string Name {  get; set; }

        public Difficulty Difficulty { get; set; }

        public int TimeLimit { get; set; }
        public List<Question> Questions { get; set; }

        public QuestionPack(string name, Difficulty difficulty = Difficulty.Medium,int timeLimit=30) 
        {
           Name = name;
           Difficulty=difficulty;
           TimeLimit = timeLimit;
           Questions = new List<Question>();
        }


    }
}
