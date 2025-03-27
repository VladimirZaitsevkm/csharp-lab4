using System;

class Program
{
  static void Main(string[] args)
  {
    Console.WriteLine("Simple Text Editor with Undo Feature");
    Console.WriteLine("-----------------------------------");

    try
    {
      var editor = new TextEditor();
      editor.Run();
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Fatal error: {ex.Message}");
      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();
    }

    Console.WriteLine("\nThank you for using the text editor!");
  }
}