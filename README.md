# RegexSpeedup

## Requiments
- .Net Framework 4.8 Developer pack
- .Net 5 SDK
- .Net 6 SDK

If missing download from: https://dotnet.microsoft.com/download/dotnet / https://dotnet.microsoft.com/download/dotnet-framework/net48

## How to

### Implement
Create a new implementation in the RegexSpeedup project. Feel free to create new classes / namespaces.

### Test
Test the new implementation by integrating it in the UnitTests project. A base test class (BaseWhiteSpaceTest) is already defined that contains may tests. CustomTest derived from this class. Call you new implementation in the RemoveAdditionalWhiteSpace function in the CustomTest class. Uncomment the [TestClass] attribute on the CustomTest class. Run the unit tests from you editor or command line ```dotnet test```.

### Benchmark
Once all test pass add the implementation to the benchmark project. Edit the CustomImplementation method in the WhitespaceBenchmark. Uncomment the [Benchmark] attribute on the method. Make sure the project is in Release mode. Compile, run, and wait for the results to show up :)
Or from the command line: ```dotnet run -c Release --project .\Benchmark\Benchmark.csproj --framework net6.0```
