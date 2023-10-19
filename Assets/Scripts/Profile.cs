using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Profile : MonoBehaviour
{
    [SerializeField] TMP_InputField[] _nameFields;
    [SerializeField] TMP_Text[] _profiles;
    [SerializeField] TMP_Text _userNameDisplay;
    [SerializeField] TMP_Text[] _Game1Stats;

    string _user1;
    string _user2;
    string _user3;


    private void Start()
    {
        Load();
        _userNameDisplay.text = _selectedUser;
    }

    public string _selectedUser;

    public void Load()
    {
        _selectedUser = PlayerPrefs.GetString("CurrentUser");
        _user1 = PlayerPrefs.GetString("User1");
        _user2 = PlayerPrefs.GetString("User2");
        _user3 = PlayerPrefs.GetString("User3");

        _profiles[0].text = _user1;
        _profiles[1].text = _user2;
        _profiles[2].text = _user3;

        _Game1Stats[0].text = PlayerPrefs.GetInt("User" + _selectedUser + "HighestCorrectAmount").ToString() + "Correct " +
        PlayerPrefs.GetFloat("User" + _selectedUser + "TimeForHighestCorrect") + " sec is highest correct time";
    }
    
    public void EnterSlotOne()
    {
        _profiles[0].text = _nameFields[0].text;
        _nameFields[0].gameObject.SetActive(false);
        _user1 = _nameFields[0].text;

        PlayerPrefs.SetString("User1", _user1); ;
        PlayerPrefs.Save();
    }

    public void EnterSlotTwo()
    {
        _profiles[1].text = _nameFields[1].text;
        _nameFields[1].gameObject.SetActive(false);
        _user2 = _nameFields[1].text;

        PlayerPrefs.SetString("User2", _user2);
        PlayerPrefs.Save();
    }

    public void EnterSlotThree()
    {
        _profiles[2].text = _nameFields[2].text;
        _nameFields[2].gameObject.SetActive(false);
        _user3 = _nameFields[2].text;

        PlayerPrefs.SetString("User3", _user3);
        PlayerPrefs.Save();
    }

    public void SelectUserOne()
    {
        _selectedUser = _profiles[0].text;
        _userNameDisplay.text = _selectedUser;

        
        PlayerPrefs.SetString("CurrentUser", _selectedUser);
        PlayerPrefs.Save();
    }

    public void SelectUserTwo()
    {
        _selectedUser = _profiles[1].text;
        _userNameDisplay.text = _selectedUser;

        PlayerPrefs.SetString("CurrentUser", _selectedUser);
        PlayerPrefs.Save();
    }

    public void SelectUserThree()
    {
        _selectedUser = _profiles[2].text;
        _userNameDisplay.text = _selectedUser;

        PlayerPrefs.SetString("CurrentUser", _selectedUser);
        PlayerPrefs.Save();
    }

    [ContextMenu("Delete All")]
    public void DeleteKeys()
    {
        PlayerPrefs.DeleteAll();
    }

}
