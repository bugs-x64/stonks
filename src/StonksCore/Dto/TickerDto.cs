using Newtonsoft.Json;

namespace StonksCore.Dto
{
    public class TickerDto
    {
        [JsonProperty("figi")]
        public string Figi { get; set; }
        [JsonProperty("ticker")]
        public string Ticker { get; set; }
        [JsonProperty("isin")]
        public string Isin { get; set; }
        [JsonProperty("minPriceIncrement")]
        public double MinPriceIncrement { get; set; }
        [JsonProperty("lot")]
        public int Lot { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }

        public int IssuerId { get; set; }
    }
}