// Зарядная станция
class ChargerStation {
    private const int MINUTE = 1000; 
    private Mutex curBlock = new();

    // Ограниченная ли в электричестве станция зарядки
    private Boolean isLimited;
    // Максимальное количество электроэнергии в киловатах
    private float maxCharge;
    // Текущее количество электроэнергии в киловатах
    private float curCharge;
    // Скорость зарядки батареи в киловатах/минуты
    private float chargingKiloWatInMin;
    // Ожидаемое количество заряда после зарядки всех текущих
    private float waitingChargeInEnd;
    // Максимальное количество одновременно заряжаемых устройств
    private uint maxUVCount;
    // Число батарей на зарядке
    private uint chargingBatteryCount;
    //Переменная до какого значения заряжать
    private float chargingUntilValue;

    // Конструктор
    public ChargerStation(Boolean isLimited, float maxCharge, float curCharge, uint maxUVCount, float chargingKiloWatInMin){
        this.isLimited = isLimited;
        this.maxCharge = maxCharge;
        this.curCharge = curCharge;
        this.maxUVCount = maxUVCount;
        this.chargingKiloWatInMin = chargingKiloWatInMin;
        this.waitingChargeInEnd = curCharge;

        chargingBatteryCount = 0;
    }

    // Добавить батарею на зарядку вовращает время в минутах на зарядку устройства
    public float startCharging(Battery battery){

        curBlock.WaitOne();

        // Состояние батареи
        if (battery.curCharge == battery.maxCharge)
            return -1.0f;

        // Все зарядки заняты ли?
        if (chargingBatteryCount >= maxUVCount)
            return -1.0f;
        
        // При попытке добавить новую батарею мы проверяем 
        // есть ли смысл распределять весь оставшийся заряд
        // на n батареи и их хватит (они все будут полными)
        // или же мы получим несколько неполных батарей 
        if (waitingChargeInEnd <= 0)
            return -1.0f;
        
        // Время до зарядки в минутах
        float time = (battery.maxCharge - battery.curCharge) / chargingKiloWatInMin;

        // Вызов устройства на зарядку
        chargingBatteryCount++;
        if (waitingChargeInEnd - (battery.maxCharge - battery.curCharge) < 0){
            battery.state = ChargeState.CHARGING;
            chargingUntilValue = waitingChargeInEnd;

            Thread t = new Thread(new ParameterizedThreadStart(chargingUntil));
            t.Start(battery);

            waitingChargeInEnd = 0;
        } else{
            waitingChargeInEnd -= (battery.maxCharge - battery.curCharge);

            battery.state = ChargeState.CHARGING;
            Thread t = new Thread(new ParameterizedThreadStart(charging));

            t.Start(battery);
        }
        curBlock.ReleaseMutex();

        return time;
    }

    // Заряжать до ограничителя chargingUntilValue
    private void chargingUntil(object battery){
        Battery bat = (Battery) battery;
        float start = 0.0f;
        
        // Эмитация зарядки
        do{
            curBlock.WaitOne();

            float loadVal = (chargingUntilValue - start > chargingKiloWatInMin)?chargingKiloWatInMin : chargingUntilValue - start;
            Thread.Sleep(MINUTE);

            // В станции закончилось электричество
            if (this.curCharge <= 0){
                curBlock.ReleaseMutex();
                break;
            }
            
            bat.plusCharge(loadVal);
            start+=loadVal;
            this.curCharge -= loadVal;

            curBlock.ReleaseMutex();
        } while(start < chargingUntilValue);

        curBlock.WaitOne();

        bat.state = ChargeState.IDLE;
        chargingBatteryCount--;

        curBlock.ReleaseMutex();
    }

    // Зарядка до определённого размера
    private void charging(object battery){
        Battery bat = (Battery) battery;
        float tmpWats = 0.0f;
        
        // Эмитация зарядки
        do{
            Thread.Sleep(MINUTE);

            // В станции закончилось электричество
            curBlock.WaitOne();
            if (this.curCharge <= 0){
                curBlock.ReleaseMutex();
                break;
            }
            
            // Если станция ограничена
            tmpWats = chargingKiloWatInMin;
            if (isLimited) {
                if (this.curCharge - tmpWats < 0){
                    tmpWats = this.curCharge;
                    this.curCharge = 0.0f;
                } else 
                    this.curCharge -= tmpWats;
            }
            curBlock.ReleaseMutex();

        } while((tmpWats = bat.plusCharge(tmpWats)) == 0.0f);

        curBlock.WaitOne();

        if (isLimited)
            this.curCharge += tmpWats;
        bat.state = ChargeState.IDLE;
        chargingBatteryCount--;

        curBlock.ReleaseMutex();
    }

    // Возвращает статус строкой
    public String toString(){
        return "Station has " + curCharge.ToString() + "/" + maxCharge.ToString() + ". On station charging " + chargingBatteryCount.ToString() + "/" + maxUVCount.ToString() + ".";
    }
}
