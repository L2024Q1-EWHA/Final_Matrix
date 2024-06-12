using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MiniGameButtonIPointer : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private MiniGameControl miniGameControl;
    public void OnPointerDown(PointerEventData eventData)
    {
        miniGameControl.StopFire();
    }

}
