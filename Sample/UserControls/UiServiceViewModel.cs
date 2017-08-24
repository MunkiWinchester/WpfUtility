using System.Windows.Input;
using WpfUtility;

namespace Sample.UserControls
{
    public class UiServiceViewModel : ObservableObject
    {
        public ICommand HidePanelCommand => new DelegateCommand(() => { IsLoading = false; });

        public ICommand ShowPanelCommand => new DelegateCommand(() => { IsLoading = true; });

        public ICommand ChangeSubMessageCommand => new DelegateCommand(() =>
        {
            if (_current < Total)
                _current++;
            else
                _current = 1;
            SubMessage = $"{_current} of {Total} files read..";
        });

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private string _mainMessage;
        public string MainMessage
        {
            get => _mainMessage;
            set { _mainMessage = value; OnPropertyChanged(); }
        }

        private string _subMessage;
        public string SubMessage
        {
            get => _subMessage;
            set { _subMessage = value; OnPropertyChanged(); }
        }

        private const int Total = 5;
        private int _current = 1;

        public UiServiceViewModel()
        {
            MainMessage = "Reading data from files..";
            SubMessage = $"{_current} of {Total} files read..";
        }
    }
}