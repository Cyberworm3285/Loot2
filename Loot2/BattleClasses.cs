using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loot2
{
    public abstract class Entity
    {
        [YAXLib.YAXAttributeFor("Name")]
        [YAXLib.YAXValueFor("Name")]
        public string name { get; set; }
        [YAXLib.YAXAttributeFor("DmgIntervall")]
        public int low { get; set; }
        [YAXLib.YAXAttributeFor("DmgIntervall")]
        public int high { get; set; }
        [YAXLib.YAXAttributeFor("Leben")]
        public int physHealth { get; set; }
        [YAXLib.YAXAttributeFor("Leben")]
        public bool dead { get; set; }
        [YAXLib.YAXAttributeFor("Elemental")]
        public string elemtalType { get; set; }
        [YAXLib.YAXAttributeFor("Elemental")]
        public string weakAgainst { get; set; }
    }

    [Serializable]
    public class Character : Entity
    {
        [YAXLib.YAXAttributeFor("AttributNamen")]
        [YAXLib.YAXValueFor("AttributNamen")]
        [YAXLib.YAXCollection(YAXLib.YAXCollectionSerializationTypes.Recursive)]
        public string[] attributeNames { get; set; }
        [YAXLib.YAXAttributeFor("AttributWerte")]
        [YAXLib.YAXValueFor("AttributWerte")]
        [YAXLib.YAXCollection(YAXLib.YAXCollectionSerializationTypes.Recursive)]
        public int[] attributeValues { get; set; }
        [YAXLib.YAXAttributeFor("Leben")]
        public int mentalHealth { get; set; }
        [YAXLib.YAXAttributeFor("Caption")]
        [YAXLib.YAXValueFor("Caption")]
        public string shortCap { get; set; }

        public override string ToString()
        {
            string result = "Name: " + name + "\n-PH: " + physHealth + "\n-MH: " + mentalHealth + "\n-Dmg: [" + low + ";" + high + "]" + "\n-ET: " + elemtalType + "\n-EW: " + weakAgainst;
            for (int i = 0; i < attributeNames.Length; i++)
            {
                result+=("\n" + attributeNames[i] + ": " + attributeValues[i]);
            }
            return result;
        }
    }

    [Serializable]
    public class Encounter
    {
        [YAXLib.YAXAttributeForClass]
        public string name { get; set; }
        [YAXLib.YAXAttributeFor("Caption")]
        [YAXLib.YAXValueFor("Caption")]
        public string shortCap { get; set; }
        [YAXLib.YAXValueFor("Enemies")]
        [YAXLib.YAXCollection(YAXLib.YAXCollectionSerializationTypes.Recursive)]
        public Enema[] enemies { get; set; }

        public override string ToString()
        {
            string result = "Name: " + name;
            foreach (Enema en in enemies)
            {
                result += "\n"+en.ToString();
            }
            return result;
        }
    }

    [Serializable]
    public class Enema : Entity
    {
        public override string ToString()
        {
            string result = (dead) ? "-Name: " + name + "\n[DEAD]"
                                   : "-Name: " + name + "\n--HP: " + physHealth + "\n--Dmg: [" + low + "; " + high + "]" + "\n--EW: " + weakAgainst + "\n--ED: " + elemtalType;
            return result;
        }
    }
}
