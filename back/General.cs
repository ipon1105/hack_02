
// Позиция в пространстве
struct Point{
    public double geoLat;
    public double geoLong;
    public double geoAlt;

    public Point(double geoLat, double geoLong, double geoAlt){
        this.geoLat = geoLat;
        this.geoLong = geoLong;
        this.geoAlt = geoAlt;
    }

    // в строку
    public String toString(){
        return "Point { " + 
            "Lat = " + geoLat + "; " +
            "Long = " + geoLong + "; " +
            "Alt = " + geoAlt +
        " }";
    }
}

// Состояние беспилотного аппарата
enum UVSTATE{
    IDLE,       // ПРОСТАИВАЕТ
    WORK,       // РАБОТАЕТ
    GOWORK,     // ДВИГАЕТСЯ НА МЕСТО РАБОТЫ
    GOHOME,     // ДВИЖЕТСЯ ДОМОЙ
    CHARGING,   // ЗАРЯЖАЕТСЯ
}

// Геометрическое представление
enum GEOREP {
    SPHERE,     // Сфера
}

// Класс беспилотника
abstract class UV{
    public Payload payload;
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
    
    public Point position;

    public UVA(){
        
    }

    public UVA(Point position){
        this.position = position;
    }
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

// Вопросы:
// 1 - Так же к модели может быть привязан один или несколько типов полезной нагрузки, которая может заменяться на базовой станции обслуживания.
// Это значит, что у нас есть несколько типов полезной нагрузки на устройстве.

// 