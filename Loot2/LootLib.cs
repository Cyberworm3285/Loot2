using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Loot2
{
    /// <summary>
    ///     Klasse für rohe <see cref="Loot"/> Daten
    /// </summary>
    public class LootLib
    {
        /// <summary>
        ///     Speichert die rohen <see cref="Loot"/>-Objekte
        /// </summary>
        public List<Loot> lootList { get; set; }
        /// <summary>
        ///     obligatorisches Random Zeugs..
        /// </summary> 
        private Random randomizer = new Random();

        public LootLib()
        {
            lootList = new List<Loot>();
        }

        public void SerializeFromDir(string path)
        {
            lootList.Clear();
            string[] allFiles = Directory.GetFiles(path);
            foreach (string s in allFiles)
            {
                lootList.AddRange(Newtonsoft.Json.JsonConvert.DeserializeObject<List<Loot>>(File.ReadAllText(s)));
            }
        }

        /// <summary>
        ///     Erstellt ein leeres Item und gibt den Befehl in dessen Unterglieder weiter
        /// </summary>
        public void createDummy()
        {
            lootList.Clear();
            lootList.Add(new Loot());
            lootList[0].createDummy();
        }

        /// <summary>
        ///     vertauscht jedes Item einmal mit einem beliebigen anderen.
        /// </summary>
        public void mixUp()
        {
            for (int i=0; i<lootList.Count;i++)
            {
                int rdm = randomizer.Next(0,lootList.Count);
                Loot tempLoot = lootList[i];
                lootList[i] = lootList[rdm];
                lootList[rdm] = tempLoot;
            }
        }
    }
}
