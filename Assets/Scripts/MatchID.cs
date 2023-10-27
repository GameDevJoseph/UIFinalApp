using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MatchID : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] int _matchID;

    MatchGameManager _gameManager;

    public int MatchIDNumber {get {return _matchID;}}

    void Start()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<MatchGameManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _gameManager.Check(this);
    }

    
}
