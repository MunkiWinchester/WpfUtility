using System;
using System.Windows.Input;
using System.Windows.Media;
using WpfUtility.GeneralUserControls;
using WpfUtility.Services;

namespace Sample.UserControls
{
    public class UiServiceViewModel : ObservableObject
    {
        private const int Total = 5;

        private readonly Random _rnd = new Random();

        private SolidColorBrush _circleColor;
        private int _current = 1;

        private bool _isLoading;

        private string _mainMessage;

        private SolidColorBrush _messageForegroundColor;

        private string _subMessage;
        private SolidColorBrush _subMessageForegroundColor;

        public UiServiceViewModel()
        {
            MainMessage = "Reading data from files..";
            SubMessage = $"{_current} of {Total} files read..";
            CircleColor = new SolidColorBrush(Colors.Red);
            MessageForegroundColor = new SolidColorBrush(Colors.Black);
            SubMessageForegroundColor = new SolidColorBrush(Colors.DimGray);
        }

        public ICommand HidePanelCommand => new DelegateCommand(() => { IsLoading = false; });

        public ICommand ShowPanelCommand => new DelegateCommand(() => { IsLoading = true; });

        public ICommand ChangeSubMessageCommand =>
            new DelegateCommand(() =>
            {
                if (_current < Total)
                    _current++;
                else
                    _current = 1;
                SubMessage = $"{_current} of {Total} files read..";
            });

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                UiServices.ToggleBusyState();
                OnPropertyChanged();
            }
        }

        public string MainMessage
        {
            get => _mainMessage;
            set
            {
                _mainMessage = value;
                OnPropertyChanged();
            }
        }

        public string SubMessage
        {
            get => _subMessage;
            set
            {
                _subMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeColorCommand =>
            new DelegateCommand(() =>
            {
                CircleColor = new SolidColorBrush(
                    Color.FromArgb(255, (byte) _rnd.Next(256), (byte) _rnd.Next(256), (byte) _rnd.Next(256)));
                MessageForegroundColor = new SolidColorBrush(
                    Color.FromArgb(255, (byte) _rnd.Next(256), (byte) _rnd.Next(256), (byte) _rnd.Next(256)));
                SubMessageForegroundColor = new SolidColorBrush(
                    Color.FromArgb(255, (byte) _rnd.Next(256), (byte) _rnd.Next(256), (byte) _rnd.Next(256)));
            });

        public SolidColorBrush CircleColor
        {
            get => _circleColor;
            set
            {
                _circleColor = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush MessageForegroundColor
        {
            get => _messageForegroundColor;
            set
            {
                _messageForegroundColor = value;
                OnPropertyChanged();
            }
        }

        public SolidColorBrush SubMessageForegroundColor
        {
            get => _subMessageForegroundColor;
            set
            {
                _subMessageForegroundColor = value;
                OnPropertyChanged();
            }
        }
    }
}