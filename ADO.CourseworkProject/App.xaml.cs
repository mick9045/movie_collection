using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace FilmRent
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow = new Views.MainWindow();
            var mainWindowViewModel = new ViewModels.MainWindowViewModel();
            // Create the ViewModel to which 
            // the main window binds. 
            mainWindowViewModel.RequestClose += delegate { MainWindow.Close(); };
            // Allow all controls in the window to 
            // bind to the ViewModel by setting the 
            // DataContext, which propagates down
            // the element tree. 
            MainWindow.DataContext = mainWindowViewModel;
            MainWindow.Show();
        }

       
    }
}
