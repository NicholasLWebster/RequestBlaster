using System.Windows;
using RequestBlaster.Models;

namespace RequestBlaster
{
    /// <summary>
    /// Interaction logic for RequestConfigurationWindow.xaml
    /// </summary>
    public partial class RequestConfigurationWindow : Window
    {
        public RequestConfiguration Configuration = new RequestConfiguration();
        
        public RequestConfigurationWindow()
        {
            InitializeComponent();
            this.DataContext = Configuration;
        }

        private async void SendRequestsButton_Click(object sender, RoutedEventArgs e)
        {
            var requestSender = new RequestSenderClient();
            await requestSender.SendRequestsAsync(Configuration);
        }
    }
}
