using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSave : MonoBehaviour
{
    [SerializeField] Settings _settings;

    private void Start()
    {
        _settings.LoadBGVolumeValue();
        _settings.LoadSFXVolumeValue();
    }
}
