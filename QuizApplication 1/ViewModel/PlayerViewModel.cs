using QuizApplication_1.Command;
using QuizApplication_1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QuizApplication_1.ViewModel
{
    internal class PlayerViewModel:ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;
       
        public QuestionPackViewModel? ActivePack { get => mainWindowViewModel.ActivePack; }

        private DispatcherTimer timer;


        Question ActiveQuestion = new Question("test", "", "", "", "");

        public RelayCommand StartQuizCommand { get; }

        private string _displayedQuery;
        public string DisplayedQuery
        {
            get { return _displayedQuery; }
            set 
            {
                _displayedQuery = value;
                OnPropertyChanged();
            }
        }

        private string _correctAnswer;
        public string CorrectAnswer
        {
            get { return _correctAnswer; }
            set 
            {
                _correctAnswer = value;
                OnPropertyChanged();
            }
        }

        private string _incorrectAnswer1;
        public string IncorrectAnswer1
        {
            get { return _incorrectAnswer1; }
            set 
            {
                _incorrectAnswer1 = value;
                OnPropertyChanged();
            }
        }
        private string _incorrectAnswer2;
        public string IncorrectAnswer2
        {
            get { return _incorrectAnswer2; }
            set
            {
                _incorrectAnswer2 = value;
                OnPropertyChanged();
            }
        }
        private string _incorrectAnswer3;
        public string IncorrectAnswer3
        {
            get { return _incorrectAnswer3; }
            set
            {
                _incorrectAnswer3 = value;
                OnPropertyChanged();
            }
        }

        private string buttonAnswer1;

        public string ButtonAnswer1
        {
            get { return buttonAnswer1; }
            set
            {
                buttonAnswer1 = value;
                OnPropertyChanged();
            }
        }

        
        private string buttonAnswer2;
        public string ButtonAnswer2
        {
            get { return buttonAnswer2; }
            set
            {
                buttonAnswer2 = value;
                OnPropertyChanged();
            }
        }
        private string buttonAnswer3;
        public string ButtonAnswer3
        {
            get { return buttonAnswer3; }
            set
            {
                buttonAnswer3 = value;
                OnPropertyChanged();
            }
        }
        private string buttonAnswer4;
        public string ButtonAnswer4
        {
            get { return buttonAnswer4; }
            set
            {
                buttonAnswer4 = value;
                OnPropertyChanged();
            }
        }


        

        public PlayerViewModel(MainWindowViewModel mainWindowViewModel)
        {
            ButtonAnswer1 = "also a test";
            ButtonAnswer2 = "just a test";

            this.mainWindowViewModel = mainWindowViewModel;
            StartQuizCommand = new RelayCommand(Testing);
            Random rnd = new Random(); 

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(60);
            
            timer.Tick += Timer_Tick;

        }

        int indexer = 0;

        public void Testing(object? obj)
        {
            ButtonAnswer1 = "this is a test";
        }

        public void LoadQuestion(Object obj)
        {
            timer.Start();
/*            if(indexer <= ActivePack.Questions.Count)
            {
                return;
            }*/

            ActiveQuestion = ActivePack.Questions[indexer];

            DisplayedQuery = ActiveQuestion.Query;
            CorrectAnswer = ActiveQuestion.CorrectAnswer;
            IncorrectAnswer1 = ActiveQuestion.IncorrectAnswers[0];
            IncorrectAnswer2 = ActiveQuestion.IncorrectAnswers[1];
            IncorrectAnswer3 = ActiveQuestion.IncorrectAnswers[2];

            ObservableCollection<string> shuffledQuestions = [CorrectAnswer,IncorrectAnswer1,IncorrectAnswer2,IncorrectAnswer3];

            ShuffleObservableCollection(shuffledQuestions);
            
            ButtonAnswer1 = "test input";
            ButtonAnswer2 = shuffledQuestions[1];
            ButtonAnswer3 = shuffledQuestions[2];
            ButtonAnswer4 = shuffledQuestions[3];

            
           
        }
        public void ShuffleObservableCollection<T>(ObservableCollection<T> collection)
        {
            
            var tempList = collection.ToList();

            
            var random = new Random();
            tempList = tempList.OrderBy(x => random.Next()).ToList();

            
            collection.Clear();
            foreach (var item in tempList)
            {
                collection.Add(item);
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            indexer++;
            
        }
    }
}
