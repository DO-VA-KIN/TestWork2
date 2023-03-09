using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Serealizer_
{
    public class ListNode
    {
        public ListNode Previous;
        public ListNode Next;
        public ListNode Random;

        public string Data;
    }

    class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serealize(FileStream s)
        {
            int num = -1;
            Encoding coder = Encoding.UTF8;
            //s.Write(coder.GetBytes(count));

            ListNode current = Head;
            while(current != null)
            {
                ListNode current2 = Head;
                for (int i = 0; current2 != null; i++)
                {
                    if (current.Random == current2)
                    {
                        num = i;
                        break;
                    }
                    current2 = current2.Next;
                }

                string w;
                if (num > -1)
                    w = current.Data +"<" + num +">\n";
                else
                    w = current.Data + "\n";

                s.Write(coder.GetBytes(w));
                current = current.Next;
            }
        }

        public void Deserealize(Stream s)
        {
            //byte[] buff = new byte[s.Length]; //если большой объём данных не предполагается, то лучше считать всё сразу для большей скорости(или искать баланс загрузки оперативки и жесткого диска)
            //s.Read(buff);                     //но полагаю, задачи оптимизации в данном случае нет
            ListNode lastPrevious = null;
            byte[] buff = new byte[1];
            Encoding coder = Encoding.UTF8;

            ListNode current = Head = new ListNode();
            while (s.Position < s.Length)
            {
                string r = null;
                string r2 = null;

                s.Read(buff);
                char ch = BitConverter.ToChar(buff);
                while (ch != '<')
                {
                    if (ch == '\n') break;
                    r += ch;
                    s.Read(buff);
                    ch = BitConverter.ToChar(buff);
                }
                s.Read(buff);
                BitConverter.ToChar(buff);
                while (ch != '>')
                {
                    if (ch == '\n') break;
                    r2 += ch;
                    s.Read(buff);
                    BitConverter.ToChar(buff);
                }

                current.Data = r;

                if (current != Head)
                    current.Previous = lastPrevious;
                lastPrevious = current;
                
                if (r2 != null)
                {

                }


            }
        }
    }
   
}

