using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Android.Content;
using Android.OS;
using Android.Views;
using JetBrains.Annotations;
using ReactiveUI.AndroidSupport;

namespace Droid.Screens.Base
{
    public class LifeCycleFragment<TViewModel> : ReactiveFragment<TViewModel>
        where TViewModel : class
    {
        [NotNull] private readonly ISubject<Bundle> _activityCreatingSubject = new Subject<Bundle>();
        [NotNull] private readonly ISubject<Context> _attachingSubject = new Subject<Context>();
        [NotNull] private readonly ISubject<Bundle> _creatingSubject = new Subject<Bundle>();
        [NotNull] private readonly ISubject<Context> _creatingViewSubject = new Subject<Context>();
        [NotNull] private readonly ISubject<Context> _destroyingSubject = new Subject<Context>();
        [NotNull] private readonly ISubject<Context> _destroyingViewSubject = new Subject<Context>();
        [NotNull] private readonly ISubject<Context> _detachingSubject = new Subject<Context>();

        [NotNull] private readonly ISubject<Context> _pausingSubject = new Subject<Context>();
        [NotNull] private readonly ISubject<Context> _resumingSubject = new Subject<Context>();
        [NotNull] private readonly ISubject<Context> _startingSubject = new Subject<Context>();
        [NotNull] private readonly ISubject<Context> _stoppingSubject = new Subject<Context>();

        [NotNull] public IObservable<Context> Attaching => _attachingSubject.AsObservable();
        [NotNull] public IObservable<Bundle> Creating => _creatingSubject.AsObservable();
        [NotNull] public IObservable<Context> CreatingView => _creatingViewSubject.AsObservable();
        [NotNull] public IObservable<Bundle> ActivityCreating => _activityCreatingSubject.AsObservable();
        [NotNull] public IObservable<Context> Starting => _startingSubject.AsObservable();
        [NotNull] public IObservable<Context> Resuming => _resumingSubject.AsObservable();

        [NotNull] public IObservable<Context> Pausing => _pausingSubject.AsObservable();
        [NotNull] public IObservable<Context> Stopping => _stoppingSubject.AsObservable();
        [NotNull] public IObservable<Context> DestroyingView => _destroyingViewSubject.AsObservable();
        [NotNull] public IObservable<Context> Destroying => _destroyingSubject.AsObservable();
        [NotNull] public IObservable<Context> Detaching => _detachingSubject.AsObservable();

        [NotNull] public CompositeDisposable Disposables { get; } = new CompositeDisposable();

        public void OnActivation([CanBeNull] Action<CompositeDisposable> d) { Resuming.Take(1).Subscribe(_ => d?.Invoke(Disposables)); }

        public override void OnAttach(Context context)
        {
            base.OnAttach(context);
            _attachingSubject.OnNext(context);
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _creatingSubject.OnNext(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            _creatingViewSubject.OnNext(Activity);
            return null;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            _activityCreatingSubject.OnNext(savedInstanceState);
        }

        public override void OnStart()
        {
            base.OnStart();
            _startingSubject.OnNext(Activity);
        }

        public override void OnResume()
        {
            base.OnResume();
            _resumingSubject.OnNext(Activity);
        }

        public override void OnPause()
        {
            base.OnPause();
            _pausingSubject.OnNext(Activity);
        }

        public override void OnStop()
        {
            base.OnStop();
            _stoppingSubject.OnNext(Activity);
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
            DisposeActivations();
            _destroyingViewSubject.OnNext(Activity);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            DisposeActivations();
            _destroyingSubject.OnNext(Activity);
        }

        public override void OnDetach()
        {
            base.OnDetach();
            _detachingSubject.OnNext(Activity);
        }

        private void DisposeActivations()
        {
            foreach (var disposable in Disposables)
                disposable.Dispose();
            Disposables.Clear();
        }
    }
}
