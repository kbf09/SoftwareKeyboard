﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HongliangSoft.Utilities.Gui;


namespace SoftwareKeyboard
{
    public partial class Form1 : Form
    {

        Button[] btns = new Button[50];
        string hiragana = "あいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもや ゆ よらりるれろわ を ん";
        Dictionary<string, string> henkan = new Dictionary<string, string>() {
            {"あ", "a"},{"い", "i"},{"う", "u"},{"え", "e"}, {"お", "o"},
            {"か", "ka"},{"き","ki"},{"く","ku"},{"け","ke"},{"こ","ko"},
            {"さ","sa"},{"し","si"},{"す","su"},{"せ","se"},{"そ","so"},
            {"た","ta"},{"ち","ti"},{"つ","tu"},{"て","te"},{"と","to"},
            {"な","na"},{"に","ni"},{"ぬ","nu"},{"ね","ne"},{"の","no"},
            {"は","ha"},{"ひ","hi"},{"ふ","hu"},{"へ","he"},{"ほ","ho"},
            {"ま","ma"},{"み","mi"},{"む","mu"},{"め","me"},{"も","mo"},
            {"や","ya"},{"  "," "},{"ゆ","yu"},{"   ",""},{"よ","yo"},
            {"ら","ra"},{"り","ri"},{"る","ru"},{"れ","re"},{"ろ","ro"},
            {"わ","wa"},{" "," "},{"を","wo"},{"    ",""},{"ん","nn"},
        };
        int size = 150;
        int nowButtonNumber = 1;

        public Form1()
        {
            InitializeComponent();
            Form2 f = new Form2();   //デバック用
            f.Show();                //デバック用
        }

        
        // 非アクティブにするおまじない
        private const int WS_EX_NOACTIVATE = 0x8000000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;
                if (!base.DesignMode)
                {
                    p.ExStyle = p.ExStyle | (WS_EX_NOACTIVATE);
                }
                return p;
            }
        }

        /* グローバルフックに変更したからいらない
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Down:
                    nowButtonNumber += 1;
                    if (nowButtonNumber >= 50) nowButtonNumber -= 50;
                    btns[nowButtonNumber].Select();
                    break;

                case Keys.Right:
                    nowButtonNumber += 5;
                    if (nowButtonNumber >= 50) nowButtonNumber -= 50;
                    btns[nowButtonNumber].Select();

                    break;
                case Keys.Up:
                     nowButtonNumber -= 1;
                    if (nowButtonNumber < 0) nowButtonNumber += 50;
                    btns[nowButtonNumber].Select();
                    break;
                case Keys.Left:
                     nowButtonNumber -= 5;
                    if (nowButtonNumber < 0) nowButtonNumber += 50;
                    btns[nowButtonNumber].Select();
                    break;

                default:
                    return base.ProcessDialogKey(keyData);
                    break;
            }
            return true;
        }
         */

        private void keyHookProc(object sender, KeyboardHookedEventArgs e)
        {
            btns[nowButtonNumber].BackColor = SystemColors.Control;
            switch (e.KeyCode)
            {
                case Keys.Down:
                    nowButtonNumber += 1;
                    if (nowButtonNumber >= 50) nowButtonNumber -= 50;
                    btns[nowButtonNumber].Select();
                    break;

                case Keys.Right:
                    nowButtonNumber += 5;
                    if (nowButtonNumber >= 50) nowButtonNumber -= 50;
                    btns[nowButtonNumber].Select();

                    break;
                case Keys.Up:
                     nowButtonNumber -= 1;
                    if (nowButtonNumber < 0) nowButtonNumber += 50;
                    btns[nowButtonNumber].Select();
                    break;
                case Keys.Left:
                     nowButtonNumber -= 5;
                    if (nowButtonNumber < 0) nowButtonNumber += 50;
                    btns[nowButtonNumber].Select();
                    break;
            }
            btns[nowButtonNumber].BackColor = Color.Red;
        }

        private static KeyboardHook keyHook;

        private void Form1_Load(object sender, EventArgs e)
        {
            keyHook = new KeyboardHook();
            keyHook.KeyboardHooked += new KeyboardHookedEventHandler(keyHookProc);
            int i, j, sum = 0;

            // とりあえずウィンドウとサイズを固定
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Size = new Size(size * 10 + 15, size * 5 + 30);

            // 最前面にする
            this.TopMost = true;

            for (i = 0; i < 10; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    //Buttonクラスのインスタンスを作成する
                    btns[sum] = new System.Windows.Forms.Button();

                    //Buttonコントロールのプロパティを設定する
                    btns[sum].Name = "id."+ sum;
                    btns[sum].Text = this.hiragana.Substring(sum, 1);
                    btns[sum].Font = new Font("MS UI Gothic", 29);
                    //サイズと位置を設定する
                    btns[sum].Location = new Point(size * i, size * j);
                    btns[sum].Size = new System.Drawing.Size(size, size);


                    //フォームに追加する
                    this.Controls.Add(btns[sum]);
                    btns[sum].Click += new EventHandler(btn_click);
                    sum++;
                }

            }

            // 「あ」にフォーカス 
            btns[0].Select();
            btns[0].BackColor = Color.Red;

            // 今のフォーカスは「あ」なのでもどす
            nowButtonNumber = 0;

        }

        void btn_click(object sender, EventArgs e)
        {
            SendKeys.Send(henkan[((Button)sender).Text]);
        }
    }
}
