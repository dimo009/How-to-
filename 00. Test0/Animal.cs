using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00.Test0
{
    [Serializable]public abstract class Animal
    {
        private string name;
        private int numberOfLegs;

       

        protected Animal(string name, int numberOfLegs)
        {
            this.Name = name;
            this.NumberOfLegs = numberOfLegs;
        }
        public string Name
        {
            get { return name; }
             set { name = value; }
        }

        public int NumberOfLegs
        {
            get { return numberOfLegs; }
            private set { numberOfLegs = value; }
        }

    }
}
