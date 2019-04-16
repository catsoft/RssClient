using System;
using Android.App;
using Autofac;
using Shared;
using Shared.Infrastructure.Dialogs;

namespace Droid.Infrastructure.Dialogs
{
    public class DialogService : IDialogService
    {
        public void ShowYesNoDialog(string message, string title, string yes, string no, Action yesDo, Action noDo)
        {
            var activity = App.Container.Resolve<Activity>();
            var builder = new AlertDialog.Builder(activity);
            builder.SetPositiveButton(yes, (sender, args) => { yesDo?.Invoke(); });
            builder.SetNegativeButton(no, (sender, args) => { noDo?.Invoke(); });
            builder.SetTitle(title);
            builder.SetMessage(message);
            builder.Show();
        }
    }
}