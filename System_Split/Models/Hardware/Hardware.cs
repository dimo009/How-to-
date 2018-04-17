using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Hardware
{
    private string name;

    private Enum hardwareType;

    private int maxCapacity;

    private int maxMemory;

    private bool isDumped;

    public bool IsDumped
    {
        get { return isDumped; }
        set { isDumped = value; }
    }


    private List<Software> softwareComponents;

    public Enum HardwareType
    {
        get { return this.hardwareType; }
        set { this.hardwareType = value; }
    }

    public Hardware(string name, Enum hardwareType, int maxCapacity, int maxMemory)
    {
        this.Name = name;
        this.HardwareType = hardwareType;
        this.MaxCapacity = maxCapacity;
        this.MaxMemory = maxCapacity;
        this.softwareComponents = new List<Software>();
        this.IsDumped = false;
    }

    public List<Software> SoftwareComponents
    {
        get { return this.softwareComponents; }
        set { this.softwareComponents = value; }
    }
    public int MaxMemory
    {
        get { return maxMemory; }
        set { maxMemory = value; }
    }


    public int MaxCapacity
    {
        get { return maxCapacity; }
         set { maxCapacity = value; }
    }

    //public string Type
    //{
    //    get { return type; }
    //    protected set { type = value; }
    //}

    public string Name
    {
        get { return name; }
        protected set { name = value; }
    }

}

