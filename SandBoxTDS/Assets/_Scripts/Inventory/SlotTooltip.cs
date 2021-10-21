using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotTooltip : MonoBehaviour {
    public static SlotTooltip instance;

    [SerializeField] GameObject tooltipPanel;
    [SerializeField] Text tooltipText;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public void ShowTooltip(Vector3 position, Item item) {
        tooltipPanel.transform.position = position;
        //tooltipText.text = item.GetDescription();
        tooltipPanel.SetActive(true);
    }

    public void HideTooltip() {
        tooltipPanel.SetActive(false);
    }
}
