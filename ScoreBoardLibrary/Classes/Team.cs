using ScoreBoardLibrary.Interfaces;

namespace ScoreBoardLibrary.Classes;

public class Team : ITeam{

    public string Name {get; private set;}

    public Team(string name){
        if(string.IsNullOrEmpty(name)){
            throw new Exception("Team Name cannot be null!");
        }
        Name = name;
    }
}