using MarkovSharp.TokenisationStrategies;
using Newtonsoft.Json;
using ReviewApi.Models;

namespace ReviewApi.Services {
    public class Reviews: IReviews {
        private readonly IConfiguration _config;
        const int trainingLines = 10000;
        StringMarkov model = new StringMarkov(3);

        public Reviews (IConfiguration config) {
            _config = config;
            string fileLocation = config["AppSettings:TrainDataFile"];
            string[] fromlines = DeserializeReviews(trainingLines, fileLocation);
            model.Learn(fromlines);
        }

        private string[] DeserializeReviews(int lines, string datalocation) {
            ReviewInput r;
            int i = 0;
            List<string> reviews = new List<string>();
            JsonSerializer serializer = new JsonSerializer();
            using (var strreader = new StreamReader(datalocation)) {
                using (var jsonReader = new JsonTextReader(strreader) { CloseInput = false, SupportMultipleContent = true }) {
                    while (i < lines && jsonReader.Read()) {
                        r = serializer.Deserialize<ReviewInput>(jsonReader);
                        reviews.Add(r.reviewText);
                        i++;
                    }
                }
            }
            return reviews.ToArray();
        }

        public async Task<ReviewResult> GenerateAsync() {
            Random rnd = new Random();
            ReviewResult r = new ReviewResult() {
                ReviewText = model.Walk().First(),
                Stars = rnd.Next(1, 6)
            };
            return await Task.FromResult<ReviewResult>(r);
        }
    }
}
