using System;

namespace LW1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] array = { { 90, 8, 15}, { 19, 18, 11}, {11, 10, 22 } };
            
            Matrix matrix = new Matrix(array);

            if (matrix.IsCheckedMatrix())
            {
                matrix.SortMatrixByAscending();
            }
            else
            {
                matrix.SortMatrixByDescending();
            }
        }
    }
}
