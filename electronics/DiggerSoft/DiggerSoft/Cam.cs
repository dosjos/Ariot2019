using System;
using System.IO;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Camera;

namespace DiggerSoft
{
    public class Cam
    {

        public static byte[] TakePicture()
        {
            var pictureBytes = Pi.Camera.CaptureImage(new CameraStillSettings
            {
                CaptureWidth = 640,
                CaptureHeight = 480,
                CaptureJpegQuality = 90,
                VerticalFlip = true
            });
            return pictureBytes;
        }

        public static void StorePicture(string folder, byte[] pictureBytes)
    {
            var targetPath = $"{folder}/picture_{DateTime.Now:yyyyMMddHHmmssfff}.jpg";

            File.WriteAllBytes(targetPath, pictureBytes);
            Console.WriteLine($"Took picture -- Byte count: {pictureBytes.Length}");
        }
    }
}
