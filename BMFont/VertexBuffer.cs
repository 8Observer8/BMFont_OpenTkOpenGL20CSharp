using OpenTK.Graphics.OpenGL;

namespace Engine
{
    public class VertexBuffer
    {
        private static VertexBuffer _instance;
        private int _squareVertexBuffer;
        private int _textureCoordBuffer;

        private VertexBuffer() { }

        public static VertexBuffer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new VertexBuffer();
                }
                return _instance;
            }
        }

        public int SquareVertexBuffer
        {
            get
            {
                return _squareVertexBuffer;
            }
        }

        public int TextureCoordBuffer
        {
            get
            {
                return _textureCoordBuffer;
            }
        }

        public bool Initialize()
        {
            // Create a buffer for our vertex positions
            GL.GenBuffers(1, out _squareVertexBuffer);

            // Activate vertexBuffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, _squareVertexBuffer);

            float[] vertices = new float[]
            {
                -0.5f, 0.5f, 0.0f,
                -0.5f, -0.5f, 0.0f,
                0.5f, 0.5f, 0.0f,
                0.5f, -0.5f, 0.0f
            };

            // Write the data into the vertexBuffer
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // Create a buffer for texture coordinates
            GL.GenBuffers(1, out _textureCoordBuffer);

            // Activate textureBuffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, _textureCoordBuffer);

            // Define the corresponding texture cooridnates
            float[] textureCoords = new float[]
            {
                0f, 0f,
                0f, 1f,
                1f, 0f,
                1f, 1f
            };

            // Loads textureCoords into the vertexBuffer
            GL.BufferData(BufferTarget.ArrayBuffer, textureCoords.Length * sizeof(float), textureCoords, BufferUsageHint.StaticDraw);

            return true;
        }
    }
}