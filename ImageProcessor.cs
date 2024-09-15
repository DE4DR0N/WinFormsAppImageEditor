namespace WinFormsAppImageEditor
{
    public class ImageProcessor
    {
        public bool binFlag { get; private set; } = false; // флаг показывающий бинарное ли изображение
        private Dictionary<int, int> keyValuePairs = new Dictionary<int, int>(); // словарь, где ключ = значения целочисленые отрицательные, значения = после преобразования в [0..255]

        // Преобразование изображения в трёхмерный массив
        public int[,,] ImageToMatrix(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;
            int[,,] matrix = new int[width, height, 3]; // 3 channels for RGB

            // Iterate through each pixel
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);

                    // Store RGB values in the matrix as ushort
                    matrix[x, y, 0] = pixelColor.R; // Red channel
                    matrix[x, y, 1] = pixelColor.G; // Green channel
                    matrix[x, y, 2] = pixelColor.B; // Blue channel
                }
            }

            // Проверка на бинарность изображения
            bool shouldBreak = false;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);

                    if ((pixelColor.R == 0 && pixelColor.G == 0 && pixelColor.B == 0) ||
                        (pixelColor.R == 255 && pixelColor.G == 255 && pixelColor.B == 255))
                    {
                        binFlag = true;
                    }
                    else
                    {
                        binFlag = false;
                        shouldBreak = true;
                        break;
                    }
                }
                if (shouldBreak) break;
            }

            return matrix;
        }

        // Преобразование бинарного изображения в двумерный массив
        public int[,] BinImageToMatrix(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;
            int[,] matrix = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    if (color.R == 0)
                    {
                        matrix[x, y] = 1;
                    }
                    else
                    {
                        matrix[x, y] = 0;
                    }
                }
            }

            return matrix;
        }

        // Преобразование растушированного изображения в матрицу
        public int[,,] ShadedImageToMatrix(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;
            int[,,] shadedMatrix = new int[width, height, 3];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);

                    // Store RGB values in the matrix as ushort
                    shadedMatrix[x, y, 0] = pixelColor.R; // Red channel
                    shadedMatrix[x, y, 1] = pixelColor.G; // Green channel
                    shadedMatrix[x, y, 2] = pixelColor.B; // Blue channel
                }
            }
            binFlag = false;
            return shadedMatrix;
        }

        // Преобразование трёхмерного массива в изображение
        public Bitmap MatrixToImage(int[,,] matrix)
        {
            int width = matrix.GetLength(0);
            int height = matrix.GetLength(1);
            Bitmap result = new Bitmap(width, height);

            // Set pixel values based on matrix values
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Color color = Color.FromArgb(
                        (int)matrix[x, y, 0],
                        (int)matrix[x, y, 1],
                        (int)matrix[x, y, 2]);
                    result.SetPixel(x, y, color);
                }
            }

            return result;
        }

        // Преобразование двумерного растушированного массива в изображение
        public Bitmap ShadedMatrixToImage(int[,] matrix)
        {
            int width = matrix.GetLength(0);
            int height = matrix.GetLength(1);
            Bitmap bitmap = new Bitmap(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int value = matrix[x, y];
                    Color color = Color.FromArgb(value, value, value);
                    bitmap.SetPixel(x, y, color);
                }
            }
            return bitmap;
        }

        // Метод для расчета расстояний
        public int[,] ComputeDistanceTransform(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            // Создаем матрицу для хранения расстояний
            int[,] distanceMatrix = new int[rows, cols];

            // Проходим по каждой ячейке матрицы
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // Если текущая ячейка - объект
                    if (matrix[i, j] == 1)
                    {
                        // Ищем расстояние до ближайшей ячейки фона
                        distanceMatrix[i, j] = FindNearestBackground(matrix, i, j);
                    }
                    // Если текущая ячейка - фон
                    else
                    {
                        // Ищем расстояние до ближайшей ячейки объекта
                        distanceMatrix[i, j] = -FindNearestObject(matrix, i, j);
                    }
                }
            }

            return distanceMatrix;
        }

        // Метод для расчета расстояний (до фона)
        private int FindNearestBackground(int[,] matrix, int x, int y)
        {
            int distance = 1;
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            // Ищем ближайший пиксель фона
            while (true)
            {
                // Проверяем вокруг текущей ячейки наличие пикселей фона
                for (int i = Math.Max(0, x - distance); i <= Math.Min(rows - 1, x + distance); i++)
                {
                    for (int j = Math.Max(0, y - distance); j <= Math.Min(cols - 1, y + distance); j++)
                    {
                        // Если находим пиксель фона, возвращаем расстояние
                        if (matrix[i, j] == 0)
                            return distance;
                    }
                }

                distance++; // Увеличиваем радиус поиска
            }
        }

        // Метод для расчета расстояний (до объекта)
        private int FindNearestObject(int[,] matrix, int x, int y)
        {
            int distance = 1;
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            // Ищем ближайший пиксель объекта
            while (true)
            {
                // Проверяем вокруг текущей ячейки наличие пикселей объекта
                for (int i = Math.Max(0, x - distance); i <= Math.Min(rows - 1, x + distance); i++)
                {
                    for (int j = Math.Max(0, y - distance); j <= Math.Min(cols - 1, y + distance); j++)
                    {
                        // Если находим пиксель объекта, возвращаем расстояние
                        if (matrix[i, j] == 1)
                            return distance;
                    }
                }

                distance++; // Увеличиваем радиус поиска
            }
        }

        // Метод для заполнения словаря
        public Dictionary<int, int> CountDuplicateValues(int[,] matrix)
        {
            Dictionary<int, int> countDict = new Dictionary<int, int>();

            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    int value = matrix[y, x];
                    if (countDict.ContainsKey(value))
                    {
                        countDict[value]++;
                    }
                    else
                    {
                        countDict[value] = 1;
                    }
                }
            }
            // Определение шага для гистограммы
            int step = 255 / (countDict.Count - 1);

            // Создание нового словаря для значений от 0 до 255 с шагом step
            Dictionary<int, int> histogramData = new Dictionary<int, int>();

            // Заполнение словаря histogramData данными из countDict
            int key = 255;
            foreach (var pair in countDict)
            {
                histogramData[key] = pair.Value;
                keyValuePairs[pair.Key] = key;
                key -= step;
            }

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    foreach (var pair in keyValuePairs)
                    {
                        if (pair.Key == matrix[x, y])
                        {
                            matrix[x, y] = pair.Value;
                        }
                    }
                }
            }

            return histogramData;
        }

        // Метод бинаризации 120
        public int[,] BinarizeMatrix120Method(int[,,] matrix, Dictionary<int, int> histogramData)
        {
            int width = matrix.GetLength(0);
            int height = matrix.GetLength(1);
            int[,] binaryMatrix = new int[width, height];

            // Нахождение максимального уровня серого в диапазоне t=[0, 120]
            int threshold = 0;
            int max = 0;
            for (int i = 0; i <= 120; i++)
            {
                if (histogramData.ContainsKey(i) && histogramData[i] > max)
                {
                    max = histogramData[i];
                    threshold = i;
                }
            }

            // Бинаризация изображения
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int grayLevel = (int)(matrix[x, y, 0] + matrix[x, y, 1] + matrix[x, y, 2]);
                    if (grayLevel <= threshold +15)
                    {
                        binaryMatrix[x, y] = 0;
                    }
                    else
                    {

                        binaryMatrix[x, y] = 255;
                    }
                }
            }

            return binaryMatrix;
        }

        //public int[,,] PolutonImage(int[,,] matrix)
        //{
        //    int width = matrix.GetLength(0);
        //    int height = matrix.GetLength(1);
        //    int[,,] polutoneMatrix = new int[width, height];
        //}

    }
}