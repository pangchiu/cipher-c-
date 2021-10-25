using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cipher
{
    public partial class cipher : Form
    {
        public cipher()
        {
            InitializeComponent();
        }

        private void cipher_Load(object sender, EventArgs e)
        {

        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            try
            {
                int p = int.Parse(pTxt.Text.Trim());
                int q = int.Parse(qTxt.Text.Trim());
                int _e = int.Parse(eTxt.Text.Trim());
                String banRo = banRoTxt.Text.Trim();
                LoaiMa type = codeBox.Checked ? LoaiMa.vi : LoaiMa.en;

                Dictionary<String, String> re = new Global().enCodeRsa(p, q, _e, banRo,type);

                if (re["mess"].Equals("OK"))
                {
                    kqTxt.Text = re["result"];
                }
                else
                {
                    kqTxt.Text = re["mess"];
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(e);
            }
        }

        private void btnKey_Click(object sender, EventArgs e)
        {
            LoaiMa type = codeBox.Checked ? LoaiMa.vi : LoaiMa.en;
            int[] keys = new Global().createKeyRsa(type);
            pTxt.Text = keys[0].ToString();
            qTxt.Text = keys[1].ToString();
            eTxt.Text = keys[2].ToString();
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            try
            {
                int p = int.Parse(pTxt.Text.Trim());
                int q = int.Parse(qTxt.Text.Trim());
                int _e = int.Parse(eTxt.Text.Trim());
                String banMa = banMaTxt.Text.Trim();
                LoaiMa type = codeBox.Checked ? LoaiMa.vi : LoaiMa.en;

                Dictionary<String, String> re = new Global().deCodeRsa(p, q, _e, banMa, type);

                if (re["mess"].Equals("OK"))
                {
                    kqTxt.Text = re["result"];
                }
                else
                {
                    kqTxt.Text = re["mess"];
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(e);
            }
        }

        private void mHMHKbtn_Click(object sender, EventArgs e)
        {
            try
            {
                int p = int.Parse(pMHKTxt.Text.Trim());
                string[] _S = sTxt.Text.Trim().Split(',');
                int[] S = new int[_S.Length];
                for (int i = 0; i < _S.Length; i++)
                {
                    S[i] = int.Parse(_S[i]);
                }

                int a = int.Parse(aMHKTxt.Text.Trim());
                string banRo = banRoMHKTxt.Text.Trim();
                LoaiMa type = codeBox2.Checked ? LoaiMa.vi : LoaiMa.en;


                Dictionary<String, dynamic> re = new Global().enCodeMhk(p, a, S, banRo, type);

                if (re["mess"].Equals("OK"))
                {
                    kqMHKTxt.Text = new Global().ConvertToString((List<List<int>>)re["result"]);
                    //kqMHKTxt.Text = ((List<List<int>>)re["result"]).ToString();
                }
                else
                {
                    kqMHKTxt.Text = re["mess"];
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void gMMHKBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int p = int.Parse(pMHKTxt.Text.Trim());
                string[] _S = sTxt.Text.Trim().Split(',');
                int[] S = new int[_S.Length];
                for (int i = 0; i < _S.Length; i++)
                {
                    S[i] = int.Parse(_S[i]);
                }

                int a = int.Parse(aMHKTxt.Text.Trim());
                List<List<int>> banMa = new Global().ConvertToList(banMaMHKTxt.Text.Trim());
                LoaiMa type = codeBox2.Checked ? LoaiMa.vi : LoaiMa.en;


                Dictionary<String, dynamic> re = new Global().deCodeMhk(p, a, S, banMa, type);

                if (re["mess"].Equals("OK"))
                {
                    kqMHKTxt.Text = ((String)re["banRo"]);

                }
                else
                {
                    kqMHKTxt.Text = re["mess"];
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void keyMHKBtn_Click(object sender, EventArgs e)
        {
            Random random = new Random();

            int lenght = random.Next(1, 10);
            int[] listS = new int[lenght];
            int sum = 0;
            for(int i = 0; i < lenght; i ++ )
            {
                int value;
                if(i == 0)
                {
                    value = random.Next(1, 10);
                }
                else
                {
                    value = random.Next(sum, sum + 10);
                }
                listS[i] = value;
                sum += value;
            }

            int p = random.Next(sum, sum + 100);
            int a;
            int indexMaxA = 0;
            for(int i = CharCode.arrPrime.Length - 1; i >= 0; i --)
            {
                if(CharCode.arrPrime[i] < p)
                {
                    indexMaxA = i;
                    break;
                }
            }
            do
            {
                a = CharCode.arrPrime[random.Next(0, indexMaxA)];
            } while (new Global().ucln(a, p) != 1);


            String S = "";
            for(int i = 0; i < listS.Length; i ++)
            {
                if (i == 0) S += listS[i].ToString();
                else S += "," + listS[i].ToString();
            }


            pMHKTxt.Text = p.ToString();
            aMHKTxt.Text = a.ToString();
            sTxt.Text = S;
        }
    }
}
