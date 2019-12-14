using OpenTK;

namespace Engine
{
    public class Transform
    {
        private Vector3 _position;
        private Vector3 _scale;

        public Transform()
        {
            _position = new Vector3(0f, 0f, 0f);
            _scale = new Vector3(1f, 1f, 1f);
        }

        public void SetPosition(float x, float y, float z)
        {
            _position.X = x;
            _position.Y = y;
            _position.Z = z;
        }

        public void SetSize(float width, float height, float depth)
        {
            _scale.X = width;
            _scale.Y = height;
            _scale.Z = depth;
        }

        public float XPos
        {
            get
            {
                return _position.X;
            }

            set
            {
                _position.X = value;
            }
        }

        public float YPos
        {
            get
            {
                return _position.Y;
            }

            set
            {
                _position.Y = value;
            }
        }

        public float ZPos
        {
            get
            {
                return _position.Z;
            }

            set
            {
                _position.Z = value;
            }
        }

        public float Width
        {
            get
            {
                return _scale.X;
            }

            set
            {
                _scale.X = value;
            }
        }

        public float Height
        {
            get
            {
                return _scale.Y;
            }

            set
            {
                _scale.Y = value;
            }
        }

        public float Depth
        {
            get
            {
                return _scale.Z;
            }

            set
            {
                _scale.Z = value;
            }
        }

        public Matrix4 ModelMatrix
        {
            get
            {
                Matrix4 matrix =
                    Matrix4.CreateScale(_scale.X, _scale.Y, _scale.Z) *
                    Matrix4.CreateTranslation(_position.X, _position.Y, _position.Z);
                return matrix;
            }
        }
    }
}