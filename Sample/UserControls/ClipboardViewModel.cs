using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfUtility.Services;

namespace Sample.UserControls
{
    public class ClipboardViewModel : ObservableObject
    {
        private ObservableCollection<string> _pasteElements = new ObservableCollection<string>();
        private ObservableCollection<int> _selectedProducts = new ObservableCollection<int>();

        public ClipboardViewModel()
        {
            PasteElements =
                new ObservableCollection<string>(
                    new List<string> {"218382", "344846", "5614645", "abcdef", "ghijkl", "218382", "mnopqr"});
        }

        public ObservableCollection<int> SelectedProducts
        {
            get => _selectedProducts;
            private set => SetField(ref _selectedProducts, value);
        }

        public ObservableCollection<string> PasteElements
        {
            get => _pasteElements;
            private set => SetField(ref _pasteElements, value);
        }


        public ICommand PasteCommand => new DelegateCommand(Paste);

        public ICommand EmptyListCommand => new DelegateCommand(EmptyList);

        private void Paste()
        {
            var rowData = ClipboardHelper.ParseClipboardData().Select(x => x[0]).ToList();
            var cleansedList = new List<int>();
            foreach (var entry in rowData)
                if (int.TryParse(entry, out var value))
                    cleansedList.Add(value);
            SelectedProducts = new ObservableCollection<int>(cleansedList);
            if (!cleansedList.Any())
                MessageBox.Show(
                    $@"Your clipboard content is empty or is in a wrong format.{
                            Environment.NewLine
                        }Please paste only valid numbers!",
                    "Clipboard content not valid!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void EmptyList()
        {
            SelectedProducts = new ObservableCollection<int>();
        }
    }
}