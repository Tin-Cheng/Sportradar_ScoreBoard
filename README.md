# Live Football World Cup Score Board

This is a simple implementation for a coding exercise.
Users of this library can create Teams, Matches, and add them to a live Scoreboard.

## Tech Stack
- .NET 7
- xUnit

## Assumptions
- There are no two teams of the same name
- If a team is involved in an unfinished match, no new match can be added to the score board. This is not a schedule table.
- When finish a match, "removes a match" from the scoreboard mean removing the match from Summary.
- Otherwise, match should reference to the scoreboard, and remove itself in the finish function.
- The number of matches will not be too large that it may cause performance issue
- There are 3 match status, Pending > In Progress > Finished
- Each Match will contains one home team and one away team
- One Score Board will have many matches
- One Score Board can generate many Summaries
  
- A more complete yet complex solution can be only allow pending match to be added into Score Board.
- Score Board then takes full control of matches, including: status, remove matches from the In-progress list and add them to an archived list, maintain an unsorted collection of in game teams, etc
- The above is not implemented to keep things simple.


## Usages

Include the classes and references in your project.
The following is an example to create objects, update matches score and status and retrieve summary.


```csharp
var scoreBoard = new ScoreBoard("FootballWorldCup");
var MexicoTeam = new Team("Mexico");
var CanadaTeam = new Team("Canada");
var match = new Match(MexicoTeam,CanadaTeam);
scoreBoard.AddAndStartMatch(match);
var searchedMatch = scoreBoard.FindMatch("Mexico - Canada");
searchedMatch.UpdateScore(0,5);

//Get all matches in order
List<IMatch> summaryList = scoreBoard.GetSummaryOfMatches();
List<Match> summaryList2 = summaryList.Cast<Match>().ToList();
string summary = scoreBoard.GetTextSummaryOfMatches(); //1. Mexico 0 - Canada 5

match.FinishMatch();
string summary2 = scoreBoard.GetTextSummaryOfMatches(); //empty string
```
Run "dotnet test" to check test.


