using System;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Tapper.Test.SourceTypes.Space3;
using Tapper.Tests.SourceTypes;
using Tapper.TypeMappers;
using Xunit;
using Xunit.Abstractions;

namespace Tapper.Tests;

public class NestedNamespaceTest
{
    private readonly ITestOutputHelper _output;

    public NestedNamespaceTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Test1()
    {
        var compilation = CompilationSingleton.Compilation;
        var codeGenerator = new TypeScriptCodeGenerator(compilation, Environment.NewLine, 2, SerializerOption.Json, NamingStyle.None, EnumNamingStyle.None, Logger.Empty);

        var type = typeof(NastingNamespaceType);
        var typeSymbol = compilation.GetTypeByMetadataName(type.FullName!)!;

        var writer = new CodeWriter();

        codeGenerator.AddType(typeSymbol, ref writer);

        var code = writer.ToString();
        var gt = @"/** Transpiled from Tapper.Test.SourceTypes.Space3.NastingNamespaceType */
export type NastingNamespaceType = {
  /** Transpiled from Tapper.Test.SourceTypes.Space1.CustomType? */
  Value?: CustomType;
  /** Transpiled from Tapper.Test.SourceTypes.Space1.Sub.CustomType2 */
  Name: CustomType2;
  /** Transpiled from Tapper.Test.SourceTypes.Space1.Sub.CustomType3? */
  Name2?: CustomType3;
  /** Transpiled from System.Collections.Generic.List<Tapper.Test.SourceTypes.Space2.CustomType4> */
  List: CustomType4[];
}
";

        _output.WriteLine(code);
        _output.WriteLine(gt);

        Assert.Equal(gt, code, ignoreLineEndingDifferences: true);
    }
}
