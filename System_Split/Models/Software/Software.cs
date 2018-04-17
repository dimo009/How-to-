using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Software
{
    private string name;
    private Enum softwareType;
    private int capacityConsumption;
    private int memoryConsumption;

    public Software(string name, Enum softwareType, int capacityConsumption, int memoryConsumption)
    {
        this.Name = name;
        this.SoftwareType = softwareType;
        this.CapacityConsumption = capacityConsumption;
        this.MemoryConsumption = memoryConsumption;
    }
    public int MemoryConsumption
    {
        get { return memoryConsumption; }
        set { memoryConsumption = value; }
    }


    public int CapacityConsumption
    {
        get { return capacityConsumption; }
        set { capacityConsumption = value; }
    }

    public Enum SoftwareType
    {
        get {return this.softwareType; }
        set {this.softwareType = value; }
    }
    public string Name
    {
        get { return name; }
        protected set { name = value; }
    }

}

