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
        Status = MatchStatus.NOT_STARTED;
    }

    public void StartMatch(){
        Status = MatchStatus.INPROGRESS;
    }
    public void UpdateScore(int homeScore, int awayScore){
        HomeTeamScore = homeScore;
        AwayTeamScore = awayScore;
    }

    public void FinishMatch(){
        Status = MatchStatus.FINISHED;
    }

    public string GetMatchName()
    {
        return HomeTeam.Name + " - " + AwayTeam.Name;
    }

    public string GetMatchNameWithScore()
    {
        return string.Format("{0} {1} - {2} {3}"
        ,HomeTeam.Name 
        ,HomeTeamScore.ToString()
        ,AwayTeam.Name
        ,AwayTeamScore);
    }
}
