//                _                                   _
//   __ _  _   _ | |_   ___   ___   ___  _ __ __   __(_)  ___   ___
//  / _` || | | || __| / _ \ / __| / _ \| '__|\ \ / /| | / __| / _ \
// | (_| || |_| || |_ | (_) |\__ \|  __/| |    \ V / | || (__ |  __/
//  \__,_| \__,_| \__| \___/ |___/ \___||_|     \_/  |_| \___| \___|
//
//                               :+*##%%%%@@@@@@@@@%%%%%%%%###*++.      
//                             .+@@+-::::::::::*@@-:::----==+*@%%%:     
//                          .:=%@+.            -@@:           :@%%@=    
//                        .:=%@*.              :@@=            =@%%@+   
//                      :=*@@#:                .@%*             %%%%@*  
//        .::----===+++*%@%%#*+++++++++++++*****#**++++*****####%%%%##= 
//   -+#%%#*+==-==+***#%%%@%%%%%%%%%%%%%%%%#**#%@%%%%%%%%#****#%%%%%*%*:
// .#*.#%%***#####**#%%%%@%%%%%%%%%%%%%%%%%%%%%%%@%%%%%#**#####**%%%%##%
//:%%+%%%%##%%##%%%##%%%%@%%%%%%%%%%%%%%%%%%%%%%%@%%%%%%@%%##%%@@%%%%%%@
//%%%%%%@#%##==%=#*%%#@%%@%%%%%%%%%%%%%%%%%%%%%%%@%%%@%%##+*#-#*%%@%%%%%
//*@%%%%%%%+++:-=+*+%%%%%%@%%%%%%%%%%%%%%%%%%%%%@%%%%%%#==+--==++%%@%%%#
// +@@%%%%%+==-:--+=%%%%%%@@@@@@@@@@@@@@@@@@@@@@@%%%%@%#+==---=++%%@@@%-
//   :-=-+%**+-#=#+#%=+###*********************+++++++*%***+*:#+%%:..   
//        :*%####%%+.                                  -*%###*%%+


public class BankruptException : Exception {
    public override string Message => "YOU ARE BROKE";
}

public class AutoService {
    public float Money {get; private set;}
    private Warehouse warehouse;
    private float fine;

    public AutoService(float money, float fine, Warehouse w) {
        Money = money;
        this.fine = fine;
        warehouse = w;
        using (var sr = new StreamReader("car.txt")) {
            Console.WriteLine(sr.ReadToEnd());
        }
    }
    
    public void TryRepair(RepairOrder order) {
        Console.WriteLine("===TRYING TO REPAIR===");
        Console.WriteLine(order);
        foreach(Detail d in order.BrokenDetails.Keys) {
            if (false == warehouse.Has(d)) {
                Console.WriteLine($"===FAILED TO REPAIR {d.Name}===");
                ChangeMoneyBalance(-fine);
                continue;
            }

            warehouse.RemoveDetail(d, 1);
            ChangeMoneyBalance(order.BrokenDetails[d]);
            Console.WriteLine($"===REPAIRED {d.Name}===");
        }
    }

    private void ChangeMoneyBalance(float amount) {
        Money += amount;
        if (Money < 0) {
            throw new BankruptException();
        }
    }
}
