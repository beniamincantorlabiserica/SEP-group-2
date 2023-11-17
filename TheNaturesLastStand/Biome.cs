using System.Security.Cryptography.X509Certificates;

namespace TheNaturesLastStand
{
    public class Biome
    {

        public string? name { get; set; }
        public string? description { get; set; }
        public int? requiredScore { get; set; }
        public List<Location> locations {set; get;}

        public Biome(string name, string description, List<Location> locations, int requiredScoreToPass) {
            this.name = name;
            this.description = description;
            this.locations = locations;
            this.requiredScore = requiredScoreToPass;
        }

        public bool VerifyScore(int currentScore)
        {
            return requiredScore <= currentScore; 
        }

        public Location GetLocation(string destination) {
            return locations[1];
        }

    }
}