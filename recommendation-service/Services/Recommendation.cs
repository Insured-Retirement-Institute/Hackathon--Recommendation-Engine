using System.Text.Json.Serialization;

namespace Services;

public class Recommendation
{
    [JsonPropertyName("portfolio_allocation")]
    public Allocation PortfolioAllocation { get; set; }

    public class Allocation
    {
        [JsonPropertyName("primary_affinity_sector")]
        public Sector PrimaryAffinitySector { get; set; }

        [JsonPropertyName("growth_sectors")]
        public Sector GrowthSectors { get; set; }

        [JsonPropertyName("stable_income")]
        public Sector StableIncome { get; set; }

        [JsonPropertyName("international_exposure")]
        public Sector InternationalExposure { get; set; }

        [JsonPropertyName("fixed_income")]
        public Sector FixedIncome { get; set; }

        public List<Sector> AllSectors =>
        [
            PrimaryAffinitySector,
            GrowthSectors,
            StableIncome,
            InternationalExposure,
            FixedIncome
        ];
        
        public class Sector
        {
            [JsonPropertyName("weight")]
            public double Weight { get; set; }

            [JsonPropertyName("holdings")]
            public List<Holding> Holdings { get; set; } = [];
        }

        public class Holding
        {
            [JsonPropertyName("symbol")]
            public string Symbol { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("weight")]
            public double Weight { get; set; }

            [JsonPropertyName("category")]
            public string Category { get; set; }
        }

        [JsonPropertyName("risk_profile")]
        public string? RiskProfile { get; set; }

        [JsonPropertyName("esg_focus")]
        public string? EsgFocus { get; set; }
    }

    [JsonPropertyName("reason")]
    public string Reason { get; set; }
}