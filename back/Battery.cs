// Состояние батареи
enum ChargeState{
    // Простаивает
    IDLE,
    // Работает
    WORK,
    // Заряжается
    CHARGING,
}

// Класс батарея
class Battery{
    public float maxCharge; // Максимальный заряд батареи в киловатах
    public float curCharge; // Текущий заряд батареи в киловатах
    public ChargeState state;   // Состояние батаре

    // Конструктор батарей
        public Battery(float maxCharge, bool isFull){
        this.maxCharge = maxCharge;
        this.curCharge = (isFull) ? maxCharge : 0;
        this.state = ChargeState.IDLE;
    }

    // Конструктор батарей
    public Battery(float maxCharge, float curCharge){
        this.maxCharge = maxCharge;
        this.curCharge = curCharge;
        this.state = ChargeState.IDLE;
    }

    // Заряд в процентах
    public float getPercentCharge(){
        if (curCharge == 0)
            return 0.0f;
        return (float)maxCharge / (float)curCharge;
    }

    // Добавить заряда, Возвращает true, если зарядился полностью
    public float plusCharge(float charge){
        this.curCharge += charge;
        if (this.curCharge >= this.maxCharge){
            float tmp = this.curCharge - maxCharge;
            this.curCharge = this.maxCharge;
            return tmp;
        }
        return 0.0f;
    }

    // Батарею в стринг
    public String toString(){
        return "Battery: { " + 
            curCharge.ToString() + "/" + maxCharge.ToString() + "; " +
            state.ToString() + " " +
            "}";
    }
}