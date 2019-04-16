using System;

namespace Shared.Infrastructure.Dialogs
{
    public interface IDialogService
    {
        void ShowYesNoDialog(string message, string title, string yes, string no, Action yesDo, Action noDo);
    }
}