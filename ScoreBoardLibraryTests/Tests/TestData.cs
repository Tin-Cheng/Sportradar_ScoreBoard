namespace ScoreBoardLibraryTests;

public partial class Tests
{
    public static IEnumerable<object[]> TestDataWithScore(){
        yield return new object[]{"Mexico",0,"Canada",5};
        yield return new object[]{"Spain",10,"Brazil",2};
        yield return new object[]{"Germany",2,"France",2};
        yield return new object[]{"Uruguay",6,"Italy",6};
        yield return new object[]{"Argentina",3,"Australia",1};
    }
    public static IEnumerable<object[]> TestDataWithoutScore(){
        yield return new object[]{"Mexico","Canada"};
        yield return new object[]{"Spain","Brazil"};
        yield return new object[]{"Germany","France"};
        yield return new object[]{"Uruguay","Italy"};
        yield return new object[]{"Argentina","Australia"};
    }

    public static IEnumerable<object[]> TestDataAllTeams(){
        yield return new object[]{"Mexico"};
        yield return new object[]{"Canada"};
        yield return new object[]{"Spain"};
        yield return new object[]{"Brazil"};
        yield return new object[]{"Germany"};
        yield return new object[]{"France"};
        yield return new object[]{"Uruguay"};
        yield return new object[]{"Italy"};
        yield return new object[]{"Argentina"};
        yield return new object[]{"Australia"};
    }

    public static IEnumerable<object[]> TestDataWithScoreInSummaryOrder(){
        yield return new object[]{"Uruguay",6,"Italy",6};
        yield return new object[]{"Spain",10,"Brazil",2};
        yield return new object[]{"Mexico",0,"Canada",5};
        yield return new object[]{"Argentina",3,"Australia",1};
        yield return new object[]{"Germany",2,"France",2};
    }
}
