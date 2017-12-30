﻿using System;
using System.Collections.Generic;

namespace nHetmans
{
    internal class Program
    {
        public static byte[] GenerateState(byte size)
        {
            byte[] state = new byte[size];
            Random random = new Random();
            for (byte i = 0; i < size; i++)
            {
                state[i] = (byte)random.Next(0, size);
            }

            return state;
        }

        public static void ShowState(byte[] state)
        {
            byte size = (byte)state.GetLength(0);

            for (byte i = 0; i < size; i++)// 0 1 2 3 4 5 6 7 - naglowek 
            {
                Console.Write(i + " ");
            }
            Console.Write("\n"); // przejscie do wlasciwego wyswietlania tresci
            
            for (byte row = 0; row < size; row++)
            {
                Console.Write(row + " ");
                for (byte column = 0; column < size; column++)
                {
                    if (state[column] == row)
                    {
                        Console.Write('\u25A0' + " ");        
                    }
                    else Console.Write("  ");
                }
                Console.Write("\n");
            }
            
        }
        
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            byte size = 8;
            byte[] initialState = GenerateState(size);
            ShowState(initialState);
            
            
        }
    }
}