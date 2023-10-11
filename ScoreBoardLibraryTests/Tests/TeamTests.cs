
using ScoreBoardLibrary.Classes;
namespace ScoreBoardLibraryTests;

public partial class Tests
{
    [Theory]
    [MemberData(nameof(TestDataAllTeams))]
    public void CreateTeam(string name)
    {
        var team = new Team(name);
        Assert.Equal(name,team.Name);
    }
    [Fact]
    public void CreateTeamWithNull_ShouldThrowException()
    {
        #pragma warning disable CS8625 // For testing null
        Assert.Throws<Exception>(()=> new Team(null));
        #pragma warning disable CS8625 // For testing null
    }
    [Fact]
    public void CreateTeamWithEmptyString_ShouldThrowException()
    {
        Assert.Throws<Exception>(()=> new Team(""));
    }
}
