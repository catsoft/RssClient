using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Core.Extensions;
using Core.Infrastructure.ViewModels;
using Droid.Container;
using Droid.NativeExtension;
using Droid.Screens.Base;
using JetBrains.Annotations;

namespace Droid.Screens.Navigation
{
    public abstract class BaseFragment<TViewModel> : ViewModelFragment<TViewModel>, IRoot, ITitle
        where TViewModel : ViewModel
    {
        private string _title;

        protected BaseFragment() { this.Inject(true); }

        protected abstract int LayoutId { get; }

        [NotNull] public new FragmentActivity Activity => base.Activity.NotNull();

        public abstract bool IsRoot { get; }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                if (base.Activity != null)
                    Activity.Title = _title;
            }
        }

        protected abstract void RestoreState(Bundle saved);

        public override View OnCreateView([NotNull] LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (savedInstanceState != null)
                RestoreState(savedInstanceState);

            var view = inflater.Inflate(LayoutId, container, false).NotNull();

            var value = new TypedValue();
            Activity?.Theme?.ResolveAttribute(Resource.Attribute.background, value, true);
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
