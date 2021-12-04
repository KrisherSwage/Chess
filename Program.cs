using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Chess
{
    class Program
    {
        public class Сhessboard
        {
            public int x;
            public int InX() //метод ввода координат с проверкой
            {
                while (true)
                {
                    Console.WriteLine("Введите целое число от 1 до 8");
                    if (int.TryParse(Console.ReadLine(), out int a) && (a > 0) && (a < 9))
                    {
                        x = a;
                        break;
                    }
                }
                return (x);
            }

            public bool BorW(int k, int l, int m, int n) //метод проверки на цвет
            {
                if ((k + l) % 2 == (m + n) % 2)
                {
                    return (true); //если одного цвета
                }
                else
                {
                    return (false); //если разных цветов
                }
            }

            public bool Knight(int k, int l, int m, int n) //метод для хода коня
            {

                if (k + 2 == m) //рассматривается каждая координата отдельно, если совпадет, то вернет true
                {
                    if (l + 1 == n)
                    {
                        return (true);
                    }
                    else
                    {
                        if (l - 1 == n)
                        {
                            return (true);
                        }
                    }
                }

                if (k - 2 == m)
                {
                    if (l + 1 == n)
                    {
                        return (true);
                    }
                    else
                    {
                        if (l - 1 == n)
                        {
                            return (true);
                        }
                    }
                }

                if (k + 1 == m)
                {
                    if (l + 2 == n)
                    {
                        return (true);
                    }
                    else
                    {
                        if (l - 2 == n)
                        {
                            return (true);
                        }
                    }
                }

                if (k + 1 == m)
                {
                    if (l + 2 == n)
                    {
                        return (true);
                    }
                    else
                    {
                        if (l - 2 == n)
                        {
                            return (true);
                        }
                    }
                }

                return (false); //вернет, если нельзя встать в клетку с координатами m n
            }
            public bool Bishop(int k, int l, int m, int n) //метод для хода слона
            {
                if(Math.Abs(k-m) == Math.Abs(l - n)) //слон по диагонали ходит => для него такое выражение
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }

            public bool Castle(int k, int l, int m, int n) //метод для хода ладьи
            {
                if ((k == m) || (l == n)) //ладья ходит по прямой => для него такое выражение
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }

            public bool Queen(int k, int l, int m, int n) //метод для хода ферзя
            {
                if ((Math.Abs(k - m) == Math.Abs(l - n)) || (k == m) || (l == n)) //объединяет в себе слона и ладью
                {
                    return (true);
                }
                else
                {
                    return (false);
                }
            }
        }
        
        static void Main(string[] args)
        {
            using (var sw = new StreamWriter("log.txt", true, Encoding.UTF8))
            {
                var сhessboard = new Сhessboard(); //экземпляр класса шахматной доски
                int k, l, m, n, getInput;
                int counter = 1; //счетчик для нумерации записей в файле
                bool flag = true;
                List<int> coordsForKnight = new List<int>(); //список для хранения вторичных координат коня

                while (flag)
                {
                    while (true)
                    {
                        Console.WriteLine("Введите 1 для работы программы; 0 для выхода.");
                        if (int.TryParse(Console.ReadLine(), out int a) && ((a == 0) || (a == 1)))
                        {
                            getInput = a;
                            break;
                        }
                    }

                    switch (getInput)
                    {
                        case (0):                                                                                       //в файл добавить запись о выходе из программы
                            Console.WriteLine("Вы вышли из программы.");
                            sw.WriteLine($"Завершена работа с текущим запуском программы. "); 
                            flag = false;
                            break;

                        case (1):                                                                                    
                            Console.WriteLine("Введите координаты первой клетки. Далее координаты фигуры.");
                            k = сhessboard.InX();
                            l = сhessboard.InX();
                            Console.WriteLine("Введите координаты первой клетки. Далее куда должна встать фигура.");
                            m = сhessboard.InX();
                            n = сhessboard.InX();
                            Console.WriteLine();                                                                    
                            
                            sw.WriteLine($"{counter}) Координата 1-й точки ({k};{l}); координата 2-й точки ({m};{n}) "); //в файл координаты 1й и 2й точки //в файл добавить запись о i-той работе программы

                            coordsForKnight.Add(k + 2);
                            coordsForKnight.Add(l + 1);

                            coordsForKnight.Add(k + 2);
                            coordsForKnight.Add(l - 1);

                            coordsForKnight.Add(k - 2);
                            coordsForKnight.Add(l + 1);

                            coordsForKnight.Add(k - 2);
                            coordsForKnight.Add(l - 1);


                            coordsForKnight.Add(k + 1);
                            coordsForKnight.Add(l + 2);

                            coordsForKnight.Add(k + 1);
                            coordsForKnight.Add(l - 2);

                            coordsForKnight.Add(k - 1);
                            coordsForKnight.Add(l + 2);

                            coordsForKnight.Add(k - 1);
                            coordsForKnight.Add(l - 2);                     //...

                            //выше заполнение списка для промежуточных координат коня

                            if (сhessboard.BorW(k, l, m, n))
                            {
                                Console.WriteLine("Клетки одного цвета.");
                                if ((k + l) % 2 == 0)
                                {
                                    Console.WriteLine("Цвет - белый.");
                                    sw.WriteLine($"Клетки одного цвета - белого. ");//соответствующая запись в файл
                                }
                                else
                                {
                                    Console.WriteLine("Цвет - черный.");
                                    sw.WriteLine($"Клетки одного цвета - черного. ");//соответствующая запись в файл
                                }
                            }
                            else
                            {
                                Console.WriteLine("Клетки разного цвета.");                                                     //соответствующая запись в файл
                                if ((k + l) % 2 == 0)
                                {
                                    Console.WriteLine($"Цвет клетки ({k},{l}) - белый. Цвет клетки ({m},{n}) - черный.");
                                    sw.WriteLine($"Клетки разного цвета. ({k},{l}) - белый; ({m},{n}) - черный. ");//соответствующая запись в файл
                                }
                                else
                                {
                                    Console.WriteLine("Цвет клетки (k,l) - черный. Цвет клетки (m,n) - белый.");
                                    sw.WriteLine($"Клетки разного цвета. ({k},{l}) - черный; ({m},{n}) - белый. ");//соответствующая запись в файл
                                }
                            }
                            Console.WriteLine();

                            int figure;
                            while (true)
                            {
                                Console.WriteLine("Введите 1 чтобы выбрать ладью; 2 чтобы выбрать коня; 3 чтобы выбрать слона; 4 чтобы выбрать ферзя.");
                                if (int.TryParse(Console.ReadLine(), out int a) && ((a == 1) || (a == 2) || (a == 3) || (a == 4)))
                                {
                                    figure = a;
                                    break;
                                }
                            }                                                                                               //записать какая фигура выбрана
                            switch (figure)
                            {
                                case (1): //ладья
                                    if (сhessboard.Castle(k, l, m, n))
                                    {
                                        Console.WriteLine($"Ладья из клетки ({k},{l}) может за один ход попасть в клетку ({m},{n}). То есть угрожает!");
                                        sw.WriteLine($"Ладья из клетки ({k},{l}) может за один ход попасть в клетку ({m},{n}). То есть угрожает! ");//соответствующая запись в файл
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Ладья из клетки ({k},{l}) не может за один ход попасть в клетку ({m},{n}). То есть не угрожает!");

                                        Console.WriteLine($"Чтобы ладье попасть из ({k},{l}) в ({m},{n}) за два хода, надо сначала встать на клетку ({k},{n}) или ({m},{l}).");
                                        sw.WriteLine($"Чтобы ладье попасть из ({k},{l}) в ({m},{n}) за два хода, надо сначала встать на клетку ({k},{n}) или ({m},{l}). ");//соответствующая запись в файл
                                    }
                                    break;

                                case (2): //конь
                                    if (сhessboard.Knight(k, l, m, n))
                                    {
                                        Console.WriteLine($"Конь из клетки ({k},{l}) может за один ход попасть в клетку ({m},{n}). То есть угрожает!");
                                        sw.WriteLine($"Конь из клетки ({k},{l}) может за один ход попасть в клетку ({m},{n}). То есть угрожает! ");//соответствующая запись в файл
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Конь из клетки ({k},{l}) не может за один ход попасть в клетку ({m},{n}). То есть не угрожает!");

                                        int prov = 0;
                                        int j = 1;
                                        for (int i = 0; i < 16; i += 2)
                                        {
                                            if (сhessboard.Knight(coordsForKnight[i], coordsForKnight[j], m, n))
                                            {
                                                Console.WriteLine($"Чтобы коню попасть из ({k},{l}) в ({m},{n}) за два хода, надо сначала встать на клетку ({coordsForKnight[i]},{coordsForKnight[j]}).");
                                                prov++;
                                                sw.WriteLine($"Чтобы коню попасть из ({k},{l}) в ({m},{n}) за два хода, надо сначала встать на клетку ({coordsForKnight[i]},{coordsForKnight[j]}). ");//соответствующая запись в файл
                                            }
                                            j += 2;
                                        }
                                        if (prov == 0)
                                        {
                                            Console.WriteLine($"Конь не может попасть в клетку ({m},{n}).");
                                            sw.WriteLine($"Конь не может попасть в клетку ({m},{n}). ");//соответствующая запись в файл
                                            break;
                                        }
                                    }
                                    break;

                                case (3): //слон
                                    if (сhessboard.Bishop(k, l, m, n))
                                    {
                                        Console.WriteLine($"Слон из клетки ({k},{l}) может за один ход попасть в клетку ({m},{n}). То есть угрожает!");
                                        sw.WriteLine($"Слон из клетки ({k},{l}) может за один ход попасть в клетку ({m},{n}). То есть угрожает! ");//соответствующая запись в файл
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Слон из клетки ({k},{l}) не может за один ход попасть в клетку ({m},{n}). То есть не угрожает!");

                                        if (сhessboard.BorW(k, l, m, n))
                                        {
                                            Console.WriteLine($"Чтобы слону попасть из ({k},{l}) в ({m},{n}) за два хода, надо сначала встать на клетку ({l},{k}) или ({n},{m}).");
                                            sw.WriteLine($"Чтобы слону попасть из ({k},{l}) в ({m},{n}) за два хода, надо сначала встать на клетку ({l},{k}) или ({n},{m}). ");//соответствующая запись в файл
                                        }
                                        else
                                        {
                                            Console.WriteLine($"Слон не может попасть в клетку ({n},{m}).");
                                            sw.WriteLine($"Слон не может попасть в клетку ({n},{m}). ");//соответствующая запись в файл
                                        }
                                    }
                                    break;

                                case (4): //ферзь
                                    if (сhessboard.Queen(k, l, m, n))
                                    {
                                        Console.WriteLine($"Ферзь из клетки ({k},{l}) может за один ход попасть в клетку ({m},{n}). То есть угрожает!");
                                        sw.WriteLine($"Ферзь из клетки ({k},{l}) может за один ход попасть в клетку ({m},{n}). То есть угрожает! ");//соответствующая запись в файл
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Ферзь из клетки ({k},{l}) не может за один ход попасть в клетку ({m},{n}). То есть не угрожает!");

                                        Console.WriteLine($"Чтобы ферзю попасть из ({k},{l}) в ({m},{n}) за два хода, надо сначала встать на клетку или ({k},{n}), или ({m},{l}).");
                                        sw.WriteLine($"Чтобы ферзю попасть из ({k},{l}) в ({m},{n}) за два хода, надо сначала встать на клетку или ({k},{n}), или ({m},{l}). ");//соответствующая запись в файл
                                    }
                                    break;
                            }

                            break;
                    }

                    Console.WriteLine("___________________________________________________________________________________________");
                    sw.WriteLine($"");//соответствующая запись в файл
                    counter++;

                }
            }

        }
    }
}
