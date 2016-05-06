using System;

namespace YoungGame
{
    public class TwzyManger
    {

        #region 公用方法
        //初始化
        public void Init()
        {
            string PointString = TwzyLocation.InitString;
            int index = 0;
            TwzyLocation.ChartArry = new int[5, 9];
            TwzyLocation.DogfaceChessCount = 24;
            for (int i = 0; i < 5; i++)
            {

                for (int j = 0; j < 9; j++)
                {
                    Set(i, j, int.Parse(PointString.Substring(index++, 1)));
                }
            }
        }

        public void Init(string pointStr, int dogfaceCount,int killCount, bool isManks)
        {
            string PointString = pointStr;
            isMonksTurn = isManks;
            int index = 0;
            TwzyLocation.ChartArry = new int[5, 9];
            TwzyLocation.DogfaceChessCount = dogfaceCount;
            _KilledDogfaceCount = killCount;
            for (int i = 0; i < 5; i++)
            {

                for (int j = 0; j < 9; j++)
                {
                    Set(i, j, int.Parse(PointString.Substring(index++, 1)));
                }
            }
            TwzyLocation.DogfaceChessCount = dogfaceCount;
        }
        //获取指定位置的值
        public int getValue(int x, int y)
        {
            if (x < 5 && y < 9)
            {
                return TwzyLocation.ChartArry[x, y];
            }
            else
            {
                return -1;
            }
        }
        #endregion

        #region 公共属性与字段
        public int DogfaceChessCount
        {
            get { return TwzyLocation.DogfaceChessCount; }
        }

        /// <summary>
        /// 布局数组
        /// </summary>
        public int[,] getArry
        {
            get
            {
                return TwzyLocation.ChartArry;
            }
        }

        public string getResultStr(int SType)
        {
            return TwzyLocation.ToString(SType);
        }

        //已经被吃掉的小兵
        int _KilledDogfaceCount = 0;
        public int KillDogfaceCount
        {
            get
            { return _KilledDogfaceCount; }
        }

        bool isMonksTurn = true;
        /// <summary>
        /// 1 小兵 2 和尚
        /// </summary>
        public int GetChessTurn
        {
            get
            {
                return (isMonksTurn) ? 2 : 1;
            }
        }

        #endregion

        #region 获取结果

        /// <summary>
        /// 是否自动获取结果 true自动获取，false通过调用GetResult()获取
        /// </summary>
        public bool IsAutoGetResult = true;
        /// <summary>
        /// 1 和尚胜利 2 小兵胜利
        /// </summary>
        public event TwzyResultHandler ResultEvent;
        public void GetResult()
        {
            if (ResultEvent != null)
            {
                if (GetMonksResult())
                {
                    ResultEvent(this, ResultEventArgs.CreatResult(2, "小兵胜利"));

                }
                else if (KillDogfaceCount == 20)
                {
                    ResultEvent(this, ResultEventArgs.CreatResult(1, "和尚胜利"));
                }
            }
        }

        private bool GetMonksResult()
        {
            TwzyPoint s = getMonksChessLocation();
            TwzyPoint q = getMonksChessLocation(s.X, s.Y + 1);
            int flags = 1;
            while (flags <= 2)
            {
                TwzyPoint p = (flags == 1) ? s : q;
                if ((p.X - 1 > -1 && getValue(p.X - 1, p.Y) == 0) ||
                    (p.X + 1 < 5 && getValue(p.X + 1, p.Y) == 0) ||
                    (((p.X == 2 && p.Y - 1 > -1 && p.Y + 1 < 9) || (p.Y - 1 > 1 && p.Y + 1 < 7)) && getValue(p.X, p.Y - 1) == 0) ||
                    (((p.X == 2 && p.Y - 1 > -1 && p.Y + 1 < 9) || (p.Y - 1 > 1 && p.Y + 1 < 7)) && getValue(p.X, p.Y + 1) == 0))
                {
                    return false;
                }
                if ((p.X - 2 > -1 && getValue(p.X - 2, p.Y) == 0) ||
                    (p.X + 2 < 5 && getValue(p.X + 2, p.Y) == 0) ||
                    (((p.X == 2 && p.Y - 2 > -1 && p.Y + 2 < 9) || (p.Y - 2 > 1 && p.Y + 2 < 7)) && getValue(p.X, p.Y - 2) == 0) ||
                    (((p.X == 2 && p.Y - 2 > -1 && p.Y + 2 < 9) || (p.Y - 2 > 1 && p.Y + 2 < 7)) && getValue(p.X, p.Y + 2) == 0))
                {
                    return false;
                }
                if ((p.X - 1 > -1 && ((p.X == 2 && p.Y - 1 > -1 && p.Y + 1 < 9) || (p.Y - 1 > 1 && p.Y + 1 < 7)) && getValue(p.X - 1, p.Y - 1) == 0 && (p.Y - p.X == 0 || p.Y - p.X == 4)) ||
                    (p.X + 1 < 5 && ((p.X == 2 && p.Y - 1 > -1 && p.Y + 1 < 9) || (p.Y - 1 > 1 && p.Y + 1 < 7)) && getValue(p.X + 1, p.Y - 1) == 0 && (p.Y + p.X == 4 || p.Y + p.X == 8)) ||
                    (p.X - 1 > -1 && ((p.X == 2 && p.Y - 1 > -1 && p.Y + 1 < 9) || (p.Y - 1 > 1 && p.Y + 1 < 7)) && getValue(p.X - 1, p.Y + 1) == 0 && (p.Y + p.X == 4 || p.Y + p.X == 8)) ||
                    (p.X + 1 > -1 && ((p.X == 2 && p.Y - 1 > -1 && p.Y + 1 < 9) || (p.Y - 1 > 1 && p.Y + 1 < 7)) && getValue(p.X + 1, p.Y + 1) == 0 && (p.Y - p.X == 0 || p.Y - p.X == 4)))
                {
                    return false;
                }
                if ((p.X - 2 > -1 && ((p.X == 2 && p.Y - 2 > -1 && p.Y + 2 < 9) || (p.Y - 2 > 1 && p.Y + 2 < 7)) && getValue(p.X - 2, p.Y - 2) == 0 && (p.Y - p.X == 0 || p.Y - p.X == 4)) ||
                    (p.X + 2 < 5 && ((p.X == 2 && p.Y - 2 > -1 && p.Y + 2 < 9) || (p.Y - 2 > 1 && p.Y + 2 < 7)) && getValue(p.X + 2, p.Y - 2) == 0 && (p.Y + p.X == 4 || p.Y + p.X == 8)) ||
                    (p.X - 2 > -1 && ((p.X == 2 && p.Y - 2 > -1 && p.Y + 2 < 9) || (p.Y - 2 > 1 && p.Y + 2 < 7)) && getValue(p.X - 2, p.Y + 2) == 0 && (p.Y + p.X == 4 || p.Y + p.X == 8)) ||
                    (p.X + 2 > -1 && ((p.X == 2 && p.Y - 2 > -1 && p.Y + 2 < 9) || (p.Y - 2 > 1 && p.Y + 2 < 7)) && getValue(p.X + 2, p.Y + 2) == 0 && (p.Y - p.X == 0 || p.Y - p.X == 4)))
                {
                    return false;
                }
                if (p.Y == 8 && ((p.X - 2 > -1 && getValue(p.X - 2, p.Y) == 0) || (p.X - 4 > -1 && getValue(p.X - 4, p.Y) == 0)||
                    (p.X==0 &&getValue(1,7)==0)||(p.X==4 &&getValue(3,7)==0)||
                    getValue(2,6)==0))
                {
                    return false;
                }
                if ((p.Y == 1 && ((p.X - 1 > 0 && getValue(p.X - 1, p.Y) == 0) ||
                                 (p.X + 1 < 4 && getValue(p.X + 1, p.Y) == 0) ||
                                 (p.X - 1 > 0 && p.X!=2 && getValue(p.X - 1, p.Y - 1) == 0) ||
                                 (p.X + 1 < 4 && p.X != 2 && getValue(p.X + 1, p.Y - 1) == 0) ||
                                 (p.X - 1 > 0 && p.X != 2 && getValue(p.X - 1, p.Y + 1) == 0) ||
                                 (p.X + 1 < 4 && p.X != 2 && getValue(p.X + 1, p.Y + 1) == 0))) ||
                    p.Y == 0 && p.X == 2 && (getValue(1, 1) == 0 || getValue(3, 1) == 0 || getValue(2, 1) == 0))
                {
                    return false;
                }
                if ((p.X == 0 && p.Y == 2 && (getValue(0, 3) == 0 || getValue(1, 2) == 0)) ||
                    (p.X == 0 && p.Y == 6 && (getValue(0, 5) == 0 || getValue(1, 6) == 0)) ||
                    (p.X == 4 && p.Y == 2 && (getValue(3, 2) == 0 || getValue(4, 3) == 0)) ||
                    (p.X == 4 && p.Y == 6 && (getValue(4, 5) == 0 || getValue(3, 6) == 0)))
                {
                    return false;
                }
                if (p.Y == 2 && (getValue(p.X, p.Y + 1) == 0 ||
                    (p.X - 1 > -1 && getValue(p.X - 1, p.Y + 1) == 0 && (p.X + p.Y == 4 || p.Y - p.X == 0)) ||
                    (p.X + 1 < 5 && getValue(p.X + 1, p.Y + 1) == 0 && (p.X + p.Y == 4 || p.Y - p.X == 0))))
                {
                    return false;
                }
                if (p.Y == 6 && (getValue(p.X, p.Y - 1) == 0 ||
                    (p.X - 1 > -1 && getValue(p.X - 1, p.Y - 1) == 0 && (p.X + p.Y == 8 || p.Y - p.X == 4)) ||
                    (p.X + 1 < 5 && getValue(p.X + 1, p.Y - 1) == 0 && (p.X + p.Y == 8 || p.Y - p.X == 4))))
                {
                    return false;
                }
                if (p.Y == 7 && ((p.X == 3 && getValue(4, 8) == 0) || (p.X == 1 && getValue(0, 8) == 0)))
                {
                    return false;
                }
                flags++;
            }

            return true;
        }

        #endregion

        #region 对棋子的操作


        public bool Set(int x, int y, int value)
        {
            if (x < 0 && x > 4 && y < 0 && y > 8 && value < 0 && value > 2)
            {
                return false;
            }
            int i = getValue(x, y);
            if (i == -1)
            {
                throw new Exception("无效的数据");
            }
            if (i == 0)//空位置
            {
                TwzyLocation.ChartArry[x, y] = value;
                if (value == 1)
                { TwzyLocation.DogfaceChessCount--; }
                return true;
            }
            else
            {
                //不是空位置
                return false;
            }
        }

        public bool Set(int x, int y)
        {
            if (isMonksTurn)
                return false;
            if (x < 0 && x > 4 && y < 0 && y > 8)
            {
                return false;
            }
            int i = getValue(x, y);
            if (i == -1)
            {
                throw new Exception("无效的数据");
            }
            if (i == 0)//空位置
            {
                TwzyLocation.ChartArry[x, y] = 1;
                isMonksTurn = true;
                TwzyLocation.DogfaceChessCount--;
                if (IsAutoGetResult)
                    GetResult();
                return true;
            }
            else
            {
                //不是空位置
                return false;
            }

        }

        public bool Move(int srcX, int srcY, int dstX, int dstY)
        {
            if (srcX < 0 && srcX > 4 && srcX < 0 && srcY > 8 &&
                dstX < 0 && dstX > 4 && dstY < 0 && dstY > 8)
            { return false; }
            int i = getValue(srcX, srcY);
            if (i != 1 && i != 2) { return false; }
            //判断轮循
            if ((isMonksTurn && i == 1) || (!isMonksTurn && i == 2))
                return false;

            if (getValue(dstX, dstY) != 0)//判断是否已经被占据
            {
                return false;
            }

            #region -
            if (srcX == dstX)// -
            {
                if (Math.Abs(srcY - dstY) == 1)//正常移动
                {
                    if ((srcY < 7 && srcY > 1 && dstY > 1 && dstY < 7) || srcX == 2)
                    {
                        TwzyLocation.ChartArry[dstX, dstY] = i;
                        TwzyLocation.ChartArry[srcX, srcY] = 0;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (Math.Abs(srcY - dstY) == 2 && getValue(srcX, srcY) == 2)
                {
                    int tmp = (srcY + dstY) / 2;
                    if (getValue(srcX, tmp) == 1)
                    {
                        if ((srcY < 7 && srcY > 1 && dstY > 1 && dstY < 7) || srcX == 2)
                        {
                            TwzyLocation.ChartArry[dstX, dstY] = i;
                            TwzyLocation.ChartArry[srcX, srcY] = 0;
                            TwzyLocation.ChartArry[srcX, tmp] = 0;
                            _KilledDogfaceCount++;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            #endregion

            #region |
            else if (srcY == dstY)// |
            {
                if (Math.Abs(srcX - dstX) == 1)
                {
                    TwzyLocation.ChartArry[dstX, dstY] = i;
                    TwzyLocation.ChartArry[srcX, srcY] = 0;
                }
                else if (Math.Abs(srcX - dstX) == 2)
                {
                    if (srcY == 8)//正常移动
                    {
                        TwzyLocation.ChartArry[dstX, dstY] = i;
                        TwzyLocation.ChartArry[srcX, srcY] = 0;
                    }
                    else if (getValue(srcX, srcY) == 2)
                    {
                        int tmp = (srcX + dstX) / 2;
                        if (getValue(tmp, srcY) == 1)
                        {
                            TwzyLocation.ChartArry[dstX, dstY] = i;
                            TwzyLocation.ChartArry[srcX, srcY] = 0;
                            TwzyLocation.ChartArry[tmp, srcY] = 0;
                            _KilledDogfaceCount++;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
                else if (Math.Abs(srcX - dstX) == 4 && srcY == 8)
                {
                    int tmp = (srcX + dstX) / 2;
                    if (getValue(tmp, srcY) == 1)
                    {
                        TwzyLocation.ChartArry[dstX, dstY] = i;
                        TwzyLocation.ChartArry[srcX, srcY] = 0;
                        TwzyLocation.ChartArry[tmp, srcY] = 0;
                        _KilledDogfaceCount++;
                    }
                    else
                        return false;
                }

                else
                    return false;

            }
            #endregion

            #region /// \\\

            else if (srcY > 0 && srcY < 5 && dstY > 0 && dstY < 5 && (srcX + srcY == 4) && (dstX + dstY == 4)) // /1 (3,1---0,4)
            {
                return subMove(srcX, srcY, dstX, dstY, i);
            }
            else if (srcY > 3 && srcY < 9 && dstY > 3 && dstY < 9 && (srcX + srcY == 8) && (dstX + dstY == 8))
            {
                return subMove(srcX, srcY, dstX, dstY, i);
            }
            else if (srcY >= 0 && srcY < 2 && dstY >= 0 && dstY < 2 && (srcX + srcY == 2) && (dstX + dstY == 2))
            {
                return subMove(srcX, srcY, dstX, dstY, i);
            }
            else if (srcY > 0 && srcY < 5 && dstY > 0 && dstY < 5 && (srcX - srcY == 0) && (dstX - dstY == 0))// /2 (2-4,2-4)
            {
                return subMove(srcX, srcY, dstX, dstY, i);
            }
            else if (srcY > 3 && srcY < 9 && dstY > 3 && dstY < 9 && (srcY - srcX == 4) && (dstY - dstX == 4))
            {
                return subMove(srcX, srcY, dstX, dstY, i);
            }
            else if (srcY >= 0 && srcY < 2 && dstY >= 0 && dstY < 2 && (srcY - srcX == -2) && (dstY - dstX == -2))
            {
                return subMove(srcX, srcY, dstX, dstY, i);
            }
            #endregion

            else
            {
                return false;
            }
            isMonksTurn = (i == 2) ? false : true;
            if (IsAutoGetResult)
                GetResult();
            return true;

        }


        private bool subMove(int srcX, int srcY, int dstX, int dstY, int i)
        {
            if (Math.Abs(srcX - dstX) == 1)
            {
                TwzyLocation.ChartArry[dstX, dstY] = i;
                TwzyLocation.ChartArry[srcX, srcY] = 0;
            }
            else if (Math.Abs(srcX - dstX) == 2 && getValue(srcX, srcY) == 2)
            {
                int tmpx = (srcX + dstX) / 2;
                int tmpy = (srcY + dstY) / 2;
                if (getValue(tmpx, tmpy) == 1)
                {
                    TwzyLocation.ChartArry[dstX, dstY] = i;
                    TwzyLocation.ChartArry[srcX, srcY] = 0;
                    TwzyLocation.ChartArry[tmpx, tmpy] = 0;
                    _KilledDogfaceCount++;
                }
                else
                    return false;
            }
            else
                return false;
            isMonksTurn = (i == 2) ? false : true;
            return true;
        }

        #endregion


        #region 人机对战代码

        #region 和尚
        /// <summary>
        /// 和尚
        /// </summary>
        public void RandomMonksChess()
        {
            TwzyPoint s = getMonksChessLocation();
            TwzyPoint q = getMonksChessLocation(s.X, s.Y + 1);

            int SecouryFlags = 1;
            int IntreFlags = 1;
            int NormalFlags = 1;

            #region 需找Kill小兵
            int pioFlags = getRandomMonksChess();
            while (SecouryFlags <= 2)
            {
                TwzyPoint p = (pioFlags == 1) ? s : q;
                //竖直
                if (p.Y == 8 && p.X - 4 > -1 && getValue(p.X - 2, p.Y) == 1 && getValue(p.X - 4, p.Y) == 0)
                {
                    if (Move(p.X, p.Y, p.X - 4, p.Y)) return;
                }
                if (p.X - 2 > -1 && getValue(p.X - 1, p.Y) == 1 && getValue(p.X - 2, p.Y) == 0)
                {
                    if (Move(p.X, p.Y, p.X - 2, p.Y)) return;
                }
                if (p.Y == 8 && p.X + 4 < 5 && getValue(p.X + 2, p.Y) == 1 && getValue(p.X + 4, p.Y) == 0)
                {
                    if (Move(p.X, p.Y, p.X + 4, p.Y)) return;
                }
                if (p.X + 2 < 5 && getValue(p.X + 1, p.Y) == 1 && getValue(p.X + 2, p.Y) == 0)
                {
                    if (Move(p.X, p.Y, p.X + 2, p.Y)) return;
                }
                //横平
                if (p.Y - 2 > -1 && getValue(p.X, p.Y - 1) == 1 && getValue(p.X, p.Y - 2) == 0
                    && (p.X == 2 || p.Y - 2 >= 1))
                {
                    if (Move(p.X, p.Y, p.X, p.Y - 2)) return;
                }
                if (p.Y + 2 < 9 && getValue(p.X, p.Y + 1) == 1 && getValue(p.X, p.Y + 2) == 0
                    && (p.X == 2 || p.Y + 2 <= 7))
                {
                    if (Move(p.X, p.Y, p.X, p.Y + 2)) return;
                }
                //斜线
                if (p.X - 2 > -1 && p.Y - 2 > -1 && getValue(p.X - 1, p.Y - 1) == 1 && getValue(p.X - 2, p.Y - 2) == 0 &&
                   (p.Y - p.X == 4 || p.Y - p.X == 0))
                {
                    if (Move(p.X, p.Y, p.X - 2, p.Y - 2)) return;
                }
                if (p.X + 2 < 5 && p.Y - 2 > -1 && getValue(p.X + 1, p.Y - 1) == 1 && getValue(p.X + 2, p.Y - 2) == 0 &&
                   (p.X + p.Y == 4 || p.X + p.Y == 8))
                {
                    if (Move(p.X, p.Y, p.X + 2, p.Y - 2)) return;
                }
                if (p.X - 2 > -1 && p.Y + 2 < 9 && getValue(p.X - 1, p.Y + 1) == 1 && getValue(p.X - 2, p.Y + 2) == 0 &&
                   (p.X + p.Y == 4 || p.X + p.Y == 8))
                {
                    if (Move(p.X, p.Y, p.X - 2, p.Y + 2)) return;
                }
                if (p.X + 2 < 5 && p.Y + 2 < 9 && getValue(p.X + 1, p.Y + 1) == 1 && getValue(p.X + 2, p.Y + 2) == 0 &&
                   (p.Y - p.X == 4 || p.Y - p.X == 0))
                {
                    if (Move(p.X, p.Y, p.X + 2, p.Y + 2)) return;
                }
                if (pioFlags == 1)
                    pioFlags = 2;
                else
                    pioFlags = 1;
                SecouryFlags++;
            }
            #endregion

            #region 获取主动权

            //pioFlags = getRandomMonksChess();
            //while (IntreFlags <= 2)
            //{
            //    TwzyPoint p=(pioFlags == 1) ? s : q;
 
            //    if()
            //}

            #endregion


            #region 正常移动
            pioFlags = getRandomMonksChess();
            while (NormalFlags <= 2)
            {
                TwzyPoint p = (pioFlags == 1) ? s : q;
                if (p.Y == 8 && p.X - 2 > -1 && getValue(p.X - 2, p.Y) == 0)
                {
                    if (Move(p.X, p.Y, p.X - 2, p.Y)) return;
                }
                if (p.X - 1 > -1 && getValue(p.X - 1, p.Y) == 0)
                {
                    if (Move(p.X, p.Y, p.X - 1, p.Y)) return;
                }
                if (p.Y == 8 && p.X + 2 < 5 && getValue(p.X + 2, p.Y) == 0)
                {
                    if (Move(p.X, p.Y, p.X + 2, p.Y)) return;
                }
                if (p.X + 1 < 5 && getValue(p.X + 1, p.Y) == 0)
                {
                    if (Move(p.X, p.Y, p.X + 1, p.Y)) return;
                }
                if (p.Y - 1 > -1 && getValue(p.X, p.Y - 1) == 0
                      && (p.X == 2 || p.Y - 1 >= 1))
                {
                    if (Move(p.X, p.Y, p.X, p.Y - 1)) return;
                }
                if (p.Y + 1 < 9 && getValue(p.X, p.Y + 1) == 0
                    && (p.X == 2 || p.Y + 1 < 7))
                {
                    if (Move(p.X, p.Y, p.X, p.Y + 1)) return;
                }
                //TODO斜线
                if (p.X - 1 > -1 && p.Y - 1 > -1 && getValue(p.X - 1, p.Y - 1) == 0
                    && (p.Y - p.X == 4 || p.Y - p.X == 0 || p.Y - p.X == -2))
                {
                    if (Move(p.X, p.Y, p.X - 1, p.Y - 1)) return;
                }
                if (p.X + 1 < 5 && p.Y - 1 > -1 && getValue(p.X + 1, p.Y - 1) == 0
                    && (p.X + p.Y == 4 || p.X + p.Y == 8 || p.X + p.Y == 2))
                {
                    if (Move(p.X, p.Y, p.X + 1, p.Y - 1)) return;
                }
                if (p.X - 1 > -1 && p.Y + 1 < 9 && getValue(p.X - 1, p.Y + 1) == 0
                   && (p.X + p.Y == 4 || p.X + p.Y == 8 || p.X + p.Y == 2))
                {
                    if (Move(p.X, p.Y, p.X - 1, p.Y + 1)) return;
                }
                if (p.X + 1 < 5 && p.Y + 1 < 9 && getValue(p.X + 1, p.Y + 1) == 0
                   && (p.X + p.Y == 4 || p.X + p.Y == 8 || p.X + p.Y == 2))
                {
                    if (Move(p.X, p.Y, p.X + 1, p.Y + 1)) return;
                }
                if (pioFlags == 1)
                    pioFlags = 2;
                else
                    pioFlags = 1;
                NormalFlags++;
            }
            #endregion

        }

        #endregion

        #region 小兵
        /// <summary>
        /// 小兵
        /// </summary>
        public void RandomDogfaceChess()
        {
            TwzyPoint s = getMonksChessLocation();
            TwzyPoint q = getMonksChessLocation(s.X, s.Y + 1);

            int SecouryFlags = 1;
            int BlockFlags = 1;
            int NormalFlags = 1;

            if (TwzyLocation.DogfaceChessCount == 0)
            {
                DogfaceChessMove(s, q);
                return;
            }

            #region Set
            int pioFlags = getRandomMonksChess();
            while (SecouryFlags <= 2)
            {
                TwzyPoint p = (pioFlags == 1) ? s : q;
                //在横竖方向存在紧急小兵
                if (p.Y == 8 && p.X - 4 > -1 && getValue(p.X - 2, p.Y) == 1 && getValue(p.X - 4, p.Y) == 0)//纵行第八条
                {
                    if (isContainMonksChess(p.X - 4, p.Y, -3, -3) && Set(p.X - 4, p.Y)) return;
                }
                if (p.X - 2 > -1 && getValue(p.X - 1, p.Y) == 1 && getValue(p.X - 2, p.Y) == 0)
                {
                    if (isContainMonksChess(p.X - 2, p.Y, -3, -3) && Set(p.X - 2, p.Y)) return;
                }
                if (p.Y == 8 && p.X + 4 < 5 && getValue(p.X + 2, p.Y) == 1 && getValue(p.X + 4, p.Y) == 0)//纵行第八条
                {
                    if (isContainMonksChess(p.X + 4, p.Y, -3, -3) && Set(p.X + 4, p.Y)) return;
                }
                if (p.X + 2 < 5 && getValue(p.X + 1, p.Y) == 1 && getValue(p.X + 2, p.Y) == 0)
                {
                    if (isContainMonksChess(p.X + 2, p.Y, -3, -3) && Set(p.X + 2, p.Y)) return;
                }
                if (p.Y - 2 > -1 && getValue(p.X, p.Y - 1) == 1 && getValue(p.X, p.Y - 2) == 0 &&
                    (p.X == 2 || p.Y - 2 >= 1))
                {
                    if (isContainMonksChess(p.X, p.Y - 2, -3, -3) && Set(p.X, p.Y - 2)) return;

                }
                if (p.Y + 2 < 9 && getValue(p.X, p.Y + 1) == 1 && getValue(p.X, p.Y + 2) == 0 &&
                    (p.X == 2 || p.Y + 2 <= 7))
                {
                    if (isContainMonksChess(p.X, p.Y + 2, -3, -3) && Set(p.X, p.Y + 2)) return;
                }
                //斜线方向存在紧急小兵
                if (p.X - 2 > -1 && p.Y - 2 > -1 && getValue(p.X - 1, p.Y - 1) == 1 && getValue(p.X - 2, p.Y - 2) == 0 &&
                   (p.X + p.Y == 4 || p.X + p.Y == 8 || p.Y - p.X == 4 || p.Y - p.X == 0))
                {
                    if (isContainMonksChess(p.X - 2, p.Y - 2, -3, -3) && Set(p.X - 2, p.Y - 2)) return;
                }
                if (p.X + 2 < 5 && p.Y - 2 > -1 && getValue(p.X + 1, p.Y - 1) == 1 && getValue(p.X + 2, p.Y - 2) == 0 &&
                   (p.X + p.Y == 4 || p.X + p.Y == 8 || p.Y - p.X == 4 || p.Y - p.X == 0))
                {
                    if (isContainMonksChess(p.X + 2, p.Y - 2, -3, -3) && Set(p.X + 2, p.Y - 2)) return;
                }
                if (p.X - 2 > -1 && p.Y + 2 < 9 && getValue(p.X - 1, p.Y + 1) == 1 && getValue(p.X - 2, p.Y + 2) == 0 &&
                   (p.X + p.Y == 4 || p.X + p.Y == 8 || p.Y - p.X == 4 || p.Y - p.X == 0))
                {
                    if (isContainMonksChess(p.X - 2, p.Y + 2, -3, -3) && Set(p.X - 2, p.Y + 2)) return;
                }
                if (p.X + 2 < 5 && p.Y + 2 < 9 && getValue(p.X + 1, p.Y + 1) == 1 && getValue(p.X + 2, p.Y + 2) == 0 &&
                   (p.X + p.Y == 4 || p.X + p.Y == 8 || p.Y - p.X == 4 || p.Y - p.X == 0))
                {
                    if (isContainMonksChess(p.X + 2, p.Y + 2, -3, -3) && Set(p.X + 2, p.Y + 2)) return;
                }
                if (pioFlags == 2)
                    pioFlags = 1;
                else
                    pioFlags = 2;
                SecouryFlags++;
            }
            pioFlags = getRandomMonksChess();
            while (BlockFlags <= 2)
            {
                TwzyPoint p = (pioFlags == 1) ? s : q;
                //在横竖方向围堵
                if (p.Y == 8 && p.X - 4 > -1 && (getValue(p.X - 4, p.Y) == 1 || getValue(p.X - 4, p.Y) == 2) && getValue(p.X - 2, p.Y) == 0)//纵行第八条
                {
                    if (isContainMonksChess(p.X - 2, p.Y, -3, -3) && Set(p.X - 2, p.Y)) return;
                }
                if (p.X - 2 > -1 && (getValue(p.X - 2, p.Y) == 1 || getValue(p.X - 2, p.Y) == 2) && getValue(p.X - 1, p.Y) == 0)
                {
                    if (isContainMonksChess(p.X - 1, p.Y, -3, -3) && Set(p.X - 1, p.Y)) return;
                }
                if (p.Y == 8 && p.X + 4 < 5 && (getValue(p.X + 4, p.Y) == 1 || getValue(p.X + 4, p.Y) == 2) && getValue(p.X + 2, p.Y) == 0)//纵行第八条
                {
                    if (isContainMonksChess(p.X + 2, p.Y, -3, -3) && Set(p.X + 2, p.Y)) return;
                }
                if (p.X + 2 < 5 && (getValue(p.X + 2, p.Y) == 1 || getValue(p.X + 2, p.Y) == 2) && getValue(p.X + 1, p.Y) == 0)
                {
                    if (isContainMonksChess(p.X + 1, p.Y, -3, -3) && Set(p.X + 1, p.Y)) return;
                }
                if (p.Y - 2 > -1 && (getValue(p.X, p.Y - 2) == 1 || getValue(p.X, p.Y - 2) == 2) && getValue(p.X, p.Y - 1) == 0 &&
                    (p.X == 2 || p.Y - 2 >= 1))
                {
                    if (isContainMonksChess(p.X, p.Y - 1, -3, -3) && Set(p.X, p.Y - 1)) return;
                }
                if (p.Y + 2 < 9 && (getValue(p.X, p.Y + 2) == 1 || getValue(p.X, p.Y + 2) == 2) && getValue(p.X, p.Y + 1) == 0 &&
                    (p.X == 2 || p.Y + 2 <= 7))
                {
                    if (isContainMonksChess(p.X, p.Y + 1, -3, -3) && Set(p.X, p.Y + 1)) return;
                }
                //斜线方向围堵
                if (p.X - 2 > -1 && p.Y - 2 > -1 && (getValue(p.X - 2, p.Y - 2) == 1 || getValue(p.X - 2, p.Y - 2) == 2) && getValue(p.X - 1, p.Y - 1) == 0 &&
                   (p.X + p.Y == 4 || p.X + p.Y == 8 || p.Y - p.X == 4 || p.Y - p.X == 0))
                {
                    if (isContainMonksChess(p.X - 1, p.Y - 1, -3, -3) && Set(p.X - 1, p.Y - 1)) return;
                }
                if (p.X - 2 > -1 && p.Y + 2 < 9 && (getValue(p.X - 2, p.Y + 2) == 1 || getValue(p.X - 2, p.Y + 2) == 2) && getValue(p.X - 1, p.Y + 1) == 0 &&
                   (p.X + p.Y == 4 || p.X + p.Y == 8 || p.Y - p.X == 4 || p.Y - p.X == 0))
                {
                    if (isContainMonksChess(p.X - 1, p.Y + 1, -3, -3) && Set(p.X - 1, p.Y + 1)) return;
                }
                if (p.X + 2 < 5 && p.Y - 2 > -1 && (getValue(p.X + 2, p.Y - 2) == 1 || getValue(p.X + 2, p.Y - 2) == 2) && getValue(p.X + 1, p.Y - 1) == 0 &&
                   (p.X + p.Y == 4 || p.X + p.Y == 8 || p.Y - p.X == 4 || p.Y - p.X == 0))
                {
                    if (isContainMonksChess(p.X + 1, p.Y - 1, -3, -3) && Set(p.X + 1, p.Y - 1)) return;
                }
                if (p.X + 2 < 5 && p.Y + 2 < 9 && (getValue(p.X + 2, p.Y + 2) == 1 || getValue(p.X + 2, p.Y + 2) == 2) && getValue(p.X + 1, p.Y + 1) == 0 &&
                   (p.X + p.Y == 4 || p.X + p.Y == 8 || p.Y - p.X == 4 || p.Y - p.X == 0))
                {
                    if (isContainMonksChess(p.X + 1, p.Y + 1, -3, -3) && Set(p.X + 1, p.Y + 1)) return;
                }

                //特殊方向
                if (p.Y == 1 && ((p.X == 1 && getValue(2, 0) == 0) || (p.X == 3 && getValue(2, 0) == 0)))
                {
                    if (Set(2, 0)) return;
                }
                if (p.X == 2 && p.Y == 0 && getValue(3, 1) == 0)
                {
                    if (Set(3, 1)) return;
                }
                if (p.X == 2 && p.Y == 0 && getValue(1, 1) == 0)
                { if (Set(1, 1)) return; }

                if (pioFlags == 2)
                    pioFlags = 1;
                else
                    pioFlags = 2;
                BlockFlags++;
            }
            pioFlags = getRandomMonksChess();
            while (NormalFlags <= 2)
            {
                TwzyPoint p = (pioFlags == 1) ? s : q;
                //纵向
                if (p.Y == 8 && p.X - 4 > -1 && getValue(p.X - 4, p.Y) == 0)//纵行第八条
                {
                    if (isContainMonksChess(p.X - 4, p.Y, -3, -3) && Set(p.X - 4, p.Y)) return;
                }
                if (p.X - 2 > -1 && getValue(p.X - 2, p.Y) == 0)
                {
                    if (isContainMonksChess(p.X - 2, p.Y, -3, -3) && Set(p.X - 2, p.Y)) return;
                }
                if (p.Y == 8 && p.X + 4 < 5 && getValue(p.X + 4, p.Y) == 0)//纵行第八条
                {
                    if (isContainMonksChess(p.X + 4, p.Y, -3, -3) && Set(p.X + 4, p.Y)) return;
                }
                if (p.X + 2 < 5 && getValue(p.X + 2, p.Y) == 0)
                {
                    if (isContainMonksChess(p.X + 2, p.Y, -3, -3) && Set(p.X + 2, p.Y)) return;
                }
                //横向
                if (p.Y - 2 > -1 && getValue(p.X, p.Y - 2) == 0 &&
                    (p.X == 2 || p.Y - 2 >= 1))
                {
                    if (isContainMonksChess(p.X, p.Y - 2, -3, -3) && Set(p.X, p.Y - 2)) return;
                }
                if (p.Y + 2 < 9 && getValue(p.X, p.Y + 2) == 0 &&
                    (p.X == 2 || p.Y + 2 <= 7))
                {
                    if (isContainMonksChess(p.X, p.Y + 2, -3, -3) && Set(p.X, p.Y + 2)) return;
                }
                //斜线正常
                if (p.X - 2 > -1 && p.Y - 2 > -1 && getValue(p.X - 2, p.Y - 2) == 0 &&
                   (p.Y - p.X == 4 || p.Y - p.X == 0 || p.Y - p.X == -2))
                {
                    if (isContainMonksChess(p.X - 2, p.Y - 2, -3, -3) && Set(p.X - 2, p.Y - 2)) return;
                }
                if (p.X - 2 > -1 && p.Y + 2 < 9 && getValue(p.X - 2, p.Y + 2) == 0 &&
                   (p.X + p.Y == 4 || p.X + p.Y == 8 || p.X + p.Y == 2))
                {
                    if (isContainMonksChess(p.X - 2, p.Y + 2, -3, -3) && Set(p.X - 2, p.Y + 2)) return;
                }
                if (p.X + 2 < 5 && p.Y - 2 > -1 && getValue(p.X + 2, p.Y - 2) == 0 &&
                   (p.X + p.Y == 4 || p.X + p.Y == 8 || p.X + p.Y == 2))
                {
                    if (isContainMonksChess(p.X + 2, p.Y - 2, -3, -3) && Set(p.X + 2, p.Y - 2)) return;
                }
                if (p.X + 2 < 5 && p.Y + 2 < 9 && getValue(p.X + 2, p.Y + 2) == 0 &&
                   (p.Y - p.X == 4 || p.Y - p.X == 0 || p.Y - p.X == -2))
                {
                    if (isContainMonksChess(p.X + 2, p.Y + 2, -3, -3) && Set(p.X + 2, p.Y + 2)) return;
                }

                if (pioFlags == 2)
                    pioFlags = 1;
                else
                    pioFlags = 2;
                NormalFlags++;
            }
            #endregion

            Random ran = new Random(DateTime.Now.Millisecond);
            while (!Set(ran.Next(0, 5), ran.Next(0, 9)))
            {
                continue;
            };
        }

        private void DogfaceChessMove(TwzyPoint s, TwzyPoint q)
        {
            int BlockFlags = 1;
            int SecouryFlags = 1;
            int InisetFlags = 1;
            int NormalFlags = 1;
            int RondaFlags = 1;

            #region //阻塞
            int PioFlags = getRandomMonksChess();
            while (BlockFlags <= 2)
            {
                TwzyPoint p = (PioFlags == 1) ? s : q;

                #region  第八列
                //追尾
                if (p.Y == 8 && p.X - 4 > -1 && (getValue(p.X - 2, p.Y) == 1 || getValue(p.X - 2, p.Y) == 2) && getValue(p.X - 4, p.Y) == 0)//确定目的地址
                {
                    //从图中可以看出挽救该种方法只有一种
                    if (getValue(p.X - 3, p.Y - 1) == 1)//确定源地址
                    {
                        if (isContainMonksChess(p.X - 4, p.Y, p.X - 3, p.Y - 1) && Move(p.X - 3, p.Y - 1, p.X - 4, p.Y)) return;
                    }
                }
                if (p.Y == 8 && p.X + 4 < 5 && (getValue(p.X + 2, p.Y) == 1 || getValue(p.X + 2, p.Y) == 2) && getValue(p.X + 4, p.Y) == 0)//确定目的地址
                {
                    //从图中可以看出挽救该种方法只有一种
                    if (getValue(p.X + 3, p.Y - 1) == 1)//确定源地址
                    {
                        if (isContainMonksChess(p.X + 4, p.Y, p.X + 3, p.Y - 1) && Move(p.X + 3, p.Y - 1, p.X + 4, p.Y)) return;
                    }
                }

                #endregion

                #region 竖直方向
                //追尾
                if (p.X - 2 > -1 && getValue(p.X - 1, p.Y) == 1 && getValue(p.X - 2, p.Y) == 0)
                {
                    //检查竖直方向的棋子
                    if (p.X - 3 > -1 && getValue(p.X - 3, p.Y) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y, p.X - 3, p.Y) && Move(p.X - 3, p.Y, p.X - 2, p.Y)) return;
                    }
                    //横向检查可利用的棋子
                    if (p.Y - 1 > -1 && getValue(p.X - 2, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y, p.X - 2, p.Y - 1) && Move(p.X - 2, p.Y - 1, p.X - 2, p.Y)) return;
                    }
                    if (p.Y + 1 < 9 && getValue(p.X - 2, p.Y + 1) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y, p.X - 2, p.Y + 1) && Move(p.X - 2, p.Y + 1, p.X - 2, p.Y)) return;
                    }
                    //检查斜线方向
                    if (p.X - 3 > -1 && p.Y - 1 < -1 && getValue(p.X - 3, p.Y - 1) == 1 && (p.Y - p.X == -2 || p.Y - p.X == 2))
                    {
                        if (isContainMonksChess(p.X - 2, p.Y, p.X - 3, p.Y - 1) && Move(p.X - 3, p.Y - 1, p.X - 2, p.Y)) return;
                    }
                    if (p.X - 1 > -1 && p.Y - 1 > -1 && getValue(p.X - 1, p.Y - 1) == 1 && (p.Y + p.X == 6 || p.Y + p.X == 10))
                    {
                        if (isContainMonksChess(p.X - 2, p.Y, p.X - 1, p.Y - 1) && Move(p.X - 1, p.Y - 1, p.X - 2, p.Y)) return;
                    }
                    if (p.X - 3 > -1 && p.Y + 1 < 9 && getValue(p.X - 3, p.Y + 1) == 1 && (p.Y + p.X == 6 || p.Y + p.X == 10))
                    {
                        if (isContainMonksChess(p.X - 2, p.Y, p.X - 3, p.Y + 1) && Move(p.X - 3, p.Y + 1, p.X - 2, p.Y)) return;
                    }
                    if (p.X - 1 > -1 && p.Y + 1 > 9 && getValue(p.X - 1, p.Y + 1) == 1 && (p.Y - p.X == -2 || p.Y - p.X == 2))
                    {
                        if (isContainMonksChess(p.X - 2, p.Y, p.X - 1, p.Y + 1) && Move(p.X - 1, p.Y + 1, p.X - 2, p.Y)) return;
                    }
                }
                if (p.X + 2 < 5 && getValue(p.X + 1, p.Y) == 1 && getValue(p.X + 2, p.Y) == 0)
                {
                    //竖直方向
                    if (p.X + 3 < 5 && getValue(p.X + 3, p.Y) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y, p.X + 3, p.Y) && Move(p.X + 3, p.Y, p.X + 2, p.Y)) return;
                    }
                    //横向
                    if (p.Y - 1 > -1 && getValue(p.X + 2, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y, p.X + 2, p.Y - 1) && Move(p.X + 2, p.Y - 1, p.X + 2, p.Y)) return;
                    }
                    if (p.Y + 1 < 9 && getValue(p.X + 2, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y, p.X + 2, p.Y + 1) && Move(p.X + 2, p.Y + 1, p.X + 2, p.Y)) return;
                    }
                    //斜线
                    if (p.X + 3 < 5 && p.Y - 1 > -1 && getValue(p.X + 3, p.Y - 1) == 1 && (p.X + p.Y == 2 || p.X + p.Y == 6))
                    {
                        if (isContainMonksChess(p.X + 2, p.Y, p.X + 3, p.Y - 1) && Move(p.X + 3, p.Y - 1, p.X + 2, p.Y)) return;
                    }
                    if (p.X + 3 < 5 && p.Y + 1 < 9 && getValue(p.X + 3, p.Y + 1) == 1 && (p.Y - p.Y == 2 || p.Y - p.X == 6))
                    {
                        if (isContainMonksChess(p.X + 2, p.Y, p.X + 3, p.Y + 1) && Move(p.X + 3, p.Y + 1, p.X + 2, p.Y)) return;
                    }
                    if (p.X + 1 < 5 && p.Y - 1 > -1 && getValue(p.X + 1, p.Y - 1) == 1 && (p.Y - p.Y == 2 || p.Y - p.X == 6))
                    {
                        if (isContainMonksChess(p.X + 2, p.Y, p.X + 1, p.Y - 1) && Move(p.X + 1, p.Y - 1, p.X + 2, p.Y)) return;
                    }
                    if (p.X + 1 < 5 && p.Y + 1 < 9 && getValue(p.X + 1, p.Y + 1) == 1 && (p.X + p.Y == 2 || p.X + p.Y == 6))
                    {
                        if (isContainMonksChess(p.X + 2, p.Y, p.X + 1, p.Y + 1) && Move(p.X + 1, p.Y + 1, p.X + 2, p.Y)) return;
                    }
                }


                #endregion

                #region 横向
                //追尾
                if (((p.X == 2 && p.Y - 2 > -1) || (p.Y - 2 > 1)) && getValue(p.X, p.Y - 1) == 1 && getValue(p.X, p.Y - 2) == 0)
                {
                    //横向
                    if (p.Y - 3 > -1 && getValue(p.X, p.Y - 3) == 1)
                    {
                        if (isContainMonksChess(p.X, p.Y - 2, p.X, p.Y - 3) && Move(p.X, p.Y - 3, p.X, p.Y - 2)) return;
                    }
                    //纵向
                    if (p.X - 1 > -1 && getValue(p.X - 1, p.Y - 2) == 1)
                    {
                        if (isContainMonksChess(p.X, p.Y - 2, p.X - 1, p.Y - 2) && Move(p.X - 1, p.Y - 2, p.X, p.Y - 2)) return;
                    }
                    if (p.X + 1 < 5 && getValue(p.X + 1, p.Y - 2) == 1)
                    {
                        if (isContainMonksChess(p.X, p.Y - 2, p.X + 1, p.Y - 2) && Move(p.X + 1, p.Y - 2, p.X, p.Y - 2)) return;
                    }
                    //斜线
                    if (p.Y - 3 > -1 && p.X - 1 > -1 && getValue(p.X - 1, p.Y - 3) == 1 && (p.Y - p.X == 2 || p.Y - p.X == 6))
                    {
                        if (isContainMonksChess(p.X, p.Y - 2, p.X - 1, p.Y - 3) && Move(p.X - 1, p.Y - 3, p.X, p.Y - 2)) return;
                    }
                    if (p.Y - 3 > -1 && p.X + 1 < 5 && getValue(p.X + 1, p.Y - 3) == 1 && (p.X + p.Y == 6 || p.X + p.Y == 10))
                    {
                        if (isContainMonksChess(p.X, p.Y - 2, p.X + 1, p.Y - 3) && Move(p.X + 1, p.Y - 3, p.X, p.Y - 2)) return;
                    }
                    if (p.Y - 1 > -1 && p.X - 1 > -1 && getValue(p.X - 1, p.Y - 1) == 1 && (p.X + p.Y == 6 || p.X + p.Y == 10))
                    {
                        if (isContainMonksChess(p.X, p.Y - 2, p.X - 1, p.Y - 1) && Move(p.X - 1, p.Y - 1, p.X, p.Y - 2)) return;
                    }
                    if (p.Y - 1 > -1 && p.X + 1 < 5 && getValue(p.X + 1, p.Y - 1) == 1 && (p.Y - p.X == 2 || p.Y - p.X == 6))
                    {
                        if (isContainMonksChess(p.X, p.Y - 2, p.X + 1, p.Y - 1) && Move(p.X + 1, p.Y - 1, p.X, p.Y - 2)) return;
                    }

                }
                if (((p.X == 2 && p.Y + 2 < 9) || p.Y + 2 < 7) && getValue(p.X, p.Y + 1) == 1 && getValue(p.X, p.Y + 2) == 0)
                {
                    //横向
                    if (p.Y + 3 < 9 && getValue(p.X, p.Y + 3) == 1)
                    {
                        if (isContainMonksChess(p.X, p.Y + 2, p.X, p.Y + 3) && Move(p.X, p.Y + 3, p.X, p.Y + 2)) return;
                    }
                    //纵向
                    if (p.X - 1 > -1 && getValue(p.X - 1, p.Y + 2) == 1)
                    {
                        if (isContainMonksChess(p.X, p.Y + 2, p.X - 1, p.Y + 2) && Move(p.X - 1, p.Y + 2, p.X, p.Y + 2)) return;
                    }
                    if (p.X + 1 < 5 && getValue(p.X + 1, p.Y + 2) == 1)
                    {
                        if (isContainMonksChess(p.X, p.Y + 2, p.X + 1, p.Y + 2) && Move(p.X + 1, p.Y + 2, p.X, p.Y + 2)) return;
                    }
                    //斜线
                    if (p.Y + 3 < 9 && p.X - 1 > -1 && getValue(p.X + 3, p.Y - 1) == 1 && (p.X + p.Y == 6 || p.X + p.Y == 2))
                    {
                        if (isContainMonksChess(p.X, p.Y + 2, p.X - 1, p.Y + 3) && Move(p.X - 1, p.Y + 3, p.X, p.Y + 2)) return;
                    }
                    if (p.Y + 3 < 9 && p.X + 1 < 5 && getValue(p.X + 1, p.Y + 3) == 1 && (p.Y - p.X == -2 || p.Y - p.X == 2))
                    {
                        if (isContainMonksChess(p.X, p.Y + 2, p.X + 1, p.Y + 3) && Move(p.X + 1, p.Y + 3, p.X, p.Y + 2)) return;
                    }
                    if (p.Y + 1 < 9 && p.X - 1 > -1 && getValue(p.X - 1, p.Y + 1) == 1 && (p.Y - p.X == -2 || p.Y - p.X == 2))
                    {
                        if (isContainMonksChess(p.X, p.Y + 2, p.X - 1, p.Y + 1) && Move(p.X - 1, p.Y + 1, p.X, p.Y + 2)) return;
                    }
                    if (p.Y + 1 < 9 && p.X + 1 < 5 && getValue(p.X + 1, p.Y + 1) == 1 && (p.X + p.Y == 6 || p.X + p.Y == 2))
                    {
                        if (isContainMonksChess(p.X, p.Y + 2, p.X + 1, p.Y + 1) && Move(p.X + 1, p.Y + 1, p.X, p.Y + 2)) return;
                    }
                }

                #endregion

                #region 斜线方向
                //追尾
                if (p.X - 2 > -1 && p.Y - 2 > -1 && getValue(p.X - 1, p.Y - 1) == 1 && getValue(p.X - 2, p.Y - 2) == 0 && (p.Y - p.X == 0 || p.Y - p.X == 4))
                {
                    //竖直
                    if (p.X - 3 > -1 && getValue(p.X - 3, p.Y - 2) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y - 2, p.X - 3, p.Y - 2) && Move(p.X - 3, p.Y - 2, p.X - 2, p.Y - 2)) return;
                    }
                    if (p.X - 1 > -1 && getValue(p.X - 1, p.Y - 2) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y - 2, p.X - 1, p.Y - 2) && Move(p.X - 1, p.Y - 2, p.X - 2, p.Y - 2)) return;
                    }
                    //横向
                    if (p.Y - 3 > -1 && getValue(p.X - 2, p.Y - 3) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y - 2, p.X - 2, p.Y - 3) && Move(p.X - 2, p.Y - 3, p.X - 2, p.Y - 2)) return;
                    }
                    if (p.Y - 1 > -1 && getValue(p.X - 2, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y - 2, p.X - 2, p.Y - 1) && Move(p.X - 2, p.Y - 1, p.X - 2, p.Y - 2)) return;
                    }
                    //斜线
                    if (p.X - 3 > -1 && p.Y - 3 > -1 && getValue(p.X - 3, p.Y - 3) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y - 2, p.X - 3, p.Y - 3) && Move(p.X - 3, p.Y - 3, p.X - 2, p.Y - 2)) return;
                    }
                    if (p.X - 3 > -1 && p.Y - 1 > -1 && getValue(p.X - 3, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y - 2, p.X - 3, p.Y - 1) && Move(p.X - 3, p.Y - 1, p.X - 2, p.Y - 2)) return;
                    }
                    if (p.X - 1 > -1 && p.Y - 3 > -1 && getValue(p.X - 1, p.Y - 3) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y - 2, p.X - 1, p.Y - 3) && Move(p.X - 1, p.Y - 3, p.X - 2, p.Y - 2)) return;
                    }
                }
                if (p.X - 2 > -1 && p.Y + 2 < 9 && getValue(p.X - 1, p.Y + 1) == 1 && getValue(p.X - 2, p.Y + 2) == 0 && (p.X + p.Y == 4 || p.X + p.Y == 8))
                {
                    //竖直
                    if (p.X - 3 > -1 && getValue(p.X - 3, p.Y + 2) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y + 2, p.X - 3, p.Y + 2) && Move(p.X - 3, p.Y + 2, p.X - 2, p.Y + 2)) return;
                    }
                    if (p.X - 1 > -1 && getValue(p.X - 1, p.Y + 2) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y + 2, p.X - 1, p.Y + 2) && Move(p.X - 1, p.Y + 2, p.X - 2, p.Y + 2)) return;
                    }
                    //横向
                    if (p.Y + 3 < 9 && getValue(p.X - 2, p.Y + 3) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y + 2, p.X - 2, p.Y + 3) && Move(p.X - 2, p.Y + 3, p.X - 2, p.Y + 2)) return;
                    }
                    if (p.Y + 1 < 9 && getValue(p.X - 2, p.Y + 1) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y + 2, p.X - 2, p.Y + 1) && Move(p.X - 2, p.Y + 1, p.X - 2, p.Y + 2)) return;
                    }
                    //斜线
                    if (p.X - 3 > -1 && p.Y + 3 < 9 && getValue(p.X - 3, p.Y + 3) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y + 2, p.X - 3, p.Y + 3) && Move(p.X - 3, p.Y + 3, p.X - 2, p.Y + 2)) return;
                    }
                    if (p.X - 3 > -1 && p.Y + 1 < 9 && getValue(p.X - 3, p.Y + 1) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y + 2, p.X - 3, p.Y + 1) && Move(p.X - 3, p.Y + 1, p.X - 2, p.Y + 2)) return;
                    }
                    if (p.X - 1 > -1 && p.Y + 3 < 9 && getValue(p.X - 1, p.Y + 3) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y + 2, p.X - 1, p.Y + 3) && Move(p.X - 1, p.Y + 3, p.X - 2, p.Y + 2)) return;
                    }
                }
                if (p.X + 2 < 5 && p.Y - 2 > -1 && getValue(p.X + 1, p.Y - 1) == 1 && getValue(p.X + 2, p.Y - 2) == 0 && (p.X + p.Y == 4 || p.X + p.Y == 8))
                {
                    //竖直
                    if (p.X + 1 < 5 && getValue(p.X + 1, p.Y - 2) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y - 2, p.X + 1, p.Y - 2) && Move(p.X + 1, p.Y - 2, p.X + 2, p.Y - 2)) return;
                    }
                    if (p.X + 3 < 5 && getValue(p.X + 3, p.Y - 2) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y - 2, p.X + 3, p.Y - 2) && Move(p.X + 3, p.Y - 2, p.X + 2, p.Y - 2)) return;
                    }
                    //横向
                    if (p.Y - 3 < -1 && getValue(p.X + 2, p.Y - 3) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y - 2, p.X + 2, p.Y - 3) && Move(p.X + 2, p.Y - 3, p.X + 2, p.Y - 2)) return;
                    }
                    if (p.Y - 1 < -1 && getValue(p.X + 2, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y - 2, p.X + 2, p.Y - 1) && Move(p.X + 2, p.Y - 1, p.X + 2, p.Y - 2)) return;
                    }
                    //斜线
                    if (p.X + 1 < 5 && p.Y - 3 > -1 && getValue(p.X + 1, p.Y - 3) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y - 2, p.X + 1, p.Y - 3) && Move(p.X + 1, p.Y - 3, p.X + 2, p.Y - 2)) return;
                    }
                    if (p.X + 3 < 5 && p.Y - 3 > -1 && getValue(p.X + 3, p.Y - 3) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y - 2, p.X + 3, p.Y - 3) && Move(p.X + 3, p.Y - 3, p.X + 2, p.Y - 2)) return;
                    }
                    if (p.X + 3 < 5 && p.Y - 1 > -1 && getValue(p.X + 3, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y - 2, p.X + 3, p.Y - 1) && Move(p.X + 3, p.Y - 1, p.X + 2, p.Y - 2)) return;
                    }
                }
                if (p.X + 2 < 5 && p.Y + 2 < 9 && getValue(p.X + 1, p.Y + 1) == 1 && getValue(p.X + 2, p.Y + 2) == 0 && (p.Y - p.X == 0 || p.Y - p.X == 4))
                {
                    //竖直
                    if (p.X + 1 < 5 && getValue(p.X + 1, p.Y + 2) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y + 2, p.X + 1, p.Y + 2) && Move(p.X + 1, p.Y + 2, p.X + 2, p.Y + 2)) return;
                    }
                    if (p.X + 3 < 5 && getValue(p.X + 3, p.Y + 2) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y + 2, p.X + 3, p.Y + 2) && Move(p.X + 3, p.Y + 2, p.X + 2, p.Y + 2)) return;
                    }
                    //横向
                    if (p.Y + 1 < 9 && getValue(p.X + 2, p.Y + 1) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y + 2, p.X + 2, p.Y + 1) && Move(p.X + 2, p.Y + 1, p.X + 2, p.Y + 2)) return;
                    }
                    if (p.Y + 3 < 9 && getValue(p.X + 2, p.Y + 3) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y + 2, p.X + 2, p.Y + 3) && Move(p.X + 2, p.Y + 3, p.X + 2, p.Y + 2)) return;
                    }
                    //斜线
                    if (p.X + 3 < 5 && p.Y + 3 < 9 && getValue(p.X + 3, p.Y + 3) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y + 2, p.X + 3, p.Y + 3) && Move(p.X + 3, p.Y + 3, p.X + 2, p.Y + 2)) return;
                    }
                    if (p.X + 1 < 5 && p.Y + 3 < 9 && getValue(p.X + 1, p.Y + 3) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y + 2, p.X + 1, p.Y + 3) && Move(p.X + 1, p.Y + 3, p.X + 2, p.Y + 2)) return;
                    }
                    if (p.X + 3 < 5 && p.Y + 1 < 9 && getValue(p.X + 3, p.Y + 1) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y + 2, p.X + 3, p.Y + 1) && Move(p.X + 3, p.Y + 1, p.X + 2, p.Y + 2)) return;
                    }
                }


                #endregion

   
                if (PioFlags == 2)
                    PioFlags = 1;
                else
                    PioFlags = 2;
                BlockFlags++;
            }
            #endregion

            #region //自保
            PioFlags = getRandomMonksChess();
            while (SecouryFlags <= 2)
            {
                TwzyPoint p = (PioFlags == 1) ? s : q;

                #region 第八行

                if (p.Y == 8 && p.X - 2 > 0 && getValue(p.X - 2, p.Y) == 1)
                {
                    if (p.X - 4 > 0 && getValue(p.X - 4, p.Y) == 0)
                    {
                        if (isContainMonksChess(p.X - 4, p.Y, p.X - 2, p.Y) && Move(p.X - 2, p.Y, p.X - 4, p.Y)) return;
                    }
                    if (p.X - 2 > 0 && p.Y - 1 > -1 && getValue(p.X - 2, p.Y - 1) == 0)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y - 1, p.X - 2, p.Y) && Move(p.X - 2, p.Y, p.X - 2, p.Y - 1)) return;
                    }
                }

                #endregion

                #region 纵向
                if (p.X - 2 > -1 && getValue(p.X - 1, p.Y) == 1 && getValue(p.X - 2, p.Y) == 0)
                {
                    if (p.X - 2 > -1 && getValue(p.X - 2, p.Y) == 0)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y, p.X - 1, p.Y) && Move(p.X - 1, p.Y, p.X - 2, p.Y)) return;
                    }
                    if (p.Y - 1 > -1 && getValue(p.X - 1, p.Y - 1) == 0)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y - 1, p.X - 1, p.Y) && Move(p.X - 1, p.Y, p.X - 1, p.Y - 1)) return;
                    }
                    if (p.Y + 1 < 9 && getValue(p.X - 1, p.Y + 1) == 0)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y + 1, p.X - 1, p.Y) && Move(p.X - 1, p.Y, p.X - 1, p.Y + 1)) return;
                    }
                    if (p.X - 2 > -1 && p.Y - 1 > -1 && getValue(p.X - 2, p.Y - 1) == 0 &&
                        (p.Y - p.X == -1 || p.Y - p.X == 3))
                    {
                        if (isContainMonksChess(p.X - 2, p.Y - 1, p.X - 1, p.Y) && Move(p.X - 1, p.Y, p.X - 2, p.Y - 1)) return;
                    }
                    if (p.X - 2 > -1 && p.Y + 1 < 9 && getValue(p.X - 2, p.Y + 1) == 0 &&
                        (p.X + p.Y == 5 || p.X + p.Y == 9))
                    {
                        if (isContainMonksChess(p.X - 2, p.Y + 1, p.X - 1, p.Y) && Move(p.X - 1, p.Y, p.X - 2, p.Y + 1)) return;
                    }
                    if (p.Y - 1 > -1 && getValue(p.X, p.Y - 1) == 0 &&
                        (p.X + p.Y == 5 || p.X + p.Y == 9))
                    {
                        if (isContainMonksChess(p.X, p.Y - 1, p.X - 1, p.Y) && Move(p.X - 1, p.Y, p.X, p.Y - 1)) return;
                    }
                    if (p.Y + 1 < 9 && getValue(p.X, p.Y + 1) == 0 &&
                        (p.Y - p.X == -1 || p.Y - p.X == 3))
                    {
                        if (isContainMonksChess(p.X, p.Y + 1, p.X - 1, p.Y) && Move(p.X - 1, p.Y, p.X, p.Y + 1)) return;
                    }
                }
                if (p.X + 1 < 5 && getValue(p.X + 1, p.Y) == 1 && getValue(p.X + 2, p.Y) == 0)
                {
                    if (p.X + 2 < 5 && getValue(p.X + 2, p.Y) == 0)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y, p.X + 1, p.Y) && Move(p.X + 1, p.Y, p.X + 2, p.Y)) return;
                    }
                    if (p.Y - 1 > -1 && getValue(p.X + 1, p.Y - 1) == 0)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y - 1, p.X + 1, p.Y) && Move(p.X + 1, p.Y, p.X + 1, p.Y - 1)) return;
                    }
                    if (p.Y + 1 < 9 && getValue(p.X + 1, p.Y + 1) == 0)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y + 1, p.X + 1, p.Y) && Move(p.X + 1, p.Y, p.X + 1, p.Y + 1)) return;
                    }
                    //斜线
                    if (p.X + 2 < 5 && p.Y - 1 > -1 && getValue(p.X + 2, p.Y - 1) == 0 &&
                        (p.X + p.Y == 3 || p.X + p.Y == 7))
                    {
                        if (isContainMonksChess(p.X + 2, p.Y - 1, p.X + 1, p.Y) && Move(p.X + 1, p.Y, p.X + 2, p.Y - 1)) return;
                    }
                    if (p.X + 2 < 5 && p.Y + 1 < 9 && getValue(p.X + 2, p.Y + 1) == 0 &&
                        (p.Y - p.X == 1 || p.Y - p.X == 5))
                    {
                        if (isContainMonksChess(p.X + 2, p.Y + 1, p.X + 1, p.Y) && Move(p.X + 1, p.Y, p.X + 2, p.Y + 1)) return;
                    }
                    if (p.Y - 1 > -1 && getValue(p.X, p.Y - 1) == 0 &&
                        (p.Y - p.X == 1 || p.Y - p.X == 5))
                    {
                        if (isContainMonksChess(p.X, p.Y - 1, p.X + 1, p.Y) && Move(p.X + 1, p.Y, p.X, p.Y - 1)) return;
                    }
                    if (p.Y + 1 < 9 && getValue(p.X, p.Y + 2) == 0 &&
                        (p.X + p.Y == 3 || p.X + p.Y == 7))
                    {
                        if (isContainMonksChess(p.X, p.Y + 2, p.X + 1, p.Y) && Move(p.X + 1, p.Y, p.X, p.Y + 2)) return;
                    }
                }
                #endregion

                #region 横向
                if (((p.X == 2 && p.Y - 2 > -1) || p.Y - 2 > 1) && getValue(p.X, p.Y - 1) == 1 && getValue(p.X, p.Y - 2) == 0)
                {
                    if (p.X - 1 > -1 && getValue(p.X - 1, p.Y - 1) == 0)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y - 1, p.X, p.Y - 1) && Move(p.X, p.Y - 1, p.X - 1, p.Y - 1)) return;
                    }
                    if (p.X + 1 < 5 && getValue(p.X + 1, p.Y - 1) == 0)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y - 1, p.X, p.Y - 1) && Move(p.X, p.Y - 1, p.X + 1, p.Y - 1)) return;
                    }
                    if (p.Y - 2 > -1 && getValue(p.X, p.Y - 2) == 0)
                    {
                        if (isContainMonksChess(p.X, p.Y - 2, p.X, p.Y - 1) && Move(p.X, p.Y - 1, p.X, p.Y - 2)) return;
                    }
                    if (p.X - 1 > -1 && p.Y - 2 > -1 && getValue(p.X - 1, p.Y - 2) == 0 &&
                        (p.Y - p.X == 1 || p.Y - p.X == 5))
                    {
                        if (isContainMonksChess(p.X - 1, p.Y - 2, p.X, p.Y - 1) && Move(p.X, p.Y - 1, p.X - 1, p.Y - 2)) return;
                    }
                    if (p.X + 1 < 5 && p.Y - 2 > -1 && getValue(p.X + 1, p.Y - 2) == 0 &&
                        (p.X + p.Y == 5 || p.X + p.Y == 9))
                    {
                        if (isContainMonksChess(p.X + 1, p.Y - 2, p.X, p.Y - 1) && Move(p.X, p.Y - 1, p.X + 1, p.Y - 2)) return;
                    }
                    if (p.X - 1 > -1 && getValue(p.X - 1, p.Y) == 0 &&
                        (p.X + p.Y == 5 || p.X + p.Y == 9))
                    {
                        if (isContainMonksChess(p.X - 1, p.Y, p.X, p.Y - 1) && Move(p.X, p.Y - 1, p.X - 1, p.Y)) return;
                    }
                    if (p.X + 1 < 5 && getValue(p.X + 1, p.Y) == 0 &&
                        (p.Y - p.X == 1 || p.Y - p.X == 5))
                    {
                        if (isContainMonksChess(p.X + 1, p.Y, p.X, p.Y - 1) && Move(p.X, p.Y - 1, p.X + 1, p.Y)) return;
                    }

                }
                if (((p.X == 2 && p.Y + 1 < 9) || p.Y + 1 < 7) && getValue(p.X, p.Y + 1) == 1 && getValue(p.X, p.Y + 2) == 0)
                {
                    if (p.X - 1 > -1 && getValue(p.X - 1, p.Y + 1) == 0)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y + 1, p.X, p.Y + 1) && Move(p.X, p.Y + 1, p.X - 1, p.Y + 1)) return;
                    }
                    if (p.X + 1 < 5 && getValue(p.X + 1, p.Y + 1) == 0)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y + 1, p.X, p.Y + 1) && Move(p.X, p.Y + 1, p.X + 1, p.Y + 1)) return;
                    }
                    if (p.Y + 2 < 9 && getValue(p.X, p.Y + 2) == 0)
                    {
                        if (isContainMonksChess(p.X, p.Y + 2, p.X, p.Y + 1) && Move(p.X, p.Y + 1, p.X, p.Y + 2)) return;
                    }
                    if (p.X - 1 > -1 && p.Y + 2 < 9 && getValue(p.X - 1, p.Y + 2) == 0 &&
                        (p.X + p.Y == 3 || p.X + p.Y == 7))
                    {
                        if (isContainMonksChess(p.X - 1, p.Y + 2, p.X, p.Y + 1) && Move(p.X, p.Y + 1, p.X - 1, p.Y + 2)) return;
                    }
                    if (p.X + 1 < 5 && p.Y + 2 < 9 && getValue(p.X + 1, p.Y + 2) == 0 &&
                        (p.Y - p.X == -1 || p.Y - p.X == 3))
                    {
                        if (isContainMonksChess(p.X + 1, p.Y + 2, p.X, p.Y + 1) && Move(p.X, p.Y + 1, p.X + 1, p.Y + 2)) return;
                    }
                    if (p.X - 1 > -1 && getValue(p.X - 1, p.Y) == 0 &&
                        (p.Y - p.X == -1 || p.Y - p.X == 3))
                    {
                        if (isContainMonksChess(p.X - 1, p.Y, p.X, p.Y + 1) && Move(p.X, p.Y + 1, p.X - 1, p.Y)) return;
                    }
                    if (p.X + 1 < 5 && getValue(p.X + 1, p.Y) == 0 &&
                        (p.X + p.Y == 3 || p.X + p.Y == 7))
                    {
                        if (isContainMonksChess(p.X + 1, p.Y, p.X, p.Y + 1) && Move(p.X, p.Y + 1, p.X + 1, p.Y)) return;
                    }
                }
                #endregion

                #region 斜线
                if (p.X - 2 > -1 && p.Y - 2 > -1 && getValue(p.X - 1, p.Y - 1) == 1 && getValue(p.X - 2, p.Y - 2) == 0 &&
                        (p.Y - p.X == 0 || p.Y - p.X == 4))
                {
                    if (p.X - 2 > -1 && p.Y - 1 > -1 && getValue(p.X - 2, p.Y - 1) == 0)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y - 1, p.X - 1, p.Y - 1) && Move(p.X - 1, p.Y - 1, p.X - 2, p.Y - 1)) return;
                    }
                    if (p.Y - 1 > -1 && getValue(p.X, p.Y - 1) == 0)
                    {
                        if (isContainMonksChess(p.X, p.Y - 1, p.X - 1, p.Y - 1) && Move(p.X - 1, p.Y - 1, p.X, p.Y - 1)) return;
                    }
                    if (p.X - 1 > -1 && p.Y - 2 > -1 && getValue(p.X - 1, p.Y - 2) == 0)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y - 2, p.X - 1, p.Y - 1) && Move(p.X - 1, p.Y - 1, p.X - 1, p.Y - 2)) return;
                    }
                    if (p.X - 1 > -1 && getValue(p.X - 1, p.Y) == 0)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y, p.X - 1, p.Y - 1) && Move(p.X - 1, p.Y - 1, p.X - 1, p.Y)) return;
                    }

                    if (p.X - 2 > -1 && p.Y - 2 > -1 && getValue(p.X - 2, p.Y - 2) == 0 &&
                        (p.Y - p.X == 0 || p.Y - p.X == 4))
                    {
                        if (isContainMonksChess(p.X - 2, p.Y - 2, p.X - 1, p.Y - 1) && Move(p.X - 1, p.Y - 1, p.X - 2, p.Y - 2)) return;
                    }
                    if (p.X - 2 > -1 && getValue(p.X - 2, p.Y) == 0 &&
                        (p.X + p.Y == 6 || p.X + p.Y == 10))
                    {
                        if (isContainMonksChess(p.X - 2, p.Y, p.X - 1, p.Y - 1) && Move(p.X - 1, p.Y - 1, p.X - 2, p.Y)) return;
                    }
                    if (p.Y - 2 > -1 && getValue(p.X, p.Y - 2) == 0 &&
                        (p.X + p.Y == 6 || p.X + p.Y == 10))
                    {
                        if (isContainMonksChess(p.X, p.Y - 2, p.X - 1, p.Y - 1) && Move(p.X - 1, p.Y - 1, p.X, p.Y - 2)) return;
                    }

                }
                if (p.X - 2 > -1 && p.Y + 2 < 9 && getValue(p.X - 1, p.Y + 1) == 1 && getValue(p.X - 2, p.Y + 2) == 0 &&
                    (p.X + p.Y == 4 || p.X+ p.Y == 8))
                {
                    if (p.X - 2 > -1 && p.Y + 1 < 9 && getValue(p.X - 2, p.Y + 1) == 0)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y + 1, p.X - 1, p.Y + 1) && Move(p.X - 1, p.Y + 1, p.X - 2, p.Y + 1)) return;
                    }
                    if (p.Y + 1 < 9 && getValue(p.X, p.Y + 1) == 0)
                    {
                        if (isContainMonksChess(p.X, p.Y + 1, p.X - 1, p.Y + 1) && Move(p.X - 1, p.Y + 1, p.X, p.Y + 1)) return;
                    }
                    if (p.X - 1 > -1 && p.Y + 2 < 9 && getValue(p.X - 1, p.Y + 2) == 0)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y + 2, p.X - 1, p.Y + 1) && Move(p.X - 1, p.Y + 1, p.X - 1, p.Y + 2)) return;
                    }
                    if (p.X - 1 > -1 && getValue(p.X - 1, p.Y) == 0)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y, p.X - 1, p.Y + 1) && Move(p.X - 1, p.Y + 1, p.X - 1, p.Y)) return;
                    }

                    if (p.X - 2 > -1 && p.Y + 2 < 9 && getValue(p.X - 2, p.Y + 2) == 0 &&
                        (p.X + p.Y == 4 || p.X + p.Y == 8))
                    {
                        if (isContainMonksChess(p.X - 2, p.Y + 2, p.X - 1, p.Y + 1) && Move(p.X - 1, p.Y + 1, p.X - 2, p.Y + 2)) return;
                    }
                    if (p.X - 2 > -1 && getValue(p.X - 2, p.Y) == 0 &&
                        (p.Y - p.X == -2 || p.Y - p.X == 2))
                    {
                        if (isContainMonksChess(p.X - 2, p.Y, p.X - 1, p.Y + 1) && Move(p.X - 1, p.Y + 1, p.X - 2, p.Y)) return;
                    }
                    if (p.Y + 2 < 9 && getValue(p.X, p.Y + 2) == 0 &&
                        (p.Y - p.X == -2 || p.Y - p.X == 2))
                    {
                        if (isContainMonksChess(p.X, p.Y + 2, p.X - 1, p.Y + 1) && Move(p.X - 1, p.Y + 1, p.X, p.Y + 2)) return;
                    }
                }
                if (p.X + 2 < 5 && p.Y - 2 > -1 && getValue(p.X + 1, p.Y - 1) == 1 &&getValue(p.X+2,p.Y-2)==0&&
                    (p.X + p.Y == 4 || p.X + p.Y == 8))
                {
                    if (p.X + 2 < 5 && p.Y - 1 > -1 && getValue(p.X + 2, p.Y - 1) == 0)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y - 1, p.X + 1, p.Y - 1) && Move(p.X + 1, p.Y - 1, p.X + 2, p.Y - 1)) return;
                    }
                    if (p.Y - 1 > -1 && getValue(p.X, p.Y - 1) == 0)
                    {
                        if (isContainMonksChess(p.X, p.Y - 1, p.X + 1, p.Y - 1) && Move(p.X + 1, p.Y - 1, p.X, p.Y - 1)) return;
                    }
                    if (p.X + 1 < 5 && p.Y - 2 > -1 && getValue(p.X + 1, p.Y - 2) == 0)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y - 2, p.X + 1, p.Y - 1) && Move(p.X + 1, p.Y - 1, p.X + 1, p.Y - 2)) return;
                    }
                    if (p.X + 1 < 5 && getValue(p.X + 1, p.Y) == 0)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y, p.X + 1, p.Y - 1) && Move(p.X + 1, p.Y - 1, p.X + 1, p.Y)) return;
                    }

                    if (p.X + 2 < 5 && p.Y - 2 > -1 && getValue(p.X + 2, p.Y - 2) == 0 &&
                        (p.X + p.Y == 4 || p.X + p.Y == 8))
                    {
                        if (isContainMonksChess(p.X + 2, p.Y - 2, p.X + 1, p.Y - 1) && Move(p.X + 1, p.Y - 1, p.X + 2, p.Y - 2)) return;
                    }
                    if (p.X + 2 < 5 && getValue(p.X + 2, p.Y) == 0 &&
                        (p.Y - p.X == 2 || p.Y - p.X == 6))
                    {
                        if (isContainMonksChess(p.X + 2, p.Y, p.X + 1, p.Y - 1) && Move(p.X + 1, p.Y - 1, p.X + 2, p.Y)) return;
                    }
                    if (p.Y - 2 > -1 && getValue(p.X, p.Y - 2) == 0 &&
                        (p.Y - p.X == 2 || p.Y - p.X == 6))
                    {
                        if (isContainMonksChess(p.X, p.Y - 2, p.X + 1, p.Y - 1) && Move(p.X + 1, p.Y - 1, p.X, p.Y - 2)) return;
                    }
                }
                if (p.X + 2 < 5 && p.Y + 2 < 9 && getValue(p.X + 1, p.Y + 1) == 1 && getValue(p.X+2,p.Y+2)==0 &&
                    (p.Y - p.X == 0 || p.Y - p.X == 4))
                {
                    if (p.X + 2 < 5 && p.Y + 1 < 9 && getValue(p.X + 2, p.Y + 1) == 0)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y + 1, p.X + 1, p.Y + 1) && Move(p.X + 1, p.Y + 1, p.X + 2, p.Y + 1)) return;
                    }
                    if (p.Y + 1 < 9 && getValue(p.X, p.Y + 1) == 0)
                    {
                        if (isContainMonksChess(p.X, p.Y + 1, p.X + 1, p.Y + 1) && Move(p.X + 1, p.Y + 1, p.X, p.Y + 1)) return;
                    }
                    if (p.X + 1 < 5 && p.Y + 2 < 9 && getValue(p.X + 1, p.Y + 2) == 0)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y + 2, p.X + 1, p.Y + 1) && Move(p.X + 1, p.Y + 1, p.X + 1, p.Y + 2)) return;
                    }
                    if (p.X + 1 < 5 && getValue(p.X + 1, p.Y) == 0)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y, p.X + 1, p.Y + 1) && Move(p.X + 1, p.Y + 1, p.X + 1, p.Y)) return;
                    }

                    if (p.X + 2 < 5 && p.Y + 2 < 9 && getValue(p.X + 2, p.Y + 2) == 0 &&
                        (p.Y - p.X == 0 || p.Y - p.X == 4))
                    {
                        if (isContainMonksChess(p.X + 2, p.Y + 2, p.X + 1, p.Y + 1) && Move(p.X + 1, p.Y + 1, p.X + 2, p.Y + 2)) return;
                    }
                    if (p.X + 2 < 5 && getValue(p.X + 2, p.Y) == 0 &&
                        (p.X + p.Y == 2 || p.X + p.Y == 6))
                    {
                        if (isContainMonksChess(p.X + 2, p.Y, p.X + 1, p.Y + 1) && Move(p.X + 1, p.Y + 1, p.X + 2, p.Y)) return;
                    }
                    if (p.Y + 2 < 9 && getValue(p.X, p.Y + 2) == 0 &&
                       (p.X + p.Y == 2 || p.X + p.Y == 6))
                    {
                        if (isContainMonksChess(p.X + 1, p.Y + 2, p.X + 1, p.Y + 1) && Move(p.X + 1, p.Y + 1, p.X, p.Y + 2)) return;
                    }
                }
                #endregion

                if (PioFlags == 2)
                    PioFlags = 1;
                else
                    PioFlags = 2;
                SecouryFlags++;
            }
            #endregion

            #region //插入
            PioFlags = getRandomMonksChess();
            while (InisetFlags <= 2)
            {
                TwzyPoint p = (PioFlags == 1) ? s : q;

                #region 第八列
                //插入
                if (p.Y == 8 && p.X - 4 > -1 && getValue(p.X - 2, p.Y) == 0 && (getValue(p.X - 4, p.Y) == 1 || getValue(p.X - 4, p.Y) == 2))
                {
                    if (getValue(p.X - 2, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X - 2, p.Y, p.X - 2, p.Y - 1) && Move(p.X - 2, p.Y - 1, p.X - 2, p.Y)) return;
                    }
                }
                if (p.Y == 8 && p.X + 4 < 5 && getValue(p.X + 2, p.Y) == 0 && (getValue(p.X + 4, p.Y) == 1 || getValue(p.X + 4, p.Y) == 2))
                {
                    if (getValue(p.X + 2, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X + 2, p.Y, p.X + 2, p.Y - 1) && Move(p.X + 2, p.Y - 1, p.X + 2, p.Y)) return;
                    }
                }
                #endregion

                #region 竖直

                //插入
                if (p.X - 2 > -1 && getValue(p.X - 1, p.Y) == 0 && (getValue(p.X - 2, p.Y) == 1 || getValue(p.X - 2, p.Y) == 2))
                {
                    if (p.Y - 1 > -1 && getValue(p.X - 1, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y, p.X - 1, p.Y - 1) && Move(p.X - 1, p.Y - 1, p.X - 1, p.Y)) return;
                    }
                    if (p.Y + 1 < 9 && getValue(p.X - 1, p.Y + 1) == 1)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y, p.X - 1, p.Y + 1) && Move(p.X - 1, p.Y + 1, p.X - 1, p.Y)) return;
                    }
                    if (p.X - 2 > -1 && p.Y - 1 > -1 && getValue(p.X - 2, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y, p.X - 2, p.Y - 1) && Move(p.X - 2, p.Y - 1, p.X - 1, p.Y)) return;
                    }
                    if (p.X - 2 > -1 && p.Y + 1 < 9 && getValue(p.X - 2, p.Y + 1) == 1)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y, p.X - 2, p.Y + 1) && Move(p.X - 2, p.Y + 1, p.X - 1, p.Y)) return;
                    }
                    if (p.Y - 1 > -1 && getValue(p.X, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y, p.X, p.Y - 1) && Move(p.X, p.Y - 1, p.X - 1, p.Y)) return;
                    }
                    if (p.Y + 1 < 9 && getValue(p.X, p.Y + 1) == 1)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y, p.X, p.Y + 1) && Move(p.X, p.Y + 1, p.X - 1, p.Y)) return;
                    }
                }
                if (p.X + 2 < 5 && getValue(p.X + 1, p.Y) == 0 && (getValue(p.X + 2, p.Y) == 1 || getValue(p.X + 2, p.Y) == 2))
                {
                    if (p.Y - 1 > -1 && getValue(p.X + 1, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y, p.X + 1, p.Y - 1) && Move(p.X + 1, p.Y - 1, p.X + 1, p.Y)) return;
                    }
                    if (p.Y + 1 < 9 && getValue(p.X + 1, p.Y + 1) == 1)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y, p.X + 1, p.Y + 1) && Move(p.X + 1, p.Y + 1, p.X + 1, p.Y)) return;
                    }
                    if (p.X + 2 < 5 && p.Y - 1 > -1 && getValue(p.X + 2, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y, p.X + 2, p.Y - 1) && Move(p.X + 2, p.Y - 1, p.X + 1, p.Y)) return;
                    }
                    if (p.X + 2 < 5 && p.Y + 1 < 9 && getValue(p.X + 2, p.Y + 1) == 1)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y, p.X + 2, p.Y + 1) && Move(p.X + 2, p.Y + 1, p.X + 1, p.Y)) return;
                    }
                    if (p.Y - 1 > -1 && getValue(p.X, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y, p.X, p.Y - 1) && Move(p.X, p.Y - 1, p.X + 1, p.Y)) return;
                    }
                    if (p.Y + 1 < 9 && getValue(p.X, p.Y + 1) == 1)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y, p.X, p.Y + 1) && Move(p.X, p.Y + 1, p.X + 1, p.Y)) return;
                    }
                }

                #endregion

                #region 横平
                //插入
                if (((p.X == 2 && p.Y - 2 > -1) || (p.Y - 2 > 1)) && getValue(p.X, p.Y - 1) == 0 && (getValue(p.X, p.Y - 2) == 1 || getValue(p.X, p.Y - 2) == 2))
                {
                    if (p.X - 1 > -1 && getValue(p.X - 1, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X, p.Y - 1, p.X - 1, p.Y - 1) && Move(p.X - 1, p.Y - 1, p.X, p.Y - 1)) return;
                    }
                    if (p.X + 1 < 5 && getValue(p.X + 1, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X, p.Y - 1, p.X + 1, p.Y - 1) && Move(p.X + 1, p.Y - 1, p.X, p.Y - 1)) return;
                    }
                    if (p.X - 1 > -1 && p.Y - 2 > -1 && getValue(p.X - 1, p.Y - 2) == 1 && (p.Y - p.X == 1 || p.Y - p.X == 5))
                    {
                        if (isContainMonksChess(p.X, p.Y - 1, p.X - 1, p.Y - 2) && Move(p.X - 1, p.Y - 2, p.X, p.Y - 1)) return;
                    }
                    if (p.X + 1 < 5 && p.Y - 2 > -1 && getValue(p.X + 1, p.Y - 2) == 1 && (p.X + p.Y == 5 || p.X + p.Y == 9))
                    {
                        if (isContainMonksChess(p.X, p.Y - 1, p.X + 1, p.Y - 2) && Move(p.X + 1, p.Y - 2, p.X, p.Y - 1)) return;
                    }
                    if (p.X - 1 > -1 && getValue(p.X - 1, p.Y) == 1 && (p.X + p.Y == 5 || p.X + p.Y == 9))
                    {
                        if (isContainMonksChess(p.X, p.Y - 1, p.X - 1, p.Y) && Move(p.X - 1, p.Y, p.X, p.Y - 1)) return;
                    }
                    if (p.X + 1 < 5 && getValue(p.X + 1, p.Y) == 1 && (p.Y - p.X == 1 || p.Y - p.X == 5))
                    {
                        if (isContainMonksChess(p.X, p.Y - 1, p.X + 1, p.Y) && Move(p.X + 1, p.Y, p.X, p.Y - 1)) return;
                    }

                }
                if (((p.X == 2 && p.Y + 2 < 9) || p.Y + 2 < 7) && getValue(p.X, p.Y + 1) == 0 && (getValue(p.X, p.Y + 2) == 1 || getValue(p.X, p.Y + 2) == 2))
                {
                    if (p.X - 1 > -1 && getValue(p.X - 1, p.Y + 1) == 1)
                    {
                        if (isContainMonksChess(p.X, p.Y + 1, p.X - 1, p.Y + 1) && Move(p.X - 1, p.Y + 1, p.X, p.Y + 1)) return;
                    }
                    if (p.X + 1 < 5 && getValue(p.X + 1, p.Y + 1) == 1)
                    {
                        if (isContainMonksChess(p.X, p.Y + 1, p.X + 1, p.Y + 1) && Move(p.X + 1, p.Y + 1, p.X, p.Y + 1)) return;
                    }
                    if (p.X - 1 > -1 && p.Y + 2 < 9 && getValue(p.X - 1, p.Y + 2) == 1 && (p.X + p.Y == 3 || p.Y + p.X == 7))
                    {
                        if (isContainMonksChess(p.X, p.Y + 1, p.X - 1, p.Y + 2) && Move(p.X - 1, p.Y + 2, p.X, p.Y + 1)) return;
                    }
                    if (p.X + 1 < 5 && p.Y + 2 < 9 && getValue(p.X + 1, p.Y + 2) == 1 && (p.Y - p.X == -1 || p.Y - p.X == 3))
                    {
                        if (isContainMonksChess(p.X, p.Y + 1, p.X + 1, p.Y + 2) && Move(p.X + 1, p.Y + 2, p.X, p.Y + 1)) return;
                    }
                    if (p.X - 1 > -1 && getValue(p.X - 1, p.Y) == 1 && (p.Y - p.X == -1 || p.Y - p.X == 3))
                    {
                        if (isContainMonksChess(p.X, p.Y + 1, p.X - 1, p.Y) && Move(p.X - 1, p.Y, p.X, p.Y + 1)) return;
                    }
                    if (p.X + 1 < 5 && getValue(p.X + 1, p.Y) == 1 && (p.X + p.Y == 3 || p.Y + p.X == 7))
                    {
                        if (isContainMonksChess(p.X, p.Y + 1, p.X + 1, p.Y + 1) && Move(p.X + 1, p.Y + 1, p.X, p.Y + 1)) return;
                    }
                }
                #endregion

                #region 斜线
                //插入
                if (p.X - 2 > -1 && p.Y - 2 > -1 && getValue(p.X - 1, p.Y - 1) == 0 &&
                    (getValue(p.X - 2, p.Y - 2) == 1 || getValue(p.X - 2, p.Y - 2) == 2) &&
                    (p.Y - p.X == 0 || p.Y - p.X == 4))
                {
                    //竖直
                    if (p.X - 2 > -1 && p.Y - 1 > -1 && getValue(p.X - 2, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y - 1, p.X - 2, p.Y - 1) && Move(p.X - 2, p.Y - 1, p.X - 1, p.Y - 1)) return;
                    }
                    if (p.Y - 1 > -1 && getValue(p.X, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y - 1, p.X, p.Y - 1) && Move(p.X, p.Y - 1, p.X - 1, p.Y - 1)) return;
                    }
                    //横向
                    if (p.X - 1 > -1 && p.Y - 2 > -1 && getValue(p.X - 1, p.Y - 2) == 1)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y - 1, p.X - 1, p.Y - 2) && Move(p.X - 1, p.Y - 2, p.X - 1, p.Y - 1)) return;
                    }
                    if (p.X - 1 > -1 && getValue(p.X - 1, p.Y) == 1)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y - 1, p.X - 1, p.Y) && Move(p.X - 1, p.Y, p.X - 1, p.Y - 1)) return;
                    }
                    //斜线
                    if (p.X - 2 > -1 && getValue(p.X - 2, p.Y) == 1 && (p.X + p.Y == 6 || p.X + p.Y == 10))
                    {
                        if (isContainMonksChess(p.X - 1, p.Y - 1, p.X - 2, p.Y) && Move(p.X - 2, p.Y, p.X - 1, p.Y - 1)) return;
                    }
                    if (p.Y - 2 > -1 && getValue(p.X, p.Y - 2) == 1 && (p.X + p.Y == 6 || p.X + p.Y == 10))
                    {
                        if (isContainMonksChess(p.X - 1, p.Y - 1, p.X, p.Y - 2) && Move(p.X, p.Y - 2, p.X - 1, p.Y - 1)) return;
                    }

                }
                if (p.X - 2 > -1 && p.Y + 2 < 9 && getValue(p.X - 1, p.Y + 1) == 0 &&
                    (getValue(p.X - 2, p.Y + 2) == 1 || getValue(p.X - 2, p.Y + 2) == 2) &&
                    (p.X + p.Y == 4 || p.X + p.Y == 8))
                {
                    //竖直
                    if (p.X - 2 > -1 && p.Y + 1 < 9 && getValue(p.X - 2, p.Y + 1) == 1)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y + 1, p.X - 2, p.Y + 1) && Move(p.X - 2, p.Y + 1, p.X - 1, p.Y + 1)) return;
                    }
                    if (p.Y + 1 < 9 && getValue(p.X, p.Y + 1) == 1)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y + 1, p.X, p.Y + 1) && Move(p.X, p.Y + 1, p.X - 1, p.Y + 1)) return;
                    }
                    //横向
                    if (p.X - 1 > -1 && p.Y + 2 < 9 && getValue(p.X - 1, p.Y + 2) == 1)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y + 1, p.X - 1, p.Y + 2) && Move(p.X - 1, p.Y + 2, p.X - 1, p.Y + 1)) return;
                    }
                    if (p.X - 1 > -1 && getValue(p.X - 1, p.Y) == 1)
                    {
                        if (isContainMonksChess(p.X - 1, p.Y + 1, p.X - 1, p.Y) && Move(p.X - 1, p.Y, p.X - 1, p.Y + 1)) return;
                    }
                    //斜线
                    if (p.X - 2 > -1 && getValue(p.X - 2, p.Y) == 1 && (p.Y - p.X == 2 || p.Y - p.X == -2))
                    {
                        if (isContainMonksChess(p.X - 1, p.Y + 1, p.X - 2, p.Y) && Move(p.X - 2, p.Y, p.X - 1, p.Y + 1)) return;
                    }
                    if (p.Y + 2 < 9 && getValue(p.X, p.Y + 2) == 1 && (p.Y - p.X == 2 || p.Y - p.X == -2))
                    {
                        if (isContainMonksChess(p.X - 1, p.Y + 1, p.X, p.Y + 2) && Move(p.X, p.Y + 2, p.X - 1, p.Y + 1)) return;
                    }
                }
                if (p.X + 2 < 5 && p.Y - 2 > -1 && getValue(p.X + 1, p.Y - 1) == 0 &&
                    (getValue(p.X + 2, p.Y - 2) == 1 || getValue(p.X + 2, p.Y - 2) == 2) &&
                    (p.X + p.Y == 4 || p.X + p.Y == 8))
                {
                    //竖直
                    if (p.X + 2 < 5 && p.Y - 1 > -1 && getValue(p.X + 2, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y - 1, p.X + 2, p.Y - 1) && Move(p.X + 2, p.Y - 1, p.X + 1, p.Y - 1)) return;
                    }
                    if (p.Y - 1 > -1 && getValue(p.X, p.Y - 1) == 1)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y - 1, p.X, p.Y - 1) && Move(p.X, p.Y - 1, p.X + 1, p.Y - 1)) return;
                    }
                    //横向
                    if (p.X + 1 < 5 && p.Y - 2 > -1 && getValue(p.X + 1, p.Y - 2) == 1)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y - 1, p.X + 1, p.Y - 2) && Move(p.X + 1, p.Y - 2, p.X + 1, p.Y - 1)) return;
                    }
                    if (p.X + 1 < 5 && getValue(p.X + 1, p.Y) == 1)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y - 1, p.X + 1, p.Y) && Move(p.X + 1, p.Y, p.X + 1, p.Y - 1)) return;
                    }
                    //斜线
                    if (p.X + 2 < 5 && getValue(p.X + 2, p.Y) == 1 && (p.Y - p.X == 2 || p.Y - p.X == 6))
                    {
                        if (isContainMonksChess(p.X + 1, p.Y - 1, p.X + 2, p.Y) && Move(p.X + 2, p.Y, p.X + 1, p.Y - 1)) return;
                    }
                    if (p.Y - 2 > -1 && getValue(p.X, p.Y - 2) == 1 && (p.Y - p.X == 2 || p.Y - p.X == 6))
                    {
                        if (isContainMonksChess(p.X + 1, p.Y - 1, p.X, p.Y - 2) && Move(p.X, p.Y - 2, p.X + 1, p.Y - 1)) return;
                    }
                }
                if (p.X + 2 < 5 && p.Y + 2 < 9 && getValue(p.X + 1, p.Y + 1) == 0 &&
                    (getValue(p.X + 2, p.Y + 2) == 1 || getValue(p.X + 2, p.Y + 2) == 2) &&
                    (p.Y - p.X == 0 || p.Y - p.X == 4))
                {
                    //竖直
                    if (p.X + 2 < 5 && p.Y + 1 < 9 && getValue(p.X + 2, p.Y + 1) == 1)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y + 1, p.X + 2, p.Y + 1) && Move(p.X + 2, p.Y + 1, p.X + 1, p.Y + 1)) return;
                    }
                    if (p.Y + 1 < 9 && getValue(p.X, p.Y + 1) == 1)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y + 1, p.X, p.Y + 1) && Move(p.X, p.Y + 1, p.X + 1, p.Y + 1)) return;
                    }
                    //横向
                    if (p.X + 1 < 5 && p.Y + 2 < 9 && getValue(p.X + 1, p.Y + 2) == 1)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y + 1, p.X + 1, p.Y + 2) && Move(p.X + 1, p.Y + 2, p.X + 1, p.Y + 1)) return;
                    }
                    if (p.X + 1 < 5 && getValue(p.X + 1, p.Y) == 1)
                    {
                        if (isContainMonksChess(p.X + 1, p.Y + 1, p.X + 1, p.Y) && Move(p.X + 1, p.Y, p.X + 1, p.Y + 1)) return;
                    }
                    //斜线
                    if (p.X + 2 < 5 && getValue(p.X + 2, p.Y) == 1 && (p.X + p.Y == 2 || p.X + p.Y == 6))
                    {
                        if (isContainMonksChess(p.X + 1, p.Y + 1, p.X + 2, p.Y) && Move(p.X + 2, p.Y, p.X + 1, p.Y + 1)) return;
                    }
                    if (p.Y + 2 < 9 && getValue(p.X, p.Y + 2) == 1 && (p.X + p.Y == 2 || p.X + p.Y == 6))
                    {
                        if (isContainMonksChess(p.X + 1, p.Y + 1, p.X, p.Y + 2) && Move(p.X, p.Y + 2, p.X + 1, p.Y + 1)) return;
                    }
                }

                #endregion

                #region 特殊
                //特殊位置
                if (p.X == 1 && p.Y == 2 && getValue(0, 2) == 0 && getValue(0, 3) == 1)
                {
                    if (Move(0, 3, 0, 2)) return;
                }
                if (p.X == 1 && p.Y == 6 && getValue(0, 6) == 0 && getValue(0, 5) == 1)
                {
                    if (Move(0, 5, 0, 6)) return;
                }
                if (p.X == 3 && p.Y == 2 && getValue(4, 2) == 0 && getValue(4, 3) == 1)
                {
                    if (Move(4, 3, 4, 2)) return;
                }
                if (p.X == 3 && p.Y == 6 && getValue(4, 6) == 0 && getValue(4, 5) == 1)
                {
                    if (Move(4, 5, 4, 6)) return;
                }

                if (p.X == 0 && p.Y == 3 && getValue(0, 2) == 0 && getValue(1, 2) == 1)
                {
                    if (Move(1, 2, 0, 2)) return;
                }
                if (p.X == 0 && p.Y == 5 && getValue(0, 6) == 0 && getValue(1, 6) == 1)
                { if (Move(1, 6, 0, 6)) return; }
                if (p.X == 4 && p.Y == 3 && getValue(4, 2) == 0 && getValue(3, 2) == 1)
                { if (Move(3, 2, 4, 2)) return; }
                if (p.X == 4 && p.Y == 5 && getValue(4, 6) == 0 && getValue(3, 6) == 1)
                { if (Move(3, 6, 4, 6)) return; }
                #endregion

                if (PioFlags == 2)
                    PioFlags = 1;
                else
                    PioFlags = 2;
                InisetFlags++;
            }
            #endregion

            #region  //调兵遣将
            PioFlags = getRandomMonksChess();
            while (NormalFlags <= 2)
            {
                TwzyPoint p = (PioFlags == 1) ? s : q;

                for (int i = 1; i < 10; i++)
                {
                    #region 垂直
                    //依然是垂直方向
                    if (p.X - i > -1 && getValue(p.X - i, p.Y) == 0)
                    {
                        //检查竖直方向的棋子
                        if (p.X - (i + 1) > -1 && getValue(p.X - (i + 1), p.Y) == 1)
                        {
                            if (isContainMonksChess(p.X - i, p.Y, p.X - (i + 1), p.Y) && Move(p.X - (i + 1), p.Y, p.X - i, p.Y)) return;
                        }
                        //横向检查可利用的棋子
                        if (p.Y - 1 > -1 && getValue(p.X - i, p.Y - 1) == 1)
                        {
                            if (isContainMonksChess(p.X - i, p.Y, p.X - i, p.Y - 1) && Move(p.X - i, p.Y - 1, p.X - i, p.Y)) return;
                        }
                        if (p.Y + 1 < 9 && getValue(p.X - i, p.Y + 1) == 1)
                        {
                            if (isContainMonksChess(p.X - i, p.Y, p.X - i, p.Y + 1) && Move(p.X - i, p.Y + 1, p.X - i, p.Y)) return;
                        }
                        //检查斜线方向
                        if (p.X - (i + 1) > -1 && p.Y - 1 < -1 && getValue(p.X - (i + 1), p.Y - 1) == 1 && (p.Y - p.X == -i || p.Y - p.X == i))
                        {
                            if (isContainMonksChess(p.X - i, p.Y, p.X - (i + 1), p.Y - 1) && Move(p.X - (i + 1), p.Y - 1, p.X - i, p.Y)) return;
                        }
                        if (p.X - (i - 1) > -1 && p.Y - 1 > -1 && getValue(p.X - (i - 1), p.Y - 1) == 1 && (p.Y + p.X == 3 * i || p.Y + p.X == 5 * i))
                        {
                            if (isContainMonksChess(p.X - i, p.Y, p.X - (i - 1), p.Y - 1) && Move(p.X - (i - 1), p.Y - 1, p.X - i, p.Y)) return;
                        }
                        if (p.X - (i + 1) > -1 && p.Y + 1 < 9 && getValue(p.X - (i + 1), p.Y + 1) == 1 && (p.Y + p.X == 3 * i || p.Y + p.X == 5 * i))
                        {
                            if (isContainMonksChess(p.X - i, p.Y, p.X - (i + 1), p.Y + 1) && Move(p.X - (i + 1), p.Y + 1, p.X - i, p.Y)) return;
                        }
                        if (p.X - (i - 1) > -1 && p.Y + 1 > 9 && getValue(p.X - (i - 1), p.Y + 1) == 1 && (p.Y - p.X == -i || p.Y - p.X == i))
                        {
                            if (isContainMonksChess(p.X - i, p.Y, p.X - (i - 1), p.Y + 1) && Move(p.X - (i - 1), p.Y + 1, p.X - i, p.Y)) return;
                        }
                    }
                    if (p.X + i < 5 && getValue(p.X + i, p.Y) == 0)
                    {
                        //竖直方向
                        if (p.X + (i + 1) < 5 && getValue(p.X + (i + 1), p.Y) == 1)
                        {
                            if (isContainMonksChess(p.X + i, p.Y, p.X + (i + 1), p.Y) && Move(p.X + (i + 1), p.Y, p.X + i, p.Y)) return;
                        }
                        //横向
                        if (p.Y - 1 > -1 && getValue(p.X + i, p.Y - 1) == 1)
                        {
                            if (isContainMonksChess(p.X + i, p.Y, p.X + i, p.Y - 1) && Move(p.X + i, p.Y - 1, p.X + i, p.Y)) return;
                        }
                        if (p.Y + 1 < 9 && getValue(p.X + i, p.Y - 1) == 1)
                        {
                            if (isContainMonksChess(p.X + i, p.Y, p.X + i, p.Y + 1) && Move(p.X + i, p.Y + 1, p.X + i, p.Y)) return;
                        }
                        //斜线
                        if (p.X + (i + 1) < 5 && p.Y - 1 > -1 && getValue(p.X + (i + 1), p.Y - 1) == 1 && (p.X + p.Y == i || p.X + p.Y == 3 * i))
                        {
                            if (isContainMonksChess(p.X + i, p.Y, p.X + (i + 1), p.Y - 1) && Move(p.X + (i + 1), p.Y - 1, p.X + i, p.Y)) return;
                        }
                        if (p.X + (i + 1) < 5 && p.Y + 1 < 9 && getValue(p.X + (i + 1), p.Y + 1) == 1 && (p.Y - p.Y == i || p.Y - p.X == i * 3))
                        {
                            if (isContainMonksChess(p.X + i, p.Y, p.X + (i + 1), p.Y + 1) && Move(p.X + (i + 1), p.Y + 1, p.X + i, p.Y)) return;
                        }
                        if (p.X + (i - 1) < 5 && p.Y - 1 > -1 && getValue(p.X + (i - 1), p.Y - 1) == 1 && (p.Y - p.Y == i || p.Y - p.X == i * 3))
                        {
                            if (isContainMonksChess(p.X + i, p.Y, p.X + (i - 1), p.Y - 1) && Move(p.X + (i - 1), p.Y - 1, p.X + i, p.Y)) return;
                        }
                        if (p.X + (i - 1) < 5 && p.Y + 1 < 9 && getValue(p.X + (i - 1), p.Y + 1) == 1 && (p.X + p.Y == i || p.X + p.Y == i * 3))
                        {
                            if (isContainMonksChess(p.X + i, p.Y, p.X + (i - 1), p.Y + 1) && Move(p.X + (i - 1), p.Y + 1, p.X + i, p.Y)) return;
                        }
                    }
                    #endregion

                    #region 横向
                    //追尾
                    if (p.Y - i > -1 && getValue(p.X, p.Y - i) == 0)
                    {
                        //横向
                        if (p.Y - (i + 1) > -1 && getValue(p.X, p.Y - (i + 1)) == 1)
                        {
                            if (isContainMonksChess(p.X, p.Y - i, p.X, p.Y - (i + 1)) && Move(p.X, p.Y - (i + 1), p.X, p.Y - i)) return;
                        }
                        //纵向
                        if (p.X - 1 > -1 && getValue(p.X - 1, p.Y - i) == 1)
                        {
                            if (isContainMonksChess(p.X, p.Y - i, p.X - 1, p.Y - i) && Move(p.X - 1, p.Y - i, p.X, p.Y - i)) return;
                        }
                        if (p.X + 1 < 5 && getValue(p.X + 1, p.Y - i) == 1)
                        {
                            if (isContainMonksChess(p.X, p.Y - i, p.X + 1, p.Y - i) && Move(p.X + 1, p.Y - i, p.X, p.Y - i)) return;
                        }
                        //斜线
                        if (p.Y - (i + 1) > -1 && p.X - 1 > -1 && getValue(p.X - 1, p.Y - (i + 1)) == 1 && (p.Y - p.X == i || p.Y - p.X == i * 3))
                        {
                            if (isContainMonksChess(p.X, p.Y - i, p.X - 1, p.Y - (i + 1)) && Move(p.X - 1, p.Y - (i + 1), p.X, p.Y - i)) return;
                        }
                        if (p.Y - (i + 1) > -1 && p.X + 1 < 5 && getValue(p.X + 1, p.Y - (i + 1)) == 1 && (p.X + p.Y == i * 3 || p.X + p.Y == i * 5))
                        {
                            if (isContainMonksChess(p.X, p.Y - i, p.X + 1, p.Y - (i + 1)) && Move(p.X + 1, p.Y - (i + 1), p.X, p.Y - i)) return;
                        }
                        if (p.Y - (i - 1) > -1 && p.X - 1 > -1 && getValue(p.X - 1, p.Y - (i - 1)) == 1 && (p.X + p.Y == i * 3 || p.X + p.Y == i * 5))
                        {
                            if (isContainMonksChess(p.X, p.Y - i, p.X - 1, p.Y - (i - 1)) && Move(p.X - 1, p.Y - (i - 1), p.X, p.Y - i)) return;
                        }
                        if (p.Y - (i - 1) > -1 && p.X + 1 < 5 && getValue(p.X + 1, p.Y - (i - 1)) == 1 && (p.Y - p.X == i || p.Y - p.X == i * 3))
                        {
                            if (isContainMonksChess(p.X, p.Y - i, p.X + 1, p.Y - (i - 1)) && Move(p.X + 1, p.Y - (i - 1), p.X, p.Y - i)) return;
                        }

                    }
                    if (p.Y + i < 9 && getValue(p.X, p.Y + i) == 0)
                    {
                        //横向
                        if (p.Y + (i + 1) < 9 && getValue(p.X, p.Y + (i + 1)) == 1)
                        {
                            if (isContainMonksChess(p.X, p.Y + i, p.X, p.Y + (i + 1)) && Move(p.X, p.Y + (i + 1), p.X, p.Y + i)) return;
                        }
                        //纵向
                        if (p.X - 1 > -1 && getValue(p.X - 1, p.Y + i) == 1)
                        {
                            if (isContainMonksChess(p.X, p.Y + i, p.X - 1, p.Y + i) && Move(p.X - 1, p.Y + i, p.X, p.Y + i)) return;
                        }
                        if (p.X + 1 < 5 && getValue(p.X + 1, p.Y + i) == 1)
                        {
                            if (isContainMonksChess(p.X, p.Y + i, p.X + 1, p.Y + i) && Move(p.X + 1, p.Y + i, p.X, p.Y + i)) return;
                        }
                        //斜线
                        if (p.Y + (i + 1) < 9 && p.X - 1 > -1 && getValue(p.X + 3, p.Y + (i + 1)) == 1 && (p.X + p.Y == i || p.X + p.Y == i * 3))
                        {
                            if (isContainMonksChess(p.X, p.Y + i, p.X - 1, p.Y + (i + 1)) && Move(p.X - 1, p.Y + (i + 1), p.X, p.Y + i)) return;
                        }
                        if (p.Y + (i + 1) < 9 && p.X + 1 < 5 && getValue(p.X + 1, p.Y + (i + 1)) == 1 && (p.Y - p.X == -i || p.Y - p.X == i))
                        {
                            if (isContainMonksChess(p.X, p.Y + i, p.X + 1, p.Y + (i + 1)) && Move(p.X + 1, p.Y + (i + 1), p.X, p.Y + i)) return;
                        }
                        if (p.Y + (i - 1) < 9 && p.X - 1 > -1 && getValue(p.X - 1, p.Y + (i - 1)) == 1 && (p.Y - p.X == -i || p.Y - p.X == i))
                        {
                            if (isContainMonksChess(p.X, p.Y + i, p.X - 1, p.Y + (i - 1)) && Move(p.X - 1, p.Y + (i - 1), p.X, p.Y + i)) return;
                        }
                        if (p.Y + (i - 1) < 9 && p.X + 1 < 5 && getValue(p.X + 1, p.Y + (i - 1)) == 1 && (p.X + p.Y == i || p.X + p.Y == i * 3))
                        {
                            if (isContainMonksChess(p.X, p.Y + i, p.X + 1, p.Y + (i - 1)) && Move(p.X + 1, p.Y + (i - 1), p.X, p.Y + i)) return;
                        }
                    }
                    #endregion

                    if (i == 2)
                    {
                        #region 斜线方向
                        //追尾
                        if (p.X - 2 > -1 && p.Y - 2 > -1 && getValue(p.X - 2, p.Y - 2) == 0 && (p.Y - p.X == 0 || p.Y - p.X == 4))
                        {
                            //竖直
                            if (p.X - 3 > -1 && getValue(p.X - 3, p.Y - 2) == 1)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y - 2, p.X - 3, p.Y - 2) && Move(p.X - 3, p.Y - 2, p.X - 2, p.Y - 2)) return;
                            }
                            if (p.X - 1 > -1 && getValue(p.X - 1, p.Y - 2) == 1)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y - 2, p.X - 1, p.Y - 2) && Move(p.X - 1, p.Y - 2, p.X - 2, p.Y - 2)) return;
                            }
                            //横向
                            if (p.Y - 3 > -1 && getValue(p.X - 2, p.Y - 3) == 1)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y - 2, p.X - 2, p.Y - 3) && Move(p.X - 2, p.Y - 3, p.X - 2, p.Y - 2)) return;
                            }
                            if (p.Y - 1 > -1 && getValue(p.X - 2, p.Y - 1) == 1)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y - 2, p.X - 2, p.Y - 1) && Move(p.X - 2, p.Y - 1, p.X - 2, p.Y - 2)) return;
                            }
                            //斜线
                            if (p.X - 3 > -1 && p.Y - 3 > -1 && getValue(p.X - 3, p.Y - 3) == 1)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y - 2, p.X - 3, p.Y - 3) && Move(p.X - 3, p.Y - 3, p.X - 2, p.Y - 2)) return;
                            }
                            if (p.X - 3 > -1 && p.Y - 1 > -1 && getValue(p.X - 3, p.Y - 1) == 1)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y - 2, p.X - 3, p.Y - 1) && Move(p.X - 3, p.Y - 1, p.X - 2, p.Y - 2)) return;
                            }
                            if (p.X - 1 > -1 && p.Y - 3 > -1 && getValue(p.X - 1, p.Y - 3) == 1)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y - 2, p.X - 1, p.Y - 3) && Move(p.X - 1, p.Y - 3, p.X - 2, p.Y - 2)) return;
                            }
                        }
                        if (p.X - 2 > -1 && p.Y + 2 < 9 && getValue(p.X - 2, p.Y + 2) == 0 && (p.X + p.Y == 4 || p.X + p.Y == 8))
                        {
                            //竖直
                            if (p.X - 3 > -1 && getValue(p.X - 3, p.Y + 2) == 1)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y + 2, p.X - 3, p.Y + 2) && Move(p.X - 3, p.Y + 2, p.X - 2, p.Y + 2)) return;
                            }
                            if (p.X - 1 > -1 && getValue(p.X - 1, p.Y + 2) == 1)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y + 2, p.X - 1, p.Y + 2) && Move(p.X - 1, p.Y + 2, p.X - 2, p.Y + 2)) return;
                            }
                            //横向
                            if (p.Y + 3 < 9 && getValue(p.X - 2, p.Y + 3) == 1)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y + 2, p.X - 2, p.Y + 3) && Move(p.X - 2, p.Y + 3, p.X - 2, p.Y + 2)) return;
                            }
                            if (p.Y + 1 < 9 && getValue(p.X - 2, p.Y + 1) == 1)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y + 2, p.X - 2, p.Y + 1) && Move(p.X - 2, p.Y + 1, p.X - 2, p.Y + 2)) return;
                            }
                            //斜线
                            if (p.X - 3 > -1 && p.Y + 3 < 9 && getValue(p.X - 3, p.Y + 3) == 1)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y + 2, p.X - 3, p.Y + 3) && Move(p.X - 3, p.Y + 3, p.X - 2, p.Y + 2)) return;
                            }
                            if (p.X - 3 > -1 && p.Y + 1 < 9 && getValue(p.X - 3, p.Y + 1) == 1)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y + 2, p.X - 3, p.Y + 1) && Move(p.X - 3, p.Y + 1, p.X - 2, p.Y + 2)) return;
                            }
                            if (p.X - 1 > -1 && p.Y + 3 < 9 && getValue(p.X - 1, p.Y + 3) == 1)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y + 2, p.X - 1, p.Y + 3) && Move(p.X - 1, p.Y + 3, p.X - 2, p.Y + 2)) return;
                            }
                        }
                        if (p.X + 2 < 5 && p.Y - 2 > -1 && getValue(p.X + 2, p.Y - 2) == 0 && (p.X + p.Y == 4 || p.X + p.Y == 8))
                        {
                            //竖直
                            if (p.X + 1 < 5 && getValue(p.X + 1, p.Y - 2) == 1)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y - 2, p.X + 1, p.Y - 2) && Move(p.X + 1, p.Y - 2, p.X + 2, p.Y - 2)) return;
                            }
                            if (p.X + 3 < 5 && getValue(p.X + 3, p.Y - 2) == 1)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y - 2, p.X + 3, p.Y - 2) && Move(p.X + 3, p.Y - 2, p.X + 2, p.Y - 2)) return;
                            }
                            //横向
                            if (p.Y - 3 < -1 && getValue(p.X + 2, p.Y - 3) == 1)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y - 2, p.X + 2, p.Y - 3) && Move(p.X + 2, p.Y - 3, p.X + 2, p.Y - 2)) return;
                            }
                            if (p.Y - 1 < -1 && getValue(p.X + 2, p.Y - 1) == 1)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y - 2, p.X + 2, p.Y - 1) && Move(p.X + 2, p.Y - 1, p.X + 2, p.Y - 2)) return;
                            }
                            //斜线
                            if (p.X + 1 < 5 && p.Y - 3 > -1 && getValue(p.X + 1, p.Y - 3) == 1)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y - 2, p.X + 1, p.Y - 3) && Move(p.X + 1, p.Y - 3, p.X + 2, p.Y - 2)) return;
                            }
                            if (p.X + 3 < 5 && p.Y - 3 > -1 && getValue(p.X + 3, p.Y - 3) == 1)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y - 2, p.X + 3, p.Y - 3) && Move(p.X + 3, p.Y - 3, p.X + 2, p.Y - 2)) return;
                            }
                            if (p.X + 3 < 5 && p.Y - 1 > -1 && getValue(p.X + 3, p.Y - 1) == 1)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y - 2, p.X + 3, p.Y - 1) && Move(p.X + 3, p.Y - 1, p.X + 2, p.Y - 2)) return;
                            }
                        }
                        if (p.X + 2 < 5 && p.Y + 2 < 9 && getValue(p.X + 2, p.Y + 2) == 0 && (p.Y - p.X == 0 || p.Y - p.X == 4))
                        {
                            //竖直
                            if (p.X + 1 < 5 && getValue(p.X + 1, p.Y + 2) == 1)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y + 2, p.X + 1, p.Y + 2) && Move(p.X + 1, p.Y + 2, p.X + 2, p.Y + 2)) return;
                            }
                            if (p.X + 3 < 5 && getValue(p.X + 3, p.Y + 2) == 1)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y + 2, p.X + 3, p.Y + 2) && Move(p.X + 3, p.Y + 2, p.X + 2, p.Y + 2)) return;
                            }
                            //横向
                            if (p.Y + 1 < 9 && getValue(p.X + 2, p.Y + 1) == 1)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y + 2, p.X + 2, p.Y + 1) && Move(p.X + 2, p.Y + 1, p.X + 2, p.Y + 2)) return;
                            }
                            if (p.Y + 3 < 9 && getValue(p.X + 2, p.Y + 3) == 1)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y + 2, p.X + 2, p.Y + 3) && Move(p.X + 2, p.Y + 3, p.X + 2, p.Y + 2)) return;
                            }
                            //斜线
                            if (p.X + 3 < 5 && p.Y + 3 < 9 && getValue(p.X + 3, p.Y + 3) == 1)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y + 2, p.X + 3, p.Y + 3) && Move(p.X + 3, p.Y + 3, p.X + 2, p.Y + 2)) return;
                            }
                            if (p.X + 1 < 5 && p.Y + 3 < 9 && getValue(p.X + 1, p.Y + 3) == 1)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y + 2, p.X + 1, p.Y + 3) && Move(p.X + 1, p.Y + 3, p.X + 2, p.Y + 2)) return;
                            }
                            if (p.X + 3 < 5 && p.Y + 1 < 9 && getValue(p.X + 3, p.Y + 1) == 1)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y + 2, p.X + 3, p.Y + 1) && Move(p.X + 3, p.Y + 1, p.X + 2, p.Y + 2)) return;
                            }
                        }
                        #endregion
                    }
                }
                if (PioFlags == 2)
                    PioFlags = 1;
                else
                    PioFlags = 2;

                NormalFlags++;
            }
            #endregion

            #region//游弋
            PioFlags = getRandomMonksChess();
            while (RondaFlags <= 2)
            {
                TwzyPoint p = (PioFlags == 1) ? s : q;

                for (int i = 1; i < 10; i++)
                {
                    #region 垂直
                    //依然是垂直方向
                    if (p.X - i > -1 && getValue(p.X - i, p.Y) == 1)
                    {
                        //检查竖直方向的棋子
                        if (p.X - (i + 1) > -1 && getValue(p.X - (i + 1), p.Y) == 0)
                        {
                            if (isContainMonksChess(p.X - (i + 1), p.Y, p.X - i, p.Y) && Move(p.X - i, p.Y, p.X - (i + 1), p.Y)) return;
                        }
                        //横向检查可利用的棋子
                        if (p.Y - 1 > -1 && getValue(p.X - i, p.Y - 1) == 0)
                        {
                            if (isContainMonksChess(p.X - i, p.Y - 1, p.X - i, p.Y) && Move(p.X - i, p.Y, p.X - i, p.Y - 1)) return;
                        }
                        if (p.Y + 1 < 9 && getValue(p.X - i, p.Y + 1) == 0)
                        {
                            if (isContainMonksChess(p.X - i, p.Y + 1, p.X - i, p.Y) && Move(p.X - i, p.Y, p.X - i, p.Y + 1)) return;
                        }
                        //检查斜线方向
                        if (p.X - (i + 1) > -1 && p.Y - 1 < -1 && getValue(p.X - (i + 1), p.Y - 1) == 0 && (p.Y - p.X == -i || p.Y - p.X == i))
                        {
                            if (isContainMonksChess(p.X - (i + 1), p.Y - 1, p.X - i, p.Y) && Move(p.X - i, p.Y, p.X - (i + 1), p.Y - 1)) return;
                        }
                        if (p.X - (i - 1) > -1 && p.Y - 1 > -1 && getValue(p.X - (i - 1), p.Y - 1) == 0 && (p.Y + p.X == 3 * i || p.Y + p.X == 5 * i))
                        {
                            if (isContainMonksChess(p.X - (i - 1), p.Y - 1, p.X - i, p.Y) && Move(p.X - i, p.Y, p.X - (i - 1), p.Y - 1)) return;
                        }
                        if (p.X - (i + 1) > -1 && p.Y + 1 < 9 && getValue(p.X - (i + 1), p.Y + 1) == 0 && (p.Y + p.X == 3 * i || p.Y + p.X == 5 * i))
                        {
                            if (isContainMonksChess(p.X - (i + 1), p.Y + 1, p.X - i, p.Y) && Move(p.X - i, p.Y, p.X - (i + 1), p.Y + 1)) return;
                        }
                        if (p.X - (i - 1) > -1 && p.Y + 1 > 9 && getValue(p.X - (i - 1), p.Y + 1) == 0 && (p.Y - p.X == -i || p.Y - p.X == i))
                        {
                            if (isContainMonksChess(p.X - (i - 1), p.Y + 1, p.X - i, p.Y) && Move(p.X - i, p.Y, p.X - (i - 1), p.Y + 1)) return;
                        }
                    }
                    if (p.X + i < 5 && getValue(p.X + i, p.Y) == 1)
                    {
                        //竖直方向
                        if (p.X + (i + 1) < 5 && getValue(p.X + (i + 1), p.Y) == 0)
                        {
                            if (isContainMonksChess(p.X + (i + 1), p.Y, p.X + i, p.Y) && Move(p.X + i, p.Y, p.X + (i + 1), p.Y)) return;
                        }
                        //横向
                        if (p.Y - 1 > -1 && getValue(p.X + i, p.Y - 1) == 0)
                        {
                            if (isContainMonksChess(p.X + i, p.Y - 1, p.X + i, p.Y) && Move(p.X + i, p.Y, p.X + i, p.Y - 1)) return;
                        }
                        if (p.Y + 1 < 9 && getValue(p.X + i, p.Y - 1) == 0)
                        {
                            if (isContainMonksChess(p.X + i, p.Y + 1, p.X + i, p.Y) && Move(p.X + i, p.Y, p.X + i, p.Y + 1)) return;
                        }
                        //斜线
                        if (p.X + (i + 1) < 5 && p.Y - 1 > -1 && getValue(p.X + (i + 1), p.Y - 1) == 0 && (p.X + p.Y == i || p.X + p.Y == 3 * i))
                        {
                            if (isContainMonksChess(p.X + (i + 1), p.Y - 1, p.X + i, p.Y) && Move(p.X + i, p.Y, p.X + (i + 1), p.Y - 1)) return;
                        }
                        if (p.X + (i + 1) < 5 && p.Y + 1 < 9 && getValue(p.X + (i + 1), p.Y + 1) == 0 && (p.Y - p.Y == i || p.Y - p.X == i * 3))
                        {
                            if (isContainMonksChess(p.X + (i + 1), p.Y + 1, p.X + i, p.Y) && Move(p.X + i, p.Y, p.X + (i + 1), p.Y + 1)) return;
                        }
                        if (p.X + (i - 1) < 5 && p.Y - 1 > -1 && getValue(p.X + (i - 1), p.Y - 1) == 0 && (p.Y - p.Y == i || p.Y - p.X == i * 3))
                        {
                            if (isContainMonksChess(p.X + (i - 1), p.Y - 1, p.X + i, p.Y) && Move(p.X + i, p.Y, p.X + (i - 1), p.Y - 1)) return;
                        }
                        if (p.X + (i - 1) < 5 && p.Y + 1 < 9 && getValue(p.X + (i - 1), p.Y + 1) == 0 && (p.X + p.Y == i || p.X + p.Y == i * 3))
                        {
                            if (isContainMonksChess(p.X + (i - 1), p.Y + 1, p.X + i, p.Y) && Move(p.X + i, p.Y, p.X + (i - 1), p.Y + 1)) return;
                        }
                    }
                    #endregion

                    #region 横向
                    //追尾
                    if (p.Y - i > -1 && getValue(p.X, p.Y - i) == 1)
                    {
                        //横向
                        if (p.Y - (i + 1) > -1 && getValue(p.X, p.Y - (i + 1)) == 0)
                        {
                            if (isContainMonksChess(p.X, p.Y - (i + 1), p.X, p.Y - i) && Move(p.X, p.Y - i, p.X, p.Y - (i + 1))) return;
                        }
                        //纵向
                        if (p.X - 1 > -1 && getValue(p.X - 1, p.Y - i) == 0)
                        {
                            if (isContainMonksChess(p.X - 1, p.Y - i, p.X, p.Y - i) && Move(p.X, p.Y - i, p.X - 1, p.Y - i)) return;
                        }
                        if (p.X + 1 < 5 && getValue(p.X + 1, p.Y - i) == 0)
                        {
                            if (isContainMonksChess(p.X, p.Y - i, p.X + 1, p.Y - i) && Move(p.X + 1, p.Y - i, p.X, p.Y - i)) return;
                        }
                        //斜线
                        if (p.Y - (i + 1) > -1 && p.X - 1 > -1 && getValue(p.X - 1, p.Y - (i + 1)) == 0 && (p.Y - p.X == i || p.Y - p.X == i * 3))
                        {
                            if (isContainMonksChess(p.X - 1, p.Y - (i + 1), p.X, p.Y - i) && Move(p.X, p.Y - i, p.X - 1, p.Y - (i + 1))) return;
                        }
                        if (p.Y - (i + 1) > -1 && p.X + 1 < 5 && getValue(p.X + 1, p.Y - (i + 1)) == 0 && (p.X + p.Y == i * 3 || p.X + p.Y == i * 5))
                        {
                            if (isContainMonksChess(p.X + 1, p.Y - (i + 1), p.X, p.Y - i) && Move(p.X, p.Y - i, p.X + 1, p.Y - (i + 1))) return;
                        }
                        if (p.Y - (i - 1) > -1 && p.X - 1 > -1 && getValue(p.X - 1, p.Y - (i - 1)) == 0 && (p.X + p.Y == i * 3 || p.X + p.Y == i * 5))
                        {
                            if (isContainMonksChess(p.X - 1, p.Y - (i - 1), p.X, p.Y - i) && Move(p.X, p.Y - i, p.X - 1, p.Y - (i - 1))) return;
                        }
                        if (p.Y - (i - 1) > -1 && p.X + 1 < 5 && getValue(p.X + 1, p.Y - (i - 1)) == 0 && (p.Y - p.X == i || p.Y - p.X == i * 3))
                        {
                            if (isContainMonksChess(p.X + 1, p.Y - (i - 1), p.X, p.Y - i) && Move(p.X, p.Y - i, p.X + 1, p.Y - (i - 1))) return;
                        }

                    }
                    if (p.Y + i < 9 && getValue(p.X, p.Y + i) == 1)
                    {
                        //横向
                        if (p.Y + (i + 1) < 9 && getValue(p.X, p.Y + (i + 1)) == 0)
                        {
                            if (isContainMonksChess(p.X, p.Y + (i + 1), p.X, p.Y + i) && Move(p.X, p.Y + i, p.X, p.Y + (i + 1))) return;
                        }
                        //纵向
                        if (p.X - 1 > -1 && getValue(p.X - 1, p.Y + i) == 0)
                        {
                            if (isContainMonksChess(p.X - 1, p.Y + i, p.X, p.Y + i) && Move(p.X, p.Y + i, p.X - 1, p.Y + i)) return;
                        }
                        if (p.X + 1 < 5 && getValue(p.X + 1, p.Y + i) == 0)
                        {
                            if (isContainMonksChess(p.X + 1, p.Y + i, p.X, p.Y + i) && Move(p.X, p.Y + i, p.X + 1, p.Y + i)) return;
                        }
                        //斜线
                        if (p.Y + (i + 1) < 9 && p.X - 1 > -1 && getValue(p.X + 3, p.Y - (i - 1)) == 0 && (p.X + p.Y == i || p.X + p.Y == i * 3))
                        {
                            if (isContainMonksChess(p.X - 1, p.Y + (i + 1), p.X, p.Y + i) && Move(p.X, p.Y + i, p.X - 1, p.Y + (i + 1))) return;
                        }
                        if (p.Y + (i + 1) < 9 && p.X + 1 < 5 && getValue(p.X + 1, p.Y + (i + 1)) == 0 && (p.Y - p.X == -i || p.Y - p.X == i))
                        {
                            if (isContainMonksChess(p.X + 1, p.Y + (i + 1), p.X, p.Y + i) && Move(p.X, p.Y + i, p.X + 1, p.Y + (i + 1))) return;
                        }
                        if (p.Y + (i - 1) < 9 && p.X - 1 > -1 && getValue(p.X - 1, p.Y + (i + 1)) == 0 && (p.Y - p.X == -i || p.Y - p.X == i))
                        {
                            if (isContainMonksChess(p.X - 1, p.Y + (i - 1), p.X, p.Y + i) && Move(p.X, p.Y + i, p.X - 1, p.Y + (i - 1))) return;
                        }
                        if (p.Y + (i - 1) < 9 && p.X + 1 < 5 && getValue(p.X + 1, p.Y + (i - 1)) == 0 && (p.X + p.Y == i || p.X + p.Y == i * 3))
                        {
                            if (isContainMonksChess(p.X + 1, p.Y + (i - 1), p.X, p.Y + i) && Move(p.X, p.Y + i, p.X + 1, p.Y + (i - 1))) return;
                        }
                    }
                    #endregion

                    if (i == 2)
                    {
                        #region 斜线方向
                        //追尾
                        if (p.X - 2 > -1 && p.Y - 2 > -1 && getValue(p.X - 2, p.Y - 2) == 1 && (p.Y - p.X == 0 || p.Y - p.X == 4))
                        {
                            //竖直
                            if (p.X - 3 > -1 && getValue(p.X - 3, p.Y - 2) == 0)
                            {
                                if (isContainMonksChess(p.X - 3, p.Y - 2, p.X - 2, p.Y - 2) && Move(p.X - 2, p.Y - 2, p.X - 3, p.Y - 2)) return;
                            }
                            if (p.X - 1 > -1 && getValue(p.X - 1, p.Y - 2) == 0)
                            {
                                if (isContainMonksChess(p.X - 1, p.Y - 2, p.X - 2, p.Y - 2) && Move(p.X - 2, p.Y - 2, p.X - 1, p.Y - 2)) return;
                            }
                            //横向
                            if (p.Y - 3 > -1 && getValue(p.X - 2, p.Y - 3) == 0)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y - 3, p.X - 2, p.Y - 2) && Move(p.X - 2, p.Y - 2, p.X - 2, p.Y - 3)) return;
                            }
                            if (p.Y - 1 > -1 && getValue(p.X - 2, p.Y - 1) == 0)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y - 1, p.X - 2, p.Y - 2) && Move(p.X - 2, p.Y - 2, p.X - 2, p.Y - 1)) return;
                            }
                            //斜线
                            if (p.X - 3 > -1 && p.Y - 3 > -1 && getValue(p.X - 3, p.Y - 3) == 0)
                            {
                                if (isContainMonksChess(p.X - 3, p.Y - 3, p.X - 2, p.Y - 2) && Move(p.X - 2, p.Y - 2, p.X - 3, p.Y - 3)) return;
                            }
                            if (p.X - 3 > -1 && p.Y - 1 > -1 && getValue(p.X - 3, p.Y - 1) == 0)
                            {
                                if (isContainMonksChess(p.X - 3, p.Y - 1, p.X - 2, p.Y - 2) && Move(p.X - 2, p.Y - 2, p.X - 3, p.Y - 1)) return;
                            }
                            if (p.X - 1 > -1 && p.Y - 3 > -1 && getValue(p.X - 1, p.Y - 3) == 0)
                            {
                                if (isContainMonksChess(p.X - 1, p.Y - 3, p.X - 2, p.Y - 2) && Move(p.X - 2, p.Y - 2, p.X - 1, p.Y - 3)) return;
                            }
                        }
                        if (p.X - 2 > -1 && p.Y + 2 < 9 && getValue(p.X - 2, p.Y + 2) == 1 && (p.X + p.Y == 4 || p.X + p.Y == 8))
                        {
                            //竖直
                            if (p.X - 3 > -1 && getValue(p.X - 3, p.Y + 2) == 0)
                            {
                                if (isContainMonksChess(p.X - 3, p.Y + 2, p.X - 2, p.Y + 2) && Move(p.X - 2, p.Y + 2, p.X - 3, p.Y + 2)) return;
                            }
                            if (p.X - 1 > -1 && getValue(p.X - 1, p.Y + 2) == 0)
                            {
                                if (isContainMonksChess(p.X - 1, p.Y + 2, p.X - 2, p.Y + 2) && Move(p.X - 2, p.Y + 2, p.X - 1, p.Y + 2)) return;
                            }
                            //横向
                            if (p.Y + 3 < 9 && getValue(p.X - 2, p.Y + 3) == 0)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y + 3, p.X - 2, p.Y + 2) && Move(p.X - 2, p.Y + 2, p.X - 2, p.Y + 3)) return;
                            }
                            if (p.Y + 1 < 9 && getValue(p.X - 2, p.Y + 1) == 0)
                            {
                                if (isContainMonksChess(p.X - 2, p.Y + 1, p.X - 2, p.Y + 2) && Move(p.X - 2, p.Y + 2, p.X - 2, p.Y + 1)) return;
                            }
                            //斜线
                            if (p.X - 3 > -1 && p.Y + 3 < 9 && getValue(p.X - 3, p.Y + 3) == 0)
                            {
                                if (isContainMonksChess(p.X - 3, p.Y + 3, p.X - 2, p.Y + 2) && Move(p.X - 2, p.Y + 2, p.X - 3, p.Y + 3)) return;
                            }
                            if (p.X - 3 > -1 && p.Y + 1 < 9 && getValue(p.X - 3, p.Y + 1) == 0)
                            {
                                if (isContainMonksChess(p.X - 3, p.Y + 1, p.X - 2, p.Y + 2) && Move(p.X - 2, p.Y + 2, p.X - 3, p.Y + 1)) return;
                            }
                            if (p.X - 1 > -1 && p.Y + 3 < 9 && getValue(p.X - 1, p.Y + 3) == 0)
                            {
                                if (isContainMonksChess(p.X - 1, p.Y + 3, p.X - 2, p.Y + 2) && Move(p.X - 2, p.Y + 2, p.X - 1, p.Y + 3)) return;
                            }
                        }
                        if (p.X + 2 < 5 && p.Y - 2 > -1 && getValue(p.X + 2, p.Y - 2) == 1 && (p.X + p.Y == 4 || p.X + p.Y == 8))
                        {
                            //竖直
                            if (p.X + 1 < 5 && getValue(p.X + 1, p.Y - 2) == 0)
                            {
                                if (isContainMonksChess(p.X + 1, p.Y - 2, p.X + 2, p.Y - 2) && Move(p.X + 2, p.Y - 2, p.X + 1, p.Y - 2)) return;
                            }
                            if (p.X + 3 < 5 && getValue(p.X + 3, p.Y - 2) == 0)
                            {
                                if (isContainMonksChess(p.X + 3, p.Y - 2, p.X + 2, p.Y - 2) && Move(p.X + 2, p.Y - 2, p.X + 3, p.Y - 2)) return;
                            }
                            //横向
                            if (p.Y - 3 < -1 && getValue(p.X + 2, p.Y - 3) == 0)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y - 3, p.X + 2, p.Y - 2) && Move(p.X + 2, p.Y - 2, p.X + 2, p.Y - 3)) return;
                            }
                            if (p.Y - 1 < -1 && getValue(p.X + 2, p.Y - 1) == 0)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y - 1, p.X + 2, p.Y - 2) && Move(p.X + 2, p.Y - 2, p.X + 2, p.Y - 1)) return;
                            }
                            //斜线
                            if (p.X + 1 < 5 && p.Y - 3 > -1 && getValue(p.X + 1, p.Y - 3) == 0)
                            {
                                if (isContainMonksChess(p.X + 1, p.Y - 3, p.X + 2, p.Y - 2) && Move(p.X + 2, p.Y - 2, p.X + 1, p.Y - 3)) return;
                            }
                            if (p.X + 3 < 5 && p.Y - 3 > -1 && getValue(p.X + 3, p.Y - 3) == 0)
                            {
                                if (isContainMonksChess(p.X + 3, p.Y - 3, p.X + 2, p.Y - 2) && Move(p.X + 2, p.Y - 2, p.X + 3, p.Y - 3)) return;
                            }
                            if (p.X + 3 < 5 && p.Y - 1 > -1 && getValue(p.X + 3, p.Y - 1) == 0)
                            {
                                if (isContainMonksChess(p.X + 3, p.Y - 1, p.X + 2, p.Y - 2) && Move(p.X + 2, p.Y - 2, p.X + 3, p.Y - 1)) return;
                            }
                        }
                        if (p.X + 2 < 5 && p.Y + 2 < 9 && getValue(p.X + 2, p.Y + 2) == 1 && (p.Y - p.X == 0 || p.Y - p.X == 4))
                        {
                            //竖直
                            if (p.X + 1 < 5 && getValue(p.X + 1, p.Y + 2) == 0)
                            {
                                if (isContainMonksChess(p.X + 1, p.Y + 2, p.X + 2, p.Y + 2) && Move(p.X + 2, p.Y + 2, p.X + 1, p.Y + 2)) return;
                            }
                            if (p.X + 3 < 5 && getValue(p.X + 3, p.Y + 2) == 0)
                            {
                                if (isContainMonksChess(p.X + 3, p.Y + 2, p.X + 2, p.Y + 2) && Move(p.X + 2, p.Y + 2, p.X + 3, p.Y + 2)) return;
                            }
                            //横向
                            if (p.Y + 1 < 9 && getValue(p.X + 2, p.Y + 1) == 0)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y + 1, p.X + 2, p.Y + 2) && Move(p.X + 2, p.Y + 2, p.X + 2, p.Y + 1)) return;
                            }
                            if (p.Y + 3 < 9 && getValue(p.X + 2, p.Y + 3) == 0)
                            {
                                if (isContainMonksChess(p.X + 2, p.Y + 3, p.X + 2, p.Y + 2) && Move(p.X + 2, p.Y + 2, p.X + 2, p.Y + 3)) return;
                            }
                            //斜线
                            if (p.X + 3 < 5 && p.Y + 3 < 9 && getValue(p.X + 3, p.Y + 3) == 0)
                            {
                                if (isContainMonksChess(p.X + 3, p.Y + 3, p.X + 2, p.Y + 2) && Move(p.X + 2, p.Y + 2, p.X + 3, p.Y + 3)) return;
                            }
                            if (p.X + 1 < 5 && p.Y + 3 < 9 && getValue(p.X + 1, p.Y + 3) == 0)
                            {
                                if (isContainMonksChess(p.X + 1, p.Y + 3, p.X + 2, p.Y + 2) && Move(p.X + 2, p.Y + 2, p.X + 1, p.Y + 3)) return;
                            }
                            if (p.X + 3 < 5 && p.Y + 1 < 9 && getValue(p.X + 3, p.Y + 1) == 0)
                            {
                                if (isContainMonksChess(p.X + 3, p.Y + 1, p.X + 2, p.Y + 2) && Move(p.X + 2, p.Y + 2, p.X + 3, p.Y + 1)) return;
                            }
                        }
                        #endregion
                    }
                }


                if (PioFlags == 2)
                    PioFlags = 1;
                else
                    PioFlags = 2;
                RondaFlags++;
            }
            #endregion
        }
        #endregion

        #region 辅助部分

        /// <summary>
        /// 检查所置棋子是否存在危险
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="rawX"></param>
        /// <param name="rawY"></param>
        /// <returns></returns>
        private bool isContainMonksChess(int x, int y, int rawX, int rawY)
        {
            if (x - 1 > -1 && y - 1 > -1 && x + 1 < 5 && y + 1 < 9 &&
                 getValue(x - 1, y - 1) == 2 &&
                 ((rawX == x + 1 && rawY == y + 1) || getValue(x + 1, y + 1) == 0) && (y - x == 4 || y - x == 0))
            {
                return false;
            }
            if (x - 1 > -1 && y + 1 < 9 && x + 1 < 5 && y - 1 > -1 &&
                  getValue(x - 1, y + 1) == 2 &&
                  ((rawX == x + 1 && rawY == y - 1) || getValue(x + 1, y - 1) == 0) && (x + y == 4 || x + y == 8))
            {
                return false;
            }
            if (x + 1 < 5 && y - 1 > -1 && x - 1 > -1 && y + 1 < 9 &&
                 getValue(x + 1, y - 1) == 2 &&
                ((rawX == x - 1 && rawY == y + 1) || getValue(x - 1, y + 1) == 0) && (y + x == 4 || y + x == 8))
            {
                return false;
            }
            if (x + 1 < 5 && y + 1 < 9 && x - 1 > -1 && y - 1 > -1 &&
                  getValue(x + 1, y + 1) == 2 &&
                  ((rawX == x - 1 && rawY == y - 1) || getValue(x - 1, y - 1) == 0) && (y - x == 4 || y - x == 0))
            {
                return false;
            }
            if (x - 1 > -1 && x + 1 < 5 && getValue(x - 1, y) == 2 && ((rawX == x + 1 && rawY == y) || getValue(x + 1, y) == 0))
            {
                return false;
            }
            if (x + 1 < 5 && x - 1 > -1 && getValue(x + 1, y) == 2 && ((rawX == x - 1 && rawY == y) || getValue(x - 1, y) == 0))
            {
                return false;
            }
            if (y - 1 > -1 && y + 1 < 9 && getValue(x, y - 1) == 2 && ((rawX == x && rawY == y + 1) || getValue(x, y + 1) == 0))
            {
                return false;
            }
            if (y + 1 < 9 && y - 1 > -1 && getValue(x, y + 1) == 2 && ((rawX == x && rawY == y - 1) || getValue(x, y - 1) == 0))
            {
                return false;
            }
            return true;
        }

        private bool isEnableMove(int rawX, int rawY)
        {

            if (rawX - 2 > -1 && getValue(rawX - 2, rawY) == 2 && getValue(rawX - 1, rawY) == 1)
            {
                return false;
            }
            if (rawX + 2 < 5 && getValue(rawX + 2, rawY) == 2 && getValue(rawX + 1, rawY) == 1)
            { }
            if (rawY - 2 > -1 && getValue(rawX, rawY - 2) == 2 && getValue(rawX, rawY + 1) == 1)
            { }
            //TODO:构思不清晰，暂时不做
            return true;
        }

        /// <summary>
        /// 随即选定需要围捕的和尚
        /// </summary>
        Random randomMonksChess = new Random(DateTime.Now.Millisecond);
        private int getRandomMonksChess()
        {
            return randomMonksChess.Next(1, 3);
        }

        /// <summary>
        /// 获取和尚棋子的位置
        /// </summary>
        /// <returns></returns>
        TwzyPoint getMonksChessLocation()
        {
            int i = 0;
            for (; i < 5; i++)
            {
                int j = 0;
                for (; j < 9; j++)
                {
                    if (TwzyLocation.ChartArry[i, j] == 2)
                        return new TwzyPoint
                        {
                            X = i,
                            Y = j
                        };
                }
            }

            return new TwzyPoint
            {
                X = -1,
                Y = -1
            };
        }
        TwzyPoint getMonksChessLocation(int i, int j)
        {
            for (; i < 5; i++)
            {
                for (; j < 9; j++)
                {
                    if (TwzyLocation.ChartArry[i, j] == 2)
                        return new TwzyPoint
                        {
                            X = i,
                            Y = j
                        };
                }
                j = 0;
            }

            return new TwzyPoint
            {
                X = -1,
                Y = -1
            };
        }

        #endregion

        #endregion



        //Test
        string filePath = "config.txt";

        string initData = "330000030" +
                          "300111003" +
                          "002101200" +
                          "300111003" +
                          "330000030";
        public string ReadFile()
        {
            if (!System.IO.File.Exists(filePath))
            {
                System.IO.File.WriteAllText(filePath, initData);
            }
            return System.IO.File.ReadAllText(filePath);
        }
    }
}
