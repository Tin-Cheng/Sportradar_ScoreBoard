using ScoreBoardLibrary.Classes;
using ScoreBoardLibrary.Enums;

namespace ScoreBoardLibraryTests;

public partial class Tests
{
    const string boardName = "WorldCup";
    [Theory]
    [InlineData(boardName)]
    public void CreateScoreBoard(string name){
        var scoreBoard = new ScoreBoard(name);
        Assert.Equal(name,scoreBoard.Name);
    }

    [Theory]
    [MemberData(nameof(TestDataWithoutScore))]
    public void CreateScoreBoardAndOneMatch(string homeTeamName,string awayTeamName)
    {
        
        var scoreBoard = new ScoreBoard(boardName);
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        scoreBoard.AddMatch(match);
        var matchName = homeTeam + " - " + awayTeamName;

        Assert.Single(scoreBoard.MatchList);
        Assert.Equal(match,scoreBoard.FindMatch(matchName));
    }

}
