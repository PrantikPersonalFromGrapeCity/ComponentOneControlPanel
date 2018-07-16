using GrapeCity.Tools.ComponentOneControlPanel.Containers;
using GrapeCity.Tools.ComponentOneControlPanel.Operations;
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

namespace GrapeCity.Tools.ComponentOneControlPanel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            System.Drawing.Image img = Properties.Resources.spinner;
            progControl.AnimatedBitmap = (System.Drawing.Bitmap)img;
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ShowProgress();
            Task.Factory.StartNew(() =>
            {
                var webOps = new WebOps();
                webOps.ProgressCompleted += WebOps_ProgressCompleted;
                webOps.ReadDataFromServer();
            });
        }

        private void WebOps_ProgressCompleted(object sender, EventArgs e)
        {
            ShowProgress(true);
            var retArg = (WebJsonArg)e;
            if(retArg.Success==false)
            {
                if(retArg.Failuretype==WebJsonFailureEnum.FailureInReadingResource)
                {
                    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Render, new Action(() =>
                    {
                        
                        ShowMessage(App.Current.Resources["ErrorReadingJsonLinkResource"] as string);
                    }));
                    return;
                }
                if (retArg.Failuretype == WebJsonFailureEnum.FailureInFetchingFromWeb)
                {
                    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Render, new Action(() =>
                    {

                        ShowMessage(App.Current.Resources["ErrorReadingJsonLinkWeb"] as string);
                    }));
                    return;
                }

            }
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Render, new Action(() =>
            {
                Level1_Click(productsButtonLevel1,null);
                


            }));

        }

        void ShowMessage(string txt)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Render, new Action(() =>
            {
                messageBlk.Text = txt;
                msgBorderMain.Visibility = Visibility.Visible;
                msgBorder.Visibility = Visibility.Visible;
                closeBtn.IsEnabled = false;

            }));

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                try
                {
                    this.DragMove();
                }
                catch
                {

                }

            }
        }

        void ShowProgress(bool hide = false)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Render, new Action(() =>
            {
                CloseMessage(null,null);
                msgBorder.Visibility = Visibility.Hidden;
                nonProgressBrdr.Visibility = hide ? Visibility.Visible : Visibility.Hidden;
                prgrsBrdr.Visibility = hide ? Visibility.Hidden : Visibility.Visible;


            }));

        }
        private void CloseApp(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
            Environment.Exit(0);
        }

        private void contactusblock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Level1_Click(object sender, RoutedEventArgs e)
        {
            var level1Buttons = level1Panel.Children.OfType<Button>()?.ToList();
            if (level1Buttons == null || level1Buttons.Count() == 0) return;
            foreach (var level1Button in level1Buttons)
                level1Button.Style = App.Current.Resources["topBtnStyle"] as Style;
            ((Button)sender).Style= App.Current.Resources["topBtnClickedStyle"] as Style;
            var btnName = ((Button)sender).Name;
            switch(btnName)
            {
                case "productsButtonLevel1":
                    {
                        var types = GlobalVariables.ProductsJson.SelectToken("Root[*].Products[*]");
                        break;
                    }
            }
        }

        private void CloseMessage(object sender, RoutedEventArgs e)
        {
            msgBorderMain.Visibility = Visibility.Hidden;
            msgBorder.Visibility = Visibility.Hidden;
            closeBtn.IsEnabled = true;
        }

        private void CopyMessage(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(messageBlk.Text);
        }
    }
}
