using System.Collections.Generic;
using System;

namespace Clones;

public class CloneVersionSystem : ICloneVersionSystem
{
    public Dictionary<string, CloneSubject> CloneSubjects = new() { ["1"] = new CloneSubject()};
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
                tempClone = CloneSubjects[key];
                tempClone.Learn(int.Parse(prog));
                CloneSubjects[key] = tempClone;
                break;
            case "roll":
                key = query[9..];
                tempClone= CloneSubjects[key];
                tempClone.Rollback();
                CloneSubjects[key] = tempClone;
                break;
            case "rele":
                key = query[8..];
                tempClone = CloneSubjects[key];
                tempClone.Relearn();
                CloneSubjects[key] = tempClone;
                break;
            case "clon":
                key = query[6..];
                CloneSubject newClone = CloneSubjects[key].Clone(CloneSubjects[key].Knowledges, CloneSubjects[key].History);
                CloneSubjects.Add((CloneSubjects.Count + 1).ToString(), newClone);
                break;
            case "chec":
                key = query[6..];
                string knowledge = CloneSubjects[key].Check();
                return knowledge;
            default: return null;
        }

        return null;
	}
}

public class StackItem
{
    public int Value { get; set; }
    public StackItem Previous { get; set; }
}

public class SimpleStack
{
    StackItem tail;

    public void Push(int value)
    {
        if (tail == null)
            tail = new StackItem { Value = value, Previous = null };
        else
        {
            var item = new StackItem { Value = value, Previous = tail };
            tail = item;
        }
    }

    public int Pop()
    {
        if (tail == null) throw new InvalidOperationException();
        var result = tail.Value;
        tail = tail.Previous;
        return result;
    }

    public int Peek()
    {
        if (tail == null) throw new InvalidOperationException();
        var result = tail.Value;
        return result;
    }

    public void Copy(SimpleStack simpleStack)
    {
        this.tail = simpleStack.tail;
    }

    public void Clear()
    {
        this.tail = null;
    }

    public bool IsEmpty()
    {
        if(this.tail == null)
        {
            return true;
        }

        return false;
    }
}

public class CloneSubject
{
    public SimpleStack Knowledges = new();
    public SimpleStack History = new();
    public CloneSubject()
    {
    }

    public void Learn(int prog)
    {
        Knowledges.Push(prog);
        History.Clear();
    }

    public void Rollback()
    {
        int prog = Knowledges.Pop();
        History.Push(prog);
    }

    public void Relearn()
    {
        int prog = History.Pop();
        Knowledges.Push(prog);
    }

    public CloneSubject Clone(SimpleStack knowledges, SimpleStack history)
    {
        CloneSubject clone = new();
        clone.Knowledges.Copy(knowledges);
        clone.History.Copy(history);
        return clone;
    }

    public string Check()
    {
        if (Knowledges.IsEmpty())
        {
            return "basic";
        }
        else
        {
            return Knowledges.Peek().ToString();
        }
    }
}