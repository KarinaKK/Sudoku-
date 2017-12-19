using System;

namespace SudokuWPF
{
    public static class SudokuGenerator
    {
        private static Random rand = new Random();
        public static Random Rand => rand;
        static void Transposing(ref int[][] arr)
        {
            int[][] newArr = new int[9][];
            for (int i = 0; i < arr.Length; i++)
            {
                newArr[i] = new int[arr[i].Length];
                for (int j = 0; j < arr[i].Length; j++)
                {
                    newArr[i][j] = arr[j][i];
                }
            }
            arr = newArr;
        }

        public static int[][] GenerateSudoku()
        {

            int[][] result = GenerateBaseGrid();
            int tries = rand.Next(6, 26);

            for (int i = 0; i < tries; ++i)
            {
                switch (rand.Next(5))
                {
                    case 0:
                        Transposing(ref result);
                        break;
                    case 1:
                        SwapAreasVeritacally(ref result);
                        break;
                    case 2:
                        SwapRows(ref result);
                        break;
                    case 3:
                        SwapColumns(ref result);
                        break;
                    case 4:
                        SwapAreasHorizontally(ref result);
                        break;
                }
            }
            return result;
        }
        public static int[][] GenerateBaseGrid()
        {
            int[][] solution = new int[9][];
            for (int i = 0; i < 9; ++i)
            {
                solution[i] = new int[9];
                for (int j = 0; j < 9; ++j)
                {
                    solution[i][j] = (i * 3 + i / 3 + j) % 9 + 1;
                }
            }
            return solution;
        }
        static void SwapRows(ref int[][] arr)
        {
            int area = rand.Next(0, 3);
            int fRow = rand.Next(1, 4);
            int sRow = fRow;
            while (sRow == fRow)
            {
                sRow = rand.Next(1, 4);
            }
            int[] tempRow = arr[fRow + (area * 3) - 1];
            arr[fRow + (area * 3) - 1] = (int[])arr[sRow + (area * 3) - 1];
            arr[sRow + (area * 3) - 1] = tempRow;

        }
        static void SwapColumns(ref int[][] arr)
        {
            Transposing(ref arr);
            SwapRows(ref arr);
            Transposing(ref arr);
        }

        public static void SwapAreasVeritacally(ref int[][] arr)
        {
            int area1 = rand.Next(1, 4);
            int area2 = area1;
            while (area1 == area2)
            {
                area2 = rand.Next(1, 4);
            }
            for (int i = area1 * 3 - 3, j = 0; i < area1 * 3; ++i, ++j)
            {
                int[] temp = arr[i];

                arr[i] = arr[area2 * 3 - 3 + j];
                arr[area2 * 3 - 3 + j] = temp;
            }

        }
        static void SwapAreasHorizontally(ref int[][] arr)
        {
            Transposing(ref arr);
            SwapAreasVeritacally(ref arr);
            Transposing(ref arr);
        }

    }
}
