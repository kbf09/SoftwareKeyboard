using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SoftwareKeyboard
{
    public partial class Form1 : Form
    {

        Button[] btns = new Button[50];
        string hiragana = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもや ゆ よらりるれろわ を ん";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int i, j, sum = 0;

            for (i = 0; i < 10; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    //Buttonクラスのインスタンスを作成する
                    btns[sum] = new System.Windows.Forms.Button();
                    Font f = new Font("MS UI Gothic", 29);

                    //Buttonコントロールのプロパティを設定する
                    btns[sum].Name = "id"+ sum;
                    btns[sum].Text = this.hiragana.Substring(sum, 1);
                    btns[sum].Font = f;
                    //サイズと位置を設定する
                    btns[sum].Location = new Point(100* i, 100 * j);
                    btns[sum].Size = new System.Drawing.Size(100, 100);


                    //フォームに追加する
                    this.Controls.Add(btns[sum]);

                    sum++;
                }
            }
        }
    }
}
