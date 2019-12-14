namespace Engine
{
    class Sprite : Object3D
    {
        private const int _vertexAmount = 4;
        private Texture _texture = null;
        private float[] _texCoords = null;

        public Sprite()
        {
            _texCoords = new float[8];
        }

        public static int VertexAmount
        {
            get { return _vertexAmount; }
        }

        public Texture Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public float[] TexCoords
        {
            get
            {
                return _texCoords;
            }
        }

        public void SetUVs(Point topLeft, Point bottomRight)
        {
            _texCoords[0] = topLeft.X;
            _texCoords[1] = topLeft.Y;

            _texCoords[2] = topLeft.X;
            _texCoords[3] = bottomRight.Y;

            _texCoords[4] = bottomRight.X;
            _texCoords[5] = topLeft.Y;

            _texCoords[6] = bottomRight.X;
            _texCoords[7] = bottomRight.Y;
        }
    }
}
