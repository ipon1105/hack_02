
// testChargingStation();
// testServiceStation();
testMap();

void testMap(){
    Map m = new Map("image.png", 10, 100, 46);
    m.exportToFile("out.xyz");
}

void testServiceStation(){
    List<Battery> batteriList1 = new List<Battery>();
    List<Mission> missionList1 = new List<Mission>();
    List<UVA> uvaList1 = new List<UVA>();

    batteriList1.Add(new Battery(5u, true));
    batteriList1.Add(new Battery(10u, true));
    batteriList1.Add(new Battery(15u, true));
    batteriList1.Add(new Battery(20u, true));

    ServiceStation station1 = new ServiceStation(
        batteriList1, 
        missionList1, 
        uvaList1
    );

    UVA uva1 = new UVA(new Point(0, 0, 0));
    UVA uva2 = new UVA(new Point(2, 0, 0));
    UVA uva3 = new UVA(new Point(4, 0, 0));
    UVA uva4 = new UVA(new Point(6, 0, 0));

    // //Миссия непрерывно следовать маршруту
    // Mission mission = new Mission(
    //     MTime.NO_END, 
    //     MMove.ROUTE_FOLLOWING,
    //     PAYLOAD.NONE
    // );

    // mission.setOperationList(new List<Operation>(){
    //     new Route(new List<Point>(){new Point(20, 0, 0)})
    // });

    // 1 -> {A : B : C}
    // 

    // Console.WriteLine(mission.toString());

    // 1 - Расчитать сколько работы сделает квадракоптер на той или иной МИССИИ
    // 2 - Расчитать 
}

// Функция подсчёта эффективности
Boolean magicMathFunc(List<Mission> missions, List<UVA> uvas){
    if (uvas == null || missions == null || uvas.Count == 0 || missions.Count == 0)
        return false;
    
    // Динамический массив
    float[][] matrix = new float[missions.Count][];
    for (int i = 0; i < matrix.Length; i++)
        matrix[i] = new float[uvas.Count];
    
    // Инициализация нулями
    for (int i = 0; i < matrix.Length; i++)
        for (int j = 0; j < matrix.Length; j ++)
            matrix[i][j] = 0;

    return true;
}

void testChargingStation(){
    Battery a = new Battery(10000, false);
    Battery b = new Battery(15000, false);
    Battery c = new Battery(14000, false);

    ChargerStation chargerStation = new ChargerStation(true, 100000, 30000, 3, 2000);
    Console.WriteLine(chargerStation.toString());

    chargerStation.startCharging(a);
    Thread.Sleep(900);
    chargerStation.startCharging(b);
    Thread.Sleep(900);
    chargerStation.startCharging(c);
    Thread.Sleep(900);
}