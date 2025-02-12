using System.ComponentModel.DataAnnotations;

namespace Models;

public class RecommendationRequest
{
    public List<QuestionAnswer> Answers { get; set; } = [];
    
    public class QuestionAnswer
    {
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }
    }
}
