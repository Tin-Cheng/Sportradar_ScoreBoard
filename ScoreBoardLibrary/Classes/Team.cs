using ScoreBoardLibrary.Interfaces;

namespace ScoreBoardLibrary.Classes;

public class Team : ITeam{

    public string Name {get; private set;}

    public Team(string name){
        Name = name;
    }
}