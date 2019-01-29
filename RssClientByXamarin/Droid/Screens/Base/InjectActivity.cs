using Android.OS;
using Android.Support.V7.App;
using Droid.Container;

namespace Droid.Screens.Base
{
    public abstract class InjectActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            this.Inject(true);
        }
    }
}