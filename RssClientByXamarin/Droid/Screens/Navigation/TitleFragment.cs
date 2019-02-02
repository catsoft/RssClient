using System;

namespace Droid.Screens.Navigation
{
    public abstract class TitleFragment : BaseFragment
    {
        protected event Action OnDetachEvent;

        private string _title;

        public string Title
        {
            get => _title;
            protected set
            {
                _title = value;
                if (Activity != null)
                    Activity.Title = _title;
            }
        }

        public abstract bool RootFragment { get; }

        public override void OnDetach()
        {
            base.OnDetach();
            
            OnDetachEvent?.Invoke();
        }
    }
}