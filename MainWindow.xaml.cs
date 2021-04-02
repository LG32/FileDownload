using FileDownloader.Model;
using FileDownloader.Tools;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FileDownloader
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public DownloadInfo DownloadInfo { get; set; }
        private readonly DisplayModel _dispalyModel = null;
        private System.Timers.Timer _updateProgressTimer;
        private System.Timers.Timer _updateSpeedTimer;

        public MainWindow()
        {
            InitializeComponent();
            _dispalyModel = new DisplayModel();
            this.DataContext = _dispalyModel;
            // this.BackgroundImage = Image.FromFile(@"图片路径");

            ImageBrush b = new ImageBrush();
            var img = new Uri("pack://application:,,,/Image/banner.png");
            var imageSource = new BitmapImage(img);
            b.ImageSource = imageSource;
            b.Stretch = Stretch.Fill;
            this.Background = b;
            // this.UrlTextBox.Text = @"http://ucan.25pp.com/Wandoujia_web_seo_baidu_homepage.apk";
        }
        
        /// <summary>
        /// 初始化定时器等
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _updateProgressTimer = new System.Timers.Timer()
            {
                Interval = 20
            };
            _updateSpeedTimer = new System.Timers.Timer()
            {
                Interval = 1000
            };
            _updateProgressTimer.Elapsed += UpdateProgressTimer_Elapsed;
            _updateProgressTimer.Start();

            _updateSpeedTimer.Elapsed += UpdateSpeedTimer_Elapsed;
            _updateSpeedTimer.Start();
        }

        /// <summary>
        /// 更新速度信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateSpeedTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Helper.downloadInfo == null) return;
            this._dispalyModel.UpdateSpeed();
        }
        /// <summary>
        /// 更新进度信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateProgressTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Helper.downloadInfo == null) return;
            this._dispalyModel.UpdateProgress();
        }

        private async void DownloadBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var url = this.UrlTextBox.Text.Trim();
                if (!Helper.IsURL(url))
                {
                    System.Windows.MessageBox.Show("输入的URL不合法，请重新输入！");
                    this.UrlTextBox.Text = string.Empty;
                    this.UrlTextBox.Focus();
                    return;
                }

                if (Helper.downloadInfo != null && !Helper.downloadInfo.IsComplate)
                {
                    return;
                }
                var path = SelectFolder();
                if (string.IsNullOrEmpty(path))
                    return;

                Progress.Visibility = Visibility.Visible;
                await Helper.DownloadAsync(url, path);

                System.Windows.MessageBox.Show("下载完成！");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                // throw;
            }
            
        }

        /// <summary>
        /// 文件保存路径选择
        /// </summary>
        /// <returns></returns>
        private string SelectFolder()
        {
            var mDialog = new FolderBrowserDialog();

            var result = mDialog.ShowDialog();
            return result == System.Windows.Forms.DialogResult.Cancel ? null : mDialog.SelectedPath.Trim();
        }


        private void DrawWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public void CloseWindow(object sender, RoutedEventArgs args)
        {
            this.Close();
        }
    }
}
