using ScoreBoardLibrary.Classes;
using ScoreBoardLibrary.Enums;
using ScoreBoardLibraryTests;
namespace ScoreBoardLibraryTests;

public partial class Tests
{
    [Theory]
    [MemberData(nameof(TestDataWithoutScore))]
    public void CreateMatch(string homeTeamName,string awayTeamName)
    {
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        Assert.Equal(match.HomeTeam.Name,homeTeamName);
        Assert.Equal(match.AwayTeam.Name,awayTeamName);
        Assert.Equal(MatchStatus.NOT_STARTED,match.Status);
    }
    [Theory]
    [MemberData(nameof(TestDataWithoutScore))]
    public void CreateMatchAndStartMatch(string homeTeamName,string awayTeamName)
    {
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        match.StartMatch();
        Assert.Equal(MatchStatus.INPROGRESS,match.Status);
    }
    [Theory]
    [MemberData(nameof(TestDataWithScore))]
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
    [MemberData(nameof(TestDataWithoutScore))]
    public void CreateMatchAndFinishMatch(string homeTeamName,string awayTeamName)
    {
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        match.FinishMatch();
        Assert.Equal(MatchStatus.FINISHED,match.Status);

    }

}
