using System;
using System.Collections.Generic;

namespace PdEventBus.Utils;

public class CompositeDisposable : IDisposable
{
    private readonly Stack<IDisposable> _disposables = new();
    
    public void Add(IDisposable disposable)
    {
        _disposables.Push(disposable);
    }
    
    public void Dispose()
    {
        foreach (var disposable in _disposables)
        {
            disposable.Dispose();
        }
    }
}

public static class DisposableExtensions {

    public static void AddTo(this IDisposable disposable, CompositeDisposable composite)
    {
        composite.Add(disposable);
    }
    
    public static void AddTo(this IDisposable disposable, IDisposable another)
    {
        
    }
}