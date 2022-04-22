using System;
using System.Linq;

namespace LW1
{
    class Matrix
    {
        int[,] values;

        public int[,] Values
        {
            get { return values; }
            set
            {
                if (value.GetLength(0) == 0 || value.GetLength(1) == 0)
                {
                    throw new Exception("Matrix is empty");
                }
                else if(value.GetLength(0) != value.GetLength(1))
                {
                    throw new Exception("Matrix is not square");
                }
                else
                {
                    values = value;
                }
            }
        }

        public Matrix(int[,] matrix)
        {
            Values = matrix;
        }

        public bool IsCheckedMatrix()
        {
            if(Values == null)
            {
                throw new Exception("Matrix does not exist");
            }

            int sum = 0;
            int amountOfElements = 0;

            foreach (var num in Values)
            {
                sum += num;
                amountOfElements++;
            }

            var avgValue = sum / amountOfElements;

            int centerRowsIndex = Values.GetLength(0) / 2;
            int cenetrColumnIndex = Values.GetLength(1) / 2;

            if (Values[centerRowsIndex, cenetrColumnIndex] < avgValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SortMatrixByAscending()
        {
            var matrix = Values.Cast<int>().OrderBy(a => a).ToArray();

            for (int rowIndex = 0; rowIndex < Values.GetLength(0); rowIndex++)
            {
                for (int columns = 0; columns < Values.GetLength(1); columns++)
                {
                    var index = rowIndex * Values.GetLength(1) + columns;

                    Values[rowIndex, columns] = matrix[index];
                }
            }
        }

        public void SortMatrixByDescending()
        {
            var matrix = Values.Cast<int>().OrderByDescending(a => a).ToArray();

            for (int rowIndex = 0; rowIndex < Values.GetLength(0); rowIndex++)
            {
                for (int columns = 0; columns < Values.GetLength(1); columns++)
                {
                    var index = rowIndex * Values.GetLength(1) + columns;

                    Values[rowIndex, columns] = matrix[index];
                }
            }
        }
    }
}

