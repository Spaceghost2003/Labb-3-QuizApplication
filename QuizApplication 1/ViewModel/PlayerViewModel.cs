using QuizApplication_1.Command;
using QuizApplication_1.Model;
using QuizApplication_1.View;
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

        private bool _areButtonsEnabled = true;
        public bool AreButtonsEnabled
        {
            get { return _areButtonsEnabled; }
            set
            {
                _areButtonsEnabled = value;
                OnPropertyChanged();
            }
        }
        private string _quizFInished;

        public  string QuizFinished
        {
            get { return _quizFInished; }
            set { 
                _quizFInished = value;
                OnPropertyChanged();
            }
        }
        private DispatcherTimer timer;

        private bool _isButtonVisible;

        public bool IsButtonVisible
        {
            get { return _isButtonVisible; }
            set 
            { 
                _isButtonVisible = value;
                OnPropertyChanged();
            }
        }
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
        public RelayCommand ShowConfigCommand { get; }  

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
            ShowConfigCommand = new RelayCommand(ShowConfig);
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
            set {
                _questionStep = value;
                OnPropertyChanged();
            }
        }
        private string _quizResult;

        public string QuizResult
        {
            get { return _quizResult; }
            set { _quizResult = value; }
        }
        private int QuestionTick = 0;

        public void ShowConfig(object obj)
        {
            timer.Stop();
            QuestionStep = 0;
            QuestionTick = 0;
            Points = 0;
            mainWindowViewModel.CurrentView = new ConfigurationView();
            timer.Stop();
        }
        public void StartQuiz(object obj)
        {
            Indexer = ActivePack.TimeLimit;
            QuestionStep = 0;
            QuestionTick = 0;
            timer.Start();
        }
        public void LoadQuestion()
        {
            AreButtonsEnabled = true;

            if (QuestionStep == ActivePack.Questions.Count || Indexer==0)
            {
                timer.Stop();
                QuestionStep = 0;
                QuestionTick = 0;
                var Qfinish = new QuizFinishedView();
                Qfinish.DataContext = mainWindowViewModel.PlayerViewModel;
                QuizFinished = $"Time up! you get {Points} points";
                Points = 0;
                mainWindowViewModel.CurrentView = Qfinish;
            }
            else
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
            Indexer--;
            QuestionTick++;
            //questionpack.Timelimit / Questions.Count
/*            if (QuestionStep == ActivePack.Questions.Count)
            {
                InputAnswer = $"Quiz over, you get {Points} points";
                Thread.Sleep(4000);
                mainWindowViewModel.CurrentView = new ConfigurationView();
                timer.Stop();
                
            }*/
             if(QuestionTick == 1)
            {
                LoadQuestion();
            }
             else if (QuestionTick == ActivePack.TimeLimit/ActivePack.Questions.Count && QuestionStep != ActivePack.Questions.Count-1)
            {
                QuestionTick = 0;
                QuestionStep++;
            }
            else if(Indexer == 0)
            {
                LoadQuestion();
            }
        }
        public async void CheckAnswer(object answer)
        {
            string selectedAnswer = answer as string; 

            AreButtonsEnabled = false;

            if (selectedAnswer == ActiveQuestion.CorrectAnswer)
            {
                
                InputAnswer = $"That is the correct Answer! you get one point!";
                QuestionStep++;
                timer.Stop();
                await Task.Delay(3000);
                Points++;
                timer.Start();
                LoadQuestion();
            }else if(selectedAnswer==string.Empty && QuestionTick == 5)
            {
                InputAnswer = "No answer selected!";
                QuestionStep++;
                return;
                
            }
            else if(selectedAnswer != ActiveQuestion.CorrectAnswer)
            {
                InputAnswer = $"That is the wrong answer, the correct answer is {ActiveQuestion.CorrectAnswer}";
                QuestionStep++;
                timer.Stop();
                await Task.Delay(3000);
                timer.Start();
                LoadQuestion();
                
            }
        }
    }
}
