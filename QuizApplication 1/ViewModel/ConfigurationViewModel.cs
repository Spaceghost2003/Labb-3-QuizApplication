using QuizApplication_1.Command;
using QuizApplication_1.Model;
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

        /*public RelayCommand AddQuestionCommand { get; set;}*/
        public ConfigurationViewModel(MainWindowViewModel mainWindowViewModel) 
        {
    

            this.mainWindowViewModel = mainWindowViewModel;
            FillQuestionsCommand = new RelayCommand(FillQuestions);
        }

        public QuestionPackViewModel? ActivePack {get => mainWindowViewModel.ActivePack;}


        public void FillQuestions(object obj)
        {
            Question question = new Question("test", "test", "test", "test", "test");
            Question question2 = new Question("test", "test", "test", "test", "test");

            ActivePack.Questions.Add(question);
            ActivePack.Questions.Add(question2);
            ActivePack.Questions.Add(question);


        }
 
        
    }
}
