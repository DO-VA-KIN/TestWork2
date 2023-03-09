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
            Encoding coder = Encoding.UTF8;
            s.Write(coder.GetBytes(Count.ToString() + "\n"));

            ListNode current = Head;
            while(current != null)
            {
                int num = -1;

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

            string r = null;
            byte[] buff = new byte[1];
            Encoding coder = Encoding.UTF8;

            s.Read(buff);
            string ch = coder.GetString(buff);
            while (ch != "\n")
            {
                r += ch;
                s.Read(buff);
                ch = coder.GetString(buff);
            }
            Count = Convert.ToInt32(r);
            r = "";

            ListNode[] nodes = new ListNode[Count];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new ListNode();
                nodes[i].Next = new ListNode();
                nodes[i].Previous = new ListNode();
                nodes[i].Random = new ListNode();
            }


            for (int i = 0; i < nodes.Length; i++)
            {
                s.Read(buff);
                ch = coder.GetString(buff);
                while (ch != "<")
                {
                    if (ch == "\n")
                    {
                        s.Position--;
                        break;
                    }
                    r += ch;
                    s.Read(buff);
                    ch = coder.GetString(buff);
                }
                nodes[i].Data = r;

                r = "";
                s.Read(buff);
                ch = coder.GetString(buff);
                while (ch != ">")
                {
                    if (ch == "\n") break;
                    r += ch;
                    s.Read(buff);
                    ch = coder.GetString(buff);
                }

                if (r != "")
                    nodes[i].Random = nodes[Convert.ToInt32(r)];

                if (i > 0)
                    nodes[i].Previous = nodes[i - 1];

                if (i < nodes.Length - 1)
                    nodes[i].Next = nodes[i + 1];

                r = "";
                while (ch != "\n")
                {
                    s.Read(buff);
                    ch = coder.GetString(buff);
                }
            }
            Head = nodes[0];
            Tail = nodes[nodes.Length - 1];        

        }
    
    }
   
}

