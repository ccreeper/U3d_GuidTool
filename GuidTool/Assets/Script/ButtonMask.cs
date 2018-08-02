using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMask : MonoBehaviour,IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData)
    {
        PassEvent(eventData, ExecuteEvents.pointerClickHandler);
    }

    public void PassEvent<T>(PointerEventData data,ExecuteEvents.EventFunction<T> func) where T:IEventSystemHandler
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(data, results);
        GameObject current = data.pointerCurrentRaycast.gameObject;
        foreach (RaycastResult re in results) {
            if (current != re.gameObject)
                ExecuteEvents.Execute(re.gameObject, data, func);
        }
    }
}
