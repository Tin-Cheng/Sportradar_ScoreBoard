
using ScoreBoardLibrary;
using ScoreBoardLibrary.Interfaces;
namespace ScoreBoardLibraryTests;

public class TeamTests
{
    [Theory]
    [InlineData("Mexico")]
    [InlineData("Canada")]
    public void CreateTeam(string name)
    {
        var team = new Team(name);
        Assert.Equal(name,team.Name);
    }
}
