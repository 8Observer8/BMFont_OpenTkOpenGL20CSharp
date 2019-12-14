using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Engine
{
    public class Camera
    {
        private Vector2 _wcCenter;
        private float _wcWidth;
        private Vector4 _viewport;
        private float _nearPlane = 0f;
        private float _farPlane = 1000f;
        private Matrix4 _viewMatrix;
        private Matrix4 _projMatrix;
        private Matrix4 _vpMatrix;

        public Color4 BackgroundColor { get; set; }

        public Camera(Vector2 wcCenter, float wcWidth, Vector4 viewport)
        {
            // WC and viewport position and size
            _wcCenter = wcCenter;
            _wcWidth = wcWidth;
            _viewport = viewport;   // [x, y, width, height]

            // Transformation matrices
            _viewMatrix = Matrix4.Identity;
            _projMatrix = Matrix4.Identity;
            _vpMatrix = Matrix4.Identity;

            BackgroundColor = new Color4(0.8f, 0.8f, 0.8f, 1f);
        }

        public Matrix4 VPMatrix
        {
            get
            {
                return _vpMatrix;
            }
        }

        public void SetupViewProjection()
        {
            GL.Viewport(
                (int)_viewport[0], (int)_viewport[1],
                (int)_viewport[2], (int)_viewport[3]);
            GL.Scissor(
                (int)_viewport[0], (int)_viewport[1],
                (int)_viewport[2], (int)_viewport[3]);
            GL.ClearColor(BackgroundColor);
            GL.Enable(EnableCap.ScissorTest);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Disable(EnableCap.ScissorTest);

            // Define the view matrix
            _viewMatrix = Matrix4.LookAt(
                new Vector3(_wcCenter[0], _wcCenter[1], 10f),
                new Vector3(_wcCenter[0], _wcCenter[1], 0f),
                new Vector3(0f, 1f, 0f));

            // Define the project matrix
            float halfWCWidth = 0.5f * _wcWidth;
            float halfWCHeight = halfWCWidth * _viewport[3] / _viewport[2]; // viewportH/viewportW
            _projMatrix = Matrix4.CreateOrthographicOffCenter(
                -halfWCWidth, halfWCWidth,
                -halfWCHeight, halfWCHeight,
                _nearPlane, _farPlane);

            _vpMatrix = _viewMatrix * _projMatrix;
        }
    }
}