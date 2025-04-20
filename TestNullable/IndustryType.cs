using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace TestNullable
{
    /// <summary>
    /// 產業分類
    /// </summary>
    public abstract class IndustryType
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        public IndustryType(int code, string name)
        {
            Code = code;
            Name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TWIndustryType : IndustryType
    {
        private static Dictionary<string, IndustryType> s_industryType = new Dictionary<string, IndustryType>();

        static TWIndustryType()
        {
            //ToDo: Load from config file?
            s_industryType.Add("1", new TWIndustryType(1, "水泥工業"));
            s_industryType.Add("2", new TWIndustryType(2, "食品工業"));
            s_industryType.Add("3", new TWIndustryType(3, "塑膠工業"));
            s_industryType.Add("4", new TWIndustryType(4, "紡織纖維"));
            s_industryType.Add("5", new TWIndustryType(5, "電機機械"));
            s_industryType.Add("6", new TWIndustryType(6, "電器電纜"));
            s_industryType.Add("7", new TWIndustryType(7, "保留"));
            s_industryType.Add("8", new TWIndustryType(8, "玻璃陶瓷"));
            s_industryType.Add("9", new TWIndustryType(9, "造紙工業"));
            s_industryType.Add("10", new TWIndustryType(10, "鋼鐵工業"));
            s_industryType.Add("11", new TWIndustryType(11, "橡膠工業"));
            s_industryType.Add("12", new TWIndustryType(12, "汽車工業"));
            s_industryType.Add("13", new TWIndustryType(13, "保留"));
            s_industryType.Add("14", new TWIndustryType(14, "建材營造"));
            s_industryType.Add("15", new TWIndustryType(15, "航運業"));
            s_industryType.Add("16", new TWIndustryType(16, "觀光事業"));
            s_industryType.Add("17", new TWIndustryType(17, "金融保險"));
            s_industryType.Add("18", new TWIndustryType(18, "貿易百貨"));
            s_industryType.Add("19", new TWIndustryType(19, "綜合"));
            s_industryType.Add("20", new TWIndustryType(20, "其他"));
            s_industryType.Add("21", new TWIndustryType(21, "化學工業"));
            s_industryType.Add("22", new TWIndustryType(22, "生技醫療業"));
            s_industryType.Add("23", new TWIndustryType(23, "油電燃氣業"));
            s_industryType.Add("24", new TWIndustryType(24, "半導體業"));
            s_industryType.Add("25", new TWIndustryType(25, "電腦及週邊設備業"));
            s_industryType.Add("26", new TWIndustryType(26, "光電業"));
            s_industryType.Add("27", new TWIndustryType(27, "通信網路業"));
            s_industryType.Add("28", new TWIndustryType(28, "電子零組件業"));
            s_industryType.Add("29", new TWIndustryType(29, "電子通路業"));
            s_industryType.Add("30", new TWIndustryType(30, "資訊服務業"));
            s_industryType.Add("31", new TWIndustryType(31, "其他電子業"));
            s_industryType.Add("32", new TWIndustryType(32, "文化創意業"));
            s_industryType.Add("33", new TWIndustryType(33, "農業科技"));
            s_industryType.Add("34", new TWIndustryType(34, "電子商務"));
            s_industryType.Add("80", new TWIndustryType(80, "管理股票"));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        public TWIndustryType(int code, string name) :
            base(code, name)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static IndustryType GetIndustryType(string code)
        {
            if (!s_industryType.ContainsKey(code))
            {
                //throw new ArgumentOutOfRangeException($"Get industry type failed, code:{code}");
                return null;
            }

            return s_industryType[code];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IndustryType> GetIndustryTypeList()
        {
            return s_industryType.Values;
        }
    }
}
