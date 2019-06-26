using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomString {
    private int rep = 0;

    public string GenerateCheckCode(int codeCount)
    {
        rep = 0;
        string str = string.Empty;
        long num2 = DateTime.Now.Ticks + this.rep;
        this.rep++;
        System.Random random = 
            new System.Random(((int)(((ulong)num2) & 0xffffffffL)) |
            ((int)(num2 >> this.rep)));
        for (int i = 0; i < codeCount; i++)
        {
            char ch;
            int num = random.Next();
            if ((num % 2) == 0)
            {
                ch = (char)(0x30 + ((ushort)(num % 10)));
            }
            else
            {
                ch = (char)(0x41 + ((ushort)(num % 0x1a)));
            }
            str = str + ch.ToString();
        }
        return str;
    }

    public string GenerateCheckCode32()
    {
        rep = 0;
        string str = string.Empty;
        long num2 = DateTime.Now.Ticks + this.rep;
        this.rep++;
        System.Random random =
            new System.Random(((int)(((ulong)num2) & 0xffffffffL)) |
            ((int)(num2 >> this.rep)));
        for (int i = 0; i < 32; i++)
        {
            char ch;
            int num = random.Next();
            if ((num % 2) == 0)
            {
                ch = (char)(0x30 + ((ushort)(num % 10)));
            }
            else
            {
                ch = (char)(0x41 + ((ushort)(num % 0x1a)));
            }
            str = str + ch.ToString();
        }
        return str;
    }


}
