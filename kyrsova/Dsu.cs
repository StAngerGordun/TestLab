using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace kyrsova
{
    public class dsu
    {
        public List<List<int>> parent2 = new List<List<int>>();//кожен елемент List-а зберігає List, який зберігає елементи множин
        //створення одноелементної множини
        public void MakeSet(int x)
        {
            List<int> list = new List<int>();
            list.Add(x);
            bool flag = true;
            for (int i = 0; i < parent2.Count; i++)
            {//перевіряємо чи наявний елемент в List-е, який ми хочемо долучити
                if (parent2[i].Contains(x))
                {
                    flag = false;
                }
            }
            if (flag == true)
                parent2.Add(list);
            else MessageBox.Show("Дана множина вже існує", "Помилка вводу");
        }
        //метод пошуку елементу в множині, який повертає номер множини, якій належить даний елемент
        public int Find(int x)
        {
            int index = parent2.Count + 1;
            for (int i = 0; i < parent2.Count; i++)
                for (int j = 0; j < parent2[i].Count; j++)
                {
                    if (parent2[i].Contains(x))
                    {
                        index = i;
                    }
                }
            return index;    // повертає індекс множини, якій належить даний елемент або повернене значення буде більше, ніж можлива дожина parent2
        }

        // метод об'єднання множин за заданими номерами
        public void Unite(int x, int y)
        {
            int min, max;
            if (x < parent2.Count && y < parent2.Count)
            {
                //знаходження множини з меншою кількістю елементів
                if (parent2[x].Count >= parent2[y].Count)
                {
                    min = y; max = x;
                }
                else { min = x; max = y; }
                //поки менша множина не стане пустою вилучаємо з неї елементи і перезаписуємо їх в довшу множину
                while (parent2[min].Count != 0)
                {
                    int r = parent2[min].First();
                    parent2[min].Remove(r);
                    parent2[max].Add(r);
                }
                parent2.RemoveAt(min);
            }
            else
            {
                MessageBox.Show("Об'єднання даних множин не можливе, бо не існує одна або дві із множин", "Помилка");
            }
        }
        // об’єднання двох множин за елементами, що належать відповідним множинам
        public void Unite2(int x, int y)
        {
            int min, max;

            if (Find(x) <= parent2.Count && Find(y) <= parent2.Count)//перевіряємо чи існують множини, яким належать елементи
            {
                if (Find(x) != Find(y))//перевірка на належність одній множині
                {
                    if (parent2[Find(x)].Count >= parent2[Find(y)].Count)//знаходження множини з меншою кількістю елементів
                    {
                        min = Find(y); max = Find(x);
                    }
                    else { min = Find(x); max = Find(y); }
                    while (parent2[min].Count != 0) //поки менша множина не стане пустою вилучаємо з неї елементи і перезаписуємо їх в довшу множину
                    {
                        int r = parent2[min].First();
                        parent2[min].Remove(r);
                        parent2[max].Add(r);
                    }
                    parent2.RemoveAt(min);
                }
                else { MessageBox.Show("Елементи належать одній множині", "Повідомлення"); }
            }
            else
            {
                MessageBox.Show("Об'єднання даних множин не можливе, бо не існує одного або двох заданих елементів", "Помилка");
            }
        }

    }
}
