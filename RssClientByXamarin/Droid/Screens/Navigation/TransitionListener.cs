using System;
using Android.Support.Transitions;
using JetBrains.Annotations;

namespace Droid.Screens.Navigation
{
    public class TransitionListener : TransitionListenerAdapter
    {
        private readonly Action _action;

        public TransitionListener(Action action)
        {
            _action = action;
        }

        public override void OnTransitionEnd([NotNull] Transition transition)
        {
            base.OnTransitionEnd(transition);

            _action?.Invoke();

            transition.RemoveListener(this);
        }
    }
}