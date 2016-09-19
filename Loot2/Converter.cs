using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace Loot_Converter
{
    /// <summary>
    ///     am besten ignorieren, das hier ist nur ein rudimentärer Converter, den ich NIE WIEDER ändern werde
    ///     <para>(Achtung, Spagetticode!)</para>
    /// </summary>
    class Converter
    {
        private Loot2.LootLib lootlib = new Loot2.LootLib();
        private XmlSerializer xml = new XmlSerializer(typeof(Loot2.LootLib));
        private string pathbase;

        public Loot2.LootLib convert(string path)
        {
            string[] lines;
            pathbase = path;

            try
            {
                lines = File.ReadAllLines(Path.Combine(path,"Items","Items.txt"));
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

            for (int i = 0; i<lines.Length; i++)
            {
                Loot2.Loot newLoot = new Loot2.Loot();
                string tempItem = lines[i];
                newLoot.name = getName(ref tempItem);
                newLoot.type = getType(ref tempItem);
                newLoot.operationsList.AddRange(getOps(ref tempItem));
                newLoot.rarity = getRarity(ref tempItem);
                newLoot.tags = getTags(ref tempItem);
                newLoot.areaTags = new List<string>();
                newLoot.questTags = new List<string>();
                newLoot.maxLootable = -1;
                newLoot.opCount = newLoot.operationsList.Count;

                lootlib.lootList.Add(newLoot);
            }

            if (Directory.Exists(Path.Combine(pathbase, "Output")))
            {
                StreamWriter sr = new StreamWriter(Path.Combine(pathbase, "Output", "Items.xml"));
                xml.Serialize(sr, lootlib);
                sr.Close();
                File.WriteAllText(Path.Combine(pathbase, "Output", "Items.json"), JsonConvert.SerializeObject(lootlib, Formatting.Indented));
            }

            return lootlib;
        }
        private string getName(ref string item)
        {
            string name;

            int index = item.IndexOf(",");
            name = item.Substring(0, index);
            item = item.Remove(0, index + 1);

            return name;
        }

        private string getType(ref string item)
        {
            string type;

            int index = item.IndexOf("{");
            type = item.Substring(0,index);
            item = item.Remove(0,index);

            return type;
        }

        private Loot2.Operation[] getOps(ref string item)
        {
            List<Loot2.Operation> operations = new List<Loot2.Operation>();

            int opCount = 0;
            int index = item.IndexOf("}");
            string opPart = item.Substring(1,index - 1);

            while ((opPart.IndexOf(";")) != -1)
            {
                int nextOpIndex = opPart.IndexOf(";");
                string opText = opPart.Substring(1,nextOpIndex-2);
                string nameText = opText.Substring(0,opText.IndexOf("]"));
                opText = opText.Remove(0,opText.IndexOf("]")+1);

                Loot2.Operation newOp = new Loot2.Operation();

                string pathPart = nameText.Substring(0, nameText.IndexOf("("));
                nameText = nameText.Remove(0,nameText.IndexOf("(")+1);
                string lineFilter = nameText.Substring(0,nameText.IndexOf(")")+1);
                string opPath = Path.Combine(pathbase,"Items","Parameter",pathPart + ".txt");
                try
                { 
                    if (lineFilter.IndexOf("r") != -1)
                    {
                        string[] attribute = File.ReadAllLines(opPath);
                        newOp.attribName.AddRange(attribute);
                    }
                    else if (lineFilter.IndexOf(",") != -1)
                    {
                        List<string> attribute = new List<string>();
                        string[] allLines = File.ReadAllLines(opPath);

                        int lastbreak = -1;
                        for (int i = 0; i <lineFilter.Length;i++)
                        {
                            if ((lineFilter[i] == ',') || (i == lineFilter.Length-1))
                            {
                                string lineString = lineFilter.Substring(lastbreak + 1, i - lastbreak -1);
                                lastbreak = i;
                                int lineInt;
                                Int32.TryParse(lineString, out lineInt);
                                attribute.Add(allLines[lineInt]);
                            }
                        }
                        newOp.attribName.AddRange(attribute);
                    }
                    else
                    {
                        string[] allLines = File.ReadAllLines(opPath);
                        int lineIndex;
                        Int32.TryParse(lineFilter.Substring(0,lineFilter.IndexOf(")")), out lineIndex);
                        newOp.attribName.Add(allLines[lineIndex]);
                    }
                }
                catch(FileNotFoundException ex)
                {
                    newOp.attribName.Add("Datei nicht gefunden");
                    Console.WriteLine(ex.Message);
                }
                string valueString = opText.Substring(1, opText.Length - 1);
                if (valueString.IndexOf("-") != -1)
                {
                    int low;
                    Int32.TryParse(valueString.Substring(0,valueString.IndexOf("-")), out low);
                    newOp.intervall[0] = low;
                    valueString = valueString.Remove(0,valueString.IndexOf("-")+1);
                    int high;
                    Int32.TryParse(valueString,out high);
                    newOp.intervall[1] = high;
                }
                else
                {
                    int highLow;
                    Int32.TryParse(valueString, out highLow);
                    newOp.intervall[0] = highLow;
                    newOp.intervall[1] = highLow;
                }
                operations.Add(newOp);
                opPart = opPart.Remove(0,opPart.IndexOf(";") + 1);
                opCount++;
            }
            item = item.Remove(0, item.IndexOf("}") + 1);
            return operations.ToArray<Loot2.Operation>();
        }

        private int getRarity(ref string item)
        {
            string rarePart = item.Substring(0,item.IndexOf(")") + 1);
            item = item.Remove(0, item.IndexOf(")") + 1);
            int rarity;
            Int32.TryParse(rarePart.Substring(1,rarePart.Length-2), out rarity);

            return rarity;
        }

        private string getTags(ref string item)
        {
            string tagPart = item.Substring(0, item.IndexOf(">")+1);
            return tagPart.Substring(1,tagPart.Length -2);
        }
    }
}
