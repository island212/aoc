﻿using System;
using System.Collections.Generic;
using System.IO;

var reports = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Input.txt"));

Console.WriteLine("########## Day 9 2021 ##########");
Console.WriteLine($"Part one solution: {SolvePartOne(reports)}");
Console.WriteLine($"Part two solution: {SolvePartTwo(reports)}");
Console.WriteLine("################################");

static int SolvePartOne(string[] reports)
{
    int scores = 0;
    var stack = new Stack<char>();
    for (int i = 0; i < reports.Length; i++)
    {
        stack.Clear();
        for (int j = 0; j < reports[i].Length; j++)
        {
            void Destack(char open, int point)
            {
                char openChar = stack.Pop();
                if (openChar != open)
                {
                    scores += point;
                    j = reports[i].Length;
                }
            }

            switch (reports[i][j])
            {
                case '(':
                case '[':
                case '{':
                case '<':
                    stack.Push(reports[i][j]);
                    break;
                case ')': Destack('(', 3); break;
                case ']': Destack('[', 57); break;
                case '}': Destack('{', 1197); break;
                case '>': Destack('<', 25137); break;
            }
        }
    }

    return scores;
}

static long SolvePartTwo(string[] reports)
{
    var scores = new List<long>();
    var stack = new Stack<char>();
    for (int i = 0; i < reports.Length; i++)
    {
        stack.Clear();
        for (int j = 0; j < reports[i].Length; j++)
        {
            void Destack(char open)
            {
                char openChar = stack.Pop();
                if (openChar != open)
                {
                    j = reports[i].Length;
                    stack.Clear();
                }
            }

            switch (reports[i][j])
            {
                case '(':
                case '[':
                case '{':
                case '<':
                    stack.Push(reports[i][j]);
                    break;
                case ')': Destack('('); break;
                case ']': Destack('['); break;
                case '}': Destack('{'); break;
                case '>': Destack('<'); break;
            }
        }

        if (stack.Count > 0)
        {
            long score = 0;
            while (stack.Count > 0)
            {
                switch (stack.Pop())
                {
                    case '(': score = score * 5 + 1; break;
                    case '[': score = score * 5 + 2; break;
                    case '{': score = score * 5 + 3; break;
                    case '<': score = score * 5 + 4; break;
                }
            }
            scores.Add(score);
        }       
    }

    scores.Sort();

    return scores[scores.Count / 2];
}
