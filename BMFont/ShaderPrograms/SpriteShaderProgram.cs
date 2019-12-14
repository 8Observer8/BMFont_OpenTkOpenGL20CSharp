using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Engine
{
    class SpriteShaderProgram : TextureShaderProgram
    {
        private int _texCoordBuffer;

        public SpriteShaderProgram(
            string vertexShaderPath, string fragmentShaderPath)
            : base(vertexShaderPath, fragmentShaderPath)
        {
            float[] textureCoords = new float[] {
                1f, 1f,
                0f, 1f,
                1f, 0f,
                0f, 0f
            };

            GL.GenBuffers(1, out _texCoordBuffer);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _texCoordBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, textureCoords.Length * sizeof(float), textureCoords, BufferUsageHint.StaticDraw);

            GL.UseProgram(_shaderProgram);
            // Set the texture unit 0 to the sampler
            GL.Uniform1(_samplerLocation, 0);
        }

        public new void Active(Color4 color, Matrix4 vpMatrix)
        {
            Active(color, vpMatrix, _texCoordBuffer);
        }

        public void SetTextureCoordinate(float[] texCoords)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _texCoordBuffer);
            GL.BufferSubData(BufferTarget.ArrayBuffer, (IntPtr)0, texCoords.Length * sizeof(float), texCoords);
        }
    }
}