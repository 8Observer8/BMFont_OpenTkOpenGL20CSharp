using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Engine;

namespace Flowchart
{
    class Window : GameWindow
    {
        private TextureManager _textureManager;
        private Font _font;
        private Text _myText;

        private const int _width = 250;
        private const int _height = 250;

        private Renderer _renderer;
        private SpriteShaderProgram _spriteShaderProgram;
        private ColorShaderProgram _colorShaderProgram;
        private Camera _camera;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Title = "OpenGL, C#";
            Width = _width;
            Height = _height;

            GL.ClearColor(Color4.DarkOliveGreen);
            GL.Enable(EnableCap.DepthTest);

            _camera = new Camera(
               wcCenter: new Vector2(_width / 2f, _height / 2f),
               wcWidth: _width,
               viewport: new Vector4(0, 0, _width, _height));

            _camera.BackgroundColor = new Color4(0.905f, 0.850f, 0.752f, 1f);

            _camera.SetupViewProjection();

            VertexBuffer.Instance.Initialize();
            _textureManager = new TextureManager();
            _textureManager.LoadTexture("font", "Assets/Fonts/font.png");

            _spriteShaderProgram = new SpriteShaderProgram(
                "Assets/Shaders/vTexture.glsl",
                "Assets/Shaders/fTexture.glsl");

            _colorShaderProgram = new ColorShaderProgram(
                "Assets/Shaders/vColor.glsl",
                "Assets/Shaders/fColor.glsl");

            _renderer = new Renderer(_colorShaderProgram, _spriteShaderProgram);
            _font = new Font(
                _textureManager.Get("font"),
                FontParser.Parse("Assets/Fonts/font.fnt"));

            _myText = new Text("Hello World!", _font);
            _myText.SetColor(new Color4(0f, 0f, 0f, 1f));
            _myText.Translate(new Vector3(_width / 2f - 100f, _height / 2f, 0f));
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _renderer.DrawText(_myText, _camera.VPMatrix);
            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _camera.SetupViewProjection();
        }
    }
}
