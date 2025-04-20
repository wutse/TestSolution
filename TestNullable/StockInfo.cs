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
    public class StockInfoKey : IEquatable<StockInfoKey>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="country"></param>
        /// <param name="symbol"></param>
        public StockInfoKey(Country country, string symbol)
        {
            Country = country;
            Symbol = symbol;
        }

        public Country Country { get; set; }
        public string Symbol { get; set; }

        public bool Equals(StockInfoKey other)
        {
            return Country == other.Country && string.Equals(Symbol, other.Symbol);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Country.GetHashCode() ^ Symbol.GetHashCode();
        }

        /// <summary>
        /// TW:{Symbol}
        /// Others:{Symbol}.{Country}
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Country == Country.TW ? $"{Symbol}" : $"{Symbol}.{Country}";
        }
    }

    /// <summary>
    /// 商品資訊
    /// </summary>
    public class StockInfo
    {
        [Obsolete("待refactor後移除", false)]
        public StockInfo()
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="market"></param>
        /// <param name="symbol"></param>
        /// <param name="name"></param>
        public StockInfo(Market market, string symbol, string name)
        {
            Key = new StockInfoKey(market.Country, symbol);
            Market = market;
            StockID = symbol;
            StockName = name;
        }

        /// <summary>
        /// 
        /// </summary>
        public StockInfoKey Key { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public Country Country => Market.Exchange.Country;

        /// <summary>
        /// 交易所
        /// </summary>
        public Exchange Exchange => Market.Exchange;

        /// <summary>
        /// 
        /// </summary>
        public Market Market { get; set; }
        /// <summary>
        /// 股票代碼
        /// </summary>
        public string StockID { get; set; }
        /// <summary>
        /// 股票名稱
        /// </summary>
        public string StockName { get; set; }
        /// <summary>
        /// 更新時間(TimeStamp)
        /// </summary>
        public DateTime UpdatedTime { get; set; }

        /// <summary>
        /// 交易單位數(股)
        /// </summary>
        public int LotShares { get; set; } = 1;
        /// <summary>
        /// 產業別
        /// </summary>
        public IndustryType Industry { get; set; }
        /// <summary>
        /// 發行股數
        /// </summary>
        public long NumberOfShares { get; set; }
        /// <summary>
        /// 是否可交易
        /// </summary>
        public bool IsTradable { get; set; }
        /// <summary>
        /// 順序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 順序
        /// </summary>
        public int A { get; set; }
        /// <summary>
        /// 順序
        /// </summary>
        public int B { get; set; }
        /// <summary>
        /// 順序
        /// </summary>
        public int C { get; set; }
        /// <summary>
        /// 順序
        /// </summary>
        public int D { get; set; }

        /// <summary>
        /// 更新資訊
        /// </summary>
        /// <param name="info"></param>
        public virtual void Update(StockInfo info)
        {
            if (Market.IsBase && !info.Market.IsBase)
            {
                Market = info.Market;
                Market.IsBase = false;
            }

            Sort = info.Sort;
            UpdatedTime = info.UpdatedTime;
            NumberOfShares = info.NumberOfShares;
            LotShares = info.LotShares;

            UpdatedTime = DateTime.Now;
            //UpdatedTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TWStockInfo : StockInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="market"></param>
        /// <param name="symbol"></param>
        /// <param name="name"></param>
        public TWStockInfo(Market market, string symbol, string name) :
            base(market, symbol, name)
        {
        }

        /// <summary>
        /// 屬性2 
        /// Bit 1: 可轉換公司債 Bit 2: 附認股權公司債 Bit 3: 警示股 Bit 4: 指數記號 Bit 5: 期貨 Bit 6:個股選擇權 Bit 7: 指數選擇權 Bit 8: 保留
        /// </summary>
        public int StockType2 { get; set; }
        /// <summary>
        /// 融資成數
        /// </summary>
        public decimal MarginRatio { get; set; }
        /// <summary>
        /// 融券成數
        /// </summary>
        public decimal ShortRatio { get; set; }
        /// <summary>
        /// 融資餘額
        /// </summary>
        public int MarginHold { get; set; }
        /// <summary>
        /// 融券餘額
        /// </summary>
        public int ShortHlod { get; set; }
        /// <summary>
        /// 外資買賣超
        /// </summary>
        public int ForeignInvestorHold { get; set; }
        /// <summary>
        /// 投信買賣超
        /// </summary>
        public int InvestmentTrustHold { get; set; }
        /// <summary>
        /// 自營買賣超
        /// </summary>
        public int DealerHold { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MarketTargetSymbol { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AliasName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsForYuantaETF { get; set; }

        /// <summary>
        /// 更新資訊
        /// </summary>
        /// <param name="info"></param>
        public override void Update(StockInfo info)
        {
            if (info is TWStockInfo twInfo)
            {
                Industry = twInfo.Industry;
                MarginRatio = twInfo.MarginRatio;
                ShortRatio = twInfo.ShortRatio;
                MarginHold = twInfo.MarginHold;
                ShortHlod = twInfo.ShortHlod;
                ForeignInvestorHold = twInfo.ForeignInvestorHold;
                InvestmentTrustHold = twInfo.InvestmentTrustHold;
                DealerHold = twInfo.DealerHold;
            }

            base.Update(info);
        }
    }
}
