using System.Text.RegularExpressions;

namespace ScoreBoardLibrary.Interfaces;

public interface IScoreBoard{
    string Name {get;}
    List<IMatch> MatchList{get;}
    void AddMatch(IMatch match);
    void AddAndStartMatch(IMatch match);
    List<IMatch> GetSummaryOfMatches();
    string GetTextSummaryOfMatches();

    IMatch? FindMatch(string matchName);
}