// Состояние беспилотного аппарата
enum UVSTATE{
    IDLE,       // ПРОСТАИВАЕТ
    WORK,       // РАБОТАЕТ
    GOWORK,     // ДВИГАЕТСЯ НА МЕСТО РАБОТЫ
    GOHOME,     // ДВИЖЕТСЯ ДОМОЙ
    CHARGING,   // ЗАРЯЖАЕТСЯ
}

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

// Геометрическое представление
enum GEOREP {
    SPHERE,     // Сфера
}

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

// Класс беспилотника
abstract class UV{
    public List<PAYLOAD> payloadList;
    public UVSTATE state;
    public GEOREP representation;
    public Battery battery;
    public float weightGrams;
    public float maxSpeed;
}

// Класс беспилотный летательный аппарат
class UVA : UV {
    public int diameterSM;          // Диаметр в сантиметрах
    public float speedHorizontal;   // Скорость Горизонтальная
    public float speedUp;   // Скорость Взлёт
    public float speedDown; // Скорость Посадка


}

// Центр принятия решений
class DotCenter{
    // Поля:
    // Состав активной группы
    // Список задач
    
    // Действия:
    // Обеспечивать координацию участников
    // Распределять задачи
    // Принимать решения
    // Перераспределять задачи между участниками роя при изменении состава активной группы

    // Входные данные:
    // 1 - Миссия

    // Выходные данные:
    // 1 - Динамически разбить миссию на отдельные задачи между участниками роя
    // 

}

// Перечисления типа миссии по времени
enum MTime {
    WAITING_END,    // Миссии с ожиданием завершения
    PERIOD,         // Переодические миссии
    NO_END,         // Непрерывные миссии
}

// Перечисление типа миссии по перемещению
enum MMove {
    ROUTE_FOLLOWING,    // Следовать маршруту
    TERRITORY_COVERAGE, // Сплошное покрытие территории
}

// Класс описывающий миссию
class Mission{
    public MTime time;
    public MMove move;

}

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

    // Добавить батарею на зарядку
    public Boolean startCharging(Battery battery){
        Console.WriteLine("Try add to charging");

        curBlock.WaitOne();
        // Все зарядки заняты ли?
        if (chargingBatteryCount >= maxUVCount){
            Console.WriteLine("Charging is fail");
            return false;
        }

        // При попытке добавить новую батарею мы проверяем 
        // есть ли смысл распределять весь оставшийся заряд
        // на n батареи и их хватит (они все будут полными)
        // или же мы получим несколько неполных батарей 
        if (waitingChargeInEnd <= 0){
            Console.WriteLine("Charging is fail");
            return false;
        }

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


        Console.WriteLine("Charging is succefull");
        return true;
    }

    // Заряжать до ограничителя chargingUntilValue
    private void chargingUntil(object battery){
        
        Console.WriteLine("Start Charging");

        Battery bat = (Battery) battery;
        float start = 0.0f;
        
        // Эмитация зарядки
        do{
            curBlock.WaitOne();

            float loadVal = (chargingUntilValue - start > chargingKiloWatInMin)?chargingKiloWatInMin : chargingUntilValue - start;
            Thread.Sleep(MINUTE);

            // В станции закончилось электричество
            if (this.curCharge <= 0){
                Console.WriteLine("The Station is empty.");
                curBlock.ReleaseMutex();
                break;
            }
            
            bat.plusCharge(loadVal);
            start+=loadVal;
            this.curCharge -= loadVal;

            curBlock.ReleaseMutex();
            Console.WriteLine(bat.toString());
            Console.WriteLine(this.toString());


        } while(start < chargingUntilValue);
        curBlock.WaitOne();
        
        Console.WriteLine("Stop Charging");
        bat.state = ChargeState.IDLE;
        chargingBatteryCount--;
        curBlock.ReleaseMutex();

        Console.WriteLine(bat.toString());
        Console.WriteLine(this.toString());
    }

    // Зарядка до определённого размера
    private void charging(object battery){
        Console.WriteLine("Start Charging");

        Battery bat = (Battery) battery;
        float tmpWats = 0.0f;
        
        // Эмитация зарядки
        do{
            Thread.Sleep(MINUTE);

            // В станции закончилось электричество
            curBlock.WaitOne();
            if (this.curCharge <= 0){
                Console.WriteLine("The Station is empty.");
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
            Console.WriteLine(bat.toString());
            Console.WriteLine(this.toString());

        } while((tmpWats = bat.plusCharge(tmpWats)) == 0.0f);
        curBlock.WaitOne();
        this.curCharge += tmpWats;
        
        Console.WriteLine("Stop Charging");
        bat.state = ChargeState.IDLE;
        chargingBatteryCount--;
        curBlock.ReleaseMutex();

        Console.WriteLine(bat.toString());
        Console.WriteLine(this.toString());
    }

    // Возвращает статус строкой
    public String toString(){
        return "Station has " + curCharge.ToString() + "/" + maxCharge.ToString() + ". On station charging " + chargingBatteryCount.ToString() + "/" + maxUVCount.ToString() + ".";
    }
}

// Станция управления
class ServiceStation {
    // ПОЛЯ:
    
    // Общий список аккамуляторных батарей на станции
    private List<Battery> batteryList;

    // Общий список миссий
    private List<Mission> missionList;
    
    // Общий список Беспилотных Летательных Аппаратов
    private List<UVA> uvaList;

    // Конструктор для станции
    public ServiceStation(
        List<Battery> batteryList,
        List<Mission> missionList,
        List<UVA> uvaList
    ){
        this.batteryList = batteryList;
        this.missionList = missionList;
        this.uvaList = uvaList;
    }

    // Добавить батарею
    public void addBattery(Battery battery){
        this.batteryList.Add(battery);
    }

    // Удалить батарею
    public void deleteBattery(Battery battery){
        this.batteryList.Add(battery);
    }

    // Добавить миссию
    public void addMission(Mission mission){
        this.missionList.Add(mission);
        // TODO: Перераспределять задачи между участниками роя
    }

    // Удалить миссию
    public void deleteMission(Mission mission){
        this.missionList.Remove(mission);
        // TODO: Перераспределять задачи между участниками роя
    }

    // Добавить беспилотный летательный аппарат
    public void addUVA(UVA uva){
        this.uvaList.Add(uva);
        // TODO: Перераспределять задачи между участниками роя
    }

    // Удалить беспилотный летательный аппарат
    public void deleteUVA(UVA uva){
        this.uvaList.Remove(uva);
        // TODO: Перераспределять задачи между участниками роя
    }

    // Общее количество расходной полезной нагрузки на станции
    // Время на замену аккамулятора
    // Время на зарядку аккамулятора
    // Время восполнения ресурса расходной полезной нагрзуки

    // Замена аккамуляторов
    // Обновление ресурса расходной полезной нагрузки
    // Смена полезной нагрузки
    
}