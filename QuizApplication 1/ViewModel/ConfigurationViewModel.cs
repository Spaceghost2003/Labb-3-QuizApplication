using Microsoft.Win32;
using QuizApplication_1.Command;
using QuizApplication_1.Model;
using QuizApplication_1.Service;
using QuizApplication_1.View;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace QuizApplication_1.ViewModel
{
    class ConfigurationViewModel:ViewModelBase
    {
        
        private readonly MainWindowViewModel? mainWindowViewModel;

        public PlayerViewModel? playerViewModel;

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


        public RelayCommand SaveQuestionPackCommand { get;}
        public RelayCommand FillQuestionsCommand { get; }
        public RelayCommand AddQuestionCommand { get; }
        public RelayCommand RemoveQuestionCommand { get; }
        public RelayCommand OpenModificationWindowCommand { get; }
        public RelayCommand PlayQuizCommand { get; }
        public RelayCommand LoadQuestionCommand { get; }
        public RelayCommand NewPackCommand { get; }

        public RelayCommand ToggleFullscreenCommand { get; }    

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



            SaveQuestionPackCommand = new RelayCommand(SavePack);
            FillQuestionsCommand = new RelayCommand(FillQuestions,CanFillQuestions);
            AddQuestionCommand = new RelayCommand(AddQuestion);
            RemoveQuestionCommand = new RelayCommand(RemoveQuestion,CanRemoveQuestion);
            OpenModificationWindowCommand = new RelayCommand(OpenModificationWindow);
            PlayQuizCommand = new RelayCommand(PlayQuiz);
            LoadQuestionCommand = new RelayCommand(LoadPack);
            NewPackCommand = new RelayCommand(NewPack);
            ToggleFullscreenCommand = new RelayCommand(ToggleFullscreen);
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
            Question question = new Question
                (
                InputQuery,
                CorrectAnswer,
                IncorrectAnswer1,
                IncorrectAnswer2,
                IncorrectAnswer3);
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

            modificationView.DataContext = new ConfigurationViewModel(mainWindowViewModel);
            modificationView.ShowDialog();
        }

        public void PlayQuiz(object obj)
        {
           mainWindowViewModel.CurrentView = new PlayerView();
            
           
          
        }
        
        public void SavePack(object obj)
        {
            string filename = ActivePack.Name;

            QuestionPackSaver.SaveToJson(ActivePack, filename);
        }


        public async void LoadPack(object obj)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"QuizApplication")
            };
            bool? success = fileDialog.ShowDialog();

            if (success == true)
            {
                string path = fileDialog.FileName;

      
                var jsonContent = await File.ReadAllTextAsync(path);
                QuestionPackDTO setter = JsonSerializer.Deserialize<QuestionPackDTO>(jsonContent);

                
                QuestionPack starter = new QuestionPack(setter.Name)
                {
                    Name = setter.Name,
                    Difficulty = setter.Difficulty,
                    TimeLimit = setter.TimeLimit,
                    Questions = setter.Questions.Select(q => new Question(
                        q.Query,
                        q.CorrectAnswer,
                        q.IncorrectAnswers[0],
                        q.IncorrectAnswers[1],
                        q.IncorrectAnswers[2]
                    )).ToList()


                };
                
                QuestionPackViewModel loadedPack = new QuestionPackViewModel(starter);
                mainWindowViewModel.ActivePack = loadedPack;
            }
        }

        private void ToggleFullscreen(object obj)
        {
            var mainWindow = Application.Current.MainWindow;
            if (mainWindow.WindowState == WindowState.Maximized && mainWindow.WindowStyle == WindowStyle.None)
            { 
                mainWindow.WindowState = WindowState.Normal;
                mainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            }
            else
            {
                mainWindow.WindowStyle = WindowStyle.None;
                mainWindow.WindowState = WindowState.Maximized;
            }
        }

        public static QuestionPackViewModel LoadFromJson(string fileName)
        {
            
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "QuizApplication");

            
            Directory.CreateDirectory(appDataPath);

            string filePath = Path.Combine(appDataPath, fileName);

            
            if (!File.Exists(filePath))
            {
                return null; 
            }

          
            string json = File.ReadAllText(filePath);

            
            var questionPackViewModel = JsonSerializer.Deserialize<QuestionPackViewModel>(json);

            if (questionPackViewModel == null)
            {
                return null; 
            }

           
           

            return questionPackViewModel;
        }
        public void NewPack(object obj)
        {
            mainWindowViewModel.ActivePack.Questions.Clear();
        }
    }
}


