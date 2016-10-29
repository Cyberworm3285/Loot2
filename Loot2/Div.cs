using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using Newtonsoft.Json;
using System.Windows.Forms;

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
                DummyProvider.LOADED_CONFIG = configFile;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                configFile = DummyProvider.DUMMY_CONFIG;
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
        public static string PATH_BASE = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);

        [YAXLib.YAXComment("untere Grenze (Standard 0-1000)")]
        [YAXLib.YAXValueFor("Unten")]
        [YAXLib.YAXAttributeFor("Bounds")]
        /// <summary>
        ///     standard Wert für untere Grenze in <see cref="LootGUI"/>
        /// </summary>
        public int lowValue { get; set; }
        [YAXLib.YAXComment("obere Grenze (Standard 0-1000)")]
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
        [YAXLib.YAXAttributeFor("Load")]
        [YAXLib.YAXComment("Legt fest ob das Kampfsystem benutzt werden soll")]
        /// <summary>
        ///     Legt fest ob das Kampfsystem benutzt werden soll
        /// </summary>
        public bool useBattleSystem { get; set; }
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
        [YAXLib.YAXValueFor("Rarity Cols")]
        [YAXLib.YAXCollection(YAXLib.YAXCollectionSerializationTypes.Recursive)]
        /// <summary>
        ///     Beschreibung für die Seltenheiten
        /// </summary>
        public string[] rarDesriptionCfg { get; set; }
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
        [YAXLib.YAXComment("Sachen fuer Das Kampfsystem")]
        [YAXLib.YAXAttributeFor("Battle")]
        public int diceCap { get; set; }
        [YAXLib.YAXAttributeFor("Battle")]
        public bool allowCounterAttack { get; set; }

        /// <summary>
        ///     Gibt den namen und die Farbe der Seltenheit mit dem gegebenen Parameter in einem <see cref="Tuple"/> aus 
        /// </summary>
        /// <param name="rar">Zahl, die die Seltenheit repräsentiert</param>
        /// <returns>Name und Farbe</returns>
        public Tuple<string ,int, Color> getRaritySpecs(int rar)
        {
            for (int i = 0; i < this.rarBoundsCfg.Length; i++)
            {
                if ((rar < rarBoundsCfg[i]) && (rar >= rarBoundsCfg[i+1]))
                {
                    return Tuple.Create<string ,int,  Color>(rarNamesCfg[i],i , (rarColCfg[i]==Color.DarkSlateGray||rarColCfg[i]==Color.Black)?Color.LightGray:rarColCfg[i]);
                }
            }
            return Tuple.Create<string ,int , Color>("[Fehler]",0 , Color.Orange);
        }
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

    public static class DummyProvider
    {
        public static Random randomizer = new Random();
        public static Character DUMMY_CHARACTER = new Character
        {
            name = "DummyName",
            shortCap = "D",
            mentalHealth = 100,
            physHealth = 100,
            dead = false,
            high = 100,
            low = 0,
            elemtalType = "-",
            weakAgainst = "-",
            counterAttribKey = "DummyAttribut1",
            hitChanceAttribKey = "DummyAttribut2",
            attributeNames = new string[]
            {
                "DummyAttribut1",
                "DummyAttribut2"
            },
            attributeValues = new int[]
            {
                15,
                12
            }
        };

        public static Encounter DUMMY_ENCOUNTER = new Encounter
        {
            name = "DummyName",
            shortCap = "D",
            enemies = new Enema[]
            {
                new Enema
                {
                    name = "DummyEnemy1",
                    elemtalType = "-",
                    weakAgainst = "-",
                    physHealth = 100,
                    dead = false,
                    high  = 100,
                    low = 0,
                    counterProb = 15,
                    hitChance = 80
                },
                new Enema
                {
                    name = "DummyEnemy2",
                    elemtalType = "-",
                    weakAgainst = "-",
                    physHealth = 100,
                    dead = false,
                    high  = 100,
                    low = 0,
                    counterProb = 15,
                    hitChance = 80
                }
            }
        };

        public static Loot      DUMMY_LOOT      = new Loot
        {
            areaTags = new List<string>() { "DummyTag1", "DummyTag2" },
            maxLootable = -1,
            name = "DummyItem",
            opCount = 1,
            questTags = new List<string>() { "DummyTag1", "DummyTag2" },
            rarity = 1000,
            tags = "abcdef",
            type = "DummyType",
            operationsList = new List<Operation>()
            {
                new Operation()
                {
                    attribName = new List<string>() { "DummyAttribut1_Name1", "DummyAttribut1_Name2" },
                    fixedOp = true,
                    intervall = new Intervall(0,100)
                },
                new Operation()
                {
                    attribName = new List<string>() { "DummyAttribut2_Name1", "DummyAttribut2_Name2" },
                    fixedOp = false,
                    intervall = new Intervall(0,100)
                },
                new Operation()
                {
                    attribName = new List<string>() { "DummyAttribut3_Name1", "DummyAttribut3_Name2" },
                    fixedOp = false,
                    intervall = new Intervall(0,100)
                }
            }
        };

        public static Config DUMMY_CONFIG = new Config
        {
            lowValue = 0,
            highvalue = 1000,
            autoLoadType = 0,
            autoSmoothIterator = -1,
            lootAlg = 0,
            rarBoundsCfg = new int[] { int.MaxValue, 600, 300, 100, int.MinValue },
            rarNamesCfg = new string[] { "Normal", "Verbessert", "Überlegen", "Legendär" },
            rarColCfg = new Color[] { Color.LightGray, Color.Green, Color.DarkViolet, Color.DarkGoldenrod },
            rarDesriptionCfg = new string[] { "Mega kacke", "Geht so", "Schon ice", "Töfte" },
            useAreaCheckBx = true,
            useQuestCheckBx = true,
            useBattleSystem = false,
            allowCounterAttack = true,
            diceCap = 20,
        };

        public static Config LOADED_CONFIG = null;

        public static Config getConfig { get { return LOADED_CONFIG ?? DUMMY_CONFIG; } }
    }
}

namespace ExtensionMethods
{
    public static class ExtensionMethods
    {
        /// <summary>
        ///     Parsed den string zu einem Int
        /// </summary>
        /// <returns><see cref="int"/>></returns>
        public static int ToInt(this string str, int replacementOnError)
        {
            try
            {
                int result = int.Parse(str);
                return result;
            }
            catch (FormatException)
            {
                return replacementOnError;
            }
        }

        public static int ToPositive(this int i)
        {
            return (i < 0) ? -1 * i : i;
        }

        public static int CoolModulo(this int i, int length)
        {
            if (i < 0)
            {
                return length - i.ToPositive() % length;
            }
            else
            {
                return i % length;
            }
        }
    }
}
