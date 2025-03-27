using System;

public class TextEditor
{
  private TextFile _file;

  public void Run()
  {
    Console.WriteLine("Simple Text Editor (type 'exit' to quit)");

    while (true)
    {
      Console.Write("\nFile path (or 'new'): ");
      var input = Console.ReadLine();

      if (input == "exit") return;
      if (input == "new") CreateNewFile();
      else OpenFile(input);
    }
  }

  private void OpenFile(string path)
  {
    try
    {
      _file = new TextFile(path);
      Console.WriteLine($"Opened: {path}");
      ShowMenu();
    }
    catch { Console.WriteLine("Error opening file"); }
  }

  private void CreateNewFile()
  {
    Console.Write("New file path: ");
    var path = Console.ReadLine();
    _file = new TextFile(path);
    ShowMenu();
  }

  private void ShowMenu()
  {
    while (true)
    {
      Console.WriteLine("\n1. View 2. Edit 3. Save 4. Undo 5. Close");
      switch (Console.ReadLine())
      {
        case "1": Console.WriteLine(_file.Content); break;
        case "2": EditFile(); break;
        case "3": _file.Save(); Console.WriteLine("Saved!"); break;
        case "4": Console.WriteLine(_file.Undo() ? "Undone!" : "Can't undo"); break;
        case "5": return;
      }
    }
  }

  private void EditFile()
  {
    Console.WriteLine("Current text (press Enter then type 'end' to finish):\n" + _file.Content);
    var newText = "";
    string line;
    while ((line = Console.ReadLine()) != "end")
      newText += line + Environment.NewLine;

    _file.Edit(newText.Trim());
  }
}