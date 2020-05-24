using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Class_14
{
    // Tetszőleges típusokat egymásba ágyazhatunk (kivétel enum-ban)
    // Tetszőleges mélységben megtehetjük (ajánlott max 1)
    class N1
    {
        class N2
        {
            class N3
            {

            }
        }

        struct N4
        {
            class N6
            {

            }
        }

        enum N5
        {
            
        }
    }

    // Beágyazott típuson láthatóság módosítók
    class M1
    {
        public class M2
        {

        }
    }
}
