using System;
using UnityEngine.EventSystems;

public class CustomButton : IPointerClickHandler
{
    public Action onClickEvent;
    private bool active;

    public void setActive(bool active)
    {
        this.active = active;
    }
        
    public void OnPointerClick(PointerEventData eventData)
    {
        onClickEvent?.Invoke();
    }
}
