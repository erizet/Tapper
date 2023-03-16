using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MessagePack;

namespace Tapper.Test.SourceTypes;


[TranspilationSource]
public class ClassWithMethod
{
    public int Add(int x, int y)
    {
        return x + y;
    }
}

[TranspilationSource]
public class ClassWithMethodReturningTask
{
    public Task<int> Add(int x, int y)
    {
        return Task.FromResult(x + y);
    }
}

