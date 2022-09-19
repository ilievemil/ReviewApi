namespace ReviewApi.Models {
    public class ReviewInput {
        public string reviewerID { get; set; }
        public string asin { get; set; }
        public string reviewText { get; set; }
        public string overall { get; set; }
    }
}