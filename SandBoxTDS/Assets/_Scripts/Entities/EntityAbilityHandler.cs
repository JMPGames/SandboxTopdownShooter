using UnityEngine;

public class EntityAbilityHandler : MonoBehaviour {
    [SerializeField] Ability[] abilities = new Ability[4];

    public void AddAbility(Ability a, int slot) {
        abilities[slot] = a;
    }

    public Ability GetAbilityAtSlot(int slot) {
        return abilities[slot];
    }

    public void RemoveAbility(int slot) {
        abilities[slot] = null;
    }

    public bool UseAbility(int slot) {
        if (abilities[slot] != null && abilities[slot].Usable()) {
            abilities[slot].Use();
            return true;
        }
        return false;
    }
}
