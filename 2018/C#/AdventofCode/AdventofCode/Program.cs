﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventofCode {
    class Program {
        const string path = @"C:\Users\Daan\Desktop\AdventOfCode\2018\Input\";
        //const string path = @"C:\Users\daans\Desktop\AdventOfCode\2018\Input\";

        static void Main(string[] args) {
            Console.WriteLine("Advent of Code - Year 2018");

            //Day1_Question1();
            //Day1_Question2();
            //Day2_Question1();
            //Day2_Question2();
            //Day3_Question1();
            //Day3_Question2();
            //Day4();
            //Day5_Question1();
            //Day5_Question2();
            //Day6_Question1();
            //StolenDay6();
            //Day7_Question1();
            //Day8();
            //Day9(435, 71184);
            //Day9(435, 7118400);
            //Day11_Question1();
            //StolenDay11();
            //Day12();
            //Day13();
            //Day14();
            //Day16();
            //Day19();
            Day23();

            Console.WriteLine("Done! Press any key to shut down the window");

            Console.ReadKey();
        }

        private static void Day1_Question1() {
            Console.WriteLine("Day 1 - Question 1");

            string input = File.ReadAllText(path + "Advent of Code Calibration values.txt");

            string[] inputs = input.Split('\n');

            int frequency = 0;

            foreach (string s in inputs) {
                if (s[0] == '-')
                    frequency -= Convert.ToInt32(s.Substring(1));
                else if (s[0] == '+')
                    frequency += Convert.ToInt32(s.Substring(1));
            }

            Console.WriteLine($"Answer Q1.1: {+frequency}\n");
        }

        private static void Day1_Question2() {
            Console.WriteLine("Day 1 - Question 2");

            string input = File.ReadAllText(path + "Advent of Code Calibration values.txt");

            string[] inputs = input.Split('\n');

            List<int> frequencies = new List<int>();

            int frequency = 0;

            frequencies.Add(frequency);

            bool looking = true;

            while (looking) {
                foreach (string s in inputs) {
                    if (s[0] == '-')
                        frequency -= Convert.ToInt32(s.Substring(1));
                    else if (s[0] == '+')
                        frequency += Convert.ToInt32(s.Substring(1));

                    if (frequencies.Contains(frequency)) {
                        Console.WriteLine($"Answer Q1.2: Duplicate Frequency is: {frequency}\n");
                        looking = false;
                        break;
                    } else
                        frequencies.Add(frequency);
                }
            }
        }

        private static void Day2_Question1() {
            Console.WriteLine("Day 2 - Question 1");

            string input = File.ReadAllText(path + "Advent of Code Box Ids.txt");

            string[] inputs = input.Split('\n');

            List<char> containsOnce = new List<char>();
            List<char> containsTwice = new List<char>();
            List<char> containsThrice = new List<char>();

            int twice = 0;
            int thrice = 0;

            foreach (string s in inputs) {
                containsOnce = new List<char>();
                containsTwice = new List<char>();
                containsThrice = new List<char>();

                foreach (char c in s) {
                    if (c == '\\' || c == '\r')
                        continue;

                    if (containsOnce.Contains(c)) {
                        containsOnce.Remove(c);
                        containsTwice.Add(c);
                    } else if (containsTwice.Contains(c)) {
                        containsTwice.Remove(c);
                        containsThrice.Add(c);
                    } else if (containsThrice.Contains(c)) {
                        containsThrice.Remove(c);
                    } else {
                        containsOnce.Add(c);
                    }
                }

                twice += Math.Min(containsTwice.Count, 1);
                thrice += Math.Min(containsThrice.Count, 1);
            }

            Console.WriteLine($"Asnwer Q2.1: {twice * thrice}\n");
        }

        private static void Day2_Question2() {
            Console.WriteLine("Day 2 - Question 2");

            string input = File.ReadAllText(path + "Advent of Code Box Ids.txt");

            string[] inputs = input.Split('\n');

            for (int i = 0; i < inputs.Length; i++) {
                for (int j = i + 1; j < inputs.Length; j++) {
                    bool differFound = false;
                    bool overshot = false;
                    for (int k = 0; k < inputs[i].Length; k++) {
                        if (inputs[i][k] != inputs[j][k]) {
                            if (differFound) {
                                overshot = true;
                                break;
                            } else
                                differFound = true;
                        }
                    }

                    if (differFound && !overshot) {
                        Console.WriteLine($"Answer Q2.2: The strings are:\n{inputs[i]}\n{inputs[j]}\n");
                        return;
                    }
                }
            }
        }

        private static void Day3_Question1() {
            Console.WriteLine("Day 3 - Question 1:");

            string input = File.ReadAllText(path + "Advent of Code Surface.txt");

            string[] inputs = input.Split('\n');

            int[,] sheet = new int[1000, 1001];

            for (int i = 0; i < 1000; i++)
                for (int j = 0; j < 1001; j++)
                    sheet[i, j] = 0;

            int[] xVals = new int[1307];
            int[] yVals = new int[1307];
            int[] wVals = new int[1307];
            int[] hVals = new int[1307];

            foreach (string s in inputs) {
                string[] inputSplit = s.Split(' ');

                string[] XY = inputSplit[2].Substring(0, inputSplit[2].Length - 1).Split(',');
                string[] WH = inputSplit[3].Split('x');

                int x = Convert.ToInt32(XY[0]);
                int y = Convert.ToInt32(XY[1]);
                int w = Convert.ToInt32(WH[0]);
                int h = Convert.ToInt32(WH[1]);

                for (int i = x; i < x + w; i++)
                    for (int j = y; j < y + h; j++)
                        sheet[i, j]++;
            }

            int duplicateClaim = 0;

            for (int i = 0; i < 1000; i++)
                for (int j = 0; j < 1001; j++)
                    if (sheet[i, j] > 1)
                        duplicateClaim++;

            Console.WriteLine($"Answer Q3.1: {duplicateClaim}\n");
        }

        private static void Day3_Question2() {
            Console.WriteLine("Day 3 - Question 2:");

            string input = File.ReadAllText(path + "Advent of Code Surface.txt");

            string[] inputs = input.Split('\n');

            int[] xVals = new int[1307];
            int[] yVals = new int[1307];
            int[] wVals = new int[1307];
            int[] hVals = new int[1307];

            for (int i = 0; i < 1307; i++) {
                string[] inputSplit = inputs[i].Split(' ');

                string[] XY = inputSplit[2].Substring(0, inputSplit[2].Length - 1).Split(',');
                string[] WH = inputSplit[3].Split('x');

                xVals[i] = Convert.ToInt32(XY[0]);
                yVals[i] = Convert.ToInt32(XY[1]);
                wVals[i] = Convert.ToInt32(WH[0]);
                hVals[i] = Convert.ToInt32(WH[1]);
            }

            for (int i = 0; i < 1307; i++) {
                bool noMatch = true;
                for (int j = 0; j < 1307; j++) {
                    if (i == j)
                        continue;

                    Rectangle rectI = new Rectangle(xVals[i], yVals[i], wVals[i], hVals[i]);
                    Rectangle rectJ = new Rectangle(xVals[j], yVals[j], wVals[j], hVals[j]);

                    if (rectI.IntersectsWith(rectJ)) {
                        noMatch = false;
                        break;
                    }
                }

                if (noMatch) {
                    Console.WriteLine($"Answer Q3.2: {i + 1}\n");
                    return;
                }
            }
        }

        private static void Day4() {
            Console.WriteLine("Day 4 - Question 1:");

            string input = File.ReadAllText(path + "Advent of Code Guard Times.txt");

            string[] inputs = input.Split("\n");

            GuardDataTable table = new GuardDataTable();

            foreach (string s in inputs)
                table.AddData(new GuardData(s));

            List<GuardSchedule> schedule = new List<GuardSchedule>();

            int asleep = -1;

            foreach (GuardData data in table.data) {
                string[] text = data.text.Split(' ');

                if (text[0].Equals("Guard")) {
                    DateTime dt = data.dateTime;
                    if (dt.Hour == 23) {
                        dt = dt.AddHours(1);
                        dt = dt.AddMinutes(-dt.Minute);
                    }
                    schedule.Add(new GuardSchedule(Convert.ToInt32(text[1].Substring(1)), dt));
                } else if (text[0].Equals("falls")) {
                    asleep = data.dateTime.Minute;
                } else if (text[0].Equals("wakes")) {
                    schedule[schedule.Count - 1].SetAsleep(asleep, data.dateTime.Minute);
                    asleep = -1;
                }
            }

            List<GuardInfo> info = new List<GuardInfo>();

            foreach (GuardSchedule gs in schedule) {
                if (info.Count == 0) {
                    info.Add(new GuardInfo(gs.guardId));
                    info[0].AddInfo(gs);
                    continue;
                }

                bool added = false;

                for (int i = 0; i < info.Count; i++) {
                    if (info[i].guardId == gs.guardId) {
                        info[i].AddInfo(gs);
                        added = true;
                        break;
                    }
                }

                if (!added) {
                    info.Add(new GuardInfo(gs.guardId));
                    info[info.Count - 1].AddInfo(gs);
                }
            }

            int MostAsleep = info[0].guardId;
            int timeSlept = info[0].TimeSlept();

            for (int i = 1; i < info.Count; i++) {
                if (timeSlept < info[i].TimeSlept()) {
                    MostAsleep = info[i].guardId;
                    timeSlept = info[i].TimeSlept();
                }
            }

            foreach (GuardInfo gi in info) {
                if (gi.guardId == MostAsleep) {
                    Console.WriteLine($"Answer Q4.1: {gi.guardId} - {gi.MinuteAsleep()} --> {gi.guardId * gi.MinuteAsleep()}\n");
                }
            }

            Console.WriteLine("Day 4 - Question 2:");

            GuardInfo mostAsleep = info[0];

            int gId = info[0].guardId;
            int highestAsleep = info[0].MostAsleep();

            foreach (GuardInfo gi in info) {
                if (mostAsleep.minutesAsleep[mostAsleep.MostAsleep()] < gi.minutesAsleep[gi.MostAsleep()])
                    mostAsleep = gi;
            }

            Console.WriteLine($"Answer Q4.2: {mostAsleep.guardId * mostAsleep.MostAsleep()}\n");
        }

        private static void Day5_Question1() {
            Console.WriteLine("Day 5 - Question 1:");

            string input = File.ReadAllText($@"{path}Advent of Code Polymers.txt");

            Console.WriteLine($"Answer Q5.1: {ReactPolymer(input)}\n");
        }

        private static void Day5_Question2() {
            Console.WriteLine("Day 5 - Question 2:");

            string input = File.ReadAllText($@"{path}Advent of Code Polymers.txt");

            List<int> results = new List<int>();

            for (int i = 'a'; i <= 'z'; i++) {
                char remove = (char)i;

                string temp = string.Copy(input);

                temp = temp.Replace(remove.ToString(), "");
                temp = temp.Replace((remove.ToString().ToUpper()), "");

                results.Add(ReactPolymer(temp));
            }

            int lowest = 0;

            for (int i = 1; i < results.Count; i++)
                if (results[lowest] > results[i])
                    lowest = i;

            Console.WriteLine($"Answer Q5.2: Char {(char)('a' + lowest)} - Length: {results[lowest]}");
        }

        private static int ReactPolymer(string input) {
            int i = 0;

            while (i < input.Length - 1) {
                if (i < 0)
                    i = 0;

                char cur = input[i];
                char next = input[i + 1];

                if (cur.ToString().ToUpper().Equals(next.ToString().ToUpper())) {
                    if (cur != next) {
                        input = input.Remove(i, 2);
                        i -= 2;
                    }
                }

                i++;
            }

            return input.Length;
        }

        private static void Day6_Question1() {
            Console.WriteLine("Day 6 - Question 1:");

            string input = File.ReadAllText(@"C:\Users\daans\Desktop\Advant of Code Coordinates.txt");
            //string input = $"1, 1\n1, 6\n8, 3\n3, 4\n5, 5\n8, 9";

            string[] inputs = input.Split('\n');

            List<Point> points = new List<Point>();

            foreach (string s in inputs) {
                string[] coords = s.Split(',');

                points.Add(new Point(Convert.ToInt32(coords[0]), Convert.ToInt32(coords[1])));
            }

            int[] map = new int[160000];

            for (int i = 0; i < 160000; i++)
                map[i] = -1;

            for (int i = 0; i < 160000; i++) {
                bool isCoord = false;

                foreach (Point p in points) {
                    if (i == GetIndex(p)) {
                        isCoord = true;
                        break;
                    }
                }

                if (isCoord)
                    continue;

                foreach (Point p in points) {
                    if (map[i] == -1) {
                        map[i] = GetIndex(p);
                        map[GetIndex(p)]--;
                    }

                    if (GetManhattan(GetPoint(i), p) < GetManhattan(GetPoint(i), GetPoint(map[i]))) {
                        map[map[i]]++;
                        map[i] = GetIndex(p);
                        map[map[i]]--;
                        continue;
                    }
                }
            }

            for (int i = 0; i < 400; i++) {
                bool isCoord = false;

                foreach (Point p in points) {
                    if (i == GetIndex(p)) {
                        isCoord = true;
                        break;
                    }
                }

                if (isCoord)
                    continue;

                foreach (Point p in points) {
                    Point po = GetPoint(map[i]);

                    if (po.X == p.X && po.Y == p.Y) {
                        points.Remove(p);
                        break;
                    }
                }
            }

            for (int i = 0; i < 160000; i += 400) {
                bool isCoord = false;

                foreach (Point p in points) {
                    if (i == GetIndex(p)) {
                        isCoord = true;
                        break;
                    }
                }

                if (isCoord)
                    continue;

                foreach (Point p in points) {
                    Point po = GetPoint(map[i]);

                    if (po.X == p.X && po.Y == p.Y) {
                        points.Remove(p);
                        break;
                    }
                }
            }

            for (int i = 160000 - 400; i < 16000; i++) {
                bool isCoord = false;

                foreach (Point p in points) {
                    if (i == GetIndex(p)) {
                        isCoord = true;
                        break;
                    }
                }

                if (isCoord)
                    continue;

                foreach (Point p in points) {
                    Point po = GetPoint(map[i]);

                    if (po.X == p.X && po.Y == p.Y) {
                        points.Remove(p);
                        break;
                    }
                }
            }

            for (int i = 399; i < 160000; i += 400) {
                bool isCoord = false;

                foreach (Point p in points) {
                    if (i == GetIndex(p)) {
                        isCoord = true;
                        break;
                    }
                }

                if (isCoord)
                    continue;

                foreach (Point p in points) {
                    Point po = GetPoint(map[i]);

                    if (po.X == p.X && po.Y == p.Y) {
                        points.Remove(p);
                        break;
                    }
                }
            }

            Console.WriteLine(points.Count);

            int min = int.MaxValue;

            foreach (Point p in points) {
                if (map[GetIndex(p)] < min)
                    min = map[GetIndex(p)];

                Console.WriteLine(map[GetIndex(p)]);
            }

            Console.WriteLine(min);
        }

        private static void StolenDay6() {
            Console.WriteLine("Day 6 - Question 1:");

            string input = File.ReadAllText(@"C:\Users\daans\Desktop\Advant of Code Coordinates.txt");
            //string input = $"1, 1\n1, 6\n8, 3\n3, 4\n5, 5\n8, 9";

            string[] inputs = input.Split('\n');

            Dictionary<int, Point> points = new Dictionary<int, Point>();

            int maxX = 0;
            int maxY = 0;
            int count = 0;

            foreach (string s in inputs) {
                string[] st = s.Trim().Split(", ");
                int x = Convert.ToInt32(st[0]);
                int y = Convert.ToInt32(st[1]);
                points.Add(count, new Point(x, y));
                count++;
                if (x > maxX)
                    maxX = x;

                if (y > maxY)
                    maxY = y;
            }

            int[,] grid = new int[maxX + 1, maxY + 1];
            Dictionary<int, int> regions = new Dictionary<int, int>();

            for (int x = 0; x < maxX; x++) {
                for (int y = 0; y < maxY; y++) {
                    int best = maxX + maxY;
                    int bestNum = -1;

                    for (int i = 0; i < count; i++) {
                        Point p = points[i];

                        int dist = Math.Abs(x - p.X) + Math.Abs(y - p.Y);
                        if (dist < best) {
                            best = dist;
                            bestNum = i;
                        } else if (dist == best) {
                            bestNum = -1;
                        }
                    }

                    grid[x, y] = bestNum;
                    if (regions.ContainsKey(bestNum)) {
                        regions[bestNum] += 1;
                    } else {
                        regions.Add(bestNum, 1);
                    }
                }
            }

            for (int x = 0; x <= maxX; x++) {
                int bad = grid[x, 0];
                regions.Remove(bad);
                bad = grid[x, maxY];
                regions.Remove(bad);
            }

            for (int y = 0; y <= maxY; y++) {
                int bad = grid[0, y];
                regions.Remove(bad);
                bad = grid[maxX, y];
                regions.Remove(bad);
            }

            int biggest = 0;
            foreach (int size in regions.Values)
                if (size > biggest)
                    biggest = size;

            Console.WriteLine(biggest);

            int inarea = 0;

            for (int x = 0; x < maxX; x++) {
                for (int y = 0; y < maxY; y++) {
                    int size = 0;

                    for (int i = 0; i < count; i++) {
                        Point p = points[i];

                        int dist = Math.Abs(x - p.X) + Math.Abs(y - p.Y);
                        size += dist;
                    }

                    if (size < 10000) {
                        inarea++;
                    }
                }
            }

            Console.WriteLine(inarea);
        }

        private static int GetManhattan(Point a, Point b) {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        private static int GetIndex(Point p) {
            return p.Y * 400 + p.X;
        }

        private static Point GetPoint(int i) {
            return new Point(i % 400, i / 400);
        }

        private static void Day7_Question1() {
            Console.WriteLine("Day 7 - Question 1:");

            //string input = File.ReadAllText(@"C:\Users\daans\Desktop\Advant of Code Assembly Instructions.txt");
            string input = $"Step C must be finished before step A can begin.\nStep C must be finished before step F can begin.\nStep A must be finished before step B can begin.\nStep A must be finished before step D can begin.\nStep B must be finished before step E can begin.\nStep D must be finished before step E can begin.\nStep F must be finished before step E can begin.";

            string[] inputs = input.Split('\n');

            Graph graph = new Graph();

            foreach (string s in inputs) {
                string[] txt = s.Split(' ');
                string v1 = txt[1];
                string v2 = txt[7];

                graph.AddEdge(v1, v2, 0);
            }

            //Console.WriteLine(graph.GetStart());
            //foreach (Vertex v in graph.GetStart())
            //    Console.WriteLine(v);

            foreach (Vertex v in graph.GetOrder())
                //Console.WriteLine(v)
                Console.Write(v.ToString()[0]);
            Console.WriteLine();

            int result = graph.Make();

            Console.WriteLine(result);

            //List<Vertex> v = graph.GetOrder();
            //for (int i = v.Count - 1; i >= 0; i--)
            //    Console.Write(v[i].ToString()[0]);


            //Console.WriteLine(graph);
        }

        private static void Day8() {
            Console.WriteLine("Day 8 - Question 1:");

            string input = File.ReadAllText(path + "Advent of Code License.txt");

            string[] inputs = input.Split(" ");

            int[] convertedInput = Array.ConvertAll(inputs, s => int.Parse(s));

            Tree tree = new Tree();

            tree.ProcessInput(convertedInput);


            Console.WriteLine($"Answer Q8.1: {tree.GetMeta()}\n");
            Console.WriteLine($"Day 8 - Question 2:\nAnswer Q8.2: {tree.GetRootMeta()}\n");
        }

        private static void Day9(int numPlayers, int numMarbles) {
            Console.WriteLine("Day 9");
            int currentMarbleIndex = 0;

            int[] scores = new int[numPlayers];

            for (int i = 0; i < numPlayers; i++)
                scores[i] = 0;

            List<int> circle = new List<int> {
                0
            };

            for (int i = 1; i <= numMarbles; i++) {
                if (i % 23 == 0) {
                    currentMarbleIndex = mod(currentMarbleIndex - 7, circle.Count);
                    scores[mod(i, numPlayers)] += i + circle[currentMarbleIndex];
                    circle.RemoveAt(currentMarbleIndex);
                } else {
                    int insertIndex = mod(currentMarbleIndex + 2, circle.Count);
                    if (insertIndex == 0) {
                        circle.Add(i);
                        currentMarbleIndex = circle.Count - 1;
                    } else {
                        circle.Insert(insertIndex, i);
                        currentMarbleIndex = insertIndex;
                    }
                }
            }

            int highest = 0;
            foreach (int i in scores)
                if (i > highest)
                    highest = i;

            Console.WriteLine($"Answer Q9: {highest}\n");
        }

        private static int mod(int x, int m) {
            int r = x % m;
            return r < 0 ? r + m : r;
        }

        private static void Day11_Question1() {
            Console.WriteLine("Day 11 - Question 1:");

            int input = 2568;
            //int input = 42;

            //Console.WriteLine(CalculateFuelCellPower(3, 5, 8));
            //Console.WriteLine(CalculateFuelCellPower(122, 79, 57));
            //Console.WriteLine(CalculateFuelCellPower(217, 196, 39));
            //Console.WriteLine(CalculateFuelCellPower(101, 153, 71));

            int[,] cells = new int[301, 301];

            for (int x = 1; x <= 300; x++) {
                for (int y = 1; y <= 300; y++) {
                    cells[x, y] = CalculateFuelCellPower(x, y, input);
                }
            }

            int heighest = int.MinValue;
            int xi = 0, yi = 0;

            for (int x = 1; x <= 298; x++) {
                for (int y = 1; y <= 298; y++) {
                    int total = 0;
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                            total += cells[x + i, y + j];

                    if (total >= heighest) {
                        heighest = total;
                        xi = x;
                        yi = y;
                    }
                }
            }

            Console.WriteLine($"Answer Q11.1: {{{xi}, {yi}}} with level: {heighest}\n");

            Console.WriteLine("Day 11 - Question 2:");

            int si = 0;
            heighest = int.MinValue;

            for (int s = 1; s < 300; s++) {
                for (int x = 1; x <= 300 - s; x++) {
                    for (int y = 1; y <= 300 - s; y++) {
                        int total = 0;

                        for (int i = 0; i < s; i++)
                            for (int j = 0; j < s; j++)
                                total += cells[x + i, y + 1];

                        if (total > heighest) {
                            heighest = total;
                            xi = x;
                            yi = y;
                            si = s;
                        }
                    }
                }
            }

            Console.WriteLine($"Answer Q11.2: {{{xi}, {yi}, {si}}} with level: {heighest}\n");
        }

        private static void StolenDay11() {
            int[,] sum = new int[301, 301];
            int bx = 0, by = 0, bs = 0, best = int.MinValue;
            for (int y = 1; y <= 300; y++) {
                for (int x = 1; x <= 300; x++) {
                    int id = x + 10;
                    int p = id * y + 2568;
                    p = (p * id) / 100 % 10 - 5;
                    sum[y, x] = p + sum[y - 1, x] + sum[y, x - 1] - sum[y - 1, x - 1];
                }
            }

            for (int s = 1; s <= 300; s++) {
                for (int y = s; y <= 300; y++) {
                    for (int x = s; x <= 300; x++) {
                        int iy = y;
                        int ix = x;
                        int iys = y - s;
                        int ixs = x - s;
                        int total = sum[y, x] - sum[y - s, x] - sum[y, x - s] + sum[y - s, x - s];

                        if (total > best) {
                            best = total;
                            bx = x;
                            by = y;
                            bs = s;
                        }
                    }
                }
            }

            Console.WriteLine($"{bx - bs + 1},{by - bs + 1},{bs}");
        }

        private static int CalculateFuelCellPower(int x, int y, int gridSerial) {
            int rackId = x + 10;
            int powerLevel = rackId * y;
            powerLevel += gridSerial;
            powerLevel *= rackId;
            powerLevel = (powerLevel / 100) % 10;
            return powerLevel - 5;
        }

        private static void Day12() {
            Console.WriteLine("Day 12 - Question 1:");

            string input = File.ReadAllText($"{path}Advent of Code - Day 12 - Plants.txt");

            string[] inputs = input.Split('\n');

            string[] starting = inputs[0].Split(' ');

            List<string> rules = new List<string>();

            for (int i = 2; i < inputs.Length; i++)
                rules.Add(inputs[i]);

            Plants plants = new Plants(starting[2], rules.ToArray());

            for (int i = 0; i < 20; i++) {
                plants.NextGen();
            }

            Console.WriteLine($"Answer Q12.1: {plants.Count()}\n");

            Console.WriteLine("Day 12 - Question 2:");

            plants = new Plants(starting[2], rules.ToArray());

            for (long i = 0; i < 500; i++)
                plants.NextGen();

            Console.WriteLine($"Answer Q12.2: {plants.Count() + 22 * (50000000000 - 500)}\n");
        }

        private static void Day13() {
            Console.WriteLine("Day 13 - Question 1:");

            string input = File.ReadAllText($"{path}Advent of Code - Day 13 - Tracks.txt");

            Field field = new Field(input);

            while (field.Tick()) { }

            Console.WriteLine($"Answer Q13.1: {field.collision}\n");

            //string[] inputs = input.Split('\n');

            //Console.WriteLine($"{{{inputs[0].Length}, {inputs.Length}}}");
        }

        private static void Day14() {
            Console.WriteLine("Day 14 - Question 1:");

            int input = 440231;
            //int input = 10;
            //int input = 51589;


            List<int> scores = new List<int> {
                3,
                7
            };

            int elf1Current = 0;
            int elf2Current = 1;

            for (int i = 0; i < input + 10; i++) {
                int newScore = scores[elf1Current] + scores[elf2Current];

                List<int> tempList = new List<int>();

                while (newScore >= 10) {
                    int temp = newScore % 10;
                    tempList.Insert(0, temp);
                    newScore /= 10;
                }

                tempList.Insert(0, newScore);

                scores.AddRange(tempList);

                elf1Current = (elf1Current + scores[elf1Current] + 1) % scores.Count;
                elf2Current = (elf2Current + scores[elf2Current] + 1) % scores.Count;
            }

            int it = input;
            string result = "";

            for (int i = 0; i < 10; i++) {
                result += scores[it + i].ToString();
            }

            Console.WriteLine($"Answer Q14.1: {result}\n");

            Console.WriteLine("Day 14 - Question 2:");

            string recipes = "37";

            elf1Current = 0;
            elf2Current = 1;

            while (true) {
                int elf1 = Convert.ToInt32(recipes.Substring(elf1Current, 1));
                int elf2 = Convert.ToInt32(recipes.Substring(elf2Current, 1));
                int newScore = elf1 + elf2;

                List<int> tempList = new List<int>();

                while (newScore >= 10) {
                    int temp = newScore % 10;
                    tempList.Insert(0, temp);
                    newScore /= 10;
                }

                tempList.Insert(0, newScore);

                foreach (int j in tempList)
                    recipes += j.ToString();

                elf1Current = (elf1Current + Convert.ToInt32(recipes.Substring(elf1Current, 1)) + 1) % recipes.Length;
                elf2Current = (elf2Current + Convert.ToInt32(recipes.Substring(elf2Current, 1)) + 1) % recipes.Length;

                if (recipes.Contains(input.ToString()))
                    break;
            }

            Console.WriteLine($"Answer Q14.2: {recipes.Length - input.ToString().Length}\n");
        }

        private static void Day16() {
            Console.WriteLine("Day 16 - Question 1:");

            string input = File.ReadAllText($"{path}Advent of Code - Day 16 - Op Codes.txt");

            //foreach (char c in input) {
            //    Console.WriteLine(c);
            //}

            string[] inputs = input.Split("\n\r\n");

            int three = 0;

            List<int>[] possible = new List<int>[16];

            for (int i = 0; i < 16; i++)
                possible[i] = new List<int>();

            foreach (string s in inputs) {
                string[] lines = s.Split('\n');

                int reg1, reg2, reg3, reg4;
                reg1 = Convert.ToInt32(lines[0][9].ToString());
                reg2 = Convert.ToInt32(lines[0][12].ToString());
                reg3 = Convert.ToInt32(lines[0][15].ToString());
                reg4 = Convert.ToInt32(lines[0][18].ToString());

                int com1, com2, com3, com4;
                string[] coms = lines[1].Split(' ');
                com1 = Convert.ToInt32(coms[0]);
                com2 = Convert.ToInt32(coms[1]);
                com3 = Convert.ToInt32(coms[2]);
                com4 = Convert.ToInt32(coms[3]);

                int res1, res2, res3, res4;
                res1 = Convert.ToInt32(lines[2][9].ToString());
                res2 = Convert.ToInt32(lines[2][12].ToString());
                res3 = Convert.ToInt32(lines[2][15].ToString());
                res4 = Convert.ToInt32(lines[2][18].ToString());

                ops[] re = new ops[16] {
                    Addr,
                    Addi,
                    Mulr,
                    Muli,
                    Banr,
                    Bani,
                    Borr,
                    Bori,
                    Setr,
                    Seti,
                    Gtir,
                    Gtri,
                    Gtrr,
                    Eqir,
                    Eqri,
                    Eqrr
                };

                int correct = 0;

                for (int i = 0; i < re.Length; i++) {
                    int[] registry = { reg1, reg2, reg3, reg4 };
                    re[i](ref registry, com2, com3, com4);
                    //if (registry[0] == res1 && registry[1] == res2 && registry[2] == res3 && registry[3] == res4) {
                    //    correct++;
                    //    if (!possible[i].Contains(com1))
                    //        possible[i].Add(com1);
                    //}
                }

                if (correct >= 3)
                    three++;

                //Console.WriteLine($"{reg1} - {reg2} - {reg3} - {reg4} --- {com1} - {com2} - {com3} - {com4} --- {res1} - {res2} - {res3} - {res4}");
            }

            Console.WriteLine($"Answer Q16.1: {three}\n");

            input = File.ReadAllText($"{path}Advent of Code - Day 16 - Op Code Program.txt");
            inputs = input.Split('\n');

            int[] reg = { 0, 0, 0, 0 };

            ops[] commands = new ops[16] {
                    Borr,
                    Seti,
                    Mulr,
                    Eqri,
                    Banr,
                    Bori,
                    Bani,
                    Gtri,
                    Addr,
                    Muli,
                    Addi,
                    Eqrr,
                    Gtir,
                    Eqir,
                    Setr,
                    Gtrr
                };

            foreach (string s in inputs) {
                int com1, com2, com3, com4;
                string[] coms = s.Split(' ');
                com1 = Convert.ToInt32(coms[0]);
                com2 = Convert.ToInt32(coms[1]);
                com3 = Convert.ToInt32(coms[2]);
                com4 = Convert.ToInt32(coms[3]);

                commands[com1](ref reg, com2, com3, com4);
            }

            Console.WriteLine($"Answer Q16.2: {reg[0]}\n");
        }

        private static void Day19() {
            Console.WriteLine("Day 19 - Question 1:");

            //string[] input = File.ReadAllText($"{path}Advent of Code - Day 19 - Test - Op Codes 2.txt").Split('\n');
            string[] input = File.ReadAllText($"{path}Advent of Code - Day 19 - Op Codes 2.txt").Split('\n');

            int[] registry = { 1, 0, 0, 0, 0, 0 };

            int instructionIndex = 0;
            //int instruction = 0;

            //int i = 0;

            instructionIndex = Convert.ToInt32(input[0].Split(' ')[1]);

            while (registry[instructionIndex] + 1 < input.Length) {
                string[] command = input[registry[instructionIndex] + 1].Split(' ');
                if (command[0].Equals("#ip")) {
                    instructionIndex = Convert.ToInt32(command[1]);
                } else if (command[0].Equals("addr")) {
                    Addr(ref registry, Convert.ToInt32(command[1]), Convert.ToInt32(command[2]), Convert.ToInt32(command[3]));
                } else if (command[0].Equals("addi")) {
                    Addi(ref registry, Convert.ToInt32(command[1]), Convert.ToInt32(command[2]), Convert.ToInt32(command[3]));
                } else if (command[0].Equals("mulr")) {
                    Mulr(ref registry, Convert.ToInt32(command[1]), Convert.ToInt32(command[2]), Convert.ToInt32(command[3]));
                } else if (command[0].Equals("muli")) {
                    Muli(ref registry, Convert.ToInt32(command[1]), Convert.ToInt32(command[2]), Convert.ToInt32(command[3]));
                } else if (command[0].Equals("banr")) {
                    Banr(ref registry, Convert.ToInt32(command[1]), Convert.ToInt32(command[2]), Convert.ToInt32(command[3]));
                } else if (command[0].Equals("bani")) {
                    Bani(ref registry, Convert.ToInt32(command[1]), Convert.ToInt32(command[2]), Convert.ToInt32(command[3]));
                } else if (command[0].Equals("borr")) {
                    Borr(ref registry, Convert.ToInt32(command[1]), Convert.ToInt32(command[2]), Convert.ToInt32(command[3]));
                } else if (command[0].Equals("bori")) {
                    Bori(ref registry, Convert.ToInt32(command[1]), Convert.ToInt32(command[2]), Convert.ToInt32(command[3]));
                } else if (command[0].Equals("setr")) {
                    Setr(ref registry, Convert.ToInt32(command[1]), Convert.ToInt32(command[2]), Convert.ToInt32(command[3]));
                } else if (command[0].Equals("seti")) {
                    Seti(ref registry, Convert.ToInt32(command[1]), Convert.ToInt32(command[2]), Convert.ToInt32(command[3]));
                } else if (command[0].Equals("gtir")) {
                    Gtir(ref registry, Convert.ToInt32(command[1]), Convert.ToInt32(command[2]), Convert.ToInt32(command[3]));
                } else if (command[0].Equals("gtri")) {
                    Gtri(ref registry, Convert.ToInt32(command[1]), Convert.ToInt32(command[2]), Convert.ToInt32(command[3]));
                } else if (command[0].Equals("gtrr")) {
                    Gtrr(ref registry, Convert.ToInt32(command[1]), Convert.ToInt32(command[2]), Convert.ToInt32(command[3]));
                } else if (command[0].Equals("eqir")) {
                    Eqir(ref registry, Convert.ToInt32(command[1]), Convert.ToInt32(command[2]), Convert.ToInt32(command[3]));
                } else if (command[0].Equals("eqri")) {
                    Eqri(ref registry, Convert.ToInt32(command[1]), Convert.ToInt32(command[2]), Convert.ToInt32(command[3]));
                } else if (command[0].Equals("eqrr")) {
                    Eqrr(ref registry, Convert.ToInt32(command[1]), Convert.ToInt32(command[2]), Convert.ToInt32(command[3]));
                }

                registry[instructionIndex]++;
            }

            Console.WriteLine($"Answer Q19.1: {registry[0]}\n");
        }

        private delegate void ops(ref int[] reg, int a, int b, int c);

        private static void Addr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] + reg[b];

        private static void Addi(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] + b;

        private static void Mulr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] * reg[b];

        private static void Muli(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] * b;

        private static void Banr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] & reg[b];

        private static void Bani(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] & b;

        private static void Borr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] | reg[b];

        private static void Bori(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] | b;

        private static void Setr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a];

        private static void Seti(ref int[] reg, int a, int b, int c) => reg[c] = a;

        private static void Gtir(ref int[] reg, int a, int b, int c) => reg[c] = a > reg[b] ? 1 : 0;

        private static void Gtri(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] > b ? 1 : 0;

        private static void Gtrr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] > reg[b] ? 1 : 0;

        private static void Eqir(ref int[] reg, int a, int b, int c) => reg[c] = a == reg[b] ? 1 : 0;

        private static void Eqri(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] == b ? 1 : 0;

        private static void Eqrr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] == reg[b] ? 1 : 0;

        class Nanobot {
            public Vector3D position = new Vector3D();
            public int radius;
        }

        private static void Day23() {
            string[] input = File.ReadAllText($"{path}Advent of Code - Day 23 - Nanobots.txt").Split('\n');

            List<Nanobot> bots = new List<Nanobot>();

            int largestIndex = 0;
            int largestRadius = int.MinValue;

            foreach (string s in input) {
                string[] split = s.Split(">, ");
                string[] pos = split[0].Substring(5).Split(',');
                Nanobot temp = new Nanobot();
                temp.position.x = Convert.ToInt32(pos[0]);
                temp.position.y = Convert.ToInt32(pos[1]);
                temp.position.z = Convert.ToInt32(pos[2]);
                temp.radius = Convert.ToInt32(split[1].Substring(2));

                if (temp.radius > largestRadius) {
                    largestRadius = temp.radius;
                    largestIndex = bots.Count;
                }

                bots.Add(temp);
            }

            int inRange = 0;

            foreach (Nanobot bot in bots)
                if (bot.position != bots[largestIndex].position)
                    if (bot.position.GetManhattan(bots[largestIndex].position) <= largestRadius)
                        inRange++;

            Console.WriteLine(inRange);

            List<Nanobot> bot7 = new List<Nanobot>();

            foreach (Nanobot bot in bots)
                bot7.Add(new Nanobot() { position = (new Vector3D(bot.position) / 10000000), radius = bot.radius / 10000000 });

            //foreach (Nanobot bot in botMil)
            //    Console.WriteLine(bot.position);

            Vector3D bestPos = new Vector3D();
            int numBots = 0;

            Vector3D current = new Vector3D(-15, -15, -15);

            for (; current.x <= 15; current.x++) {
                for (; current.y <= 15; current.y++) {
                    for (; current.z <= 15; current.z++) {
                        int numHits = 0;
                        foreach (Nanobot bot in bot7) {
                            if (bot.position.GetManhattan(current) <= bot.radius) {
                                numHits++;
                            }
                        }
                        if (numHits > numBots) {
                            numBots = numHits;
                            bestPos = new Vector3D(current);
                        }
                    }
                    current.z = -15;
                }
                current.y = -15;
            }

            List<Nanobot> bot6 = new List<Nanobot>();

            foreach (Nanobot bot in bots)
                bot6.Add(new Nanobot() { position = (new Vector3D(bot.position) / 1000000), radius = bot.radius / 1000000 });

            current = bestPos * 10;

            Vector3D prevBest = bestPos * 10;

            bestPos = new Vector3D();
            numBots = 0;

            for(current.x = (int)prevBest.x - 5; current.x <= prevBest.x + 5; current.x++) {
                for(current.y = (int)prevBest.y - 5; current.y <= prevBest.y + 5; current.y++) {
                    for(current.z = (int)prevBest.z - 5; current.z <= prevBest.z + 5; current.z++) {
                        int numHits = 0;
                        foreach(Nanobot bot in bot6) {
                            if(bot.position.GetManhattan(current) <= bot.radius) {
                                numHits++;
                            }
                        }
                        if(numHits > numBots) {
                            numBots = numHits;
                            bestPos = new Vector3D(current);
                        }
                    }
                }
            }

            int mod = 100000;

            while(mod > 1) {
                List<Nanobot> tempBots = new List<Nanobot>();

                foreach(Nanobot bot in bots)
                    tempBots.Add(new Nanobot() { position = (new Vector3D(bot.position) / mod), radius = bot.radius / mod });

                current = bestPos * 10;

                prevBest = bestPos * 10;

                bestPos = new Vector3D();

                numBots = 0;

                for (current.x = prevBest.x - 5; current.x <= prevBest.x + 5; current.x++) {
                    for(current.y = prevBest.y - 5;current.y <= prevBest.y + 5; current.y++) {
                        for(current.z = prevBest.z; current.z <= prevBest.z + 5; current.z++) {
                            int numHits = 0;
                            foreach(Nanobot bot in tempBots) 
                                if (bot.position.GetManhattan(current) <= bot.radius)
                                    numHits++;
                            

                            if(numHits > numBots) {
                                numBots = numHits;
                                bestPos = new Vector3D(current);
                            }
                        }
                    }
                }

                mod /= 10;
            }

            Console.WriteLine((int)bestPos.GetManhattan(new Vector3D()));

            Console.WriteLine($"{bestPos} -- {numBots}");
        }
    }
}
