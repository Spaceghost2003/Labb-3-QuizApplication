using QuizApplication_1.Command;
using QuizApplication_1.Model;
using QuizApplication_1.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using System.Windows.Controls;

namespace QuizApplication_1.ViewModel
{
    class ConfigurationViewModel:ViewModelBase
    {
        
        private readonly MainWindowViewModel? mainWindowViewModel;

        private Difficulty _selectedDifficulty;

        
        public Difficulty SelectedDifficulty
        {
            get => _selectedDifficulty;
            set
            {
                _selectedDifficulty = value;
                OnPropertyChanged();
            }
        }

       
        public IEnumerable<Difficulty> DifficultyOptions => Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>();
    


        public RelayCommand FillQuestionsCommand { get; }
        public RelayCommand AddQuestionCommand { get; }
        public RelayCommand RemoveQuestionCommand { get; }
        public RelayCommand OpenModificationWindowCommand { get; }
        public RelayCommand PlayQuizCommand { get; }


        private bool _canLoadQuestions = true;

        private Question _selectedQuestion;

        public Question SelectedQuestion
        {
            get => _selectedQuestion;
            set
            {
                _selectedQuestion = value;
                OnPropertyChanged();
                RemoveQuestionCommand.RaiseCanExecuteChanged();
            }
        }


        private string _inputName;

        public string InputName
        {
            get { return _inputName; }
            set 
            {
                _inputName = value; 
                OnPropertyChanged();
            }
        }


        private string _inputQuery;
        private string _correctAnswer;
        private string _incorrectAnswer1;
        private string _incorrectAnswer2;
        private string _incorrectAnswer3;

        public string InputQuery
        {
            get { return _inputQuery; }
            set
            {
                _inputQuery = value; 
                OnPropertyChanged();
            
            }
        }
        public string CorrectAnswer
        {
            get { return _correctAnswer; }
            set
            {
                _correctAnswer = value;
                OnPropertyChanged();
            }
        }

        public string IncorrectAnswer1
        {
            get { return _incorrectAnswer1; }
            set 
            {
                _incorrectAnswer1 = value;
                OnPropertyChanged();
            }
        }
        public string IncorrectAnswer2
        {
            get { return _incorrectAnswer2; }
            set
            {
                _incorrectAnswer2 = value;
                OnPropertyChanged();
            }
        }

        public string IncorrectAnswer3
        {
            get { return _incorrectAnswer3; }
            set
            {
                _incorrectAnswer3 = value;
                OnPropertyChanged();
            }
        }


        
        public ConfigurationViewModel(MainWindowViewModel mainWindowViewModel) 
        {
            
    
            
            
            this.mainWindowViewModel = mainWindowViewModel;

            FillQuestionsCommand = new RelayCommand(FillQuestions,CanFillQuestions);
            AddQuestionCommand = new RelayCommand(AddQuestion);
            RemoveQuestionCommand = new RelayCommand(RemoveQuestion,CanRemoveQuestion);
            OpenModificationWindowCommand = new RelayCommand(OpenModificationWindow);
            PlayQuizCommand = new RelayCommand(PlayQuiz);
        }

        public QuestionPackViewModel? ActivePack {get => mainWindowViewModel.ActivePack;}
        public QuestionPackViewModel? TestPack { get => mainWindowViewModel.TestPack;}

        public PlayerViewModel OpenPlayerView { get => mainWindowViewModel.PlayerViewModel; }
        public void FillQuestions(object obj)
        {
            Question question = new Question("Who is the prime minister of Sweden?", "Ulf Kristersson", "Mona Sahlin", "Ulf Lundell", "Donald Trump");
            Question question2 = new Question("Who is the protagonist of Metal Gear Solid?", "Solid Snake", "Super Mario", "Zelda", "John Halo");
            Question question3 = new Question("What is the capital of Vietnam", "Ho Chi Minh City", "Hanoi", "tokyo", "Paris");
            Question question4 = new Question("What is 3+5?", "8", "2", "forty five", "5");
            Question question5 = new Question("Who founded Microsoft?", "Bill Gates", "John Microsoft", "Steve Jobs", "Michael Jackson");

            ActivePack.Questions.Add(question);
            ActivePack.Questions.Add(question2);
            ActivePack.Questions.Add(question3);
            ActivePack.Questions.Add(question4);
            ActivePack.Questions.Add(question5);

            _canLoadQuestions = false;

        }

        public bool CanFillQuestions(object? arg)
        {
            return _canLoadQuestions;
        }

        public void AddQuestion(object obj)
        {
            Question question = new Question(InputQuery,CorrectAnswer,IncorrectAnswer1,IncorrectAnswer2,IncorrectAnswer3);
            ActivePack.Questions.Add(question);
        }

       public void RemoveQuestion(object obj)
        {
            if(SelectedQuestion != null)
            {
                ActivePack.Questions.Remove(SelectedQuestion);
            }
        }




        private bool CanRemoveQuestion(object arg)
        {
            return SelectedQuestion != null;
        }


        public void OpenModificationWindow(object obj)
        {
            ModificationView modificationView = new ModificationView();
            modificationView.ShowDialog();
        }

        public void PlayQuiz(object obj)
        {
           mainWindowViewModel.CurrentView = new PlayerView();


        }

    }
}
