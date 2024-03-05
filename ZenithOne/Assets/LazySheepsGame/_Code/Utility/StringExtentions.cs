using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames
{
    public static class StringExtentions
    {
        public static string SetColor(this string inputText, string color)
        {
            return "<color=" + color + ">" + inputText + "</color>";
        }

    }
}