using System;
using System.ComponentModel;

namespace Sample.UserControls
{
    /// <summary>
    /// Interaction logic for Hyperlinks.xaml
    /// </summary>
    public partial class Hyperlinks
    {
        private readonly HyperlinksViewModel _viewModel;

        public Hyperlinks()
        {
            _viewModel = new HyperlinksViewModel();
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
                DataContext = _viewModel;
        }

        private void ListHyperlinkTextBlock_OnHyperlinkClicked(int sender, EventArgs e)
        {
            _viewModel.OpenNumber(sender);
        }
    }
}