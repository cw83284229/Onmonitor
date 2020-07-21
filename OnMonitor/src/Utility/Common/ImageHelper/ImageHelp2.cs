using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Utility.Common.ImageHelper
{
    public class ImageHelp2
    {

        /// <summary>
        /// Image转Bitmap
        /// </summary>
        /// <param name="image"></param>

        /// <returns></returns>
        public Bitmap Resize(Image image)

        {

            Bitmap imgOutput = new Bitmap(image, 256, 256);

            return imgOutput;

        }




        public float GetSimilarity(Bitmap img1, Bitmap img2)
        {

            Bitmap imgOutput1 = new Bitmap(img1, 256, 256);
            Bitmap imgOutput2 = new Bitmap(img2, 256, 256);

            var imgint1 = GetHisogram(imgOutput1);
            var imgint2 = GetHisogram(imgOutput2);

            var requst = GetResult(imgint1, imgint2);
            return requst;


        }










        /// <summary>
        /// 计算图片直方图
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public int[] GetHisogram(Bitmap img)

        {

            BitmapData data = img.LockBits(new System.Drawing.Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int[] histogram = new int[256];

            unsafe

            {

                byte* ptr = (byte*)data.Scan0;

                int remain = data.Stride - data.Width * 3;

                for (int i = 0; i < histogram.Length; i++)

                    histogram[i] = 0;

                for (int i = 0; i < data.Height; i++)

                {

                    for (int j = 0; j < data.Width; j++)

                    {

                        int mean = ptr[0] + ptr[1] + ptr[2];

                        mean /= 3;

                        histogram[mean]++;

                        ptr += 3;

                    }

                    ptr += remain;

                }

            }

            img.UnlockBits(data);

            return histogram;

        }



        //计算相减后的绝对值

        private float GetAbs(int firstNum, int secondNum)

        {

            float abs = Math.Abs((float)firstNum - (float)secondNum);

            float result = Math.Max(firstNum, secondNum);

            if (result == 0)

                result = 1;

            return abs / result;

        }



        /// <summary>
        /// 比对图片直方图获得相似度结果
        /// </summary>
        /// <param name="firstNum"></param>
        /// <param name="scondNum"></param>
        /// <returns></returns>

        public float GetResult(int[] firstNum, int[] scondNum)

        {

            if (firstNum.Length != scondNum.Length)

            {

                return 0;

            }

            else

            {

                float result = 0;

                int j = firstNum.Length;

                for (int i = 0; i < j; i++)

                {

                    result += 1 - GetAbs(firstNum[i], scondNum[i]);

                    Console.WriteLine(i + "----" + result);

                }

                return result / j;

            }

        }


    }

    public class Imagehelp3

    {

        /// <summary>
        /// 相似度比较
        /// </summary>
        /// <param name="map1">标准图</param>
        /// <param name="map2">欲比较图</param>
        public static float xsdsf(Bitmap map1, Bitmap map2, bool jc)
        {
            int width = map1.Width;
            int height = map1.Height;
            long argv = 0;
            Bitmap imag = new Bitmap(width, height);
            //转换大小
            // map1 = new Bitmap(Resize(map1, width, height));//未打包修改图片大小代码，自行转换，这里屏蔽
            // map2 = new Bitmap(Resize(map2, width, height));
            if (jc)
            {
                //灰度化
                // map1 = ToGray(map1);//未打包修改图片灰度化，自行转换，这里屏蔽
                // map2 = ToGray(map2);
                //二值化
                // map1 = ConvertTo1Bpp1(map1, "10");//未打包修改图片二值化代码，自行转换，这里屏蔽
                // map2 = ConvertTo1Bpp1(map2, "10");
            }
            //将两张图相同像素设置为0，不同像素设置为1
            for (int i = 0; i < map1.Width; i++)
            {
                for (int j = 0; j < map1.Height; j++)
                {
                    Color color1 = map1.GetPixel(i, j);
                    Color color2 = map2.GetPixel(i, j);
                    if (color1.B == color2.B)
                    {
                        imag.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                    }
                    else
                    {
                        imag.SetPixel(i, j, Color.FromArgb(1, 1, 1));
                        argv++;
                    }
                }
            }
            long xsh = width * height;
            float butongb = (float)argv / xsh;
            return 1 - butongb;
        }


    }

}








