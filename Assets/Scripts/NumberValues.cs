using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberValues : MonoBehaviour
{
    [SerializeField] int numberValue;
    [SerializeField] Image _imageDrop;

    public int NumberValue { get { return numberValue; } }  
    public void AssignValue(int value)
    {
        numberValue = value;
    }

    public void AssignImageDrop(Image image)
    {
        _imageDrop = image;
    }
}
