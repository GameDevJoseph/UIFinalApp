using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MathGameManager : MonoBehaviour
{
    enum MathType { Add, Sub, Multiply };

    [SerializeField] MathType _mathType;

    

    int _value1;
    int _value2;

    int _correctAnswer;
    int _correctAmount;
    

    [SerializeField] TMP_Text _questionText;
    [SerializeField] TMP_InputField _answerInput;
    [SerializeField] TMP_Text _debugText;

    float _maxTime = 300f;
    

    bool _isCorrectAssigned = false;

    private void Start()
    {
        GenerateQuestion();
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

    public void GenerateQuestion()
    {
        _mathType = (MathType)Random.Range(0, 3);

        switch (_mathType)
        {
            case MathType.Add: Add(); break;
            case MathType.Sub: Subtract(); break;
            case MathType.Multiply: Multiply(); break;
            default: break;
        }
    }
    public void ValueEntered()
    {
        if(_answerInput.text == _correctAnswer.ToString())
        {
            _correctAmount += 1;
            _debugText.color = Color.green;
            _debugText.text = "Correct";
            GenerateQuestion();
        }else
        {
            _answerInput.text = "";
            _debugText.color = Color.red;
            _debugText.text = "Incorrect";
        }
    }

    public void OnInputSelect()
    {
        _answerInput.text = "";
    }
}
