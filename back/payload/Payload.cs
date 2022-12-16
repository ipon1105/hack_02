// Тип полезной нагрузки
enum PAYLOAD{
    NONE,                       // Нет полезной нагрузки
    SPRAYING,                   // Система опрыскивания
    SPREADING,                  // Система разброса
    LIDAR_3D,                   // 3D LIDAR
    HIGH_DEFINITION_CAMERA,     // Фотокамера высокого разрешения
    GIMBAL_VIDEO_CAMERA,        // Видеокамера с подвесом
    LOAD_SUSPENSION,            // Подвес груза
    GROUND_PRESENTAION_RADAR,   // Георадар
}

// Тип потребления зарядки
enum BUILT{
    BUILT_IN,    // Потребление от встроенного аккамулятора
    BUILT_OUT,   // Потребление от внешнего аккамулятора
}

// Класс описывающий полезную нагрузку
abstract class Payload{
    // Состояние полезной нагрузки
    protected PAYLOAD state;
    // Способ отрисовки
    protected GEOREP representation;
    // Тип потребления электричества
    protected BUILT built;
    // Батарея 
    protected Battery battery;
    // Масса полезной нагрузки в граммах без ресурса
    protected float weight;
}

// Система опрыскивания
class Spraying : Payload{
    private int second = 1000/60;
    // Максимальная вместимость в весе
    private float maxCapacity;
    // Текущий вес ресурса полезной нагрузки
    private float curCapacity;
    // Расход полезной нагрузки в секунду
    private float ratePerSecond;
    // Приоритетная высота распыления над уровнем замли
    private float sprayHeight;
    // Эмитация работы
    private Thread payloadWork;
    // Расход электричесва киловат в минуту
    private float consumptionPerMinute;
    
    // Конструктор
    public Spraying(float weight, float maxCapacity, float curCapacity, float ratePerSecond, float sprayHeight, float consumptionPerMinute, Battery battery){
        this.state = PAYLOAD.SPRAYING;
        this.representation = GEOREP.SPHERE;

        this.weight = weight;
        this.maxCapacity = maxCapacity;
        this.curCapacity = curCapacity;
        this.sprayHeight = sprayHeight;
        this.ratePerSecond = ratePerSecond;
        this.battery = battery;

        if (battery != null && battery.curCharge != 0)
            this.built = BUILT.BUILT_IN;
        else 
            this.built = BUILT.BUILT_OUT;

        payloadWork = new Thread(work);
    }
    
    // Начать распыление
    public void startSpraying(){
        this.battery.state = ChargeState.WORK;
        payloadWork.Start();
    }

    // Остановить распыление
    public void stopSpraying(){
        payloadWork.Abort();
        this.battery.state = ChargeState.IDLE;
    }

    // Вернуть максимальное время работы в секундах
    public float getWorkingTime(){
        return curCapacity / ratePerSecond;
    }

    // Необходимая высота распыления над уровнем земли
    public float getSprayHeight(){
        return sprayHeight;
    }

    // Эмитация работы
    private void work(){
        int minute = 0;
        while(true){

            if (this.built == BUILT.BUILT_IN && minute == 60){
                if (battery.curCharge - consumptionPerMinute < 200){
                    this.built = BUILT.BUILT_OUT;
                } else
                    this.battery.curCharge -= consumptionPerMinute;
                minute = 0;
            }

            this.curCapacity -= ratePerSecond;
            Thread.Sleep(second);
            minute++;
        }
    }

    // Вернуть состояние в строке
    public String toString(){
        return "Spraying{ " +
            this.battery.toString() + " | " + consumptionPerMinute + "; " +
            curCapacity + "/" + maxCapacity + " | " + ratePerSecond + "; " +
            built + ";" +
            getCurrentWeight() + 
            " }";
    }

    // Вернуть текущий вес
    public float getCurrentWeight(){
        return this.weight + this.curCapacity; 
    }

    // Вернуть потребление киловат в минуты 
    public float getConsumptionPerMinute(){
        return (this.built == BUILT.BUILT_OUT) ? consumptionPerMinute : 0.0f;
    }
}
