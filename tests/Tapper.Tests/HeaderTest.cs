using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Tapper.Test.SourceTypes.Space3;
using Xunit;
using Xunit.Abstractions;

namespace Tapper.Tests;

public class HeaderTest
{
    private readonly ITestOutputHelper _output;

    public HeaderTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Test_Header1()
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

        var targetTypes = compilation.GetSourceTypes(false);
        var targetTypeLookupTable = targetTypes.ToLookup<INamedTypeSymbol, INamespaceSymbol>(static x => x.ContainingNamespace, SymbolEqualityComparer.Default);

        var type = typeof(NastingNamespaceType);
        var typeSymbol = compilation.GetTypeByMetadataName(type.FullName!)!;

        var group = targetTypeLookupTable.Where(x => SymbolEqualityComparer.Default.Equals(x.Key, typeSymbol.ContainingNamespace)).First()!;

        var writer = new CodeWriter();

        codeGenerator.AddHeader(group, ref writer);

        var code = writer.ToString();
        var gt = @"/* THIS (.ts) FILE IS GENERATED BY Tapper */
/* eslint-disable */
/* tslint:disable */
import { CustomType } from './Tapper.Test.SourceTypes.Space1';
import { CustomType2, CustomType3 } from './Tapper.Test.SourceTypes.Space1.Sub';
import { CustomType4 } from './Tapper.Test.SourceTypes.Space2';

";

        _output.WriteLine(code);
        _output.WriteLine(gt);

        Assert.Equal(gt, code, ignoreLineEndingDifferences: true);
    }

    [Fact]
    public void Test_Header2()
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

        var targetTypes = compilation.GetSourceTypes(false);
        var targetTypeLookupTable = targetTypes.ToLookup<INamedTypeSymbol, INamespaceSymbol>(static x => x.ContainingNamespace, SymbolEqualityComparer.Default);

        var type = typeof(Space2.CustomType2);
        var typeSymbol = compilation.GetTypeByMetadataName(type.FullName!)!;

        var group = targetTypeLookupTable.Where(x => SymbolEqualityComparer.Default.Equals(x.Key, typeSymbol.ContainingNamespace)).First()!;

        var writer = new CodeWriter();

        codeGenerator.AddHeader(group, ref writer);

        var code = writer.ToString();
        var gt = @"/* THIS (.ts) FILE IS GENERATED BY Tapper */
/* eslint-disable */
/* tslint:disable */
import { CustomType1 } from './Space1';

";

        _output.WriteLine(code);
        _output.WriteLine(gt);

        Assert.Equal(gt, code, ignoreLineEndingDifferences: true);
    }
}
