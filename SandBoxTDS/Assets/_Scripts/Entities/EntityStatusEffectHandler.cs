using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityStatusEffectHandler : MonoBehaviour {
    List<StatusEffect> statusEffects = new List<StatusEffect>();
    [SerializeField] string[] immunities;

    public bool AddStatusEffect(StatusEffect se) {
        if (immunities.Any(i => i == se.Title)) {
            return false;
        }
        statusEffects.Add(se);
        return true;
    }

    public bool RemoveStatusEffect(StatusEffect se) {
        if (CheckForStatusEffectByTitle(se.Title)) {
            statusEffects.Remove(se);
            return true;
        }
        return false;
    }

    public bool CheckForStatusEffectByTitle(string title) {        
        if (statusEffects.Any(se => se.Title == title)) {
            return true;
        }
        return false;
    }
}
