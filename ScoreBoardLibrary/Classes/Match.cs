using ScoreBoardLibrary.Interfaces;

namespace ScoreBoardLibrary.Classes;

public class Match
{
    public Team HomeTeam {get;private set;}
    public Team AwayTeam {get;private set;}

    public int HomeTeamScore {get;private set;}
    public int AwayTeamScore {get;private set;}

    public Match(Team homeTeam,Team awayTeam){

    }

}
