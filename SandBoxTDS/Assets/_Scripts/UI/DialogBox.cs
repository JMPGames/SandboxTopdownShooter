using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour {
    public static DialogBox instance;

    [SerializeField] Image icon;
    [SerializeField] Text titleText;
    [SerializeField] Text dialogText;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public void Open(Sprite icon, string title, string dialog) {
        this.icon.sprite = icon;
        titleText.text = title;
        dialogText.text = dialog;
        gameObject.SetActive(true);
    }

    public void Change(Sprite icon, string dialog) {
        this.icon.sprite = icon;
        dialogText.text = dialog;
    }

    public void Close() {
        gameObject.SetActive(false);
    }
}
