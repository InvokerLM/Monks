using System;
using System.Drawing;
using System.Windows.Forms;
using YoungGame;
namespace Monks
{
    public partial class Form1 : Form
    {

        /// <summary>
        /// 实现双缓冲，主要负责减轻窗体重绘过程中的闪烁的问题。
        /// </summary>
        private void UpStyle()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint |
                     ControlStyles.ContainerControl |
                      ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }

        public Form1()
        {
            InitializeComponent();
            UpStyle();
            SetCursor((Bitmap)Properties.Resources.mhand1, new Point(8, 8));
        }
        /// <summary>
        /// 1 选择白棋  2 选择黑棋
        /// </summary>
        int selectIndex = 1;
        #region 启动方案

        private void CreatMenu()
        {

            IniteTwzyManger();

            StartMeun(false);

            if (selectIndex == 2)
            {
                cManger.RandomMonksChess();
                InitePio();
            }

            InitUI();
        }

        #endregion

        #region 注册的事件
        void cManger_ResultEvent(object sender, ResultEventArgs e)
        {
            string s = "";
            if (selectIndex == 1 && e.ResultCode == 1)
            {
                s = ":)  恭喜你获得胜利！";
                PlaySound(Properties.Resources.win);
            }
            else if (selectIndex == 1 && e.ResultCode == 2)
            {
                s = ":(  再接再厉，一定会成功的！";
                PlaySound(Properties.Resources.killed);
            }
            else if (selectIndex == 2 && e.ResultCode == 1)
            {
                s = ":(  再接再厉，一定会成功的！";
                PlaySound(Properties.Resources.killed);
            }
            else
            {
                s = ":)  恭喜你获得胜利！";
                PlaySound(Properties.Resources.win);
            }

            MenuForm.Show(s);
            isNeedGoon = false;//关闭继续功能
            StartMeun(true);
        }
        #endregion

        #region 初始化TwzyManger
        TwzyManger cManger;
        private void IniteTwzyManger()
        {
            cManger = new TwzyManger();
            cManger.IsAutoGetResult = false;
            cManger.ResultEvent += new TwzyResultHandler(cManger_ResultEvent);
            cManger.Init();
            InitePio();
        }

        private void InitUI()
        {
            if (selectIndex == 1)
            {
                picUser.Image = Properties.Resources.pio2;
            }
            else
            {
                picUser.Image = Properties.Resources.pio1;
            }

            UIChange();
        }
        private void UIChange()
        {
            labDogfaceCount.Text = "X " + cManger.DogfaceChessCount.ToString();

            if (cManger.DogfaceChessCount == 0)
            {
                picAddPio.Image = Properties.Resources.picempty;
            }
            else if (cManger.DogfaceChessCount > 0 && cManger.DogfaceChessCount <= 8)
            {
                picAddPio.Image = Properties.Resources.pichalf;
            }
            else if (cManger.DogfaceChessCount > 8 && cManger.DogfaceChessCount <= 16)
            {
                picAddPio.Image = Properties.Resources.picfull;
            }
        }
        #endregion

        #region 加载布局
        private void InitePio()
        {
            if (cManger == null)
                return;
            int[,] arry = cManger.getArry;

            pio02.PioType = arry[0, 2];
            pio02.LocX = 0;
            pio02.LocY = 2;

            pio03.PioType = arry[0, 3];
            pio03.LocX = 0;
            pio03.LocY = 3;

            pio04.PioType = arry[0, 4];
            pio04.LocX = 0;
            pio04.LocY = 4;

            pio05.PioType = arry[0, 5];
            pio05.LocX = 0;
            pio05.LocY = 5;

            pio06.PioType = arry[0, 6];
            pio06.LocX = 0;
            pio06.LocY = 6;



            pio12.PioType = arry[1, 2];
            pio12.LocX = 1;
            pio12.LocY = 2;

            pio13.PioType = arry[1, 3];
            pio13.LocX = 1;
            pio13.LocY = 3;

            pio14.PioType = arry[1, 4];
            pio14.LocX = 1;
            pio14.LocY = 4;

            pio15.PioType = arry[1, 5];
            pio15.LocX = 1;
            pio15.LocY = 5;

            pio16.PioType = arry[1, 6];
            pio16.LocX = 1;
            pio16.LocY = 6;



            pio22.PioType = arry[2, 2];
            pio22.LocX = 2;
            pio22.LocY = 2;

            pio23.PioType = arry[2, 3];
            pio23.LocX = 2;
            pio23.LocY = 3;

            pio24.PioType = arry[2, 4];
            pio24.LocX = 2;
            pio24.LocY = 4;

            pio25.PioType = arry[2, 5];
            pio25.LocX = 2;
            pio25.LocY = 5;

            pio26.PioType = arry[2, 6];
            pio26.LocX = 2;
            pio26.LocY = 6;


            pio32.PioType = arry[3, 2];
            pio32.LocX = 3;
            pio32.LocY = 2;

            pio33.PioType = arry[3, 3];
            pio33.LocX = 3;
            pio33.LocY = 3;

            pio34.PioType = arry[3, 4];
            pio34.LocX = 3;
            pio34.LocY = 4;

            pio35.PioType = arry[3, 5];
            pio35.LocX = 3;
            pio35.LocY = 5;

            pio36.PioType = arry[3, 6];
            pio36.LocX = 3;
            pio36.LocY = 6;


            pio42.PioType = arry[4, 2];
            pio42.LocX = 4;
            pio42.LocY = 2;

            pio43.PioType = arry[4, 3];
            pio43.LocX = 4;
            pio43.LocY = 3;

            pio44.PioType = arry[4, 4];
            pio44.LocX = 4;
            pio44.LocY = 4;

            pio45.PioType = arry[4, 5];
            pio45.LocX = 4;
            pio45.LocY = 5;

            pio46.PioType = arry[4, 6];
            pio46.LocX = 4;
            pio46.LocY = 6;


            pio11.PioType = arry[1, 1];
            pio11.LocX = 1;
            pio11.LocY = 1;

            pio21.PioType = arry[2, 1];
            pio21.LocX = 2;
            pio21.LocY = 1;

            pio31.PioType = arry[3, 1];
            pio31.LocX = 3;
            pio31.LocY = 1;

            pio20.PioType = arry[2, 0];
            pio20.LocX = 2;
            pio20.LocY = 0;

            pio37.PioType = arry[3, 7];
            pio37.LocX = 3;
            pio37.LocY = 7;


            pio28.PioType = arry[2, 8];
            pio28.LocX = 2;
            pio28.LocY = 8;

            pio48.PioType = arry[4, 8];
            pio48.LocX = 4;
            pio48.LocY = 8;

            pio17.PioType = arry[1, 7];
            pio17.LocX = 1;
            pio17.LocY = 7;

            pio27.PioType = arry[2, 7];
            pio27.LocX = 2;
            pio27.LocY = 7;

            pio08.PioType = arry[0, 8];
            pio08.LocX = 0;
            pio08.LocY = 8;
        }
        #endregion

        #region mouse
        int px = -1;
        int py = -1;
        bool isSet = false;

        #region 添加黑子
        private void picAddPio_MouseLeave(object sender, EventArgs e)
        {
            if (cManger.DogfaceChessCount == 0)
            {
                picAddPio.Image = Properties.Resources.picempty;
            }
            else if (cManger.DogfaceChessCount > 0 && cManger.DogfaceChessCount <= 8)
            {
                picAddPio.Image = Properties.Resources.pichalf;
            }
            else if (cManger.DogfaceChessCount > 8 && cManger.DogfaceChessCount <= 16)
            {
                picAddPio.Image = Properties.Resources.picfull;
            }
        }
        private void picAddPio_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            if (selectIndex == 1)
                return;
            if (cManger.DogfaceChessCount == 0)
            {
                picAddPio.Image = Properties.Resources.picempty;
            }
            else if (cManger.DogfaceChessCount > 0 && cManger.DogfaceChessCount <= 8)
            {
                picAddPio.Image = Properties.Resources.selpichalf;
            }
            else if (cManger.DogfaceChessCount > 8 && cManger.DogfaceChessCount <= 16)
            {
                picAddPio.Image = Properties.Resources.selpicfull;
            }
        }

        private void picAddPio_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            if (selectIndex == 1)
                return;
            if (cManger.DogfaceChessCount > 0)
            {
                SetCursor((Bitmap)Properties.Resources.mhand2, new Point(8, 8));
                isSet = true;
            }
            else
            {
                SetCursor((Bitmap)Properties.Resources.mhand1, new Point(8, 8));
            }
        }

        #endregion

        private void pio_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            SetCursor((Bitmap)Properties.Resources.mhand4, new Point(8, 8));
            Pio tmpPio = sender as Pio;
            if (tmpPio.PioType == 1 || tmpPio.PioType == 2)
            {
                InitePio();
                tmpPio.setNullPic();
            }
        }

        private void pio_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            Pio tmpPio = sender as Pio;
            SetCursor(tmpPio.LocX, tmpPio.LocY);
            MoveChess(tmpPio, selectIndex);
        }
        #endregion

        #region 棋子移动
        //总移动管理
        private void MoveChess(Pio tmpPio, int chessType)
        {
            if (cManger == null)
                return;
            if (tmpPio.PioType == 0)
            {
                if (chessType == 2)
                {
                    DogfaceMove(tmpPio);
                }
                else
                {
                    ManksMove(tmpPio);
                }
                InitePio();
            }
            else if (tmpPio.PioType == 1 || tmpPio.PioType == 2)
            {
                px = tmpPio.LocX;
                py = tmpPio.LocY;
            }

            isSet = false;
            UIChange();
            isNeedGoon = true;//开启继续功能
            cManger.GetResult();
        }
        //小兵移动
        private void DogfaceMove(Pio tmpPio)
        {

            if (px != -1 && py != -1)
            {
                if (cManger.Move(px, py, tmpPio.LocX, tmpPio.LocY))
                {
                    cManger.RandomMonksChess();
                    PlaySound(Properties.Resources.move);

                    //Debug
                    SetText(cManger.getResultStr(2));
                }
                else
                {
                    PlaySound(Properties.Resources.Ding);
                }
            }
            if (isSet && cManger.DogfaceChessCount > 0)
            {
                if (cManger.Set(tmpPio.LocX, tmpPio.LocY))
                {
                    cManger.RandomMonksChess();
                    PlaySound(Properties.Resources.move);

                    //Debug
                    SetText(cManger.getResultStr(2));
                }
                else
                {
                    PlaySound(Properties.Resources.Ding);
                }
            }
            py = -1;
            px = -1;

        }
        //和尚移动
        private void ManksMove(Pio tmpPio)
        {
            if (px != -1 && py != -1)
            {
                if (cManger.Move(px, py, tmpPio.LocX, tmpPio.LocY))
                {
                    cManger.RandomDogfaceChess();
                    PlaySound(Properties.Resources.move);

                    //Debug
                    SetText(cManger.getResultStr(2));
                }
                else
                {
                    PlaySound(Properties.Resources.Ding);
                }
            }
            else
            {
                PlaySound(Properties.Resources.Ding);
            }
            py = -1;
            px = -1;
        }
        #endregion

        #region 设置鼠标样式
        private void SetCursor(int x, int y)
        {
            if (cManger == null)
                return;
            int i = cManger.getValue(x, y);
            if (i == 1)
            {
                SetCursor((Bitmap)Properties.Resources.mhand2, new Point(8, 8));
            }
            else if (i == 2)
            {
                SetCursor((Bitmap)Properties.Resources.mhand3, new Point(8, 8));
            }
            else
            {
                SetCursor((Bitmap)Properties.Resources.mhand1, new Point(8, 8));
            }
        }

        private void SetCursor(Bitmap cursor, Point hotPoint)
        {
            int hotX = hotPoint.X;
            int hotY = hotPoint.Y;
            Bitmap myNewCursor = new Bitmap(cursor.Width * 2 - hotX, cursor.Height * 2 - hotY);
            Graphics g = Graphics.FromImage(myNewCursor);
            g.Clear(Color.FromArgb(0, 0, 0, 0));
            g.DrawImage(cursor, cursor.Width - hotX, cursor.Height - hotY, cursor.Width, cursor.Height);

            this.Cursor = new Cursor(myNewCursor.GetHicon());

            g.Dispose();
            myNewCursor.Dispose();
        }

        #endregion

        #region 声音
        bool isPlaySound = true;
        private void PlaySound(System.IO.Stream stream)
        {
            if (!isPlaySound)
                return;
            System.Media.SoundPlayer newS = new System.Media.SoundPlayer(stream);
            //newS.Load();
            newS.Play();
        }
        private void picSound_MouseEnter(object sender, EventArgs e)
        {
            if (isPlaySound)
            {
                picSound.Image = Properties.Resources.soundDown;
            }
            else
            {
                picSound.Image = Properties.Resources.nosoundDown;
            }
        }

        private void picSound_MouseLeave(object sender, EventArgs e)
        {
            if (isPlaySound)
            {
                picSound.Image = Properties.Resources.sound;
            }
            else
            {
                picSound.Image = Properties.Resources.nosound;
            }
        }

        private void picSound_MouseDown(object sender, MouseEventArgs e)
        {
            if (isPlaySound)
            {
                picSound.Image = Properties.Resources.soundSel;
            }
            else
            {
                picSound.Image = Properties.Resources.nosoundSel;
            }
        }

        private void picSound_MouseUp(object sender, MouseEventArgs e)
        {
            isPlaySound = !isPlaySound;
            if (isPlaySound)
            {
                picSound.Image = Properties.Resources.soundDown;
            }
            else
            {
                picSound.Image = Properties.Resources.nosoundDown;
            }
        }

        #endregion

        #region 重新启动
        private void picRedo_MouseEnter(object sender, EventArgs e)
        {
            picRedo.Image = Properties.Resources.undo2;
        }

        private void picRedo_MouseLeave(object sender, EventArgs e)
        {
            picRedo.Image = Properties.Resources.undo1;
        }
        private void picRedo_MouseDown(object sender, MouseEventArgs e)
        {
            picRedo.Image = Properties.Resources.undo3;
        }

        private void picRedo_MouseUp(object sender, MouseEventArgs e)
        {
            StartMeun(true);

        }

        #endregion

        private void pio_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                SetCursor((Bitmap)Properties.Resources.mhand1, new Point(8, 8));
                InitePio();
                isSet = false;
                px = -1;
                py = -1;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定退出？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) != DialogResult.OK)
            {
                e.Cancel = true;
                return;
            }
        }

        //-----------------------------------------------菜单部分--------------------------------------------

        #region 调用菜单
        bool isShow = false;
        int loadSpeed = 10;
        int loadWeight = 4;
        private void timerMenu_Tick(object sender, EventArgs e)
        {
            if (isShow)
            {
                MenuShow();
            }
            else
            {
                MenuHide();
            }


        }
        private void MenuHide()
        {
            panMenu.Left -= loadSpeed;
            loadSpeed += loadWeight;
            if (Math.Abs(panMenu.Left) > this.Width + 10)
            {
                timerMenu.Enabled = false;
                loadSpeed = 10;
            }
        }
        private void MenuShow()
        {
            panMenu.Left += loadSpeed;
            loadSpeed += loadWeight;
            if (panMenu.Left > -10)
            {
                timerMenu.Enabled = false;
                loadSpeed = 10;
                panMenu.Left = 0;
            }
        }
        private void StartMeun(bool isShow)
        {
            picGoon.Visible = isNeedGoon;
            this.isShow = isShow;
            timerMenu.Enabled = true;
        }
        #endregion

        #region 菜单特效

        #region 选择按钮
        private void picRadWhite_MouseEnter(object sender, EventArgs e)
        {

            picRadWhite.Image = (selectIndex == 1) ?
                Properties.Resources.radwhiteDown1 : Properties.Resources.radwhiteDown2;
        }

        private void picRadWhite_MouseLeave(object sender, EventArgs e)
        {
            picRadWhite.Image = (selectIndex == 1) ?
              Properties.Resources.radwhite1 : Properties.Resources.radwhite2;
        }
        private void picRadWhite_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            picRadWhite.Image = Properties.Resources.radwhiteSel1;
            picRadBlack.Image = Properties.Resources.radblack2;

        }
        private void picRadWhite_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            picRadWhite.Image = Properties.Resources.radwhiteDown1;
            selectIndex = 1;
        }


        private void picRadBlack_MouseEnter(object sender, EventArgs e)
        {
            picRadBlack.Image = (selectIndex == 2) ?
                Properties.Resources.radblackDown1 : Properties.Resources.radblackDown2;
        }
        private void picRadBlack_MouseLeave(object sender, EventArgs e)
        {
            picRadBlack.Image = (selectIndex == 2) ?
                Properties.Resources.radblack1 : Properties.Resources.radblack2;

        }
        private void picRadBlack_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            picRadBlack.Image = Properties.Resources.radblackSel1;
            picRadWhite.Image = Properties.Resources.radwhite2;
        }
        private void picRadBlack_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            picRadBlack.Image = Properties.Resources.radblackDown1;
            selectIndex = 2;
        }

        #endregion

        #region 开始按钮
        private void picStart_MouseEnter(object sender, EventArgs e)
        {
            picStart.Image = Properties.Resources.startbtnDown1;
        }

        private void picStart_MouseLeave(object sender, EventArgs e)
        {
            picStart.Image = Properties.Resources.startbtn1;
        }
        private void picStart_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            picStart.Image = Properties.Resources.startbtnSel1;
        }
        private void picStart_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            picStart.Image = Properties.Resources.startbtnDown1;
            CreatMenu();
            InitePio();
        }
        #endregion

        #region 退出按钮

        private void picExit_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            picExit.Image = Properties.Resources.exitbtnSel1;
        }

        private void picExit_MouseEnter(object sender, EventArgs e)
        {
            picExit.Image = Properties.Resources.exitbtnDown1;
        }

        private void picExit_MouseLeave(object sender, EventArgs e)
        {
            picExit.Image = Properties.Resources.exitbtn1;
        }

        private void picExit_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;
            picExit.Image = Properties.Resources.exitbtnDown1;
            this.Close();
        }
        #endregion
        #endregion


        #region  //-----------------------------调试---------------------------------------------------



        private void InitDebug()
        {

            cManger = new TwzyManger();
            cManger.IsAutoGetResult = false;
            cManger.ResultEvent += new TwzyResultHandler(cManger_ResultEvent);
            cManger.Init(richTextBox1.Text.Replace("\n", ""), (int)numDogfaceCount.Value, (int)numKill.Value, radManks.Checked);

            InitePio();

            UIChange();
        }

        string undoText;
        private void SetText(string info)
        {
            undoText = richTextBox1.Text;
            richTextBox1.Text = info;

            numDogfaceCount.Value = cManger.DogfaceChessCount;
            radManks.Checked = (selectIndex == 1) ? true : false;
            radDogface.Checked = (selectIndex == 1) ? false : true;
            numKill.Value = cManger.KillDogfaceCount;
        }

        private void btnDebug_Click(object sender, EventArgs e)
        {
            InitDebug();
            StartMeun(false);
            if (radManks.Checked)
                selectIndex = 1;
            else
                selectIndex = 2;
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = undoText;
        }

        int debugFlags = 1;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (debugFlags > 5)
            {
                PassForm p = new PassForm();
                p.Owner = this;
                if (p.ShowDialog() == DialogResult.OK)
                {
                    panel1.Visible = true;
                }
                debugFlags = 1;
            }
            else
            {
                debugFlags++;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
        #endregion

        #region 继续
        bool isNeedGoon = false;
        private void picGoon_MouseEnter(object sender, EventArgs e)
        {
            picGoon.Image = Properties.Resources.picgoonDown;
        }

        private void picGoon_MouseLeave(object sender, EventArgs e)
        {
            picGoon.Image = Properties.Resources.picgoon;

        }

        private void picGoon_MouseDown(object sender, MouseEventArgs e)
        {
            picGoon.Image = Properties.Resources.picgoonSel;

        }

        private void picGoon_MouseUp(object sender, MouseEventArgs e)
        {
            InitDebug();
            StartMeun(false);
            picGoon.Image = Properties.Resources.picgoonDown;
        }

        #endregion
        #region 帮助
        private void picHelp_MouseEnter(object sender, EventArgs e)
        {
            picHelp.Image = Properties.Resources.btnhelpDown;
        }

        private void picHelp_MouseLeave(object sender, EventArgs e)
        {
            picHelp.Image = Properties.Resources.btnhelp;

        }

        private void picHelp_MouseDown(object sender, MouseEventArgs e)
        {
            picHelp.Image = Properties.Resources.btnhelpSel;

        }

        private void picHelp_MouseUp(object sender, MouseEventArgs e)
        {
            picHelp.Image = Properties.Resources.btnhelpDown;
            new HelpForm().ShowDialog();
        }
        #endregion


    }
}
