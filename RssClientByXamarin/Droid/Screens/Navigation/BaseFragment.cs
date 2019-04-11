using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Droid.Container;
using Droid.Screens.Base;
using Shared.Infrastructure.ViewModels;

namespace Droid.Screens.Navigation
{
    public abstract class BaseFragment<TViewModel> : ViewModelFragment<TViewModel>, IRoot, ITitle
        where TViewModel : ViewModel
    {
        private string _title;
        
        protected BaseFragment()
        {
            this.Inject(true);
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                if (Activity != null)
                    Activity.Title = _title;
            }
        }
        
        public abstract bool IsRoot { get; }

        protected abstract void RestoreState(Bundle saved);
        
        protected abstract int LayoutId { get; }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (savedInstanceState != null)
                RestoreState(savedInstanceState);

            var view = inflater.Inflate(LayoutId, container, false);

            var value = new TypedValue();
            Activity.Theme.ResolveAttribute(Resource.Attribute.background, value, true);
            view.SetBackgroundColor(new Color((int) value.Float));

            return view;
        }
        
        public override void OnDetach()
        {
            Activity?.HideKeyboard();

            base.OnDetach();
        }
    }
}