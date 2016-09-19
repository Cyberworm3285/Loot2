using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loot2
{
    /// <summary>
    ///     Speichert <see cref="Loot"/>-Attribute mit einem, oder ggf. mehreren Namen und einem Intervall von Werten 
    /// </summary>
    public class Operation
    {
        [YAXLib.YAXCollection(YAXLib.YAXCollectionSerializationTypes.Serially, SeparateBy = ",")]
        [YAXLib.YAXSerializeAs("Name")]
        [YAXLib.YAXAttributeForClass]
        /// <summary>
        ///     speichert alle Möglichen Namen für diese Operation
        /// </summary>
        public List<string> attribName { get; set; }
        [YAXLib.YAXCollection(YAXLib.YAXCollectionSerializationTypes.Serially, SeparateBy = ",")]
        [YAXLib.YAXSerializeAs("Intervall")]
        [YAXLib.YAXAttributeFor("")]
        public int[] intervall { get; set; }

        public Operation()
        {
            attribName = new List<string>();
            intervall = new int[2];
        }

        /// <summary>
        ///     befüllt das Objekt mit standard Werten
        ///     <para>(Ende des Baum-Aufrugfs)</para>
        /// </summary>
        public void createDummy(int number)
        {
            intervall[0] = 0;
            intervall[1] = 100;
            attribName.Add("DummyAttribut" + number + "_1");
            attribName.Add("DummyAttribut" + number + "_2");
        }
    }
}
