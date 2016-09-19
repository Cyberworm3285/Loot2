using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;

namespace Loot2
{
    /// <summary>
    ///     Klasse um die Config Datei im Hauptordner zu laden und verarbeiten
    /// </summary>
    class ConfigLoader
    {
        /// <summary>
        ///     Hauptordner-Dateipfad
        /// </summary>
        private string pathBase = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
        /// <summary>
        ///     die eigentliche Config-Datei
        /// </summary>
        private Config configFile;

        /// <summary>
        ///     versucht die Config-Datei zu laden und erstellt sie ggf. neu
        /// </summary>

        private void loadConfigs()
        {
            YAXLib.YAXSerializer xml = new YAXLib.YAXSerializer(typeof(Config));
            try
            {
                string xmlString = File.ReadAllText(Path.Combine(pathBase, "config.xml"));
                configFile = (Config)xml.Deserialize(xmlString);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                //erstellt die Hardgecodete Standard Datei
                configFile = new Config
                {
                    lowValue = 0,
                    highvalue = 1000,
                    autoLoadType = 2,
                    autoSmoothIterator = -1,
                    lootAlg = 0,
                    rarBoundsCfg = new int[] { int.MaxValue, 600, 300, 100, int.MinValue },
                    rarNamesCfg = new string[] { "Normal", "Verbessert", "Überlegen", "Legendär" },
                    rarColCfg = new Color[] { Color.DarkSlateGray, Color.Green, Color.DarkViolet, Color.DarkGoldenrod },
                    useAreaCheckBx = true,
                    useQuestCheckBx = true
                };
                string xmlString = xml.Serialize(configFile);
                File.WriteAllText(Path.Combine(pathBase, "config.xml"), xmlString);
            }
        }

        /// <summary>
        ///     gibt die geladene Config-Datei aus
        /// </summary>
        /// <returns>Die geladene <see cref="Config"/>-Datei</returns>
        public Config getConfig()
        {
            loadConfigs();
            return configFile;
        }
    }

    /// <summary>
    ///     Variablen-Container zum (de)serialisieren
    /// </summary>
    public class Config
    {
        [YAXLib.YAXComment("untere Grenze (0-1000)")]
        [YAXLib.YAXValueFor("Unten")]
        [YAXLib.YAXAttributeFor("Bounds")]
        /// <summary>
        ///     standard Wert für untere Grenze in <see cref="LootGUI"/>
        /// </summary>
        public int lowValue { get; set; }
        [YAXLib.YAXComment("obere Grenze (0-1000)")]
        [YAXLib.YAXValueFor("Oben")]
        [YAXLib.YAXAttributeFor("Bounds")]
        /// <summary>
        ///     standard Wert für obere Grenze in <see cref="LootGUI"/>
        /// </summary>
        public int highvalue { get; set; }
        [YAXLib.YAXComment("Automatisches Laden (-1 -> deaktiviert, ansonsten 0-2)")]
        [YAXLib.YAXAttributeFor("Load")]
        /// <summary>
        ///     standard Lade-Modus beim Laden (<see cref="LootGUI"/>)
        /// </summary>
        public int autoLoadType { get; set; }
        [YAXLib.YAXComment("Automatisches Smoothen (-1(,0) -> deaktiviert, ansonsten 1-x)")]
        [YAXLib.YAXAttributeFor("Load")]
        /// <summary>
        ///     standard Iterationen von der Smooth Methode beim Laden (<see cref="LootGUI"/>)
        /// </summary>
        public int autoSmoothIterator { get; set; }
        [YAXLib.YAXComment("Automatsch ausgewählter Loot-Algorithmus (-1 -> deaktiviert, ansonsten 0-3)")]
        [YAXLib.YAXAttributeFor("Load")]
        /// <summary>
        ///     standardmäßig ausgewählter Loot Algorithmus (<see cref="LootGUI"/>)
        /// </summary>
        public int lootAlg { get; set; }
        [YAXLib.YAXComment("Grenzen der Wahrscheinlickeitsabtrennung (muss gleichviele Elemente haben wie rarNames&rarCol!)")]
        [YAXLib.YAXAttributeFor("Rarity Bounds")]
        [YAXLib.YAXCollection(YAXLib.YAXCollectionSerializationTypes.Serially, SeparateBy = ",")]
        /// <summary>
        ///     Grenzen der Rarities für Marker am Diagramm (<see cref="popUpDia"/>)
        /// </summary>
        public int[] rarBoundsCfg { get; set; }
        [YAXLib.YAXComment("Namen der Wahrscheinlickeitsabtrennung (muss gleichviele Elemente haben wie rarBounds&rarCol!)")]
        [YAXLib.YAXAttributeFor("Rarity Names")]
        [YAXLib.YAXCollection(YAXLib.YAXCollectionSerializationTypes.Serially, SeparateBy = ",")]
        /// <summary>
        ///     Namen der Marker am Diagramm (<see cref="popUpDia"/>)
        /// </summary>
        public string[] rarNamesCfg { get; set; }
        [YAXLib.YAXComment("Farben der Wahrscheinlickeitsabtrennung (muss gleichviele Elemente haben wie rarNames&rarBounds!)")]
        [YAXLib.YAXValueFor("Rarity Cols")]
        [YAXLib.YAXCollection(YAXLib.YAXCollectionSerializationTypes.Recursive)]
        /// <summary>
        ///     Farben der Marker am Diagramm (<see cref="popUpDia"/>)
        /// </summary>
        public Color[] rarColCfg { get; set; }
        [YAXLib.YAXComment("Boolean, ob der Area-Tag verwendet werden soll")]
        [YAXLib.YAXAttributeFor("Load")]
        /// <summary>
        ///     Gibt an, ob eine Checked Listbox mit ausgelesenen Area-Strings als GUI Element verwendet werden soll
        /// </summary>
        public bool useAreaCheckBx { get; set; }
        [YAXLib.YAXComment("Boolean, ob der Quest-Tag verwendet werden soll")]
        [YAXLib.YAXAttributeFor("Load")]
        /// <summary>
        ///      Gibt an, ob eine Checked Listbox mit ausgelesenen Quest-Strings als GUI Element verwendet werden soll
        /// </summary>
        public bool useQuestCheckBx { get; set; }
    }

    /// <summary>
    ///     <see cref="IComparer{T}"/> für <see cref="Loot"/> sortiert nach: <see cref="Loot.rarity"/>
    /// </summary>
    public class LootComparer : IComparer<Loot>
    {
        int IComparer<Loot>.Compare(Loot x, Loot y)
        {
            if (x.rarity > y.rarity)
            { return 1; }
            else if(x.rarity < y.rarity)
            { return -1; }
            else
            { return 0; }
        }
    }

    /// <summary>
    ///     <see cref="IComparer{T}"/> für <see cref="Loot"/> sortiert nach: <see cref="Loot.name"/>
    /// </summary>
    public class LootComparerName : IComparer<Loot>
    {
        int IComparer<Loot>.Compare(Loot x, Loot y)
        {
            return (string.CompareOrdinal(x.name, y.name));
        }
    }
}
