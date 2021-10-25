public interface IRepairable {
    void LoseDurability(int amount);
    void GainDurability(bool max = true, int amount = 0);
}
