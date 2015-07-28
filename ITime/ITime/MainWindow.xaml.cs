using System;
using System.Windows;
using System.Windows.Input;
using ITime.ViewModels;

namespace ITime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DoneWindow _doneWindow;
        MainViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();

            double left = Properties.Settings.Default.LeftPosition;
            double top = Properties.Settings.Default.Topposition;
            if (left < 0 || top < 0)
            {
                WindowStartupLocation = WindowStartupLocation.Manual;
                Left = SystemParameters.VirtualScreenWidth / 2 - 100;
                Top = 0;
            }
            else
            {
                Left = left;
                Top = top;
            }

            _vm = new MainViewModel();
            DataContext = _vm;

            LocationChanged += MainWindow_LocationChanged;

            _vm.PropertyChanged += _vm_PropertyChanged;
        }

        void _vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsDone")
            {
                if (_vm.IsDone)
                    ShowDone();
                else CloseDone();
            }
        }

        void CloseDone()
        {
            if (_doneWindow != null)
            {
                _doneWindow.Close();
                _doneWindow = null;
            }
        }

        void ShowDone()
        {
            _doneWindow = new DoneWindow();
            _doneWindow.MouseEnter += _doneWindow_MouseEnter;
            _doneWindow.Show();
        }

        void _doneWindow_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseDone();
        }

        void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            if (Top < 0)
                Top = 0;
            if (Left < 0)
                Left = 0;
            if (Top > SystemParameters.WorkArea.Bottom - ActualHeight)
                Top = SystemParameters.WorkArea.Bottom - ActualHeight;
            if (Left > SystemParameters.WorkArea.Right - ActualWidth)
                Left = SystemParameters.WorkArea.Right - ActualWidth;
        }

        void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_doneWindow != null)
                _doneWindow.Close();

            Properties.Settings.Default.LeftPosition = Left;
            Properties.Settings.Default.Topposition = Top;
            Properties.Settings.Default.Save();
            Close();
        }

        void ctrMoving_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}
