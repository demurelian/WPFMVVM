using System.Windows;
using WPFMVVM.Infrastructure.Commands.Base;

namespace WPFMVVM.Infrastructure.Commands
{
    internal class CloseApplicationCommand : Command
    {
        public override bool CanExecute(object? parameter) => true;
        public override void Execute(object? parameter) => Application.Current.Shutdown();
    }
}
