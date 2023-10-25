using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Profile : MonoBehaviour
{
    [SerializeField] TMP_InputField[] _nameFields;
    [SerializeField] TMP_Text[] _profiles;
    [SerializeField] TMP_Text _userNameDisplay;
    [SerializeField] TMP_Text[] _GameStats;

    [SerializeField] TMP_Text _debugText;

    string[] _users;


    private void Start()
    {
        _users = new string[3];
        Load();
        for (int i = 0; i < _profiles.Length; i++)
        {
                if (_users[i] != string.Empty)
                {
                    _nameFields[i].gameObject.SetActive(false);
                    _profiles[i].transform.parent.gameObject.SetActive(true);
                }
                else if(_users[i] == string.Empty)
                {
                    _nameFields[i].gameObject.SetActive(true);
                    _profiles[i].transform.parent.gameObject.SetActive(false);
                }
        }
        _userNameDisplay.text = _selectedUser;
    }

    string _selectedUser;

    public string SelectedUser { get { return _selectedUser; } }    

    public void Load()
    {
        _selectedUser = PlayerPrefs.GetString("CurrentUser");
        _users[0] = PlayerPrefs.GetString("User1");
        _users[1] = PlayerPrefs.GetString("User2");
        _users[2] = PlayerPrefs.GetString("User3");

        _profiles[0].text = _users[0];
        _profiles[1].text = _users[1];
        _profiles[2].text = _users[2];

        
    }
    
    public void EnterSlotOne()
    {
        if (_nameFields[0].text != string.Empty)
        {

            if (_nameFields[0].text == _profiles[1].text || _nameFields[0].text == _profiles[2].text)
            {
                _debugText.text = "Profile already exist";
                return;
            }
            else
            {
                _debugText.text = "";

                _profiles[0].text = _nameFields[0].text;
                _nameFields[0].gameObject.SetActive(false);
                _users[0] = _nameFields[0].text;

                _nameFields[0].gameObject.SetActive(false);
                _profiles[0].transform.parent.gameObject.SetActive(true);

                PlayerPrefs.SetString("User1", _users[0]);
                PlayerPrefs.Save();
            }
        }
    }

    public void EnterSlotTwo()
    {
        if (_nameFields[1].text != string.Empty)
        {
            if (_nameFields[1].text == _profiles[2].text || _nameFields[1].text == _profiles[0].text)
            {
                _debugText.text = "Profile already exist";
                return;
            }
            else
            {
                _debugText.text = "";
                _profiles[1].text = _nameFields[1].text;
                _nameFields[1].gameObject.SetActive(false);
                _users[1] = _nameFields[1].text;

                _nameFields[1].gameObject.SetActive(false);
                _profiles[1].transform.parent.gameObject.SetActive(true);

                PlayerPrefs.SetString("User2", _users[1]);
                PlayerPrefs.Save();
            }
        }
    }

    public void EnterSlotThree()
    {
        if (_nameFields[1].text != string.Empty)
        {
            if (_nameFields[2].text == _profiles[1].text || _nameFields[2].text == _profiles[0].text)
            {
                _debugText.text = "Profile already exist";
                return;
            }
            else
            {
                _debugText.text = "";
                _profiles[2].text = _nameFields[2].text;
                _nameFields[2].gameObject.SetActive(false);
                _users[2] = _nameFields[2].text;

                _nameFields[2].gameObject.SetActive(false);
                _profiles[2].transform.parent.gameObject.SetActive(true);

                PlayerPrefs.SetString("User3", _users[2]);
                PlayerPrefs.Save();
            }
        }
    }

    public void SelectUserOne()
    {
        _selectedUser = _profiles[0].text;
        _userNameDisplay.text = _selectedUser;

        
        PlayerPrefs.SetString("CurrentUser", _selectedUser);
        PlayerPrefs.Save();

        AcquireStats(0);
        
    }

    public void SelectUserTwo()
    {
        _selectedUser = _profiles[1].text;
        _userNameDisplay.text = _selectedUser;

        PlayerPrefs.SetString("CurrentUser", _selectedUser);
        PlayerPrefs.Save();

        AcquireStats(1);
    }

    public void SelectUserThree()
    {
        _selectedUser = _profiles[2].text;
        _userNameDisplay.text = _selectedUser;

        PlayerPrefs.SetString("CurrentUser", _selectedUser);
        PlayerPrefs.Save();

        AcquireStats(2);
    }

    [ContextMenu("Delete All")]
    public void DeleteKeys()
    {
        PlayerPrefs.DeleteAll();
    }

    void AcquireStats(int userNumber)
    {
        _GameStats[userNumber].text = PlayerPrefs.GetInt("User" + _selectedUser + "HighestCorrectAmount").ToString() + " Correct " +
        PlayerPrefs.GetString("User" + _selectedUser + "TimeForHighestCorrect") + " is highest correct time";

        _GameStats[1].text = PlayerPrefs.GetString("User" + _selectedUser + "FastestTime") + " Is your fastest time";
    }

}
