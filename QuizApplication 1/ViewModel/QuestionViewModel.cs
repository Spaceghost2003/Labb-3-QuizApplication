using QuizApplication_1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplication_1.ViewModel
{
    internal class QuestionViewModel : ViewModelBase
    {
        private readonly Question model;
        public string Query
        {
            get => model.Query;
            set
            {
                model.Query = value;
                OnPropertyChanged();
            }
        }

        public string CorrectAnswer
        {
            get => model.CorrectAnswer;
            set
            {
                model.CorrectAnswer = value;
                OnPropertyChanged();
            }
        }

        public string IncorrectAnswers
        {
            get  => model.IncorrectAnswers[3];
            set
            {
                model.IncorrectAnswers[3] = value;
                OnPropertyChanged();
            }
        }
                
    }
}
