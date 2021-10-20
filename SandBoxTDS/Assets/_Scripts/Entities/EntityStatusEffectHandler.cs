using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityStatusEffectHandler : MonoBehaviour {
    List<StatusEffect> StatusEffects = new List<StatusEffect>();
    string[] immunities;

    public bool AddStatusEffect(StatusEffect se) {
        if (immunities.Any(i => i == se.Title)) {
            return false;
        }
        StatusEffects.Add(se);
        return true;
    }

    public bool RemoveStatusEffect(StatusEffect se) {
        if (CheckForStatusEffectByTitle(se.Title)) {
            StatusEffects.Remove(se);
            return true;
        }
        return false;
    }

    public bool CheckForStatusEffectByTitle(string title) {        
        if (StatusEffects.Any(se => se.Title == title)) {
            return true;
        }
        return false;
    }
}
