using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGameManager
{
    public int CorrectCount = 0;
    public int UncorrectCount = 0;

    public int CountBoxForLevel = 3;
    public int ProcentErrorColor = 0;
    public int CountColorsForLevel = 2;

    public int Level = 1;
    public CoreGameManager()
    {
        CorrectBlock.OnTouchCorrect += TouchCorrectBlock;
        UncorrectBlock.OnTouchUncorrect += TouchUncorrectBlock;
    }
    private void TouchCorrectBlock(IBlock block)
    {
        Debug.Log("You win");
        CorrectCount--;

        if (CorrectCount <= 0 && UncorrectCount <= 0)
        {
            MatchNumbersForGame();
            Level++;
        }
    }

    private void TouchUncorrectBlock(IBlock block)
    {
        Debug.Log("You lose");
        UncorrectCount--;
    }

    private void MatchNumbersForGame()
    {
        CountBoxForLevel = (int)(Mathf.Pow(Level / 5f, 2) + 3f);
        ProcentErrorColor = (int)(Mathf.Pow(Level / 2f, 2) + 0f);
        CountColorsForLevel = (int)(Mathf.Pow(Level / 3f, 2) + 2f);
    }
}
