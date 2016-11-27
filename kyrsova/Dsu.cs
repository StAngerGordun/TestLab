using System;
using System.Collections.Generic;
using System.Linq;

namespace kyrsova
{
    public class dsu
    {
        public List<List<int>> parent { get; private set; }//кожен елемент List-а зберігає List, який зберігає елементи множин

        public dsu()
        {
            parent = new List<List<int>>();//кожен елемент List-а зберігає List, який зберігає елементи множин
        }
        //створення одноелементної множини
        public void MakeSet(int union)
        {
            if (!parent.Any(t => t.Contains(union)))
                parent.Add(new List<int> { union });
            else throw new Exception("Помилка вводу. Дана множина вже існує");
        }

        //метод пошуку елементу в множині, який повертає номер множини, якій належить даний елемент
        public int Find(int elementForFind)
        {
            for (int i = 0; i < parent.Count; i++)
                if (parent[i].Contains(elementForFind))
                    return i;
            throw new KeyNotFoundException("Елемент не знайдено");// повертає індекс множини, якій належить даний елемент або повернене значення буде більше, ніж можлива дожина parent2
        }

        // метод об'єднання множин за заданими номерами
        public void UniteByNumbers(int firstNumber, int secondNumber)
        {
            if (firstNumber >= parent.Count || secondNumber >= parent.Count)
                throw new ArgumentException("Об'єднання даних множин не можливе, бо не існує одна або дві із множин");
            //знаходження множини з меншою кількістю елементів
            int min, max;
            if (parent[firstNumber].Count >= parent[secondNumber].Count)
            {
                min = secondNumber;
                max = firstNumber;
            }
            else
            {
                min = firstNumber; 
                max = secondNumber;
            }
            //поки менша множина не стане пустою вилучаємо з неї елементи і перезаписуємо їх в довшу множину
            parent[max].AddRange(parent[min]);
            parent.RemoveAt(min);
        }

        // об’єднання двох множин за елементами, що належать відповідним множинам
        public void UniteByElements(int firstElement, int secondElement)
        {
            var first = Find(firstElement);
            var second = Find(secondElement);
            if (first > parent.Count || second > parent.Count)//перевіряємо чи існують множини, яким належать елементи
                throw new ArgumentException("Об'єднання даних множин не можливе, бо не існує одного або двох заданих елементів");

            if (first == second)//перевірка на належність одній множині
                throw new Exception("Елементи належать одній множині");
            int min, max;
            if (parent[first].Count >= parent[second].Count) //знаходження множини з меншою кількістю елементів
            {
                min = second;
                max = first;
            }
            else
            {
                min = first; 
                max = second;
            }
            parent[max].AddRange(parent[min]);
            parent.RemoveAt(min);
        }

    }
}
