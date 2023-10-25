using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchGameManager : MonoBehaviour
{
    [SerializeField] MatchID[] _matchs;

    [SerializeField] int _score;

    [SerializeField] Sprite[] _matchSprites;

    [SerializeField] MatchID[] _randomMatch;

    bool _isCheckingMatch = false;

    private void Start()
    {
        _matchs = new MatchID[2];
    }

    public void Check(MatchID match)
    {
        if (_matchs[0] == null)
        {
            _matchs[0] = match;
            match.GetComponent<Image>().sprite = _matchSprites[match.MatchIDNumber];
        }
        else if (_matchs[0] != null && _matchs[1] == null)
        {
            _matchs[1] = match;
            match.GetComponent<Image>().sprite = _matchSprites[match.MatchIDNumber];
            CheckCorrectMatch();
        }

        
    }


    public void CheckCorrectMatch()
    {
        if (_matchs[0] == null && _matchs[1] == null)
            return;

        if (_matchs[0].MatchIDNumber == _matchs[1].MatchIDNumber)
        {
            foreach (MatchID match in _matchs)
            {
                match.GetComponent<Image>().raycastTarget = false;
            }
            _score += 1;
            ResetCheck();
            
        }else
        {
            _isCheckingMatch = true;
            StartCoroutine(WrongMatchFlip());   
        }
        
    }

    IEnumerator WrongMatchFlip()
    {
        while (_isCheckingMatch)
        {
            yield return new WaitForSeconds(1f);
            foreach (MatchID match in _matchs)
            {
                match.GetComponent<Image>().sprite = _matchSprites[16];
            }
            _isCheckingMatch = false;
            ResetCheck();
        }
    }

    void ResetCheck()
    {
        _matchs[0] = null;
        _matchs[1] = null;
    }

}


