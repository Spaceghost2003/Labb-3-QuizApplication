using QuizApplication_1.Command;
using QuizApplication_1.Model;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace QuizApplication_1.ViewModel
{
    internal class PlayerViewModel:ViewModelBase
    {
        private readonly MainWindowViewModel? mainWindowViewModel;
       
        public QuestionPackViewModel? ActivePack { get => mainWindowViewModel.ActivePack; }

        private DispatcherTimer timer;


  

        private Question _activeQuestion;

        public Question ActiveQuestion
        {
            get { return _activeQuestion; }
            set 
            { 
                _activeQuestion = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AnswerCommand { get; }
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

            
            this.mainWindowViewModel = mainWindowViewModel;
            StartQuizCommand = new RelayCommand(StartQuiz);
            AnswerCommand = new RelayCommand(CheckAnswer);
            Random rnd = new Random(); 

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
        
            timer.Tick += Timer_Tick;

        }

      

        private  int _indexer;

        public  int Indexer
        {
            get { return _indexer; }
            set 
            { 
                _indexer = value;
                OnPropertyChanged();
            }
        }

        private int _points;

        public int Points
        {
            get { return _points; }
            set
            { 
                _points = value; 
                OnPropertyChanged();
            }
        }

        private string wrongAnswer;

        public  string WrongAnswer
        {
            get { return wrongAnswer; }
            set 
            { 
                wrongAnswer = value;
                OnPropertyChanged();
            }
        }

        private string _inputAnswer;

        public string  InputAnswer
        {
            get { return _inputAnswer; }
            set
            {
                _inputAnswer = value;
                OnPropertyChanged();
            }
        }

        private int _questionStep;

        public int QuestionStep
        {
            get { return _questionStep; }
            set { _questionStep = value; }
        }
        

        private int QuestionTick = 0;

        public void StartQuiz(object obj)
        {
            QuestionTick = 0;
            timer.Start();
        }
        public void LoadQuestion()
        {
            ActiveQuestion = ActivePack.Questions[QuestionStep];

            DisplayedQuery = ActiveQuestion.Query;
            CorrectAnswer = ActiveQuestion.CorrectAnswer;
            IncorrectAnswer1 = ActiveQuestion.IncorrectAnswers[0];
            IncorrectAnswer2 = ActiveQuestion.IncorrectAnswers[1];
            IncorrectAnswer3 = ActiveQuestion.IncorrectAnswers[2];

            ObservableCollection<string> shuffledQuestions = new ObservableCollection<string> 
            { CorrectAnswer, IncorrectAnswer1, IncorrectAnswer2, IncorrectAnswer3 };

            ShuffleObservableCollection(shuffledQuestions);
            
            ButtonAnswer1 = shuffledQuestions[0];
            ButtonAnswer2 = shuffledQuestions[1];
            ButtonAnswer3 = shuffledQuestions[2];
            ButtonAnswer4 = shuffledQuestions[3];

            InputAnswer = string.Empty;
            Indexer++;
        }
        public void ShuffleObservableCollection<T>(ObservableCollection<T> collection)
        {
            var random = new Random();
            for (int i = collection.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (collection[i], collection[j]) = (collection[j], collection[i]); 
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Indexer++;
            QuestionTick++;
            //questionpack.Timelimit / Questions.Count
            if (QuestionStep == ActivePack.Questions.Count)
            {
                InputAnswer = $"Quiz finished, you get {Points} points";
              
               /* mainWindowViewModel.CurrentView = ;*/
            }
            else if (QuestionTick == ActivePack.TimeLimit/ActivePack.Questions.Count)
            {
                QuestionTick = 0;
                QuestionStep++;
            }
            else if(QuestionTick == 0)
            {
                LoadQuestion();
            }
        }



        public void CheckAnswer(object answer)
        {
            string selectedAnswer = answer as string;

            if (selectedAnswer == ActiveQuestion.CorrectAnswer)
            {
                //add if() if the quiz is over show points and press button to return to configview
                InputAnswer = $"That is the correct Answer! you get one point!";
                Points++;
                Thread.Sleep(3000);
                return;
            }else if(selectedAnswer==string.Empty && QuestionTick == 10)
            {
                InputAnswer = "No answer selected!";
                
            }
            else if(selectedAnswer != ActiveQuestion.CorrectAnswer)
            {
                InputAnswer = $"That is the wrong answer, the correct answer is {ActiveQuestion.CorrectAnswer}";
                Indexer++;
                return;
            }
        }
    }
}
