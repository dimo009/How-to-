using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00.Test0
{
    public class Whale : Animal
    {

        private double weight;


        public Whale()
        {
            this.Name = "Kit";
        }

        public Whale(string name)
        {
            this.Name = "HAHAH";
        }
        public Whale(string name, double weight) : base(name, numberOfLegs: 0)
        {
            this.Weight = weight;
        }
        public double Weight
        {
            get { return this.weight; }
            set { this.weight = value; }
        }


        private string MakeSound()
        {
            return "HAHAHAHAHAHHAAHHA";
        }

        private double CalculateWeight(double tonsOfFood)
        {
            return this.Weight + tonsOfFood;
        }
    }
}

