using UnityEngine;
using System;

namespace Assets
{
    internal class Perlin2D
    {
        private readonly byte[] permutationTable;

        public Perlin2D(int seed)
        {
            System.Random rand = new System.Random(seed);
            permutationTable = new byte[1024];
            rand.NextBytes(permutationTable);
        }



        private float[] PseudorandomoweWektory(int x, int y)
        {
            int v = (int)(((x * 1836311903) ^ (y * 2971215073 + 4807526976)) & 1023);
            v = permutationTable[v] & 3;

            switch (v)
            {
                case 0: return new float[] { 1, 0 };
                case 1: return new float[] { -1, 0 };
                case 2: return new float[] { 0, 1 };
                default: return new float[] { 0, -1 };
            }
        }

        private static float QunticCurve(float t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        private static float Interpolate(float a, float b, float t)
        {

            float t2 = (float)(1 - Math.Cos(t * Math.PI)) / 2;
            return (a * (1 - t2) + b * t2);
        }

        private static float Scalar(float[] a, float[] b)
        {
            return a[0] * b[0] + a[1] * b[1];
        }

        public float Noise(float fx, float fy)
        {
            // Koordynaty górnego lewego wektora prostokąta
            int left = (int)Math.Floor(fx);
            int top = (int)Math.Floor(fy);
            // Lokalne koordynaty punktu
            float QuadPointX = fx - left;
            float QuadPoindY = fy - top;

            // Pseudorandomowe wektory dla wszystkich kątów prostokąta
            float[] topLeftGradient = PseudorandomoweWektory(left, top);
            float[] topRightGradient = PseudorandomoweWektory(left + 1, top);
            float[] bottomLeftGradient = PseudorandomoweWektory(left, top + 1);
            float[] bottomRightGradient = PseudorandomoweWektory(left + 1, top + 1);

            // Wektory od kątów do punktu
            float[] ToTopLeft = { QuadPointX, QuadPoindY };
            float[] ToTopRight = { QuadPointX - 1, QuadPoindY };
            float[] ToBottomLeft = { QuadPointX, QuadPoindY - 1 };
            float[] BottomRight = { QuadPointX - 1, QuadPoindY - 1 };

            // Skalarowy iloczyn wektorów (Wartość wektorów)
            float tl = Scalar(ToTopLeft, topLeftGradient);
            float tr = Scalar(ToTopRight, topRightGradient);
            float bl = Scalar(ToBottomLeft, bottomLeftGradient);
            float br = Scalar(BottomRight, bottomRightGradient);


           // QuadPointX = QunticCurve(QuadPointX);
           // QuadPoindY = QunticCurve(QuadPoindY);

            // Interpolacja:
            float tx = Interpolate(tl, tr, QuadPointX);
            float bx = Interpolate(bl, br, QuadPointX);
            float tb = Interpolate(tx, bx, QuadPoindY);

            return tb;
        }

        public float Noise(float fx, float fy, int octaves, float persistence ,float amplitude)
        {

            float max = 0F;

            float result = 0; //Wartość punktu po iteracjam

            while (octaves-- > 0)
            {

                //Iteracja algorytmu, zmienszamy amplitudę
                result += Noise(fx, fy) * amplitude;
                amplitude *= persistence;
                max += amplitude;
                fx *= 2F;
                fy *= 2F;
            }

            return result ;
        }


    }
}