using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp_Class_14
{
    class IntList
    {
        public int Length { get; private set; }
        public int Capacity { get; private set; } = 1;

        private int[] array;

        public IntList()
        {
            array = new int[Capacity];
        }

        public void Add(int element)
        {
            if (Length + 1 > Capacity)
            {
                array = Resize(Capacity * 2);
                Capacity = array.Length;
            }

            array[Length++] = element;
        }

        public bool Remove(int element)
        {
            var index = Array.IndexOf(array, element);

            if (index == -1)
                return false;

            for (int i = index, j = index + 1; j < Length; i++, j++)
            {
                array[i] = array[j];
            }

            Length--;

            return true;
        }

        private int[] Resize(int newSize)
        {
            var newArray = new int[newSize];

            for (int i = 0; i < Length; i++)
            {
                newArray[i] = array[i];
            }

            return newArray;
        }
    }

    class StringList
    {
        public int Length { get; private set; }
        public int Capacity { get; private set; } = 1;

        private string[] array;

        public StringList()
        {
            array = new string[Capacity];
        }

        public void Add(string element)
        {
            if (Length + 1 > Capacity)
            {
                array = Resize(Capacity * 2);
                Capacity = array.Length;
            }

            array[Length++] = element;
        }

        public bool Remove(string element)
        {
            var index = Array.IndexOf(array, element);

            if (index == -1)
                return false;

            for (int i = index, j = index + 1; j < Length; i++, j++)
            {
                array[i] = array[j];
            }

            Length--;

            return true;
        }

        private string[] Resize(int newSize)
        {
            var newArray = new string[newSize];

            for (int i = 0; i < Length; i++)
            {
                newArray[i] = array[i];
            }

            return newArray;
        }
    }

    // Ez az osztály létrehozáskor át fog venni egy TÍPUST, amit mi T-nek nevezünk, amely a generikus típusa lesz az osztálynak
    // Ezt a T-t bárhol használhatjuk az osztályban, ahol egy típust használnánk (kisebb megkötések azért vannak)
    class MyList<T>
    {
        public int Length { get; private set; }
        public int Capacity { get; private set; } = 1;

        // A tömb T típusú
        private T[] array;

        public MyList()
        {
            array = new T[Capacity];
        }

        // T típusú elemet adok a tömbhöz
        public void Add(T element)
        {
            if (Length + 1 > Capacity)
            {
                array = Resize(Capacity * 2);
                Capacity = array.Length;
            }

            array[Length++] = element;
        }

        // T típusú elemet törlök a tömbből
        public bool Remove(T element)
        {
            var index = Array.IndexOf(array, element);

            if (index == -1)
                return false;

            for (int i = index, j = index + 1; j < Length; i++, j++)
            {
                array[i] = array[j];
            }

            Length--;

            return true;
        }

        private T[] Resize(int newSize)
        {
            var newArray = new T[newSize];

            for (int i = 0; i < Length; i++)
            {
                newArray[i] = array[i];
            }

            return newArray;
        }
    }
}
