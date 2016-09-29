using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loot2
{
    /// <summary>
    ///     Klasse für Daten eines <see cref="Loot"/>-Objekts mit ggf. mehreren <see cref="Operation"/>-Elementen
    /// </summary>
    public class Loot
    {
        /// <summary>
        ///     <see cref="string"/>, der nachher als Name des Items ausgegeben wird
        /// </summary>
        public string name { get; set; }
        /// <summary>
        ///     Typ des Items der ggf. zum Filtern oder sortieren benutzt wird (wird hinter <see cref="name"/> ausgegeben)
        /// </summary>
        public string type { get; set; }
        /// <summary>
        ///     Zeichenkette (<see cref="string"/>), wo jeweils ein <see cref="char"/> ein Tag Element darstellt
        /// </summary>
        public string tags { get; set; }
        /// <summary>
        ///     Tag für die Zugehörigkeit zu bestimmeten Quests
        /// </summary>
        public List<string> questTags { get; set; }
        /// <summary>
        ///     Tag für die Zugehörigkeit zu bestimmten Gebieten
        /// </summary>
        public List<string> areaTags { get; set; }
        /// <summary>
        ///     Maximale Anzahl, die das Item gelootet werden kann
        /// </summary>
        public int maxLootable { get; set; }
        /// <summary>
        ///     <see cref="int"/> von 0-1000 (selten-häufig)
        /// </summary>
        public int rarity { get; set; }
        /// <summary>
        ///     Anzahl der zu berechnenden Operationen
        /// </summary>
        public int opCount { get; set; }
        /// <summary>
        ///     speichert die Operationen (Attribut,Schaden,Namen,Anzahl, etc)
        /// </summary>
        public List<Operation> operationsList { get; set; }

        public Loot()
        {
            areaTags = new List<string>();
            questTags = new List<string>();
            operationsList = new List<Operation>();
        }

        /// <summary>
        ///     besetzt das Objekt mit standard Werten, erstellt 2 Operationen und gibt den Befehl an diese weiter
        /// </summary>
        public void createDummy()
        {
            name = "DummyName";
            type = "DummyType";
            tags = "DummyTags";
            questTags.Add("DummyQuest1");
            questTags.Add("DummyQuest2");
            areaTags.Add("DummyArea1");
            areaTags.Add("DummyArea2");
            maxLootable = -1;
            rarity = 1000;
            opCount = 2;
            operationsList.Add(new Operation());
            operationsList[0].createDummy(1 , true);
            operationsList.Add(new Operation());
            operationsList[1].createDummy(2, false);
            operationsList.Add(new Operation());
            operationsList[2].createDummy(3, false);
        }
    }
}
