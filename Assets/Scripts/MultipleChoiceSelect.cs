using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultipleChoiceSelect : MonoBehaviour
{
    [SerializeField] int _value;

    bool _isSelected;

    TMP_Text _displayText;


    public int Value { get { return _value; } set { _value = value; } }

    private void Start()
    {
        _displayText = GetComponentInChildren<TMP_Text>();
    }

    public void AssignText()
    {
        _displayText.text = _value.ToString();
    }

}
