using UnityEngine;

public enum NPCType { BASIC, SHOP, QUEST }

public class NPCController : Entity, IMobile {
    [SerializeField] NPCType npcType;
    [SerializeField] string[] dialog;

    int dialogOption;
    bool interacting;
    bool inRange;

    void Update() {
        if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space)) && inRange) {
            Interact();
        }
    }

    public virtual void Interact() {
        if (interacting) {
            DialogBox.instance.Change(Icon, dialog[dialogOption]);
        }
        else {
            DialogBox.instance.Open(Icon, Title, dialog[dialogOption]);
            interacting = true;
        }
        DialogOptionIncrement();
    }

    public (Vector3, int) GetMove() {
        throw new System.NotImplementedException();
    }

    void DialogOptionIncrement() {
        dialogOption += 1;
        if (dialogOption >= dialog.Length) {
            dialogOption = 0;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            interacting = false;
            inRange = false;
            DialogBox.instance.Close();
        }
    }
}
