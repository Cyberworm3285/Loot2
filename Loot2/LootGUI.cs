using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ExtensionMethods;


namespace Loot2
{
    /// <summary>
    ///  HauptGUI
    /// </summary>
    public partial class LootGUI : Form
    {
        /// <summary>
        ///     LootHandler mit LootBibleothek und Algorithmen
        /// </summary>
        private Looter looter;
        /// <summary>
        ///     Zum Laden des standard Zustands von <see cref="LootGUI"/> und <see cref="popUpDia"/>
        /// </summary>
        private ConfigLoader configLoader = new ConfigLoader();
        /// <summary>
        ///     Ist die GUI im Blacklist Modus?
        /// </summary>
        private bool blacklist = true;
        /// <summary>
        ///     Variable für den Modus zum Dateien laden (XML,json,eigenes)
        /// </summary>
        private int fileMode = 0;
        /// <summary>
        ///     momentan ausgewählter LootAlgorithmus
        /// </summary>
        private int lootMode = 0;
        /// <summary>
        ///     Standard-Variablen-Container
        /// </summary>
        private Config config;
        private TabControl tabs;
        private CheckedListBox typeChckLBx;
        private CheckedListBox areaChckLBx;
        private CheckedListBox questChckLBx;
        private string pathBase = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);


        /// <summary>
        ///     Kostruktor erstellt außerdem <see cref="Config"/> und <see cref="Looter"/>
        /// </summary>
        public LootGUI()
        {
            InitializeComponent();
            config = configLoader.getConfig();
            createTabs(config.useAreaCheckBx, config.useQuestCheckBx);
            looter = new Looter(itemLBx, logLBx, typeChckLBx, config, areaChckLBx, questChckLBx, rarOutput);
            initializeConfigs();
            lootModeBtn.Text += " "+looter.algNames[lootMode];
            xmlJsonBtn.Text = looter.typeNames[0];
        }

        /// <summary>
        ///     Verarbeitet sämtliche relevanten Informationen aus der <see cref="Config"/> Klasse und übernimmt die Änderungen
        /// </summary>
        private void initializeConfigs()
        {
            string[] namen = new string[] { "XML", "json", "Vorgänger-Formatierung" };

            untenTBx.Text = config.lowValue.ToString();
            obenTBx.Text = config.highvalue.ToString();
            lootMode = config.lootAlg;
            if (config.autoLoadType != -1)
            {
                looter.useFileMethod(config.autoLoadType);
                aktDataTypeLbl.Text = "Momentan geladen: " + namen[config.autoLoadType] + " (" + looter.lootLib.lootList.Count.ToString() + ")";
            }
            if (config.autoSmoothIterator != -1)
            {
                smoothTxt.Text = config.autoSmoothIterator.ToString();
                smoothProbBtn_Click(null, null);
            }
        }

        /// <summary>
        ///     speichert einfach den momentanen Inhalt als Dummys (Item_Dummy.xxx)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dummyCreatorBtnr_Click(object sender, EventArgs e)
        {
            looter.jsonSave();
        }

        /// <summary>
        ///     Parsed die Bound Textfelder und generiert ein Item mit dem ausgewählten Algorithmus
        /// </summary>
        private void lootBtn_Click(object sender, EventArgs e)
        {
            logLBx.Items.Clear();
            int lowValue;
            int highValue;
            int.TryParse(untenTBx.Text, out lowValue);
            int.TryParse(obenTBx.Text, out highValue);
            looter.useLootAlgorithm(lootMode, lowValue, highValue, blackWhiteTBx.Text, blacklist, NFilterTBx.Text);          
        }

        /// <summary>
        ///     lädt Dateien nach dem momentanen Filemode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadBtn_Click(object sender, EventArgs e)
        {
            try
            {
                looter.useFileMethod(fileMode);
                aktDataTypeLbl.Text = "Momentan geladen: " + looter.typeNames[fileMode] + " (" + looter.lootLib.lootList.Count.ToString() + ")";
            }
            catch(FileNotFoundException ex)
            {
                itemLBx.Items.Add("Die zu ladende Datei ist nicht vorhanden!");
                Console.WriteLine(ex.Message);
            }
            if (looter.lootLib.lootList.Count == 0)
                itemLBx.Items.Add("Keine Items geladen!");
        }

        /// <summary>
        ///     gibt jedes Item einmal vollständig generiert aus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void traverseBtn_Click(object sender, EventArgs e)
        {
            looter.traverseItems();
        }

        /// <summary>
        ///     ändert den Blacklist Modus und stellt diese dar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blackWhiteListBtn_Click(object sender, EventArgs e)
        {
            if (blacklist)
            {
                blacklist = false;
                blackWhiteListBtn.Text = "Whitelist";
            }
            else
            {
                blacklist = true;
                blackWhiteListBtn.Text = "Blacklist";
            }
        }

        /// <summary>
        ///     säubert die Anzeigen und speichert eine Log-Datei in \Saves mit den aktuellen <see cref="DateTime"/>-Eigenschaften
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearBtn_Click(object sender, EventArgs e)
        {
            string[] items = new string[itemLBx.Items.Count];
            for (int i = 0; i < itemLBx.Items.Count; i++)
            {
                items[i] = itemLBx.Items[i].ToString();
            }
            itemLBx.Items.Clear();
            logLBx.Items.Clear();
            if (items.Length == 0) { return; }
            string pathBase = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
            string newFileName = DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second;
            Directory.CreateDirectory(Path.Combine(pathBase,"Saves"));
            File.WriteAllLines(Path.Combine(pathBase,"Saves",newFileName + ".log") , items);
        }

        /// <summary>
        ///     wechselt den Filmode und stellt diesen dar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xmlJsonBtn_Click(object sender, EventArgs e)
        {
            fileMode++;
            fileMode %= looter.typeNames.Count; // nur Werte von 0-2, also zyklisch mit Modulo umgesetzt
            xmlJsonBtn.Text = looter.typeNames[fileMode];
        }

        /// <summary>
        ///     füllt die Listen mit Dummy Elementen (wichtig, wenn man die Dateien verloren hat, und/oder die
        ///     LootGUI zum Item erstellen nicht zur Hand hat)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dummyCreatorBtn_Click(object sender, EventArgs e)
        {
            looter.createDummy();
            looter.loadCheckBoxes();
            aktDataTypeLbl.Text = "Dummy";
        }

        /// <summary>
        ///     wechselt zwischen den Loot Algorithmen und stellt diese dar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lootModeBtn_Click(object sender, EventArgs e)
        {
            int max = looter.getLootAlorithmCount();

            lootMode = (lootMode + 1) % max;
            lootModeBtn.Text = "Lootmode: " + looter.algNames[lootMode];
        }

        /// <summary>
        ///     Ruft die Smooth Funktion auf um die <see cref="Loot.rarity"/>-Werte zu mitteln
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void smoothProbBtn_Click(object sender, EventArgs e)
        {
            int iteration;
            int.TryParse(smoothTxt.Text, out iteration);
            looter.smoothItemProbs(iteration);
            smoothTxt.Text = "";
        }

        /// <summary>
        ///     benutzt den momentan ausgewählten Algorithmus solange, bis jedes item einmal ausgegeben 
        ///     wurde und gibt jeweils die zugehörige Iteration an
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AlgCheckBtn_Click(object sender, EventArgs e)
        {
            const int averageCount = 100;
            lootMode = 0;
            for (int i = 0; i < looter.getLootAlorithmCount(); i++)
            {
                int tempAverage = 0;
                for (int j = 0; j < averageCount; j++)
                {
                    List<Loot> rest = new List<Loot>();
                    rest.AddRange(looter.lootLib.lootList);

                    int counter = 1;
                    while ((rest.Count != 0) && (counter < 1000))
                    {
                        int lowValue;
                        int highValue;
                        int.TryParse(untenTBx.Text, out lowValue);
                        int.TryParse(obenTBx.Text, out highValue);
                        int index = rest.IndexOf(looter.useLootAlgorithm(lootMode, lowValue, highValue, blackWhiteTBx.Text, blacklist, NFilterTBx.Text, false));
                        if (index != -1) { rest.RemoveAt(index); }
                        counter++;
                    }
                    tempAverage += counter;
                }
                tempAverage /= averageCount;
                logLBx.Items.Add(looter.algNames[i] + " benötigt durchschnittlich ");
                if (tempAverage >= 1000)
                {
                    logLBx.Items.Add("1000+ Durchläufe");
                }
                else
                {
                    logLBx.Items.Add(tempAverage + " Durchläufe (" + averageCount + ")");
                }
                Console.WriteLine(i);
            }
        }

        private void createTabs(bool area, bool quest)
        {
            tabs = new TabControl();
            tabs.Parent = this;
            tabs.Height = this.Height - 26;
            tabs.Width = 150;
            this.Width += 176;
            tabs.Top = 13;
            tabs.Left = smoothProbBtn.Left + smoothProbBtn.Width + 13;
            tabs.Name = "tabs";
            tabs.TabPages.Add(new TabPage("Types"));
            typeChckLBx = new CheckedListBox();
            typeChckLBx.CheckOnClick = true;
            typeChckLBx.Name = "typeChckBx";
            typeChckLBx.Parent = tabs.TabPages[0];
            typeChckLBx.Height = tabs.Height;
            typeChckLBx.Width = tabs.Width - 26;
            typeChckLBx.Top = 0;
            typeChckLBx.Left = 13;
            if (!(area || quest)) { return; }
            if (area)
            {
                tabs.TabPages.Add(new TabPage("Area"));
                areaChckLBx = new CheckedListBox();
                areaChckLBx.CheckOnClick = true;
                areaChckLBx.Name = "areaChckBx";
                areaChckLBx.Parent = tabs.TabPages[tabs.TabPages.Count-1];
                areaChckLBx.Height = tabs.Height;
                areaChckLBx.Width = tabs.Width -26;
                areaChckLBx.Top = 0;
                areaChckLBx.Left = 13;
            }
            if (quest)
            {
                tabs.TabPages.Add(new TabPage("Quests"));
                questChckLBx = new CheckedListBox();
                questChckLBx.CheckOnClick = true;
                questChckLBx.Name = "areaChckBx";
                questChckLBx.Parent = tabs.TabPages[tabs.TabPages.Count - 1];
                questChckLBx.Height = tabs.Height;
                questChckLBx.Width = tabs.Width - 26;
                questChckLBx.Top = 0;
                questChckLBx.Left = 13;
            }
        }

        private void editBounds(bool input, TextBox output, int replacement)
        {
            if (input)
            {
                var specs = config.getRaritySpecs(output.Text.ToInt(replacement)-1);
                int nextIndex;
                nextIndex = (specs.Item2 + 1) % config.rarBoundsCfg.Length;
                output.Text = config.rarBoundsCfg[nextIndex.ToPositive()].ToString();

                int zahl = output.Text.ToInt(0);
                int indexNext = Array.FindIndex(config.rarBoundsCfg, i => i < zahl);
               // output.Text = config.rarBoundsCfg[indexNext % config.rarBoundsCfg.Length];
            }
        }

        private void obenTBx_KeyDown(object sender, KeyEventArgs e)
        {
           // editBounds(e.Control,sender as TextBox, 1000);
        }
    }
}
