using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System_Split.Interfaces;

public class ComponentsManager
{
    private HashSet<Hardware> hardwareComponents;
    private HashSet<Hardware> dumpedComponents;
    private IReader reader;
    private IWriter writer;
    private MethodInfo[] methods;
    private const string EndCommand = "System Split";
    private const string WrongParametersCountInCommandExceptionMessage = "Expected parameters are: {0}";



    public ComponentsManager(IReader reader, IWriter writer, HashSet<Hardware> hardwareComponents)
    {
        this.hardwareComponents = hardwareComponents;
        this.dumpedComponents = new HashSet<Hardware>();
        this.reader = reader;
        this.writer = writer;
        this.methods = this.GetType()
            .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
    }

    public void Run()
    {
        this.ExecuteCommands();

    }

    private void ExecuteCommands()
    {
        var inputLine = this.reader.ReadLine()
            .Split(new char[] { '(', ')', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .ToArray();


        while (!inputLine.Equals(EndCommand))
        {
            var commandName = inputLine[0].Trim();

            var methodForExecution = methods.FirstOrDefault(m => m.Name.Equals(commandName, StringComparison.OrdinalIgnoreCase));

           

            if (methodForExecution != null)
            {
                this.InvokeMethod(inputLine.Skip(1).ToArray(), methodForExecution);
            }


            inputLine = this.reader.ReadLine()
              .Split(new char[] { '(', ')', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
              .ToArray();
        }


    }

    private void InvokeMethod(string[] args, MethodInfo methodForExecution)
    {
        ParameterInfo [] requiredParams = methodForExecution.GetParameters();

        if (args.Length < requiredParams.Length)
        {
            throw new ArgumentException(string.Format(WrongParametersCountInCommandExceptionMessage,
                    string.Join(", ", requiredParams.Select(rp => rp.Name))));
        }

        else if (requiredParams.Length==1 && requiredParams[0].ParameterType.Name=="String[]")
        {
            methodForExecution.Invoke(this,  new object[] { args});
        }

        else if (requiredParams.Length==0)
        {
            methodForExecution.Invoke(this, null);
        }

        else
        {
            var parsedParams = new object[requiredParams.Length];

            for (int i = 0; i < requiredParams.Length; i++)
            {
                var paramType = requiredParams[i].ParameterType;
                parsedParams[i] = Convert.ChangeType(args[i], paramType);
            }
            methodForExecution.Invoke(this, parsedParams);
        }
        
    }

    public void RegisterPowerHardware(string name, int capacity, int memory)
    {
       
        hardwareComponents.Add(new PowerHardware(name, HardwareType.Power, capacity, memory));
    }

    public void RegisterHeavyHardware(string[] components)
    {
        var name = components[0];
        var capacity = int.Parse(components[1]);
        var memory = int.Parse(components[2]);
        hardwareComponents.Add(new HeavyHardware(name, HardwareType.Heavy, capacity, memory));
    }

    public void RegisterExpressSoftware(string[] components)
    {
        var hardwareComponentName = components[1];
        string name = components[2];
        int capacity = int.Parse(components[3]);
        int memory = int.Parse(components[4]);

        if (CheckIfHardwareExists(hardwareComponentName))
        {
            if (hardwareComponents.FirstOrDefault(n => n.Name == hardwareComponentName).MaxCapacity >= capacity && hardwareComponents.FirstOrDefault(n => n.Name == hardwareComponentName).MaxMemory >= memory)
            {
                hardwareComponents.FirstOrDefault(n => n.Name == hardwareComponentName).SoftwareComponents.Add(new ExpressSoftware(name, SoftwareType.Express, capacity, memory));
                hardwareComponents.FirstOrDefault(n => n.Name == hardwareComponentName).MaxCapacity -= hardwareComponents.FirstOrDefault(n => n.Name == hardwareComponentName).SoftwareComponents.Find(x => x.Name == name).CapacityConsumption;
                hardwareComponents.FirstOrDefault(n => n.Name == hardwareComponentName).MaxMemory -= hardwareComponents.FirstOrDefault(n => n.Name == hardwareComponentName).SoftwareComponents.Find(x => x.Name == name).MemoryConsumption;

            }
            else
            {
                return;
            }
        }


    }

    public void RegisterLightSoftware(List<string> components)
    {
        var hardwareComponentName = components[1];
        string name = components[2];
        int capacity = int.Parse(components[3]);
        int memory = int.Parse(components[4]);

        if (CheckIfHardwareExists(hardwareComponentName))
        {
            if (hardwareComponents.FirstOrDefault(n => n.Name == hardwareComponentName).MaxCapacity >= capacity && hardwareComponents.FirstOrDefault(n => n.Name == hardwareComponentName).MaxMemory >= memory)
            {
                hardwareComponents.FirstOrDefault(n => n.Name == hardwareComponentName).SoftwareComponents.Add(new LightSoftware(name, SoftwareType.Light, capacity, memory));
                hardwareComponents.FirstOrDefault(n => n.Name == hardwareComponentName).MaxCapacity -= hardwareComponents.FirstOrDefault(n => n.Name == hardwareComponentName).SoftwareComponents.Find(x => x.Name == name).CapacityConsumption;
                hardwareComponents.FirstOrDefault(n => n.Name == hardwareComponentName).MaxMemory -= hardwareComponents.FirstOrDefault(n => n.Name == hardwareComponentName).SoftwareComponents.Find(x => x.Name == name).MemoryConsumption;

            }
            else
            {
                return;
            }
        }


    }

    public void ReleaseSoftwareComponent(List<string> components)
    {
        var hardwareName = components[1];
        var softwareName = components[2];
        var capacity = 0;
        var memory = 0;


        if (CheckIfHardwareExists(hardwareName))
        {
            if (hardwareComponents.FirstOrDefault(hw => hw.Name == hardwareName).SoftwareComponents.Exists(c => c.Name == softwareName))
            {
                var elementToRemove = hardwareComponents.FirstOrDefault(hw => hw.Name == hardwareName).SoftwareComponents.Find(c => c.Name == softwareName);
                capacity = hardwareComponents.FirstOrDefault(hw => hw.Name == hardwareName).SoftwareComponents.FirstOrDefault(c => c.Name == softwareName).CapacityConsumption;
                memory = hardwareComponents.FirstOrDefault(hw => hw.Name == hardwareName).SoftwareComponents.FirstOrDefault(c => c.Name == softwareName).MemoryConsumption;
                hardwareComponents.FirstOrDefault(hw => hw.Name == hardwareName).MaxCapacity += capacity;
                hardwareComponents.FirstOrDefault(hw => hw.Name == hardwareName).MaxMemory += memory;
                hardwareComponents.FirstOrDefault(hw => hw.Name == hardwareName).SoftwareComponents.Remove(elementToRemove);
            }
        }
    }

    public void Analyze()
    {
        var sb = new StringBuilder();
        sb.AppendLine("System Analysis");
        sb.AppendLine($"Hardware Components: {hardwareComponents.Count(hc => hc.IsDumped == false)}");
        var numberOfSoftwareElements = 0;
        var totalOperationalMemoryInUse = 0;
        var totalCapacityTaken = 0;

        foreach (var HW_Element in hardwareComponents.Where(hc => hc.IsDumped == false))
        {
            numberOfSoftwareElements += HW_Element.SoftwareComponents.Count();
            foreach (var SW_Element in HW_Element.SoftwareComponents)
            {
                totalOperationalMemoryInUse += SW_Element.MemoryConsumption;
                totalCapacityTaken += SW_Element.CapacityConsumption;
            }
        }
        sb.AppendLine($"Software Components: {numberOfSoftwareElements}");
        sb.AppendLine($"Total Operational Memory: {totalOperationalMemoryInUse} / {hardwareComponents.Where(hc => hc.IsDumped == false).Sum(m => m.MaxMemory) + totalOperationalMemoryInUse }");
        sb.Append($"Total Capacity Taken: {totalCapacityTaken} / {hardwareComponents.Where(hc => hc.IsDumped == false).Sum(c => c.MaxCapacity) + totalCapacityTaken}");

        Console.WriteLine(sb.ToString());

    }

    public void Dump(List<string> components)
    {
        var componentToRemoveName = components[1];
        hardwareComponents.FirstOrDefault(n => n.Name == componentToRemoveName).IsDumped = true;
    }

    public void Restore(List<string> components)
    {
        var componentToRestoreName = components[1];



        if (CheckIfExistsInDumpedList(componentToRestoreName))
        {
            hardwareComponents.FirstOrDefault(n => n.Name == componentToRestoreName).IsDumped = false;
        }

    }

    public void Destroy(List<string> components)
    {
        var componentToDestroyName = components[1];
        var componentToDestroy = hardwareComponents.FirstOrDefault(n => n.Name == componentToDestroyName);

        if (CheckIfExistsInDumpedList(componentToDestroyName))
        {
            hardwareComponents.Remove(componentToDestroy);
        }
    }

    public void SystemSplit()
    {

        foreach (var hardwareComponent in hardwareComponents.Where(hw => hw.IsDumped == false).OrderByDescending(hw => hw.HardwareType.ToString()))
        {
            Console.WriteLine($"Hardware Component - {hardwareComponent.Name}");
            Console.WriteLine($"Express Software Components - {hardwareComponent.SoftwareComponents.Count(c => c.SoftwareType.ToString() == "Express")}");
            Console.WriteLine($"Light Software Components - {hardwareComponent.SoftwareComponents.Count(c => c.SoftwareType.ToString() == "Light")}");
            Console.WriteLine($"Memory Usage: {hardwareComponent.SoftwareComponents.Sum(sw => sw.MemoryConsumption)} / {hardwareComponent.SoftwareComponents.Sum(sw => sw.MemoryConsumption) + hardwareComponent.MaxMemory}");
            Console.WriteLine($"Capacity Usage: {hardwareComponent.SoftwareComponents.Sum(sw => sw.CapacityConsumption)} / {hardwareComponent.SoftwareComponents.Sum(sw => sw.CapacityConsumption) + hardwareComponent.MaxCapacity}");
            Console.WriteLine($"Type: {hardwareComponent.HardwareType.ToString()}");
            if (hardwareComponent.SoftwareComponents.Count > 0)
            {
                Console.WriteLine($"Software Components: {string.Join(", ", hardwareComponent.SoftwareComponents.Select(c => c.Name))}");
            }
            else
            {
                Console.WriteLine("Software Components: None");
            }

        }

    }

    public void DumpAnalyze()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"Dump Analysys");
        sb.AppendLine($"Power Hardware Components: {hardwareComponents.Where(hc => hc.IsDumped == true).Count(hc => hc.HardwareType.ToString() == "Power")}");
        sb.AppendLine($"Heavy Hardware Components: {hardwareComponents.Where(hc => hc.IsDumped == true).Count(hc => hc.HardwareType.ToString() == "Heavy")}");
        var numberOfExpressComponents = 0;
        var numberOfLightComponents = 0;

        foreach (var item in hardwareComponents.Where(v => v.IsDumped == true))
        {
            foreach (var sw in item.SoftwareComponents)
            {
                if (sw.SoftwareType.ToString() == "Express")
                {
                    numberOfExpressComponents++;
                }
                else if (sw.SoftwareType.ToString() == "Light")
                {
                    numberOfLightComponents++;
                }
            }
        }

        // sb.AppendLine($"Express Software Components: {hardwareComponents.Where(z => z.IsDumped == true).Select(c => c.SoftwareComponents.Where(sc => sc.SoftwareType.ToString() == "Express")).Count()}");
        sb.AppendLine($"Express Software Components: {numberOfExpressComponents}");
        sb.AppendLine($"Light Software Components: {numberOfLightComponents}");

        // sb.AppendLine($"Light Software Components: {hardwareComponents.Where(z => z.IsDumped == true).Select(c => c.SoftwareComponents.Where(sc => sc.SoftwareType.ToString() == "Light")).Count()}");
        sb.AppendLine($"Total Dumped Memory: {hardwareComponents.Where(z => z.IsDumped == true).Select(c => c.SoftwareComponents.Sum(sc => sc.MemoryConsumption)).Sum()}");
        sb.Append($"Total Dumped Capacity: {hardwareComponents.Where(z => z.IsDumped == true).Select(c => c.SoftwareComponents.Sum(sc => sc.CapacityConsumption)).Sum()}");

        Console.WriteLine(sb.ToString());
    }





    public bool CheckIfHardwareExists(string name)
    {
        if (hardwareComponents.FirstOrDefault(n => n.Name == name).Name == name)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool CheckIfExistsInDumpedList(string name)
    {
        if (hardwareComponents.FirstOrDefault(n => n.Name == name).Name == name && hardwareComponents.FirstOrDefault(n => n.Name == name).IsDumped == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}

//“System Analysis
//  Hardware Components: {countOfHardwareComponents}
//  Software Components: {countOfSoftwareComponents}
//  Total Operational Memory: {totalOperationalMemoryInUse} / {maximumMemory}
//  Total Capacity Taken: {totalCapacityTaken} / {maximumCapacity}”
