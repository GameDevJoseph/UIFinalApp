using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    Image _image;
    [SerializeField] int _number;
    bool _canStack = true;
    int placedNumber;

    public void OnDrop(PointerEventData eventData)
    {
        if (_canStack)
        {
            eventData.pointerDrag.GetComponent<Draggable>().StartingPos = _image.rectTransform.localPosition;
            placedNumber = eventData.pointerDrag.GetComponent<NumberValues>().NumberValue;
            _canStack = false;
        }
    }


    
    void Start()
    {
        _image = GetComponent<Image>();
        _canStack = true;
    }
}
