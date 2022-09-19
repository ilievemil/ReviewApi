namespace ReviewApi.Models {
    public interface IReviews {
        
        public Task<ReviewResult> GenerateAsync();
    }
}
