using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System_Split.Models.Input_Output;

class StartUp
    {
        static void Main(string[] args)
        {

        var reader = new ConsoleReader();
        var writer = new ConsoleWriter();
        var hardwareComponents = new HashSet<Hardware>();
        new ComponentsManager(reader, writer, hardwareComponents).Run();
        }
    }
