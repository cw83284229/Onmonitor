using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimilarImages
{
   public  class ImageHash
    {
        private static readonly string[] imageExtensions = new string[] { ".png", ".jpg", ".jpeg" };
        private static int currentPrecision = 20;
        private static InterpolationMode currentInterpolationMode;


        /// <summary>
        /// 获取图片对比数据
        /// </summary>
        /// <param name="folderPath"></param>
        /// <param name="validImageCount"></param>
        /// <param name="precision"></param>
        /// <param name="interpolationMode"></param>
        /// <param name="hashEnum"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public static  double GetSimilarity(Bitmap bitmap, Bitmap bitmap2, HashEnum hashEnum)
        {


            // Get hashes
            var imageHashPairs = GetImageHashes(bitmap, hashEnum);
            var imageHashPairs2 = GetImageHashes(bitmap2, hashEnum);
           
            //获取明汉差异并返回
            double HammingDistance = GetHammingDistancePercent(imageHashPairs, imageHashPairs2);
            return HammingDistance;
        }

        public enum HashEnum
        {
            Difference,
            Mean,
            Perceptual
        }
        /// <summary>
        /// 得到图像的散列
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="hashEnum"></param>
        /// <returns></returns>
        private static  string GetImageHashes(Bitmap bitmap, HashEnum hashEnum)
        {
           
            // 获取hash类型
            Func<Bitmap, string> hashMethod = null;
            switch (hashEnum)
            {
                case HashEnum.Mean:
                    hashMethod = (imageName) => 
                        GetMeanHash(bitmap);
                    break;
                case HashEnum.Difference:
                    hashMethod = (imageName) => 
                        GetDifferenceHash(bitmap);
                    break;
                case HashEnum.Perceptual:
                    hashMethod = (imageName) => 
                        GetPerceptualHash(bitmap);
                    break;
                default: break;
            }

            // 获取hash值
          
           string hash = hashMethod(bitmap);
       
            return hash;
        }
        /// <summary>
        /// 获得平均hash
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static string GetMeanHash(Bitmap bitmap)
        {
            // Get gray image matrix
            double[,] imageMatrix = GetResizedGrayImageMatrix(bitmap, 
                currentPrecision, currentPrecision, out double grayMean);
            if (imageMatrix == null) { return null; }
            
            // Get hash
            StringBuilder sbHash = new StringBuilder();
            for (int w = 0; w < currentPrecision; w++)
            {
                for (int h = 0; h < currentPrecision; h++)
                {
                    sbHash.Append(imageMatrix[w, h] > grayMean ? "1" : "0");
                }
            }
            return sbHash.ToString();
        }
        /// <summary>
        /// 获得反向hash
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static string GetDifferenceHash(Bitmap bitmap)
        {
            // Get gray image matrix
            double[,] imageMatrix = GetResizedGrayImageMatrix(bitmap,
                currentPrecision, currentPrecision + 1, out double _);
            if (imageMatrix == null) { return null; }

            // Get hash
            StringBuilder sbHash = new StringBuilder();
            for (int w = 0; w < currentPrecision; w++)
            {
                for (int h = 0; h < currentPrecision; h++)
                {
                    sbHash.Append(imageMatrix[w, h] > imageMatrix[w + 1, h] ? "1" : "0");
                }
            }
            return sbHash.ToString();
        }

        #region Perceptual hash
        /// <summary>
        /// 得到感知哈希
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private static string GetPerceptualHash(Bitmap bitmap)
        {
            // Get gray image matrix
            double[,] imageMatrix = GetResizedGrayImageMatrix(bitmap,
                currentPrecision, currentPrecision, out double _);
            if (imageMatrix == null) { return null; }

            // Generate transformation matrix
            double[,] transMatrix = GetTransMatrix(currentPrecision);

            // Get DCT coefficient
            double[,] frequencyMatrix = MatrixHelper.MultiplyMatrixes(
                MatrixHelper.MultiplyMatrixes(transMatrix, imageMatrix),
                MatrixHelper.GetTansposedMatrix(transMatrix));

            // Take low frequency part and get mean frequency
            double frequencySum = 0;
            int height = currentPrecision / 2;
            int width = currentPrecision / 2;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    frequencySum += frequencyMatrix[i, j];
                }
            }
            double frequencyMean = frequencySum / height / width;

            // Get hash
            StringBuilder sbHash = new StringBuilder();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    sbHash.Append(frequencyMatrix[i, j] > frequencyMean ? "1" : "0");
                }
            }
            return sbHash.ToString();
        }

        private static ConcurrentDictionary<int, double[,]> listTransMatrix = 
            new ConcurrentDictionary<int, double[,]>();
        /// <summary>
        /// 得到反式矩阵
        /// </summary>
        /// <param name="precision"></param>
        /// <returns></returns>
        private static double[,] GetTransMatrix(int precision)
        {
            return listTransMatrix.GetOrAdd(precision, key =>
            {
                // Generate transformation matrix
                double[,] transMatrix = new double[key, key];
                for (int i = 0; i < transMatrix.GetUpperBound(0) + 1; i++)
                {
                    for (int j = 0; j < transMatrix.GetUpperBound(1) + 1; j++)
                    {
                        double ci = i == 0 ? Math.Sqrt(1.0 / key) : Math.Sqrt(2.0 / key);
                        transMatrix[i, j] = ci * Math.Cos(Math.PI * (j + 0.5) * i / key);
                    }
                }
                return transMatrix;
            });
        }
        /// <summary>
        /// 获取调整大小的灰度图像矩阵
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="grayMean"></param>
        /// <returns></returns>
        private static double[,] GetResizedGrayImageMatrix(Bitmap bitmap,
            int height, int width, out double grayMean)
        {
            grayMean = 0;

            Stopwatch watch = new Stopwatch();
            watch.Restart();

            // Load original image
            Bitmap originalBmp;
            try { originalBmp = bitmap; }
            catch (ArgumentException) { return null; }

            watch.Stop();
            long originalBmpTime = watch.ElapsedMilliseconds;

            watch.Restart();
            long resizeImageTime = 0;

            // Resize image
            double[,] imageMatrix = new double[width, height];
            double graySum = 0;
            using (Bitmap bmp = new Bitmap(width, height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CompositingQuality = CompositingQuality.HighSpeed;
                    g.InterpolationMode = currentInterpolationMode;
                    g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                    g.DrawImage(originalBmp, 0, 0, bmp.Width, bmp.Height);
                }

                watch.Stop();
                resizeImageTime = watch.ElapsedMilliseconds;
                watch.Restart();

                // Get gray image matrix
                BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                    ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                IntPtr ptr = bd.Scan0;
                int length = Math.Abs(bd.Stride) * bmp.Height;
                byte[] pixels = new byte[length];
                Marshal.Copy(ptr, pixels, 0, length);
                for (int i = 0; i < pixels.Length; i += 4)
                {
                    double gray = pixels[i + 1] * 0.30 +
                                  pixels[i + 2] * 0.59 +
                                  pixels[i + 3] * 0.11;
                    int x = Math.DivRem(i / 4, height, out int y);
                    imageMatrix[x, y] = gray;
                    graySum += gray;
                }
                bmp.UnlockBits(bd);
            }

            watch.Stop();
            long imageMatrixTime = watch.ElapsedMilliseconds;
            Debug.WriteLine($"Original: {originalBmpTime,3}ms; Resize: {resizeImageTime,3}ms; Matrix: {imageMatrixTime}ms");

            originalBmp.Dispose();

            // Get mean gray
            grayMean = graySum / height / width;

            return imageMatrix;
        }

        #endregion Perceptual hash
        /// <summary>
        /// 获取图片Hash明汉距离百分比
        /// </summary>
        /// <param name="hash1"></param>
        /// <param name="hash2"></param>
        /// <returns></returns>
        private static double GetHammingDistancePercent(string hash1, string hash2)
        {
            if (hash1.Length != hash2.Length)
            {
                throw new ArgumentException("哈希值长度不一样");
            }

            int sameNum = 0;
            for (int i = 0; i < hash1.Length; i++)
            {
                if (hash1[i] == hash2[i]) { sameNum++; }
            }
            return (double)sameNum / hash1.Length;
        }
    }
}
