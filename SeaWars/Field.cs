using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaWars
{
    public struct FieldParams
    {
        public int width;
        public int height;
        public int ships;
    }
    struct Field
    {    
       public FieldParams myfieldParams;
       public char[,] fieldSymbols;

        public Field(FieldParams fieldParams, char[,] newFielSymbols)
            {
               myfieldParams = fieldParams;
               fieldSymbols = newFielSymbols;
            }
    }
}
