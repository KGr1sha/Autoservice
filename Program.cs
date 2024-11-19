public class Program {
    static void Main(string[] args) {
        var warehouse = new Warehouse("warehouse.txt");
        var service = new AutoService(1000, 10, warehouse);
        warehouse.ReadFromFile();
        Console.WriteLine("WAREHOUSE:");
        warehouse.PrintDetails();

        RepairOrder order = new RepairOrder(new List<Detail>() {
            new Detail("engine", 100),
            new Detail("fara", 10),
        });
        service.TryRepair(order);
        warehouse.SaveToFile();
    }
}
