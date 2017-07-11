using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUtility;

namespace Sample.UserControls
{
    public class HighlightViewModel : ObservableObject
    {
        private ObservableCollection<SampleEntry> _sampleEntries;
        public ObservableCollection<SampleEntry> SampleEntries
        {
            get => _sampleEntries;
            set => SetField(ref _sampleEntries, value);
        }

        public HighlightViewModel()
        {
            SampleEntries = new ObservableCollection<SampleEntry>(JsonHelper.ReadValuesFromSample());
        }
    }
}
