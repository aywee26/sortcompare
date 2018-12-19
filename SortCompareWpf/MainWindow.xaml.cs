using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SortCompareWpf
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private Random _random = new Random();
    private int[] _array;
    private const int _minArraySize = 2;
    private const int _maxArraySize = 100000;
    private string _desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

    public MainWindow()
    {
      InitializeComponent();
    }

    private int ParseNumberOfElements()
    {
      bool parsed = int.TryParse(NumElementsTextBox.Text, out int numElements);
      if(!parsed || numElements < _minArraySize || numElements > _maxArraySize)
      {
        return 0;
      }
      return numElements;
    }

    private bool CanCreateArray()
    {
      return ParseNumberOfElements() >= _minArraySize;
    }

    private void Log(string message)
    {
      LogTextBox.Text += (Environment.NewLine + message);
    }

    private void WriteToFile(string fileName, int[] array)
    {
      using(var writer = new StreamWriter(File.Create(fileName)))
      {
        int length = array.Length;
        for(int i = 0; i < length; i++)
        {
          writer.WriteLine($"array[{i}] = {array[i]}");
        }
      }
    }

    private void NumElementsTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      if(CreateArrayButton != null)
      {
        CreateArrayButton.IsEnabled = CanCreateArray();
      }
    }

    private void CreateArrayButton_Click(object sender, RoutedEventArgs e)
    {
      int numElements = ParseNumberOfElements();

      var stopwatch = new Stopwatch();
      stopwatch.Start();

      _array = new int[numElements];

      for(int i = 0; i < _array.Length; i++)
      {
        _array[i] = _random.Next(int.MinValue, int.MaxValue);
      }
      stopwatch.Stop();

      Log($"Массив из {numElements} элементов создан и заполнен случайными числами за {stopwatch.ElapsedMilliseconds} мс.");

      BubbleSortButton.IsEnabled = true;
      QsortButton.IsEnabled = true;

      string fileName = Path.Combine(_desktopFolder, "original-array.txt");
      WriteToFile(fileName, _array);
      Log($"Содержимое массива сохранено в файл {fileName}");
    }

    private void BubbleSortButton_Click(object sender, RoutedEventArgs e)
    {
      int length = _array.Length;
      int[] newArray = new int[length];
      Array.Copy(_array, newArray, length);

      var stopwatch = new Stopwatch();
      stopwatch.Start();

      BubbleSortAlgorithm.SortArray(newArray);
      stopwatch.Stop();
      Log($"Массив из {length} элементов отсортирован методом пузырьковой сортировки за {stopwatch.ElapsedMilliseconds} мс.");

      string fileName = Path.Combine(_desktopFolder, "bubble-sorted-array.txt");
      WriteToFile(fileName, newArray);
      Log($"Содержимое отсортированного массива сохранено в файл {fileName}");
    }

    private void QsortButton_Click(object sender, RoutedEventArgs e)
    {
      int length = _array.Length;
      int[] newArray = new int[length];
      Array.Copy(_array, newArray, length);

      var stopwatch = new Stopwatch();
      stopwatch.Start();

      QSortAlgorithm.SortArray(newArray);
      stopwatch.Stop();
      Log($"Массив из {length} элементов отсортирован методом QSort за {stopwatch.ElapsedMilliseconds} мс.");

      string fileName = Path.Combine(_desktopFolder, "qsorted-array.txt");
      WriteToFile(fileName, newArray);
      Log($"Содержимое отсортированного массива сохранено в файл {fileName}");
    }
  }
}
