using System;
using JetBrains.Annotations;

namespace Core.Infrastructure.Dialogs
{
    public interface IDialogService
    {
        void ShowYesNoDialog(
            [CanBeNull] string message,
            [CanBeNull] string title,
            [CanBeNull] string yes,
            [CanBeNull] string no,
            [CanBeNull] Action yesDo,
            [CanBeNull] Action noDo);
    }
}
