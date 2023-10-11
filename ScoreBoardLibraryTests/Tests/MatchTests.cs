using ScoreBoardLibrary.Classes;
using ScoreBoardLibrary.Enums;
using ScoreBoardLibraryTests;
namespace ScoreBoardLibraryTests;

public class MatchTests
{
    [Theory]
    [MemberData(nameof(TestData.TestDataWithoutScore), MemberType = typeof(TestData))]
    public void Create_Match_For_Valid_Input(string homeTeamName,string awayTeamName)
    {
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        Assert.Equal(match.HomeTeam.Name,homeTeamName);
        Assert.Equal(match.AwayTeam.Name,awayTeamName);
        Assert.Equal(MatchStatus.NOT_STARTED,match.Status);
    }
    [Fact]
    public void Create_Match_With_Null_Should_Throw_Exception()
    {
        #pragma warning disable CS8625 // For testing null
        Assert.Throws<Exception>(()=> new Match(null,null));
        #pragma warning disable CS8625 // For testing null
    }
    [Theory]
    [MemberData(nameof(TestData.TestDataAllTeams), MemberType = typeof(TestData))]
    public void Create_Match_With_Same_Team_Should_Throw_Exception(string teamName)
    {
        var team = new Team(teamName);
        Assert.Throws<Exception>(()=> new Match(team,team));
    }
    [Theory]
    [InlineData(0,-1)]
    [InlineData(-1,0)]
    [InlineData(999,-999)]
    [InlineData(-999,999)]
    [InlineData(int.MinValue,int.MinValue)]
    public void Update_Match_With_NegativeScore_Should_Throw_Exception(int homeScore,int awayScore)
    {
        var homeTeam = new Team("homeTeam");
        var awayTeam = new Team("awayTeam");
        var match = new Match(homeTeam,awayTeam);
        Assert.Throws<Exception>(()=> match.UpdateScore(homeScore,awayScore));
    }
    [Theory]
    [MemberData(nameof(TestData.TestDataWithoutScore), MemberType = typeof(TestData))]
    public void Start_Match_With_Valid_Input(string homeTeamName,string awayTeamName)
    {
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        match.StartMatch();
        Assert.Equal(MatchStatus.INPROGRESS,match.Status);
        Assert.Equal(DateTime.UtcNow,match.StartDateTime,TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Start_An_In_Progress_Match_Should_Throw_Exception()
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
    public void Finish_Match_Before_Start_Should_Throw_Exception()
    {
        var homeTeam = new Team("homeTeam");
        var awayTeam = new Team("awayTeam");
        var match = new Match(homeTeam,awayTeam);
        Assert.Throws<Exception>(()=> match.FinishMatch());
    }
    [Fact]
    public void Finish_A_Finished_Match_Should_Throw_Exception()
    {
        var homeTeam = new Team("homeTeam");
        var awayTeam = new Team("awayTeam");
        var match = new Match(homeTeam,awayTeam);
        match.StartMatch();
        match.FinishMatch();
        Assert.Throws<Exception>(()=> match.FinishMatch());
    }
    
    [Theory]
    [MemberData(nameof(TestData.TestDataWithScore), MemberType = typeof(TestData))]
    public void Update_Score_With_Valid_Input(string homeTeamName, int homeTeamScore, string awayTeamName, int awayTeamScore)
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
    [MemberData(nameof(TestData.TestDataWithoutScore), MemberType = typeof(TestData))]
    public void Finish_A_Started_Match(string homeTeamName,string awayTeamName)
    {
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        match.StartMatch();
        match.FinishMatch();
        Assert.Equal(MatchStatus.FINISHED,match.Status);

    }

    [Theory]
    [MemberData(nameof(TestData.TestDataWithoutScore), MemberType = typeof(TestData))]
    public void Get_Match_Name_With_Valid_Input(string homeTeamName,string awayTeamName)
    {
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        var matchName = homeTeamName + " - " + awayTeamName;
        Assert.Equal(matchName,match.GetMatchName());

    }


    [Theory]
    [MemberData(nameof(TestData.TestDataWithScore), MemberType = typeof(TestData))]
    public void Get_Match_Name_With_Score_With_Valid_Input(string homeTeamName, int homeTeamScore, string awayTeamName, int awayTeamScore)
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
    public void Update_Match_Score_With_Large_Number_Without_Exception(int homeScore,int awayScore)
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
    public void Update_Match_Score_With_Too_Large_Number_Should_Throw_Exception(int homeScore,int awayScore)
    {
        var homeTeam = new Team("homeTeam");
        var awayTeam = new Team("awayTeam");
        var match = new Match(homeTeam,awayTeam);
        Assert.Throws<Exception>(()=> match.UpdateScore(homeScore+1,awayScore-1));
    }

}
