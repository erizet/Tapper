using System;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Tapper.TypeMappers;

internal class TaskTypeMapper : ITypeMapper
{
    public ITypeSymbol Assign { get; }

    public TaskTypeMapper(Compilation compilation)
    {
        Assign = compilation.GetTypeByMetadataName("System.Threading.Tasks.Task`1")!;
    }

    public string MapTo(ITypeSymbol typeSymbol, ITranspilationOptions options)
    {
        if (typeSymbol is INamedTypeSymbol namedTypeSymbol
            && namedTypeSymbol.IsGenericType
            && SymbolEqualityComparer.Default.Equals(namedTypeSymbol.ConstructedFrom, Assign))
        {
            var mapper0 = options.TypeMapperProvider.GetTypeMapper(namedTypeSymbol.TypeArguments[0]);

            return $"{mapper0.MapTo(namedTypeSymbol.TypeArguments[0], options)}";
        }

        throw new InvalidOperationException($"TaskTypeMapper is not support {typeSymbol.ToDisplayString()}.");
    }
}

