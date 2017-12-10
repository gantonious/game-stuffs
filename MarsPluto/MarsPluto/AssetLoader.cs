using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace MarsPluto
{
    class AssetLoader
    {
        private Dictionary<String, int> texRepo= new Dictionary<String, int>();

        public int loadTexture(string path)
        {

            if (texRepo.ContainsKey(path)) return texRepo[path];
            if (!File.Exists(path)) path = "Assets/Textures/notFound.png";
      
            int textureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureID);

            Bitmap bmp = new Bitmap(path);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), 
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                            OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                (int)TextureWrapMode.Clamp);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Linear);

            texRepo[path] = textureID;
            return textureID;
        }

        public int loadFont(string path, string caption, int width, int height, int size)
        {
            if (!File.Exists(path)) path = "Assets/Textures/notFound.png";

            int textureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureID);

            Bitmap bmp = new Bitmap(width, height);


            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.Clear(Color.Transparent);
                gfx.DrawString(caption, new Font(path, size), new SolidBrush(Color.White), new PointF(0, 0));
            }

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                            OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
                (int)TextureWrapMode.Clamp);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Linear);




            return textureID;
        }
    }
}
