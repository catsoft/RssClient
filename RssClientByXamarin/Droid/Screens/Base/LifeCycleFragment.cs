using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Android.Content;
using Android.OS;
using Android.Views;

namespace Droid.Screens.Base
{
    public class LifeCycleFragment<TViewModel> : ReactiveUI.AndroidSupport.ReactiveFragment<TViewModel>
        where TViewModel : class
    {
        private readonly ISubject<Context> _attachingSubject = new Subject<Context>();
        private readonly ISubject<Bundle> _creatingSubject = new Subject<Bundle>();
        private readonly ISubject<Context> _creatingViewSubject = new Subject<Context>();
        private readonly ISubject<Bundle> _activityCreatingSubject = new Subject<Bundle>();
        private readonly ISubject<Context> _startingSubject = new Subject<Context>();
        private readonly ISubject<Context> _resumingSubject = new Subject<Context>();
        
        private readonly ISubject<Context> _pausingSubject = new Subject<Context>();
        private readonly ISubject<Context> _stoppingSubject = new Subject<Context>();
        private readonly ISubject<Context> _destroyingViewSubject = new Subject<Context>();
        private readonly ISubject<Context> _destroyingSubject = new Subject<Context>();
        private readonly ISubject<Context> _detachingSubject = new Subject<Context>();
        
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public IObservable<Context> Attaching => _attachingSubject.AsObservable();
        public IObservable<Bundle> Creating => _creatingSubject.AsObservable();
        public IObservable<Context> CreatingView => _creatingViewSubject.AsObservable();
        public IObservable<Bundle> ActivityCreating => _activityCreatingSubject.AsObservable();
        public IObservable<Context> Starting => _startingSubject.AsObservable();
        public IObservable<Context> Resuming => _resumingSubject.AsObservable();
        
        public IObservable<Context> Pausing => _pausingSubject.AsObservable();
        public IObservable<Context> Stopping => _stoppingSubject.AsObservable();
        public IObservable<Context> DestroyingView => _destroyingViewSubject.AsObservable();
        public IObservable<Context> Destroying => _destroyingSubject.AsObservable();
        public IObservable<Context> Detaching => _detachingSubject.AsObservable();

        public CompositeDisposable Disposables => _disposables; 

        public void OnActivation(Action<CompositeDisposable> d)
        {
            Resuming.Take(1).Subscribe(_ => d(_disposables));
        }

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
            foreach (var disposable in _disposables)
                disposable.Dispose();
            _disposables.Clear();
        }
    }
}