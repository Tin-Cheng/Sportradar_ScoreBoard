using ScoreBoardLibrary.Enums;
namespace ScoreBoardLibrary.Interfaces;

public interface IMatch{
    ITeam HomeTeam {get;}
    ITeam AwayTeam {get;}
    int HomeTeamScore {get;}
    int AwayTeamScore {get;}

    MatchStatus Status {get;}

    public void StartMatch();
    public void UpdateScore(int homeScore, int awayScore);
    public void FinishMatch();

    public string GetMatchName();
    public string GetMatchNameWithScore();
}