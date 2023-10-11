using ScoreBoardLibrary.Interfaces;
using ScoreBoardLibrary.Enums;

namespace ScoreBoardLibrary.Classes;

public class ScoreBoard : IScoreBoard
{
    public string Name {get; private set;}
    public List<IMatch> MatchList {get;private set;}

    public ScoreBoard(string name){
        Name = name;
        MatchList = new List<IMatch>();
    }    
    public void AddMatch(IMatch match)
    {
        if(MatchList.Any(
            x => x.Status != MatchStatus.FINISHED && 
            (
                x.HomeTeam.Name == match.HomeTeam.Name 
                || x.HomeTeam.Name == match.AwayTeam.Name 
                || x.AwayTeam.Name == match.HomeTeam.Name 
                || x.AwayTeam.Name == match.AwayTeam.Name
            )
        )){
            throw new Exception("Home Team and Away Team should not be in an unfinished match!");
        }
        MatchList.Add(match);
    }

    public void AddAndStartMatch(IMatch match){
        AddMatch(match);
        match.StartMatch();
    }

    public List<IMatch> GetSummaryOfMatches()
    {
        return MatchList.FindAll(x=>x.Status==MatchStatus.INPROGRESS)
                .OrderByDescending(x => x.GetTotalScore())
                .ThenByDescending(x => x.StartDateTime).ToList();
    }

    public IMatch? FindMatch(string matchName)
    {
        return MatchList.Find(x => x.GetMatchName().Equals(matchName));
    }
}
