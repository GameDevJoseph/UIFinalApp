using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberValues : MonoBehaviour
{
    [SerializeField] int _numberValue;
    [SerializeField] Image _imageDrop;

    public int NumberValue { get { return _numberValue; } }  
    public void AssignValue(int value)
    {
        _numberValue = value;
    }

    public void AssignImageDrop(Image image)
    {
        _imageDrop = image;
    }

    public Sprite RevealImageValue()
    {
        return _imageDrop.sprite;
    }
}
