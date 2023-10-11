using ScoreBoardLibrary.Classes;
using ScoreBoardLibrary.Enums;

namespace ScoreBoardLibraryTests;

public class MatchTests
{
    [Theory]
    [InlineData("Mexico","Canada")]
    [InlineData("Spain","Brazil")]
    public void CreateMatch(string homeTeamName,string awayTeamName)
    {
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        Assert.Equal(match.HomeTeam.Name,homeTeamName);
        Assert.Equal(match.AwayTeam.Name,awayTeamName);
        Assert.Equal(match.Status,MatchStatus.NOT_STARTED);
    }
    [Theory]
    [InlineData("Mexico","Canada")]
    [InlineData("Spain","Brazil")]
    public void CreateMatchAndStartMatch(string homeTeamName,string awayTeamName)
    {
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        match.StartMatch();
        Assert.Equal(match.Status,MatchStatus.INPROGRESS);
    }
    [Theory]
    [InlineData("Mexico",0,"Canada",5)]
    [InlineData("Spain",10,"Brazil",2)]
    public void CreateMatchAndUpdateScore(string homeTeamName, int homeTeamScore, string awayTeamName, int awayTeamScore)
    {
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        match.UpdateScore(homeTeamScore,awayTeamScore);
        Assert.Equal(match.HomeTeamScore,homeTeamScore);
        Assert.Equal(match.AwayTeamScore,awayTeamScore);
    }

    [Theory]
    [InlineData("Mexico","Canada")]
    [InlineData("Spain","Brazil")]
    public void CreateMatchAndFinishMatch(string homeTeamName,string awayTeamName)
    {
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        match.FinishMatch();
        Assert.Equal(match.Status,MatchStatus.FINISHED);

    }

}
