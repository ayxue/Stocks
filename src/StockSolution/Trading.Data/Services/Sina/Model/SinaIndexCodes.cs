using System;
using System.Collections.Generic;
using System.Text;

namespace Trading.Data.Services.Sina.Model
{
    // Source: http://vip.stock.finance.sina.com.cn/mkt/#hs_a
    public static class SinaInstrumentCategory
    {
        public static readonly string ShZh_A = "hs_a"; // 沪深A股
        public static readonly string Sh_Hgt = "hsgs_hgt_sh"; // 沪股通

        public static class Index
        {
            public static readonly string Index_Sh_50 = "zhishu_000016"; // 上证50
            public static readonly string Index_Sh_All = "zhishu_000001"; // 上证指数
            public static readonly string Index_Sz_Cyb = "zhishu_399006"; // 创业板
            public static readonly string Index_ShSz_300 = "hs300"; // 沪深300
            public static readonly string Index_ShSz_500 = "zhishu_000905"; // 中证500
        }
    }
}
