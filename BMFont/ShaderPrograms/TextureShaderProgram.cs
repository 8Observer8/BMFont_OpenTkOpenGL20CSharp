using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Engine
{
    class TextureShaderProgram : ColorShaderProgram
    {
        protected int _samplerLocation;
        protected int _aTexCoordLocation;

        public TextureShaderProgram(
            string vertexShaderPath, string fragmentShaderPath)
            : base(vertexShaderPath, fragmentShaderPath)
        {
            _aTexCoordLocation = GL.GetAttribLocation(_shaderProgram, "aTexCoord");
            if (_aTexCoordLocation < 0)
            {
                Logger.Instance.Print("Failed to get the storage location of aTexCoord");
                return;
            }

            _samplerLocation = GL.GetUniformLocation(_shaderProgram, "uSampler");
            if (_samplerLocation < 0)
            {
                Logger.Instance.Print("Failed to get the storage location of uSampler");
                return;
            }

            GL.UseProgram(_shaderProgram);
            // Set the texture unit 0 to the sampler
            GL.Uniform1(_samplerLocation, 0);
        }

        public void Active(Color4 color, Matrix4 vpMatrix, int texBuffer = -1)
        {
            base.Active(color, vpMatrix);
            if (texBuffer == -1)
            {
                ActiveTextureBuffer(VertexBuffer.Instance.TextureCoordBuffer);
            }
            else
            {
                ActiveTextureBuffer(texBuffer);
            }
        }

        private void ActiveTextureBuffer(int buffer)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer);
            GL.VertexAttribPointer(_aTexCoordLocation, 2, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(_aTexCoordLocation);
        }
    }
}