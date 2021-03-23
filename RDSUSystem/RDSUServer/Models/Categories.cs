using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;

namespace RDSUServer.Models
{
    public class Categories
    {
        public int Id { get; set; }

        public virtual Tournaments Tournament { get; set; }

        public ushort Category { get; set; }

        public string Protocol { get; set; }

        private static Timer timer = new Timer(86400000);

        private static string[] program;

        private static string[] dances;

        private static Old[] Olds;

        static Categories()
        {
            timer.Elapsed += new ElapsedEventHandler(XMLReader);
            XMLReader(null, null);
            timer.Start();
        }

        public static ushort Categorieser(bool isSt, byte dance, byte old)
        {
            ushort res = isSt ? 0 : (ushort)(dances.Length * Olds.Length);
            res += (ushort)(dance * Olds.Length + old);
            return res;
        }

        public static byte OldReader(int year)
        {
            int i = 0;
            while (Olds.Length - 1 > i && Olds[i].old > year) i++;
            return (byte)(Olds.Length - i);
        }

        private static void XMLReader(object sender, ElapsedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Category.xml");
            XmlNodeList list = doc.DocumentElement.ChildNodes;
            XmlNodeList olds = list.Item(0).ChildNodes;
            XmlNodeList dancess = list.Item(1).ChildNodes;
            XmlNodeList programs = list.Item(2).ChildNodes;
            Olds = new Old[olds.Count];
            for(int i = 0; i < olds.Count; i++)
            {
                Olds[i] = new Old { name = olds.Item(i).Name, old = Convert.ToByte(olds.Item(i).InnerText) };
            }
            dances = new string[dancess.Count];
            for (int i = 0; i < dancess.Count; i++)
            {
                dances[i] = dancess.Item(i).Name;
            }
            program = new string[programs.Count];
            for (int i = 0; i < programs.Count; i++)
            {
                program[i] = programs.Item(i).Name;
            }
        }

        public static string StringCategory(ushort category)
        {
            string res;
            if(category >= dances.Length * Olds.Length)
            {
                res = program[1];
                category -= (ushort)(dances.Length * Olds.Length);
            }
            else
            {
                res = program[0];
            }
            res += ", " + dances[category/Olds.Length] + " ";
            category /= (ushort)Olds.Length;
            res = Olds[category%Olds.Length].name;
            return res;
        }

        private struct Old
        {
            public string name;

            public byte old;
        }
    }
}
