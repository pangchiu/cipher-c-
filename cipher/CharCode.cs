using System;
using System.Collections.Generic;
using System.Text;

namespace cipher
{
    class CharCode
    {
         static CharCode myCharCode;
        
       public  Dictionary<char, int> ByChar ;
        public  Dictionary<int, char> ByInt;
        public static int[] arrPrimeForVi = {2,3,5,7,11,13,17,19,23,29,
                                31,37,41,43,47,53,59,61,67,
                                71,73,79,83};
        public static int[] arrPrimeForEn = {2,3,5,7,11,13};

        public static int[] arrPrime = {
            2,
3,
5,
7,
11,
13,
17,
19,
23,
29,
31,
37,
41,
43,
47,
53,
59,
61,
67,
71,
73,
79,
83,
89,
97,
101,
103,
107,
109,
113,
127,
131,
137,
139,
149,
151,
157,
163,
167,
173,
179,
181,
191,
193,
197,
199,
211,
223,
227,
229,
233,
239,
241,
251,
257,
263,
269,
271,
277,
281,
283,
293,
        };




        public CharCode()
        {

            ByChar = creatCharCodeBychar();
            ByInt = creatCharCodeByInt();
        }
         public static CharCode instance() { 
            if(myCharCode == null)
            {
                myCharCode = new CharCode();

            }

            return myCharCode;
        }

        public List<int> ConvertToAscii(String str)
        {
            str = str.ToUpper();

            List<int> list = new List<int>();

            for(int i = 0; i < str.Length; i ++)
            {
                try
                {
                    list.Add((int)str[i] - 65);
                }catch(Exception e)
                {
                    list.Add(-1);
                }
            }

            return list;
        }

        public  List<int> convertToUnicode(String str)
        {

            List<int> list = new List<int>();
            foreach (char c in str)
            {
                try
                {
                    list.Add(ByChar[c]);
                }
                catch (Exception e)
                {
                    list.Add(-1);
                }

            }
            return list;
        }

        public  string convertToUnicode(List<int> arr)
        {


            string str = "";
            foreach (int c in arr)
            {
                str += ByInt[c];

            }
            return str;
        }

         Dictionary<char, int> creatCharCodeBychar()
        {
            Dictionary<char, int> dictionary = new Dictionary<char, int>();
            List<char> keys = new List<char>();
            List<int> values = new List<int>();
            keys.Add(' ');
            values.Add(0);


           addKeysAndValues1(keys,values);

            for (int i = 97; i <= 122; i++)
            {
                keys.Add((char)i);
                values.Add(values[values.Count - 1] + 1);
            }

            String sub = "ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚÝ";
            for (int i = 0; i < sub.Length; i++)
            {
                keys.Add(sub[i]);
                values.Add(values[values.Count - 1] + 1);
            }


            for (int i = 0; i < sub.Length; i++)
            {
                keys.Add(char.ToLower(sub[i]));
                values.Add(values[values.Count - 1] + 1);
            }

            keys.Add('Đ');
            values.Add(values[values.Count - 1] + 1);
            keys.Add('đ');
            values.Add(values[values.Count - 1] + 1);

            for (int i = 7840; i <= 7929; i++)
            {
                keys.Add(Convert.ToChar(i));
                values.Add(values[values.Count - 1] + 1);
            }

            for (int i = 0; i < values.Count; i++)
            {
                dictionary.Add(keys[i], values[i]);
            }

            return dictionary;
        }
        
        
        void addKeysAndValues1(List<char> keys, List<int> values) {
            
             for (int i = 65; i <= 90; i++)
            {
                keys.Add((char)i);
                values.Add(values[values.Count - 1] + 1);
            }
        }


         Dictionary<int, char> creatCharCodeByInt()
        {
            Dictionary<int, char> dictionary = new Dictionary<int, char>();
            List<char> keys = new List<char>();
            List<int> values = new List<int>();
            keys.Add(' ');
            values.Add(0);


            addKeysAndValues1(keys,values);

            for (int i = 97; i <= 122; i++)
            {
                keys.Add((char)i);
                values.Add(values[values.Count - 1] + 1);
            }

            String sub = "ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚÝ";
            for (int i = 0; i < sub.Length; i++)
            {
                keys.Add(sub[i]);
                values.Add(values[values.Count - 1] + 1);
            }


            for (int i = 0; i < sub.Length; i++)
            {
                keys.Add(char.ToLower(sub[i]));
                values.Add(values[values.Count - 1] + 1);
            }

            keys.Add('Đ');
            values.Add(values[values.Count - 1] + 1);
            keys.Add('đ');
            values.Add(values[values.Count - 1] + 1);

            for (int i = 7840; i <= 7929; i++)
            {
                keys.Add(Convert.ToChar(i));
                values.Add(values[values.Count - 1] + 1);
            }

            for (int i = 0; i < values.Count; i++)
            {
                dictionary.Add(values[i], keys[i]);
            }

            return dictionary;
        }

    }
    
    

}
