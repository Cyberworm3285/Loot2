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

        public abstract bool counterSuccess();
        public abstract bool hitSuccess();
        public abstract void die(Action<string> logFunction);
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
        [YAXLib.YAXAttributeFor("CounterAttribKey")]
        [YAXLib.YAXValueFor("CounterAttribKey")]
        public string counterAttribKey { get; set; }
        [YAXLib.YAXAttributeFor("HitChanceAttribKey")]
        [YAXLib.YAXValueFor("HitChanceAttribKey")]
        public string hitChanceAttribKey { get; set; }

        public override string ToString()
        {
            if (dead) return "Name: " + name + "\n[DEAD]";
            else
            {
                string result = "Name: " + name + "\n-PH: " + physHealth + "\n-MH: " + mentalHealth + "\n-Dmg: [" + low + ";" + high + "]" + "\n-ET: " + elemtalType + "\n-EW: " + weakAgainst;
                for (int i = 0; i < attributeNames.Length; i++)
                {
                    result += ("\n" + attributeNames[i] + ": " + attributeValues[i]);
                }
                return result;
            }
        }

        public override bool counterSuccess()
        {
            int index = Array.IndexOf(attributeNames, this.counterAttribKey);
            if (index == -1) throw new Exception("CounterAttributeKey is invalid");
            return DummyProvider.randomizer.Next(1, DummyProvider.getConfig.diceCap) <= attributeValues[index];
        }

        public override bool hitSuccess()
        {
            int index = Array.IndexOf(attributeNames, this.hitChanceAttribKey);
            if (index == -1) throw new Exception("HitChanceAttributeKey is invalid");
            return DummyProvider.randomizer.Next(1, DummyProvider.getConfig.diceCap) <= attributeValues[index];
        }

        public override void die(Action<string> logFunction)
        {
            logFunction("Character has died: " + name);
            dead = true;
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
        [YAXLib.YAXAttributeFor("Defense")]
        [YAXLib.YAXValueFor("Defense")]
        public int counterProb { get; set; }
        [YAXLib.YAXAttributeFor("Offense")]
        [YAXLib.YAXValueFor("Offense")]
        public int hitChance { get; set; }

        public override string ToString()
        {
            string result = (dead) ? "-Name: " + name + "\n[DEAD]"
                                   : "-Name: " + name + "\n--HP: " + physHealth + "\n--Dmg: [" + low + "; " + high + "]" + "\n--EW: " + weakAgainst + "\n--ED: " + elemtalType;
            return result;
        }

        public override bool counterSuccess()
        {
            return DummyProvider.randomizer.Next(1, 100) <= this.counterProb;
        }

        public override bool hitSuccess()
        {
            return DummyProvider.randomizer.Next(1, 100) <= this.hitChance;
        }

        public override void die(Action<string> logFunction)
        {
            logFunction("Enemy has died: " + name);
            dead = true;
        }
    }
}
