using System.Windows.Forms.DataVisualization.Charting;

namespace WinFormsAppImageEditor
{
    public partial class FormShading : Form
    {
        private Bitmap originalImage;
        private Bitmap shadedImage;
        private ImageProcessor processor;
        private Dictionary<int, int> countDict;
        private int[,,] matrix;

        public FormShading(Bitmap image, ImageProcessor processor)
        {
            InitializeComponent();
            originalImage = new Bitmap(image);
            shadedImage = new Bitmap(image.Width, image.Height);
            this.processor = processor;

            ApplyDistanceTransform();
        }

        private void ApplyDistanceTransform()
        {
            int[,] matrix = processor.BinImageToMatrix(originalImage);
            int[,] distanceMatrix = processor.ComputeDistanceTransform(matrix);
            countDict = processor.CountDuplicateValues(distanceMatrix);

            // Создание нового экземпляра серии данных
            var series = new Series();
            series.ChartType = SeriesChartType.Column;

            // Добавление данных в серию
            foreach (var pair in countDict)
            {
                series.Points.AddXY(pair.Key, pair.Value);
            }

            chart.Series.Add(series);
            chart.ChartAreas[0].AxisX.Title = "Value";
            chart.ChartAreas[0].AxisY.Title = "Count";

            // Ограничиваем значения по AxisX
            chart.ChartAreas[0].AxisX.Minimum = countDict.Keys.Min();
            chart.ChartAreas[0].AxisX.Maximum = countDict.Keys.Max();

            // Обработка полученной матрицы и вывод изображения
            shadedImage = processor.ShadedMatrixToImage(distanceMatrix);
            pictureBox.Image = shadedImage;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            matrix = processor.ShadedImageToMatrix(shadedImage);
            FormMatrix matrixForm = new FormMatrix(matrix, shadedImage, processor);
            matrixForm.MatrixChanged += MatrixForm_MatrixChanged;
            matrixForm.Show();
            Form120 form120 = new Form120(matrix, countDict, processor);
            form120.Show();
        }

        private void MatrixForm_MatrixChanged(object sender, int[,,] newMatrix)
        {
            shadedImage = processor.MatrixToImage(newMatrix);
            pictureBox.Image = shadedImage;
        }
    }
}