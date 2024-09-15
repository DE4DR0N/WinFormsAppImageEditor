using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsAppImageEditor
{
    public partial class Form120 : Form
    {
        private int[,,] matrix;
        private int[,] matrixBin;
        private Dictionary<int, int> countDict;
        private ImageProcessor processor;
        public Form120(int[,,] matrix, Dictionary<int, int> countDict, ImageProcessor processor)
        {
            InitializeComponent();
            this.matrix = matrix;
            this.countDict = countDict;
            this.processor = processor;

            matrixBin = processor.BinarizeMatrix120Method(matrix, countDict);
            DisplayMatrix();
        }

        private void DisplayMatrix()
        {
            dataGridView.Rows.Clear();
            int width = matrixBin.GetLength(0);
            int height = matrixBin.GetLength(1);

            // Add columns to DataGridView
            for (int i = 0; i < width; i++)
            {
                dataGridView.Columns.Add($"Pixel {i + 1}", $"Pixel {i + 1}");
            }

            // Add rows and populate values
            for (int y = 0; y < height; y++)
            {
                dataGridView.Rows.Add();
                for (int x = 0; x < width; x++)
                {
                    if (matrixBin[x, y] == 0) dataGridView.Rows[y].Cells[x].Value = 1;
                    else dataGridView.Rows[y].Cells[x].Value = 0;
                }
            }
        }

    }
}
