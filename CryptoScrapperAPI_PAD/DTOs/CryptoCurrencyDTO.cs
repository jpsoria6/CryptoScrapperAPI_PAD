namespace CryptoScrapperAPI_PAD.DTOs
{
    public class CryptoCurrencyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Slug { get; set; }
        public int CmcRank { get; set; }
        public int NumMarketPairs { get; set; }
        public double CirculatingSupply { get; set; }
        public double TotalSupply { get; set; }
        public double MaxSupply { get; set; }
        public bool InfiniteSupply { get; set; }
        public DateTime LastUpdated { get; set; }
        public DateTime DateAdded { get; set; }
        public List<string> Tags { get; set; }
        public dynamic Platform { get; set; }
        public double? SelfReportedCirculatingSupply { get; set; }
        public double? SelfReportedMarketCap { get; set; }
        public Quote Quote { get; set; }
    }

    public class Quote
    {
        public USD USD { get; set; }
    }

    public class USD
    {
        public dynamic price { get; set; }
        public dynamic volume_24h { get; set; }
        public dynamic volume_change_24h { get; set; }
        public dynamic percent_change_1h { get; set; }
        public dynamic percent_change_24h { get; set; }
        public dynamic percent_change_7d { get; set; }
        public dynamic percent_change_30d { get; set; }
        public dynamic percent_change_60d { get; set; }
        public dynamic percent_change_90d { get; set; }
        public dynamic market_cap { get; set; }
        public dynamic market_cap_dominance { get; set; }
        public dynamic fully_diluted_market_cap { get; set; }
        public dynamic tvl { get; set; }
        public dynamic last_updated { get; set; }
    }
}
