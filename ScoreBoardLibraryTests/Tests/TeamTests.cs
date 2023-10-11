
using ScoreBoardLibrary.Classes;
namespace ScoreBoardLibraryTests;

public class TeamTests
{
    [Theory]
    [MemberData(nameof(TestData.TestDataAllTeams), MemberType = typeof(TestData))]
    public void Create_Team_With_Valid_Input(string name)
    {
        var team = new Team(name);
        Assert.Equal(name,team.Name);
    }
    [Fact]
    public void Create_Team_With_Null_Should_Throw_Exception()
    {
        #pragma warning disable CS8625 // For testing null
        Assert.Throws<Exception>(()=> new Team(null));
        #pragma warning disable CS8625 // For testing null
    }
    [Fact]
    public void Create_Team_With_Empty_String_Should_Throw_Exception()
    {
        Assert.Throws<Exception>(()=> new Team(""));
    }
}
