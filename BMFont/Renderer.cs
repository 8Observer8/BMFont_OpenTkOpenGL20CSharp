using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine
{
    class Renderer
    {
        private ColorShaderProgram _colorShaderProgram;
        private SpriteShaderProgram _spriteShaderProgram;

        public Renderer(
            ColorShaderProgram colorShaderProgram,
            SpriteShaderProgram shaderProgram)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            _colorShaderProgram = colorShaderProgram;
            _spriteShaderProgram = shaderProgram;
        }

        //private void DrawImmediateModeVertex(Vector3 position, Color4 color, Point uvs)
        //{
        //    GL.Color4(color.R, color.G, color.B, color.A);
        //    GL.TexCoord2(uvs.X, uvs.Y);
        //    GL.Vertex3(position.X, position.Y, position.Z);
        //}

        public void DrawSprite(Sprite sprite, Matrix4 vpMatrix)
        {
            GL.UseProgram(_spriteShaderProgram.ShaderProgram);

            GL.BindTexture(TextureTarget.Texture2D, sprite.Texture.Id);

            // To prevent texture wrappings
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToEdge);

            // Handles how magnification and minimization filters will work
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.LinearMipmapLinear);

            // Enable texture unit0
            GL.ActiveTexture(TextureUnit.Texture0);

            _spriteShaderProgram.LoadObjectTransform(sprite.XForm.ModelMatrix);

            _spriteShaderProgram.SetTextureCoordinate(sprite.TexCoords);

            _spriteShaderProgram.Active(sprite.Color, vpMatrix);

            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, Sprite.VertexAmount);
        }

        public void DrawText(Text text, Matrix4 vpMatrix)
        {
            foreach (CharacterSprite c in text.CharacterSprites)
            {
                DrawSprite(c.Sprite, vpMatrix);
            }
        }

        public void DrawColoredRect(Rect rect, Matrix4 vpMatrix)
        {
            GL.UseProgram(_colorShaderProgram.ShaderProgram);
            _colorShaderProgram.LoadObjectTransform(rect.XForm.ModelMatrix);
            _colorShaderProgram.Active(rect.Color, vpMatrix);
            GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
        }
    }
}