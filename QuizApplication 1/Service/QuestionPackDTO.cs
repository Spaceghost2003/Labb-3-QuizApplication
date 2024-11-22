using QuizApplication_1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplication_1.Service
{
    internal class QuestionPackDTO
    {
        public string Name { get; set; }
        public Difficulty Difficulty { get; set; }
        public int TimeLimit { get; set; }
        public List<QuestionDTO> Questions { get; set; }

        public QuestionPackDTO() { }    
    }
}
