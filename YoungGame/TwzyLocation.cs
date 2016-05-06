
namespace YoungGame
{
     class TwzyLocation
    {
        /// <summary>
        /// 0 为空 1 为兵 2 为和尚 3为不可用
        /// </summary>
        public static int[,] ChartArry;
        public static int DogfaceChessCount = 24;//剩余的小兵
        public static string InitString = "330000030" +
                                          "300111003" +
                                          "002101200" +
                                          "300111003" +
                                          "330000030";
        static TwzyLocation()
        {
            ChartArry = new int[5, 9];
        }

        public static  string ToString(int SType)
        {
            string s = "";
            for(int i=0;i<5;i++)
            {
                for(int j=0;j<9;j++)
                {
                    if (SType == 1)
                    {
                        s += " (" + i.ToString() + "," + j.ToString() + ")-" + ChartArry[i, j].ToString();
                    }
                    else if (SType == 2)
                    {
                        s += ChartArry[i, j].ToString();
                    }
                }
                s += "\n";
            }
            return s;
        }
       
    }
}
