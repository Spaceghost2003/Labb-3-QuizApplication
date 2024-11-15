using QuizApplication_1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QuizApplication_1.ViewModel
{
    internal class QuestionPackViewModel : ViewModelBase
    {
        private readonly QuestionPack model;

        

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name { 
            get => model.Name;
            set
            {
                model.Name = value;
                OnPropertyChanged();
            }
        }

        public Difficulty Difficulty {
            get => model.Difficulty;
            set 
            { 
                model.Difficulty = value;
                OnPropertyChanged();
            }
        }

        public int TimeLimit {
            get => model.TimeLimit;
            set 
            { 
                model.TimeLimit= value;
                OnPropertyChanged();
            }
        }

        public QuestionPackViewModel(QuestionPack model)
        {
            this.model = model;
            this.Questions= new ObservableCollection<Question>(model.Questions);

        }
        public ObservableCollection<Question> Questions { get; }

        public Question GetNextQuestion(int index)
        {
           
            return Questions.ElementAtOrDefault(index);
        }
    }
}
