namespace SortCompareWpf
{
  public static class QSortAlgorithm
  {
    public static void SortArray(int[] array)
    {
      Sort(array, 0, array.Length - 1);
    }

    private static void Sort(int[] array, int lower, int upper)
    {
      if(lower >= upper)
      {
        return;
      }

      int p = Partition(array, lower, upper);
      Sort(array, lower, p - 1);
      Sort(array, p + 1, upper);
    }

    private static int Partition(int[] array, int lower, int upper)
    {
      int s = upper;
      int p = lower;

      while(s != p)
      {
        if(array[p] <= array[s])
        {
          p++;
        }
        else
        {
          Swap(array, p, s);
          Swap(array, p, s - 1);
          s--;
        }
      }
      return p;
    }

    private static void Swap(int[] array, int a, int b)
    {
      int tmp = array[a];
      array[a] = array[b];
      array[b] = tmp;
    }
  }
}
