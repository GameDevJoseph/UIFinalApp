using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MatchGameManager : MonoBehaviour
{
    [SerializeField] MatchID[] _matches;

    [SerializeField] Sprite[] _matchSprites;

    [SerializeField] TMP_Text _timerText;

    [SerializeField] List<GameObject> _spawnPrefabs = new List<GameObject>();

    [SerializeField] Transform _parentGrid;

    [SerializeField] AudioSource _sfxSource;

    [SerializeField] AudioClip _selectAudio;

    [SerializeField] AudioClip _correctAudio;

    [SerializeField] AudioClip _incorrectAudio;

    bool _isCheckingMatch = false;
    float _countdownTimer;
    int _minutes;
    int _matchesLeft = 16;
    string _player;

    bool _isAllMatched = false;

    private void Start()
    {
        _player = PlayerPrefs.GetString("CurrentUser");
        _timerText.text = Mathf.FloorToInt(_countdownTimer % 60).ToString();
        _matches = new MatchID[2];
        for(int i = 0; i < 32; i++)
        {
            int randomNumber = Random.Range(0, _spawnPrefabs.Count);
            var match = Instantiate(_spawnPrefabs[randomNumber], transform.position, transform.rotation);
            match.transform.parent = _parentGrid.transform;
            _spawnPrefabs.Remove(_spawnPrefabs[randomNumber]);
        }
    }

    public void Check(MatchID match)
    {
        if (_matches[0] == null)
        {
            _matches[0] = match;
            match.GetComponent<Image>().sprite = _matchSprites[match.MatchIDNumber];
        }
        else if (_matches[0] != null && _matches[1] == null)
        {
            _matches[1] = match;
            match.GetComponent<Image>().sprite = _matchSprites[match.MatchIDNumber];
            CheckCorrectMatch();
        }
        _sfxSource.PlayOneShot(_selectAudio);
    }

    private void Update()
    {

        if (_isAllMatched)
            return;

        _countdownTimer += Time.deltaTime;

        if (_countdownTimer > 59)
        {
            _countdownTimer = 0;
            _minutes += 1;
        }

        _timerText.text = _minutes + ":" + Mathf.FloorToInt(_countdownTimer % 60).ToString();
    }

    public void CheckCorrectMatch()
    {
        if (_matches[0] == null && _matches[1] == null)
            return;

        if (_matches[0].MatchIDNumber == _matches[1].MatchIDNumber)
        {
            foreach (MatchID match in _matches)
            {
                match.GetComponent<Image>().raycastTarget = false;
            }
            _sfxSource.PlayOneShot(_correctAudio);
            _matchesLeft -= 1;
            ResetCheck();
            CheckIfAllFlipped();
            
        }else
        {
            _isCheckingMatch = true;
            _sfxSource.PlayOneShot(_incorrectAudio);
            StartCoroutine(WrongMatchFlip());   
        }
        
    }

    IEnumerator WrongMatchFlip()
    {
        while (_isCheckingMatch)
        {
            yield return new WaitForSeconds(1f);
            foreach (MatchID match in _matches)
            {
                match.GetComponent<Image>().sprite = _matchSprites[16];
            }
            _isCheckingMatch = false;
            ResetCheck();
        }
    }

    void ResetCheck()
    {
        _matches[0] = null;
        _matches[1] = null;
    }

    void CheckIfAllFlipped()
    {
        if(_matchesLeft <= 0)
        {
            _isAllMatched = true;
            PlayerPrefs.SetString("User" + _player + "FastestTime", _timerText.text);
        }
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseGame(int value)
    {
        Time.timeScale = value;
    }
    
}


