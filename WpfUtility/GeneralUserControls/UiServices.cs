using System.Windows.Input;

namespace WpfUtility.GeneralUserControls
{
    /// <summary>
    /// Contains helper methods for UI, so far just one for showing a wait cursor
    /// </summary>
    public class UiServices
    {
        /// <summary>
        /// Indicating whether the UI is currently busy or not
        /// </summary>
        private static bool _isBusy;

        /// <summary>
        /// Sets the busystate
        /// </summary>
        public static void ToggleBusyState()
        {
            _isBusy = !_isBusy;
            Mouse.OverrideCursor = _isBusy ? Cursors.Wait : null;
        }
    }
}