using ScoreBoardLibrary.Interfaces;
using ScoreBoardLibrary.Enums;

namespace ScoreBoardLibrary.Classes;

public class ScoreBoard : IScoreBoard
{
    public string Name => throw new NotImplementedException();

    public List<System.Text.RegularExpressions.Match> MatchList => throw new NotImplementedException();

    List<IMatch> IScoreBoard.MatchList => throw new NotImplementedException();

    public void AddMatch(IMatch match)
    {
        throw new NotImplementedException();
    }

    public List<System.Text.RegularExpressions.Match> GetSummaryOfMatches()
    {
        throw new NotImplementedException();
    }

    List<IMatch> IScoreBoard.GetSummaryOfMatches()
    {
        throw new NotImplementedException();
    }

    public IMatch FindMatch(string matchName)
    {
        throw new NotImplementedException();
    }

    public ScoreBoard(string name){

    }
}
