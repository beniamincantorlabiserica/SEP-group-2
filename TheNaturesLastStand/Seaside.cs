using System.Security.Cryptography.X509Certificates;

namespace TheNaturesLastStand
{

public abstract class Biome
{
    public string name;
    public string description;
    public List<Location> locations;
    public abstract bool VerifyScore(int currentScore);
}



    public class Seaside : Biome
    {

        string? name { get; set; }
        string? description { get; set; }
        int? requiredScore { get; set; }
        List<Location> locations {set; get;}

        public Seaside(string name, string description, List<Location> locations, int requiredScoreToPass) {
            this.name = name;
            this.description = description;
            this.locations = locations;
            this.requiredScore = requiredScoreToPass;
        }

        public override bool VerifyScore(int currentScore)
        {
            return requiredScore <= currentScore; 
        }

        public Location GetLocation(string destination) {
            return locations[1];
        }

    }
}