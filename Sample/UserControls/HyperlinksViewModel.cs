using System.Collections.Generic;
using System.Diagnostics;
using WpfUtility.Services;

namespace Sample.UserControls
{
    public class HyperlinksViewModel : ObservableObject
    {
        private List<int> _integerListe = new List<int>();

        public HyperlinksViewModel()
        {
            IntegerListe = new List<int> {66, 33, 11, 99};
        }

        public List<int> IntegerListe
        {
            get => _integerListe;
            private set => SetField(ref _integerListe, value);
        }

        public void OpenNumber(int sender)
        {
            Process.Start($"http://google.com/#q={sender}");
        }
    }
}