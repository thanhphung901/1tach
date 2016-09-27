using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Cart_result_entity
    {
        public int Basket_quantity { get; set; }
        public decimal Basket_Price { get; set; }
        public decimal NEWS_PRICE1 { get; set; }
        public decimal NEWS_PRICE2 { get; set; }
        public int NEWS_ID { get; set; }
        public int UNIT_ID2 { get; set; }
        public string NEWS_SEO_URL { get; set; }
        public string NEWS_URL { get; set; }
        public string NEWS_IMAGE3 { get; set; }
        public string NEWS_TITLE { get; set; }
        public string CAT_SEO_URL { get; set; }
        public string BASKET_FIELD2 { get; set; }
        public string BASKET_FIELD3 { get; set; }
    }
}
