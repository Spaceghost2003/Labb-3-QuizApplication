using QuizApplication_1.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace QuizApplication_1.ViewModel
{
    internal class PlayerViewModel:ViewModelBase
    {
        public RelayCommand UpdateButtonCommand { get; }
        
        public MainWindowViewModel? MainWindowViewModel { get; }

        private DispatcherTimer timer;
        private string _testData;
        private object mainWindowViewModel;

        public string testData { get => _testData;
            private set 
            {
                _testData = value; 
                OnPropertyChanged();
            }
        }

        public PlayerViewModel(MainWindowViewModel? mainWindowViewModel)
        {
            this.MainWindowViewModel = mainWindowViewModel;

            testData = "Start Value:";

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            //timer.Start();

        }
      



        private void Timer_Tick(object? sender, EventArgs e)
        {
            testData += "x";
        }
    }
}
