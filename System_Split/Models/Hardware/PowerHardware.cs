using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PowerHardware : Hardware
{
    public PowerHardware(string name, Enum hardwareType, int maxCapacity, int maxMemory) : base(name, hardwareType, maxCapacity, maxMemory)
    {
        this.MaxCapacity = base.MaxCapacity - ((75*base.MaxCapacity)/100);
        this.MaxMemory = base.MaxMemory + ((75 * base.MaxMemory) / 100);
    }
}

