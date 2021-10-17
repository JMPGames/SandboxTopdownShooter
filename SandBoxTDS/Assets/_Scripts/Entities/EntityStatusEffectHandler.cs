using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatusEffectHandler : MonoBehaviour {
    List<StatusEffect> StatusEffects { get; set; }

    public bool CheckForStatusEffectByTitle(string title) {
        if (StatusEffects.Count <= 0) {
            return false;
        }

        for (int i = 0; i < StatusEffects.Count; i++) {
            if (StatusEffects[i].Title == title) {
                return true;
            }
        }
        return false;
    }
}
