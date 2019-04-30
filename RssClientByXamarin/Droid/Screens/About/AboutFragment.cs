using Android.OS;
using Android.Views;
using Core.Extensions;
using Core.ViewModels.About;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.About
{
    public class AboutFragment : BaseFragment<AboutViewModel>
    {
        [NotNull] private AboutFragmentViewHolder _viewHolder;

        protected override int LayoutId => Resource.Layout.fragment_about;
        public override bool IsRoot => true;

        // ReSharper disable once EmptyConstructor
        // ReSharper disable once NotNullMemberIsNotInitialized
        public AboutFragment() { }

        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState).NotNull();

            _viewHolder = new AboutFragmentViewHolder(view);
            
            Title = GetText(Resource.String.about_title);

            OnActivation(disposable =>
            {
                this.OneWayBind(ViewModel, model => model.AboutVersionCompliance, fragment => fragment._viewHolder.VersionTextView.Text)
                    .AddTo(disposable);
                
                this.OneWayBind(ViewModel, model => model.OtherText, fragment => fragment._viewHolder.OtherTextView.Text)
                    .AddTo(disposable);
                
                this.OneWayBind(ViewModel, model => model.ProjectLink, fragment => fragment._viewHolder.ProjectLinkTextView.Text)
                    .AddTo(disposable);
            });
            
            return view;
        }
    }
}
