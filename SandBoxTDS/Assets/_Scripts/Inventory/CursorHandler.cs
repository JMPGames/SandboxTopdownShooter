using UnityEngine;
using UnityEngine.UI;

public class CursorHandler : MonoBehaviour {
    public static CursorHandler instance;

    public IMoveable HeldObject { get; set; }
    Image icon;
    public Vector3 offset;

    void Awake() {
        if (instance == null) {
            instance = this;
        }        
    }

    void Start() {
        icon = GetComponent<Image>();
    }

    void Update() {
        icon.transform.position = Input.mousePosition + offset;
    }

    public void Grab(IMoveable obj) {
        HeldObject  = obj;
        icon.sprite = obj.Icon;
        icon.color  = Color.white;
    }

    public IMoveable Place() {
        IMoveable tmp = HeldObject;
        Drop();
        return tmp;
    }

    public void Drop() {
        HeldObject = null;
        icon.color = new Color(0, 0, 0, 0);
    }
}
