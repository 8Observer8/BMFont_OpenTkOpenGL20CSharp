using System;
using System.Collections.Generic;
using System.IO;

namespace Engine
{
    class FontParser
    {
        static int HeaderSize = 4;

        // Gets the value after an equal sign and converts it
        // from a string to an integer
        private static int GetValue(string s)
        {
            string value = s.Substring(s.IndexOf('=') + 1);
            return int.Parse(value);
        }

        public static Dictionary<char, CharacterData> Parse(string filePath)
        {
            Dictionary<char, CharacterData> charDictionary = new Dictionary<char, CharacterData>();

            string[] lines = File.ReadAllLines(filePath);

            for (int i = HeaderSize; i < lines.Length; i += 1)
            {
                string firstLine = lines[i];
                string[] typesAndValues = firstLine.Split(" ".ToCharArray(),
                    StringSplitOptions.RemoveEmptyEntries);

                // All the data comes in a certain order,
                // used to make the parser shorter
                CharacterData charData = new CharacterData
                {
                    Id = GetValue(typesAndValues[1]),
                    X = GetValue(typesAndValues[2]),
                    Y = GetValue(typesAndValues[3]),
                    Width = GetValue(typesAndValues[4]),
                    Height = GetValue(typesAndValues[5]),
                    XOffset = GetValue(typesAndValues[6]),
                    YOffset = GetValue(typesAndValues[7]),
                    XAdvance = GetValue(typesAndValues[8])
                };
                charDictionary.Add((char)charData.Id, charData);
            }
            return charDictionary;
        }
    }
}