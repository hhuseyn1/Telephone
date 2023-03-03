using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Source;

public partial class MainWindow : Window
{
    public List<string> ListWords { get; set; }
    public ObservableCollection<string> CloseWords { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        CloseWords = new();
        ListWords = new()
        {
            "Step IT Academy",
            "Computer Academy",
            ".Net",
            "C# the best",
            "My name is Huseyn"
        };
        Textline.Text = "Step";
        CheckWord();
    }

    private void CBtn_Click(object sender, RoutedEventArgs e)
    {
        Textline.Text = string.Empty;
    }

    private void AddBtn_Click(object sender, RoutedEventArgs e)
    {
        if (!ListWords.Contains(Textline.Text))
            ListWords.Add(Textline.Text);
        else
            MessageBox.Show($"{Textline.Text} is also exist in your List","Information",MessageBoxButton.OK,MessageBoxImage.Information);
    }

    private void CheckWord()
    {
        Task.Run(() =>
        {
            Dispatcher.Invoke(() =>
            {
                CloseWords.Clear();
                if (string.IsNullOrEmpty(Textline.Text) && string.IsNullOrWhiteSpace(Textline.Text)) return;
                foreach (var item in ListWords)
                {
                    if (item.ToLower().StartsWith(Textline.Text.ToLower()) && Textline.Text != item)
                        CloseWords.Add(item);
                }

            });
        });
    }

    private void UDRLBtn_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn)
        {
            if (btn.Name == "UpBtn" && ScreenLbox.SelectedIndex > 0)
                ScreenLbox.SelectedIndex--;

            else if (btn.Name == "DownBtn" && ScreenLbox.SelectedIndex < ScreenLbox.Items.Count-1)
                ScreenLbox.SelectedIndex++;

            else if(btn.Name == "RightBtn")
                if (CloseWords.Count > 0)
                    Textline.Text = ScreenLbox.SelectedItem.ToString();

            CheckWord();
        }
    }

    private void Textline_TextChanged(object sender, TextChangedEventArgs e)
    {
        CheckWord();
    }
}
