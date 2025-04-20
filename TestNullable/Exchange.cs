using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestNullable
{
    /// <summary>
    /// 交易所
    /// </summary>
    public class Exchange
    {
        /// <summary>
        /// 
        /// </summary>
        public static Dictionary<string, Exchange> ExchangeList { get; set; } = new Dictionary<string, Exchange>();

        static Exchange()
        {
            ExchangeList.Add(TW.Code, TW); //備用項目
            ExchangeList.Add(TWSE.Code, TWSE);
            ExchangeList.Add(TPEx.Code, TPEx);
            ExchangeList.Add(TAIFEX.Code, TAIFEX);
            ExchangeList.Add(HKEX.Code, HKEX);
            ExchangeList.Add(HKFE.Code, HKFE);
            ExchangeList.Add(SSE.Code, SSE);
            ExchangeList.Add(SZSE.Code, SZSE);
            ExchangeList.Add(CFFEX.Code, CFFEX);
            ExchangeList.Add(TSE.Code, TSE);
            ExchangeList.Add(TOCOM.Code, TOCOM);
            ExchangeList.Add(US.Code, US); //備用項目
            ExchangeList.Add(NYSE.Code, NYSE);
            ExchangeList.Add(NASDAQ.Code, NASDAQ);
            ExchangeList.Add(AMEX.Code, AMEX);
            ExchangeList.Add(CME.Code, CME);
            ExchangeList.Add(CBOT.Code, CBOT);
            ExchangeList.Add(CBOE.Code, CBOE);
            ExchangeList.Add(COMEX.Code, COMEX);
            ExchangeList.Add(NYMEX.Code, NYMEX);
            ExchangeList.Add(NYBOT.Code, NYBOT);
            ExchangeList.Add(KRX.Code, KRX);
            ExchangeList.Add(SGX.Code, SGX);
            ExchangeList.Add(ICE_SG.Code, ICE_SG);
            ExchangeList.Add(SMX.Code, SMX);
            ExchangeList.Add(LSE.Code, LSE);
            ExchangeList.Add(LIFFE.Code, LIFFE);
            ExchangeList.Add(LME.Code, LME);
            ExchangeList.Add(IPE.Code, IPE);
            ExchangeList.Add(FWB.Code, FWB);
            ExchangeList.Add(FOREX.Code, FOREX);
            ExchangeList.Add(FF.Code, FF);
            ExchangeList.Add(Others.Code, Others);
        }

        private Exchange(Country country, string code, string name)
        {
            Country = country;
            Code = code;
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        public Country Country { get; private set; }
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

        #region 交易所列表

        #region TW

        /// <summary>
        /// 台灣所屬交易所
        /// 當無法明確知道商品所屬交易所時用此項目
        /// </summary>
        public static Exchange TW { get; private set; } = new Exchange(Country.TW, "TW", "台灣") { IsBase = true };
        /// <summary>
        /// 台灣證券交易所
        /// </summary>
        public static Exchange TWSE { get; private set; } = new Exchange(Country.TW, "TWSE", "台灣證券交易所");
        /// <summary>
        /// 櫃買中心
        /// </summary>
        public static Exchange TPEx { get; private set; } = new Exchange(Country.TW, "TPEx", "櫃買中心");
        /// <summary>
        /// 臺灣期貨交易所
        /// </summary>
        public static Exchange TAIFEX { get; private set; } = new Exchange(Country.TW, "TAIFEX", "臺灣期貨交易所");

        #endregion

        #region HK

        /// <summary>
        /// 香港交易所
        /// </summary>
        public static Exchange HKEX { get; private set; } = new Exchange(Country.HK, "HKEX", "香港交易所");

        /// <summary>
        /// 香港期貨交易所
        /// </summary>
        public static Exchange HKFE { get; private set; } = new Exchange(Country.HK, "HKFE", "香港期貨交易所");

        #endregion

        #region CN

        /// <summary>
        /// 上海證券交易所
        /// </summary>
        public static Exchange SSE { get; private set; } = new Exchange(Country.CN, "SSE", "上海證券交易所");
        /// <summary>
        /// 深圳證券交易所
        /// </summary>
        public static Exchange SZSE { get; private set; } = new Exchange(Country.CN, "SZSE", "深圳證券交易所");
        /// <summary>
        /// 中國金融期貨交易所
        /// </summary>
        public static Exchange CFFEX { get; private set; } = new Exchange(Country.CN, "CFFEX", "中國金融期貨交易所");

        #endregion

        #region JP

        /// <summary>
        /// 東京證券交易所
        /// </summary>
        public static Exchange TSE { get; private set; } = new Exchange(Country.JP, "TSE", "東京證券交易所");

        /// <summary>
        /// 東京證券交易所
        /// </summary>
        public static Exchange TOCOM { get; private set; } = new Exchange(Country.JP, "TOCOM", "東京工業品交易所");

        #endregion

        #region US

        /// <summary>
        /// 美國所屬交易所
        /// 當無法明確知道商品所屬交易所時用此項目
        /// </summary>
        public static Exchange US { get; private set; } = new Exchange(Country.US, "US", "美國") { IsBase = true };
        /// <summary>
        /// 紐約證券交易所
        /// </summary>
        public static Exchange NYSE { get; private set; } = new Exchange(Country.US, "NYSE", "紐約證券交易所");
        /// <summary>
        /// 
        /// </summary>
        public static Exchange NASDAQ { get; private set; } = new Exchange(Country.US, "NASDAQ", "那斯達克股票交易所");
        /// <summary>
        /// 美國證券交易所
        /// </summary>
        public static Exchange AMEX { get; private set; } = new Exchange(Country.US, "AMEX", "美國證券交易所");
        /// <summary>
        /// 芝加哥商品交易所 
        /// </summary>
        public static Exchange CME { get; private set; } = new Exchange(Country.US, "CME", "芝加哥商品交易所");
        /// <summary>
        /// 芝加哥期貨交易所 
        /// </summary>
        public static Exchange CBOT { get; private set; } = new Exchange(Country.US, "CBOT", "芝加哥期貨交易所");
        /// <summary>
        /// 紐約商業交易所 
        /// </summary>
        public static Exchange CBOE { get; private set; } = new Exchange(Country.US, "CBOE", "芝加哥選擇權交易所");
        /// <summary>
        /// 紐約商業交易所 
        /// </summary>
        public static Exchange COMEX { get; private set; } = new Exchange(Country.US, "COMEX", "紐約商品交易所");
        /// <summary>
        /// 紐約商業交易所 
        /// </summary>
        public static Exchange NYMEX { get; private set; } = new Exchange(Country.US, "NYMEX", "紐約商業交易所");
        /// <summary>
        /// 紐約商業交易所 
        /// </summary>
        public static Exchange NYBOT { get; private set; } = new Exchange(Country.US, "NYBOT", "紐約期貨交易所");

        #endregion

        #region KR

        /// <summary>
        /// 韓國交易所 
        /// </summary>
        public static Exchange KRX { get; private set; } = new Exchange(Country.KR, "KRX", "韓國交易所");

        #endregion

        #region SG

        /// <summary>
        /// 新加坡交易所 
        /// </summary>
        public static Exchange SGX { get; private set; } = new Exchange(Country.SG, "SGX", "新加坡交易所");
        /// <summary>
        /// ICE新加坡期貨交易所 
        /// </summary>
        public static Exchange ICE_SG { get; private set; } = new Exchange(Country.SG, "ICE_SG", "ICE新加坡期貨交易所");
        /// <summary>
        /// 新加坡交易所 
        /// </summary>
        public static Exchange SMX { get; private set; } = new Exchange(Country.SG, "SMX", "新加坡商品交易所");

        #endregion

        #region EN
        /// <summary>
        /// 倫敦證券交易所 
        /// </summary>
        public static Exchange LSE { get; private set; } = new Exchange(Country.EN, "LSE", "倫敦證券交易所");
        /// <summary>
        /// 倫敦國際金融期貨交易所 
        /// </summary>
        public static Exchange LIFFE { get; private set; } = new Exchange(Country.EN, "LIFFE", "倫敦國際金融期貨交易所");
        /// <summary>
        /// 倫敦金屬交易所 
        /// </summary>
        public static Exchange LME { get; private set; } = new Exchange(Country.EN, "LME", "倫敦金屬交易所");
        /// <summary>
        /// 倫敦國際石油交易所 
        /// </summary>
        public static Exchange IPE { get; private set; } = new Exchange(Country.EN, "IPE", "倫敦國際石油交易所");

        #endregion

        #region DE

        /// <summary>
        /// 法蘭克福證券交易所
        /// </summary>
        public static Exchange FWB { get; private set; } = new Exchange(Country.DE, "FWB", "法蘭克福證券交易所");

        #endregion

        #region EU

        /// <summary>
        /// 歐洲期貨交易所
        /// </summary>
        public static Exchange EUREX { get; private set; } = new Exchange(Country.EU, "EUREX", "歐洲期貨交易所");

        #endregion

        #region Others

        /// <summary>
        /// TODO : 
        /// 國際匯率 
        /// </summary>
        public static Exchange FOREX { get; private set; } = new Exchange(Country.Others, "FOREX", "國際匯率");

        /// <summary>
        /// TODO : 與其他期貨交易所重複
        /// 國外期貨 
        /// </summary>
        public static Exchange FF { get; private set; } = new Exchange(Country.Others, "FF", "國外期貨");

        /// <summary>
        /// TODO : 待整理
        /// 未歸類其他
        /// </summary>
        public static Exchange Others { get; private set; } = new Exchange(Country.Others, "Others", "其他");

        #endregion

        #endregion

    }
}
