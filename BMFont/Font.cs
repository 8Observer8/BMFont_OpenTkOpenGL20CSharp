using System.Collections.Generic;

namespace Engine
{
    class Font
    {
        private Texture _texture;
        private Dictionary<char, CharacterData> _characterData;

        public Font(
            Texture texture,
            Dictionary<char, CharacterData> characterData)
        {
            _texture = texture;
            _characterData = characterData;
        }

        public CharacterSprite CreateSprite(char c)
        {
            CharacterData charData = _characterData[c];
            Sprite sprite = new Sprite();
            sprite.Texture = _texture;

            // Setup UVs
            Point topLeft = new Point((float)charData.X / _texture.Width,
                                        (float)charData.Y / _texture.Height);
            Point bottomRight = new Point(topLeft.X + ((float)charData.Width / _texture.Width),
                                          topLeft.Y + ((float)charData.Height / _texture.Height));
            sprite.SetUVs(topLeft, bottomRight);
            sprite.XForm.Width = charData.Width;
            sprite.XForm.Height = charData.Height;
            //sprite.SetWidth(charData.Width);
            //sprite.SetHeight(charData.Height);

            return new CharacterSprite(sprite, charData);
        }
    }
}