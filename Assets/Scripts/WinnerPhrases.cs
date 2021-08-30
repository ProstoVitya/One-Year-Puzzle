using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WinnerPhrases
{
    private static string[] _victoryPhrases =
                    { "Умничка)", "Молодец<3", "Собрала лучшую парочку (:", "ОГОО",
                      "О, смотри! Это мы!!!", "Лучшая собирательница<3", "Супер!", "Победительница",
                      "Поздравляю ты собрала себя! или меня...", "О а о какие красавцы <3"
                    };

    public static string Phrase => _victoryPhrases[Random.Range(0, _victoryPhrases.Length)];
}
