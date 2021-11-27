using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Command;

public abstract class BaseCommandTest
{
    [TestMethod] public void NoSet() => CheckInvalid( "N1 # CMD()#");
    [TestMethod] public void NoCommand() => CheckInvalid( "N1 #set ()#");
    [TestMethod] public void NoParameterGroup() => CheckInvalid( "N1 #set CMD#");
    [TestMethod] public void NoClosing() => CheckInvalid( "N1 #set CMD()");
    [TestMethod] public void NoLineNumber() => CheckInvalid( "N #set CMD()#");
    [TestMethod] public void NoLineNumberN() => CheckInvalid( "1 #set CMD()#");
    [TestMethod] public void InvalidLineNumber() => CheckInvalid( "NA #set CMD()#");
    [TestMethod] public void NegativeLineNumber() => CheckInvalid( "N-1 #set CMD()#");
    [TestMethod] public void FractionalLineNumber() => CheckInvalid( "N1.5 #set CMD()#");
    [TestMethod] public void InvalidParam() => CheckInvalid( "N1 #set CMD(#)#");
    // TODO: check if the following test are valid / invalid
    //[TestMethod] public void NoSpacesBeforeSet() => CheckInvalid( "N1#set CMD()#");
    //[TestMethod] public void NoSpacesBeforeCommand() => CheckInvalid( "N1 #setCMD()#");
    //[TestMethod] public void LongLineNumber() => CheckInvalid( "N21474836470 #set CMD()#");
    //[TestMethod] public void LowerCaseN() => CheckInvalid( "n1 #set CMD()#");
    
    
    [TestMethod] public void EmptyCommand() => CheckValid( 1, "CMD", "", "", "N1 #set CMD()#");
    [TestMethod] public void EmptyCommandSpaces() => CheckValid( 1, "CMD", "", "", "N1 #set CMD(  )#");
    [TestMethod] public void LongCommandName() => CheckValid( 1, "Abcdefghijklmnopqrstuvwxyz0123456789", "", "", "N1 #set Abcdefghijklmnopqrstuvwxyz0123456789()#");
    [TestMethod] public void SpaceBeforeEnd() => CheckValid( 1, "CMD", "", "", "N1 #set CMD() #");
    [TestMethod] public void SingleParam() => CheckValid( 1, "CMD", "2", "", "N1 #set CMD(2)#");
    [TestMethod] public void SingleParamSpaces() => CheckValid( 1, "CMD", "2", "", "N1 #set CMD( 2 )#");
    [TestMethod] public void TwoParams() => CheckValid( 1, "CMD", "1;2", "", "N1 #set CMD(1;2)#");
    [TestMethod] public void TwoParamsSpacesInside() => CheckValid( 1, "CMD", "1 ;  2", "", "N1 #set CMD(1 ;  2)#");
    [TestMethod] public void TwoParamsSpacesOutside() => CheckValid( 1, "CMD", "1;2", "", "N1 #set CMD( 1;2  )#");
    [TestMethod] public void ThreeParams() => CheckValid( 1, "CMD", "1;2;3", "", "N1 #set CMD(1;2;3)#");
    [TestMethod] public void ThreeParamsR() => CheckValid( 1, "CMD", "1;2;R3", "", "N1 #set CMD(1;2;R3)#");
    [TestMethod] public void FourParams() => CheckValid( 1, "CMD", "1;2;3;4", "", "N1 #set CMD(1;2;3;4)#");
    [TestMethod] public void FourParamsR() => CheckValid( 1, "CMD", "1;2;3;R4", "", "N1 #set CMD(1;2;3;R4)#");
    [TestMethod] public void Comment() => CheckValid( 1, "CMD", "", ";comment", "N1 #set CMD()#;comment");
    [TestMethod] public void CommentSpaces() => CheckValid( 1, "CMD", "", ";comment", "N1 #set CMD()#  ;comment");
    [TestMethod] public void CommentParan() => CheckValid( 1, "CMD", "", "(comment)", "N1 #set CMD()#(comment)");
    [TestMethod] public void LineNumberZero() => CheckValid( 0, "CMD", "", "", "N0 #set CMD()#");
    [TestMethod] public void LineNumber1234() => CheckValid( 1234, "CMD", "", "", "N1234 #set CMD()#");
    [TestMethod] public void LineNumberMaxInt32() => CheckValid( 2147483647, "CMD", "", "", "N2147483647 #set CMD()#");
    [TestMethod] public void Benchmark() => CheckValid( 123450, "AcommandName", "1 ; 3.14  ; R4", "; Sets a command value", "N123450  #set AcommandName( 1 ; 3.14  ; R4)# ; Sets a command value");
    
    private void CheckValid(int expectedLineNumber, string expectedCommand, string expectedParameters, string expectedComment, string input)
    {
        bool result = TryParseCommand(input, out int lineNumber, out string command, out string parameters, out string comment);
        Assert.IsTrue(result);
        Assert.AreEqual(expectedLineNumber, lineNumber);
        Assert.AreEqual(expectedCommand, command);
        Assert.AreEqual(expectedParameters, parameters);
        Assert.AreEqual(expectedComment, comment);
    }

    private void CheckInvalid(string input)
    {
        bool result = TryParseCommand(input, out int _, out string _, out string _, out string _);
        Assert.IsFalse(result);
    }

    protected abstract bool TryParseCommand(string input, out int lineNumber, out string command, out string parameters, out string comment);
}