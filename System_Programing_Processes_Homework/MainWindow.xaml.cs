using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace System_Programing_Processes_Homework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Proces>? Proces { get; set; }
        public List<string>? BlackList { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Proces = new List<Proces>();
            foreach (var item in Process.GetProcesses())
            {
                Proces p = new Proces();
                p.Name = item.ProcessName;
                p.MachineName = item.MachineName;
                p.Id = item.Id;
                p.ThreadsCount = item.Threads.Count;
                p.HandlesCount = item.HandleCount;
                Proces.Add(p);
            }
            BlackList = new List<string>();
            DataContext = this;

        }

        private void StartingProces(object sender, RoutedEventArgs e)
        {

            if (textBox.Text.Length != 0)
            {
                int check = 0;
                try
                {
                    foreach (var item in BlackList)
                    {
                        if (item == textBox.Text)
                        {
                            MessageBox.Show("Siz bu Prosesi start ede bilmezsiniz cunki bu proces black liste elave olunub", "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
                            textBox.Text = null;
                            check++;
                            break;
                        }

                    }
                    if (check == 0)
                    {
                        Process.Start(textBox.Text);
                        textBox.Text = null;


                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    MessageBox.Show("Bele ada sahib Proces yoxdur", "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
                    textBox.Text = null;
                }

            }
            else
            {
                MessageBox.Show("Siz yaradilacaq Proces adin daxil etmemisiniz", "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void KillingProces(object sender, RoutedEventArgs e)
        {
            Proces? p = item.SelectedItem as Proces;

            if (p != null)
            {
                foreach (var item in Process.GetProcesses())
                {
                    if (item.ProcessName == p.Name)
                    {
                        item.Kill();
                        textBox.Text = null;

                    }

                }
            }

            else
            {
                MessageBox.Show("Dayandirilacaq procesi secmemimisiniz", "INFO", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void AddProcesBlackList(object sender, RoutedEventArgs e)
        {
            int check = 0;
            if (textBox.Text.Length != 0)
            {

                foreach (var item in BlackList)
                {
                    if (textBox.Text != item)
                    { 

                        check++;
                        break;
                    }

                }
                if (check == BlackList.Count())
                {
                    BlackList.Add(textBox.Text);
                    MessageBox.Show("Sizin daxil etdiyiniz Process Black Liste elave olundu", "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
                    textBox.Text = null;

                }
                else
                {
                    MessageBox.Show("Sizin daxil etdiyiniz Process Black Liste artiq var", "INFO", MessageBoxButton.OK, MessageBoxImage.Information);
                    textBox.Text = null;

                }

            }
            else
            {
                MessageBox.Show("Siz Black Liste Elave olunacaq Proces adin daxil etmemisiniz","INFO",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }
    }
}