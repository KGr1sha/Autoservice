public class RepairOrder {
    public Dictionary<Detail,float> BrokenDetails {get; private set;}

    public RepairOrder(IEnumerable<Detail> details) {
        BrokenDetails = new Dictionary<Detail, float>();
        foreach(Detail d in details) {
            BrokenDetails.Add(d, CalculateRepairCost(d.Cost));
        }
    }

    private float CalculateRepairCost(float detailCost) {
        var rand = new Random();
        return (float)rand.NextDouble() * 
            (detailCost * 2 - detailCost / 2)
            + detailCost / 2;
    }

    public override string ToString() {
        string result = "===REPAIR ORDER===\n";
        foreach(Detail d in BrokenDetails.Keys) {
            result += $"name: {d.Name} || cost: {d.Cost} || work cost: {BrokenDetails[d] - d.Cost}\n"; 
        }
        result += "===END OF THE ORDER===";
        return result;
    }
}
