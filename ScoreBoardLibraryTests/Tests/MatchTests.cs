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
    [Fact]
    public void CreateMatchWithNull_ShouldThrowException()
    {
        #pragma warning disable CS8625 // For testing null
        Assert.Throws<Exception>(()=> new Match(null,null));
        #pragma warning disable CS8625 // For testing null
    }
    [Theory]
    [InlineData(0,-1)]
    [InlineData(-1,0)]
    [InlineData(999,-999)]
    [InlineData(-999,999)]
    [InlineData(int.MinValue,int.MinValue)]
    public void CreateMatch_UpdateWith_NegativeScore_ShouldThrowException(int homeScore,int awayScore)
    {
        var homeTeam = new Team("homeTeam");
        var awayTeam = new Team("awayTeam");
        var match = new Match(homeTeam,awayTeam);
        Assert.Throws<Exception>(()=> match.UpdateScore(homeScore,awayScore));
    }
    [Theory]
    [MemberData(nameof(TestDataWithoutScore))]
    public void CreateMatch_StartMatch(string homeTeamName,string awayTeamName)
    {
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        match.StartMatch();
        Assert.Equal(MatchStatus.INPROGRESS,match.Status);
        Assert.Equal(DateTime.UtcNow,match.StartDateTime,TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void CreateMatch_StartInProgressMatch_ShouldThrowException()
    {
        var homeTeam = new Team("homeTeam");
        var awayTeam = new Team("awayTeam");
        var match = new Match(homeTeam,awayTeam);
        match.StartMatch();
        Assert.Throws<Exception>(()=> match.StartMatch());
    }
    [Fact]
    public void CreateMatch_StartFinishedMatch_ShouldThrowException()
    {
        var homeTeam = new Team("homeTeam");
        var awayTeam = new Team("awayTeam");
        var match = new Match(homeTeam,awayTeam);
        match.StartMatch();
        match.FinishMatch();
        Assert.Throws<Exception>(()=> match.StartMatch());
    }
    [Fact]
    public void CreateMatch_FinishMatchBeforeStart_ShouldThrowException()
    {
        var homeTeam = new Team("homeTeam");
        var awayTeam = new Team("awayTeam");
        var match = new Match(homeTeam,awayTeam);
        Assert.Throws<Exception>(()=> match.FinishMatch());
    }
    [Fact]
    public void CreateMatch_FinishFinishedMatch_ShouldThrowException()
    {
        var homeTeam = new Team("homeTeam");
        var awayTeam = new Team("awayTeam");
        var match = new Match(homeTeam,awayTeam);
        match.StartMatch();
        match.FinishMatch();
        Assert.Throws<Exception>(()=> match.FinishMatch());
    }
    
    [Theory]
    [MemberData(nameof(TestDataWithScore))]
    public void CreateMatch_UpdateScore(string homeTeamName, int homeTeamScore, string awayTeamName, int awayTeamScore)
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
    public void CreateMatch_FinishMatch(string homeTeamName,string awayTeamName)
    {
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        match.StartMatch();
        match.FinishMatch();
        Assert.Equal(MatchStatus.FINISHED,match.Status);

    }

    [Theory]
    [MemberData(nameof(TestDataWithoutScore))]
    public void CreateMatch_GetMatchName(string homeTeamName,string awayTeamName)
    {
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        var matchName = homeTeamName + " - " + awayTeamName;
        Assert.Equal(matchName,match.GetMatchName());

    }


    [Theory]
    [MemberData(nameof(TestDataWithScore))]
    public void CreateMatch_GetMatchNameWithScore(string homeTeamName, int homeTeamScore, string awayTeamName, int awayTeamScore)
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
    [Theory]
    [InlineData(int.MaxValue,int.MaxValue)]
    public void CreateMatch_GetMatchNameWithScore_LargeNumber(int homeScore,int awayScore)
    {
        var homeTeam = new Team("homeTeam");
        var awayTeam = new Team("awayTeam");
        var match = new Match(homeTeam,awayTeam);
        var ex = Record.Exception(()=>match.UpdateScore(homeScore,awayScore));
        Assert.Null(ex);
    }
    [Theory]
    [InlineData(int.MaxValue,int.MaxValue)]
    [InlineData(int.MinValue,int.MinValue)]
    public void CreateMatch_GetMatchNameWithScore_TooLarge_ThrowException(int homeScore,int awayScore)
    {
        var homeTeam = new Team("homeTeam");
        var awayTeam = new Team("awayTeam");
        var match = new Match(homeTeam,awayTeam);
        Assert.Throws<Exception>(()=> match.UpdateScore(homeScore+1,awayScore-1));
    }

}
