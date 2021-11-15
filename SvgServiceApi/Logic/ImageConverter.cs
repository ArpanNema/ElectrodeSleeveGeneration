using Aspose.Imaging;
using Aspose.Svg;
using Aspose.Svg.Rendering.Image;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SvgServiceApi.Logic
{
    public class ImageConverter
    {
        public static void convert() {
            string dataDir = @"<SVG output folder>";
            string outputDir = @"<Path to save png converted file>";

            using (var document = new SVGDocument(Path.Combine(dataDir, "output1.svg")))
            {
                using (var device = new ImageDevice(new ImageRenderingOptions(ImageFormat.Png), outputDir + @"\output.png"))
                {
                    document.RenderTo(device);
                }
            }

            // Load an existing image into an instance of RasterImage class
            using (RasterImage rasterImage = (RasterImage)Image.Load(outputDir + @"\output_1.png"))
            {
                // Before cropping, the image should be cached for better performance
                if (!rasterImage.IsCached)
                {
                    rasterImage.CacheData();
                }

                // Define shift values for all four sides
                int leftShift = 0;
                int rightShift = 1800;
                int topShift = 50;
                int bottomShift = 2500;

                // Based on the shift values, apply the cropping on image Crop method will shift the image bounds toward the center of image and Save the results to disk
                rasterImage.Crop(leftShift, rightShift, topShift, bottomShift);
                rasterImage.Save(outputDir + @"\CroppingByShifts_out.png");
            }
        }
    }
}
