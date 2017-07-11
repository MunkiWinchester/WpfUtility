using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfUtility;

namespace Sample.UserControls
{
    public class HyperlinksViewModel : ObservableObject
    {
        private List<int> _integerListe = new List<int>();

        public List<int> IntegerListe
        {
            get => _integerListe;
            private set => SetField(ref _integerListe, value);
        }

        public HyperlinksViewModel()
        {
            IntegerListe = new List<int> {66, 33, 11, 99};
        }

        public void OpenNumber(int sender)
        {
            System.Diagnostics.Process.Start($"http://google.com/#q={sender}");
        }
    }
}
