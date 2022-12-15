
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