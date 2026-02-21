using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        List<ColorObject> colorObjectForRandom = new List<ColorObject>(_baseColorObject);

        for(int i = 0;  i < CountColorsForLevel && i < 4; i++)
        {
            ColorObjects.Add(colorObjectForRandom[Random.Range(0, colorObjectForRandom.Count)]);

            colorObjectForRandom.Remove(ColorObjects[i]);
        }

        for (int i = 0; i < CountColorsForLevel && i < 4; i++)
        {
            if(Random.Range(0,100) <= ProcentErrorColor)
            {
                SwithName();
            }
        }
    }

    private void SwithName()
    {
        int objNumber1 = Random.Range(0, ColorObjects.Count); //Берём случаный номер объекта для замены имени
        int objNumber2 = ColorObjects.Count - 1 - objNumber1; //Заменяем у выбраного объекта имя на следующее в списке доступных объектов

        var nameObj1 = ColorObjects[objNumber1].Name;

        ColorObjects[objNumber1] = new ColorObject(ColorObjects[objNumber2].Name, ColorObjects[objNumber1].Color);
        ColorObjects[objNumber2] = new ColorObject(nameObj1, ColorObjects[objNumber2].Color);


    }
}
