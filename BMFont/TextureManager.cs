using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;

namespace Engine
{
    class TextureManager : IDisposable
    {
        private Dictionary<string, Texture> _textureDatabase = new Dictionary<string, Texture>();

        #region IDisposable Members

        public void Dispose()
        {
            foreach (Texture t in _textureDatabase.Values)
            {
                GL.DeleteTextures(1, new int[] { t.Id });
            }
        }

        #endregion

        public Texture Get(string textureName)
        {
            return _textureDatabase[textureName];
        }

        public void LoadTexture(string textureId, string path)
        {
            // Create the image object
            Bitmap image = null;
            try
            {
                image = new Bitmap(path);
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.Assert(false,
                    "\"TextureManager.cs\".  Could not open file, [" + path + "].");
                return;
            }

            // Generate a texture buffer object
            int texture = GL.GenTexture();

            // Bind the texture object to the Texture2D target
            GL.BindTexture(TextureTarget.Texture2D, texture);

            // Create and set up a rectangle
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            // Lock image data on the rectangle
            BitmapData data = image.LockBits(rect, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            // Load image data to the texture buffer object
            GL.TexImage2D(TextureTarget.Texture2D, 0,
                PixelInternalFormat.Rgba, image.Width, image.Height,
                0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                PixelType.UnsignedByte, data.Scan0);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            _textureDatabase.Add(textureId, new Texture(texture, image.Width, image.Height));

            image.UnlockBits(data);
        }
    }
}