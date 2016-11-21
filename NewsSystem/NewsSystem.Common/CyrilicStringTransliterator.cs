using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsSystem.Common
{
    public static class CyrilicStringTransliterator
    {
        private static string[] CyrilicSpecials = { "дж", "дз", "ьо", "йо", "йя" };
        private static string[] EnglishSpecials = { "dzh", "dz", "yo", "yo", "ia" };

        private static string[] CyrilicAlphabet = { "а", "б", "в", "г", "д", "е", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ь", "ю", "я" };
        private static string[] EnglishAlphabet = { "a", "b", "v", "g", "d", "e", "zh", "z", "i", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "h", "ts", "ch", "sh", "sht", "a", "y", "yu", "ya" };

        public static string TranslateToEnglishLetters(string cyrilicInput)
        {
            string cleanTitle = cyrilicInput.Trim().ToLower();
            for (int i = 0; i < CyrilicSpecials.Length; i++)
            {
                cleanTitle = cleanTitle.Replace(CyrilicSpecials[i], EnglishSpecials[i]);
            }
            for (int i = 0; i < CyrilicAlphabet.Length; i++)
            {
                cleanTitle = cleanTitle.Replace(CyrilicAlphabet[i], EnglishAlphabet[i]);
            }
            return cleanTitle;
        }
    }
}

