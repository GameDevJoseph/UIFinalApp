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
    [SerializeField] GameObject _statsGameobject;
    [SerializeField] TMP_Text _statsText;

    bool _isCheckingMatch = false;
    float _countTimer;
    int _minutes;
    int _matchesLeft = 16;
    string _player;

    bool _isAllMatched = false;
    bool _hasStarted = false;

    private void Start()
    {
        Time.timeScale = 1;
        _player = PlayerPrefs.GetString("CurrentUser");
        _timerText.text = _countTimer.ToString("F0");
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
        if (!_hasStarted)
            return;

        if (_matches[0] != null && _matches[1] != null)
            return;

        if (_matches[0] == null)
        {
            _matches[0] = match;
            _matches[0].GetComponent<Image>().raycastTarget = false;
            match.GetComponent<Image>().sprite = _matchSprites[match.MatchIDNumber];
        }
        else if (_matches[0] != null && _matches[1] == null)
        {

            _matches[1] = match;
            _matches[1].GetComponent<Image>().raycastTarget = false;
            match.GetComponent<Image>().sprite = _matchSprites[match.MatchIDNumber];
            CheckCorrectMatch();
        }
        _sfxSource.PlayOneShot(_selectAudio);

    }

    private void Update()
    {

        if (_isAllMatched)
            return;

        if (!_hasStarted)
            return;

        _countTimer += Time.deltaTime;



        _timerText.text = _countTimer.ToString("F0");
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
            _matches[0].GetComponent<Image>().raycastTarget = true;
            _matches[1].GetComponent<Image>().raycastTarget = true;
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
            

            if(_countTimer < PlayerPrefs.GetFloat("User" + _player + "FastestTime", _countTimer))
                PlayerPrefs.SetFloat("User" + _player + "FastestTime", _countTimer);

            _statsGameobject.SetActive(true);
            _statsText.text = "You found all the matches in " + _timerText.text;
            PauseGame(0);
        }
    }

    public void LoadButton(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void PauseGame(int value)
    {
        Time.timeScale = value;
    }

    public void StartGame()
    {
        _hasStarted = true;
    }
    
}


