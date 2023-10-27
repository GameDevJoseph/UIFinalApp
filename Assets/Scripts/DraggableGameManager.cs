using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DraggableGameManager : MonoBehaviour
{
    [SerializeField] Sprite _hideImage;
    [SerializeField] GameObject[] _prefabs;
    [SerializeField] List<Image> _gridSlots = new List<Image>();
    [SerializeField] GameObject[] _spawnSlots;
    [SerializeField] Image[] _imageDrops;
    [SerializeField] GameObject _instructionsObject;
    [SerializeField] GameObject _statsObject;
    [SerializeField] TMP_Text _statText;
    [SerializeField] TMP_Text _timerStatText;
    [SerializeField] TMP_Text _counterText;

    [SerializeField] AudioSource _sfxSource;
    [SerializeField] AudioClip _droppedInAudio;

    string _player;
    float _countdownTimer = 10f;
    bool _isBeingDisplayed;
    bool _timerOn;
    int _correctAnswers;
    int _minutes;

    private void Start()
    {
        Time.timeScale = 1;
        RandomizeImages();
        _counterText.text = _countdownTimer.ToString("F0");
        _statsObject.gameObject.SetActive(false);
        _player = PlayerPrefs.GetString("CurrentUser");
    }

    public void RandomizeImages()
    {
        for (int i = 0; i < _prefabs.Length; i++)
        {
            var imageNumber = _prefabs[i];
            var randomNumber = Random.Range(0, _gridSlots.Count);
            _spawnSlots[i] = _gridSlots[randomNumber].gameObject;
            var currentSlot = _gridSlots[randomNumber].GetComponent<Image>();
            _spawnSlots[i].GetComponent<Image>().sprite = imageNumber.GetComponent<Image>().sprite;
            _gridSlots.Remove(currentSlot);
        }

        AssignSlots();
        TurnEmptySlotsOff();
    }

    public void AssignSlots()
    {
        for(int i = 0; i < _spawnSlots.Length; i++)
        {
            _spawnSlots[i].GetComponent<NumberValues>().AssignValue(i + 1);
            _spawnSlots[i].GetComponent<NumberValues>().AssignImageDrop(_imageDrops[i]);
            
        }
    }

    void TurnEmptySlotsOff()
    {
        for(int i = 0; i < _gridSlots.Count; i++)
        {
            _gridSlots[i].GetComponent<Image>();
            _gridSlots[i].gameObject.SetActive(false);
        }
    }

    void TurnOffOnRaycastTarget(bool value)
    {
        for (int i = 0; i < _spawnSlots.Length; i++)
        {
            _spawnSlots[i].GetComponent<Image>().raycastTarget = value;
        }
    }

    public void TurnOffNumberDisplay()
    {
        for(int i = 0; i < _spawnSlots.Length; i++)
        {
            _spawnSlots[i].GetComponent<Image>().sprite = _hideImage;
        }
    }

    public void StartGame()
    {
        _isBeingDisplayed = true;
        StartCoroutine(DisplayNumbers());
        _timerOn = true;
    }

    IEnumerator DisplayNumbers()
    {
        while(_isBeingDisplayed)
        {
            _instructionsObject.gameObject.SetActive(false);
            TurnOffOnRaycastTarget(false);
            yield return new WaitForSeconds(10f);
            TurnOffNumberDisplay();
            TurnOffOnRaycastTarget(true);
            _isBeingDisplayed = false;
        }
    }

    private void Update()
    {
        if (_countdownTimer <= 0)
            _countdownTimer = 0;

        if (_isBeingDisplayed && _timerOn)
        {
            _countdownTimer -= Time.deltaTime;
            _counterText.text = _countdownTimer.ToString("F0");
        }
        else if(!_isBeingDisplayed && _timerOn)
        {
            _countdownTimer += Time.deltaTime;
            
            if (_countdownTimer > 59)
            {
                _countdownTimer = 0;
                _minutes += 1;
            }

            _counterText.text = _countdownTimer.ToString("F0");
        }

         
    }

    public void SubmitAnswers()
    {
        for(int i = 0; i < _imageDrops.Length; i++)
        {
            bool isAnswerCorrect = _imageDrops[i].GetComponent<Drop>().CheckNumber();
            if (isAnswerCorrect)
                _correctAnswers++;

        }

        _statText.text = _correctAnswers.ToString() + " / " + _imageDrops.Length + " Correct";
        _timerStatText.text = _counterText.text + " Seconds";
        _timerOn = false;

        for(int i = 0; i < _spawnSlots.Length ;i++)
        {
            _spawnSlots[i].GetComponent<Image>().sprite = _spawnSlots[i].GetComponent<NumberValues>().RevealImageValue();
        }
        

        if (_correctAnswers > PlayerPrefs.GetInt("User" + _player + "HighestCorrectAmount"))
        {
            PlayerPrefs.SetInt("User" + _player + "HighestCorrectAmount", _correctAnswers);
            PlayerPrefs.SetFloat("User" + _player + "TimeForHighestCorrect", _countdownTimer);
        }

    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("DraggableGame");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame(int value)
    {
        Time.timeScale = value;
    }

}
