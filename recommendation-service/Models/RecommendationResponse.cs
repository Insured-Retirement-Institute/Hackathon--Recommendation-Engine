using System.ComponentModel.DataAnnotations;

namespace Models;

public class RecommendationResponse
{
    public List<AllocationModel> Allocations { get; set; } = [];

    public string? Reason { get; set; }

    public class AllocationModel
    {
        public string AssetClass { get; set; }

        public string AssetId { get; set; }

        public string? AssetDisplayName { get; set; }

        public double AllocationPercentage { get; set; }
    }
}