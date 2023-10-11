using ScoreBoardLibrary.Classes;

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
    }
}
