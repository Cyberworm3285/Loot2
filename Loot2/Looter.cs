using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace Loot2
{
    /// <summary>
    ///     Klasse mit Daten und Logik
    /// </summary>
    class Looter
    {
        /// <summary>
        ///     Speicher Der Loot-Rohdaten
        /// </summary>
        public LootLib lootLib = new LootLib();
        /// <summary>
        /// Speicher für Standard-Konfigurationen
        /// </summary>
        private Config config;
        /// <summary>
        /// Randomizer für Klassenweite Zufalls-Generierung
        /// </summary>
        private Random randomizer = new Random();

        /// <summary>
        ///     Methodengruppe für Loot-Algorithmen
        /// </summary>
        /// <param name="unten">Mindest-Rarity-Wert (ignoriert)</param>
        /// <param name="oben">Maximal-Rarity-Wert (ignoriert)</param>
        /// <param name="tags">Tag Zeichenkette von Char als String (ignoriert)</param>
        /// <param name="blackWhite">Modus der Black/Whitelist (ignoriert)</param>
        /// <param name="nameFilter">Zwangsweise enthaltener Teilstring im Name (ignoriert)</param>
        /// <param name="extrainfo">Wahrheitswert der das Ereignislog aktiviert</param>
        /// <param name="source"><see cref="Loot"/>-<see cref="List{T}"/> mit Rohdaten</param>
        /// <returns>Die Rohdaten (<see cref="Loot"/>)</returns>
        private delegate Loot lootAlgs(int unten, int oben, string tags, bool blackWhite, string nameFilter, bool extrainfo, List<Loot> source);
        /// <summary>
        ///     Methodengruppe für Lade-Methoden
        /// </summary>
        private delegate void loadFile();
        /// <summary>
        ///     <see cref="Array"/> mit Loot-Algorithmen
        /// </summary>
        private lootAlgs[] lootAlgorithms;
        /// <summary>
        ///     <see cref="Array"/> mit Lade-Methoden
        /// </summary>
        private loadFile[] loadMethods;
        /// <summary>
        ///     Liste mit Namen für die Loot-Algorithmen
        /// </summary>
        public List<string> algNames = new List<string> { "Scheiß_drauf", "Faul_Mit_Parameter", "Smooth", "Smooth_PCR" };
        public List<string> typeNames = new List<string>() { "JSON", "Vorgänger" };
        private int actRar;

        /// <summary>
        ///     für abwärts-Kompatibilität zu älteren Versionen (kein XML und json)
        /// </summary>
        private Loot_Converter.Converter converter = new Loot_Converter.Converter();


        //geben Items, Typen und Zwischenschritte aus, werden im Constructor von LootGUI übergeben
        /// <summary>
        ///     zum Output von generierten Items
        /// </summary>
        private ListBox output;
        /// <summary>
        ///     zum Output von Zwischenschritten
        /// </summary>
        private ListBox logOutput;
        /// <summary>
        ///     zum Auflisten der item-Typen
        /// </summary>
        private CheckedListBox typeOutput, areaTags, questTags;
        /// <summary>
        ///     zum Output der Seltenheit des letzten Loots
        /// </summary>
        private Button rarityOutput;

        /// <summary>
        ///     <see cref="popUpDia"/> ist die Owner-<see cref="Form"/> vom Diagramm
        /// </summary>
        private popUpDia Charto;
        /// <summary>
        ///     BattleStarGalactica ist die Owner- Form des Kampsystems
        /// </summary>
        private BattleStarGalactica battleMaster;
        /// <summary>
        ///     Status der Form um sie ggf. zu aktivieren
        /// </summary>
        private bool isChartoShown = false;

        /// <summary>
        ///     Konstruktor des Looter, der sämtliche Logik und Daten enthält
        /// </summary>
        /// <param name="outputList">Die Listbox in der die fertigen Items ausgegeben werden sollen</param>
        /// <param name="lOutput">Die Listbox in der ggf. die Zwischenschritte ausgegeben werden sollen</param>
        /// <param name="tOutput">Die Checked Listbox, in der die Itemtypes ausgegeben werden sollen</param>
        /// <param name="cfg">Die Configurationsdatei aus dem Hauptordner geladen (mit <see cref="ConfigLoader"/>)</param>
        public Looter(ListBox outputList, ListBox lOutput, CheckedListBox tOutput, Config cfg, CheckedListBox aTags, CheckedListBox qTags, Button rarOutput)
        {
            output = outputList;
            logOutput = lOutput;
            typeOutput = tOutput;
            rarityOutput = rarOutput;
            areaTags = aTags;
            questTags = qTags;
            Charto = new popUpDia(cfg);

            if (cfg.useBattleSystem)
            {
                battleMaster = new BattleStarGalactica();
                battleMaster.Show();
            }

            config = cfg;
            initializeLootAlgs();
            initializeLoadMethods();
            rarityOutput.Click += rarButtonClick;
        }

        public void newBattlesystem()
        {
            battleMaster = new BattleStarGalactica();
            battleMaster.Show();
        }

        /// <summary>
        ///     Baumverfahren zur Dummy-Erstellung (Befehl wird bis ganz nach unten weitergereicht)
        /// </summary>
        public void createDummy()
        {
            lootLib.createDummy();
        }

        /// <summary>
        ///     gibt ggf. den angegebenen String in der Log-Listbox aus
        /// </summary>
        /// <param name="message">Die Nachricht</param>
        /// <param name="enabled">Die Vorraussetzung</param>
        private void writeLog(string message, bool enabled)
        {
            if (enabled)
            { logOutput.Items.Add(message); }
        }

        /// <summary>
        ///     Methode zur Speicherung der json Datei
        /// </summary>
        public void jsonSave()
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,"Items", "JSON_Output", "Items.json");
            File.WriteAllText(path, Newtonsoft.Json.JsonConvert.SerializeObject(lootLib.lootList, Newtonsoft.Json.Formatting.Indented));
        }

        /// <summary>
        ///     Methode zum Laden der Items.json Datei
        /// </summary>
        public void jsonLoad()
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "Items", "JSON");
            lootLib.SerializeFromDir(path);
            loadCheckBoxes();
        }

        /// <summary>
        ///     Konvertierung der in \Input\Items gespeicherten Dateien in die neue lootLib Bibleothek
        /// </summary>
        public void convertAndLoadFromOld()
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
            lootLib = converter.convert(path);
            loadCheckBoxes();
        }

        /// <summary>
        ///     traversiert sämtliche Items und merkt sich jeden Typ genau einmal und gibt diesen aus
        /// </summary>
        public void loadCheckBoxes()
        {
            typeOutput.Items.Clear();
            for (int i = 0; i < lootLib.lootList.Count; i++)
            {
                string temp = lootLib.lootList[i].type;
                if (typeOutput.FindStringExact(temp) == -1)
                {
                    typeOutput.Items.Add(temp, true);
                }
                if ((config.useAreaCheckBx) && (lootLib.lootList[i].areaTags.Count != 0))
                {
                    areaTags.Items.Clear();
                    for (int j = 0; j < lootLib.lootList[i].areaTags.Count; j++)
                    {
                        temp = lootLib.lootList[i].areaTags[j];
                        if ((areaTags.FindStringExact(temp) == -1) && (config.useAreaCheckBx))
                        {
                            areaTags.Items.Add(temp, true);
                        }
                    }
                }
                if ((config.useQuestCheckBx) && (lootLib.lootList[i].questTags.Count != 0))
                {
                    questTags.Items.Clear();
                    for (int j = 0; j < lootLib.lootList[i].questTags.Count; j++)
                    {
                        temp = lootLib.lootList[i].questTags[j];
                        if ((questTags.FindStringExact(temp) == -1) && (config.useQuestCheckBx))
                        {
                            questTags.Items.Add(temp, true);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     erstellt aus dem gegeben Roh-Item einen string, der zufällige Werte aus eben diesen Item erhält
        /// </summary>
        /// <param name="rawItem">Die Roh Daten zur Generierung</param>
        /// <returns>Einen String aus den Rohdaten(rawItem) mit zufälligen Spezifikationen</returns>
        public string generateLoot(Loot rawItem)
        {
            string item = "";
            item += rawItem.name + " [" + rawItem.type + "] ";
            //Itemname + Typen + i * (Operationname + Wert) 
            //(Bei dem Wert 0 wird dieser ausgeblendet)
            if (rawItem.operationsList.Count != 0)
            {
                List<Operation> FixedOpList = new List<Operation>();
                List<Operation> NonFixedOpList = new List<Operation>();
                FixedOpList.AddRange(rawItem.operationsList.FindAll(o => o.fixedOp));
                NonFixedOpList.AddRange(rawItem.operationsList.FindAll(o => !o.fixedOp));

                for (int i = 0; i < rawItem.opCount; i++)
                {
                    int rdmOp = randomizer.Next(0, NonFixedOpList.Count);
                    Operation op = NonFixedOpList[rdmOp];
                    processOperation(op, ref item);
                    NonFixedOpList.RemoveAt(rdmOp);
                }
                foreach (Operation op in FixedOpList)
                {
                    processOperation(op, ref item);
                }
            }
            var tup = config.getRaritySpecs(rawItem.rarity);
            rarityOutput.Text = tup.Item1;
            actRar = tup.Item2;
            rarityOutput.BackColor = tup.Item3;
            return item;
        }

        private void rarButtonClick(object sender, EventArgs e)
        {
            logOutput.Items.Add(config.rarDesriptionCfg[actRar]);
        }

        /// <summary>
        ///     Fügt einem gegebenen <see cref="string"/> die gegebene <see cref="Operation"/> hinzu 
        /// </summary>
        /// <param name="op">Ausgangs-<see cref="Operation"/></param>
        /// <param name="itemString"><see cref="string"/> zum anheften</param>
        private void processOperation(Operation op, ref string itemString)
        {
            int rdmName = randomizer.Next(0, op.attribName.Count);
            itemString += " || " + op.attribName[rdmName];
            if (op.intervall.low != 0)
            {
                int tempValue = randomizer.Next(op.intervall.low, op.intervall.high + 1);
                itemString+= "(" + tempValue + ")";
            }
        }

        /// <summary>
        ///     Erklärung:
        ///     <para>ignoriert sämtliche Parameter und gibt zufällig ein Item aus</para>
        /// </summary>
        /// <param name="unten">Mindest-Rarity-Wert (ignoriert)</param>
        /// <param name="oben">Maximal-Rarity-Wert (ignoriert)</param>
        /// <param name="tags">Tag Zeichenkette von Char als String (ignoriert)</param>
        /// <param name="blackWhite">Modus der Black/Whitelist (ignoriert)</param>
        /// <param name="nameFilter">Zwangsweise enthaltener Teilstring im Name (ignoriert)</param>
        /// <param name="extrainfo">Wahrheitswert der das Ereignislog aktiviert</param>
        /// <param name="source"><see cref="Loot"/>-<see cref="List{T}"/> mit Rohdaten</param>
        /// <returns>Die Rohdaten (<see cref="Loot"/>)</returns>
        private Loot lootAlgorithm01(int unten, int oben, string tags, bool blackWhite, string nameFilter, bool extrainfo, List<Loot> source)
        {
            writeLog("Algorithmus: " + algNames[0], extrainfo);
            writeLog("Jo, scheiß auf die Parameter", extrainfo);
            if (lootLib.lootList.Count == 0)
            {
                writeLog("Keine Items geladen, yo!", true);
                return null;
            }
            int rdm = randomizer.Next(0, source.Count);
            if (extrainfo) output.Items.Add(generateLoot(source[rdm]));
            return (source[rdm]);
        }

        /// <summary>
        ///     Erklärung:
        ///     <para>
        ///         Die Itemliste wird gemischt
        ///         und das erste Item in der Liste,
        ///         das die gegebenen Anforderungen erfüllt
        ///         ist automatisch ausgewählt.
        ///         -> Seltene Items sind nur durch Anforderungen wirklich selten
        ///     </para>
        /// </summary>
        /// <param name="unten">Mindest-Rarity-Wert</param>
        /// <param name="oben">Maximal-Rarity-Wert</param>
        /// <param name="tags">Tag Zeichenkette von Char als String</param>
        /// <param name="blackWhite">Modus der Black/Whitelist</param>
        /// <param name="nameFilter">Zwangsweise enthaltener Teilstring im Name</param>
        /// <param name="extrainfo">Wahrheitswert der das Ereignislog aktiviert</param>
        /// <param name="source"><see cref="Loot"/>-<see cref="List{T}"/> mit Rohdaten</param>
        /// <returns>Die Rohdaten (<see cref="Loot"/>)</returns>
        private Loot lootAlgorithm02(int unten, int oben, string tags, bool blackWhite, string nameFilter, bool extrainfo, List<Loot> source)
        {
            writeLog("Algorithmus: " + algNames[1], extrainfo);
            writeLog("Mische Liste..", extrainfo);
            lootLib.mixUp();
            //Start Index ist -1, für Fehlerabfrage nach Listendurchgang 
            int index = -1;
            string item = "";

            //gemischte Liste durchgehen
            for (int i = 0; i < source.Count; i++)
            {
                //Anforderungen überprüfen
                writeLog("Überprüfe Item " + source[i].name + " !", extrainfo);
                if (isLootAllowed(source[i], unten, oben, tags, blackWhite, nameFilter))
                {
                    //Durchgang beendet Item befindet sich an Stelle: i
                    writeLog("Ja passt", extrainfo);
                    index = i;
                    break;
                }
                else
                {
                    //Anforderungen sind nicht erfüllt, gehe zum nächsten Item
                    writeLog("Ne passt nicht", extrainfo);
                }
            }
            if (index == -1) //da ist die oben genannte Fehlerabfrage (kein Item erfüllt die Anforderungen, der Index wurde nicht verändert)
            {
                writeLog("Keine Übereinstimmung", extrainfo);
                return null;
            }
            //erstellt aus dem gegeben Item einen Item-String, der dann auch direkt ausgegeben wird
            item = this.generateLoot(source[index]);
            if (extrainfo) output.Items.Add(item);
            return source[index];
        }

        /// <summary>
        ///     Erklärung:
        ///     <para>
        ///         Traversiert alle Items und weist jedem Item einen Abschnitt
        ///         auf einer Zahl zu, dieser ist die <see cref="Loot.rarity"/> Eigenschaft
        ///         und die Zahl besteht aus allen Abschnitten zusammen.
        ///         Je größer die Wahrscheinlichkeit, desto größer ist der Abschnitt
        ///         auf der Gesamtzahl und somit auch die Wahrscheinlickeit         
        ///         dieses Item auszuwählen. 
        ///         Ein Randomizer bestimmt eine Zahl zwischen 0 und der Gesamtzahl 
        ///         und mit Hilfe einer Schleife wird ermittelt auf wessen Abschnitt diese
        ///         Zahl liegt.
        ///     </para>
        ///     <para>-> seltene Items sind auf jeden Fall selten UND werden durch Anforderungen limitiert</para>
        ///     <para>Vergleich CS:GO Skin Gambling im internet verwendet eine ähnliche Vorgehensweise </para>
        /// </summary>
        /// <param name="unten">Mindest-Rarity-Wert</param>
        /// <param name="oben">Maximal-Rarity-Wert</param>
        /// <param name="tags">Tag Zeichenkette von Char als String</param>
        /// <param name="blackWhite">Modus der Black/Whitelist</param>
        /// <param name="nameFilter">Zwangsweise enthaltener Teilstring im Name</param>
        /// <param name="extrainfo">Wahrheitswert der das Ereignislog aktiviert</param>
        /// <param name="source"><see cref="Loot"/>-<see cref="List{T}"/> mit Rohdaten</param>
        /// <returns>Die Rohdaten (<see cref="Loot">LootKlasse</see>)</returns>
        private Loot lootAlgorithm03(int unten, int oben, string tags, bool blackWhite, string nameFilter, bool extrainfo, List<Loot> source)
        {
            writeLog("Algorithmus: " + algNames[2], extrainfo);
            //Zuerst werden sämtliche Items, die eh nicht den Anforderungen entsprechen ausgefiltert 
            //(sonst gibts nachher Probleme mit den Abschnitten)
            List<Loot> filteredList = new List<Loot>();

            for (int i = 0; i < source.Count; i++)
            {
                if (isLootAllowed(source[i], unten, oben, tags, blackWhite, nameFilter))
                {
                    //writeLog(source[i].name + " ist erlaubt!",extrainfo);
                    filteredList.Add(source[i]);
                }
                else
                {
                    //writeLog(source[i].name + " ist nicht erlaubt!",extrainfo);
                }
            }

            int counter = filteredList.Count;
            writeLog(counter.ToString() + " Items gefiltert!", extrainfo);

            if (counter == 0)
            {
                writeLog("Keine Übereinstimmung", extrainfo);
                return null;
            }

            /*
            *   Array zur Abspeicherung der Obergrenzen des items an stelle i 
            *   Beipiel : 1. Item mit Namen "Arsch" hat seltenheit 200
            *   -> itemProbs[0] == 200, das ist der Abschnitt dieses Items
            */
            int[] itemProbs = new int[counter];
            int allItemProbs = 0;

            //jedes item erhält seinen Wert: Vorheriger gesamtwert + eigene Länge des Abschnitts
            for (int i = 0; i < counter; i++)
            {
                itemProbs[i] = allItemProbs + filteredList[i].rarity;
                writeLog(filteredList[i].name + " erhält Nummer " + itemProbs[i], extrainfo);
                allItemProbs += filteredList[i].rarity;
            }


            List<Loot> newList = new List<Loot>();
            newList.AddRange(filteredList);

            if (extrainfo)
            {
                createCharto();
                Charto.drawPie(newList, allItemProbs);
            }

            //LastNumber Speichert den letzten Überprüften Wert ab damit sich die Zahl auch wirklich ZWISCHEN den Werten befindet
            // (möglicherweise Überflüssig)
            int rdmLootNumber = randomizer.Next(0, allItemProbs + 1);
            int lastNumber = 0;

            writeLog("Item ist an Stelle: " + rdmLootNumber.ToString(), extrainfo);
            for (int i = 0; i < counter; i++)
            {
                if ((rdmLootNumber > lastNumber) && (rdmLootNumber <= itemProbs[i]))
                {
                    string generatedLoot = generateLoot(filteredList[i]);
                    writeLog("-> " + filteredList[i].name, extrainfo);
                    if (extrainfo) output.Items.Add(generatedLoot);
                    return filteredList[i];
                }
                else
                {
                    if (i != 0) { lastNumber = itemProbs[i - 1]; }
                }
            }
            return null;
        }

        /// <summary>
        ///     Erklärung:
        ///     <para>
        ///         Erst wird eine Seltenheit aus den Werten von <see cref="Config.rarBoundsCfg"/> generiert und dann
        ///         wird <see cref="Looter.lootAlgorithm03(int, int, string, bool, string, bool, List{Loot})"/> die modifizierte Liste angehängt 
        ///     </para>
        ///     <para>
        ///         (konstante Wahrscheinlichkeit für die einzelnen Seltenheiten)
        ///     </para>
        /// </summary>
        /// <param name="unten">Mindest-Rarity-Wert (ignoriert)</param>
        /// <param name="oben">Maximal-Rarity-Wert (ignoriert)</param>
        /// <param name="tags">Tag Zeichenkette von Char als String</param>
        /// <param name="blackWhite">Modus der Black/Whitelist</param>
        /// <param name="nameFilter">Zwangsweise enthaltener Teilstring im Name</param>
        /// <param name="extrainfo">Wahrheitswert der das Ereignislog aktiviert</param>
        /// <param name="source"><see cref="Loot"/>-<see cref="List{T}"/> mit Rohdaten</param>
        /// <returns>Die Rohdaten (<see cref="Loot"/>)</returns>
        private Loot lootAlgorithm04(int unten, int oben, string tags, bool blackWhite, string nameFilter, bool extrainfo, List<Loot> source)
        {
            writeLog("Algorithmus: " + algNames[3], extrainfo);
            writeLog("(Bounds werden ignoriert)", extrainfo);
            //Random Wert und Variablen zum Übergeben/Rechnen
            int rng = randomizer.Next(0, 1001);
            int i = 1;
            int highBound, lowBound;

            writeLog(rng + "..", extrainfo);
            //Schleife zum Ermitteln des Seltenheitswerts
            for (; i < config.rarBoundsCfg.Length; i++)
            {
                if (rng >= config.rarBoundsCfg[i])
                {
                    break;
                }
                writeLog("seltener als " + config.rarNamesCfg[i - 1] + "..", extrainfo);
            }
            writeLog("-> Seltenheit: " + config.rarNamesCfg[i - 1], extrainfo);
            //Automatisches setzen des Intervalls
            lowBound = config.rarBoundsCfg[i];
            highBound = config.rarBoundsCfg[i - 1];
            List<Loot> filteredList = new List<Loot>();
            //Filtern nach normalen & neu generierten Parametern
            writeLog("Bereite Liste vor..", extrainfo);
            for (int j = 0; j < source.Count; j++)
            {
                if ((source[j].rarity >= lowBound) && (source[j].rarity < highBound))
                {
                    filteredList.Add(source[j]);
                }
            }
            if (filteredList.Count == 0)
            {
                writeLog("keine Übereinstimmung", extrainfo);
                return null;
            }
            //ausgeben eines zufälliugen Items mit passenden parametern
            writeLog("vorläufige Liste: " + filteredList.Count + " Items", extrainfo);
            writeLog("übergebe an " + algNames[2], extrainfo);
            writeLog("START {", extrainfo);
            Loot result = lootAlgorithm03(lowBound, highBound, tags, blackWhite, nameFilter, extrainfo, filteredList);
            writeLog("} ENDE", extrainfo);
            return result;
        }

        /// <summary>
        ///     Gibt einen Wahrheitswert aus, der angibt, ob das Item mit den angegebenen Parametern erlaubt ist. 
        /// </summary>
        /// <param name="item">zu überprüfendes <see cref="Loot"/> Objekt </param>
        /// <param name="unten">Mindest-Rarity-Wert</param>
        /// <param name="oben">Maximal-Rarity-Wert</param>
        /// <param name="tags">Tag Zeichenkette von Char als String</param>
        /// <param name="blackWhite">Modus der Black/Whitelist</param>
        /// <param name="nameFilter">Zwangsweise enthaltener Teilstring im Name</param>
        /// <param name="extrainfo">Wahrheitswert der das Ereignislog aktiviert</param>
        /// <returns><see cref="bool"/> -> <see cref="true"/> = erlaubt</returns>
        private bool isLootAllowed(Loot item, int unten, int oben, string tags, bool blackWhite, string nameFilter)
        {
            //Achtung : ich könnte zwar schon beim ersten Verstoß false returnen, allerdings
            //halte ich mir die möglickeit offen SÄMTLICHE betreffende Verstöße noch irgendwo auszugeben
            //*schlechte Performance intensifies*

            Loot tempLoot = item;
            //Index des Itemtyps in der Type-CheckedListBox
            int typeIndex = (typeOutput.FindString(tempLoot.type));
            //ist diese Checkbox-Zeile aktiviert?
            bool typeAllowed = typeOutput.GetItemChecked(typeIndex);
            //Anfänglich wird davon ausgegeangen, dass sowohl der Name und die Tags zugelassen sind.
            bool nameAllowed = true;
            bool blackWhiteAllowed = true;
            bool questAllowed = false;
            bool areaAllowed = false;
            if (nameFilter != "")
            {
                //Wenn ein Filter übergeben wird, MUSS dieser im Namen vorhanden sein, ansonten ist das Item nicht zugelassen
                if (tempLoot.name.IndexOf(nameFilter) == -1) { nameAllowed = false; }
            }
            if (tags != "")
            {
                if (blackWhite)
                {
                    for (int k = 0; k < tempLoot.tags.Length; k++)
                    {
                        if (tags.IndexOf(tempLoot.tags[k]) != -1)
                        {
                            //wenn ein Tag-Filter vorhanden ist und es eine Blacklist ist, dann darf KEINER der Filter-Tags in den item Tags sein!
                            blackWhiteAllowed = false;
                            break;
                        }
                    }
                }
                else
                {
                    if (tempLoot.tags.Length == 0)
                    {
                        blackWhiteAllowed = false;
                    }
                    else
                    {
                        for (int j = 0; j < tempLoot.tags.Length; j++)
                        {
                            if (tags.IndexOf(tempLoot.tags[j]) == -1)
                            {
                                //wenn ein Tag-Filter vorhanden ist und es eine Whitelist ist, dann muss JEDES der Filter-Tags in den item Tags sein!
                                blackWhiteAllowed = false;
                                break;
                            }
                        }
                    }
                }
            }
            if (config.useAreaCheckBx)
            {
                if ((areaTags.Items.Count == 0) || (item.areaTags.Count == 0)) { areaAllowed = true; }
                else
                {
                    for (int i = 0; i < item.areaTags.Count; i++)
                    {
                        int index = areaTags.FindStringExact(item.areaTags[i]);
                        if (index != -1)
                        {
                            if (areaTags.GetItemChecked(index))
                            {
                                areaAllowed = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (config.useQuestCheckBx)
            {
                if ((questTags.Items.Count == 0) || (item.questTags.Count == 0)) { questAllowed = true; }
                else
                {
                    for (int i = 0; i < item.questTags.Count; i++)
                    {
                        int index = questTags.FindStringExact(item.questTags[i]);
                        if (index != -1)
                        {
                            if (questTags.GetItemChecked(index))
                            {
                                questAllowed = true;
                                break;
                            }
                        }
                    }
                }
            }
            return ((tempLoot.rarity >= unten) &&
                    (tempLoot.rarity <= oben) &&
                    (typeAllowed) &&
                    (nameAllowed) &&
                    (blackWhiteAllowed) &&
                    (areaAllowed) &&
                    (questAllowed));
                    //Alles muss erfüllt sein (logischerweise)
        }

        /// <summary>
        ///     Traversiert die ganze ItemListe (<see cref="LootLib.lootList"/>) und gibt den Index des Items, sowie eine Beispiel Generierung aus
        /// </summary>
        public void traverseItems()
        {
            output.Items.Add("<-------------------------------------------------Traversierung------------------------------------------------->");
            for (int i = 0; i < lootLib.lootList.Count; i++)
            {
                string tags = (lootLib.lootList[i].tags.Length == 0) ? "leer" : ">" + lootLib.lootList[i].tags + "<";
                //der string füllt den index bis zur länge des maximalen indexes mit '0'en auf 
                output.Items.Add(new string('0', lootLib.lootList.Count.ToString().Length - i.ToString().Length) + i.ToString() + ": " + generateLoot(lootLib.lootList[i]) + " Tags: " + tags);
            }
            output.Items.Add("<------------------------------------------------/Traversierung------------------------------------------------->");
        }

        /// <summary>
        ///     fügt die ganzen scheißteile hinzu, yo!
        /// </summary>
        private void initializeLootAlgs()
        {
            lootAlgorithms = new lootAlgs[]
            {
                lootAlgorithm01,
                lootAlgorithm02,
                lootAlgorithm03,
                lootAlgorithm04
            };
            int unbenannt = 1;
            while (lootAlgorithms.Length > algNames.Count)
            {
                algNames.Add("Unbenannt " + unbenannt++);
            }
        }

        /// <summary>
        ///     fügt die verschiedenen File-Load Methoden hinzu
        /// </summary>
        private void initializeLoadMethods()
        {
            loadMethods = new loadFile[] { jsonLoad, convertAndLoadFromOld };
        }

        /// <summary>
        ///     gibt die Anzahl der Alghorithmen aus 
        /// </summary>
        /// <returns><see cref="int"/> Anzahl der Loot-Algorithmen</returns>
        public int getLootAlorithmCount()
        {
            return lootAlgorithms.Length;
        }

        /// <summary>
        ///     benutzt den Algorithmus an dem angegeben Index
        /// </summary>
        /// <param name="algoIndex">Index des gewünschten Algorithmusses</param>
        /// <param name="unten">Mindest-Rarity-Wert</param>
        /// <param name="oben">Maximal-Rarity-Wert</param>
        /// <param name="tags">Tag Zeichenkette von Char als String</param>
        /// <param name="blackWhite">Modus der Black/Whitelist</param>
        /// <param name="nameFilter">Zwangsweise enthaltener Teilstring im Name</param>
        /// <param name="enableLog">Wahrheitswert der das Ereignislog aktiviert (standeardmäßig <see cref="true"/>)</param>
        /// <returns>Die Rohdaten (<see cref="Loot"/>)</returns>
        public Loot useLootAlgorithm(int algoIndex, int unten, int oben, string tags, bool blackWhite, string nameFilter, bool enableLog = true)
        {
            Loot result = lootAlgorithms[algoIndex](unten, oben, tags, blackWhite, nameFilter, enableLog, lootLib.lootList);
            if (result == null)
            {
                return null;
            }
            if (result.maxLootable == 1)
            {
                lootLib.lootList.RemoveAt(lootLib.lootList.FindIndex(delegate (Loot loot) { return (loot == result); }));
            }
            else if (result.maxLootable != -1)
            {
                result.maxLootable--;
            }
            return result;
        }

        /// <summary>
        ///     benutzt die angegeben File Load Methode
        /// </summary>
        /// <param name="index">Index der Load-File-Methode (xml,json,..)</param>
        public void useFileMethod(int index)
        {
            loadMethods[index]();
        }

        /// <summary>
        ///     erstellt, wenn noch nicht vorhanden, ein <see cref="popUpDia"/> mit Diagramm
        /// </summary>
        private void createCharto()
        {
            if (Charto.IsDisposed)
            {
                Charto = new popUpDia(config);
                Charto.Show();
            }
            else if (!isChartoShown)
            {
                isChartoShown = true;
                Charto.Show();
            }
        }

        /// <summary>
        ///     Mittelt die <see cref="Loot.rarity"/> Werte zyklisch
        /// </summary>
        /// <param name="it">Anzahl der Iterationen</param>
        public void smoothItemProbs(int it)
        {
            //logischerweise wird erst nach dem Rarity-Parameter sortiert
            lootLib.lootList.Sort(new LootComparer());
            if (lootLib.lootList.Count < 2) { return; }
            for (int j = 0; j < it; j++)
            {
                for (int i = 1; i < lootLib.lootList.Count - 1; i++)
                {
                    int maxIndex = lootLib.lootList.Count - 1;
                    lootLib.lootList[i].rarity = (int)((lootLib.lootList[i - 1].rarity + lootLib.lootList[i + 1].rarity) / 2);
                }
            }
        }
    }
}
