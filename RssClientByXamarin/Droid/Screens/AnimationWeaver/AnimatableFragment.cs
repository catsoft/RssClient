using System;
using Android.Graphics;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Core.Resources;
using Droid.Screens.Navigation;
using JetBrains.Annotations;

namespace Droid.Screens.AnimationWeaver
{
    public class AnimatableFragment : Fragment
    {
        [NotNull] private readonly FragmentNavigator _fragmentNavigator;

        public AnimatableFragment([NotNull] FragmentNavigator fragmentNavigator)
        {
            _fragmentNavigator = fragmentNavigator;
        }
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var random = new Random((int) DateTime.Now.Ticks);

            var linearLayout = new LinearLayout(Activity)
            {
                LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
                    ViewGroup.LayoutParams.MatchParent),
                Clickable = true,
            };

            linearLayout.SetBackgroundColor(
                new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)));
            
            linearLayout.Orientation = Orientation.Horizontal;

            AddButton(linearLayout, () => _fragmentNavigator.GoBack(), Strings.AnimatableFragmentGoBack);
            AddButton(linearLayout, () => _fragmentNavigator.GoTo(new AnimatableFragment(_fragmentNavigator)), Strings.AnimatableFragmentGoNext);

            return linearLayout;
        }

        private void AddButton([NotNull] LinearLayout linearLayout, Action action, string title)
        {
            var button = new Button(Activity)
            {
                LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
                    160) {Gravity = GravityFlags.CenterHorizontal}
            };

            button.Text = title;
            button.Click += (sender, args) => action?.Invoke();
            linearLayout.AddView(button);
        }
    }
}