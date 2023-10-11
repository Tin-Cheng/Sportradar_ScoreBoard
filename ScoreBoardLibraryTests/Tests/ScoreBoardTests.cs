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
    public void CreateScoreBoardAndAddOneMatch(string homeTeamName,string awayTeamName)
    {
        var scoreBoard = new ScoreBoard(boardName);
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        scoreBoard.AddMatch(match);
        var matchName = homeTeamName + " - " + awayTeamName;

        Assert.Single(scoreBoard.MatchList);
        Assert.Equal(match,scoreBoard.FindMatch(matchName));
    }

    [Fact]
    public void CreateScoreBoard_AddTestMatches_StartGames_UpdateScores_GetSummary(){
        var scoreBoard = new ScoreBoard(boardName);
        foreach(object[] matchData in TestDataWithScore()){
            var newHomeTeam = new Team((string)matchData[0]);
            var newAwayTeam = new Team((string)matchData[2]);
            var newMatch = new Match(newHomeTeam,newAwayTeam);
            scoreBoard.AddMatch(newMatch);
            newMatch.StartMatch();
            newMatch.UpdateScore((int)matchData[1],(int)matchData[3]);
            System.Threading.Thread.Sleep(10);
        }

        var summaryList = scoreBoard.GetSummaryOfMatches().ToArray();
        var index = 0;
        foreach(object[] matchData in TestDataWithScoreInSummaryOrder()){
            var homeTeamName = (string)matchData[0];
            var awayTeamName = (string)matchData[2];
            var homeTeamScore = (int)matchData[1];
            var awayTeamScore = (int)matchData[3];

            Assert.Equal(homeTeamName,summaryList[index].HomeTeam.Name);
            Assert.Equal(awayTeamName,summaryList[index].AwayTeam.Name);
            Assert.Equal(homeTeamScore,summaryList[index].HomeTeamScore);
            Assert.Equal(awayTeamScore,summaryList[index].AwayTeamScore);
            index += 1;
        }
        Assert.Equal(index,summaryList.Length);
    }

    [Fact]
    public void CreateScoreBoard_AddTestMatches_StartGames_UpdateScores_FinishMatch_GetSummary(){
        var scoreBoard = new ScoreBoard(boardName);
        foreach(object[] matchData in TestDataWithScoreAndFinished()){
            var newHomeTeam = new Team((string)matchData[0]);
            var newAwayTeam = new Team((string)matchData[2]);
            var newMatch = new Match(newHomeTeam,newAwayTeam);
            scoreBoard.AddMatch(newMatch);
            newMatch.StartMatch();
            newMatch.UpdateScore((int)matchData[1],(int)matchData[3]);
            if((bool)matchData[4]){
                newMatch.FinishMatch();
            }
            System.Threading.Thread.Sleep(10);
        }

        var summaryList = scoreBoard.GetSummaryOfMatches().ToArray();
        var index = 0;
        foreach(object[] matchData in TestDataWithScoreAndFinishedInSummaryOrder()){
            var homeTeamName = (string)matchData[0];
            var awayTeamName = (string)matchData[2];
            var homeTeamScore = (int)matchData[1];
            var awayTeamScore = (int)matchData[3];

            Assert.Equal(homeTeamName,summaryList[index].HomeTeam.Name);
            Assert.Equal(awayTeamName,summaryList[index].AwayTeam.Name);
            Assert.Equal(homeTeamScore,summaryList[index].HomeTeamScore);
            Assert.Equal(awayTeamScore,summaryList[index].AwayTeamScore);
            index += 1;
        }
        Assert.Equal(index,summaryList.Length);
    }

}
