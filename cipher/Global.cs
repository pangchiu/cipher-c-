using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace cipher
{

    public enum LoaiMa { vi,en}
     class Global
    {
        public Dictionary<string, string> enCodeRsa (int p, int q, int e, String banRo, LoaiMa type = LoaiMa.en)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string banMa = "";
            int phiN;
            int d;
            int N;
            int Z;
            

            // kiểm tra p , q
            if (!isPrime(p) || !isPrime(q) || q == p)
            {
                result["mess"] = "p hoặc q không hợp lệ";
                return result;
            }

            // kiểm tra e
            phiN = (p - 1) * (q - 1); 
            if(phiN <= e || e < 0 || ucln(e,phiN) != 1 )
            {
                result["mess"] = "e không hợp lệ";
                return result;
            }


            
            List<int> listX;
            N = p * q;
            d = inverseOf(e, N);
            if (type == LoaiMa.vi)
            {
                Z = 177;
                listX = CharCode.instance().convertToUnicode(banRo);
              
            }else
            {
                listX = CharCode.instance().ConvertToAscii(banRo);
                Z = 26;

            }

            // kiểm tra không gian
            if (N > Z)
            {
                result["mess"] = "không gian Bảng mã nhỏ hơn N";
                return result;
            }

            if (!isListXInvald(listX, Z))
            {
                result["mess"] = "không nhận dạng được các ký tự bản rõ";
                return result;
            }


            // đổi sang bản mã
            if(type == LoaiMa.vi)
            {
                listX.ForEach(x => banMa += CharCode.instance().ByInt[sAM(x, e, N)]);
            }else
            {
                listX.ForEach(x => banMa += (char)(sAM(x, e, N) + 65));
            }

            result["mess"] = "OK";
            result["result"] = banMa;
            return result;
        }

        public Dictionary<string, string> deCodeRsa(int p, int q, int e, String banMa, LoaiMa type = LoaiMa.en)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            string banRo = "";
            int phiN;
            int d;
            int N;
            int Z;


            // kiểm tra p , q
            if (!isPrime(p) || !isPrime(q) || q == p)
            {
                result["mess"] = "p hoặc q không hợp lệ";
                return result;
            }

            // kiểm tra e
            phiN = (p - 1) * (q - 1);
            if (phiN <= e || e < 0 || ucln(e, phiN) != 1)
            {
                result["mess"] = "e không hợp lệ";
                return result;
            }



            List<int> listY;
            N = p * q;
            d = inverseOf(e, phiN);
            if (type == LoaiMa.vi)
            {
                Z = 177;
                listY = CharCode.instance().convertToUnicode(banMa);

            }
            else
            {
                listY = CharCode.instance().ConvertToAscii(banMa);
                Z = 26;

            }

            // kiểm tra không gian
            if (N > Z)
            {
                result["mess"] = "không gian Bảng mã nhỏ hơn N";
                return result;
            }

            if (!isListXInvald(listY, Z))
            {
                result["mess"] = "không nhận dạng được các ký tự bản mã";
                return result;
            }


            // đổi sang bản mã
            if (type == LoaiMa.vi)
            {
                listY.ForEach(y => banRo += CharCode.instance().ByInt[sAM(y, d, N)]);
            }
            else
            {
                listY.ForEach(y => banRo += (char)(sAM(y, d, N) + 65));
            }

            result["mess"] = "OK";
            result["result"] = banRo;
            return result;
        }

        public Dictionary<string, dynamic> enCodeMhk(int p, int a, int[] s, String banRo, LoaiMa type = LoaiMa.en)
        {
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            String str = isInputInvald(s, p, a);
            Dictionary<char, List<int>> dictionaryX = new Dictionary<char,List<int>>();
            List<List<int>> listX = new List<List<int>>();
            List<List<int>> listY = new List<List<int>>();
            int[] T = new int [s.Length];
            if (!str.Equals("OK"))
            {
                result["mess"] = str;
                return result;

            }
            // đổi bản rõ ra nhị phân
            listX = stringToBinary(banRo, type);
            if(listX.Count == 0)
            {
                result["mess"] = "bản rõ không hợp lệ";
                return result;
            }
            
            

            // tính t
            for(int i = 0; i < s.Length; i++ )
            {
                T[i] = s[i] * a % p;
            }

            // điều chỉnh độ dài X
            for(int i = 0; i < listX.Count; i ++)
            {
                if(listX[i].Count > T.Length)
                {
                    int r = T.Length -  (listX[i].Count % T.Length);

                        for (int j = 1; j <= r; j++)
                        {
                            listX[i].Insert(0, 0);
                        }
       

                }
            }


            //tính Y
            for(int i = 0; i < listX.Count; i++)
            {
                // tính cho mỗi phần tử
                int j = listX[i].Count - 1;
                List<int> temp = new List<int>();
                int value = 0;
                int count = 0;
                int index = T.Length - 1;
                while(j >= 0)
                {
                    value += listX[i][j] * T[index];
                    count++;
                    index--;
                    j--;
                    if(j < 0 || count == T.Length)
                    {
                        temp.Add(value);
                        value = 0;
                        count = 0;
                    }
                    
                }
                listY.Add(temp);
            }

            result["mess"] = "OK";
            result["result"] = listY;
             return result;
        }

        public Dictionary<string, dynamic> deCodeMhk(int p, int a, int[] s, List<List<int>> banMa, LoaiMa type = LoaiMa.en)
        {
            Dictionary<string, dynamic> result = new Dictionary<string, dynamic>();
            String str = isInputInvald(s, p, a);
            string banRo = "";
            if (!str.Equals("OK"))
            {
                result["mess"] = str;
                return result;

            }

            //kiểm tra bản mã
            if (!isBanMaInvald(banMa))
            {
                result["mess"] = "bản mã không hợp lệ";
                return result;
            }

            for(int i = 0; i < banMa.Count; i ++)
            {
                List<int> listX = new List<int>();
                for (int j = 0; j < banMa[i].Count; j++)
                {
                    int C = inverseOf(a, p) * banMa[i][j] % p;
                    
                    for(int k = s.Length - 1; k >= 0; k--)
                    {
                        listX.Add(C / s[k]);
                        C = C % s[k];
                        if (C == 0) break;
                    }
                    
                }

                // đổi nhị phân sang số nguyên
                
               int value = swapBinaryToDex(listX);
                if(type == LoaiMa.vi)
                {
                    banRo += CharCode.instance().ByInt[value];
                }
                else
                {
                    banRo += (char)(value + 65);
                }
            }
           

         
            result["mess"] = "OK";
            result["banRo"] = banRo;
            return result;

        }
        
        public int[] createKeyRsa(LoaiMa type)
        {
            int[] arr = new int[3];
            Random random = new Random();
            if(type == LoaiMa.en)
            {
                int indexP = random.Next(0, CharCode.arrPrimeForEn.Length - 1);
                int p = CharCode.arrPrimeForEn[indexP];
                int maxQ = 26 / p;
                int indexMaxQ = 0;
                for(int i = CharCode.arrPrimeForEn.Length - 1; i >= 0; i--)
                {
                    if(maxQ >= CharCode.arrPrimeForEn[i])
                    {
                        indexMaxQ = i;
                        break;
                    }
                }


                int q;
                do
                {
                   q  = CharCode.arrPrimeForEn[random.Next(0, indexMaxQ)];
                }while(q == p);

      
                int e;
                int phiN = (q - 1) * (p - 1);
                do
                {
                    e = random.Next(1, phiN - 1);
                } while (e == q || p == e || ucln(e,phiN) != 1);
                arr[0] = p;
                arr[1] = q;
                arr[2] = e;

            }

            else
            {
                int indexP = random.Next(0, CharCode.arrPrimeForVi.Length - 1);
                int p = CharCode.arrPrimeForVi[indexP];
                int maxQ = 177 / p;
                int indexMaxQ = 0;
                for (int i = CharCode.arrPrimeForVi.Length - 1; i >= 0; i--)
                {
                    if (maxQ >= CharCode.arrPrimeForVi[i])
                    {
                        indexMaxQ = i;
                        break;
                    }
                }


                int q;
                do
                {
                    q = CharCode.arrPrimeForVi[random.Next(0, indexMaxQ)];
                } while (q == p);


                int e;
                int phiN = (q - 1) * (p - 1);
                do
                {
                    e = random.Next(1, phiN - 1);
                } while (e == q || p == e || ucln(e, phiN) != 1);
                arr[0] = p;
                arr[1] = q;
                arr[2] = e;

            }

            return arr;
            
        }

        bool isListXInvald(List<int> listX, int Z)
        {
            foreach (int e in listX)
            {
                if (e >= Z || e < 0) return false;
            }
            return true;
        }

        bool isPrime(int n)
        {
            if (n <= 3)
            {
                return n > 1;
            }
            else if (n % 2 == 0 || n % 3 == 0)
            {
                return false;
            }

            int i = 5;
            while (i * i <= n)
            {
                if (n % i == 0 || n % (i + 2) == 0) return false;
                i = i + 6;
            }

            return true;
        }

        // bình phương và nhân
        int sAM(int a, int x, int n)
        {
            int d = 1;
            while (x != 0)
            {
                if (x % 2 != 0) d = (d * a) % n;
                x = x / 2;
                a = (a * a) % n;
            }

            return d;


        }

        // thuật toán eucler
        int inverseOf(int a, int n)
        {
            if (ucln(a, n) == 1)
            {
                List<int> x = new List<int>
                {
                    n,
                    a
                };
                List<int> b = new List<int>
                {
                    0,
                    1
                };
                List<int> y = new List<int>();
                while (x[x.Count - 2] % x[x.Count - 1] != 0)
                {
                    y.Add(x[x.Count - 2] / x[x.Count - 1]);
                    x.Add(x[x.Count - 2] % x[x.Count - 1]);
                    b.Add(b[b.Count - 2] - (b[b.Count - 1] * y[y.Count - 1]));
                }
                return b[b.Count - 1] < 0 ? b[b.Count - 1] + n : b[b.Count - 1];
            }
            return 0;
        }

        //ƯỚC TRUNG LỚN NHẤT
        public int ucln(int a, int b)
        {
            while (a * b != 0)
            {
                int r = a % b;
                a = b;
                b = r;
            }
            return a + b;
        }
        // đổi bản rõ ra nhị phân
        List<List<int>>  stringToBinary (String str,LoaiMa type)
        {
            List<List<int>> list = new List<List<int>>();
            List<int> temp;
            if (type == LoaiMa.vi)
            {
                temp = CharCode.instance().convertToUnicode(str);
                if( !temp.TrueForAll(x => x > 0 && x < 177)) return list;
            }
            else
            {
                temp = CharCode.instance().ConvertToAscii(str);
                if (!temp.TrueForAll(x => x > 0 && x < 26)) return list;
            }

            for (int i = 0; i < temp.Count; i++)
            {

                List<int> binaryValue = swap(temp[i]);
                list.Add(binaryValue);
            }

            return list;
        }

        //đổi nhị phân
        List<int> swap(int a)
        {
            List<int> arr = new List<int>();
            if(a == 0)
            {
                arr.Add(0);
                return arr;
            }
            while (a > 0)
            {
                arr.Insert(0, a % 2);
                a = a / 2;
            }
            return arr;
        }
        String isInputInvald(int[] arr,int p,int a)
        {
            int sum = 0;
            for(int i = 0; i < arr.Length; i ++)
            {
                if (arr[i] < sum) return "Dãy S không là dãy siêu tăng";
                else
                {
                    sum += arr[i];
                }
            }

            if (p <= sum) return "p không hợp lệ";

            if (a <= 1 || a >= p - 1 || ucln(a, p) != 1) return "a không hợp lệ";

            return "OK";

        }


        public string ConvertToString(List<List<int>> list)
        {
            string str = "";
          for(int i = 0; i  < list.Count; i++)
          {
                string s = "{";
                for (int j = 0; j < list[i].Count; j++)
                    if (j == list[i].Count - 1) s += list[i][j];
                    else s+= list[i][j] + ',';
                if (i == list.Count - 1) s += "}";
                else s += "}-";
                str += s;
            }
            return str;
        }

        public List<List<int>> ConvertToList(string str)
        {
            var parten = @"\{(.*?)\}";
            var temp = Regex.Matches(str,parten);
            List<List<int>> banMa = new List<List<int>>();

            foreach(Match t in temp)
            {
                string s = t.Groups[1].ToString();
                s.Remove(0, 1);
                s.Remove(s.Length - 1, 1);
                string[] listS = s.Split(',');
                List<int> value = new List<int>();
                for (int i = 0; i < listS.Length; i++)
                {
                    try
                    {
                        value.Add(int.Parse(listS[i].ToString()));
                    }
                    catch(Exception ex)
                    {
                        value.Add(-1);
                    }
                }
                banMa.Add(value);
                 

            }
            return banMa;

        }

        bool isBanMaInvald(List<List<int>> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    if (list[i][j] == -1) return false;
                }
            }

            return true;
        }


        int swapBinaryToDex(List<int> list)
        {
            int sum = 0;
            for(int i = list.Count - 1; i >= 0; i --)
            {
                sum += (int)Math.Pow((double) 2,(double)i) * list[i];
            }
            return sum;
        }
    }
}
