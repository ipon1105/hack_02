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

// Класс батарея
class Battery{
    public uint maxCharge; // Максимальный заряд батареи в минутах
    public uint curCharge; // Текущий заряд батареи в минутах

    // Конструктор батарей
    public Battery(uint maxCharge, bool isFull){
        this.maxCharge = maxCharge;
        this.curCharge = (isFull) ? maxCharge : 0;
    }

    // Конструктор батарей
    public Battery(uint maxCharge, uint curCharge){
        this.maxCharge = maxCharge;
        this.curCharge = curCharge;
    }
}

// Модель БПА

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