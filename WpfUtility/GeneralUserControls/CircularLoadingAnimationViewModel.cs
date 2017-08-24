using System.Windows.Media;

namespace WpfUtility.GeneralUserControls
{
    internal class CircularLoadingAnimationViewModel : ObservableObject
    {
        private SolidColorBrush _foregroundColor;

        public CircularLoadingAnimationViewModel()
        {
            ForegroundColor = new SolidColorBrush(Colors.Red);
        }

        public SolidColorBrush ForegroundColor
        {
            get => _foregroundColor;
            set => SetField(ref _foregroundColor, value);
        }
    }
}