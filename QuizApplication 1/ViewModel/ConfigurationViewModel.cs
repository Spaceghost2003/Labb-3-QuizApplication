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

namespace QuizApplication_1.ViewModel
{
    class ConfigurationViewModel:ViewModelBase
    {
        
        private readonly MainWindowViewModel? mainWindowViewModel;

        public RelayCommand FillQuestionsCommand { get; }
        public RelayCommand AddQuestionCommand { get; }
        public RelayCommand RemoveQuestionCommand { get; }

        public RelayCommand OpenModificationWindowCommand { get; }

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
            FillQuestionsCommand = new RelayCommand(FillQuestions);
            AddQuestionCommand = new RelayCommand(AddQuestion);
            RemoveQuestionCommand = new RelayCommand(RemoveQuestion,CanRemoveQuestion);
            OpenModificationWindowCommand = new RelayCommand(OpenModificationWindow);
        }

        public QuestionPackViewModel? ActivePack {get => mainWindowViewModel.ActivePack;}


        public void FillQuestions(object obj)
        {
            Question question = new Question("test", "test", "test", "test", "test");
            Question question2 = new Question("test1", "test", "test", "test", "test");

            ActivePack.Questions.Add(question);
            ActivePack.Questions.Add(question2);
            ActivePack.Questions.Add(question);


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

    }
}
