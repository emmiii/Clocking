using DTO;
using System;
using System.Windows;
using System.Windows.Media;
using TimeKeeper;
using static DTO.Enum;

namespace Clock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetTimeBalance();
        }

        private void SetTimeBalance()
        {
            var (hours, minutes) = TimeCalculator.GetTimeBalance();
            TimeBalance.Content = $"{hours}:{minutes}";
            TimeBalance.Foreground = hours > 0 || minutes > 0 ? Brushes.LimeGreen : Brushes.OrangeRed;
        }

        private void Clocking(Clocking clocking)
        {
            var timeStamp = new TimeStamp()
            {
                Clocking = clocking,
                Time = DateTime.Now,
                WorkingHours = int.Parse(WorkDay.Text)
            };
            TimePersister.WriteTimeToFile(timeStamp);
            SetTimeBalance();
        }

        private void In_Button_Click(object sender, RoutedEventArgs e) => Clocking(DTO.Enum.Clocking.In);
        private void Out_Button_Click(object sender, RoutedEventArgs e) => Clocking(DTO.Enum.Clocking.Out);
    }
}
