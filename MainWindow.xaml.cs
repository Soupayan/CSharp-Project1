using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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

namespace project2
{
  using System.CodeDom;
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>

  using System.Windows.Threading;
  public partial class MainWindow : Window
  {
    DispatcherTimer timer = new DispatcherTimer();
    int tenthsOfSecondsElapsed;
    int matchesFound;
    int numberOfClicks;
    TextBlock lastTextBlockClicked;
    List<TextBlock> textBlocks = new List<TextBlock>();
    Boolean byPass = false;

    bool findingMatch = false;

    public MainWindow()
    {
      InitializeComponent();
      timer.Interval = TimeSpan.FromSeconds(0.1);
      timer.Tick += Timer_Tick;
      SetUpGame();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      tenthsOfSecondsElapsed++;
      timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
      if (matchesFound == 8)
      {
        timer.Stop();
        timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
      }

    }

    private void SetUpGame()
    {
      List<string> animalEmoji = new List<string>
      {
        "🐕‍🦺", "🐕‍🦺",
        "🦍", "🦍",
        "🐒", "🐒",
        "🐅", "🐅",
        "🐄", "🐄",
        "🐪", "🐪",
        "🐔", "🐔",
        "🦏", "🦏",
      };

      Random random = new Random();
      // foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
      // {
      //   if (textBlock.Name != "timeTextBlock")
      //   {
      //     int index = random.Next(animalEmoji.Count);
      //     string nextEmoji = animalEmoji[index];
      //     textBlock.Text = nextEmoji;
      //     animalEmoji.RemoveAt(index);
      //     textBlock.Visibility = Visibility.Visible;
      //   }
      // }
      foreach (Border borders in mainGrid.Children.OfType<Border>())
      {
        TextBlock textBlock = borders.Child as TextBlock;
        if (textBlock.Name != "timeTextBlock")
        {
          int index = random.Next(animalEmoji.Count);
          string nextEmoji = animalEmoji[index];
          textBlock.Text = nextEmoji;
          animalEmoji.RemoveAt(index);
          textBlock.Visibility = Visibility.Hidden;
        }
      }
      timer.Start();
      tenthsOfSecondsElapsed = 0;
      matchesFound = 0;
      numberOfClicks = 0;
    }

    // private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
    // {
    //   TextBlock textBlock = sender as TextBlock;
    //   if (findingMatch == false)
    //   {
    //     textBlock.Visibility = Visibility.Visible;
    //     lastTextBlockClicked = textBlock;
    //     findingMatch = true;
    //   }
    //   else if (textBlock.Text == lastTextBlockClicked.Text)
    //   {
    //     matchesFound++;
    //     textBlock.Visibility = Visibility.Visible;
    //     findingMatch = false;
    //   }
    //   else
    //   {
    //     lastTextBlockClicked.Visibility = Visibility.Hidden;
    //     findingMatch = false;
    //   }

    // }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
      var textBlock = (sender as Border).Child as TextBlock;
      // TextBlock textBlockClicked;
      if (textBlock.Visibility == Visibility.Visible)
      {
        byPass = true;
      }
      else
      {
        byPass = false;
      }

      if (!byPass)
      {
        numberOfClicks++;
        if (numberOfClicks >= 3)
        {
          foreach (TextBlock textBlockClicked in textBlocks)
          {
            textBlockClicked.Visibility = Visibility.Hidden;
          }
          textBlocks.Clear();
          numberOfClicks = 1;
        }

        // textBlock.Visibility = Visibility.Visible;

        if (findingMatch == false)
        {
          textBlock.Visibility = Visibility.Visible;
          lastTextBlockClicked = textBlock;
          findingMatch = true;
          textBlocks.Add(textBlock);
        }
        else if (textBlock.Text == lastTextBlockClicked.Text)
        {
          matchesFound++;
          textBlock.Visibility = Visibility.Visible;
          findingMatch = false;
          textBlocks.Clear();
        }
        else if (numberOfClicks < 3)
        {
          textBlock.Visibility = Visibility.Visible;
          findingMatch = false;
          textBlocks.Add(textBlock);
        }
      }
    }


    private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
    {
      if (matchesFound == 8)
      {
        SetUpGame();
      }
    }
  }
}
