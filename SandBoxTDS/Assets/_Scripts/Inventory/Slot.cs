using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    public Item Item { get; set; }
    public Image Icon { get; set; }
    public bool Empty { get { return Item == null; } }

    public void OnPointerClick(PointerEventData eventData) { }

    public void OnPointerEnter(PointerEventData eventData) {
        //Show tooltip
    }

    public void OnPointerExit(PointerEventData eventData) {
        //Hide tooltip
    }
}
