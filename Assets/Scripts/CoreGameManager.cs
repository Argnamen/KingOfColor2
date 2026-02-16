using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGameManager : MonoBehaviour
{
    public int CorrectCount = 0;
    public int UncorrectCount = 0;
    private void OnEnable()
    {
        CorrectBlock.OnTouchCorrect += TouchCorrectBlock;
        UncorrectBlock.OnTouchUncorrect += TouchUncorrectBlock;
    }
    private void TouchCorrectBlock(IBlock block)
    {
        Debug.Log("You win");
        CorrectCount--;
    }

    private void TouchUncorrectBlock(IBlock block)
    {
        Debug.Log("You lose");
        UncorrectCount--;
    }
}
