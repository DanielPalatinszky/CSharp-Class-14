using MathLibrary;
using System;
using System.Collections.Generic;

namespace CSharp_Class_14
{
    class Program
    {
        static void Main(string[] args)
        {
            // Vegyük észre, hogy vannak olyan metódusok, melyek minden egyes típuson elérhetőek (primitívtől kezdve az osztályokig, mindenen):
            var i = 10;
            i.ToString();
            i.GetHashCode();
            i.Equals(10);

            // Honnan jönnek ezek?
            // C#-ban van egy kitüntetett őse minden egyes típusnak, amiből mindenki származik
            // Ez a típus van a C# típushierarchia gyökerében/csúcsán
            // Ő az object
            object a = 10;
            object b = new List<int>();

            // Ha készítünk egy saját osztályt, akkor az is alapértelmezetten leszáármazik belőle, nem kell kiírnunk

            // Hogy ténylegesen minden típus leszármazik-e belőle vagy a háttérben erős trükközések vannak, az nem tiszta!

            // Mire jó ez?
            // Polimorfizmus!
            // Próbáljunk meg készíteni egy listához hasonló típust, amely tetszőleges típust képes tárolni!
            var list = new List<object>();
            list.Add(10);
            list.Add("");
            list.Add(new List<int>());

            // Mi ezzel a baj?
            // Egyrészt nagyon lassú, mivel az object-ként való tárolás úgynevezett dobozolást (boxing) végez!
            // Ez azt jelenti, hogy valójában becsomagoljuk az értékeket egy object-be, majd amikor vissza akarjuk szerezni az értéket, akkor kicsomagoljuk (unboxing)

            // Másrészt nem biztonságos, mivel egy object-ről nem tudjuk hatékonyan eldönteni, hogy pontosan milyen típust takar/tárol

            // Hogyan készíthetnénk egy olyan osztályt ami biztonságosan és gyorsan képes tetszőleges típust kezelni?

            //--------------------------------------------------

            // Készítsünk egy saját lista osztályt, amely az ismert módon tömböt használ az elemek tárolására
            // Van egy kis gond viszont! Nem tudjuk milyen típust fog tárolni a lista!
            // Nem gond, készítsük el először az egész számokat tartalmazó változatot:
            var intList = new IntList();
            intList.Add(1);
            intList.Add(2);
            intList.Add(3);
            intList.Remove(2);

            // Készítsük el a string-ekete tartalmazó változatot is:
            var stringList = new StringList();
            stringList.Add("A");
            stringList.Add("B");
            stringList.Add("C");
            stringList.Remove("B");

            // Ha megnézzük a két lista osztályt, akkor észrevehetjük, hogy a lista működése szempontjából mindegy milyen típust tárol
            // Tehát ha lenne lehetőségünk arra, hogy ne a lista defininiálása közben mondjuk meg a tárolni kívánt típust, hanem a lista példányosításakor, akkor tökéletesen működne
            // Erre valók a generikus típusok:
            var myList = new MyList<int>(); // Létrehozáskor megmondom, hogy a T valójában int

            // Hányféle generikus típusa lehet egy osztálynak?
            // Bármennyi:
            var example = new Example1<int, float, List<int>, string>();

            // Valójában önmagában egy metódus is használhat generikus típusokat:
            var i1 = 10;
            var i2 = 20;
            Swap<int>(ref i1, ref i2);

            // Valójában a fordító van olyan okos, hogy a paraméterekből kitalálja, hogy mi a generikus típus:
            Swap(ref i1, ref i2);

            //--------------------------------------------------

            // A generikus típusok nagyon hasznosak, de egy dolgot elveszítünk a használatukkal
            // Fordítási időben semmit nem tudunk a generikus típusról
            // Például:
            Test1(new A());

            // Előfordulhat azonban, hogy szükségünk van bizonyos típusinformációkra, akkor is amikor generikus típusunk van
            // Ilyenkor megtehetjük, hogy a generikus típusra bizonyos megkötéseket teszünk a where kulcsszó segítségével:
            var b1 = new B<A>();

            // Ezáltal a T-ről biztosan tudjuk, hogy azt tudja amit A tud:
            b1.Method(new A());

            // Természetesen a where metóduson is működik:
            Test2(new A());

            // Milyen megkötéseket tehetünk még?

            // Mondhatjuk, hogy a generikus típus interfacet valósít meg (lásd: Test3 metódus)
            // Továbbadhatom a megadott generikus típust akár (lásd: Test4 metódus)
            // A class kulcsszó segítségével mondhatom azt, hogy a generikus kulcsszónak egy class-nak kell lennie (lásd: Test5 metódus)
            // A struct kulcsszó segítségével mondhatom azt, hogy a generikus kulcsszónak egy struct-nak kell lennie (lásd: Test6 metódus)
            // A new() segítségével mondhatom azt, hogy a generikus típusnak rendelkeznie kell egy paraméter nélküli konstruktorral, így akár példányt is létrehozhatok (lásd: Test7)
            // Van még pár, de ezek a legfontosabbak

            //--------------------------------------------------

            // Előfordulhat, hogy szükségünk van arra, hogy nem nullozható típusokat null-ként kezeljünk (pl. float, double, int, azaz értéktípusokat)
            // Ezt megoldhatjuk mi magunk is azáltal, hogy létrehozunk egy generikus csomagolóosztályt, ami értéktípusokat tárol
            // Szerencsénkre a C# beépítve is tartalmaz egy ilyen osztályt: Nullable<T>

            //float f1 = null; // Nem megy
            Nullable<float> f2 = null; // Megy

            // A ? operátor segítségével gyorsabban is készíthetünk nullable típust:
            float? f3 = null;

            // Hogyan vizsgálhatjuk, hogy egy nullable-nek null az értéke?

            // HasValue és Value:
            if (f3.HasValue)
                Console.WriteLine(f3.Value);

            // == és !=
            if (f3 != null)
                Console.WriteLine(f3.Value);

            // C# 7-től az is kulcsszó segítségével:
            if (f3 is float value)
                Console.WriteLine(value);

            // Hogyan konvertálhatunk nullable-t értéktípussá?

            // A ?? (null coalescing) operátor segítségével:
            var val1 = f3 ?? 0; // Ha f3-nak van értéke, akkor a val1 változó értéke legyen f3 értéke, ha viszont f3 null, akkor a val1 változó értéke legyen 0

            // Kasztolással:
            //var val2 = (float)f3; // Vigyázz, mert hiba keletkezhet

            // ??= operátor segítségével nem kell változóba helyeznünk az értéket:
            List<int> numbers = null;
            (numbers ??= new List<int>()).Add(10); // Ha a numbers nem null, akkor adj hozzá 10-et, különben a numbers legyen egy új lista és adj hozzá 10-et

            //--------------------------------------------------

            // Típusokban más típusokat is létrehozhatunk (lásd: NestedTypes.cs)

            // Alapesetben a beágyazott típus csak az őt tartalmazó típuson belül látszik:
            //var n1 = new N1.N2(); // Nem megy (szülő nevén keresztül érhetnénk el)

            // Azonban a beágyazott típusokra is tehetünk hozzáférés módosítókat (private, protected, public) (alapból private):
            var m1 = new M1.M2(); // A szülő nevén keresztül érhetjük el

            // A protected értelemszerűen az eddig tanultak alapján működik

            // Mi a helyzet a nem beágyazott típusok esetén?
            // Ha egy namespace-ben levő típusra teszünk láthatóság módosítokat, akkor teljesen más eredményt érünk el
            // Ugyanis ebben az esetben a láthatóság módosító azt szabályozza, hogy az adott projekten kívül látszik-e a típus

            // A jelenlegi kódunk egy solution-ben és azon belül egy projektben van
            // Azonban a solution több projektet is tartalmazhat
            // Például létrehozhatunk egy "Class Library" projektet, amiből egy dll fájl fog keletkezni
            // A dll fájl lényegében egy függvényeket és típusokat tartalmazó könyvtár (pl. MathLibrary tele matematikai típusokkal és függvényekkel)
            // Ezt a dll-t a saját projektünkben referálhatjuk mint projekt (ha ugyanazon a solution-ön belül vagyunk) vagy akár mint külön dll fájl kitallózva (ha ugyanaz vagy más solution)
            // Így felhasználhatjuk az adott könyvtárban levő típusokat és függvényeket

            // Összefoglaló néven a különböző projekteket (kise ferdítéssel) assembly-nek nevezzük

            // Hozzunk létre egy MathLibrary nevű "Class Library" projektet és benne egy egyszerű típus 1 művelettel (Solution Explorer)
            // Majd ezen projekt "References" füle alatt adjuk hozzá (Solution Explorer)
            // Így tudjuk használni:
            MyMath.DoMath();

            // Ezen tudás birtokában ha public egy namespace-beli típus, akkor bárki láthatja, ahogyan az imént láttuk

            // Azonban se private, se protected nem lehet egy namespace-ben levő típus
            // Ahhoz, hogy az adott típust csak a projekten belül lehessen látni internal-nak kell jelölni:
            //HiddenMath.DoMath(); // Nem megy

            // internal lehet nem namespace-beli típus is!
            // Nem csak típus, de ahogy eddig láttuk a tagok is kaphatnak láthatóság módosítókat, így természetesen internal-t is

            // Van még két láthatóság módosító:
            // protected internal: a típus vagy tag csak az adott assembly-ben vagy egy másik assembly-ben levő leszármazottból érhető el
            // private protected: a típus vagy tag csak az adott assembly-ben levő adott osztály vagy annak leszármazottaiban érhető el
        }

        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        static void Test1<T>(T t)
        {
            //t.Method(); // Nem megy
        }

        static void Test2<T>(T t) where T : A
        {
            t.Method();
        }

        static void Test3<T>(T t) where T : ICloneable
        {

        }

        static void Test4<T>(T t) where T : List<T>
        {

        }

        static void Test5<T>(T t) where T : class
        {

        }

        static void Test6<T>(T t) where T : struct
        {

        }

        static void Test7<T>(T t) where T : new()
        {
            new T();
        }
    }
}
