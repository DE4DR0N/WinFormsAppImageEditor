using System.Drawing.Imaging;

namespace WinFormsAppImageEditor
{
    public partial class FormMain : Form
    {
        private Bitmap currentImage;

        private ImageProcessor processor = new ImageProcessor();

        public FormMain()
        {
            InitializeComponent();
            if (pictureBox.Image is null)
            {
                bttnShading.Enabled = false;
            }
        }

        private void LoadImage(string filePath)
        {
            try
            {
                currentImage = new Bitmap(filePath);
                pictureBox.Image = currentImage;
                ObtaingImage();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveImage(string filePath)
        {
            try
            {
                currentImage.Save(filePath, ImageFormat.Bmp);
                MessageBox.Show("Image successfuly saved.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ObtaingImage()
        {
            int[,,] matrix = processor.ImageToMatrix(currentImage);
            bttnShading.Enabled = processor.binFlag;
            FormMatrix matrixForm = new FormMatrix(matrix, currentImage, processor);
            matrixForm.MatrixChanged += MatrixForm_MatrixChanged;
            matrixForm.Show();
        }

        private void MatrixForm_MatrixChanged(object sender, int[,,] newMatrix)
        {
            currentImage = processor.MatrixToImage(newMatrix);
            pictureBox.Image = currentImage;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            ObtaingImage();
        }

        private void bttnShading_Click(object sender, EventArgs e)
        {
            FormShading formShading = new FormShading(currentImage, processor);
            formShading.Show();
        }

        private void loadToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Bitmap Files (*.bmp)|*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                LoadImage(filePath);
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Bitmap Files (*.bmp)|*.bmp";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                SaveImage(filePath);
            }
        }
    }
}