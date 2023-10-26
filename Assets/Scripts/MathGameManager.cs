using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathGameManager : MonoBehaviour
{
    enum MathType { Add, Sub, Multiply };

    [SerializeField] MathType _mathType;

    [SerializeField] List<MultipleChoiceSelect> _choices = new List<MultipleChoiceSelect>();

    int _value1;
    int _value2;

    int _correctAnswer;

    [SerializeField] int[] _lastCreatedNumber;

    [SerializeField] TMP_Text _questionText;

    

    bool _isCorrectAssigned = false;

    private void Start()
    {
        _lastCreatedNumber = new int[4];
        _mathType = (MathType)Random.Range(0, 3);

        switch (_mathType)
        {
            case MathType.Add: Add(); break;
            case MathType.Sub: Subtract(); break;
            case MathType.Multiply: Multiply(); break;
            default: break;
        }

        for (int i = 0; i < _lastCreatedNumber.Length; i++)
        {
            var randomValue = Random.Range(0, _choices.Count);

            if (!_isCorrectAssigned)
            {
                _choices[randomValue].Value = _correctAnswer;
                _isCorrectAssigned = true;
            }
            else
            {
                var randomRange = Random.Range(1, 8);
                var randomBool = Random.Range(0, 2);

                if (randomBool == 0)
                {
                    _choices[randomValue].Value = _correctAnswer + randomRange;

                    foreach(var number in _lastCreatedNumber)
                    {
                        if(_choices[randomValue].Value == number)
                        {
                            _choices[randomValue].Value += Random.Range(1, 4);
                        }
                    }
                }
                else
                {
                    _choices[randomValue].Value = _correctAnswer - randomRange;

                    foreach (var number in _lastCreatedNumber)
                    {
                        if (_choices[randomValue].Value == number)
                        {
                            _choices[randomValue].Value -= Random.Range(1, 4);
                        }
                    }
                }
            }

            _lastCreatedNumber[i] = _choices[randomValue].Value;
            _choices[randomValue].AssignText();
            _choices.Remove(_choices[randomValue]);
        }


    }

    private void Update()
    {
        if (_choices.Count < 1)
            return;

        
    }

    void Multiply()
    {
        RandomizeValues(1, 10);
        _correctAnswer = _value1 * _value2;
        _questionText.text = _value1.ToString() + " X " + _value2.ToString();
    }

    void Subtract()
    {
        RandomizeValues(8, 50);
        _correctAnswer = _value1 - _value2;
        _questionText.text = _value1.ToString() + " - " + _value2.ToString();
    }

    void Add()
    {
        RandomizeValues(8, 50);
        _correctAnswer = _value1 + _value2;
        _questionText.text = _value1.ToString() + " + " + _value2.ToString();
    }


    void RandomizeValues(int minValue, int maxValue)
    {
        _value1 = Random.Range(minValue, maxValue);
        _value2 = Random.Range(minValue, maxValue);
    }

}
