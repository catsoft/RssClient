using System;
using System.Reactive.Linq;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Core.Extensions;
using Core.ViewModels.RssFeeds.Create;
using Droid.NativeExtension;
using Droid.NativeExtension.Events;
using Droid.Screens.Navigation;
using JetBrains.Annotations;
using ReactiveUI;

namespace Droid.Screens.RssFeeds.Create
{
    public class RssFeedCreateFragment : BaseFragment<RssFeedCreateViewModel>
    {
        [NotNull] private RssFeedCreateFragmentViewHolder _viewHolder;

        protected override int LayoutId => Resource.Layout.fragment_rss_create;
        public override bool IsRoot => false;

        // ReSharper disable once NotNullMemberIsNotInitialized
        // ReSharper disable once EmptyConstructor
        public RssFeedCreateFragment()
        {
        }
        
        protected override void RestoreState(Bundle saved) { }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            Title = GetText(Resource.String.create_title);

            _viewHolder = new RssFeedCreateFragmentViewHolder(view);

            OnActivation(disposable =>
            {
                this.Bind(ViewModel, model => model.Url, fragment => fragment._viewHolder.EditText.Text)
                    .AddTo(disposable);

                ViewModel.Url.WhenAnyValue(s => s)
                    .Subscribe(s => _viewHolder.EditText.SetTextAndSetCursorToLast(s))
                    .AddTo(disposable);

                _viewHolder.EditText.GetEditorAction()
                    .Subscribe(action =>
                    {
                        if (action.ActionId == ImeAction.Done) _viewHolder.SendButton.CallOnClick();
                    })
                    .AddTo(disposable);

                this.BindCommand(ViewModel, model => model.CreateCommand, fragment => fragment._viewHolder.SendButton)
                    .AddTo(disposable);
                
                ViewModel.WhenAnyValue(w => w.Url)
                    .Select(w => !Patterns.WebUrl.Matcher(_viewHolder.EditText.Text).Matches())
                    .Subscribe(w => ViewModel.IsUrlInvalid = w)
                    .AddTo(disposable);

                ViewModel.WhenAnyValue(w => w.ErrorMessage)
                    .BindTo(_viewHolder.TextInputLayout, layout => layout.Error)
                    .AddTo(disposable);
                
                ViewModel.WhenAnyValue(w => w.IsUrlInvalid)
                    .BindTo(_viewHolder.TextInputLayout, layout => layout.ErrorEnabled)
                    .AddTo(disposable);
            });

            return view;
        }
    }
}
