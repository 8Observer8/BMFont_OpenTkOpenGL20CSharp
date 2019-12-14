using OpenTK.Graphics;

namespace Engine
{
    abstract class Object3D
    {
        public Object3D()
        {
            XForm = new Transform();
            Color = new Color4(0.5f, 0.5f, 0.5f, 1f);
        }

        public Transform XForm { get; }
        public Color4 Color { get; set; }

        public void SetPosition(float x, float y, float z)
        {
            XForm.SetPosition(x, y, z);
        }
    }
}