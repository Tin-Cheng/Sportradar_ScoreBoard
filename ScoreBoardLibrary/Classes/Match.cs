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
    public DateTime StartDateTime{get; private set;}

    public Match(ITeam homeTeam,ITeam awayTeam){
        if(homeTeam == null || awayTeam == null){
            throw new Exception("Home Team and Away Team cannot be null!");
        }
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        Status = MatchStatus.NOT_STARTED;
    }

    public void StartMatch(DateTime? startDateTime = null){
        if(Status != MatchStatus.NOT_STARTED){
            throw new Exception("The match is started already!");
        }
        Status = MatchStatus.INPROGRESS;
        StartDateTime = startDateTime ?? DateTime.UtcNow;
    }
    public void UpdateScore(int homeScore, int awayScore){
        if(homeScore < 0 || awayScore < 0){
            throw new Exception("Scores should be lower then 0!");
        }
        HomeTeamScore = homeScore;
        AwayTeamScore = awayScore;
    }

    public void FinishMatch(){
        if(Status != MatchStatus.INPROGRESS){
            throw new Exception("The match is not in progress!");
        }
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
    public int GetTotalScore()
    {
        return HomeTeamScore + AwayTeamScore;
    }
}
