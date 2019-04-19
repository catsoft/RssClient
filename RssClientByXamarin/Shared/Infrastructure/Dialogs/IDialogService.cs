#region

using System;
using JetBrains.Annotations;

#endregion

namespace Shared.Infrastructure.Dialogs
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
