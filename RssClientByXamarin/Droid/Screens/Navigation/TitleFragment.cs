using System;
using Android.Support.V4.App;

namespace Droid.Screens.Navigation
{
    public abstract class TitleFragment : BaseFragment
    {
        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                if(Activity != null)
                    Activity.Title = _title;
            }
        }

        public abstract bool RootFragment { get; }
    }
}