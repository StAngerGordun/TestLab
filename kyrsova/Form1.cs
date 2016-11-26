using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace kyrsova
{
  
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        dsu dsu = new dsu();
        
        int set, find, setX, setY, X,Y;
        int x1 = 100;
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Ви хочете закрити програму?", "Вихід", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) Application.Exit();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Для створення одноелементної множини натисніть кномку Створити, для пошуку елементу натисніть Пошук, для об'єднання множин їх номерами натисніть кнопку Об'єднання1, для об'єднання множин за елементами, які їм належать  натисніть кнопку Об'єднання2", "Допомога", MessageBoxButtons.OK);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("", "Виконавець");
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {//малювання множин
            float wid = pictureBox1.Width;

            Font drawFont = new Font("Arial", 23);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            SolidBrush drawBrush1 = new SolidBrush(Color.Red);//для виділення множин, що шукаються
            float y = 0;

            for (int i = 0; i < dsu.parent2.Count; i++)
            {
                e.Graphics.DrawRectangle(Pens.Black, 10, y, 50 * dsu.parent2[i].Count, 60);// прямокутник, в який будуть вписуватися елементи множини
                for (int j = 0; j < dsu.parent2[i].Count; j++)
                {
                    if (y < pictureBox1.Height-10)
                    {
                        if (i == x1)//якщо поточна вершина є шуканою, то малюємо, використовуючи drawBrush1
                        {
                            if (((40) * j + 10) < wid-10)
                            {
                            PointF drawPoint = new PointF((40) * j + 15, y + 10);
                            e.Graphics.DrawEllipse(Pens.Red, (40) * j + 15, y + 10, 40, 40);
                            e.Graphics.DrawString(Convert.ToString(dsu.parent2[i][j]), drawFont, drawBrush1, drawPoint);
                            }
                            else
                            {
                               MessageBox.Show("Розширення екрану не дозволяє графічно зображувати бульше елементів", "Попередження");
                            }
                        }
                        else
                        {
                            if (((40) * j + 5)< wid-10)
                            {
                            PointF drawPoint = new PointF((40) * j + 15, y + 10);
                            e.Graphics.DrawEllipse(Pens.Black, (40) * j + 15, y + 10, 40, 40);
                            e.Graphics.DrawString(Convert.ToString(dsu.parent2[i][j]), drawFont, drawBrush, drawPoint);
                            }
                             else
                            {
                              MessageBox.Show("Розширення екрану не дозволяє графічно зображувати бульше елементів", "Попередження");
                            }
                        }
                    }
                    else
                    { MessageBox.Show("Розширення екрану не дозволяє графічно зображувати бiльше елементів", "Попередження"); }
                }
                y += 70;//крок зміщення малювання множин по координаті ОУ
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (Math.Abs( int.Parse(textBox1.Text)) > 99)
                    MessageBox.Show("Елемент не входить в діапазону");
                else
                    set = int.Parse(textBox1.Text);
                errorProvider1.SetError(textBox1, "");
            }
            catch
            { errorProvider1.SetError(textBox1, "Число не вірного типу або взагалі не введене"); }
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                find = int.Parse(textBox2.Text);
                errorProvider2.SetError(textBox2, "");
            }
            catch
            { errorProvider2.SetError(textBox2, "Число не вірного типу або взагалі не введене"); }
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                setX = int.Parse(textBox3.Text);
                errorProvider3.SetError(textBox3, "");
            }
            catch
            { errorProvider3.SetError(textBox3, "Число не вірного типу або взагалі не введене"); }
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                setY = int.Parse(textBox4.Text);
                errorProvider4.SetError(textBox4, "");
            }
            catch
            { errorProvider4.SetError(textBox4, "Число не вірного типу або взагалі не введене"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                pictureBox1.Visible = true;
                dsu.MakeSet(set);
                x1 = 100;
                pictureBox1.Invalidate();
            }
            else
            {
                MessageBox.Show("Заповніть правильно значення, яке треба додати", "Помилка вводу");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                if (dsu.Find(find)<=dsu.parent2.Count)
                {
                    MessageBox.Show(Convert.ToString(dsu.Find(find)),"Номер множини, якій належить даний елемент");
                    x1 = dsu.Find(find);
                    pictureBox1.Invalidate();
                }
                else
                    MessageBox.Show("Такого елементу не існує", "Помилка");
            }
            else
            {
                MessageBox.Show("Заповніть правильно значення, яке треба знайти", "Помилка вводу");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox4.Text != "")
            {
                dsu.Unite(setX, setY);
                x1 = 100;  
            }
            else
            {
                MessageBox.Show("Заповніть правильно номера множин, які треба об'єднати", "");
            }
            pictureBox1.Invalidate();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                X = int.Parse(textBox5.Text);
                errorProvider5.SetError(textBox5, "");
            }
            catch
            { errorProvider5.SetError(textBox5, "Число не вірного типу або взагалі не введене"); }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Y = int.Parse(textBox6.Text);
                errorProvider6.SetError(textBox6, "");
            }
            catch
            { errorProvider6.SetError(textBox6, "Число не вірного типу або взагалі не введене"); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "" && textBox6.Text != "")
            {
                dsu.Unite2(X, Y);
                x1 = 100;
            }
            else
            {
                MessageBox.Show("Заповніть правильно номера множин, які треба об'єднатfи", "Помилка вводу");
            }
            pictureBox1.Invalidate();
        }
    }
    
}
