using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace AutoClick
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer lmbTimer;
        Timer rmbTimer;
        bool lmbHold;
        bool rmbHold;

        KeyboardHook hook;

        public MainWindow()
        {
            InitializeComponent();
            checkLmbHold.IsEnabled = false;
            checkRmbHold.IsEnabled = false;
            txtLmbDelay.IsEnabled = false;
            txtRmbDelay.IsEnabled = false;
            btnStart.IsEnabled = false;

            lmbTimer = new Timer(lmbTimer_Tick);
            rmbTimer = new Timer(rmbTimer_Tick);

            hook = new KeyboardHook();
            hook.RegisterHotKey(ModifierKeys.Control | ModifierKeys.Shift, System.Windows.Forms.Keys.C);
            hook.KeyPressed += Hook_KeyPressed;
        }

        private void Hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            lmbTimer.Change(Timeout.Infinite, Timeout.Infinite);
            rmbTimer.Change(Timeout.Infinite, Timeout.Infinite);
            root.IsEnabled = true;
            btnStart.Content = "Start";
        }

        private void lmbTimer_Tick(object data)
        {
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            if (!lmbHold)
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
        }

        private void rmbTimer_Tick(object data)
        {
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightDown);
            if (!rmbHold)
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.RightUp);
        }

        private bool canStart()
        {
            if (!checkLmb.IsChecked.GetValueOrDefault() && !checkRmb.IsChecked.GetValueOrDefault())
                return false;
            if (!int.TryParse(txtLmbDelay.Text, out _))
                return false;
            if (!int.TryParse(txtRmbDelay.Text, out _))
                return false;
            if (!int.TryParse(txtStartDelay.Text, out _))
                return false;
            return true;
        }

        private void checkLmb_Click(object sender, RoutedEventArgs e)
        {
            checkLmbHold.IsEnabled = checkLmb.IsChecked.GetValueOrDefault();
            txtLmbDelay.IsEnabled = checkLmb.IsChecked.GetValueOrDefault();
            btnStart.IsEnabled = canStart();
        }

        private void checkRmb_Click(object sender, RoutedEventArgs e)
        {
            checkRmbHold.IsEnabled = checkRmb.IsChecked.GetValueOrDefault();
            txtRmbDelay.IsEnabled = checkRmb.IsChecked.GetValueOrDefault();
            btnStart.IsEnabled = canStart();
        }

        private void txtLmbDelay_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsInitialized)
                btnStart.IsEnabled = canStart();

        }

        private void txtRmbDelay_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsInitialized)
                btnStart.IsEnabled = canStart();
        }

        private void checkLmbHold_Click(object sender, RoutedEventArgs e)
        {

        }

        private void checkRmbHold_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtLmbDelay.Text, out _))
            {
                txtLmbDelay.Focus();
                Keyboard.Focus(txtLmbDelay);
                return;
            }
            if (!int.TryParse(txtRmbDelay.Text, out _))
            {
                txtRmbDelay.Focus();
                Keyboard.Focus(txtRmbDelay);
                return;
            }
            if (!int.TryParse(txtStartDelay.Text, out _))
            {
                txtStartDelay.Focus();
                Keyboard.Focus(txtStartDelay);
                return;
            }
            if (!checkLmb.IsChecked.GetValueOrDefault() && !checkRmb.IsChecked.GetValueOrDefault())
            {
                MessageBox.Show("Nothing to click!");
                return;
            }

            if (checkLmb.IsChecked.GetValueOrDefault())
            {
                lmbHold = checkLmbHold.IsChecked.GetValueOrDefault();
                if (lmbHold)
                {
                    lmbTimer.Change(int.Parse(txtLmbDelay.Text) + int.Parse(txtStartDelay.Text), Timeout.Infinite);
                }
                else
                {
                    lmbTimer.Change(int.Parse(txtStartDelay.Text), int.Parse(txtLmbDelay.Text));
                }
            }
            if (checkRmb.IsChecked.GetValueOrDefault())
            {
                rmbHold = checkRmbHold.IsChecked.GetValueOrDefault();
                if (rmbHold)
                {
                    rmbTimer.Change(int.Parse(txtRmbDelay.Text) + int.Parse(txtStartDelay.Text), Timeout.Infinite);
                }
                else
                {
                    rmbTimer.Change(int.Parse(txtStartDelay.Text), int.Parse(txtRmbDelay.Text));
                }
            }
            btnStart.Content = "Clicking";
            root.IsEnabled = false;
        }

        private void checkTopMost_Click(object sender, RoutedEventArgs e)
        {
            Topmost = checkTopMost.IsChecked.GetValueOrDefault();
        }

        private void txtStartDelay_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsInitialized)
                btnStart.IsEnabled = canStart();
        }
    }
}
