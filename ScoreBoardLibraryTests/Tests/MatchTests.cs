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
        Assert.Equal(DateTime.UtcNow,match.StartDateTime,TimeSpan.FromSeconds(1));
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
        Assert.Equal(match.GetTotalScore(),homeTeamScore+awayTeamScore);
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

    [Theory]
    [MemberData(nameof(TestDataWithoutScore))]
    public void CreateMatchAndGetMatchName(string homeTeamName,string awayTeamName)
    {
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        var matchName = homeTeamName + " - " + awayTeamName;
        Assert.Equal(matchName,match.GetMatchName());

    }


    [Theory]
    [MemberData(nameof(TestDataWithScore))]
    public void CreateMatchAndGetMatchNameWithScore(string homeTeamName, int homeTeamScore, string awayTeamName, int awayTeamScore)
    {
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        match.UpdateScore(homeTeamScore,awayTeamScore);
        var matchName = homeTeamName + " - " + awayTeamName;
        var matchNameWithScore = string.Format("{0} {1} - {2} {3}"
        ,homeTeamName 
        ,homeTeamScore.ToString()
        ,awayTeamName
        ,awayTeamScore);
        Assert.Equal(matchNameWithScore,match.GetMatchNameWithScore());
    }

}
