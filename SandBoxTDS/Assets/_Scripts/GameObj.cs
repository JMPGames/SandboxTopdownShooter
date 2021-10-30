using UnityEngine;

public class GameObj : MonoBehaviour {
    public int Id { get { return id; } }
    public string Title { get { return title; } }
    public string Description { get { return description; } }
    public Sprite Icon { get { return icon; } }

    [SerializeField] int id;
    [SerializeField] string title;
    [SerializeField] string description;
    [SerializeField] Sprite icon;
}
