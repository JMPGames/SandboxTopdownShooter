using System.Collections.Generic;

public interface IEffectable {
    List<StatusEffect> StatusEffects { get; set; }
    void AddStatusEffect(StatusEffect effect);
    void RemoveStatusEffect(StatusEffect effect);
    bool CheckForStatusEffectByTitle(string title);
}
