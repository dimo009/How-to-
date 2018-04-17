using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ExpressSoftware : Software
{
    public ExpressSoftware(string name, Enum softwareType, int capacityConsumption, int memoryConsumption) : base(name, softwareType, capacityConsumption, memoryConsumption)
    {
        this.MemoryConsumption = base.MemoryConsumption * 2;

    }
}

