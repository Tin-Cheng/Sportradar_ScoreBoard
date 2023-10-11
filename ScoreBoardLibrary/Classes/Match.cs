using ScoreBoardLibrary.Interfaces;
using ScoreBoardLibrary.Enums;

namespace ScoreBoardLibrary.Classes;

public class Match: IMatch
{
    public ITeam HomeTeam {get; private set;}
    public ITeam AwayTeam {get; private set;}
    public int HomeTeamScore {get; private set;}
    public int AwayTeamScore {get; private set;}
    public MatchStatus Status {get; private set;}

    public Match(ITeam homeTeam,ITeam awayTeam){
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
    }

    public void StartMatch(){

    }
    public void UpdateScore(int homeScore, int awayScore){

    }

    public void FinishMatch(){

    }

}
