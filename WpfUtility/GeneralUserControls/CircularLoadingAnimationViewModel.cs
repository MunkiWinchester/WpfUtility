using System.Windows.Media;

namespace WpfUtility.GeneralUserControls
{
    internal class CircularLoadingAnimationViewModel : ObservableObject
    {
        private SolidColorBrush _foregroundColor;
        private bool _isLoading;

        public CircularLoadingAnimationViewModel()
        {
            ForegroundColor = new SolidColorBrush(Colors.Red);
        }

        public SolidColorBrush ForegroundColor
        {
            get => _foregroundColor;
            set => SetField(ref _foregroundColor, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetField(ref _isLoading, value);
        }
    }
}