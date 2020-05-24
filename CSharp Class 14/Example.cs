using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Class_14
{
    // Tetszőleges számú generikus típus
    class Example1<T1, T2, T3, T4>
    {
    }

    class A
    {
        public void Method()
        {

        }
    }

    // A T generikus típusnak A-nak vagy A egy leszármazottjának kell lennie (hiszen a polimorfizmus továbbra is működik)
    class B<T> where T : A
    {
        public void Method(T t)
        {
            t.Method();
        }
    }
}
