using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ProjectTalon.Core.Data;
using ProjectTalon.UI.ViewModels;
using ProjectTalon.UI.Views;
using Splat;

namespace ProjectTalon.UI
{
    public partial class App : Application
    {
        public App()
        {
            DataContext = new ApplicationViewModel();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var vm = Locator.Current.GetService<IMainWindowViewModel>();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = vm
                };
                ((ApplicationViewModel)DataContext).SetWindow(desktop.MainWindow);
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}