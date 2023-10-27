using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Settings _settings;
    [SerializeField] Profile _profile;
    [SerializeField] Button _playGameButton;

    

    private void Update()
    {
        CheckForUser();
    }

    public void LoadLevel(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }

    private void Start()
    {
        Time.timeScale = 1;
        CheckForUser();
        _settings.LoadBGVolumeValue();
        _settings.LoadSFXVolumeValue();
    }

    void CheckForUser()
    {
        if (_profile.SelectedUser == "")
            _playGameButton.interactable = false;
        else
            _playGameButton.interactable = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
