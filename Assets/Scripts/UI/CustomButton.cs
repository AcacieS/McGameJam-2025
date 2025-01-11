using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomButton : MonoBehaviour, IPointerClickHandler
{
    public Action onClickEvent;
    private bool active = true;

    
    public void setActive(bool active)
    {
        this.active = active;
    }
        
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!active) return;
        Debug.Log(gameObject.name + " clicked; invoking event");
        onClickEvent?.Invoke();
    }
}
