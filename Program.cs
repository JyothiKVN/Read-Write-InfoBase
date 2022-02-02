using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoBaseAssign
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime strTime = DateTime.Now;
            Console.Write("Started at: "+ strTime+"\n");
            string Sourcepath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string file = Path.Combine(Sourcepath,"words.txt");
            string uniques = Path.Combine(Sourcepath, "uniques.txt");
            string fullwords = Path.Combine(Sourcepath, "fullwords.txt");
            string[] unique = null;
            string[] fullword = null;
             
            #region Reading file
            if (File.Exists(file))
            { 
                string[] lines = File.ReadAllLines(file); 
                List<string> list = new List<string>(); 
                List<KeyValuePair<string, string>> finallst = new List<KeyValuePair<string,string>>();
                foreach (string ln in lines)
                {    
                    for (int i = 0; i < ln.Length-3; i++)
                    {
                        string temp = ln.Substring(i, 4);
                        if (list.Contains(temp))
                        { 
                            list.Remove(temp); 
                            finallst.RemoveAll(a => a.Key.Equals(temp));
                        }
                        else
                        {
                            list.Add(temp);
                            finallst.Add(new KeyValuePair<string, string>(temp, ln)); 
                        }
                    } 
                }
                list.Sort();
                finallst = finallst.OrderBy(a => a.Key).ToList();

                unique = list.ToArray();                
                fullword = finallst.Select(a=> a.Value).ToArray(); 
            }
            #endregion
            #region writing to file
            if (File.Exists(uniques)) 
                File.Delete(uniques); 

            if (File.Exists(fullwords))
                File.Delete(fullwords);

            using (StreamWriter uniq = File.CreateText(uniques))
            {
                foreach (string ln in unique)
                {
                    uniq.WriteLine(ln);
                }
            }
             
            using (StreamWriter fullwrd = new StreamWriter(fullwords))
            {
                foreach (string ln in fullword)
                {
                    fullwrd.WriteLine(ln);
                }
            } 
            #endregion

            Console.Write("Total time taken: " + (DateTime.Now - strTime).TotalSeconds);
            Console.ReadLine();
        }
    }
}
/********
 * bicy
book
ecyc
icyc
recy


bicycle
book
recycle
bicycle
recycle

 * ******/