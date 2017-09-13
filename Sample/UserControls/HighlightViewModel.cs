using System.Collections.ObjectModel;
using WpfUtility;
using WpfUtility.Services;

namespace Sample.UserControls
{
    public class HighlightViewModel : ObservableObject
    {
        private ObservableCollection<SampleEntry> _sampleEntries;

        public HighlightViewModel()
        {
            SampleEntries = new ObservableCollection<SampleEntry>(JsonHelper.ReadValuesFromSample());
        }

        public ObservableCollection<SampleEntry> SampleEntries
        {
            get => _sampleEntries;
            set => SetField(ref _sampleEntries, value);
        }
    }
}