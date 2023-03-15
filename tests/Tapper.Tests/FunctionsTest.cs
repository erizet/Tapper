using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tapper.Test.SourceTypes;
using Xunit.Abstractions;
using Xunit;

namespace Tapper.Tests;
public class FunctionsTest
{
    private readonly ITestOutputHelper _output;

    public FunctionsTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Functions_is_included()
    {
        var compilation = CompilationSingleton.Compilation;

        var options = new TranspilationOptions(
            compilation,
            SerializerOption.Json,
            NamingStyle.None,
            EnumStyle.Value,
            NewLineOption.Lf,
            2,
            false,
            true
        );

        var codeGenerator = new TypeScriptCodeGenerator(compilation, options);

        var type = typeof(AttributeAnnotatedClassWithFunction);
        var typeSymbol = compilation.GetTypeByMetadataName(type.FullName!)!;

        var writer = new CodeWriter();

        codeGenerator.AddType(typeSymbol, ref writer);

        var code = writer.ToString();
        var gt = @"/** Transpiled from Tapper.Test.SourceTypes.AttributeAnnotatedClassWithFunction */
export type AttributeAnnotatedClassWithFunction = {
  /** Transpiled from Tapper.Test.SourceTypes.AttributeAnnotatedClassWithFunction.Add(int, int) */
  Add(x: number, y: number): number;
}
";
        _output.WriteLine(code);
        _output.WriteLine(gt);

        Assert.Equal(gt, code, ignoreLineEndingDifferences: true);
    }
}
