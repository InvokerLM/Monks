using System;

namespace YoungGame
{
    public delegate void TwzyResultHandler(object sender, ResultEventArgs e);

    public class ResultEventArgs : EventArgs
    {
        /// <summary>
        /// 游戏结果 1 和尚胜利 2 小兵胜利
        /// </summary>
        public int ResultCode { get; set; }
        public string ResultMessage { get; set; }

        public static ResultEventArgs CreatResult(int code, string msg)
        {
            return new ResultEventArgs
            {
                ResultCode = code,
                ResultMessage = msg
            };
        }
    }


    public delegate void TwzyChessTurnHandler(object sender, ChessTurnEventArgs e);

    public class ChessTurnEventArgs : EventArgs
    {
        /// <summary>
        /// 轮循  1 小兵 2 和尚
        /// </summary>
        public int ChessTurnCode { get; set; }
        public string ChessTurnMessage { get; set; }

        public static ChessTurnEventArgs CreatChessTurn(int code, string msg)
        {
            return new ChessTurnEventArgs
            {
                ChessTurnCode = code,
                ChessTurnMessage = msg
            };
        }
    }


    public delegate void TwzyChessCountHandler(object sender, ChessCountEventArgs e);
    public class ChessCountEventArgs : EventArgs
    {
        /// <summary>
        /// 轮循  1 小兵 2 和尚
        /// </summary>
        public int ChessCount { get; set; }
        public int KilleChessdCount { get; set; }

        public static ChessCountEventArgs CreatChessTurn(int chessCount, int KillCount)
        {
            return new ChessCountEventArgs
            {
                ChessCount = chessCount,
                KilleChessdCount = KillCount
            };
        }
    }

}
