
testChargingStation();

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