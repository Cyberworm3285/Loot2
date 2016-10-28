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
using YAXLib;
using ExtensionMethods;

namespace Loot2
{
    public partial class BattleStarGalactica : Form
    {
        private List<Character> chars = new List<Character>();
        private List<Encounter> encounters = new List<Encounter>();
        int enCounter, charCounter, enemyCounter;
        Random rand = new Random();
        private int logCounter = 0;

        public BattleStarGalactica()
        {
            InitializeComponent();
        }

        private void BattleStarGalactica_Load(object sender, EventArgs e)
        {
            string[] allCharFiles = null;
            string[] allEncFiles = null;
            try
            {
                allCharFiles = Directory.GetFiles(Path.Combine(Config.PATH_BASE, "Characters"));
                allEncFiles = Directory.GetFiles(Path.Combine(Config.PATH_BASE, "Encounters"));
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            if (allCharFiles == null || allEncFiles == null) return;
            createDummys(allCharFiles.Length == 0, allEncFiles.Length == 0);
            string[] allLines;
            YAXSerializer serializer = new YAXSerializer(typeof(Character));
            foreach (string s in allCharFiles)
            {
                chars.Add(serializer.DeserializeFromFile(s) as Character);
            }
            serializer = new YAXSerializer(typeof(Encounter));
            foreach (string s in allEncFiles)
            {
                encounters.Add(serializer.DeserializeFromFile(s) as Encounter);
            }
            updateTabs(0, 0, 0);
        }

        private void updateTabs(int ch, int en, int enem)
        {
            charTabCtrl.TabPages.Clear();
            encTabCtrl.TabPages.Clear();
            foreach (Character c in chars)
            {
                TabPage temp = new TabPage(c.shortCap);
                Label lb = new Label();
                lb.Name = c.name + "_Label";
                lb.Location = new Point(0, 0);
                lb.Size = new Size(encTabCtrl.Width - 10, encTabCtrl.Height - 20);
                if (c.attributeNames.Length != c.attributeValues.Length) throw new Exception("Uneven attribute-lenghts");
                lb.Text = c.ToString();
                temp.Controls.Add(lb);
                charTabCtrl.TabPages.Add(temp);
            }
            foreach (Encounter enc in encounters)
            {
                TabPage temp = new TabPage(enc.shortCap);
                Label lb = new Label();
                lb.Name = enc.name + "_Label";
                lb.Location = new Point(0, 0);
                lb.Size = new Size(encTabCtrl.Width - 10, encTabCtrl.Height - 20);
                lb.Text = enc.ToString();
                temp.Controls.Add(lb);
                encTabCtrl.TabPages.Add(temp);
            }
            if (chars.Count == 0 || encounters.Count == 0) return; 
            charTabCtrl.SelectedIndex = ch;
            encTabCtrl.SelectedIndex = en;
            charSearchTxt.Text = chars[ch].name;
            encTxt.Text = encounters[en].name;
            enSearchTxt.Text = encounters[en].enemies[enem].name;
        }

        private void createDummys(bool noChars, bool noEncounters)
        {
            YAXSerializer serializer;
            if (noChars)
            {
                serializer = new YAXSerializer(typeof(Character));
                serializer.SerializeToFile(DummyProvider.DUMMY_CHARACTER, Path.Combine(Config.PATH_BASE, "Characters", "DummyChar.xml"));
            }
            if (noEncounters)
            {
                serializer = new YAXSerializer(typeof(Encounter));
                serializer.SerializeToFile(DummyProvider.DUMMY_ENCOUNTER, Path.Combine(Config.PATH_BASE, "Encounters", "DummyEncounter.xml"));
            }
        }

        private void charBtnRight_Click(object sender, EventArgs e)
        {
            charTabCtrl.SelectedIndex = ++charTabCtrl.SelectedIndex % charTabCtrl.TabCount;
            charCounter++;
        }

        private void charBtnLeft_Click(object sender, EventArgs e)
        {
            int c = charTabCtrl.SelectedIndex - 1;
            charTabCtrl.SelectedIndex = (c < 0) ? charTabCtrl.TabCount - 1 : c;
            enCounter--;
        }

        private void encBtnRight_Click(object sender, EventArgs e)
        {
            switchEncTab(1);
        }

        private void encBtnLeft_Click(object sender, EventArgs e)
        {
            switchEncTab(-1);
        }

        private void updateTextFields(object sender, EventArgs e)
        {
            charSearchTxt.Text = chars[charTabCtrl.SelectedIndex].name;
            encTxt.Text = encounters[encTabCtrl.SelectedIndex].name;
        }

        private void charSearch_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chars.Count; i++)
            {
                if (chars[i].name.Contains(charSearchTxt.Text))
                {
                    charCounter = i;
                    charSearchTxt.Text = chars[i].name;
                }
            }
        }

        private void encSearch_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < encounters.Count; i++)
            {
                if (encounters[i].name.Contains(encSearch.Text))
                {
                    enCounter = i;
                    encTxt.Text = encounters[i].name;
                }
            }
        }

        private void charTxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                charSearch_Click(sender, e);
            }
        }

        private void enBtnLeft_Click(object sender, EventArgs e)
        {
            enemyCounter--;
            enSearchTxt.Text = encounters[enCounter].enemies[enemyCounter.CoolModulo(encounters[enCounter].enemies.Length)].name;
        }

        private void enBtnRight_Click(object sender, EventArgs e)
        {
            enemyCounter++;
            int temp = enemyCounter.CoolModulo(encounters[enCounter].enemies.Length);
            enSearchTxt.Text = encounters[enCounter].enemies[temp].name;
        }

        private void attackCharToNPC_Click(object sender, EventArgs e)
        {
            if (encounters[enCounter].enemies[enemyCounter].dead)
            {
                log("[" + logCounter++.ToString() + "] Enemy is dead!");
            }
            encounters[enCounter].enemies[enemyCounter].dead = attack(chars[charCounter], encounters[enCounter].enemies[enemyCounter]);
            updateTabs(charCounter, enCounter, enemyCounter);
        }

        private void enBtnSearch_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < encounters[enCounter].enemies.Length; i++)
            {
                if (encounters[enCounter].enemies[i].name.Contains(enSearchTxt.Text))
                {
                    enemyCounter = i;
                    enSearchTxt.Text = encounters[enCounter].enemies[i].name;
                }
            }
        }

        private void switchEncTab(int modifier)
        {
            if (modifier < 0)
            {
                encTabCtrl.SelectedIndex = (encTabCtrl.SelectedIndex + modifier < 0) ? encTabCtrl.TabCount - 1 : encTabCtrl.SelectedIndex + modifier;
            }
            else
            {
                encTabCtrl.SelectedIndex = (encTabCtrl.SelectedIndex + modifier) % encTabCtrl.TabCount;
            }
            encSearch.Text = encounters[encTabCtrl.SelectedIndex].enemies.First().name;
            enCounter += modifier;
            enCounter = enCounter.CoolModulo(encTabCtrl.TabCount);
        }

        private void encTabCtrl_TabIndexChanged(object sender, EventArgs e)
        {
            enCounter = encTabCtrl.TabIndex;
        }

        private bool attack(Entity en1, Entity en2)
        {
            en2.physHealth -= rand.Next(en1.low, en2.high);
            return (en2.physHealth <= 0);
        }

        private void log(string str)
        {
            if (logOutputTxtBx.Items.Count > 3)
            {
                logOutputTxtBx.Items.RemoveAt(0);
            }
            logOutputTxtBx.Items.Add(str);
        }
    }
}
