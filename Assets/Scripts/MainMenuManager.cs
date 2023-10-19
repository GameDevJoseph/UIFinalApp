using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Settings _settings;
    

    public void LoadLevel(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }

    private void Start()
    {
        _settings.LoadBGVolumeValue();
        _settings.LoadSFXVolumeValue();
    }
}
