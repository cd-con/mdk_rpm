using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib_11
{
    public interface Human
    {
        public string Name { get; set; }

        public string Talk();
    }

    public class Batya : Human
    {
        private string _name;
        public string Name { get => _name; set => _name = value; }

        public Batya(string name) => Name = name;

        public string Talk() => $"Меня зовут {Name}";
    }

    public class SinaKorzina : Human
    {
        public Batya batya;

        private string _name;
        public string Name { get => _name; set => _name = value; }
        public string MidName;

        public SinaKorzina(string name, Batya batya, string midName = null)
        {
            this.batya = batya;
            Name = name;
            MidName = midName;
        }

        public string Talk() => MidName == null ? $"Меня зовут {Name}, а моего батю - {batya.Name}" : $"Меня зовут {Name} {MidName}";
    }
}
