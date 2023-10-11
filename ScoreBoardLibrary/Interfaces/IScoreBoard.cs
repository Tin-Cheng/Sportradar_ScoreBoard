using System.Text.RegularExpressions;

namespace ScoreBoardLibrary.Interfaces;

public interface IScoreBoard{
    string Name {get;}
    List<IMatch> MatchList{get;}
    void AddMatch(IMatch match);
    List<IMatch> GetSummaryOfMatches();

    IMatch FindMatch(string matchName);
}