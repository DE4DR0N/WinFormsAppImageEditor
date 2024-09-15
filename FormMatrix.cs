namespace WinFormsAppImageEditor
{
    public partial class FormMatrix : Form
    {
        private bool binFlag;
        private Bitmap image;
        private int[,,] matrix;
        private ImageProcessor processor;

        public FormMatrix(int[,,] matrix, Bitmap image, ImageProcessor processor)
        {
            InitializeComponent();

            this.matrix = matrix;
            this.image = image;
            this.processor = processor;
            binFlag = processor.binFlag;

            DisplayMatrix();
        }

        public event EventHandler<int[,,]> MatrixChanged;

        private void DisplayMatrix()
        {
            dataGridView.Rows.Clear();
            int width = matrix.GetLength(0);
            int height = matrix.GetLength(1);

            // Add columns to DataGridView
            for (int i = 0; i < width; i++)
            {
                dataGridView.Columns.Add($"Pixel {i + 1}", $"Pixel {i + 1}");
            }

            // Add rows and populate values
            for (int y = 0; y < height; y++)
            {
                dataGridView.Rows.Add();
                if (binFlag)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (matrix[x, y, 0] == 0) dataGridView.Rows[y].Cells[x].Value = 1;
                        else dataGridView.Rows[y].Cells[x].Value = 0;
                    }
                }
                else
                {
                    for (int x = 0; x < width; x++)
                    {
                        dataGridView.Rows[y].Cells[x].Value = $"{matrix[x, y, 0]}, {matrix[x, y, 1]}, {matrix[x, y, 2]}";
                    }
                }
            }
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Update the matrix with the new values
            int x = e.ColumnIndex;
            int y = e.RowIndex;
            if (binFlag)
            {
                int value = Convert.ToInt32(dataGridView.Rows[y].Cells[x].Value);

                if (value == 1)
                {
                    matrix[x, y, 0] = 0;
                    matrix[x, y, 1] = 0;
                    matrix[x, y, 2] = 0;
                }
                else
                {
                    matrix[x, y, 0] = 255;
                    matrix[x, y, 1] = 255;
                    matrix[x, y, 2] = 255;
                }
            }
            else
            {
                string[] values = dataGridView.Rows[y].Cells[x].Value.ToString().Split(',');
                if (values.Length == 3 &&
                    ((Convert.ToInt32(values[0]) >= 0 && Convert.ToInt32(values[0]) <= 255) ||
                    (Convert.ToInt32(values[1]) >= 0 && Convert.ToInt32(values[1]) <= 255) ||
                    (Convert.ToInt32(values[2]) >= 0 && Convert.ToInt32(values[2]) <= 255)))
                {
                    matrix[x, y, 0] = Convert.ToInt32(values[0]);
                    matrix[x, y, 1] = Convert.ToInt32(values[1]);
                    matrix[x, y, 2] = Convert.ToInt32(values[2]);
                }
                else
                {
                    MessageBox.Show("Invalid input format. Please use 'R,G,B' format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Convert the updated matrix back to image
            image = processor.MatrixToImage(matrix);
            MatrixChanged?.Invoke(this, matrix);
        }
    }
}