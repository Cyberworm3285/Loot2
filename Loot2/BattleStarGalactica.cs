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

namespace Loot2
{
    public partial class BattleStarGalactica : Form
    {
        private List<Character> chars = new List<Character>();
        private List<Encounter> encounters = new List<Encounter>();
        private int charIndex = 0;
        private int encIndex = 0;

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
            updateTabs();
        }

        private void updateTabs()
        {
            charTabCtrl.TabPages.Clear();
            encTabCtrl.TabPages.Clear();
            foreach (Character c in chars)
            {
                TabPage temp = new TabPage(c.shortCap);
                ListBox lBox = new ListBox();
                lBox.Name = c.name + "_ListBox";
                lBox.Location = new Point(0, 0);
                lBox.Size = new Size(encTabCtrl.Width - 10, encTabCtrl.Height - 20);
                lBox.Items.Add("Name: " + c.name);
                if (c.attributeNames.Length != c.attributeValues.Length) throw new Exception("Uneven attribute-lenghts");
                for (int i = 0; i < c.attributeNames.Length; i++)
                {
                    lBox.Items.Add(c.attributeNames[i] + ": " + c.attributeValues[i]);
                }
                temp.Controls.Add(lBox);
                charTabCtrl.TabPages.Add(temp);
            }
            foreach (Encounter enc in encounters)
            {
                TabPage temp = new TabPage(enc.shortCap);
                ListBox lBox = new ListBox();
                lBox.Name = enc.name + "_ListBox";
                lBox.Location = new Point(0, 0);
                lBox.Size = new Size(encTabCtrl.Width - 10, encTabCtrl.Height - 20);
                lBox.Items.Add("Name: " + enc.name);
                foreach (Enema en in enc.enemies)
                {
                    lBox.Items.Add("-Name: " + en.name);
                    lBox.Items.Add("--HP: " + en.health);
                    lBox.Items.Add("--EW: " + en.elementalWeakness);
                    lBox.Items.Add("--ED: " + en.dealsElementalDamge);
                }
                temp.Controls.Add(lBox);
                encTabCtrl.TabPages.Add(temp);
            }
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
        }

        private void charBtnLeft_Click(object sender, EventArgs e)
        {
            charTabCtrl.SelectedIndex = --charTabCtrl.SelectedIndex % charTabCtrl.TabCount;
        }

        private void encBtnRight_Click(object sender, EventArgs e)
        {
            encTabCtrl.SelectedIndex = ++encTabCtrl.SelectedIndex % encTabCtrl.TabCount;
        }

        private void encBtnLeft_Click(object sender, EventArgs e)
        {
            encTabCtrl.SelectedIndex = --encTabCtrl.SelectedIndex % encTabCtrl.TabCount;
        }

        private void updateTextFields(object sender, EventArgs e)
        {
            charTxt.Text = chars[charTabCtrl.SelectedIndex].name;
            encTxt.Text = encounters[encTabCtrl.SelectedIndex].name;
        }

        private void charSearch_Click(object sender, EventArgs e)
        {
            SearchTabWithText(charTabCtrl, charTxt.Text);
        }

        private void encSearch_Click(object sender, EventArgs e)
        {
            SearchTabWithText(encTabCtrl, charTxt.Text);
        }

        private void SearchTabWithText(TabControl ctrl, string value)
        {
            for (int i = 0; i < charTabCtrl.TabCount; i++)
            {
                if (charTabCtrl.TabPages[i].Text.Contains(charTxt.Text))
                {
                    charTabCtrl.SelectTab(i);
                    return;
                }
            }
        }
    }
}
