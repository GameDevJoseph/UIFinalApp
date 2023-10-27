using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    [SerializeField] Slider _timerSlider;

    [SerializeField] AudioSource _sfxSource;
    [SerializeField] AudioClip _correctSfx;
    [SerializeField] AudioClip _incorrectSfx;

    [SerializeField] GameObject _stats;
    [SerializeField] TMP_Text _statsText;

    float _time = 120f;

    string _player;

    bool _hasGameStarted = false;

    private void Start()
    {
        Time.timeScale = 1;
        _player = PlayerPrefs.GetString("CurrentUser");

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

    void Update()
    {
        if (!_hasGameStarted)
            return;


        _time -= Time.deltaTime;

        _timerSlider.value = _time;

        if(_time <= 0)
        {
            _stats.SetActive(true);
            _statsText.text = _correctAmount.ToString() + " answered correctly in 2 mins";

            PlayerPrefs.SetInt("User" + _player + "MostCorrect", _correctAmount);
            Time.timeScale = 0;
        }
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
            _answerInput.text = "";
            _sfxSource.PlayOneShot(_correctSfx);
            GenerateQuestion();
        }else
        {
            _answerInput.text = "";
            _debugText.color = Color.red;
            _debugText.text = "Incorrect";
            _sfxSource.PlayOneShot(_incorrectSfx);
        }
    }

    public void OnInputSelect()
    {
        _answerInput.text = "";
    }

    public void StartGame()
    {
        _hasGameStarted = true;
        GenerateQuestion();
    }

    public void LoadLevel(string value)
    {
        SceneManager.LoadScene(value);
    }

    public void PauseGame(int value)
    {
        Time.timeScale = value;
    }
}
