using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("\n\nProgram Started:\n\n");

        // Read the input folder path from the console
        Console.Write("\tInput Folder:\t");
        string inputFolderPath = Console.ReadLine();

        // Read the output folder path from the console
        Console.Write("\tOutput Folder:\t");
        string outputFolderPath = Console.ReadLine();

        Console.WriteLine("\n\nBUSY");
        foreach (string inputFile in Directory.GetFiles(inputFolderPath))
        {
            string fileName = Path.GetFileName(inputFile);
            string outputPath = Path.Combine(outputFolderPath, fileName);

            // Add WhiteBoarder Function that takes inputFile as argument and returns newImage
            // The canvas of the new image must be in aspect ratio 1:1 and the image must be centered into the canvas
            // The Image should be 90% of its original size and zoomed out in the canvas such that the canvas looks like a white boarder around the image.
            WhiteBoarder(inputFile, outputPath);
        }

        Console.WriteLine("Images processed successfully.");
        Console.ReadLine();
    }

    static void WhiteBoarder(string inputFile, string outputPath)
    {
        using (Image image = Image.FromFile(inputFile))
        {
            int maxDimension = Math.Max(image.Width, image.Height);
            int borderSize = (int)(0.05 * maxDimension); // 5% border

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
    }


    static void WhiteBoarder2(string inputFile, string outputPath)
    {
        using (Image image = Image.FromFile(inputFile))
        {
            int maxDimension = Math.Max(image.Width, image.Height);
            int minDimension = (int)(0.9 * Math.Min(image.Width, image.Height));
            int borderSize = (maxDimension - minDimension) / 2;

            using (Bitmap canvas = new Bitmap(maxDimension, maxDimension))
            {
                using (Graphics g = Graphics.FromImage(canvas))
                {
                    // Set the canvas color to white
                    g.Clear(Color.White);

                    // Draw the original image centered on the canvas
                    g.DrawImage(image, new Rectangle(borderSize, borderSize, minDimension, minDimension));

                    // Save the new image to the output path
                    canvas.Save(outputPath, ImageFormat.Jpeg);
                }
            }
        }
    }

    static void WhiteBoarder3(string inputFile, string outputPath)
    {
        using (Image image = Image.FromFile(inputFile))
        {
            int maxDimension = Math.Max(image.Width, image.Height);
            float ratio = (float)image.Width / image.Height;

            int newWidth, newHeight;
            if (image.Width > image.Height)
            {
                newWidth = (int)(0.9 * maxDimension);
                newHeight = (int)(newWidth / ratio);
            }
            else
            {
                newHeight = (int)(0.9 * maxDimension);
                newWidth = (int)(newHeight * ratio);
            }

            int borderX = (maxDimension - newWidth) / 2;
            int borderY = (maxDimension - newHeight) / 2;

            using (Bitmap canvas = new Bitmap(maxDimension, maxDimension))
            {
                using (Graphics g = Graphics.FromImage(canvas))
                {
                    // Set the canvas color to white
                    g.Clear(Color.White);

                    // Draw the original image centered on the canvas
                    g.DrawImage(image, new Rectangle(borderX, borderY, newWidth, newHeight));

                    // Save the new image to the output path
                    canvas.Save(outputPath, ImageFormat.Jpeg);
                }
            }
        }
    }

}



//using (var image = Image.FromFile(inputFile))
//{
//    int borderSize = (int)(Math.Max(image.Width, image.Height) * 0.1); // 10% border
//    int newWidth = image.Width + 2 * borderSize;
//    int newHeight = image.Height + 2 * borderSize;

//    var newImage = new Bitmap(newWidth, newHeight);
//    using (var g = Graphics.FromImage(newImage))
//    {
//        g.Clear(Color.White); // Draw the white border
//        int x = (newWidth - image.Width) / 2;
//        int y = (newHeight - image.Height) / 2;
//        g.DrawImage(image, new Point(x, y)); // Draw the original image on top of the border
//    }

//    newImage.Save(outputPath, System.Drawing.Imaging.ImageFormat.Jpeg);
//}
