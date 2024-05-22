using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("\n\nProgram Started:\n\n");
        (string inputFolderPath, string outputFolderPath, double boarderPtg) = GetUserInputs();

        Console.WriteLine("\n\nBUSY");
        foreach (string inputFile in Directory.GetFiles(inputFolderPath))
        {
            string fileName = Path.GetFileName(inputFile);
            string outputPath = Path.Combine(outputFolderPath, fileName);

            // Adds boarder
            var shrinkedFile = AddWhiteBoarder(inputFile, outputPath, boarderPtg);

            var labeledFilePath = Path.Combine(Path.GetDirectoryName(shrinkedFile), "Labelled", fileName);

            // Write Text on image.
            WriteTextOnImage(shrinkedFile, labeledFilePath, "Sample Text");
        }

        Console.WriteLine("Images processed successfully.");
        Console.ReadLine();
    }

    static (string inputFolder, string outputFolder, double boarderPtg)
        GetUserInputs()
    {
        // Read the input folder path from the console
        Console.Write("\tInput Folder:\t");
        string inputFolderPath = Console.ReadLine();

        // Read the output folder path from the console
        Console.Write("\tOutput Folder:\t");
        string outputFolderPath = Console.ReadLine();

        // Read boarder Percentage
        Console.Write("\tBoarder Percentage(%):\t");
        double boarderPtg = Convert.ToDouble(Console.ReadLine());

        return (inputFolderPath, outputFolderPath, boarderPtg);
    }

    static string AddWhiteBoarder(string inputFile, string outputPath, double boarderPercentage = 5)
    {
        using (Image image = Image.FromFile(inputFile))
        {
            int maxDimension = Math.Max(image.Width, image.Height);
            int borderSize = (int)((boarderPercentage / 100) * maxDimension); // 5% border default

            int canvasSize = maxDimension + 2 * borderSize;
            int imageX = (canvasSize - image.Width) / 2;
            int imageY = (canvasSize - image.Height) / 2;

            using (Bitmap canvas = new Bitmap(canvasSize, canvasSize))
            {
                using (Graphics g = Graphics.FromImage(canvas))
                {
                    // Set the canvas color to white
                    g.Clear(Color.White);

                    // Draw the original image centered on the canvas
                    g.DrawImage(image, new Rectangle(imageX, imageY, image.Width, image.Height));

                    // Save the new image to the output path
                    canvas.Save(outputPath, ImageFormat.Jpeg);
                }
            }
        }
        return outputPath;
    }

    static void WriteTextOnImage(string imagePath, string outputPath, string text)
    {
        using (Image image = Image.FromFile(imagePath))
        using (Graphics g = Graphics.FromImage(image))
        {
            // define your font and color
            Font font = new Font("Arial", 20);
            SolidBrush brush = new SolidBrush(Color.Black);

            // Define where teh text will be placed
            PointF point = new PointF(image.Width / 2, image.Height / 2);

            // Draw the text on the image.
            g.DrawString(text, font, brush, point);

            // Save the new image to the output path.
            image.Save(outputPath, ImageFormat.Jpeg);
        }

    }



}

