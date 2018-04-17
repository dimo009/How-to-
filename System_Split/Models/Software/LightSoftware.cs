using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class LightSoftware : Software
{
    public LightSoftware(string name, Enum softwareType, int capacityConsumption, int memoryConsumption) : base(name, softwareType, capacityConsumption, memoryConsumption)
    {
        this.CapacityConsumption = base.CapacityConsumption + ((50*base.CapacityConsumption)/ 100);
        this.MemoryConsumption = base.MemoryConsumption - ((50 * base.MemoryConsumption) / 100);
    }
}

