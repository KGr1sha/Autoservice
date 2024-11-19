public class Warehouse {
    private string filename;
    private Dictionary<Detail, int> details;
    
    public Warehouse(string filename) {
        this.filename = filename;
        details = new Dictionary<Detail, int>();
    }

    public void AddDetail(Detail d, int amount) {
        details[d] = details.GetValueOrDefault(d, 0) + amount;
    }

    public void RemoveDetail(Detail d, int amount) {
        if (details.ContainsKey(d) == false) {
            throw new ArgumentException("Tried to remove detail that does not exist");
        }
        if (details[d] < amount) {
            throw new ArgumentException("Tried to remove more details then exists");
        }

        details[d] -= amount;
    }

    public bool Has(Detail d) {
        return details.ContainsKey(d);
    }

    public void SaveToFile() {
        using (var fileWriter = new StreamWriter(filename)) {
            foreach(Detail d in details.Keys) {
                fileWriter?.WriteLine($"{d.Name}:{d.Cost}:{details[d]}");
            }
        }
    }

    public void ReadFromFile() {
        using (var fileReader = new StreamReader(filename)) {
            string? line;
            details.Clear();
            int count = 0;
            while((line = fileReader?.ReadLine()) != null) {
                string[] keyval = line.Split(':');
                string name = keyval[0];
                float cost = float.Parse(keyval[1]);
                int amount = int.Parse(keyval[2]);
                details.Add(new Detail(name, cost), amount);
                count++;
            }
            Console.WriteLine($"read {count} details from file");
        }
    }

    public void PrintDetails() {
        foreach(Detail d in details.Keys) {
            Console.WriteLine($"{d.Name} || cost: {d.Cost} || amount: {details[d]}"); 
        }
    }
}

public class Detail {
    public string Name {get; private set;}
    public float Cost {get; private set;}

    public Detail(string name, float cost) {
        Name = name;
        Cost = cost;
    }

    public override int GetHashCode() {
        return Name.GetHashCode();
    }

    public override bool Equals(Object? obj)
    {
        return GetHashCode() == obj?.GetHashCode();
    }
}

