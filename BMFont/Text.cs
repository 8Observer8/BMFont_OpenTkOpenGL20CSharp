using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK;

namespace Engine
{
    class Text
    {
        private Font _font;
        private List<CharacterSprite> _bitmapText = new List<CharacterSprite>();
        private string _text;
        private Color4 _color = new Color4(1f, 1f, 1f, 1f);

        public Text(string text, Font font)
        {
            _text = text;
            _font = font;

            CreateText(0f, 0f, 0f);
        }

        public List<CharacterSprite> CharacterSprites
        {
            get { return _bitmapText; }
        }

        private void CreateText(float x, float y, float z)
        {
            _bitmapText.Clear();
            float currentX = x;
            float currentY = y;

            // Kerning should go here, use char previousCharacter = nil
            foreach (char c in _text)
            {
                CharacterSprite sprite = _font.CreateSprite(c);
                float xOffset = ((float)sprite.Data.XOffset) / 2;
                float yOffset = ((float)sprite.Data.YOffset) / 2;
                sprite.Sprite.SetPosition(currentX + xOffset, currentY - yOffset, z);
                currentX += sprite.Data.XAdvance;
                _bitmapText.Add(sprite);
            }
            SetColor(_color);
        }

        public void SetColor(Color4 color)
        {
            _color = color;
            foreach (CharacterSprite s in _bitmapText)
            {
                s.Sprite.Color = _color;
            }
        }

        public void Translate(Vector3 vector)
        {
            foreach (CharacterSprite s in _bitmapText)
            {
                s.Sprite.SetPosition(
                    s.Sprite.XForm.XPos + vector.X,
                    s.Sprite.XForm.YPos + vector.Y,
                    s.Sprite.XForm.ZPos + vector.Z);
            }
        }
    }
}
