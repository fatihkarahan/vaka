using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _vaka
{
    public class Program
    {
        public static string[] rectangle = { "00", "55" };
        public static string robot1 = "00N";
        public static string robot2 = "00N";

        static void Main(string[] args)
        {
            bool isFinishRobot1 = false;
            bool isFinishRobot2 = false;
        start: Console.WriteLine("1.Robot için Bulunduğunuz nokta : " + robot1);
            try
            {
                Console.WriteLine("1.Robot için Yön veya kordinat giriniz :");
            nPR1: string newPoint = Console.ReadLine().ToUpper();
                if (string.IsNullOrEmpty(newPoint))
                {
                    Console.WriteLine("1.Robot için Hatalı veya eksik yön girdiniz lütfen tekrar girin :");
                    goto nPR1;
                }
            //1.robot hareketi
            againR1: foreach (var c in newPoint)
                {
                    if (string.IsNullOrEmpty(newPoint) || newPoint.Contains(" "))
                    {
                        Console.WriteLine("1.Robot için Hatalı veya eksik yön girdiniz lütfen tekrar girin :");
                        goto nPR1;
                    }
                    int i = 0;
                    if (c.ToString().Contains("L") || c.ToString().Contains("R") || c.ToString().Contains("M"))
                    {
                        if (c.ToString() == "L")
                            robot1 = LPoint(robot1);
                        else if (c.ToString() == "R")
                            robot1 = RPoint(robot1);
                        else if (c.ToString() == "M")
                            robot1 = MPoint(robot1);
                        Console.WriteLine(c.ToString() + " hareketi sonrası bulunduğunuz konum :" + robot1);
                        i++;
                        if (c.ToString().Length == i)
                            isFinishRobot1 = true;
                    }
                    else
                    {
                        robot1 = AddPoint(robot1, newPoint);
                        Console.WriteLine(newPoint + " hareketi sonrası bulunduğunuz konum :" + robot1);
                        //1. robot hareketini bitirene kadar devam etsin diye eklendi
                        Console.WriteLine("1.Robot için Yön veya kordinat giriniz :");
                        newPoint = Console.ReadLine().ToUpper();
                        goto againR1;
                    }
                }
                if (!isFinishRobot1)
                {
                    Console.WriteLine("Hatalı veya eksik yön girdiniz lütfen tekrar girin :");
                    goto nPR1;
                }
                Console.WriteLine("2.Robot için Bulunduğunuz nokta : " + robot2);
                Console.WriteLine("2.Robot için Yön veya kordinat giriniz :");
            nPR2: newPoint = Console.ReadLine().ToUpper();
                if (string.IsNullOrEmpty(newPoint))
                {
                    Console.WriteLine("2.Robot için Hatalı veya eksik yön girdiniz lütfen tekrar girin :");
                    goto nPR2;
                }
            //2.robot haraketi
            againR2: foreach (char c in newPoint)
                {
                    int i = 0;
                    if (string.IsNullOrEmpty(newPoint) || newPoint.Contains(" "))
                    {
                        Console.WriteLine("1.Robot için Hatalı veya eksik yön girdiniz lütfen tekrar girin :");
                        goto nPR2;
                    }
                    if (c.ToString().Contains("L") || c.ToString().Contains("R") || c.ToString().Contains("M"))
                    {
                        if (c.ToString() == "L")
                            robot2 = LPoint(robot2);
                        else if (c.ToString() == "R")
                            robot2 = RPoint(robot2);
                        else if (c.ToString() == "M")
                            robot2 = MPoint(robot2);
                        Console.WriteLine(c.ToString() + " hareketi sonrası bulunduğunuz konum :" + robot2);
                        i++;
                        if (c.ToString().Length == i)
                            isFinishRobot2 = true;
                    }
                    else
                    {
                        robot2 = AddPoint(robot2, newPoint);
                        Console.WriteLine(newPoint + " hareketi sonrası bulunduğunuz konum :" + robot2);
                        //2. robot hareketini bitirene kadar devam etsin diye eklendi
                        Console.WriteLine("Bulunduğunuz nokta : " + robot2);
                        Console.WriteLine("Yön veya kordinat giriniz :");
                        newPoint = Console.ReadLine();
                        goto againR2;
                    }
                }
                if (!isFinishRobot2)
                {
                    Console.WriteLine("Hatalı veya eksik yön girdiniz lütfen tekrar girin :");
                    goto nPR2;
                }
                Console.WriteLine("2.Robotun bulunduğu konum :" + robot2);
                Console.WriteLine("1.Robotun bulunduğu konum :" + robot1);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                //log
                Console.WriteLine("Hatalı veya eksik yön girdiniz lütfen tekrar girin :");
                goto start;
            }

        }
        //dikdörtgenini sınırlarını aşmasın diye kontrol eklendi.
        private static string CheckLastPoint(string point)
        {
            //robotun yönünü tutuyor
            string direction = "";
            if (point.Length > 2)
                direction = point.Substring(2, 1);
            //küçük sınırlar aşılmasın diye eklendi
            if (Convert.ToInt32(point.Substring(0, 2)) < Convert.ToInt32(rectangle.First()))
            {
                if (Convert.ToInt32(point.Substring(0, 1)) < Convert.ToInt32(rectangle.First().Substring(0, 1)))
                    point = rectangle.First().Substring(0, 1) + point.Substring(1, 1);
                else if (Convert.ToInt32(point.Substring(1, 1)) < Convert.ToInt32(rectangle.First().Substring(1, 1)))
                    point = point.Substring(0, 1) + rectangle.First().Substring(1, 1);

            }
            //büyük sınırlar aşılmasın diye eklendi
            if (Convert.ToInt32(point.Substring(0, 2)) > Convert.ToInt32(rectangle.Last()))
                if (Convert.ToInt32(point.Substring(0, 1)) > Convert.ToInt32(rectangle.Last().Substring(0, 1)))
                    point = rectangle.Last().Substring(0, 1) + point.Substring(1, 1);
            if (Convert.ToInt32(point.Substring(1, 1)) > Convert.ToInt32(rectangle.Last().Substring(1, 1)))
                point = point.Substring(0, 1) + rectangle.Last().Substring(1, 1);
            point = point.Substring(0, 2) + direction;
            return point;
        }
        //verilen direk konuma gitmesi için eklendi.
        private static string AddPoint(string point, string newDirection)
        {
            //yön yoksa default n veriliyor lmr hareketlerini yapabilmesi için
            point = newDirection.Length > 2 ? newDirection.Substring(0, 2) + (newDirection.Substring(2, 1).Contains("N") || newDirection.Substring(2, 1).Contains("S") || newDirection.Substring(2, 1).Contains("E") || newDirection.Substring(2, 1).Contains("W") ? newDirection.Substring(2, 1) : "N") : newDirection.Substring(0, 2) + "N";
            point = CheckLastPoint(point);
            return point;
        }
        //turn left robotun bulunduğu yönde sola dönmesi için eklendi
        private static string LPoint(string point)
        {
            string direction = point.Length == 2 ? point.Substring(2, 0) : point.Substring(2, 1);
            if (direction == "S")
                direction = "E";
            else if (direction == "E")
                direction = "N";
            else if (direction == "N")
                direction = "W";
            else if (direction == "W")
                direction = "S";

            point = point.Substring(0, 2) + direction;
            return point;
        }
        //turn right robotun bulunduğu yönde sağa dönmesi için eklendi
        private static string RPoint(string point)
        {
            string direction = point.Substring(2, 1);
            if (direction == "S")
                direction = "w";
            else if (direction == "w")
                direction = "N";
            else if (direction == "N")
                direction = "E";
            else if (direction == "E")
                direction = "S";

            point = point.Substring(0, 2) + direction;
            return point;
        }
        //go robotun bulunduğu yönde 1 adım ilerlemesi için eklendi
        private static string MPoint(string point)
        {
            string direction = "";
            if (point.Length > 2)
                direction = point.Substring(2, 1);
            if (direction == "S")
                point = point.Substring(0, 1) + (Convert.ToInt32(point.Substring(1, 1)) - 1) + direction;
            else if (direction == "W")
                point = (Convert.ToInt32(point.Substring(0, 1)) - 1) + point.Substring(1, 1) + direction;
            else if (direction == "N")
                point = point.Substring(0, 1) + (Convert.ToInt32(point.Substring(1, 1)) + 1) + direction;
            else if (direction == "E")
                point = (Convert.ToInt32(point.Substring(0, 1)) + 1) + point.Substring(1, 1) + direction;
            point = CheckLastPoint(point);
            return point;
        }
    }
}
