using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNullable
{
    /// <summary>
    /// 
    /// </summary>
    public class Market
    {
        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, Market> MarketList { get; set; } = new Dictionary<string, Market>();

        static Market()
        {
            //TW
            MarketList.Add(TW.Code, TW);
            MarketList.Add(TWSE.Code, TWSE);
            MarketList.Add(TWSEODD.Code, TWSEODD);
            MarketList.Add(TIB.Code, TIB);
            MarketList.Add(TPEx.Code, TPEx);
            MarketList.Add(TPExODD.Code, TPExODD);
            MarketList.Add(ESM.Code, ESM);
            MarketList.Add(PSB.Code, PSB);
            MarketList.Add(GISA.Code, GISA);
            MarketList.Add(TXF.Code, TXF);
            MarketList.Add(TXO.Code, TXO);
            //HK
            MarketList.Add(HKEX.Code, HKEX);
            MarketList.Add(GEM.Code, GEM);
            //CN
            MarketList.Add(SSE.Code, SSE);
            MarketList.Add(STAR.Code, STAR);
            MarketList.Add(SZSE.Code, SZSE);
            MarketList.Add(ChiNext.Code, ChiNext);
            //JP
            MarketList.Add(JP.Code, JP);
            //US
            MarketList.Add(NYSE.Code, NYSE);
            MarketList.Add(NASDAQ.Code, NASDAQ);
            //KR
            MarketList.Add(KR.Code, KR);
            //DE
            MarketList.Add(DE.Code, DE);
            //Others
            MarketList.Add(FOREX.Code, FOREX);
            MarketList.Add(FF.Code, FF);
            MarketList.Add(Others.Code, Others);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        private Market(Exchange exchange, string code, string name)
        {
            Exchange = exchange;
            Code = code;
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        public Country Country => Exchange.Country;
        /// <summary>
        /// 
        /// </summary>
        public Exchange Exchange { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 基本市場(by country)
        /// </summary>
        public bool IsBase { get; set; } = false;

        #region TW

        /// <summary>
        /// 台灣所屬市場
        /// 當無法明確知道商品所屬市場時用此項目
        /// </summary>
        public static Market TW { get; private set; } = new Market(Exchange.TW, "TW", "台灣") { IsBase = true };
        /// <summary>
        /// 上市
        /// </summary>
        public static Market TWSE { get; private set; } = new Market(Exchange.TWSE, "TWSE", "上市");
        /// <summary>
        /// 上市零股
        /// </summary>
        [Obsolete("不確定是否應該有此設定")]
        public static Market TWSEODD { get; private set; } = new Market(Exchange.TWSE, "TWSEODD", "上市零股");
        /// <summary>
        /// 創新板
        /// </summary>
        public static Market TIB { get; private set; } = new Market(Exchange.TWSE, "TIB", "創新板");
        /// <summary>
        /// 上櫃
        /// </summary>
        public static Market TPEx { get; private set; } = new Market(Exchange.TPEx, "TPEx", "上櫃");
        /// <summary>
        /// 上櫃零股
        /// </summary>
        [Obsolete("不確定是否應該有此設定")]
        public static Market TPExODD { get; private set; } = new Market(Exchange.TPEx, "TPExODD", "上櫃零股");
        /// <summary>
        /// 興櫃
        /// </summary>
        public static Market ESM { get; private set; } = new Market(Exchange.TPEx, "ESM", "興櫃");
        /// <summary>
        /// 戰略新板
        /// </summary>
        public static Market PSB { get; private set; } = new Market(Exchange.TPEx, "PSB", "戰略新板");
        /// <summary>
        /// 創櫃
        /// </summary>
        public static Market GISA { get; private set; } = new Market(Exchange.TPEx, "GISA", "創櫃");
        /// <summary>
        /// 期貨
        /// </summary>
        public static Market TXF { get; private set; } = new Market(Exchange.TAIFEX, "TXF", "期貨");
        /// <summary>
        /// 選擇權
        /// </summary>
        [Obsolete("不確定是否應該有此設定")]
        public static Market TXO { get; private set; } = new Market(Exchange.TAIFEX, "TXO", "選擇權");

        #endregion

        #region US

        /// <summary>
        /// 美國所屬市場
        /// 當無法明確知道商品所屬市場時用此項目
        /// </summary>
        public static Market US { get; private set; } = new Market(Exchange.US, "US", "US") { IsBase = true };
        /// <summary>
        /// 
        /// </summary>
        public static Market NYSE { get; private set; } = new Market(Exchange.NYSE, "NYSE", "NYSE");
        /// <summary>
        /// 
        /// </summary>
        public static Market NASDAQ { get; private set; } = new Market(Exchange.NASDAQ, "NASDAQ", "NASDAQ");

        #endregion

        #region HK

        /// <summary>
        /// 香港主板
        /// </summary>
        public static Market HKEX { get; private set; } = new Market(Exchange.HKEX, "HKEX", "HK");

        /// <summary>
        /// 香港創業板
        /// （Growth Enterprise Market，簡稱GEM）
        /// </summary>
        public static Market GEM { get; private set; } = new Market(Exchange.HKEX, "GEM", "香港創業板");

        #endregion

        #region JP

        /// <summary>
        /// 主板
        /// </summary>
        public static Market JP { get; private set; } = new Market(Exchange.TSE, "JP", "JP");

        #endregion

        #region KR

        /// <summary>
        /// 韓國主板
        /// </summary>
        public static Market KR { get; private set; } = new Market(Exchange.KRX, "KR", "KR");

        #endregion

        #region SG

        /// <summary>
        /// 新加坡主板
        /// </summary>
        public static Market SG { get; private set; } = new Market(Exchange.SGX, "SG", "SG");

        #endregion

        #region DE

        /// <summary>
        /// 主板
        /// </summary>
        public static Market DE { get; private set; } = new Market(Exchange.FWB, "DE", "DE");

        #endregion

        #region CN

        /// <summary>
        /// 上海主板
        /// </summary>
        public static Market SSE { get; private set; } = new Market(Exchange.SSE, "SSE", "SSE");

        /// <summary>
        /// 科創板
        /// </summary>
        public static Market STAR { get; private set; } = new Market(Exchange.SSE, "STAR", "STAR");

        /// <summary>
        /// 深圳主板
        /// </summary>
        public static Market SZSE { get; private set; } = new Market(Exchange.SZSE, "SZSE", "SZSE");

        /// <summary>
        /// 創業板
        /// </summary>
        public static Market ChiNext { get; private set; } = new Market(Exchange.SZSE, "ChiNext", "ChiNext");

        #endregion

        #region Others

        /// <summary>
        /// TODO : 待整理
        /// 國際匯率 
        /// </summary>
        public static Market FOREX { get; private set; } = new Market(Exchange.Others, "FOREX", "國際匯率");

        /// <summary>
        /// 國外期貨
        /// </summary>
        public static Market FF { get; private set; } = new Market(Exchange.FF, "FF", "FF");

        /// <summary>
        /// TODO : 待整理
        /// 未歸類其他
        /// </summary>
        public static Market Others { get; private set; } = new Market(Exchange.Others, "Others", "其他");

        #endregion
    }
}
