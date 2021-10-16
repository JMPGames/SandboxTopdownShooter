public interface ISellable {
    void Buy(int numberToBuy);
    bool CanAfford(int numberToBuy);
    void Sell(int numberToSell);
}
