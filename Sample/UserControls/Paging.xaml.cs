using System.ComponentModel;

namespace Sample.UserControls
{
    /// <summary>
    ///     Interaction logic for Paging.xaml
    /// </summary>
    public partial class Paging
    {
        public Paging()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
                DataContext = new PagingViewModel();
        }
    }
}