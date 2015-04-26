using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TimeToTomato
{
    /// <summary>
    /// Interaction logic for DoneWindow.xaml
    /// </summary>
    public partial class DoneWindow : Window
    {
        public DoneWindow()
        {
            InitializeComponent();
            Loaded += DoneWindow_Loaded;
        }

        void DoneWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation(25, 75, new TimeSpan(0, 0, 0, 0, 500));
            txtDone.BeginAnimation(TextBlock.FontSizeProperty, da);

            SystemSounds.Beep.Play();
        }
    }
}
