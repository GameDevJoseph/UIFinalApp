using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Image _image;

    public Vector3 StartingPos { private get ; set; }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        var alpha = _image.color;
        alpha.a = 0.5f;
        _image.color = alpha;
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var alpha = _image.color;
        alpha.a = 1f;
        _image.color = alpha;
        _image.raycastTarget = true;
        ResetPosition();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        StartingPos = _image.rectTransform.localPosition;
    }
    void ResetPosition()
    {
        _image.rectTransform.localPosition = StartingPos;
    }

    public void TurnOffRaycast()
    {
        _image.raycastTarget = false;
    }
}
