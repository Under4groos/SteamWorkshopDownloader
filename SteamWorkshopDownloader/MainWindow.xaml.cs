using SteamWorkshopDownloaderLib.Model;
using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SteamWorkshopDownloader
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ResSteamCmd resSteamCmd = new ResSteamCmd();

        public MainWindow()
        {
            SteamcmdKiller.Start();
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closing += (o, e) =>
            {
                resSteamCmd.Dispose();
                SteamcmdKiller.Start();
            };

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists("DATA.txt"))
                add_id_game.Text = File.ReadAllText("DATA.txt");
            resSteamCmd.Install();
            resSteamCmd.Login();
            resSteamCmd.InstallationСompleted += () =>
            {
                this.Dispatcher.BeginInvoke((ThreadStart)delegate ()
                {
                    
                    install_page.Visibility = Visibility.Collapsed;
                    main_menu.Visibility = Visibility.Visible;
                });
            };
 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            resSteamCmd.DownloadAddon(add_id.Text, add_id_game.Text);

            File.WriteAllText("DATA.txt", add_id_game.Text);

        }
    }
}
