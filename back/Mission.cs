
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

// Приоритеты
enum Priority {
    LOW, // Низкий
    MIDDLE, // Средний
    HIGHT, // Высокий
    SUPER_HIGHT, // Наивысший
}

// Класс описывающий миссию
class Mission{
    // Тип миссии по времени
    private MTime time;
    // Тип миссии по перемещению
    private MMove move;
    // Необходимый тип полезной нагрузки
    private PAYLOAD payload;
    // Список простейших команд, из которых состоит миссия
    private List<Operation> operationList; 
    // Максимальное количество дронов на миссию
    private int maxUvaCount; 
    // Текущее количество дронов на миссию
    private int curUvaCount; 
    // Приоритет мисси
    private Priority priority;
    
    // Конструктор
    public Mission(MTime time, MMove move, PAYLOAD payload, int maxUvaCount,Priority priority){
        this.time = time;
        this.move = move;
        this.payload = payload;
        this.maxUvaCount = maxUvaCount;
        this.curUvaCount = 0;
        this.priority = priority;
    }

    // Установить список операций
    public void setOperationList(List<Operation> operationList){
        this.operationList = operationList;
    }

    // Миссию вернуть строкой
    public String toString(){

        String operationListResult = "";
        foreach (Operation a in operationList){
            switch(a.getState()){
                case OType.Move: 
                    operationListResult += ((Route) a).toString();
                break;
            }
        }

        return "Mission{"+
            time + "; " +
            move + "; " +
            payload + "; " +
            operationListResult + " " +
            "}";
    }
}

// Тип операции
enum OType{
    Move, // Операция передвижения
    
}

// Класс простейшей операции
class Operation {
    // Тип операции
    protected OType state;

    // Возвращает состояние
    public OType getState(){
        return state;
    }

    // В строку
    public String toString(){
        return "Operation{"+
            state + " " +
        "}";
    }
}

// Класс описывающий множество точек по которым нужно следовать
class Route : Operation {
    // Список точек следования
    private List<Point> routeList;

    // Конструктор
    public Route(List<Point> routeList){
        this.routeList = routeList;
        this.state = OType.Move;
    }

    // Возвращает список координат следования
    public List<Point> getRouteList(){
        return routeList;
    }

    // В строку
    public String toString(){

        String routeListResult = "List{";
        foreach (Point tmp in routeList){
            routeListResult += tmp.toString() + " ";
        }
        routeListResult += "}";

        return "Route{"+
            routeListResult + "; " +
            ((Operation) this).toString() + " " +
        "}";
    }
}