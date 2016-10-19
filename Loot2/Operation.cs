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
        /// <summary>
        ///     speichert alle Möglichen Namen für diese Operation
        /// </summary>
        public List<string> attribName { get; set; }
        public Intervall intervall { get; set; }
        public bool fixedOp;

        public Operation()
        {
            attribName = new List<string>();
        }
    }

    public struct Intervall
    {
        public int low, high;

        /// <summary>
        ///     Erstellt ein neues <see cref="Intervall"/>
        /// </summary>
        /// <param name="l">Untere Grenze des intervalls</param>
        /// <param name="h">Obere Grenze des Intervalls</param>
        public Intervall(int l, int h)
        {
            low = l;
            high = h;
        }
    }
}
