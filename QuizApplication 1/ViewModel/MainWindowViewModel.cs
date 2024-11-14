using QuizApplication_1.Command;
using QuizApplication_1.Model;
using QuizApplication_1.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace QuizApplication_1.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {

     


        

        public ObservableCollection<QuestionPackViewModel> Packs { get; set; }

		public PlayerViewModel PlayerViewModel {get;}



		public ConfigurationViewModel ConfigurationViewModel { get;}

		private QuestionPackViewModel? _activePack;

		public QuestionPackViewModel? ActivePack
		{
			get => _activePack; 
			set
			{
				_activePack = value;
				OnPropertyChanged();
				ConfigurationViewModel.OnPropertyChanged(nameof(ActivePack));
			}
		}

		private QuestionPackViewModel _testPack;

		public QuestionPackViewModel TestPack
		{
			get { return _testPack; }
			set 
			{ 
				_testPack = value;
				OnPropertyChanged();
                ConfigurationViewModel.OnPropertyChanged(nameof(ActivePack));
            }
          
		}


		public MainWindowViewModel()
		{

			
            PlayerViewModel = new PlayerViewModel(this);
			ConfigurationViewModel = new ConfigurationViewModel(this);
			ActivePack = new QuestionPackViewModel(new QuestionPack("Active Question Pack"));

			TestPack = new QuestionPackViewModel(new QuestionPack("This is a a test pack") );


			
		}

		
	

        //---------------------------------------------------------------------------------------





    }
}
