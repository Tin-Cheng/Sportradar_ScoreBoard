namespace ScoreBoardLibrary.Interfaces;

public interface IMatch{
    ITeam HomeTeam {get;}
    ITeam AwayTeam {get;}

    int HomeTeamScore {get;}
    int AwayTeamScore {get;}

}