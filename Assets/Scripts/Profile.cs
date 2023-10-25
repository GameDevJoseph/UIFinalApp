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

    public string _selectedUser;

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
        _profiles[0].text = _nameFields[0].text;
        _nameFields[0].gameObject.SetActive(false);
        _users[0] = _nameFields[0].text;

        PlayerPrefs.SetString("User1", _users[0]); ;
        PlayerPrefs.Save();
    }

    public void EnterSlotTwo()
    {
        _profiles[1].text = _nameFields[1].text;
        _nameFields[1].gameObject.SetActive(false);
        _users[1] = _nameFields[1].text;

        PlayerPrefs.SetString("User2", _users[1]);
        PlayerPrefs.Save();
    }

    public void EnterSlotThree()
    {
        _profiles[2].text = _nameFields[2].text;
        _nameFields[2].gameObject.SetActive(false);
        _users[2] = _nameFields[2].text;

        PlayerPrefs.SetString("User3", _users[2]);
        PlayerPrefs.Save();
    }

    public void SelectUserOne()
    {
        _selectedUser = _profiles[0].text;
        _userNameDisplay.text = _selectedUser;

        
        PlayerPrefs.SetString("CurrentUser", _selectedUser);
        PlayerPrefs.Save();

        _GameStats[0].text = PlayerPrefs.GetInt("User" + _selectedUser + "HighestCorrectAmount").ToString() + " Correct " +
        PlayerPrefs.GetString("User" + _selectedUser + "TimeForHighestCorrect") + " sec is highest correct time";

        _GameStats[1].text = PlayerPrefs.GetString("User" + _selectedUser + "FastestTime") + " Is your fastest time";

        
    }

    public void SelectUserTwo()
    {
        _selectedUser = _profiles[1].text;
        _userNameDisplay.text = _selectedUser;

        PlayerPrefs.SetString("CurrentUser", _selectedUser);
        PlayerPrefs.Save();

        _GameStats[0].text = PlayerPrefs.GetInt("User" + _selectedUser + "HighestCorrectAmount").ToString() + " Correct " +
        PlayerPrefs.GetFloat("User" + _selectedUser + "TimeForHighestCorrect") + " sec is highest correct time";
    }

    public void SelectUserThree()
    {
        _selectedUser = _profiles[2].text;
        _userNameDisplay.text = _selectedUser;

        PlayerPrefs.SetString("CurrentUser", _selectedUser);
        PlayerPrefs.Save();

        _GameStats[0].text = PlayerPrefs.GetInt("User" + _selectedUser + "HighestCorrectAmount").ToString() + " Correct " +
        PlayerPrefs.GetFloat("User" + _selectedUser + "TimeForHighestCorrect") + " sec is highest correct time";
    }

    [ContextMenu("Delete All")]
    public void DeleteKeys()
    {
        PlayerPrefs.DeleteAll();
    }

}
