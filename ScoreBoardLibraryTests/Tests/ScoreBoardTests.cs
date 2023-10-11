using ScoreBoardLibrary.Classes;
using ScoreBoardLibrary.Enums;

namespace ScoreBoardLibraryTests;

public class ScoreBoardTests
{
    const string boardName = "WorldCup";
    [Theory]
    [InlineData(boardName)]
    public void Create_Score_Board_With_Valid_Input(string name){
        var scoreBoard = new ScoreBoard(name);
        Assert.Equal(name,scoreBoard.Name);
    }

    [Theory]
    [MemberData(nameof(TestData.TestDataWithoutScore), MemberType = typeof(TestData))]
    public void Add_One_Match_With_Valid_Input(string homeTeamName,string awayTeamName)
    {
        var scoreBoard = new ScoreBoard(boardName);
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var match = new Match(homeTeam,awayTeam);
        scoreBoard.AddAndStartMatch(match);
        var matchName = homeTeamName + " - " + awayTeamName;

        Assert.Single(scoreBoard.MatchList);
        Assert.Equal(match,scoreBoard.FindMatch(matchName));
    }
    
    [Theory]
    [MemberData(nameof(TestData.TestDataWithoutScore), MemberType = typeof(TestData))]
    public void Create_Matches_With_Same_Team_Without_Finishing_Should_Throw_Exception(string homeTeamName,string awayTeamName)
    {
        
        var scoreBoard = new ScoreBoard(boardName);
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var firstMatch = new Match(homeTeam,awayTeam);
        scoreBoard.AddAndStartMatch(firstMatch);
        var secondMatch = new Match(homeTeam,awayTeam);
        Assert.Throws<Exception>(()=>scoreBoard.AddAndStartMatch(secondMatch));
    }
    
    [Theory]
    [MemberData(nameof(TestData.TestDataWithoutScore), MemberType = typeof(TestData))]
    public void Create_Matches_With_Same_Team_With_Finishing_With_Valid_Input(string homeTeamName,string awayTeamName)
    {
        
        var scoreBoard = new ScoreBoard(boardName);
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);

        var firstMatch = new Match(homeTeam,awayTeam);
        scoreBoard.AddAndStartMatch(firstMatch);
        firstMatch.FinishMatch();
        var secondMatch = new Match(homeTeam,awayTeam);
        var ex = Record.Exception(()=>scoreBoard.AddAndStartMatch(secondMatch));
        Assert.Null(ex);
    }

    
    [Theory]
    [MemberData(nameof(TestData.TestDataWithoutScore), MemberType = typeof(TestData))]
    public void Create_Match_But_Home_Team_Not_Finished_Should_Throw_Exception(string homeTeamName,string awayTeamName)
    {
        
        var scoreBoard = new ScoreBoard(boardName);
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);
        var fooTeam = new Team("foo");

        var firstMatch = new Match(homeTeam,awayTeam);
        scoreBoard.AddAndStartMatch(firstMatch);
        var secondMatch = new Match(homeTeam,fooTeam);
        Assert.Throws<Exception>(()=>scoreBoard.AddAndStartMatch(secondMatch));
    }
    [Theory]
    [MemberData(nameof(TestData.TestDataWithoutScore), MemberType = typeof(TestData))]
    public void Create_Match_But_Away_Team_Not_Finished_Should_Throw_Exception(string homeTeamName,string awayTeamName)
    {
        
        var scoreBoard = new ScoreBoard(boardName);
        var homeTeam = new Team(homeTeamName);
        var awayTeam = new Team(awayTeamName);
        var fooTeam = new Team("foo");

        var firstMatch = new Match(homeTeam,awayTeam);
        scoreBoard.AddAndStartMatch(firstMatch);
        var secondMatch = new Match(fooTeam,awayTeam);
        Assert.Throws<Exception>(()=>scoreBoard.AddAndStartMatch(secondMatch));
    }

    [Fact]
    public void Start_Games_Update_Scores_Get_Summary_With_Valid_Inputs(){
        var scoreBoard = new ScoreBoard(boardName);
        foreach(object[] matchData in TestData.TestDataWithScore()){
            var newHomeTeam = new Team((string)matchData[0]);
            var newAwayTeam = new Team((string)matchData[2]);
            var newMatch = new Match(newHomeTeam,newAwayTeam);
            scoreBoard.AddAndStartMatch(newMatch);
            newMatch.UpdateScore((int)matchData[1],(int)matchData[3]);
            System.Threading.Thread.Sleep(10);
        }

        var summaryList = scoreBoard.GetSummaryOfMatches().ToArray();
        var index = 0;
        foreach(object[] matchData in TestData.TestDataWithScoreInSummaryOrder()){
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
        var stringResult = "1. Uruguay 6 - Italy 6 \n2. Spain 10 - Brazil 2 \n3. Mexico 0 - Canada 5 \n4. Argentina 3 - Australia 1 \n5. Germany 2 - France 2 \n";
        Assert.Equal(stringResult,scoreBoard.GetTextSummaryOfMatches());
    }

    [Fact]
    public void Start_Games_Update_Scores_Finish_Partial_Matches_Get_Summary_With_Valid_Inputs(){
        var scoreBoard = new ScoreBoard(boardName);
        foreach(object[] matchData in TestData.TestDataWithScoreAndFinished()){
            var newHomeTeam = new Team((string)matchData[0]);
            var newAwayTeam = new Team((string)matchData[2]);
            var newMatch = new Match(newHomeTeam,newAwayTeam);
            scoreBoard.AddAndStartMatch(newMatch);
            newMatch.UpdateScore((int)matchData[1],(int)matchData[3]);
            if((bool)matchData[4]){
                newMatch.FinishMatch();
            }
            System.Threading.Thread.Sleep(10);
        }

        var summaryList = scoreBoard.GetSummaryOfMatches().ToArray();
        var index = 0;
        foreach(object[] matchData in TestData.TestDataWithScoreAndFinishedInSummaryOrder()){
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
        var stringResult = "1. Uruguay 6 - Italy 6 \n2. Mexico 0 - Canada 5 \n3. Argentina 3 - Australia 1 \n";
        Assert.Equal(stringResult,scoreBoard.GetTextSummaryOfMatches());
    }
}
