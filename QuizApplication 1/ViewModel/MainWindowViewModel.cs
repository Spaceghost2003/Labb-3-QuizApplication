using QuizApplication_1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public MainWindowViewModel()
        {
			PlayerViewModel = new PlayerViewModel(this);
			ConfigurationViewModel = new ConfigurationViewModel(this);
			ActivePack = new QuestionPackViewModel(new QuestionPack("Active Question Pack"));
        }


		
	

    }
}
