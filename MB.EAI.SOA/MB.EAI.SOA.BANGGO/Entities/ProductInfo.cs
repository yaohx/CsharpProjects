using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace MB.EAI.SOA.BANGGO.Entities
{
    [DataContract]
    public class SkuVo
    {
        [DataMember]
        [JsonProperty( "goodsSn")]
        public string GoodsSn { get; set; }
        [DataMember]
        [JsonProperty( "colorCode")]
        public string ColorCode { get; set; }
        [DataMember]
        [JsonProperty( "colorName")]
        public string ColorName { get; set; }
        [DataMember]
        [JsonProperty( "sizeCode")]
        public string SizeCode { get; set; }
        [DataMember]
        [JsonProperty( "sizeName")]
        public string SizeName { get; set; }
        [DataMember]
        [JsonProperty( "custumCode")]
        public string CustumCode { get; set; }
        [DataMember]
        [JsonProperty( "barCode")]
        public string BarCode { get; set; }
        [DataMember]
        [JsonProperty( "opType")]
        public string OpType { get; set; }

    }
    [DataContract]
   public class ProductInfo
    {
       [DataMember]
       [JsonProperty("User")]
        public string User { get; set; }
       [DataMember]
       [JsonProperty( "goodsSn")]
        public string GoodsSn { get; set; }
       [DataMember]
       [JsonProperty( "goodsName")]
        public string GoodsName { get; set; }
       [DataMember]
       [JsonProperty( "goodsWeight")]
        public string GoodsWeight { get; set; }
       [DataMember]
       [JsonProperty( "marketPrice")]
        public string MarketPrice { get; set; }
       [DataMember]
       [JsonProperty( "brandCode")]
        public string BrandCode { get; set; }
       [DataMember]
       [JsonProperty( "themeCode")]
        public string ThemeCode { get; set; }
       [DataMember]
       [JsonProperty( "seasonCode")]
        public string SeasonCode { get; set; }
       [DataMember]
       [JsonProperty( "seriesCode")]
        public string SeriesCode { get; set; }
       [JsonProperty( "marketDate")]
        public string MarketDate { get; set; }
       [DataMember]
       [JsonProperty( "salePoint")]
        public string SalePoint { get; set; }
       [DataMember]
       [JsonProperty( "opType")]
        public string OpType { get; set; }
       [DataMember]
       [JsonProperty( "skuVos")]
        public List<SkuVo> SkuVos { get; set; }
    }
    [DataContract]
    public class ReturnMSG
    {
        [DataMember]
        public bool isok { get; set; }
         [DataMember]
        public string Message { get; set; }

    }
}
