using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeToTomato.ViewModels;

namespace TimeToTomato
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.Left = SystemParameters.VirtualScreenWidth / 2 - 100;
            this.Top = 0;
            this.DataContext = new MainViewModel();

            this.LocationChanged += MainWindow_LocationChanged;
        }

        void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            if (this.Top < 0)
                this.Top = 0;
            if (this.Left < 0)
                this.Left = 0;
            if (this.Top > SystemParameters.WorkArea.Bottom - this.ActualHeight)
                this.Top = SystemParameters.WorkArea.Bottom - this.ActualHeight;
            if (this.Left > SystemParameters.WorkArea.Right - this.ActualWidth)
                this.Left = SystemParameters.WorkArea.Right - this.ActualWidth;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ctrMoving_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
