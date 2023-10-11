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
        MatchList.Add(match);
    }

    public List<IMatch> GetSummaryOfMatches()
    {
        return MatchList.OrderByDescending(x => x.GetTotalScore())
                .ThenByDescending(x => x.StartDateTime).ToList();
    }

    public IMatch? FindMatch(string matchName)
    {
        return MatchList.Find(x => x.GetMatchName().Equals(matchName));
    }

}
