using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ColorUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void UpdateText(ColorObject colorObject)
    {
        _text.text = colorObject.Name;
        _text.color = colorObject.Color;
    }
}
