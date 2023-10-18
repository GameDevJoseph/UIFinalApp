using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Randomizer : MonoBehaviour
{
    [SerializeField] GameObject[] _prefabs;
    [SerializeField] List<Image> _gridSlots = new List<Image>();
    [SerializeField] GameObject[] _spawnSlots;
    [SerializeField] Image[] _imageDrops;

    private void Start()
    {
        RandomizeImages();
    }

    public void RandomizeImages()
    {
        for (int i = 0; i < _prefabs.Length; i++)
        {
            var imageNumber = _prefabs[i];
            var randomNumber = Random.Range(0, _gridSlots.Count);
            _spawnSlots[i] = _gridSlots[randomNumber].gameObject;
            var currentSlot = _gridSlots[randomNumber].GetComponent<Image>();
            _spawnSlots[i].GetComponent<Image>().sprite = imageNumber.GetComponent<Image>().sprite;
            _spawnSlots[i].AddComponent<NumberValues>();
            _spawnSlots[i].AddComponent<Draggable>();
            _gridSlots.Remove(currentSlot);
        }

        AssignSlots();
        TurnEmptySlotsOff();
    }

    public void AssignSlots()
    {
        for(int i = 0; i < _spawnSlots.Length; i++)
        {
            _spawnSlots[i].GetComponent<NumberValues>().AssignValue(i + 1);
            _spawnSlots[i].GetComponent<NumberValues>().AssignImageDrop(_imageDrops[i]);
            
        }
    }

    void TurnEmptySlotsOff()
    {
        for(int i = 0; i < _gridSlots.Count; i++)
        {
            _gridSlots[i].GetComponent<Image>();
            _gridSlots[i].gameObject.SetActive(false);
        }
    }
}
