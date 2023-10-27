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
    [SerializeField] GameObject[] _deleteButton;
    [SerializeField] GameObject _deletePanel;
    [SerializeField] TMP_Text _deleteConfirmationText;

    string[] _users;
    int _deleteSlotValue;

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
            else if (_users[i] == "")
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
        _profiles[0].text = _users[0];

        _users[1] = PlayerPrefs.GetString("User2");
        _profiles[1].text = _users[1];

        _users[2] = PlayerPrefs.GetString("User3");
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
        _deleteButton[0].SetActive(true);

        PlayerPrefs.SetString("CurrentUser", _selectedUser);
        PlayerPrefs.Save();

        AcquireStats();

    }

    public void SelectUserTwo()
    {
        _selectedUser = _profiles[1].text;
        _userNameDisplay.text = _selectedUser;

        PlayerPrefs.SetString("CurrentUser", _selectedUser);
        PlayerPrefs.Save();

        AcquireStats();
    }

    public void SelectUserThree()
    {
        _selectedUser = _profiles[2].text;
        _userNameDisplay.text = _selectedUser;

        PlayerPrefs.SetString("CurrentUser", _selectedUser);
        PlayerPrefs.Save();

        AcquireStats();
    }

    [ContextMenu("Delete All")]
    public void DeleteKeys()
    {
        PlayerPrefs.DeleteAll();
    }

    void AcquireStats()
    {
        _GameStats[0].text = PlayerPrefs.GetInt("User" + _selectedUser + "HighestCorrectAmount").ToString() + " Correct " +
        PlayerPrefs.GetFloat("User" + _selectedUser + "TimeForHighestCorrect").ToString("F2") + " sec is highest correct time";

        _GameStats[1].text = PlayerPrefs.GetFloat("User" + _selectedUser + "FastestTime").ToString("F2") + " Is your fastest time";

        _GameStats[2].text = PlayerPrefs.GetInt("User" + _selectedUser + "MostCorrect").ToString() + " answered correctly in 2 mins";
    }

    

    public void DeleteUser()
    {

        _profiles[_deleteSlotValue].transform.parent.gameObject.SetActive(false);
        _profiles[_deleteSlotValue].text = "";
        _nameFields[_deleteSlotValue].gameObject.SetActive(true);
        _nameFields[_deleteSlotValue].text = "Enter Name...";


        PlayerPrefs.DeleteKey("User" + _users[_deleteSlotValue] + "HighestCorrectAmount");
        PlayerPrefs.DeleteKey("User" + _users[_deleteSlotValue] + "TimeForHighestCorrect");
        PlayerPrefs.DeleteKey("User" + _users[_deleteSlotValue] + "FastestTime");
        PlayerPrefs.DeleteKey("User" + _users[_deleteSlotValue] + "MostCorrect");
        PlayerPrefs.DeleteKey("User" + (_deleteSlotValue + 1));
        _users[_deleteSlotValue] = null;
        _selectedUser = string.Empty;
        PlayerPrefs.SetString("CurrentUser", _selectedUser);
        _userNameDisplay.text = "";
        AcquireStats();
        PlayerPrefs.Save();

    }


    public void EnableDeletePanel(int value)
    {
        _deleteConfirmationText.text = "Are you sure you want to delete the profile " + _users[value];
        _deletePanel.SetActive(true);
        _deleteSlotValue = value;
    }

    public void DisableDeletePanel()
    {
        _deleteConfirmationText.text = "";
        _deletePanel.SetActive(false);

    }
}
