
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
}
