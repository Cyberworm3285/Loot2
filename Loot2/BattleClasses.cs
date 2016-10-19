using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loot2
{
    public class Character
    {
        [YAXLib.YAXAttributeFor("Name")]
        [YAXLib.YAXValueFor("Name")]
        public string name { get; set; }
        [YAXLib.YAXAttributeFor("Caption")]
        [YAXLib.YAXValueFor("Caption")]
        public string shortCap { get; set; }
        [YAXLib.YAXAttributeFor("AttributNamen")]
        [YAXLib.YAXValueFor("AttributNamen")]
        [YAXLib.YAXCollection(YAXLib.YAXCollectionSerializationTypes.Recursive)]
        public string[] attributeNames { get; set; }
        [YAXLib.YAXAttributeFor("AttributWerte")]
        [YAXLib.YAXValueFor("AttributWerte")]
        [YAXLib.YAXCollection(YAXLib.YAXCollectionSerializationTypes.Recursive)]
        public int[] attributeValues { get; set; }
    }

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
    }

    public class Enema
    {
        [YAXLib.YAXAttributeFor("Name")]
        [YAXLib.YAXValueFor("Name")]
        public string name { get; set; }
        [YAXLib.YAXAttributeFor("HP")]
        [YAXLib.YAXValueFor("HP")]
        public int health { get; set; }
        [YAXLib.YAXAttributeFor("EW")]
        [YAXLib.YAXValueFor("EW")]
        public string elementalWeakness { get; set; }
        [YAXLib.YAXAttributeFor("ES")]
        [YAXLib.YAXValueFor("ES")]
        public string dealsElementalDamge { get; set; }
    }
}
