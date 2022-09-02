using System.Diagnostics;

using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Shape_AI
{
    internal class DataPrep
    {
        public Bitmap Initializer(int Resolution = 100, int Gamma = 1)
        //Takes the first / only file in the UserInput directory and stores this in a bitmap
        {
            //1: Creates the filepath to the correct folder
            string UserInputFilepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Shape_AI\Shape_AI\Shape_AI\Data\UserInput\";
            
            //2: Creates an 1 item array,
            String[] SearchFile = new string[0];

            //3: Searches for the file, stores the file directory in a 1-item array
            SearchFile = Directory.GetFiles(UserInputFilepath);

            //4: Creates the Bitmap using the only item in the array (the complete file directory)
            Bitmap UserImage = new Bitmap(SearchFile[0]);

            this.Compressor(UserImage, Resolution, Gamma);  //calling the next method
            return UserImage;
        }

        public Bitmap Compressor(Bitmap UserImage, int Resolution, int Gamma)
        //Compresses the image into a smaller size making data preperation more feasible
        {

            //Initial setup for lowering resolution:
            Rectangle DestRect = new Rectangle(0, 0, Resolution, Resolution);
            Bitmap BMPoutput = new Bitmap(Resolution, Resolution);
            BMPoutput.SetResolution(UserImage.Width, UserImage.Height);

            //Several methods to keep the details as high as possible
            using (var gr = Graphics.FromImage(BMPoutput))
            {          
                gr.CompositingMode = CompositingMode.SourceCopy;
                gr.CompositingQuality = CompositingQuality.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;

                //Changing the resolution
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    gr.DrawImage(UserImage, DestRect, 0, 0, UserImage.Width, UserImage.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            this.ContourMaker(BMPoutput, Gamma); //calling next method
            return BMPoutput;
        }

        public Bitmap ContourMaker(Bitmap UserImage, int Gamma)
        //Changes a compressed colored image into true black and white, thus higlighting the contours
        {
            //Takes the prefered Gamme and applies the passed down mutiplier:
            int GammeMultiplier = -5000000 * Gamma;

            //Stores the bitmap into an array, pixel by pixel
            for (int i = 0; i < UserImage.Width; i++)           //i = x coordinate
            {

                for (int j = 0; j < UserImage.Height; j++)      //j = y coordinate
                {
                    //temporarely stores the color value into an int:
                    int ColorValue = UserImage.GetPixel(i, j).ToArgb();


                    //checks if color is dark enough
                    if (ColorValue < GammeMultiplier)
                    {
                        UserImage.SetPixel(i, j, Color.Black);
                    }

                    //checks if color is too light
                    else
                    {
                        UserImage.SetPixel(i, j, Color.White);
                    }

                }
            }

            this.DataReader(UserImage); //calling next method
            return UserImage;
        }

        public void DataReader(Bitmap UserImage)
        //Stores the contoured bitmap into a 2 dimensional array
        {
            //Creates a 2D array to be used later:
            int[,] UserDataArray = new int[UserImage.Width, UserImage.Height];

            //Stores the bitmap into an array, pixel by pixel
            for (int i = 0; i < UserImage.Width; i++)           //i = x coordinate
            {
                for (int j = 0; j < UserImage.Height; j++)      //j = y coordinate
                {
                    //checks if the color is black
                    if (UserImage.GetPixel(i, j).ToString() == "Color [A=255, R=0, G=0, B=0]")
                    {
                        UserDataArray[i, j] = 1;
                    }

                    //checks if the color is white
                    else if (UserImage.GetPixel(i, j).ToString() == "Color [A=255, R=255, G=255, B=255]")
                    {
                        UserDataArray[i, j] = 0;
                    }

                }
            }

            this.DateWriter(UserDataArray); //calling next method
        }

        public void DateWriter(int[,] UserDataArray)
        //Writes the 2D array to a textfile for retrieval later on
        {
            //Creates the streamwriter object and file directory
            string UserDataArrayEncodedFilepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Shape_AI\Shape_AI\Shape_AI\Data\EncodedData\EncodedData.txt";
            StreamWriter UserDataArrayEncoded = new StreamWriter(UserDataArrayEncodedFilepath);

            //Loops 2 dimensional through the array ands stores this in a text file:
            for (int y = 0; y < UserDataArray.GetLength(1); y++)
            {
                for (int x = 0; x < UserDataArray.GetLength(0); x++)
                {
                    UserDataArrayEncoded.Write(UserDataArray[x, y]);
                }
                UserDataArrayEncoded.Write('\n');
            }

            UserDataArrayEncoded.Close();

        }
            
    }
}
