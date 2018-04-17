using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class HeavyHardware : Hardware
{
    public HeavyHardware(string name, Enum hardwareType, int maxCapacity, int maxMemory) : base(name, hardwareType, maxCapacity, maxMemory)
    {
        this.MaxCapacity = base.MaxCapacity * 2;
        this.MaxMemory = base.MaxMemory - ((25 * base.MaxMemory) / 100);
    }

}



