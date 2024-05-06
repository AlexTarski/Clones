using System.Collections.Generic;
using System;

namespace Clones;

public class CloneVersionSystem : ICloneVersionSystem
{
    public Dictionary<string, CloneSubject> cloneSubjects = new() { ["1"] = new CloneSubject()};
    public string Execute(string query)
	{
        string command = query[..4];
        string key;
        CloneSubject tempClone;

        switch (command)
        {
            case "lear":
                int index = query.LastIndexOf(" ");
                key = query[6..index];
                string prog = query[(index + 1)..];
                tempClone = cloneSubjects[key];
                tempClone.Learn(int.Parse(prog));
                cloneSubjects[key] = tempClone;
                break;
            case "roll":
                key = query[9..];
                tempClone= cloneSubjects[key];
                tempClone.Rollback();
                cloneSubjects[key] = tempClone;
                break;
            case "rele":
                key = query[8..];
                tempClone = cloneSubjects[key];
                tempClone.Relearn();
                cloneSubjects[key] = tempClone;
                break;
            case "clon":
                key = query[6..];
                CloneSubject newClone = cloneSubjects[key].Clone(cloneSubjects[key].Knowledges, cloneSubjects[key].History);
                cloneSubjects.Add((cloneSubjects.Count + 1).ToString(), newClone);
                break;
            case "chec":
                key = query[6..];
                string knowledge = cloneSubjects[key].Check();
                return knowledge;
            default: return null;
        }

        return null;
	}
}

public class CloneSubject
{
    public LinkedStack<int> Knowledges = new();
    public LinkedStack<int> History = new();
    public CloneSubject()
    {
    }
    public void Learn (int prog)
    {
        Knowledges.Push(prog);
        History.Clear();
    }
    
    public void Rollback() 
    { 
        int prog = Knowledges.Pop();
        History.Push(prog);
    }
    public void Relearn () 
    {
        int prog = History.Pop();
        Knowledges.Push(prog);
    }
    public CloneSubject Clone(LinkedStack<int> knowledges, LinkedStack<int> history)
    {
        CloneSubject clone = new();
        clone.Knowledges = knowledges.Copy();
        clone.History = history.Copy();
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
            return Knowledges.Peek().ToString();
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

    public T Peek()
    {
        if (stack.Count == 0) throw new InvalidCastException();
        T item = stack.Last.Value;
        return item;
    }

    public LinkedStack<T> Copy()
    {
        LinkedStack<T> copy = new();
        copy.stack = new LinkedList<T>(this.stack);
        return copy;
    }
    public void Clear() => stack.Clear();
}