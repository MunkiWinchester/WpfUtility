using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using WpfUtility;

namespace Sample.UserControls
{
    public class PagingViewModel : ObservableObject
    {
        private ObservableCollection<SampleEntry> _sampleEntries;
        public ObservableCollection<SampleEntry> SampleEntries
        {
            get => _sampleEntries;
            set => SetField(ref _sampleEntries, value);
        }

        private int _totalEntries;

        public int TotalEntries
        {
            get => _totalEntries;
            set
            {
                _totalEntries = value;
                OnPropertyChanged();
            }
        }

        private int _currentPage;

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                {
                    _currentPage = value;
                    LoadPage();
                    OnPropertyChanged();
                }
            }
        }

        private int _totalPages;

        public int TotalPages
        {
            get => _totalPages;
            set
            {
                _totalPages = value;
                OnPropertyChanged();
            }
        }

        private int _entriesPerPage;

        public int EntriesPerPage
        {
            get => _entriesPerPage;
            set
            {
                {
                    _entriesPerPage = value;
                    LoadPage();
                    OnPropertyChanged();
                }
            }
        }

        public ICommand GoToFirstPageCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    CurrentPage = 1;
                });
            }
        }

        public ICommand GoToPreviousPageCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    CurrentPage = CurrentPage - 1 >= 1 ? CurrentPage - 1 : CurrentPage;
                });
            }
        }

        public ICommand GoToNextPageCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    CurrentPage = CurrentPage + 1 <= TotalPages ? CurrentPage + 1 : CurrentPage;
                });
            }
        }

        public ICommand GoToLastPageCommand
        {
            get { return new DelegateCommand(() => { CurrentPage = TotalPages; }); }
        }

        private readonly List<SampleEntry> _allSampleEntries;

        public PagingViewModel()
        {
            _allSampleEntries = JsonHelper.ReadValuesFromSample();
            EntriesPerPage = 20;
            CurrentPage = 1;
        }

        private void LoadPage()
        {
            TotalEntries = _allSampleEntries.Count;
            TotalPages = (int)Math.Ceiling((double)TotalEntries / EntriesPerPage);
            SampleEntries =
                new ObservableCollection<SampleEntry>(_allSampleEntries.Skip(EntriesPerPage * (CurrentPage - 1))
                    .Take(EntriesPerPage));
        }
    }
}
