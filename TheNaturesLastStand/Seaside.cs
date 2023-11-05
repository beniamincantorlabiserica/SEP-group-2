using System.Security.Cryptography.X509Certificates;

namespace TheNaturesLastStand
{

interface IBiome
{
    string Name(string BiomeName);
    string Description(string BiomeDescription);
//  location.Locations(); probably has to be done by the person coding the location class
    int ScoreNeededToPass(int CurrentScore, int RequiredScore);

}


    public class Seaside : IBiome
    {

        string? BiomeDescription { get; set; }
        string? BiomeName { get; set; }
        string? CurrentBiome { get; set; }
        int? RequiredScore { get; set; }
        int? CurrentScore { get; set; }
    //  location.Locations(); probably needs to be implemented by the person doing location class? since it doesn't exist yet in main

        public string Name(string BiomeName)
        {
            Console.WriteLine("\nCurrent biome: {0} ", BiomeName);
            return CurrentBiome = BiomeName;
        }

        public string Description(string BiomeDescription)
        {
            Console.WriteLine("\n--- {0} ---\n",BiomeDescription);
            return BiomeDescription;
        }

        public int ScoreNeededToPass(int CurrentScore, int RequiredScore)
        {
            if (RequiredScore > 1)
            {
                Console.WriteLine("To advance to the next biome you need {0} more score points! ",RequiredScore - CurrentScore);
            }
            else
            {
                RequiredScore = 10;
                return RequiredScore;
            }
            
        return RequiredScore;
        
        }

    }
}