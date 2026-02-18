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

    public List<ColorObject> ColorObjects = new List<ColorObject>();
    private ColorObject[] _baseColorObject;
    public CoreGameManager()
    {
        CorrectBlock.OnTouchCorrect += TouchCorrectBlock;
        UncorrectBlock.OnTouchUncorrect += TouchUncorrectBlock;

        _baseColorObject = new ColorObject[4]
        {
            new ColorObject("Black", Color.black),
            new ColorObject("Green",Color.green),
            new ColorObject("Red", Color.red),
            new ColorObject("Blue", Color.blue),
        };

        MatchNumbersForGame();
    }
    private void TouchCorrectBlock(IBlock block)
    {
        Debug.Log("You win");
        CorrectCount--;

        if(CorrectCount <= 0)
        {
            ColorObjects.Remove(ColorObjects[ColorObjects.Count - 1]);
        }

        if (ColorObjects.Count <= 0)
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

        for(int i = 0;  i < CountColorsForLevel && i < 4; i++)
        {
            ColorObjects.Add(_baseColorObject[i]);
        }
    }
}
