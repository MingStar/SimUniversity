namespace MingStar.SimUniversity.Board
{
    public class TradingSite
    {
        public const int TradeOutStudentQuantity = 3;

        private static TradingSite _instance;

        protected TradingSite()
        {
        }

        public static TradingSite Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TradingSite();
                }
                return _instance;
            }
        }

        public override string ToString()
        {
            return "Trading Site";
        }
    }
}