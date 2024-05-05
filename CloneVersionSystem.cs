using System.Collections.Generic;
using System;

namespace Clones;

public class CloneVersionSystem : ICloneVersionSystem
{
    public Dictionary<string, CloneSubject> cloneSubjects = new() { ["1"] = new CloneSubject()};
    public string Execute(string query)
	{
        string command = query[..4];
        switch (command)
        {
            case "lear":
                int index = query.LastIndexOf(" ");
                string key = query[6..index];
                string prog = query[(index + 1)..];
                CloneSubject temp = cloneSubjects[key];
                temp.Learn(prog);
                cloneSubjects[key] = temp;
                break;
            case "roll":
                break;
            case "rele":
                break;
            case "clon":
                break;
            case "check":
                break;
            default: return null;
        }

        return null;
	}
}

public class CloneSubject
{
    public LinkedStack<string> Knowledges = new();
    public LinkedStack<string> History = new();
    public CloneSubject()
    {
    }
    public void Learn (string prog)
    {
        Knowledges.Push(prog);
        History.Clear();
    }
    
    public void Rollback() 
    { 
        string prog = Knowledges.Pop();
        History.Push(prog);
    }
    public void Relearn () 
    {
        string prog = History.Pop();
        Knowledges.Push(prog);
    }
    static public CloneSubject Clone(LinkedStack<string> knowledges, LinkedStack<string> history)
    {
        CloneSubject clone = new()
        {
            Knowledges = knowledges,
            History = history
        };
        return clone;
    }

    public string Check()
    {
        if(Knowledges.Count == 0)
        {
            return "basic";
        }
        else
        {
            return Knowledges.Pop();
        }
    }
}

public class LinkedStack<T>
{
    private LinkedList<T> stack = new();
    public int Count => stack.Count;

    public LinkedStack()
    {
    }

    public void Push(T item)
    {
            stack.AddLast(item);
    }

    public T Pop()
    {
        if (stack.Count == 0) throw new InvalidCastException();
        T item = stack.Last.Value;
        stack.RemoveLast();
        return item;
    }

    public void Clear() => stack.Clear();
}