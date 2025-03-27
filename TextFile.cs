using System;
using System.Collections.Generic;
using System.IO;

public class TextFile
{
  private string _currentContent;
  private readonly Stack<TextFileMemento> _history = new Stack<TextFileMemento>();

  public string FilePath { get; }

  public string Content => _currentContent;

  public bool HasUnsavedChanges { get; private set; }

  public int HistoryCount => _history.Count;

  public TextFile(string filePath)
  {
    if (string.IsNullOrWhiteSpace(filePath))
      throw new ArgumentException("Путь к файлу не может быть пустым");

    FilePath = filePath;

    if (File.Exists(filePath))
    {
      _currentContent = File.ReadAllText(filePath);
    }
    else
    {
      _currentContent = string.Empty;
    }

    SaveState();
    HasUnsavedChanges = false;
  }

  public void Edit(string newContent)
  {
    if (newContent == null)
      throw new ArgumentNullException(nameof(newContent));

    if (_currentContent != newContent)
    {
      SaveState();
      _currentContent = newContent;
      HasUnsavedChanges = true;
    }
  }

  public bool Undo()
  {
    if (_history.Count <= 1)
      return false;

    _history.Pop();

    var previousState = _history.Peek();
    _currentContent = previousState.Content;
    HasUnsavedChanges = true;

    return true;
  }

  public void Save()
  {
    File.WriteAllText(FilePath, _currentContent);
    HasUnsavedChanges = false;

    while (_history.Count > 1)
    {
      _history.Pop();
    }
  }

  public void ShowHistory()
  {
    Console.WriteLine($"История изменений (всего {_history.Count} состояний):");

    int i = 0;
    foreach (var memento in _history)
    {
      Console.WriteLine($"[{i++}] {memento.Timestamp:HH:mm:ss} - {Truncate(memento.Content, 30)}");
    }
  }

  private void SaveState()
  {
    _history.Push(new TextFileMemento(_currentContent));
  }

  private static string Truncate(string value, int maxLength)
  {
    if (string.IsNullOrEmpty(value))
      return value;

    return value.Length <= maxLength ?
        value :
        value.Substring(0, maxLength) + "...";
  }
}

public class TextFileMemento
{
  public string Content { get; }
  public DateTime Timestamp { get; }

  public TextFileMemento(string content)
  {
    Content = content;
    Timestamp = DateTime.Now;
  }
}