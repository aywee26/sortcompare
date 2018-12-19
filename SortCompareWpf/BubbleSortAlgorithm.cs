namespace SortCompareWpf
{
  public static class BubbleSortAlgorithm
  {
    public static void SortArray(int[] array)
    {
      int length = array.Length;
      int temp = array[0];

      for(int i = 0; i < length; i++)
      {
        for(int j = i + 1; j < length; j++)
        {
          if(array[i] > array[j])
          {
            temp = array[i];
            array[i] = array[j];
            array[j] = temp;
          }
        }
      }
    }
  }
}
